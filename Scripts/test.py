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
input_csv = "csvfile.csv"

# Read the cleaned CSV file into a pandas DataFrame
data = pd.read_csv(input_csv)

# Define the path to your Access database
access_db_path = r"C:\Users\jsanc635\source\repos\TestDevelopmnetCalibrationsGUI\DataSource\BASE DE DATOSTEST2.mdb"

# Connect to the Access database
conn_str = (
    r"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};"
    f"Dbq={access_db_path};"
    "PWD=TEST;"
)
conn = pypyodbc.win_connect_mdb(conn_str)
cursor = conn.cursor()

table_name = "CALIBRATION_RECORDS"


for index, row in data.iterrows():


    try:
        intern_calib = True if row['calib_int/ext'] == "INTERNA" else False
        certified = True if row['rango'] == "Y" else False
        serial = row['serie'] if row['serie'] != "" else None
        last_calib = pd.to_datetime(row['f_calibrado']) if pd.notna(row['f_calibrado']) else None
        due_calib = pd.to_datetime(row['f_due_calib']) if pd.notna(row['f_due_calib']) else None
        recv_date = safe_parse_date(row['fecha_entrada'])
        cost = float(row['costo']) if pd.notna(row['costo']) else None

        cursor.execute("""
                INSERT INTO {table_name} (instrument_name, panel, cell, description, manufacturer, intern_calib, model, serial, units, last_calib, due_calib, po, cost, recv_date, certified, assigned_to, status, notes)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
            """.format(table_name=table_name), 
            (
                row['instrumento'],
                row['panel'],
                row['celda'],
                row['descripcion'],
                row['fabricante'],
                intern_calib,
                row['modelo'],
                row['serie'],
                row['r_unidad_medida'],
                last_calib,
                due_calib,
                row['po'],
                cost,
                recv_date,
                certified,
                row['asignado_a'],
                row['status'],
                row['notas']
            )
        )
        print(index)
    except Exception as e:

        print("the values that raised the error:")

        # Print the converted values
        print(f"instrument_name: {row['instrumento']}")
        print(f"panel: {row['panel']}")
        print(f"cell: {row['celda']}")
        print(f"description: {row['descripcion']}")
        print(f"manufacturer: {row['fabricante']}")
        print(f"intern_calib: {intern_calib}")
        print(f"model: {row['modelo']}")
        print(f"serial: {row['serie']}")
        print(f"units: {row['r_unidad_medida']}")
        print(f"last_calib: {last_calib}")
        print(f"due_calib: {due_calib}")
        print(f"po: {row['po']}")
        print(f"cost: {cost}")
        print(f"recv_date: {recv_date}")
        print(f"certified: {certified}")
        print(f"assigned_to: {row['asignado_a']}")
        print(f"status: {row['status']}")
        print(f"notes: {row['notas']}")


        traceback_info = traceback.format_exc()
        print("An error occurred:")
        print(traceback_info)
        sys.exit(1)



print("finished")
conn.commit()
cursor.close()
conn.close()