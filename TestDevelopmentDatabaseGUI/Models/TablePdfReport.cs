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
        bool isLandscape;

        Dictionary<string, float> columnWidths = new Dictionary<string, float>();

        public TablePdfReport(string reportFilePath, bool landscape = false)
        {
            ReportFilePath = reportFilePath;
            isLandscape = landscape;

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
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase(date, dateFont), 120, 59, 0);
        }

        public void Create(DataTable Data)
        {
            DetermineColumnWidths(Data);

            PdfReader reader = new PdfReader(templatePath);
            PdfStamper stamper = new PdfStamper(reader, new FileStream(ReportFilePath, FileMode.Create));
            PdfContentByte canvas = stamper.GetOverContent(1);
            Rectangle pageSize = reader.GetPageSizeWithRotation(1);
            AddDateToPage(ref canvas);
            PdfPTable table = new PdfPTable(Data.Columns.Count);
            PdfImportedPage templatePage = stamper.GetImportedPage(reader, 1);

            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            var customFont = new Font(baseFont, 8);

            foreach (DataColumn column in Data.Columns)
            {
                table.AddCell(new Phrase(column.ColumnName, customFont));
            }

            float currentTableHeight = 0;
            float tableWidth = columnWidths.Values.Sum();

            foreach (DataRow row in Data.Rows)
            {
                foreach (DataColumn column in Data.Columns)
                {
                    if (row[column] != null)
                    {

                        PdfPCell cell = new PdfPCell(new Phrase(row[column].ToString(), customFont));
                        table.AddCell(cell);
                        string columnName = column.ColumnName;
                        float estimatedHeight = EstimateCellHeight(columnName, row[column].ToString());
                        currentTableHeight += estimatedHeight;
                    }
                }

                table.CalculateHeights();
                float totalHeight = table.TotalHeight;

                if (totalHeight + currentTableHeight > pageSize.Height + 100) // I left a 200 margin space, adjust it according to your needs.
                {
                    table.SetTotalWidth(columnWidths.Values.ToArray());
                    table.LockedWidth = true;

                    float tableStartPos = isLandscape ? 50 : (pageSize.Width - tableWidth) / 2; // Here's the centering when in portrait mode.
                    table.WriteSelectedRows(0, -1, tableStartPos, pageSize.Height - 100, canvas);

                    stamper.InsertPage(reader.NumberOfPages + 1, pageSize);
                    canvas = stamper.GetOverContent(reader.NumberOfPages);
                    canvas.AddTemplate(templatePage, 0, 0); // This line will add the template to the new page.

                    AddDateToPage(ref canvas);
                    table = new PdfPTable(Data.Columns.Count);
                    currentTableHeight = 0;
                }
            }

            table.SetTotalWidth(columnWidths.Values.ToArray());
            table.LockedWidth = true;
            float finalTableStartPos = isLandscape ? 50 : (pageSize.Width - tableWidth) / 2; // Again centering when in portrait mode.
            table.WriteSelectedRows(0, -1, finalTableStartPos, pageSize.Height - 100, canvas); // Start from the top of the page, minus a margin of 100

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