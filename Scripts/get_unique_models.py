import pandas as pd

# Read the Excel file
file_path = r"C:\Users\jsanc635\OneDrive - Johnson Controls\Documents\Equipos.xlsx"
df = pd.read_excel(file_path, sheet_name="Equipos", engine="openpyxl")


# Select the columns you need
columns = ["Modelo", "Descripcion", "Fabricante", "CalibracionInterna"]
filtered_df = df[columns]

# Remove duplicates from the "Modelo" column, keeping the first occurrence
unique_df = filtered_df.drop_duplicates(subset="Modelo", keep="first")

# Write unique values to a CSV file
unique_df.to_csv("unique_modelos.csv", index=False)