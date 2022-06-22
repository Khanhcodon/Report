using System;
using ClosedXML;
using System.IO;
using ClosedXML.Excel;

using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml.Wordprocessing;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// Lớp hỗ trợ đọc file xlsx
    /// </summary>
    public class XlsxParser
    {
        private XLWorkbook _workbook;

        /// <summary>
        /// Contructor
        /// </summary>
        public XlsxParser()
        {

        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="fileStream">FileStream</param>
        public XlsxParser(Stream fileStream)
        {
            _workbook = new XLWorkbook(fileStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public XlsxParser(string filePath)
        {
            _workbook = new XLWorkbook(filePath);
        }

        /// <summary>
        /// Trả về một vùng dữ liệu của file excel
        /// </summary>
        /// <param name="range">Vùng dữ liệu cần select, ví dụ: "A3:D5"</param>
        /// <returns>IXLRange</returns>
        public IXLRange GetRange(string range)
        {
            if (string.IsNullOrEmpty(range))
            {
                throw new ArgumentNullException("range");
            }
            return _workbook.Range(range);
        }

        /// <summary>
        /// Trả về một worksheet của file xlsx theo tên.
        /// </summary>
        /// <param name="name">Tên worksheet cần lấy</param>
        /// <returns>IXLWorksheet</returns>
        public IXLWorksheet GetWorkSheet(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            return _workbook.Worksheet(name);
        }

        /// <summary>
        /// Trả về một worksheet theo vị trí
        /// </summary>
        /// <param name="position">Vị trí worksheet cần lấy</param>
        /// <returns>IXLWorksheet</returns>
        public IXLWorksheet GetWorkSheet(int position)
        {
            return _workbook.Worksheet(position);
        }

        /// <summary>
        /// Trả về một row theo vị trí
        /// </summary>
        /// <param name="rowNumber">Vị trí row cần lấy</param>
        /// <param name="workSheetPosition">Worksheet cần lấy</param>
        /// <returns>IXLRow</returns>
        public IXLRow GetRow(int rowNumber, int workSheetPosition)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            if (workSheet == null)
            {
                throw new Exception("Worksheet not exist!");
            }

            return workSheet.Row(rowNumber);
        }

        /// <summary>
        /// Trả về danh sách các Rows trong worksheet và range được chọn
        /// </summary>
        /// <param name="rowNumber">Range các rows cần lấy, Ví dụ: "4:8", "4:5,10:23", "12",...</param>
        /// <param name="workSheetName">Worksheet cần lấy rows</param>
        /// <returns>IXLRows</returns>
        public IXLRows GetRows(string workSheetName, string rowNumber)
        {
            if (string.IsNullOrEmpty(workSheetName))
            {
                throw new ArgumentNullException("workSheetName");
            }

            var workSheet = GetWorkSheet(workSheetName);
            return GetRows(workSheet, rowNumber);
        }

        /// <summary>
        /// Trả về danh sách các Rows trong worksheet và range được chọn
        /// </summary>
        /// <param name="rowNumber">Range các rows cần lấy, Ví dụ: "4:8", "4:5,10:23", "12",...</param>
        /// <param name="workSheetPosition">Vị trí worksheet cần lấy</param>
        /// <returns>IXLRows</returns>
        public IXLRows GetRows(string rowNumber, int workSheetPosition)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            return GetRows(workSheet, rowNumber);
        }

        /// <summary>
        /// Trả về danh sách các Rows trong worksheet và range được chọn
        /// </summary>
        /// <param name="workSheet">Worksheet cần lấy rows</param>
        /// <param name="rowNumber">Range các rows cần lấy, Ví dụ: "4:8", "4:5,10:23", "12",...</param>
        /// <returns>IXLRows</returns>
        public static IXLRows GetRows(IXLWorksheet workSheet, string rowNumber)
        {
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }

            if (string.IsNullOrEmpty(rowNumber))
            {
                return workSheet.Rows();
            }

            return workSheet.Rows(rowNumber);
        }

        /// <summary>
        /// Trả về danh sách các Columns trong worksheet và range được chọn
        /// </summary>
        /// <param name="columnName">Range của columns cần lấy, ví dụ: "G:H", "10:23", "G:H, Q:T", "G", ...</param>
        /// <param name="workSheetPosition">Vị trí của worksheet cần lấy</param>
        /// <returns>IXLColumns</returns>
        public IXLColumns GetColumns(string columnName, int workSheetPosition)
        {
            var workSheet = _workbook.Worksheet(workSheetPosition);
            return GetColumns(columnName, workSheet);
        }

        /// <summary>
        /// Trả về danh sách các Columns trong worksheet và range được chọn
        /// </summary>
        /// <param name="columnName">Range của columns cần lấy, ví dụ: "G:H", "10:23", "G:H, Q:T", "G", ...</param>
        /// <param name="workSheetName">Tên của worksheet cần lấy</param>
        /// <returns>IXLColumns</returns>
        public IXLColumns GetColumns(string columnName, string workSheetName)
        {
            var workSheet = _workbook.Worksheet(workSheetName);
            return GetColumns(columnName, workSheet);
        }

        /// <summary>
        /// Trả về danh sách các Columns trong worksheet và range được chọn
        /// </summary>
        /// <param name="columnName">Range của columns cần lấy, ví dụ: "G:H", "10:23", "G:H, Q:T", "G", ...</param>
        /// <param name="workSheet">Worksheet cần lâys</param>
        /// <returns>IXLColumns</returns>
        public static IXLColumns GetColumns(string columnName, IXLWorksheet workSheet)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("columnName");
            }

            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }
            return workSheet.Columns(columnName);
        }

        #region Converter

        /// <summary>
        /// Convert tệp xls và xlsx sang Pdf sử dụng thư viện Interop của Microsoft.
        /// </summary>
        /// <param name="inputPath">Đường dẫn file Excel.</param>
        /// <param name="outputPath">Đường dẫn file pdf sinh ra.</param>
        /// <remarks>
        /// - Yêu cầu cài đặt MS-Office trên server: sử dụng bản 2013 thì càng tốt.
        /// - Một số thiết lập cần thiết:
        ///   + Create the directory C:\Windows\SysWOW64\config\systemprofile\Desktop (for the 32-bit version of Excel/Office on a 64-bit Windows computer) or C:\Windows\System32\config\systemprofile\Desktop (for a 32-bit version of Office on a 32-bit Windows computer or a 64-bit version of Office on a 64-bit Windows computer).
        ///   + For the Desktop directory, add Full control permissions for the relevant user (for example in Win7 and IIS 7 and DefaultAppPool set permissions for user IIS AppPool\DefaultAppPool).
        /// </remarks>
        public static void ConvertToPdf(string inputPath, string outputPath)
        {
            object missing = System.Reflection.Missing.Value;
            object saveChange = false;
            // object objDNS = Excel.XlSaveAction.xlDoNotSaveChanges;
            Excel.Workbook aExcel = null;
            Excel.Application ExcelApp = new Excel.Application();

            try
            {
                aExcel = ExcelApp.Workbooks.Open(inputPath, missing, true);
                if (aExcel == null)
                {
                    throw new Exception("Excel không có quyền đọc tệp truyền vào.");
                }

                aExcel.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, outputPath);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (aExcel != null)
                {
                    aExcel.Close(saveChange);
                }
                if (ExcelApp != null)
                {
                    ExcelApp.Quit();
                    ExcelApp = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlTable"></param>
        /// <param name="forderPath"></param>
        /// <param name="isPaging"></param>
        /// <param name="data"></param>
        public static void SaveXlsx(string htmlTable, string forderPath, bool isPaging = false, DataTable data = null)
        {
            try
            {
                DataTable dt = ConvertHTMLTablesToDataTable(htmlTable);
                if (isPaging)
                {
                    dt = data;
                }
                //Exporting to Excel
                //Codes for the Closed XML
                using (XLWorkbook wb = new XLWorkbook())
                {
                    string fisrtCellData;
                    var headers = GetHeaderForTemplate(htmlTable, out fisrtCellData);
                    if (headers.Count != 0)
                    {
                        var ws = wb.Worksheets.Add("Bao cao");
                        foreach (var header in headers)
                        {
                            var cell = header.Merge.Split(':')[0];
                            ws.Cell(cell).Value = header.Title;
                            var style = ws.Cell(cell).Style;
                            style.Font.Bold = true;
                            style.Alignment.WrapText = true;
                            if (!string.IsNullOrEmpty(header.Width))
                            {
                                var collumn = cell.Substring(0, 1);
                                var col = ws.Column(collumn);
                                col.Width = Int32.Parse(header.Width);
                            }
                            
                            style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            ws.Range(header.Merge).Merge();
                        }
                        ws.Cell(fisrtCellData).InsertData(dt.AsEnumerable());
                    }
                    else
                    {
                        var ws = wb.Worksheets.Add(dt, "Customers");
                    }

                    wb.Style.Alignment.WrapText = true;
                    GetFile(wb, forderPath);// The method is defined below
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="specials"></param>
        /// <param name="forderPath"></param>
        /// <param name="pathTemplate"></param>
        public static void SaveToDatatable(DataTable data, DataTable specials, string forderPath, string pathTemplate = "")
        {
            try
            {
                if (string.IsNullOrEmpty(pathTemplate))
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(data, "Customers");

                        wb.Style.Alignment.WrapText = true;
                        GetFile(wb, forderPath);// The method is defined below
                    }
                }
                else
                {
                    var document = new XlsxParser(pathTemplate);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<Header> GetHeader()
        {
            // From a query
            var list = new List<Header>();
            list.Add(new Header() { Title = "Họ và tên", Merge = "A1:A3", Width = "100" });
            list.Add(new Header() { Title = "Tồn đâu kỳ", Merge = "B1:C1" });
            list.Add(new Header() { Title = "Tổng", Merge = "B2:B3", Width = "100" });
            list.Add(new Header() { Title = "Quá hạn", Merge = "C2:C3", Width = "100" });
            list.Add(new Header() { Title = "Nhận trong kỳ", Merge = "D1:D3", Width = "100" });
            list.Add(new Header() { Title = "Đã xử lý", Merge = "E1:I1" });
            list.Add(new Header() { Title = "Tồn cuối kỳ", Merge = "J1:K1" });
            list.Add(new Header() { Title = "Tổng", Merge = "E2:E3", Width = "100"  });
            list.Add(new Header() { Title = "Đúng hạn", Merge = "F2:G2" });
            list.Add(new Header() { Title = "Quá hạn", Merge = "H2:I2" });
            list.Add(new Header() { Title = "Tổng", Merge = "J2:J3", Width = "100" });
            list.Add(new Header() { Title = "Quá Hạn", Merge = "K2:K3", Width = "100" });
            list.Add(new Header() { Title = "Để Biết", Merge = "F3", Width = "100" });
            list.Add(new Header() { Title = "Để xử lý", Merge = "G3", Width = "100" });
            list.Add(new Header() { Title = "Để Biết", Merge = "H3", Width = "100" });
            list.Add(new Header() { Title = "Để xử lý", Merge = "I3", Width = "100" });
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelWorkbook"></param>
        /// <param name="fileName"></param>
        public static void GetFile(XLWorkbook excelWorkbook, string fileName)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelWorkbook"></param>
        /// <returns></returns>
        public static MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static DataTable ConvertHTMLTablesToDataTable(string HTML)
        {
            DataTable dt = null;
            //DataRow dr = null;
            //DataColumn dc = null;
            string TableExpression = "<table[^>]*>(.*?)</table>";
            string theadExpression = "<thead[^>]*>(.*?)</thead>";
            string HeaderExpression = "<th [^>]*>(.*?)</th>";
            string RowExpression = "<tr[^>]*>(.*?)</tr>";
            string ColumnExpression = "<td[^>]*>(.*?)</td>";
            bool HeadersExist = false;
            int iCurrentColumn = 0;
            int iCurrentRow = 0;
            int countHeader = 0;
            
            MatchCollection Tables = Regex.Matches(HTML, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match Table in Tables)
            {
                iCurrentRow = 0;
                HeadersExist = false;

                dt = new DataTable();

                MatchCollection theads = Regex.Matches(Table.Value, theadExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                
                if (Table.Value.Contains("<th"))
                {
                    HeadersExist = true;
                    foreach (Match thead in theads)
	                {
		                MatchCollection headerRows = Regex.Matches(thead.Value, RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        var lastHeader = headerRows.Count;
                        countHeader = headerRows.Count;
                        foreach (Match headerRow in headerRows)
                        {
                            lastHeader--;
                            if (lastHeader == 0)
                            {
                                MatchCollection Headers = Regex.Matches(headerRow.Value, HeaderExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                                foreach (Match Header in Headers)
                                {
                                    //dt.Columns.Add(Header.Groups(1).ToString);  
                                    var data = WebUtility.HtmlDecode(Header.Groups[1].ToString());
                                    dt.Columns.Add(data);

                                } 
                            }
                        }
	                }
                    
                }
                else
                {
                    for (int iColumns = 1; iColumns <= Regex.Matches(Regex.Matches(Regex.Matches(Table.Value, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).Count; iColumns++)
                    {
                        dt.Columns.Add("Column " + iColumns);
                    }
                }

                MatchCollection Rows = Regex.Matches(Table.Value, RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match Row in Rows)
                {
                    if (!(iCurrentRow < countHeader & HeadersExist == true))
                    {
                        var drow = dt.NewRow();
                        iCurrentColumn = 0;
                        MatchCollection Columns = Regex.Matches(Row.Value, ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        foreach (Match Column in Columns)
                        {
                            DataColumnCollection columns = dt.Columns;

                            //if (!columns.Contains("Column " + iCurrentColumn))
                            //{
                            //    dt.Columns.Add("Column " + iCurrentColumn);
                            //}
                            var data = WebUtility.HtmlDecode(Column.Groups[1].ToString());
                            drow[iCurrentColumn] = data;
                            iCurrentColumn += 1;
                        }
                        dt.Rows.Add(drow);
                    }
                    iCurrentRow += 1;
                }
            }
            return (dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="firstCellData"></param>
        /// <returns></returns>
        public static List<Header> GetHeaderForTemplate(string html, out string firstCellData)
        {
            firstCellData = "A"; 
            var header = new List<Header>();
            var template = "<jsonheader>(.*?)</jsonheader>";
            var templateCount = "<jsonheadercount>(.*?)</jsonheadercount>";
            MatchCollection configCounts = Regex.Matches(html, templateCount, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match configCount in configCounts)
            {
                var count = configCount.Groups[1].ToString();
                count = count.Trim();
                firstCellData += count;
            }

            MatchCollection configs = Regex.Matches(html, template, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match config in configs)
            {
                var content = config.Groups[1].ToString();
               
                var setting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Header>>(content);

                return setting;
            }

           

            return header;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<Header> GetHeaderForHtml(string html)
        {
            var header = new List<Header>();
            var template = "<jsonheadertemp>(.*?)</jsonheadertemp>";
           
            MatchCollection configs = Regex.Matches(html, template, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match config in configs)
            {
                var content = config.Groups[1].ToString();
                var setting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Header>>(content);

                return setting;
            }

            return header;
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class Header
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Justify { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Font { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Merge { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Width { get; set; }
    }
}
