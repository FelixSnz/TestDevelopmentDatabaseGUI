import pandas as pd
import pypyodbc
import sys
import math

# Define the path to your input CSV file
input_csv = "csvfile.csv"

# Read the cleaned CSV file into a pandas DataFrame
data = pd.read_csv(input_csv)


def nullable_date(str_date):
    print("this is the type of strdate: ", type(str_date))
    print("this is str_date: ", f"'{str_date}'")
    if str_date is float:
        if math.isnan(str_date):
            return
    try:
        if str_date != "":
            return pd.to_datetime(str_date)
        else:
            print(str_date)
            sys.exit(1)

    except Exception as e:
        print (e)



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
table_name = "CALIBRATION_RECORDS"

# Iterate through the DataFrame rows and insert them one by one into the Access table
for index, row in data.iterrows():


    # Set intern_calib column value to True if calib_int/ext column has "INTERNA", False otherwise
    intern_calib = True if row['calib_int/ext'] == "INTERNA" else False
    
    # Set certified column value to True if rango column has "Y", False otherwise
    certified = True if row['rango'] == "Y" else False


    print("------------------------------------------")
    # Convert the last_calib, due_calib, and recv_date columns to datetime objects
    
    
    

    last_calib = nullable_date(row['f_calibrado'])
    print("this is last: ", last_calib)

    due_calib = nullable_date(row['f_due_calib'])
    print("this is due: ", due_calib)

    recv_date = nullable_date(row['fecha_entrada'])
    print("este es el fecha de entrada: ", recv_date)

    
    
    # Convert the cost column to a float

    print("este es el cost: ", row['costo'])
    cost = float(row['costo'])



    #sys.exit(1)
    cursor.execute("""
            INSERT INTO {table_name} (instrument_name, panel, cell, description, manufacturer, intern_calib, model, serial, units, last_calib, due_calib, po, cost, recv_date, certified, assigned_to, status, notes)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        """.format(table_name=table_name), 
        ( #, , , , , , , , , , , , , , , , , )
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
            0.0,
            
            recv_date,
            certified,
            row['asignado_a'],
            row['status'],
            row['notas']
        )
    )
    
    break

   

# Commit the changes and close the connection
conn.commit()
cursor.close()
conn.close()