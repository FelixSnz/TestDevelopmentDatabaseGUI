using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDevelopmentDatabaseGUI.Forms
{
    public partial class SequenceEditorPanel : Form
    {


        private Dictionary<string, Type> columnTypes = new Dictionary<string, Type>
        {
            {"TestName", typeof(string)},
            {"TestDescription", typeof(string)},
            {"Units", typeof(string)},
            {"Expected", typeof(float)},
            {"Actual", typeof(float)},
            {"Tolerance", typeof(float)},

        };
        public SequenceEditorPanel()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //return;
            var columnName = dataGridView1.Columns[e.ColumnIndex].Name;


            if (!IsColumnValueValid(columnName, e.FormattedValue, e.RowIndex))
            {
                e.Cancel = true;
            }

            
        }

        private bool IsColumnValueValid(string ColumnName, object Value, int rowIndex)
        {


            switch (ColumnName)
            {
                case "Expected":
                case "Actual":
                    // Check if the input is a valid integer
                    if (!int.TryParse((string)Value, out int newValueInt))
                    {

                        MessageBox.Show($"Please enter a valid integer value in row {rowIndex + 1}, column '{ColumnName}'.");
                        return false;
                    }
                    // Check if the value is in the desired range
                    else if (newValueInt < -100 || newValueInt > 100)
                    {
                        MessageBox.Show($"Please enter a value between -100 and 100 in row {rowIndex + 1}, column '{ColumnName}'.");
                        return false;
                    }
                    break;

                case "Tolerance":
                    // Check if the input is a valid float
                    if (!float.TryParse((string)Value, out float newValueFloat))
                    {
                        MessageBox.Show($"Please enter a valid float value in row {rowIndex + 1}, column '{ColumnName}'.");
                        return false;
                    }
                    // Check if the value is in the desired range
                    else if (newValueFloat < 0.01f || newValueFloat > 0.2f)
                    {
                        MessageBox.Show($"Please enter a value between 0.01 and 0.2 in row {rowIndex + 1}, column '{ColumnName}'.");
                        return false;
                    }
                    break;
                default:
                    // Check if the cell is empty
                    if (string.IsNullOrWhiteSpace((string)Value))
                    {
                        MessageBox.Show($"Cell cannot be empty in row {rowIndex + 1}, column '{ColumnName}'.");
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Skip the new row at the end
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        var columnName = dataGridView1.Columns[cell.ColumnIndex].Name;

                        // Call your validation method
                        if (!IsColumnValueValid(columnName, cell.Value, row.Index))
                        {
                            // If a validation fails, stop checking the rest of the cells
                            return;
                        }
                    }
                }
            }

            MessageBox.Show("All cell values are valid!");



            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                // Save your XML data to the file using the fileName variable

                CreateXmlSequenceFile(fileName);
            }



        }


        private void CreateXmlSequenceFile(string filename)
        {


            DataSet ds = new DataSet("MyDataSet");
            DataTable dt = new DataTable("MyDataTable");

            // Create columns in your DataTable to match the columns in your DataGridView
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column != null)
                {
                    Console.WriteLine($"{column.Name} : {column.ValueType}");
                    Console.WriteLine($"{column.Name} : {columnTypes[column.Name]}");
                    Console.WriteLine($"-----------------------------------");
                    dt.Columns.Add(column.Name, columnTypes[column.Name]);
                }

            }

            // Populate your DataTable with data from your DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Skip the new row at the end
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {

                        float tempFloatVal;
                        switch (cell.ColumnIndex)
                        {
                            case 0:

                                //MessageBox.Show($"value {cell.Value}: we think {cell.ColumnIndex} is string");

                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();


                                break;
                            case 1:
                                //MessageBox.Show($"value {cell.Value}: we think {cell.ColumnIndex} is string");
                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                                break;
                            case 2:
                                //MessageBox.Show($"value {cell.Value}: we think {cell.ColumnIndex} is string");
                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                                break;
                            case 3:
                                
                                if (float.TryParse(cell.Value.ToString(), out tempFloatVal))
                                {
                                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = tempFloatVal;
                                    
                                }
                                else
                                {
                                    MessageBox.Show($"bad parse: value {cell.Value}: we think {cell.ColumnIndex} is float");

                                }
                                break;
                            case 4:
                                
                                if (float.TryParse(cell.Value.ToString(), out tempFloatVal))
                                {
                                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = tempFloatVal;
                                }
                                else
                                {
                                    MessageBox.Show($"bad parse: value {cell.Value}: we think {cell.ColumnIndex} is float");

                                }
                                break;
                            case 5:
                                if (float.TryParse(cell.Value.ToString(), out tempFloatVal))
                                {
                                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = tempFloatVal;
                                }
                                else
                                {
                                    MessageBox.Show($"bad parse: value {cell.Value}: we think {cell.ColumnIndex} is float");

                                }
                                break;
                            default:
                                MessageBox.Show($"algo mal en defaaul case, hay una columa no manejada");
                                break;
                        }

                        //dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                    }
                }
            }

            //Application.Exit();

            ds.Tables.Add(dt);

            // Save the DataSet as XML
            ds.WriteXml(filename);
            MessageBox.Show("writed xml");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DataSet ds = new DataSet("MyDataSet");

            

            // Load the XML into the DataSet
            ds.ReadXml(SequenceFileTextbox.Text);

            // Prevent the DataGridView from auto-generating columns
            dataGridView1.AutoGenerateColumns = false;

            // Set the DataSource of the DataGridView to the DataTable in the DataSet
            DataTable dt = ds.Tables[0];

            // Clear any existing rows in the DataGridView
            dataGridView1.Rows.Clear();

            // Loop through each row in the DataTable
            foreach (DataRow dr in dt.Rows)
            {
                // Add the row data to the DataGridView
                dataGridView1.Rows.Add(dr.ItemArray);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML Files (*.xml)|*.xml"; // Filter files by extension

            // Show the dialog and get result
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Output the selected file path to your TextBox
                SequenceFileTextbox.Text = openFileDialog.FileName;
            }
        }
    }
}
