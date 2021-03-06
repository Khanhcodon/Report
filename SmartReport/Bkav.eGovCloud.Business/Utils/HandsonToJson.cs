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
using Newtonsoft.Json.Linq;
using Bkav.eGovCloud.Business.Customer;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business
{
    public class MergedCell
    {
        public int row { get; set; }

        public int col { get; set; }

        public int rowspan { get; set; }

        public int colspan { get; set; }

        public bool removed { get; set; }

     
    }

    public class HeaderObject
    {
        public HeaderObject() { }
        public HeaderObject(string typeName, string typeDB, string typeHandson, string regEx)
        {
            this.TypeName = typeName;
            this.TypeDB = typeDB;
            this.TypeHandson = typeHandson;
            this.RegEx = regEx;
        }

        public string TypeName { get; set; }

        public string TypeHandson { get; set; }

        public string TypeDB { get; set; }

        public bool ReadOnly { get; set; }

        public bool Hidden { get; set; }

        public bool Required { get; set; }

        public string RegEx { get; set; }

        public string Source { get; set; }

        public string CatalogName { get; set; }

        public bool HiddenMobile { get; set; }

        public bool IsInline { get; set; }

        public string DefaultValue { get; set; }

        public string FieldModel { get; set; }

        public string DataFieldModel { get; set; }

        public string PeriodModel { get; set; }

        public string LocalityModel { get; set; }

        public string DiaggregationModel { get; set; }

        public bool isAutoSize { get; set; }
    }

    public class XoayObject
    {
        public XoayObject() { }

        public int GiaTriIndex { get; set; }

        public int CatalogIndex { get; set; }

        public int CatalogCount { get; set; }

        public JArray CatalogValues { get; set; }

        public JArray CatalogValuesAscii { get; set; }

        public string CatalogName { get; set; }

        public string CatalogNameAscii { get;set; }

        public string GiaTriName { get; set; }

        public string GiaTriNameAscii { get; set; }

        public string CatalogType { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class HandsonToJson
    {
        JArray fieldTableData { get; set; }
        JArray configTableData { get; set; }
        JArray valueTableData { get; set; }

        JObject xoayInfo { get; set; }
        int countCols { get; set; }
        int countRows { get; set; }

        // 20191226 VuHQ START Cù Trọng Xoay
        public XoayObject XoayObject { get; set; }
        public dynamic DefineFieldObject { get; set; }

        public Dictionary<int, string> XoayHeaders { get; set; }
        // 20191226 VuHQ END Cù Trọng Xoay

        public HandsonToJson() { }
        /// <summary>
        /// Contructor
        /// </summary>
        public HandsonToJson(dynamic defineFieldObject, dynamic defineConfigObject, dynamic defineValueObject)
        {
            fieldTableData = defineFieldObject.data;
            countCols = defineFieldObject.countCols;
            countRows = fieldTableData.Count;

            configTableData = defineConfigObject.data;
            valueTableData = defineValueObject.data;

            xoayInfo = defineValueObject.xoayInfo;

            if (xoayInfo != null)
            {
                XoayObject = new XoayObject();
                XoayObject.GiaTriIndex = int.Parse(xoayInfo["index_gia_tri"] != null ? xoayInfo["index_gia_tri"].ToString() : "0");
                XoayObject.CatalogIndex = int.Parse(xoayInfo["index_catalog"] != null? xoayInfo["index_catalog"].ToString() :"0");
                XoayObject.CatalogType = xoayInfo["CatalogType"] != null? xoayInfo["CatalogType"].ToString() : "0";
                //XoayObject.CatalogCount = int.Parse(xoayInfo["catalog_count"].ToString());
                //XoayObject.CatalogValues = (JArray)xoayInfo["CatalogValues"];
            }
            else
            {
                XoayObject = null;
            }

            DefineFieldObject = defineFieldObject;
        }

        /// <returns></returns>
        public Dictionary<string, string> ConvertHeaderHandsonToJson(out Dictionary<string, string> xoayHeader)
        {
            var header = new Dictionary<string, string>();
            xoayHeader = new Dictionary<string, string>();
            Dictionary<int, string> comments = null;

            var listData = new List<object>();
            var headerName = GetHeaderNameAndComment(out comments, fieldTableData);

            if (XoayObject != null)
            {
                XoayObject.CatalogNameAscii = headerName.ElementAt(XoayObject.CatalogIndex).Value;
                XoayObject.GiaTriNameAscii = headerName.ElementAt(XoayObject.GiaTriIndex).Value;

                XoayObject.CatalogName = comments.ElementAt(XoayObject.CatalogIndex).Value;
                XoayObject.GiaTriName = comments.ElementAt(XoayObject.GiaTriIndex).Value;
            }

            for (int index = 0; index < headerName.Count; index++)
            {
                var item = headerName.ElementAt(index);
                headerName[item.Key] = item.Value + "!!" + comments[item.Key];
            }

            // gán type cho field
            // 
            string[,] typeDimesion = new string[,] { { "Số nguyên", "Số thực", "Kí tự - ", "Kí tự - Kí tự ngắn", "Kí tự - Kí tự dài", "Thời gian", "Checkbox", "Catalog" },
                { "int", "double", "varchar", "varchar", "text", "datetime", "bit", "varchar" }
            };

            string typeName = string.Empty;
            string columnName = string.Empty;
            var typeCode = string.Empty;

            var i = 0;
            foreach (var item in configTableData)
            {
                columnName = headerName[i];
                if (item[countRows].ToString().Equals("Kí tự"))
                    typeName = item[countRows].ToString() + " - " + item[countRows + 1].ToString();
                else
                    typeName = item[countRows].ToString();

                for (int j = 0; j < typeDimesion.Length / 2; j++)
                {
                    if (typeName.Equals(typeDimesion[0, j]))
                    {
                        typeCode = typeDimesion[1, j];
                        break;
                    }
                }

                if (string.IsNullOrEmpty(typeCode))
                    typeCode = "float";
                columnName = string.IsNullOrEmpty(columnName) ? "Column" + i : columnName;
                header.Add(columnName, typeCode);

                i++;
            }

            // 20191226 VuHQ START Cù Trọng Xoay
            //if (XoayObject != null)
            //{
            //    i = 0;

            //    var fieldTableDataXoay = (JArray)fieldTableData.DeepClone();
            //    fieldTableDataXoay.Clear();
            //    fieldTableDataXoay.Add(XoayObject.CatalogValues);
            //    var headerNameXoay = GetHeaderNameAndComment(out comments, fieldTableDataXoay, XoayObject.CatalogCount, 1);
            //    XoayHeaders = new Dictionary<int, string>(headerNameXoay);

            //    for (int index = 0; index < headerNameXoay.Count; index++)
            //    {
            //        var item = headerNameXoay.ElementAt(index);
            //        headerNameXoay[item.Key] = item.Value + "!!" + comments[item.Key];
            //    }

            //    foreach (var item in header)
            //    {
            //        if (i != XoayObject.GiaTriIndex)
            //        {
            //            if (i == XoayObject.CatalogIndex)
            //            {
            //                var giaTriHeader = header.ElementAt(XoayObject.GiaTriIndex);
            //                for (var j = 0; j < XoayObject.CatalogCount; j++)
            //                {
            //                    xoayHeader.Add(headerNameXoay.ElementAt(j).Value, giaTriHeader.Value);
            //                }
            //            }
            //            else
            //            {
            //                xoayHeader.Add(item.Key, item.Value);
            //            }
            //        }
            //        i++;
            //    }
            //}
            // 20191226 VuHQ END Cù Trọng Xoay

            return header;
        }

        public List<HeaderObject> GetHeaderTemplates()
        {
            List<HeaderObject> headerTemplates = new List<HeaderObject>();
            headerTemplates.Add(new HeaderObject("Số nguyên", "int", "numeric", @"^\d{0}$"));
            //headerTemplates.Add(new HeaderObject("Số thực", "double", "numeric", @"^\d+(\.\d+){0}$"));
            headerTemplates.Add(new HeaderObject("Số thực", "double", "numeric", @"^[-+]?[0-9]*\.?[0-9]{0}$"));
            headerTemplates.Add(new HeaderObject("Kí tự", "varchar", "text", @".{0}?"));
            headerTemplates.Add(new HeaderObject("Kí tự - ", "text", "text", @".{0}?"));
            headerTemplates.Add(new HeaderObject("Kí tự - Kí tự ngắn", "varchar", "text", @".{0}?"));
            headerTemplates.Add(new HeaderObject("Kí tự - Kí tự dài", "text", "text", @".{0}?"));
            headerTemplates.Add(new HeaderObject("Thời gian", "datetime", "date", @".{0}?"));
            headerTemplates.Add(new HeaderObject("Checkbox", "bit", "checkbox", @"^(true{0})$"));
            headerTemplates.Add(new HeaderObject("Catalog", "varchar", "dropdown", @".{0}?"));

            headerTemplates.Add(new HeaderObject("Kí tự - Kí tự ngắn", "string", "text", @".{0}?"));
            headerTemplates.Add(new HeaderObject("Kí tự - Kí tự dài", "mediumtext", "text", @".{0}?"));
            headerTemplates.Add(new HeaderObject("Số thực", "float", "numeric", @"^[-+]?[0-9]*\.?[0-9]{0}$"));
            headerTemplates.Add(new HeaderObject("Hình ảnh", "text", "text", @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)"));
            return headerTemplates;
        }

        public Dictionary<string, HeaderObject> ConvertHeaderHandsonToJsonExtra(out Dictionary<string, HeaderObject> xoayHeader, bool? isBCCP)
        {
            var boolisBCCP = isBCCP;
            xoayHeader = new Dictionary<string, HeaderObject>();
            var header = new Dictionary<string, HeaderObject>();
            Dictionary<int, string> comments = null;

            var listData = new List<object>();
            var headerName = GetHeaderNameAndComment(out comments, fieldTableData);

            for (int index = 0; index < headerName.Count; index++)
            {
                var item = headerName.ElementAt(index);
                headerName[item.Key] = item.Value + "!!" + comments[item.Key];
            }

            // gán type cho field
            List<HeaderObject> headerTemplates = GetHeaderTemplates();

            var typeName = string.Empty;
            HeaderObject headerObject = null;
            var columnName = string.Empty;

            for (int i = 0; i < countCols; i++)
            {
                columnName = headerName[i];
                if (configTableData[i][countRows].ToString().Equals("Kí tự"))
                    typeName = configTableData[i][countRows].ToString() + " - " + configTableData[i][countRows + 1].ToString();
                else
                    typeName = configTableData[i][countRows].ToString();

                foreach(HeaderObject headerTemplate in headerTemplates)
                {
                    headerObject = null;
                    if (typeName.Equals(headerTemplate.TypeName))
                    {
                        headerObject = new HeaderObject();
                        headerObject.TypeName = headerTemplate.TypeName;
                        headerObject.TypeDB = headerTemplate.TypeDB;
                        headerObject.TypeHandson = headerTemplate.TypeHandson;
                        headerObject.CatalogName = configTableData[i][countRows + 1].ToString();
                        headerObject.DefaultValue = configTableData[i][countRows + 2].ToString();
                        
                        //headerObject.Required = string.IsNullOrEmpty(configTableData[i][countRows + 3].ToString()) ? false : bool.Parse(configTableData[i][countRows + 3].ToString());
                        //headerObject.ReadOnly = string.IsNullOrEmpty(configTableData[i][countRows + 4].ToString()) ? false : bool.Parse(configTableData[i][countRows + 4].ToString());
                        //headerObject.Hidden = string.IsNullOrEmpty(configTableData[i][countRows + 7].ToString()) ? false : bool.Parse(configTableData[i][countRows + 7].ToString());
                        //headerObject.isAutoSize = string.IsNullOrEmpty(configTableData[i][countRows + 12].ToString()) ? false : bool.Parse(configTableData[i][countRows + 12].ToString());
                        headerObject.Required = getCheckBoxValue(countRows + 3, i, configTableData);
                        headerObject.ReadOnly = getCheckBoxValue(countRows + 4, i, configTableData);
                        headerObject.Hidden = getCheckBoxValue(countRows + 7, i, configTableData);
                        headerObject.isAutoSize = getCheckBoxValue(countRows + 12, i, configTableData);
                        if (configTableData[i].Count() > 15) {
                            if(boolisBCCP == true)
                            {
                                headerObject.FieldModel = configTableData[i][countRows + 13].ToString();
                                headerObject.DataFieldModel = configTableData[i][countRows + 14].ToString();
                                headerObject.PeriodModel = configTableData[i][countRows + 15].ToString();
                                headerObject.LocalityModel = configTableData[i][countRows + 16].ToString();
                                headerObject.DiaggregationModel = configTableData[i][countRows + 17].ToString();
                            }        
                        }
                    


                        if (configTableData[i].Count() > 11)
                            headerObject.HiddenMobile = string.IsNullOrEmpty(configTableData[i][countRows + 10].ToString()) ? false : bool.Parse(configTableData[i][countRows + 10].ToString());
                        else
                            headerObject.HiddenMobile = false;

                        if (configTableData[i].Count() > 12)
                            headerObject.IsInline = string.IsNullOrEmpty(configTableData[i][countRows + 11].ToString()) ? false : bool.Parse(configTableData[i][countRows + 11].ToString());
                        else
                            headerObject.IsInline = false;

                        if (string.IsNullOrEmpty(configTableData[i][countRows + 5].ToString()) || configTableData[i][countRows + 5].ToString().Equals("Độ dài: (_, ___)"))
                        {
                            if (typeName.Equals("Checkbox"))
                                headerObject.RegEx = headerObject.Required ? string.Format(headerTemplate.RegEx, "") : string.Format(headerTemplate.RegEx, "|null|false");
                            else
                                headerObject.RegEx = headerObject.Required ? string.Format(headerTemplate.RegEx, "+") : string.Format(headerTemplate.RegEx, "*");
                            break;
                        } else
                        {
                            string minValueStr = string.Empty;
                            string maxValueStr = string.Empty;
                            var arrString = configTableData[i][countRows + 5].ToString();
                            if (!string.IsNullOrEmpty(arrString))
                            {
                                arrString = arrString.Split(':')[1];
                                arrString = ReplaceChars(arrString, new char[] { '(', ')', '_' });
                                int value;
                                if (Int32.TryParse(arrString.Split(',')[0], out value))
                                {
                                    minValueStr = arrString.Split(',')[0].Trim();
                                }

                                if (Int32.TryParse(arrString.Split(',')[1], out value))
                                {
                                    maxValueStr = arrString.Split(',')[0].Trim();
                                }

                                headerObject.RegEx = headerObject.Required ? string.Format(headerTemplate.RegEx, "{" + minValueStr + "," + maxValueStr + "}") 
                                    : string.Format(headerTemplate.RegEx, "*");
                            }
                            break;
                        }
                    }
                }
                if (headerObject != null)
                {
                    columnName = string.IsNullOrEmpty(columnName) ? "Column" + i : columnName;
                    header.Add(columnName, headerObject);
                }
            }
            
            return header;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workSheetPosition"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetHeaderNameAndComment(out Dictionary<int, string> comments, JArray _fieldTableData, int? _countCols = 0, int _countRows = 0)
        {
            if (_countCols == 0 && _countRows == 0)
            {
                _countCols = countCols;
                _countRows = countRows;
            }

            var names = new Dictionary<int, string>();
            comments = new Dictionary<int, string>();
            for (int i = 0; i < _countRows; i++)
            {
                for (int j = 0; j < _countCols; j++)
                {
                    var name = _fieldTableData[i][j].ToString();
                    var comment = name;
                    if (name == "%")
                    {
                        try
                        {
                            var valuePrev = _fieldTableData[i][j - 1].ToString();
                            name = "tl_" + valuePrev;
                        }
                        catch (Exception)
                        {
                            name = "tl";
                        }
                    }

                    // Kiểm tra trong cấu hình của DefineFieldJson xem vị trí cell này có trong mergedCell hay ko.
                    var mergedCells = (List<MergedCell>)JsonConvert.DeserializeObject<List<MergedCell>>(DefineFieldObject.mergedCells.ToString());
                    var isMergCell = mergedCells.Exists(p => p.row == i && j > p.col && j - p.col < p.colspan);
                    if (string.IsNullOrEmpty(name) && i != countRows - 1 && isMergCell)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (!string.IsNullOrEmpty(_fieldTableData[i][k].ToString()))
                            {
                                name = _fieldTableData[i][k].ToString();
                                comment = _fieldTableData[i][k].ToString();
                                break;
                            }
                        }
                    }

                    name = ConvertToAscii(name);

                    if (names.ContainsKey(j) && string.IsNullOrEmpty(name))
                    { }
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
                    name = item.Value.Substring(item.Value.Length - 64, 64);
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
                    if (names[item.Key].Length > 64)
                    {
                        names[item.Key] =  names[item.Key].Substring( names[item.Key].Length - 64, 64);
                    }
                    names[item.Key] = names[item.Key].Substring(1);
                    names[item.Key] = "f" + names[item.Key];
                    name = names[item.Key];
                };
                int n;
                string _firstValue = name.Substring(0,1);
                bool isNumeric = int.TryParse(_firstValue, out n);
                if (isNumeric) {
                    names[item.Key] = names[item.Key].Substring(1);
                    names[item.Key] = "f" + names[item.Key];
                    name = names[item.Key];
                }

            }

            return names;
        }

        public List<object> ConvertHandsonToJson(Dictionary<string, string> header, bool isXoay)
        {
            var listData = new List<object>();
            var headerName = string.Empty;
            var data = new ExpandoObject() as IDictionary<string, object>;
            var cellValue = string.Empty;
            double price;

            var _countCols = 0;
            for (int i = 0; i < valueTableData.Count; i++)
            {
                if (isXoay)
                    _countCols = valueTableData[i].Count();
                else
                    _countCols = countCols;

                data = new ExpandoObject() as IDictionary<string, object>;
                for (int j = 0; j < _countCols; j++)
                {
                    headerName = header.ElementAt(j).Key.Split(new string[] { "!!" }, StringSplitOptions.None)[0];
                    cellValue = valueTableData[i][j].ToString();

                    bool isDouble = Double.TryParse(cellValue, out price);
                    if (isDouble)
                    {
                        cellValue = cellValue.Replace(",", ".");
                    }
                    data.Add(string.IsNullOrEmpty(headerName) ? "Column" + j : headerName, cellValue);
                }
                data.Add("pos", i);

                listData.Add(data);
            }

            return listData;
        }

        public string BuildQuery(Dictionary<string, string> header, bool isXoay)
        {
            var listData = new List<object>();
            var headerName = string.Empty;
            var data = new ExpandoObject() as IDictionary<string, object>;
            var cellValue = string.Empty;
            double price;
            var query = "";
            var _countCols = 0;
            for (int i = 0; i < valueTableData.Count; i++)
            {
                var subQuery = "Select ";

                if (isXoay)
                    _countCols = valueTableData[i].Count();
                else
                    _countCols = countCols;

                data = new ExpandoObject() as IDictionary<string, object>;
                for (int j = 0; j < _countCols; j++)
                {
                    headerName = header.ElementAt(j).Key.Split(new string[] { "!!" }, StringSplitOptions.None)[0];
                    cellValue = valueTableData[i][j].ToString();

                    bool isDouble = Double.TryParse(cellValue, out price);
                    if (isDouble)
                    {
                        cellValue = cellValue.Replace(",", ".");
                    }
                    
                    if (cellValue.Trim().StartsWith("LK(") || cellValue.Trim().StartsWith("TB(") || cellValue.Trim().StartsWith("TH("))
                    {
                    }
                    else
                    {
                        cellValue = "'" + cellValue + "'";
                    }

                    subQuery += cellValue + " as " + "'" + headerName + "'";

                    if (j != _countCols - 1)
                    {
                        subQuery += ", ";
                    }
                    data.Add(string.IsNullOrEmpty(headerName) ? "Column" + j : headerName, cellValue);
                }
                query += subQuery;
                if (i != valueTableData.Count - 1)
                {
                    query += " union ";
                }
            }

            query = query.Replace("LK(", "Get_LuyKe(").Replace("TB(", "Get_FactModel(").Replace("TH(", "Get_TongHopDiaBan(");
            return query;
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
            str = Regex.Replace(str, "[,|~|@|/|.|:|?|#|$|%|&|*|(|)|+|”|“|'|\"|!|`|–]", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "[<|>]", "__", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\s+", "");
            str = Regex.Replace(str, "[\\s]", "");
            str = Regex.Replace(str, @"[-|+|=|]", "");

            return str;
        }

        public string ConvertHeaderFormCodeToFormMobile(string formCode, int countCols)
        {
            string[] item = null;
            var listData = new List<object>();

            dynamic formCodeObject = JsonConvert.DeserializeObject(formCode);
            var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(formCodeObject["header"].ToString());
            var data = new Dictionary<string, object>();

            int index = 1;
            foreach (KeyValuePair<string, string> header in headers)
            {
                item = header.Key.Split(new string[] { "!!" }, StringSplitOptions.None);
                dynamic objectConfig = new ExpandoObject();
                objectConfig.name = item[1];
                objectConfig.type = "string";

                objectConfig.defaultValue = "";
                objectConfig.index = index;

                data.Add(item[0], objectConfig);

                index++;
            }

            return JsonConvert.SerializeObject(data);
        }

        public Dictionary<string, HeaderObject> parseColumnSetting(Form form)
        {
            dynamic formCode = JsonConvert.DeserializeObject(form.FormCode);

            if (formCode.header == null) return new Dictionary<string, HeaderObject>();

            var headerTemplates = GetHeaderTemplates();

            var columnSetting = new Dictionary<string, HeaderObject>();
            // 1. generate extra - columnSetting
            var typeName = string.Empty;
            HeaderObject headerObject = null;
            var columnName = string.Empty;

            int i = 1;
            var headersOld = JsonConvert.DeserializeObject<Dictionary<string, string>>(formCode.header.ToString());

            foreach (KeyValuePair<string, string> item in headersOld)
            {
                var headerTemplate = headerTemplates.Where(p => p.TypeDB == item.Value).First();
                headerObject = new HeaderObject();

                headerObject.TypeName = headerTemplate.TypeName;
                headerObject.TypeDB = headerTemplate.TypeDB;
                headerObject.TypeHandson = headerTemplate.TypeHandson;
                headerObject.CatalogName = "";
                headerObject.Required = false;
                headerObject.ReadOnly = false;
                headerObject.RegEx = headerObject.Required ? string.Format(headerTemplate.RegEx, "+") : string.Format(headerTemplate.RegEx, "*");

                columnName = string.IsNullOrEmpty(columnName) ? "Column" + i : columnName;
                columnSetting.Add(item.Key, headerObject);

                i++;
            }

            return columnSetting;
        }
        //    public string parseFormcodeXlsToHandson(Form form)
        //{
        //    var columnSetting = new Dictionary<string, HeaderObject>();


        //    dynamic formCode = JsonConvert.DeserializeObject(form.FormCode);
        //    var headerTemplates = GetHeaderTemplates();

        //    // generate extra
        //    // 1. generate extra - columnSetting
        //    var typeName = string.Empty;
        //    HeaderObject headerObject = null;
        //    var columnName = string.Empty;

        //    int i = 1;
        //    var headersOld = JsonConvert.DeserializeObject<Dictionary<string, string>>(formCode.header.ToString());

        //    foreach (KeyValuePair<string, string> item in headersOld)
        //    {
        //        var headerTemplate = headerTemplates.Where(p => p.TypeDB == item.Value).First();
        //        headerObject = new HeaderObject();

        //        headerObject.TypeName = headerTemplate.TypeName;
        //        headerObject.TypeDB = headerTemplate.TypeDB;
        //        headerObject.TypeHandson = headerTemplate.TypeHandson;
        //        headerObject.CatalogName = "";
        //        headerObject.Required = false;
        //        headerObject.ReadOnly = false;
        //        headerObject.RegEx = headerObject.Required ? string.Format(headerTemplate.RegEx, "+") : string.Format(headerTemplate.RegEx, "*");

        //        columnName = string.IsNullOrEmpty(columnName) ? "Column" + i : columnName;
        //        columnSetting.Add(item.Key, headerObject);

        //        i++;
        //    }

        //    // generate extra - headerSetting
        //    var headerSetting = formCode.dataHeader;

        //    // generate extra - mergedCells
        //    var headerNested = JsonConvert.DeserializeObject(formCode.headerMerge.ToString());

        //    // generate colWidths
        //    var colWidths = new int[0];

        //    var extra = JsonConvert.SerializeObject(new
        //    {
        //        columnSetting = columnSetting,
        //        headerSetting = headerSetting,
        //        mergedCells = headerNested
        //    });

        //    return extra;
        //}

        #region private method

        private bool getCheckBoxValue(int columnIndex, int rowIndex, JArray array)
        {
            var isInBound = (columnIndex >= 0) && (columnIndex < array[rowIndex].Count());
            var isChecked = false;
            if (isInBound)
            {
                isChecked = string.IsNullOrEmpty(array[rowIndex][columnIndex].ToString()) ? false : bool.Parse(array[rowIndex][columnIndex].ToString());
            }

            return isChecked;
        }

        private string ReplaceChars(object obj, char[] options)
        {
            foreach (char o in options)
            {
                obj = obj.ToString().Replace(o.ToString(), string.Empty);
            }

            return obj.ToString();
        }
        #endregion
    }
}
