using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace TestDevelopmentDatabaseGUI.Forms.Popups
{
    public partial class DatabaseConfig : Form
    {
        GlobalObjects _globalObjects = GlobalObjects.Instance;
        GlobalConfig _globalConfig = GlobalConfig.Instance;
        public DatabaseConfig()
        {
            InitializeComponent();
            LoadTablesToComboBox();
        }

        private void LoadTablesToComboBox()
        {

            if (!_globalObjects.sqlConnHandler.connected)
            {
                MessageBox.Show("couldn't load tables, there is no database connection");
            }

            // Get the schema information for the specified table
            List<string> TableNames = _globalObjects.sqlConnHandler.GetEnabledTables();
            
            // Clear the ComboBox items
            cmbTables.Items.Clear();

            // Add each column name to the ComboBox
            foreach (string TableName in TableNames)
            {
                cmbTables.Items.Add(TableName);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _globalConfig.sqlConfig.TableName = cmbTables.SelectedItem.ToString();
            _globalConfig.Save();
            Main.Instance.UpdateDataSourceLabel();

            DialogResult = DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
