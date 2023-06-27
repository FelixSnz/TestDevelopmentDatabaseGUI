import os
from pdfminer.pdfparser import PDFParser
from pdfminer.pdfdocument import PDFDocument
from pdfminer.pdftypes import resolve1

def extract_form_fields(pdf_path):
    with open(pdf_path, 'rb') as file:
        parser = PDFParser(file)
        document = PDFDocument(parser)
        
        if not document.is_extractable:
            raise Exception("PDF is not extractable")

        form_fields = resolve1(document.catalog['AcroForm'])['Fields']
        for field in form_fields:
            field = resolve1(field)
            print('Field name: {} Value: {}'.format(field.get('T').decode('utf-8'), field.get('V')))

# Test the function
extract_form_fields(r'pdf\test1.pdf')