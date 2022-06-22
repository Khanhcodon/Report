using System;
using System.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// Lớp hỗ trợ đọc file xlsx
    /// </summary>
    public class DocxDataParser
    {

        /// <summary>
        /// Contructor
        /// </summary>
        public DocxDataParser()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetTable(DataTable data)
        {
            foreach (DataRow dtRow in data.Rows)
            {
                // On all tables' columns
                foreach (DataColumn dc in data.Columns)
                {
                    var field1 = dtRow[dc].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void ExportDocx(string fileName, DataTable data)
        {
            //using (var document = WordprocessingDocument.Open(@"D://abc.docx", true))
            using (WordprocessingDocument document = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = document.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                Body body = mainPart.Document.AppendChild(new Body());
                var doc = mainPart.Document;
                DocumentFormat.OpenXml.Wordprocessing.Table table = new DocumentFormat.OpenXml.Wordprocessing.Table();
                TableProperties props = new TableProperties(
                    new TableBorders(
                        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 }
                    ));
                table.ClearAllAttributes();
                table.AppendChild<TableProperties>(props);

                foreach (DataRow dtRow in data.Rows)
                {
                    var tr = new DocumentFormat.OpenXml.Wordprocessing.TableRow();
                    // On all tables' columns
                    foreach (DataColumn dc in data.Columns)
                    {
                        var tc = new DocumentFormat.OpenXml.Wordprocessing.TableCell();
                        var feild = dtRow[dc].ToString();
                        tc.Append(new Paragraph(new Run(new Text(feild))));
                        tc.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
                        tr.Append(tc);
                    }
                    table.Append(tr);
                }

                //for (var i = 0; i <= data.GetUpperBound(0); i++)
                //{
                //    var tr = new DocumentFormat.OpenXml.Wordprocessing.TableRow();
                //    for (var j = 0; j <= data.GetUpperBound(1); j++)
                //    {
                //        var tc = new DocumentFormat.OpenXml.Wordprocessing.TableCell();
                //        string[] datas = data[i, j].ToString().Split('\n');
                //        for (int k = 0; k < datas.Length; k++)
                //        {
                //            tc.Append(new Paragraph(new Run(new Text(datas[k]))));
                //            tc.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
                //        }
                //        //tc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                //        tr.Append(tc);
                //    }
                //    table.Append(tr);
                //}
                doc.Body.Append(table);
                doc.Save();
            }
        }

    }
}
