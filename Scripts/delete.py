import pypyodbc

access_db_path = r"C:\Users\jsanc635\source\repos\TestDevelopmnetCalibrationsGUI\DataSource\CalibrationDatabase.accdb"

# Connect to the Access database
conn_str = (
    r"Driver={Microsoft Access Driver (*.mdb, *.accdb)};"
    f"Dbq={access_db_path};"
    "PWD=TEST;"
)

# Establish a connection to the database
conn = pypyodbc.connect(conn_str)

# Open a cursor
cursor = conn.cursor()

# Execute a SQL DELETE statement to remove all rows from the table
cursor.execute("DELETE FROM Equipos")

# Commit the changes
conn.commit()

# Close the cursor and the connection
cursor.close()
conn.close()