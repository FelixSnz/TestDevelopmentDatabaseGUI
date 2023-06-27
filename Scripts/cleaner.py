import pandas as pd

# Define the path to your input CSV file
input_csv = "csvfile.csv"

# Define the path and name of the output CSV file
output_csv = "csvfile2.csv"

# Read the CSV file into a pandas DataFrame
data = pd.read_csv(input_csv)

# Remove rows where the "calib_int/ext" column is an empty string or NaN
data = data[(pd.notna(data["calib_int/ext"])) & (data["calib_int/ext"] != "07-31-18")]


# Write the cleaned DataFrame to an output CSV file
data.to_csv(output_csv, index=False)