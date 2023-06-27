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


# Define the mapping of incorrect values to correct ones
calib_mapping = {
    "INTERNO": "INTERNA",
    "INT.": "INTERNA",
    "INTERENA": "INTERNA",
    "INTERNA]": "INTERNA",
    "INT": "INTERNA",
    "int.": "INTERNA",
    "int": "INTERNA",
    "INTENRNA": "INTERNA",
    "interna": "INTERNA",
    "INTERN0": "INTERNA",
    "INTTERNO": "INTERNA",
    "INERNO": "INTERNA",
    "INETERNO": "INTERNA",
    "interno": "INTERNA",
    "INTRNA": "INTERNA",
    "INTERNPO": "INTERNA",
    "IINTERNA": "INTERNA",
    "EXETRNO": "EXTERNA",
    "EXTERNO": "EXTERNA",
    "Externo": "EXTERNA",
    "EXT": "EXTERNA",
    "EXT.": "EXTERNA",
    "EST": "EXTERNA",
    "EXTERN0": "EXTERNA",
    "EXTENO": "EXTERNA",
    "EXETERNA": "EXTERNA",
    "EXTRENO": "EXTERNA",
    "EXTERNIO": "EXTERNA",
    "externo": "EXTERNA",
    "EZTERNO": "EXTERNA",
}

# Replace the incorrect values in the "calib_int/ext" column
data["calib_int/ext"] = data["calib_int/ext"].replace(calib_mapping)
# Remove rows where the "calib_int/ext" column has no value (NaN)
data = data.dropna(subset=["calib_int/ext"])

data = data[data["calib_int/ext"] != ""]


# Close the database connection
connection.close()

# Write the DataFrame to a CSV file
data.to_csv(output_csv, index=False)