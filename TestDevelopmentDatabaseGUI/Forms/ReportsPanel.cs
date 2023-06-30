using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TestDevelopmentDatabaseGUI.Models;
using System.Linq;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace TestDevelopmentDatabaseGUI.Forms
{
    public partial class ReportsPanel : Form
    {
        GlobalObjects _globalObjects = GlobalObjects.Instance;
        GlobalConfig _globalConfig = GlobalConfig.Instance;

        string header_template_path = Path.Combine(Application.StartupPath, "Resources\\pdf_templates\\header_template_for_optiview.pdf");

        public ReportsPanel()
        {
            InitializeComponent();

            if (!(_globalConfig.sqlConfig.SelectedDatabase == "OptiviewDb"))
            {
                MessageBox.Show($"The Database '{_globalConfig.sqlConfig.SelectedDatabase}' doesn't support report creation", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                this.Dispose();
                return;
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {

            DataTable tb = _globalObjects.sqlConnHandler.Search(new List<SearchTag> { new SearchTag("E28.SerialNumber", typeof(string), tbxSearchId.Text) }, "AND");

            DataTable newTable = new DataTable();

            // Clone the structure (i.e., get only the schema) of the source DataTable for the first 5 columns
            for (int i = 0; i < 4; i++)
            {
                newTable.Columns.Add(tb.Columns[i].ColumnName, tb.Columns[i].DataType);
            }

            // Now, we copy the data from the source DataTable to the new one.
            foreach (DataRow row in tb.Rows)
            {
                DataRow newRow = newTable.NewRow();
                for (int i = 0; i < 4; i++)
                {
                    newRow[i] = row[i];
                }
                newTable.Rows.Add(newRow);
            }

            dataGridView1.DataSource = newTable;
        }

        private DataTable GenerateTestRecordTable(string timestamp)
        {

            DataTable tb = _globalObjects.sqlConnHandler.Search(new List<SearchTag> { new SearchTag("Timestamp", typeof(SingleDatetime), DateTime.Parse("6/27/2023 12:35:20 PM").ToString("yyyy-MM-ddTHH:mm:ss")) }, "AND");

            DataRow row = tb.Rows[0];

            var values = row.ItemArray.Cast<object>().ToList();

            var selectedValues = values
            .Skip(4)
            .Select(value => value is bool boolValue ? (boolValue ? "PASS" : "FAIL") : value.ToString())
            .ToList();

            DataTable newTable = new DataTable();
            newTable.Columns.Add("Test Name", typeof(string));
            newTable.Columns.Add("Test Value", typeof(object));

            var columnNames = tb.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
            var selectedColumnNames = columnNames.Skip(4).ToList();

            for (int i = 0; i < selectedColumnNames.Count; i++)
            {
                newTable.Rows.Add(selectedColumnNames[i], selectedValues[i]);
            }
            return newTable;
        }


        private void ReportsPanel_Load(object sender, EventArgs e)
        {

        }

        private void btnPdfExport_Click(object sender, EventArgs e)
        {


            if(dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show($"Please, first select a single row to create its report, selected row amount: {dataGridView1.SelectedRows.Count}", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            // Open the existing PDF file
            PdfReader reader = new PdfReader(header_template_path);
            PdfStamper stamper = new PdfStamper(reader, new FileStream("output.pdf", FileMode.Create));

            // Get the AcroFields from the PDF
            AcroFields formFields = stamper.AcroFields;

            // Fill the text fields with values


            DateTime dt;
            string newDateStr = "";
            if (DateTime.TryParseExact(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), "M/d/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                // Convert DateTime to desired format
                newDateStr = dt.ToString("ddd, MMM dd, yyyy");

                Console.WriteLine(newDateStr);  // Outputs: Tue, Jun 27, 2023
            }
            else
            {
                Console.WriteLine("Invalid date format");
            }

            formFields.SetFieldProperty("Model", "textsize", 10f, null);
            formFields.SetField("Model", _globalConfig.sqlConfig.TableName);

            formFields.SetFieldProperty("PartNumber", "textsize", 10f, null);
            formFields.SetField("PartNumber", dataGridView1.SelectedRows[0].Cells[3].Value.ToString());

            formFields.SetFieldProperty("SerialNumber", "textsize", 10f, null);
            formFields.SetField("SerialNumber", dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

            formFields.SetFieldProperty("EmployeeNumber", "textsize", 10f, null);
            formFields.SetField("EmployeeNumber", dataGridView1.SelectedRows[0].Cells[1].Value.ToString());

            formFields.SetFieldProperty("IssueDate", "textsize", 10f, null);
            formFields.SetField("IssueDate", DateTime.Now.ToString("MM/dd/yyyy"));

            formFields.SetFieldProperty("TestDate", "textsize", 10f, null);
            formFields.SetField("TestDate", newDateStr);

            

            // Close the stamper and reader
            stamper.Close();
            reader.Close();




            if (dataGridView1.ColumnCount == 0 || dataGridView1.RowCount == 0)
            {
                MessageBox.Show("No data available for export.");
                return;
            }
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.FileName = "*.pdf";
            savefiledialog.Filter = "PDF Files|*.pdf";
            savefiledialog.DefaultExt = "pdf";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                //TablePdfReport s = new TablePdfReport(savefiledialog.FileName, true);
                TablePdfReport s = new TablePdfReport("output1.pdf");
                s.Create(GenerateTestRecordTable(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                MessageBox.Show("Report generated.", "Generate PDF report", MessageBoxButtons.OK, MessageBoxIcon.Information);

                List<string> pdfs = new List<string>() { "output.pdf", "output1.pdf" };


                MergePdfFiles(pdfs, savefiledialog.FileName);

            }


        }


        static void MergePdfFiles(List<string> inputPdfs, string outputPdf)
        {
            // create PdfReader objects for each input pdf
            List<PdfReader> readers = new List<PdfReader>();
            foreach (string pdf in inputPdfs)
            {
                readers.Add(new PdfReader(pdf));
            }

            // create output stream for merged pdf
            using (FileStream stream = new FileStream(outputPdf, FileMode.Create))
            {
                Document doc = new Document();
                PdfCopy copy = new PdfCopy(doc, stream);
                doc.Open();

                // iterate through all PdfReaders
                foreach (PdfReader reader in readers)
                {
                    // copy each page from each PdfReader to the output pdf
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        copy.AddPage(copy.GetImportedPage(reader, i));
                    }

                    reader.Close();
                }

                doc.Close();
            }
        }
    }
}
