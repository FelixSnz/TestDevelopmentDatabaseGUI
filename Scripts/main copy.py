import pypyodbc
import pandas as pd

# Define the path to your Access database
access_db_path = r"C:\Users\jsanc635\source\repos\TestDevelopmnetCalibrationsGUI\DataSource\BASE DE DATOSTEST2.mdb"

# Define the table you want to export as a CSV
table_name = "TABLA GENERAL"

# Define the path and name of the output CSV file
output_csv = "csvfile.csv"

# Define the connection string for the Access database
conn_str = (
    r"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};"
    f"Dbq={access_db_path};"
    "PWD=TEST;"
)

# Connect to the Access database
connection = pypyodbc.win_connect_mdb(conn_str)

# Create a cursor object
cursor = connection.cursor()

# Read the table data into a pandas DataFrame
data = pd.read_sql(f"SELECT * FROM [{table_name}]", connection)

# Close the database connection
connection.close()

# Write the DataFrame to a CSV file
data.to_csv(output_csv, index=False)