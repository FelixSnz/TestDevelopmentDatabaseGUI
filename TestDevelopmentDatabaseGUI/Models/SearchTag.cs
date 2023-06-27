using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDevelopmentDatabaseGUI.Models
{
    public class SearchTag
    {
        public string ColumnName { get; set; }
        public Type ColumnType { get; set; }
        public object SearchVal { get; set; }


        public SearchTag(string columnName, Type columnType, object value = null)
        {
            ColumnName = columnName;
            ColumnType = columnType;
            SearchVal = value;
        }


        //waits for two values in the case of type single and datetime (because the search tag will be a range)
        public SearchTag(string columnName, Type columnType, object value1 = null, object value2 = null)
        {
            ColumnName = columnName;
            ColumnType = columnType;
            SearchVal = $"{value1},{value2}";
        }

        public override string ToString()
        {
            string reprString = "";
            switch (ColumnType)
            {
                case Type t when t == typeof(string):

                    reprString = $"'{SearchVal}' in '{ColumnName}'";
                    break;
                case Type t when t == typeof(bool):
                    reprString = $"'{ColumnName}' is '{SearchVal}'";
                    break;
                case Type t when t == typeof(Single) || t == typeof(DateTime):
                    string[] rangeVals = SearchVal.ToString().Split(',');
                    reprString = $"'{rangeVals[0]}' < '{ColumnName}' < '{rangeVals[1]}'";
                    break;
                default:
                    // You can add more cases for other data types
                    MessageBox.Show("unknown column datatype");
                    break;
            }


            return reprString;

        }
    }
}
