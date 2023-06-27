import pandas as pd

# Define the path to your input CSV file
input_csv = "csvfile2.csv"

# Define the path and name of the output CSV file
output_csv = "output.csv"

# Read the CSV file into a pandas DataFrame
data = pd.read_csv(input_csv)

# Create an empty DataFrame for storing the incorrect values
incorrect_values_df = pd.DataFrame(columns=["row_number", "calib_int/ext", "notas"])

# Iterate through the rows in the input DataFrame
for index, row in data.iterrows():
    # Check if the "calib_int/ext" value is not "INTERNA" or "EXTERNA"
    if row["calib_int/ext"] not in ["INTERNA", "EXTERNA"]:
        # Add the index, incorrect "calib_int/ext", and corresponding "notas" values to the new DataFrame
        incorrect_values_df = incorrect_values_df.append(
            {"row_number": index, "calib_int/ext": row["calib_int/ext"], "notas": row["notas"]},
            ignore_index=True,
        )

# Write the incorrect values DataFrame to a CSV file
incorrect_values_df.to_csv(output_csv, index=False)