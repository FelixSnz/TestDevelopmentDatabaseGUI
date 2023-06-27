import pdfrw

# Replace this with the path to your PDF template
template_path = r'pdf\Untitled 1.pdf'

# Replace this with the path where you want to save the filled PDF
output_path = 'out.pdf'

# Sample data for the table
equipment_data = [
    {"model": "Model A", "description": "Description A", "equipment_id": "1234", "due_calib_date": "2023-05-01"},
    {"model": "Model B", "description": "Description B", "equipment_id": "5678", "due_calib_date": "2023-06-01"},
    {"model": "Model C", "description": "Description C", "equipment_id": "9012", "due_calib_date": "2023-07-01"},
]

def fill_pdf_template(template_path, equipment_data, output_path):
    # Read the PDF template
    template_pdf = pdfrw.PdfReader(template_path)

    # Find the table in the PDF template
    for page in template_pdf.pages:
        if page.Annots != None:
            print("arre")
            for annotation in page.Annots:
                # Assuming the table is a form field named 'TableField'
                if annotation.T == "UsedInstruments":
                    table_data = ""

                    # Format the data and create a string
                    for row in equipment_data:
                        table_data += f"{row['model']}, {row['description']}, {row['equipment_id']}, {row['due_calib_date']}\n"

                    # Update the table field with the data
                    annotation.update(pdfrw.PdfDict(V=table_data))

    # Write the filled PDF to a new file
    pdfrw.PdfWriter().write(output_path, template_pdf)

fill_pdf_template(template_path, equipment_data, output_path)