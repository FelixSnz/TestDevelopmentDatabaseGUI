import pandas as pd
import pypyodbc
import sys
import traceback

def safe_parse_date(date_str):
    if pd.notna(date_str):
        try:
            return pd.to_datetime(date_str)
        except ValueError:
            return None
    else:
        return None
    

# Define the path to your input CSV file
input_csv = "CALIBRATION_RECORDS.csv"

# Read the cleaned CSV file into a pandas DataFrame
data = pd.read_csv(input_csv)

# Define the path to your Access database
access_db_path = r"C:\Users\jsanc635\source\repos\TestDevelopmnetCalibrationsGUI\DataSource\CalibrationDatabase.accdb"

# Connect to the Access database
conn_str = (
    r"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};"
    f"Dbq={access_db_path};"
    "PWD=TEST;"
)
conn = pypyodbc.win_connect_mdb(conn_str)
cursor = conn.cursor()

table_name = "Equipos"


for index, row in data.iterrows():


    try:


        # intern_calib = True if row['intern_calib'] == "True" else False
        # certified = True if row['certified'] == "True" else False
        serial = row['serial'] if row['serial'] != "" else None
        last_calib = pd.to_datetime(row['last_calib']) if pd.notna(row['last_calib']) else None
        due_calib = pd.to_datetime(row['due_calib']) if pd.notna(row['due_calib']) else None
        recv_date = safe_parse_date(row['recv_date'])
        cost = float(row['cost']) if pd.notna(row['cost']) else None

        cursor.execute("""
                INSERT INTO {table_name} (ID, Panel, Celda, Descripcion, Fabricante, CalibracionInterna, Modelo, Serial, Unidades, UltimaCalibracion, ProximaCalibracion, Po, Costo, FechaDeAdquisicion, Certificada, Responsable, Status, Notas)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
            """.format(table_name=table_name), 
            (
                row['instrument_name'],
                row['panel'],
                row['cell'],
                row['description'],
                row['manufacturer'],
                row['intern_calib'] ,
                row['model'],
                serial,
                row['units'],
                last_calib,
                due_calib,
                row['po'],
                cost,
                recv_date,
                row['certified'],
                row['assigned_to'],
                row['status'],
                row['notes']
            )
        )
        print(index)
        #sys.exit(1)
    except Exception as e:

        print("the values that raised the error:")

        # Print the converted values
        print("Instrument Name: ", row['instrument_name'])
        print("Panel: ", row['panel'])
        print("Cell: ", row['cell'])
        print("Description: ", row['description'])
        print("Manufacturer: ", row['manufacturer'])
        print("Internal Calibration: ", row['intern_calib'])
        print("Model: ", row['model'])
        print("Serial: ", row['serial'])
        print("Units: ", row['units'])
        print("Last Calibration: ", row['last_calib'])
        print("Due Calibration: ", row['due_calib'])
        print("PO: ", row['po'])
        print("Cost: ", row['cost'])
        print("Receive Date: ", row['recv_date'])
        print("Certified: ", row['certified'])
        print("Assigned To: ", row['assigned_to'])
        print("Status: ", row['status'])
        print("Notes: ", row['notes'])

        traceback_info = traceback.format_exc()
        print("An error occurred:")
        print(traceback_info)
        sys.exit(1)



print("finished")
conn.commit()
cursor.close()
conn.close()