using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TestDevelopmentDatabaseGUI.Services;
using TestDevelopmentDatabaseGUI.Models;
using TestDevelopmentDatabaseGUI.Utils;
using System.Windows.Documents;
using System.Linq;

namespace TestDevelopmentDatabaseGUI.Forms
{
    public partial class ReportsPanel : Form
    {
        GlobalObjects _globalObjects = GlobalObjects.Instance;
        GlobalConfig _globalConfig = GlobalConfig.Instance;

        public ReportsPanel()
        {
            InitializeComponent();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {

            DataTable tb = _globalObjects.sqlConnHandler.Search(new List<SearchTag> { new SearchTag("E28.SerialNumber", typeof(string), tbxSearchId.Text) }, "AND");

            _globalObjects.sqlConnHandler.Disconnect();

            DataRow row = tb.Rows[0];

            var values = row.ItemArray.Cast<object>().ToList();
            DateTime dateTimeValue = (DateTime)values[0];
            string serialNumber = (string)values[1];
            int employeeNumber = (int)values[2];
            string partNumber = (string)values[3];
            var selectedValues = values
            .Skip(4)
            .Select(value => value is bool boolValue ? (boolValue ? "PASS" : "FAIL") : value.ToString())
            .ToList();

            DataTable newTable = new DataTable();
            newTable.Columns.Add("key", typeof(string));
            newTable.Columns.Add("value", typeof(object));

            var columnNames = tb.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
            var selectedColumnNames = columnNames.Skip(4).ToList();

            for (int i = 0; i < selectedColumnNames.Count; i++)
            {
                newTable.Rows.Add(selectedColumnNames[i], selectedValues[i]);
            }


            dataGridView1.DataSource = newTable;
        }

        private void ReportsPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
