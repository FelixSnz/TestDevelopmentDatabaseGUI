using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TestDevelopmentDatabaseGUI.Models
{
    internal class TablePdfReport
    {
        string portraitTemplatePath = Path.Combine(Application.StartupPath, "Resources\\pdf_templates\\portrait_report_template.pdf");
        string landscapeTemplatePath = Path.Combine(Application.StartupPath, "Resources\\pdf_templates\\landscape_report_template.pdf");
        string templatePath;
        string ReportFilePath;

        Dictionary<string, float> columnWidths = new Dictionary<string, float>();

        public TablePdfReport(string reportFilePath, bool landscape=false)
        {
            ReportFilePath = reportFilePath;

            if (landscape)
            {
                templatePath = landscapeTemplatePath;
            }
            else
            {
                templatePath = portraitTemplatePath;
            }
        }

        private void AddDateToPage(ref PdfContentByte canvas)
        {
            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            var dateFont = new Font(baseFont, 10);
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase(date, dateFont), 120, 64, 0);
        }

        public void Create(DataTable Data)
        {
            DetermineColumnWidths(Data);

            PdfReader reader = new PdfReader(templatePath);
            PdfStamper stamper = new PdfStamper(reader, new FileStream(ReportFilePath, FileMode.Create));
            PdfContentByte canvas = stamper.GetOverContent(1);
            PdfImportedPage page = stamper.GetImportedPage(reader, 1);
            AddDateToPage(ref canvas);
            PdfPTable table = new PdfPTable(Data.Columns.Count);

            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            var customFont = new Font(baseFont, 8);

            foreach (DataColumn column in Data.Columns)
            {
                table.AddCell(new Phrase(column.ColumnName, customFont));
            }

            float maxTableHeight = 4500;
            float currentTableHeight = 0;

            foreach (DataRow row in Data.Rows)
            {
                foreach (DataColumn column in Data.Columns)
                {
                    if (row[column] != null)
                    {
                        string cellValue;
                        if (DateTime.TryParse(row[column].ToString(), out DateTime dateValue))
                        {
                            cellValue = dateValue.ToShortDateString();
                        }
                        else
                        {
                            cellValue = row[column].ToString();
                        }
                        PdfPCell cell = new PdfPCell(new Phrase(cellValue, customFont));
                        table.AddCell(cell);
                        string columnName = column.ColumnName;
                        float estimatedHeight = EstimateCellHeight(columnName, cellValue);
                        currentTableHeight += estimatedHeight;
                    }
                }

                if (currentTableHeight > maxTableHeight && Data.Rows.IndexOf(row) < Data.Rows.Count - 2)
                {
                    table.CalculateHeights();
                    table.SetTotalWidth(columnWidths.Values.ToArray());
                    table.LockedWidth = true;

                    table.WriteSelectedRows(0, -1, 50, 500, canvas);
                    stamper.InsertPage(reader.NumberOfPages + 1, reader.GetPageSizeWithRotation(1));
                    canvas = stamper.GetOverContent(reader.NumberOfPages);
                    AddDateToPage(ref canvas);
                    canvas.AddTemplate(page, 0, 0);

                    table = new PdfPTable(Data.Columns.Count);
                    currentTableHeight = 0;
                }
            }

            table.SetTotalWidth(columnWidths.Values.ToArray());
            table.LockedWidth = true;
            table.WriteSelectedRows(0, -1, 25, 500, canvas);

            stamper.Close();
            reader.Close();
        }

        private void DetermineColumnWidths(DataTable Data)
        {
            foreach (DataColumn column in Data.Columns)
            {
                float maxWidth = 4;
                foreach (DataRow row in Data.Rows)
                {
                    string cellValue;
                    if (DateTime.TryParse(row[column].ToString(), out DateTime dateValue))
                    {
                        cellValue = dateValue.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        cellValue = row[column].ToString();
                    }

                    float length = cellValue.Length;
                    if (length > maxWidth) maxWidth = length;
                }
                columnWidths[column.ColumnName] = maxWidth * 5.6f;
            }
        }

        private float EstimateCellHeight(string columnName, string content)
        {
            float fontSize = 8;
            float columnWidth = columnWidths[columnName];
            float charsPerLine = columnWidth / fontSize;
            int numLines = (int)Math.Ceiling(content.Length / charsPerLine);
            return numLines * fontSize * 1.5f;
        }
    }
}