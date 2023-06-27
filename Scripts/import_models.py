import pandas as pd
import pypyodbc

# Read the CSV file
csv_file_path = "unique_modelos.csv"
df = pd.read_csv(csv_file_path)

# Define the path to your Access database
access_db_path = r"C:\Users\jsanc635\source\repos\TestDevelopmnetCalibrationsGUI\DataSource\CalibrationDatabase.accdb"

# Connect to the Access database
conn_str = (
    r"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};"
    f"Dbq={access_db_path};"
    "PWD=TEST;"
)
# Connect to the Access database
conn = pypyodbc.win_connect_mdb(conn_str)
cursor = conn.cursor()

# Define the table name where you want to insert the data
table_name = "Modelos"



# Insert rows from the CSV file into the Access database table
insert_query = '''
INSERT INTO Modelos (Modelo, Descripcion, Fabricante, CalibracionInterna) VALUES (?, ?, ?, ?)
'''

for i, row in df.iterrows():
    print(i)
    modelo = row['Modelo']
    descripcion = row['Descripcion']
    fabricante = row['Fabricante']
    calibracion_interna = bool(row['CalibracionInterna'])
    data = (modelo, descripcion, fabricante, calibracion_interna)
    
    try:
        cursor.execute(insert_query, data)
    except pypyodbc.IntegrityError as e:
        print(f"Error: {e}. Skipping the row with Modelo '{modelo}' as it already exists in the database.")
        
conn.commit()
cursor.close()
conn.close()