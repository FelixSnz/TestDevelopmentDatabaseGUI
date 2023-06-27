using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TestDevelopmentDatabaseGUI.Forms.FormFields;
using TestDevelopmentDatabaseGUI.Models;


namespace TestDevelopmentDatabaseGUI.Forms
{
    public partial class SearchPanel : Form
    {

        Dictionary<string, Type> columnDataTypes = new Dictionary<string, Type>();
        Form DynamicFormField = null;

        GlobalConfig _globalConfig = GlobalConfig.Instance;
        GlobalObjects _globalObjects = GlobalObjects.Instance;

        private bool itemSelected = false;
        public SearchPanel()
        {
            InitializeComponent();
            LoadColumnNamesToComboBox();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                List<SearchTag> searchTags = lstTags.Items.OfType<SearchTag>().ToList();
                Console.WriteLine($"cantidad de tag items: {lstTags.Items.Count}");

                string operation = cmbOperations.Text.ToString();



                
                dataGridView1.DataSource = _globalObjects.sqlConnHandler.Search(searchTags, operation);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_globalObjects.sqlConnHandler.connState == ConnectionState.Open)
                {
                    Console.WriteLine("closes");
                    _globalObjects.sqlConnHandler.Disconnect();
                }
            }

        }

        private void LoadColumnNamesToComboBox()
        {

            if (!_globalObjects.sqlConnHandler.connected)
            {
                MessageBox.Show("Couldn't load table columns, there is no database connection");
            }

            // Get the schema information for the specified table
            using (DataTable schemaTable = _globalObjects.sqlConnHandler.GetTableSchema(_globalConfig.sqlConfig.TableName))
            {
                // Clear the ComboBox items
                cmbColumns.Items.Clear();

                // Add each column name to the ComboBox
                foreach (DataRow row in schemaTable.Rows)
                {
                    
                    string columnName = row["ColumnName"].ToString();

                    string columnType = row["DataType"].ToString();

                    Type dataType = (Type)row["DataType"];
                    //cmbColumns.AutoCompleteCustomSource.Add(columnName);
                    cmbColumns.Items.Add(columnName);
                    columnDataTypes[columnName] = dataType;
                }
            }

            foreach (KeyValuePair<string, Type> columnInfo in columnDataTypes)
            {
                Console.WriteLine("Column: {0}, Data Type: {1}", columnInfo.Key, columnInfo.Value);
            }
        }

        // use the container 'pnlDynamicCmpContainer' to create a custom form field depending on the 
        //datatyoe of the selected column
        private void cmbColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the panel
            pnlDynamicCmpContainer.Controls.Clear();

            // Get selected column data type
            Type columnType = columnDataTypes[cmbColumns.SelectedItem.ToString()];



            switch (columnType)
            {
                case Type t when t == typeof(string):
                    // Create an instance of StringControl
                    DynamicFormField = new FormFields.TextForm();

                    break;
                case Type t when t == typeof(bool):
                    // Create an instance of BoolControl
                    DynamicFormField = new FormFields.YesNoQuestion();
                    break;
                case Type t when t == typeof(Single):
                    // Create an instance of NumberControl
                    break;
                case Type t when t == typeof (double):
                    DynamicFormField = new FormFields.ValueRange();
                    break;
                case Type t when t == typeof(DateTime):
                    // Create an instance of DateControl
                    DynamicFormField = new FormFields.DateRangePicker();
                    break;
                default:
                    // You can add more cases for other data types
                    MessageBox.Show("unknown column datatype");
                    break;
            }

            // If a control was created, add it to the panel
            if (DynamicFormField != null)
            {
                DynamicFormField.TopLevel = false;
                DynamicFormField.FormBorderStyle = FormBorderStyle.None;
                DynamicFormField.Dock = DockStyle.Fill;
                pnlDynamicCmpContainer.Controls.Add(DynamicFormField);
                DynamicFormField.Show();
            }
        }

        private void btnAddTag_Click_1(object sender, EventArgs e)
        {
            string columnName = cmbColumns.SelectedItem.ToString();
            Type columnType = columnDataTypes[columnName];

            columnName = $"{columnName}";
            SearchTag tag = null;

            switch (columnType)
            {
                case Type t when t == typeof(string):
                    if (DynamicFormField is TextForm textForm)
                    {
                        string value = textForm.txtValue.Text;
                        tag = new SearchTag(columnName, t, value);
                    }
                    break;
                case Type t when t == typeof(bool):
                    if (DynamicFormField is YesNoQuestion yesNoForm)
                    {
                        bool value = yesNoForm.YesCheckBox.Checked;
                        tag = new SearchTag(columnName, t, value);
                    }
                    break;
                case Type t when t == typeof(Single):
                    if (DynamicFormField is ValueRange valueRangeForm)
                    {
                        double min = (double)valueRangeForm.Minimum.Value;
                        double max = (double)valueRangeForm.Maximum.Value;
                        tag = new SearchTag(columnName, t, min, max);
                    }
                    break;
                case Type t when t == typeof(DateTime):
                    if (DynamicFormField is DateRangePicker dateRangePickerForm)
                    {
                        DateTime from = dateRangePickerForm.FromField.Value;
                        DateTime to = dateRangePickerForm.ToField.Value;
                        tag = new SearchTag(columnName, t, from.ToShortDateString(), to.ToShortDateString());
                    }
                    break;
            }

            if (tag != null)
            {
                lstTags.Items.Add(tag);
            }
        }

        private void lstTags_DoubleClick(object sender, EventArgs e)
        {
            if (lstTags.SelectedItem != null)
            {
                lstTags.Items.Remove(lstTags.SelectedItem);
            }
        }

        private void btnCsvExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount == 0 || dataGridView1.RowCount == 0)
            {
                MessageBox.Show("No data available for export.");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    // Write the header row
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        sw.Write(dataGridView1.Columns[i].HeaderText);
                        if (i < dataGridView1.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);

                    // Write the data rows
                    foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                    {
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {

                            // Check if the value is a DateTime
                            if (dgvRow.Cells[i].Value != null && DateTime.TryParse(dgvRow.Cells[i].Value.ToString(), out DateTime dateValue))
                            {
                                // If it's a DateTime, write it in short date format
                                sw.Write(dateValue.ToShortDateString());
                            }
                            else
                            {
                                sw.Write(dgvRow.Cells[i].Value);
                            }

                            if (i < dataGridView1.Columns.Count - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.Write(sw.NewLine);
                    }
                    MessageBox.Show("Export complete.", "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnPdfExport_Click(object sender, EventArgs e)
        {
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
                TablePdfReport s = new TablePdfReport(savefiledialog.FileName, true);
                s.Create((DataTable)dataGridView1.DataSource);
                MessageBox.Show("Export complete.", "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cmbColumns_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // If the entered text is not in the item list, cancel the validation and set focus on the ComboBox
            if (!comboBox.Items.Contains(comboBox.Text))
            {
                e.Cancel = true;
                comboBox.Focus();
                cmbColumns.Text = "";
                MessageBox.Show("Please select a valid item from the list.", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbColumns_DropDown(object sender, EventArgs e)
        {
            cmbColumns.AutoCompleteMode = AutoCompleteMode.None;
            itemSelected = false;
        }

        private void cmbColumns_DropDownClosed(object sender, EventArgs e)
        {
            cmbColumns.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            if (itemSelected)
            {
                cmbColumns_SelectedIndexChanged(sender, e);
            }
        }

        private void cmbColumns_SelectionChangeCommitted(object sender, EventArgs e)
        {

            itemSelected = true;

        }
    }
}