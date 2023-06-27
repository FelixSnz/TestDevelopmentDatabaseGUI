using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;

namespace TestDevelopmentDatabaseGUI.Utils
{
    internal static class Database
    {

        public static DataTable GetDataTable(string query)
        {
            using (OleDbConnection con = new OleDbConnection("YourConnectionString"))
            {
                con.Open();

                using (OleDbDataAdapter da = new OleDbDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
        }

        public static Type GetClrType(int oleDbType)
        {
            // You can add more data type mappings if needed
            Dictionary<int, Type> typeMapping = new Dictionary<int, Type>
            {
                { 2, typeof(short) },
                { 3, typeof(int) },
                { 4, typeof(long) },
                { 5, typeof(decimal) },
                { 6, typeof(float) },
                { 7, typeof(DateTime) },
                { 11, typeof(bool) },
                { 17, typeof(byte) },
                { 72, typeof(Guid) },
                { 128, typeof(byte[]) },
                { 130, typeof(string) },
                { 131, typeof(decimal) }
            };

            return typeMapping.ContainsKey(oleDbType) ? typeMapping[oleDbType] : typeof(object);
        }
    }
}
