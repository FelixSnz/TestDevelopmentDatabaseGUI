import pdfrw
def fill_pdf(input_pdf, output_pdf, data):
    template_pdf = pdfrw.PdfReader(input_pdf)

    for page in template_pdf.pages:
        annotations = page['/Annots']
        if annotations:
            for annotation in annotations:
                if annotation['/Subtype'] == '/Widget':
                    key = annotation['/T'][1:-1]  # Remove parentheses around field name
                    if key in data:
                        annotation.update(
                            pdfrw.PdfDict(V=data[key],Ff=1)
                        )
    pdfrw.PdfWriter().write(output_pdf, template_pdf)

input_pdf_path = r"pdf\Untitled1.pdf"
output_pdf_path = 'output.pdf'
data_to_fill = {
    'text_box1': 'amigooooo'
}

fill_pdf(input_pdf_path, output_pdf_path, data_to_fill)