from pdfrw import PdfReader, PdfWriter, IndirectPdfDict

def write_data_to_pdf(input_pdf_path, output_pdf_path, data_dict):
    pdf = PdfReader(input_pdf_path)

    for page in pdf.pages:
        annotations = page['/Annots']
        if annotations is None:
            continue

        for annotation in annotations:
            if annotation['/Subtype'] == '/Widget':
                if annotation['/T']:
                    key = annotation['/T'][1:-1]
                    if key in data_dict:
                        annotation.update(IndirectPdfDict(V='{}'.format(data_dict[key])))

    PdfWriter().write(output_pdf_path, pdf)

# Here, you provide the data for the form fields in the form of a dictionary
form_data = {
    'InstrumentID': '1234',
    'Description': 'Calibration Instrument',
    'Manufacturer': 'Instrument Co.',
    'Model': 'Model X',
    'SerialNumber': 'SN7890',
    'CalibrationDate': '2023-04-27',
    'RH': '50%',
    'Temp': '77°F',
    'ReceivedConditions': 'Good',
    'TakenAction': 'Calibrated',
    'CompletedConditions': 'Excellent',
    'EmployeeNumber': '3221865'
}

# Here, you provide the data for the table in the form of a dictionary
table_data = {}
for i in range(1, 11):
    table_data[f"Model_Row_{i}"] = f"Model {i}"
    table_data[f"Desc_Row_{i}"] = f"Description {i}"
    table_data[f"ID_Row_{i}"] = f"ID {i}"
    table_data[f"Due_Date_Row_{i}"] = f"2023-05-{i:02}"

# Merge the two dictionaries
data = {**form_data, **table_data}

write_data_to_pdf(r'pdf\header_template3.pdf', 'output16.pdf', data)