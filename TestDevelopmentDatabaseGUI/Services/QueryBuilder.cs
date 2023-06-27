using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDevelopmentDatabaseGUI.Models;
using TestDevelopmentDatabaseGUI.Utils;

namespace TestDevelopmentDatabaseGUI.Services
{

    
    internal static class QueryBuilder
    {
        static GlobalConfig globalConfig = GlobalConfig.Instance;
        //public static string SearchWithTags(List<SearchTag> searchTags)
        //{
        //    StringBuilder queryBuilder = new StringBuilder($"SELECT * FROM [Equipos]");

        //    if (searchTags.Count > 0)
        //    {
        //        queryBuilder.Append(" WHERE ");
        //        for (int i = 0; i < searchTags.Count; i++)
        //        {
        //            SearchTag tag = searchTags[i];
        //            queryBuilder.AppendFormat("([{0}] LIKE '%{1}%')", tag.Column, tag.Value1.Replace("'", "''"));

        //            if (i < searchTags.Count - 1)
        //            {
        //                queryBuilder.Append(" AND ");
        //            }
        //        }
        //    }

        //    return queryBuilder.ToString();
        //}

        public static string BuildSearchQuery(List<SearchTag> searchTags, string operation)
        {
            StringBuilder query = new StringBuilder($"SELECT * FROM {globalConfig.sqlConfig.TableName}");

            if (searchTags.Count > 0 )
            {
                query.Append(" WHERE ");
                for (int i = 0; i < searchTags.Count; i++)
                {
                    SearchTag tag = searchTags[i];

                    switch (tag.ColumnType)
                    {
                        case Type t when t == typeof(string):
                            query.Append($"[{tag.ColumnName}] LIKE '%{tag.SearchVal}%'");
                            break;
                        case Type t when t == typeof(bool):
                            query.Append($"[{tag.ColumnName}] = {((bool)tag.SearchVal ? 1 : 0)}");
                            break;
                        case Type t when t == typeof(Single):
                            string[] rangeVals = tag.SearchVal.ToString().Split(',');
                            query.Append($"{tag.ColumnName} > {rangeVals[0]} AND {tag.ColumnName} < {rangeVals[1]}");
                            break;
                        case Type t when t == typeof(DateTime):
                            string[] dateRangeVals = tag.SearchVal.ToString().Split(',');
                            DateTime date1 = DateTime.Parse(dateRangeVals[0]);
                            DateTime date2 = DateTime.Parse(dateRangeVals[1]);
                            DateTime[] dateRange = { date1, date2 };
                            query.Append($"{tag.ColumnName} > #{date1.ToString("MM/dd/yyyy")}# AND {tag.ColumnName} < #{date2.ToString("MM/dd/yyyy")}#");
                            break;
                        default:
                            // You can add more cases for other data types
                            //MessageBox.Show("unknown column datatype");
                            break;
                    }

                    if (i < searchTags.Count - 1)
                    {
                        query.Append($" {operation} ");
                    }
                }
            }

            return query.ToString();
        }
    }
}
