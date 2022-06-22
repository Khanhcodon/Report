using System;
using ClosedXML;
using System.IO;
using ClosedXML.Excel;

//using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml.Wordprocessing;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.Dynamic;
using System.Globalization;
using System.Text;
using Bkav.eGovCloud.Business.Utils;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class XlsxToJson
    {
        private XLWorkbook _workbook;

        /// <summary>
        /// Contructor
        /// </summary>
        public XlsxToJson()
        {

        }
        
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="fileStream">FileStream</param>
        public XlsxToJson(Stream fileStream)
        {
            _workbook = new XLWorkbook(fileStream);
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="fileStream">FileStream</param>
        public XlsxToJson(string filepath)
        {
            _workbook = new XLWorkbook(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream">FileStream</param>
        public List<Sheet> GetSheetCount(Stream fileStream)
        {
            var listData = new List<Sheet>();
            _workbook = new XLWorkbook(fileStream);
            var listSheet = _workbook.Worksheets;
            int i = 1;
            foreach (IXLWorksheet worksheet in listSheet)
            {
                var data = new Sheet();
                var nameSheet = worksheet.Name;
                data.IndexSheet = i;
                data.Name = nameSheet;
                listData.Add(data);
                i++;
            }  
            return listData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="headerStart"></param>
        /// <param name="headerEnd"></param>
        /// <returns></returns>
        public List<object> ConvertXlsxToJson(int indexSheet = 1, int start = 2, int headerStart = 1, int headerEnd = 1)
        {
            var listData = new List<object>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : headerEnd;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : start;
            var headerName = GetHeaderName(indexSheet, headerStart, headerEnd);
            for (int i = start ; i <= docRows; i++)
            {
                    var row = GetRow(i, indexSheet);
                    var data = new ExpandoObject() as IDictionary<string, object>;
                    for (int j = 1; j <= docColums; j++)
                    {

                        var columnValueObject = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue : row.Cell(j).CachedValue;
                        var columnValue = columnValueObject.ToString();
                        double price;
                        columnValue = columnValue.Trim();
                        bool isDouble = Double.TryParse(columnValue, out price);
                        if (isDouble)
                        {
                            columnValue = columnValue.Replace(",", ".");
                        }
                        columnValue = columnValue != "#DIV/0!" ? columnValue : "0";
                        columnValue = columnValue != "" ? columnValue : "0";
                        columnValue = columnValue != "-" ? columnValue : "0";
                        var columnName = headerName[j];
                        data.Add(string.IsNullOrEmpty(columnName) ? "Column" + j : columnName, columnValue);
                    }
                    data.Add("pos", i);
                    listData.Add(data);
            }

            return listData;
        }

        public List<object> GetDataToXlsx(int indexSheet = 1, int start = 2, int headerStart = 1, int headerEnd = 1, int rows = 0, int columns = 0, List<Dictionary<string, object>> dataConfig = null)
        {
            var listData = new List<object>();
            var listDataDefault = new List<IDictionary<string, object>>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = rows != 0 ? rows : CountInRowUsed(indexSheet);
            var docColums = columns != 0? columns : CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : headerEnd;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : start;
            var headerName = GetHeaderName(indexSheet, headerStart, headerEnd);
            for (int i = start; i <= docRows; i++)
            {
                if (i > start)
                {
                    var row = GetRow(i, indexSheet);
                    var data = new ExpandoObject() as IDictionary<string, object>;
                    for (int j = 1; j <= docColums; j++)
                    {

                        var columnValueObject = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue : row.Cell(j).CachedValue;
                        var columnValue = columnValueObject.ToString();
                        double price;
                        columnValue = columnValue.Trim();
                        bool isDouble = Double.TryParse(columnValue, out price);
                        if (isDouble)
                        {
                            columnValue = columnValue.Replace(",", ".");
                        }
                        columnValue = columnValue != "#DIV/0!" ? columnValue : "0";
                        columnValue = columnValue != "" ? columnValue : "0";
                        columnValue = columnValue != "-" ? columnValue : "0";
                        var columnName = headerName[j];
                        data.Add(string.IsNullOrEmpty(columnName) ? "Column" + j : columnName, columnValue);
                    }
                    data.Add("pos", i);
                    listDataDefault.Add(data);
                    listData.Add(data);
                }
            }

            var listDataLast = new List<object>();
            if (dataConfig != null)
            {
                for (int i = 0; i < dataConfig.Count; i++)
                {
                    var item = dataConfig[i];
                   
                    if (listDataDefault.Count() > i + 1)
                    {
                        var it = listDataDefault[i].ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                        foreach (KeyValuePair<string, object> entry in it)
                        {
                            if (item.ContainsKey(entry.Key))
                            {
                                item[entry.Key] = entry.Value;
                            }
                        }
                    }
                    listDataLast.Add(item);
                }
            }
            return listDataLast;
        }

        public List<object> ConvertXlsxToJsonAndFormat(int indexSheet = 1, int start = 2, int headerStart = 1, int headerEnd = 1)
        {
            var listData = new List<object>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : headerEnd;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : start;
            var headerName = GetHeaderName(indexSheet, headerStart, headerEnd);
            for (int i = start; i <= docRows; i++)
            {
                    var row = GetRow(i, indexSheet);
                    var datastyle = new ExpandoObject() as IDictionary<string, object>;
                    for (int j = 1; j <= docColums; j++)
                    {
                        var alginText = getFormat(sheet.Cell(i, j).Style.Alignment.Horizontal.ToString());
                        bool boldText = sheet.Cell(i, j).RichText.Phonetics.Bold;
                        bool italicText = sheet.Cell(i, j).RichText.Phonetics.Italic;
                        var underLineText = sheet.Cell(i, j).RichText.Phonetics.Underline.ToString();
                        var width = sheet.Column(j).Width.ToString();
                        if (boldText == true)
                        {
                            alginText += " htBold";
                        }
                        if (italicText == true)
                        {
                            alginText += " htItalic";
                        }
                        if (underLineText != "None")
                        {
                            alginText += " htUnderline";
                        }
                        datastyle.Add("Column" + j, alginText);
                    }
                    listData.Add(datastyle);
            }

            return listData;
        }
        public List<object> ConvertXlsxToJsonAndWidth(int indexSheet = 1, int start = 2, int headerStart = 1, int headerEnd = 1)
        {
            var listData = new List<object>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : headerEnd;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : start;
            var headerName = GetHeaderName(indexSheet, headerStart, headerEnd);
            for (int i = start; i <= docRows; i++)
            {
                    var row = GetRow(i, indexSheet);
                    var dataWidth = new ExpandoObject() as IDictionary<string, object>;
                    for (int j = 1; j <= docColums; j++)
                    {
                        double width = sheet.Column(j).Width;
                        var widthInt = (width * 6).ToString();
                        dataWidth.Add("Column" + j, widthInt);
                    }

                    listData.Add(dataWidth);
            }

            return listData;
        }

        //public string GetAllSheet(int indexSheet = 1) {
        //    var sheet = GetWorkSheet(indexSheet);
        //    foreach (IXLWorksheet worksheet in sheet.Worksheet)
        //    {
        //        Console.WriteLine(worksheet.Name); // outputs the current worksheet name.
        //                                           // do the thing you want to do on each individual worksheet.
        //    }
        //    return "";
        //}
        public string getFormat(string align) {
            string result = "";
            switch (align) {
                case "Center":
                    result = "htCenter";
                    break;
                case "Left":
                    result =  "htLeft";
                    break;
                case "Right":
                    result = "htRight";
                    break;
                default:
                    result = "htLeft";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="headerStart"></param>
        /// <param name="headerEnd"></param>
        /// <param name="headerEnd"></param>
        /// <returns>key</returns>
        public List<Dictionary<string, object>> ConvertXlsxToJsonXin(int indexSheet = 1, int startTitle = 0, int endTitle = 0, int startData = 0, int endData = 0, string key = "yearkey")
        {
            var listData = new List<Dictionary<string, object>>();
            var indexStartData = GetIndexHeader(indexSheet);
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);

            //check title va data theo co ban va nang cao
            startTitle = startTitle == 0 ? 1 : startTitle;
            endTitle = endTitle == 0 ? (indexStartData == 1 ? indexStartData : indexStartData - 1) : endTitle;
            startData = startData == 0 ? (indexStartData == 1 ? indexStartData + 1 : indexStartData) : startData;
            endData = endData == 0 ? docRows : endData;
            //check title va data theo co ban va nang cao

            var headerName = new Dictionary<int, string>();

            headerName = GetHeaderName(indexSheet, startTitle, endTitle);

            for (int i = startData; i <= endData; i++)
            {
                var row = GetRow(i, indexSheet);
                var data = new Dictionary<string, object>();
                for (int j = 1; j <= docColums; j++)
                {
                    var columnValueObject = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue : row.Cell(j).CachedValue;
                    var columnValue = columnValueObject.ToString();
                    double price;
                    columnValue = columnValue.Trim();
                    bool isDouble = Double.TryParse(columnValue, out price);
                    if (isDouble)
                    {
                        columnValue = columnValue.Replace(",", ".");
                    }

                    columnValue = columnValue != "#DIV/0!" ? columnValue : "";
                    columnValue = columnValue != "" ? columnValue : "";
                    columnValue = columnValue != "-" ? columnValue : "";
                    var columnName = headerName[j];
                    data.Add(string.IsNullOrEmpty(columnName) ? "Column" + j : columnName, columnValue);
                }

                listData.Add(data);
            }

            return listData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="headerStart"></param>
        /// <param name="headerEnd"></param>
        /// <param name="headerEnd"></param>
        /// <returns>key</returns>
        public List<Dictionary<string, object>> ConvertXlsxToJsonGroupByKey(int indexSheet = 1,int startTitle = 0,  int endTitle = 0, int startData = 0, int endData = 0, string key = "yearkey")
        {
            var listData = new List<Dictionary<string, object>>();
            var indexStartData = GetIndexHeader(indexSheet);
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);

            //check title va data theo co ban va nang cao
            startTitle =  startTitle == 0 ? 1 : startTitle ;
            endTitle = endTitle == 0 ? ( indexStartData ==1 ? indexStartData : indexStartData - 1 ) : endTitle;
            startData = startData == 0 ? (indexStartData ==1 ? indexStartData + 1 : indexStartData) : startData;
            endData = endData == 0 ? docRows : endData;
            //check title va data theo co ban va nang cao

            var headerName = new  Dictionary<int, string>();

            headerName = GetHeaderName(indexSheet, startTitle, endTitle);

            for (int i = startData; i <= endData; i++)
            {
                var row = GetRow(i, indexSheet);
                var data = new Dictionary<string, object>();
                for (int j = 1; j <= docColums; j++)
                {
                    var columnValueObject = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue : row.Cell(j).CachedValue;
                    var columnValue = columnValueObject.ToString();
                    double price;
                    columnValue = columnValue.Trim();
                    bool isDouble = Double.TryParse(columnValue, out price);
                    if (isDouble)
                    {
                        columnValue = columnValue.Replace(",", ".");
                    }

                    columnValue = columnValue != "#DIV/0!" ? columnValue : "";
                    columnValue = columnValue != "" ? columnValue : "";
                    columnValue = columnValue != "-" ? columnValue : "";
                    var columnName = headerName[j];
                    data.Add(string.IsNullOrEmpty(columnName) ? "Column" + j : columnName, columnValue);
                }

                listData.Add(data);
            }

            return listData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="headerStart"></param>
        /// <param name="headerEnd"></param>
        /// <returns></returns>
        public string HeaderToJson(int indexSheet = 1, int start = 2, int headerStart = 1, int headerEnd = 1)
        {
            var listData = new List<object>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : headerEnd;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : start;
            var listColumn = new List<List<object>>();

            for (int i = 1; i <= headerEnd; i++)
            {
                var row = GetRow(i, indexSheet);
                var listInRow = new List<object>();

                for (int j = 1; j <= docColums; j++)
                {
                    if (row.Cell(j).IsMerged() && row.Cell(j).MergedRange().ColumnCount() > 1)
                    {
                        var colspan = row.Cell(j).MergedRange().ColumnCount();
                        var cellValue = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue : row.Cell(j).CachedValue;

                        listInRow.Add(new { label= cellValue, colspan = colspan });

                        j = j + colspan - 1 ;
                    }
                    else
                    {
                        var cellValue = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue : row.Cell(j).CachedValue;

                        if (row.Cell(j).IsMerged())
                        {
                            var rowspan = row.Cell(j).MergedRange().RowCount();
                            listInRow.Add(new { label= cellValue, colspan = 1, rowspan = rowspan });
                        }
                        else
                        {
                            listInRow.Add(cellValue);
                        }
                    }
                }

                listColumn.Add(listInRow);
            }

            return JsonConvert.SerializeObject(listColumn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="headerStart"></param>
        /// <param name="headerEnd"></param>
        /// <returns></returns>
        public string GetAddressMegre(int indexSheet = 1, int start = 5, int headerStart = 1, int headerEnd = 3)
        {
            var listData = new List<object>();
            //var table = _workbook.GetTable(indexSheet);
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);

            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : 1;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : 1;

            List<string> list = new List<string>();

            var names = new Dictionary<int, string>();
            var comments = new Dictionary<int, string>();

            var merges = sheet.MergedRanges;
            foreach (var merge in merges)
            {
                var col = merge.RangeAddress.FirstAddress.ColumnNumber - 1;
                var row = merge.RangeAddress.FirstAddress.RowNumber - 1;
                var colspan = merge.ColumnCount();
                var rowspan = merge.RowCount();
                listData.Add(new { row = row, col = col, colspan = colspan, rowspan = rowspan, remove = false });
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(listData);
        }

        public List<object> GetDataHeader(int indexSheet = 1, int start = 5, int headerStart = 1, int headerEnd = 3)
        {
            var listData = new List<object>();
            //var table = _workbook.GetTable(indexSheet);
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);

            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : 1;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : 1;

            for (int i = headerStart; i <= headerEnd; i++)
            {
                var row = new List<string>();
                for (int j = 1; j <= docColums; j++)
                {
                    var cell = sheet.Cell(i, j);
                    row.Add(cell.CachedValue.ToString());
                }

                listData.Add(row);
            }
            return listData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="headerStart"></param>
        /// <param name="headerEnd"></param>
        /// <returns></returns>
        public List<object> GenDataMobileForm(Dictionary<string, object> data, int indexSheet = 1, int start = 2, int headerStart = 1, int headerEnd = 1, int columnData = 4)
        {
            var listData = new List<object>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : headerEnd;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : start;
            for (int index = 0; index < data.Count; index++)
            {
                var item = data.ElementAt(index);
                var rowIndex = start + index;
                sheet.Cell(rowIndex, columnData).Value = item.Value;
            }

            _workbook.Save();
            return listData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="headerStart"></param>
        /// <param name="headerEnd"></param>
        /// <returns></returns>
        public List<object> GenDataMobileFormFull(List<Dictionary<string, object>> data, string config, int indexSheet = 1, int start = 2, int headerStart = 1, int headerEnd = 1, int columnData = 1)
        {
            var listData = new List<object>();

            var configForm = JsonConvert.DeserializeObject<Dictionary<string, PropertyTypeForm>>(config);

            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : headerEnd;
            start = parts.Count() > 0 ? int.Parse(parts[1]) : start;
            var valueIndex = 0;
            foreach (var item in data)
            {
                var rowIndex = start + valueIndex;

                for (int i = 0; i < configForm.Count; i++)
                {
                    var valueData = configForm.ElementAt(i);
                    columnData = valueData.Value.index;
                    sheet.Cell(rowIndex, columnData).Value = item[valueData.Key];
                }
                valueIndex++;
            }
            _workbook.Save();
            return listData;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="workSheetPosition"></param>
        /// <param name="comments"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetHeaderNameAndComment(int workSheetPosition, out Dictionary<int, string> comments, int start = 1, int end = 1)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            if (workSheet == null)
            {
                throw new Exception("Worksheet not exist!");
            }
            var count = CountInColumnUsed(workSheetPosition);
            var names = new Dictionary<int, string>();
            comments = new Dictionary<int, string>();
            for (int i = start; i <= end; i++)
            {
                for (int j = 1; j <= count; j++)
                {
                    var cell = workSheet.Cell(i, j);
                    var value = cell.IsMerged() ? cell.MergedRange().FirstCell().Value.ToString() : cell.Value.ToString();
                    var name = value;
                    var comment = value;
                    if (name == "%")
                    {
                        try
                        {
                            var cellPrev = workSheet.Cell(i, j - 1);
                            name = "tl_" + cellPrev.Value.ToString();

                        }
                        catch (Exception)
                        {
                            name = "tl";
                        }
                    }
                    name = ConvertToAscii(name);
                    if (names.ContainsKey(j) && string.IsNullOrEmpty(name))
                    {
                    }
                    else
                    {
                        if (names.ContainsKey(j))
                        {
                            if (names[j].EndsWith("_" + name) || names[j].Equals(name))
                            {
                                names[j] = names[j];
                                comments[j] = comments[j];
                            }
                            else
                            {
                                names[j] = names[j] + "_" + name;
                                comments[j] = comments[j] + " - " + comment;
                            }
                        }
                        else
                        {
                            names[j] = name;
                            comments[j] = comment;
                        }
                    }
                }
            }

            for (int index = 0; index < names.Count; index++)
            {
                var item = names.ElementAt(index);
                var name = item.Value;
                if (item.Value.Length > 64)
                {
                    name = item.Value.Substring(item.Value.Length - 65, 64);
                }
                int testValue = 0;
                if (int.TryParse(name, out testValue))
                {
                    name = "Column_" + name;
                }

                name = name.Replace(";", "");
                names[item.Key] = name;
                while (names.Count(tr => tr.Value.Equals(name, StringComparison.CurrentCultureIgnoreCase)) > 1)
                {
                    names[item.Key] = name + "_1";
                    name = names[item.Key];
                };
            }

            return names;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="headerConfig"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConvertHeaderXlsxToJson(int indexSheet = 1, string headerConfig = "")
        {
            var listData = new List<object>();
            //var table = _workbook.GetTable(indexSheet);
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : 1;
            var start = parts.Count() > 0 ? int.Parse(parts[1]) : 1;
            var comments = new Dictionary<int, string>();
            var headerName = GetHeaderNameAndComment(indexSheet, out comments, 1, headerEnd);

            for (int index = 0; index < headerName.Count; index++)
            {
                var item = headerName.ElementAt(index);
                headerName[item.Key] = item.Value + "!!" + comments[item.Key];
            }

            var header = new Dictionary<string, string>();


            var row = GetRow(start -1 , indexSheet);
            var data = new ExpandoObject() as IDictionary<string, object>; ;
            for (int j = 1; j <= docColums; j++)
            {
                var columnValue = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue.ToString() : row.Cell(j).CachedValue.ToString();
                columnValue = (columnValue != "#DIV/0!" && columnValue != "") ? columnValue : "0";

                var valueType = columnValue.Length >1 ? "mediumtext" :  "string";
                try
                {
                    var value = int.Parse(columnValue);
                    valueType = "int";
                }
                catch (Exception)
                {
                    try
                    {
                        var value = float.Parse(columnValue);
                        valueType = columnValue.Length > 3 ? "double" : "float";
                    }
                    catch (Exception)
                    {
                    }
                }
                var columnName = headerName[j];
                columnName = string.IsNullOrEmpty(columnName) ? "Column" + j : columnName;
                header.Add(columnName, valueType);
                //data.Add(string.IsNullOrEmpty(columnName) ? "Column" + j : columnName, columnValue);
            }
            listData.Add(data);
            return header;
        }

        public Dictionary<string, object> ConvertHeaderXlsxToFormMobile(int indexSheet = 1, string headerConfig = "")
        {
            var listData = new List<object>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            var parts = strHeaderAI.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var headerEnd = parts.Count() > 0 ? int.Parse(parts[3]) : 1;
            var start = parts.Count() > 0 ? int.Parse(parts[1]) : 1;
            var comments = new Dictionary<int, string>();
            var headerName = GetHeaderNameAndComment(indexSheet, out comments, 1, headerEnd);

            var data = new Dictionary<string, object>();

            for (int index = 0; index < headerName.Count; index++)
            {
                var item = headerName.ElementAt(index);

                headerName[item.Key] = item.Value;
                dynamic objectConfig = new ExpandoObject();
                objectConfig.type = "string";
                objectConfig.name = comments[item.Key];
                objectConfig.defaultValue = "";
                objectConfig.index = index + 1;

                data.Add(item.Value.ToString(), objectConfig);
            }
            
            return data;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <returns></returns>
        public string GetHeaderAI(int indexSheet = 1)
        {
            var listData = new List<object>();
            //var table = _workbook.GetTable(indexSheet);
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            string formatValue = "";
            for (int i = 1; i <= docRows; i++)
            {
                var row = GetRow(i, indexSheet);
                var a = row.Cell(1).Style.Border;
                var template = "{0},{1},{2},{3}";
               
                if (row.Cell(1).Value.ToString().Trim() == "1,1" || row.Cell(1).Value.ToString().Trim() == "9999") { 
                    return  string.Format(template, indexSheet, i + 1, 1, i - 2);
                }
                else if (row.Cell(1).Value.ToString().Trim() == "A" && row.Cell(2).Value.ToString().Trim() == "B")
                {
                    formatValue =  string.Format(template, indexSheet, i + 1, 1, i - 1);
                }
                else if (row.Cell(1).Value.ToString().Trim() == "1" && row.Cell(2).Value.ToString().Trim() == "2")
                {
                    formatValue = string.Format(template, indexSheet, i + 1, 1, i - 1);
                }
            }
            return formatValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <returns></returns>
        public int GetIndexHeader(int indexSheet = 1)
        {
            var listData = new List<object>();

            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            int formatValue = 1;
            // check trong tu dong 2
            for (int i = 2; i <= docRows; i++)
            {
                var row = GetRow(i, indexSheet);
                if (row.Cell(1).Value.ToString().Trim() != "") {
                    formatValue = i;
                    break;
                }
            }
            return formatValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <returns></returns>
        public void SaveCSV(string filePath, int indexSheet = 1)
        {
            var sheet = GetWorkSheet(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            System.IO.File.WriteAllLines(filePath,
            sheet.RowsUsed().Select(row =>
                             string.Join(";", row.Cells(1, docColums)
                             .Select(cell => cell.GetValue<string>()))));
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
        /// 
        /// </summary>
        /// <param name="workSheetPosition"></param>
        /// <returns></returns>
        public int CountInRowUsed(int workSheetPosition)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            if (workSheet == null)
            {
                throw new Exception("Worksheet not exist!");
            }
            var totalRows = workSheet.LastRowUsed().RowNumber();
            return totalRows;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workSheetPosition"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public IXLTable GetTable(int workSheetPosition, int tableIndex = 1)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            if (workSheet == null)
            {
                throw new Exception("Worksheet not exist!");
            }
            var table = workSheet.Table(tableIndex);
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workSheetPosition"></param>
        /// <returns></returns>
        public int CountInColumnUsed(int workSheetPosition)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            if (workSheet == null)
            {
                throw new Exception("Worksheet not exist!");
            }
            var totalRows = workSheet.LastColumnUsed().ColumnNumber();
            return totalRows;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workSheetPosition"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetHeaderName(int workSheetPosition, int start = 1, int end = 1)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            if (workSheet == null)
            {
                throw new Exception("Worksheet not exist!");
            }
            var count = CountInColumnUsed(workSheetPosition);
            var names = new Dictionary<int, string>();
            for (int i = start; i <= end; i++)
            {
                for (int j = 1; j <= count; j++)
                {
                    var cell = workSheet.Cell(i, j);
                    var value = cell.IsMerged() ? cell.MergedRange().FirstCell().Value.ToString() : cell.Value.ToString();
                    var name = value;
                    if (name == "%")
                    {
                        try
                        {
                            var cellPrev = workSheet.Cell(i, j - 1);
                            name = "tl_" + cellPrev.Value.ToString();

                        }
                        catch (Exception)
                        {
                            name = "tl";
                        }
                    }
                    name = ConvertToAscii(name);
                    if (names.ContainsKey(j) && string.IsNullOrEmpty(name))
                    {
                    }
                    else
                    {
                        names[j] = names.ContainsKey(j) ? names[j].EndsWith("_" + name) || names[j].Equals(name) ? names[j] : names[j] + "_" + name : name;
                    }
                }
            }

            for (int index = 0; index < names.Count; index++)
            {
                var item = names.ElementAt(index);
                var name = item.Value;
                if (item.Value.Length > 64)
                {
                    name = item.Value.Substring(item.Value.Length - 65, 64);
                }
                int testValue = 0;
                if (int.TryParse(name, out testValue))
                {
                    name = "Column_" + name;
                }

                name = name.Replace(";", "");
                names[item.Key] = name;
                while (names.Count(tr => tr.Value.Equals(name, StringComparison.CurrentCultureIgnoreCase)) > 1)
                {
                    names[item.Key] = name + "_1";
                    name = names[item.Key];
                };
            }

            return names;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToAscii(string str)
        {
            str = str.ToLower().Trim();
            // str = Regex.Replace(str, @"\(.*?\)", string.Empty);
            //str = TCVN3ConvertUnicode.TCVN3ToUnicode(str);
            str = Regex.Replace(str, @"\(.*?\)", "");
            str = Regex.Replace(str, "[á|à|ả|ã|ạ|â|ă|ấ|ầ|ẩ|ẫ|ậ|ắ|ằ|ẳ|ẵ|ặ]", "a", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ]", "e", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự]", "u", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[í|ì|ỉ|ĩ|ị]", "i", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[ó|ò|ỏ|õ|ọ|ô|ơ|ố|ồ|ổ|ỗ|ộ|ớ|ờ|ở|ỡ|ợ]", "o", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[đ|Đ]", "d", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[ý|ỳ|ỷ|ỹ|ỵ|Ý|Ỳ|Ỷ|Ỹ|Ỵ]", "y", RegexOptions.IgnoreCase);
            str = str.Replace("%", "tl");
            str = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(str));
            str = Regex.Replace(str, "[,|~|@|/|.|:|?|#|$|%|&|*|(|)|+|”|“|'|\"|!|`|–|=]", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[<|>]", "__", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\s+", "");
            str = Regex.Replace(str, "[\\s]", "");
            str = Regex.Replace(str, @"-+", "");

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workSheetPosition"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetColumnName(int workSheetPosition, int columnName)
        {
            var workSheet = GetWorkSheet(workSheetPosition);
            if (workSheet == null)
            {
                throw new Exception("Worksheet not exist!");
            }
            var cl = workSheet.Column(columnName);
            return "";
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
            list.Add(new Header() { Title = "Tổng", Merge = "E2:E3", Width = "100" });
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<object> ConvertXlsxToHtml(int indexSheet = 1, int start = 1, int end = 1)
        {
            var listData = new List<object>();
            var sheet = GetWorkSheet(indexSheet);
            var docRows = CountInRowUsed(indexSheet);
            var docColums = CountInColumnUsed(indexSheet);
            var strHeaderAI = GetHeaderAI(indexSheet);
            
            for (int i = start; i <= end; i++)
            {
                var row = GetRow(i, indexSheet);
                var data = new ExpandoObject() as IDictionary<string, object>;
                for (int j = 1; j <= docColums; j++)
                {

                    var columnValueObject = row.Cell(j).IsMerged() ? row.Cell(j).MergedRange().FirstCell().CachedValue : row.Cell(j).CachedValue;
                    var columnValue = columnValueObject.ToString();
                    var alginText = getFormat(sheet.Cell(i, j).Style.Alignment.Horizontal.ToString());
                    bool boldText = sheet.Cell(i, j).RichText.Phonetics.Bold;
                    bool italicText = sheet.Cell(i, j).RichText.Phonetics.Italic;
                    var underLineText = sheet.Cell(i, j).RichText.Phonetics.Underline.ToString();
                    var width = sheet.Column(j).Width.ToString();
                    if (boldText == true)
                    {
                        alginText += " htBold";
                    }
                    if (italicText == true)
                    {
                        alginText += " htItalic";
                    }
                    if (underLineText != "None")
                    {
                        alginText += " htUnderline";
                    }
                    var columnName = row.Cell(j).Address.ColumnLetter;
                    data.Add(string.IsNullOrEmpty(columnName) ? "Column" + j : columnName, columnValue);
                }
                data.Add("pos", i);
                listData.Add(data);
            }

            return listData;
        }


        public static object GetPropValue(object src, string propName)
        {
            var names = src.GetType().GetProperties().Select(d => d.Name);
            return src.GetType().GetProperty(propName).GetValue(src, null);

        }
    }

    public class PropertyTypeForm
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        public string type { get; set; }
        public object defaultValue { get; set; }
        public int index { get; set; }
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
    /// <summary>
    /// 
    /// </summary>
    public class Sheet {
        /// <summary>
        /// 
        /// </summary>
        public int IndexSheet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}
