using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TestDevelopmentDatabaseGUI.Models;
using System.Windows.Forms;
using TestDevelopmentDatabaseGUI.Forms;

namespace TestDevelopmentDatabaseGUI.Services
{
    public class SqlConnHandler
    {

        private bool _connected;
        private SqlConnection conn { set; get; }

        // Public property that is read-only outside the class
        public bool connected
        {
            get
            {
                return  _connected;
            }
        }

        public ConnectionState connState {
            get
            {
                return conn.State;
            }
        }

        public SqlConnHandler()
        {

            this._connected = false;
            Main.Instance.SetButtonsState(false);
            Main.Instance.UpdateDataSourceLabel();

        }

        public void Connect(string server, string database, string user, string password)
        {

            
            string connString = $"Server={server};Database={database};User Id={user};Password={password};";

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                this._connected=true;
                Main.Instance.SetButtonsState(true);
                Main.Instance.UpdateDataSourceLabel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this._connected = false;
                Main.Instance.SetButtonsState(false);
                Main.Instance.ClearDataSourceLabel();
                MessageBox.Show(e.ToString(), "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Disconnect()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                _connected = false;
                Main.Instance.SetButtonsState(false);
                Main.Instance.CloseCurrentChildForm();
                Main.Instance.UnHighligthCurrentBtn();
                Main.Instance.ClearDataSourceLabel();
                conn = null;
            }
        }

        public string[] GetColumnNames(string table)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                Console.WriteLine("No database connection");
                return null;
            }

            string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            var columnNames = new List<string>();
            while (reader.Read())
            {
                columnNames.Add(reader[0].ToString());
            }
            reader.Close();

            return columnNames.ToArray();
        }

        public DataTable GetTableSchema(string tableName)
        {

            string query = $"SELECT * FROM {tableName} WHERE 1=0";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = null;
            DataTable dt = null;


            reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
            dt = reader.GetSchemaTable();
            

            //Console.WriteLine(e);
            

            reader?.Close();
            

            return dt;
        }

        public void Insert(string table, object[] data)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                Console.WriteLine("No database connection");
                return;
            }

            string[] columnNames = GetColumnNames(table);
            string placeholders = string.Join(", ", new string[columnNames.Length].Select((s, i) => $"@p{i}").ToArray());
            string query = $"INSERT INTO {table} VALUES ({placeholders})";
            SqlCommand cmd = new SqlCommand(query, conn);

            for (int i = 0; i < data.Length; i++)
            {
                cmd.Parameters.AddWithValue($"@p{i}", data[i]);
            }

            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data inserted successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public DataTable Search(List<SearchTag> searchTags, string operation)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                MessageBox.Show("No database connection");
                return null;
            }

            string query = QueryBuilder.BuildSearchQuery(searchTags, operation);

            Console.WriteLine($"query: {query}");
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return dt;
        }

        public List<string> GetEnabledTables()
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                Console.WriteLine("No database connection");
                return null;
            }

            string query = "SELECT name FROM sys.tables;";

            Console.WriteLine($"query: {query}");
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            var modifiableTables = new List<string>();

            while (reader.Read())
            {
                modifiableTables.Add(reader[0].ToString());
            }
            reader.Close();

            return modifiableTables;
        }
    }
}
