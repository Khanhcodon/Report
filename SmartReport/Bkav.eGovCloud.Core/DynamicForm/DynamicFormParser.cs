using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <summary>
    /// Lớp convert docx sang fỏm động
    /// </summary>
    public static class DynamicFormParser
    {
        private const string WordprocessingMlNamespace =
            @"http://schemas.microsoft.com/office/2006/xmlPackage";

        private const string DocumentXmlXPath =
            "/t:Types/t:Override[@ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml\"]";

        private const string BodyXPath = "/pkg:package/pkg:part/pkg:xmlData/w:document/w:body";

        /// <summary>
        /// Convert from docx xml file sang form động
        /// </summary>
        /// <param name="inputXmlStream"></param>
        /// <returns></returns>
        public static string ParseFromDocx(Stream inputXmlStream)
        {
            var sb = new StringBuilder();

            var xmlDoc = new XmlDocument { PreserveWhitespace = true };
            xmlDoc.Load(inputXmlStream);

            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("pkg", WordprocessingMlNamespace);
            nsmgr.AddNamespace("w", @"http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            var result = new List<JsControl>();

            if (xmlDoc.DocumentElement != null)
            {
                var node = xmlDoc.DocumentElement.SelectSingleNode(BodyXPath, nsmgr);

                if (node == null)
                {
                    return "";
                }
                // sb.AppendLine(ReadNode(node));
            }

            var contentLines = GetContentLines(sb.ToString());
            var row = 1;
            var id = 1;
            foreach (var line in contentLines)
            {
                // result.Add(ParseControl(line, row, id));
                row++;
                id++;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputXmlStream"></param>
        /// <returns></returns>
        public static string ParseFromDocxNew(Stream inputXmlStream)
        {
            var sb = new StringBuilder();

            var xmlDoc = new XmlDocument { PreserveWhitespace = true };
            xmlDoc.Load(inputXmlStream);

            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("pkg", WordprocessingMlNamespace);
            nsmgr.AddNamespace("w", @"http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            var result = new List<JsControl>();

            if (xmlDoc.DocumentElement == null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }

            var bodyNode = xmlDoc.DocumentElement.SelectSingleNode(BodyXPath, nsmgr);

            if (bodyNode == null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }

            var row = 1;
            var id = 1;
            foreach (XmlNode node in bodyNode.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element) continue;

                var localName = node.LocalName;
                if (!localName.Equals("p") && !localName.Equals("tbl"))
                {
                    continue;
                }

                if (localName.Equals("p"))
                {
                    // Paragraph
                    bool isBold;
                    string justify;
                    bool isItalic;

                    var text = ReadNode(node, out isBold, out isItalic, out justify).Trim();
                    if (string.IsNullOrEmpty(text))
                    {
                        continue;
                    }

                    result.AddRange(ParseControl(text, row, id, isBold, isItalic, justify));
                    row++;
                    id++;
                }

                if (localName.Equals("tbl"))
                {
                    var tblRows = node.SelectNodes("w:tr", nsmgr);
                    foreach (XmlNode tblRow in tblRows)
                    {
                        var tblCols = tblRow.SelectNodes("w:tc", nsmgr);
                        foreach (XmlNode tblCol in tblCols)
                        {
                            var width = 798 / (tblCols.Count) - 8;

                            bool isBold;
                            string justify;
                            bool isItalic;

                            var colText = ReadNode(tblCol, out isBold, out isItalic, out justify).Trim();

                            result.AddRange(ParseControl(colText, row, id, isBold, isItalic, justify, width));
                            id++;
                        }
                        row++;
                    }
                }
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        private static List<JsControl> ParseControl(string text, int row, int id, bool isBold, bool isItalic, string justify, float w = 798)
        {
            var result = new List<JsControl>();

            var regex = new Regex("([0-9]*.[^\\.])+[ ]*[\\.\\.…]+"); //([0-9]*.[^\\.])+[((\\.){3}(\\.))(…)]+
            var matches = regex.Matches(text);

            text = ReplaceHtmlChars(text);

            if (matches.Count == 0)
            {
                result.Add(ParseLabelControl(text, row, id, isBold, isItalic, justify, w));
            }
            else
            {
                var width = w / (matches.Count) - 8;
                foreach (Match itm in matches)
                {
                    result.Add(ParseTextboxControl(itm.ToString(), row, id, isBold, isItalic, justify, width));
                    id += 200;
                }
            }

            return result;
        }

        private static string ReplaceHtmlChars(string text)
        {
            text = text.Replace("\"", "'").Replace("<", "(").Replace(">", ")");
            return text;
        }

        private static JsControl ParseTextboxControl(string line, int row, int id, bool isBold, bool isItalic, string justify, float w = 798)
        {
            var result = new JsControl();

            var regex = new Regex("[(\\.)(…)]+");
            line = regex.Replace(line, "");

            var textLength = line.Length;
            var height = textLength < 123 ? "23px" : "42px";
            var width = (int)w + "px";

            var mash = line.ToLower().Contains("điện thoại") ? "mobile" : "text";

            result.TypeId = 9;
            result.PosOrder = 1;
            result.PosRow = (short)row;
            result.Properties = new List<JsProperty>();
            result.Properties.Add(new JsProperty() { Id = 15, Value = Guid.NewGuid().ToString("N") });
            result.Properties.Add(new JsProperty() { Id = 17, Value = line });
            result.Properties.Add(new JsProperty() { Id = 6, Value = width });
            result.Properties.Add(new JsProperty() { Id = 7, Value = height });
            result.Properties.Add(new JsProperty() { Id = 2, Value = isBold ? "bold" : "normal" });
            result.Properties.Add(new JsProperty() { Id = 3, Value = isItalic ? "italic" : "normal" });
            result.Properties.Add(new JsProperty() { Id = 18, Value = justify });
            result.Properties.Add(new JsProperty() { Id = 19, Value = mash });
            result.Properties.Add(new JsProperty() { Id = 28, Value = "@@" });
            result.Properties.Add(new JsProperty() { Id = 29, Value = "@" });
            result.Properties.Add(new JsProperty() { Id = 26, Value = "@" });
            result.Properties.Add(new JsProperty() { Id = 8, Value = "none" });
            result.Properties.Add(new JsProperty() { Id = 20, Value = "none" });
            result.Properties.Add(new JsProperty() { Id = 16, Value = "false" });
            result.Properties.Add(new JsProperty() { Id = 13, Value = "false" });

            return result;
        }

        private static JsControl ParseLabelControl(string line, int row, int id, bool isBold, bool isItalic, string justify, float w = 798)
        {
            var result = new JsControl();

            var textLength = line.Length;
            var height = textLength < 123 ? "23px" : "42px";
            var width = (int)w + "px";

            result.TypeId = 1;
            result.PosOrder = 1;
            result.PosRow = (short)row;
            result.Properties = new List<JsProperty>();
            result.Properties.Add(new JsProperty() { Id = 15, Value = id.ToString() });
            result.Properties.Add(new JsProperty() { Id = 1, Value = line });
            result.Properties.Add(new JsProperty() { Id = 6, Value = width });
            result.Properties.Add(new JsProperty() { Id = 7, Value = height });
            result.Properties.Add(new JsProperty() { Id = 2, Value = isBold ? "bold" : "normal" });
            result.Properties.Add(new JsProperty() { Id = 3, Value = isItalic ? "italic" : "normal" });
            result.Properties.Add(new JsProperty() { Id = 18, Value = justify });

            return result;
        }

        private static List<string> GetContentLines(string text)
        {
            var contentLines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var contents = new List<string>();
            var content = "";
            for (var i = 0; i < contentLines.Length; i++)
            {
                var line = contentLines[i];
                if (line.StartsWith(")"))
                {
                    content += line;
                    contents.Add(content);
                    content = "";
                    continue;
                }

                if (line.EndsWith("(") || line.EndsWith("( "))
                {
                    var nextContent = contentLines[i + 1];
                    content = line + nextContent;
                    if (nextContent.EndsWith(")"))
                    {
                        contents.Add(content);
                        content = "";
                    }
                    i++;
                    continue;
                }

                if (line.StartsWith(":"))
                {
                    var lastContent = contents.Last();
                    contents[contents.Count - 1] = lastContent + line;
                    continue;
                }

                contents.Add(line);
                content = "";
            }

            return contents;
        }

        private static string ReadNode(XmlNode node, out bool isBold, out bool isItalic, out string justify)
        {
            isBold = false;
            isItalic = false;
            justify = "";

            if (node == null || node.NodeType != XmlNodeType.Element)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType != XmlNodeType.Element) continue;

                switch (child.LocalName)
                {
                    case "t":                           // Text
                        sb.Append(child.InnerText.TrimEnd());
                        
                        string space = ((XmlElement)child).GetAttribute("xml:space");
                        if (!string.IsNullOrEmpty(space) && space == "preserve")
                            sb.Append(' ');

                        break;

                    case "cr":                          // Carriage return
                    case "br":                          // Page break
                        break;

                    case "tab":                         // Tab
                        break;
                    case "i":
                        isItalic = true;
                        break;
                    case "b":
                        isBold = true;
                        break;
                    case "jc":
                        var attr = child.Attributes["w:val"];
                        if (attr != null)
                        {
                            justify = attr.Value;
                        }
                        break;
                    default:
                        bool isBoldChild;
                        string justifyChild;
                        sb.Append(ReadNode(child, out isBoldChild, out isItalic, out justifyChild));

                        isBold |= isBoldChild;
                        justify = String.IsNullOrEmpty(justifyChild) ? justify : justifyChild;

                        break;
                }
            }

            return sb.ToString();
        }
    }
}
