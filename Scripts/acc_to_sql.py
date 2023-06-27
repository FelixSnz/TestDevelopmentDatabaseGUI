import pyodbc
import pypyodbc

import configparser

# Read database configuration
config = configparser.ConfigParser()
config.read('config.ini')

# Access database connection details
access_db_file = r'C:\Users\jsanc635\OneDrive - Johnson Controls\repos\TestDevelopmnetCalibrationsGUI\DataSource\CalibrationDatabase.accdb'
access_table_name = 'Equipos'

# SQL database connection details
host = config['sql']['host']
database = config['sql']['database']
user = config['sql']['user']
password = config['sql']['password']

sql_table_name = 'Equipos_Calibracion'

# Connect to Access database
conn_str = (
    r"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};"
    f"Dbq={access_db_file};"
    "PWD=TEST;"
)
access_conn = pypyodbc.win_connect_mdb(conn_str)
access_cursor = access_conn.cursor()

# Connect to SQL database
sql_conn_str = f'DRIVER={{SQL Server}};SERVER={host};DATABASE={database};UID={user};PWD={password}'
sql_conn = pyodbc.connect(sql_conn_str)
sql_cursor = sql_conn.cursor()

# Fetch data from Access table
access_cursor.execute(f'SELECT * FROM {access_table_name}')
access_rows = access_cursor.fetchall()

# Insert data into SQL table
for row in access_rows:
    placeholders = ', '.join(['?' for _ in row])
    sql_insert_query = f'INSERT INTO {sql_table_name} VALUES ({placeholders})'
    sql_cursor.execute(sql_insert_query, row)
    sql_conn.commit()

# Close connections
access_cursor.close()
access_conn.close()
sql_cursor.close()
sql_conn.close()

print('Data transfer completed successfully!')