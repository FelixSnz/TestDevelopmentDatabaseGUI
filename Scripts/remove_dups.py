import pandas as pd

# Define the path to your input CSV file
input_csv = "csvfile.csv"

# Define the path and name of the output CSV file
output_csv = "csvfile2.csv"

# Read the CSV file into a pandas DataFrame
data = pd.read_csv(input_csv)

# Remove rows with duplicate values in the "instrumento" column, keeping the first occurrence
data = data.drop_duplicates(subset='serie', keep='first')

# Write the cleaned DataFrame to an output CSV file
data.to_csv(output_csv, index=False)