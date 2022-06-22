using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

using DocumentFormat.OpenXml.Packaging;
using ICSharpCode.SharpZipLib.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Spire.Doc;

using Word = Microsoft.Office.Interop.Word;

using Bkav.eGovCloud.Core.FileSystem;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// 
    /// </summary>
    public class DocxParser
    {
        private const string ContentTypeNamespace =
            @"http://schemas.openxmlformats.org/package/2006/content-types";

        private const string WordprocessingMlNamespace =
            @"http://schemas.openxmlformats.org/wordprocessingml/2006/main";

        private const string DocumentXmlXPath =
            "/t:Types/t:Override[@ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml\"]";

        private const string BodyXPath = "/w:document/w:body";

        #region ExtractText()

        /// <summary>
        /// Extracts text from the Docx file.
        /// </summary>
        /// <returns>Extracted text.</returns>
        public string ExtractText(string docxFile)
        {
            if (string.IsNullOrEmpty(docxFile))
                throw new Exception("Input file not specified.");
            var zip = new ZipFile(docxFile);
            var docxFileLocation = FindDocumentXmlLocation(zip);

            if (string.IsNullOrEmpty(docxFileLocation))
                throw new Exception("It is not a valid Docx file.");

            return ReadDocumentXml(zip, docxFileLocation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docxFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string ExtractText(Stream docxFile)
        {
            if (docxFile == null)
                throw new ArgumentNullException("docxFile");

            var zip = new ZipFile(docxFile);
            var docxFileLocation = FindDocumentXmlLocation(zip);

            if (string.IsNullOrEmpty(docxFileLocation))
                throw new Exception("It is not a valid Docx file.");

            return ReadDocumentXml(zip, docxFileLocation);
        }

        #endregion

        #region Extract XML

        /// <summary>
        /// Trả về đối tượng xml của docx
        /// </summary>
        /// <param name="docxFile"></param>
        /// <returns></returns>
        public XmlDocument ExtractXml(Stream docxFile)
        {
            if (docxFile == null)
                throw new ArgumentNullException("docxFile");

            var zip = new ZipFile(docxFile);
            var docxFileLocation = FindDocumentXmlLocation(zip);

            if (string.IsNullOrEmpty(docxFileLocation))
                throw new Exception("It is not a valid Docx file.");

            XmlDocument xmlDoc = null;
            foreach (ZipEntry entry in zip)
            {
                if (String.Compare(entry.Name, docxFileLocation, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    var documentXml = zip.GetInputStream(entry);

                    xmlDoc = new XmlDocument { PreserveWhitespace = true };
                    xmlDoc.Load(documentXml);
                    documentXml.Close();

                    var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                    nsmgr.AddNamespace("w", WordprocessingMlNamespace);
                }
            }
            zip.Close();
            return xmlDoc;
        }

        #endregion

        #region FindDocumentXmlLocation()

        /// <summary>
        /// Gets location of the "document.xml" zip entry.
        /// </summary>
        /// <returns>Location of the "document.xml".</returns>
        private static string FindDocumentXmlLocation(ZipFile zip)
        {
            foreach (ZipEntry entry in zip)
            {
                if (String.Compare(entry.Name, "[Content_Types].xml", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    var contentTypes = zip.GetInputStream(entry);

                    var xmlDoc = new XmlDocument { PreserveWhitespace = true };
                    xmlDoc.Load(contentTypes);
                    contentTypes.Close();

                    //Create an XmlNamespaceManager for resolving namespaces
                    var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                    nsmgr.AddNamespace("t", ContentTypeNamespace);

                    // Find location of "document.xml"
                    if (xmlDoc.DocumentElement != null)
                    {
                        var node = xmlDoc.DocumentElement.SelectSingleNode(DocumentXmlXPath, nsmgr);

                        if (node != null)
                        {
                            var location = ((XmlElement)node).GetAttribute("PartName");
                            return location.TrimStart(new[] { '/' });
                        }
                    }
                    break;
                }
            }
            zip.Close();
            return null;
        }

        #endregion

        #region ReadDocumentXml()

        /// <summary>
        /// Reads "document.xml" zip entry.
        /// </summary>
        /// <returns>Text containing in the document.</returns>
        private string ReadDocumentXml(ZipFile zip, string docxFileLocation)
        {
            var sb = new StringBuilder();

            foreach (ZipEntry entry in zip)
            {
                if (String.Compare(entry.Name, docxFileLocation, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    var documentXml = zip.GetInputStream(entry);

                    var xmlDoc = new XmlDocument { PreserveWhitespace = true };
                    xmlDoc.Load(documentXml);
                    documentXml.Close();

                    var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                    nsmgr.AddNamespace("w", WordprocessingMlNamespace);

                    if (xmlDoc.DocumentElement != null)
                    {
                        var node = xmlDoc.DocumentElement.SelectSingleNode(BodyXPath, nsmgr);

                        if (node == null)
                            return string.Empty;

                        sb.Append(ReadNode(node));
                    }
                    break;
                }
            }
            zip.Close();
            return sb.ToString();
        }

        #endregion

        #region ReadNode()

        /// <summary>
        /// Reads content of the node and its nested childs.
        /// </summary>
        /// <param name="node">XmlNode.</param>
        /// <returns>Text containing in the node.</returns>
        private string ReadNode(XmlNode node)
        {
            if (node == null || node.NodeType != XmlNodeType.Element)
                return string.Empty;

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
                        sb.Append(Environment.NewLine);
                        break;

                    case "tab":                         // Tab
                        sb.Append("\t");
                        break;

                    case "p":                           // Paragraph
                        sb.Append(ReadNode(child));
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        break;

                    default:
                        sb.Append(ReadNode(child));
                        break;
                }
            }
            return sb.ToString();
        }

        #endregion

        #region Template by Docx

        /// <summary>
        /// Parse template docx và trả về Stream sau khi parse
        /// </summary>
        /// <param name="path">Đường dẫn file template</param>
        /// <param name="keyValues">Danh sách key-value cần thay thế</param>
        /// <returns>Stream nội dung file docx sau khi parse</returns>
        public Stream ParseTemplate(string path, Dictionary<string, string> keyValues)
        {
            MemoryStream result = new MemoryStream();
            string documentText;
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, isEditable: false))
            {
                using (StreamReader reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    documentText = reader.ReadToEnd();
                }

                foreach (var key in keyValues.Keys)
                {
                    documentText = documentText.Replace(key, keyValues[key]);
                }

                using (StreamWriter writer = new StreamWriter(result))
                {
                    writer.Write(documentText);
                    writer.Flush();
                    result.Position = 0;
                    return result;
                }
            }
        }

        /// <summary>
        /// Parse template docx sau đó tạo thành file mới theo đường dẫn truyền vào và trả về đường dẫn file mới.
        /// </summary>
        /// <param name="path">Đường dẫn file template</param>
        /// <param name="keyValues">Danh sách key-value cần thay thế</param>
        /// <param name="outPath">Đường dẫn file đầu ra</param>
        /// <returns>Đường dẫn file đầu ra</returns>
        public void ParseTemplateToFile(string path, Dictionary<string, string> keyValues, string outPath)
        {
            try
            {
                File.Copy(path, outPath);
            }
            catch
            {
                throw new Exception("Đường dẫn đầu ra không hợp lệ.");
            }

            var result = new MemoryStream();
            string documentText;
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outPath, isEditable: true))
            {
                using (StreamReader reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    documentText = reader.ReadToEnd();
                }

                foreach (var key in keyValues.Keys)
                {
                    documentText = documentText.Replace(key, keyValues[key]);
                }

                using (StreamWriter writer = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    writer.Write(documentText);
                }

                wordDoc.MainDocumentPart.Document.Save();
                wordDoc.Close();
            }
        }

        /// <summary>
        /// Parse temlate docx ra file pdf và trả về đường dẫn file mới
        /// </summary>
        /// <param name="path">Đường dẫn file template docx</param>
        /// <param name="keyValues">Danh sách key-value cần thay thế</param>
        /// <param name="outPath">Đường dẫn file mới</param>
        /// <returns>Đường dẫn file mới</returns>
        public void ParseTemplateToPdf(string path, Dictionary<string, string> keyValues, string outPath)
        {
            var outTempfile = FileUtil.GetRandomGuidFile(Path.GetTempPath(), ".docx");
            ParseTemplateToFile(path, keyValues, outTempfile);

            ParseToPdf(outTempfile, outPath);

            try
            {
                File.Delete(outTempfile);
            }
            catch { }
        }

        #endregion

        #region Docx To Pdf

        /// <summary>
        /// Convert Docx to pdf
        /// </summary>
        /// <param name="docxPath"></param>
        /// <param name="path"></param>
        public static void ParseToPdf(string docxPath, string path)
        {
            var tempPath = Path.GetTempFileName();
            var document = new Spire.Doc.Document();
            document.LoadFromFile(docxPath);
            document.SaveToFile(tempPath, FileFormat.PDF);

            RemoveSpireText(tempPath, path);

            try
            {
                File.Delete(tempPath);
            }
            catch { }
        }

        /// <summary>
        /// Convert Docx to pdf
        /// <para>Hàm dùng thư viện free của Spire nên chỉ được tối đa 3 trang.</para>
        /// </summary>
        /// <param name="docxStream"></param>
        /// <param name="path"></param>
        public void ParseToPdf(Stream docxStream, string path)
        {
            var tempPath = Path.GetTempFileName();
            var document = new Spire.Doc.Document();
            document.LoadFromStream(docxStream, FileFormat.Docx);
            document.SaveToFile(tempPath, FileFormat.PDF);

            RemoveSpireText(tempPath, path);

            try
            {
                File.Delete(tempPath);
            }
            catch { }
        }

        private static void RemoveSpireText(string inputPath, string outPath)
        {
            try
            {
                using (Stream inputPdfStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputPdfStream = new FileStream(outPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                using (Stream outputPdfStream2 = new FileStream(outPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var reader = new PdfReader(inputPdfStream);
                    var t = new CustomTextExtractionStrategy("Evaluation Warning : The document was created with Spire.Doc for .NET");
                    var text = PdfTextExtractor.GetTextFromPage(reader, 1, t);

                    if (t.myPoints.Count > 0)
                    {
                        var point = t.myPoints[0];

                        var stamper = new PdfStamper(reader, outputPdfStream) { FormFlattening = true, FreeTextFlattening = true };

                        // Tạo một ảnh màu trắng chèn lên trên dòng chữ Evaluation của Spire
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(new Bitmap(400, 20), BaseColor.WHITE);
                        image.SetAbsolutePosition(point.Rect.Left, point.Rect.Bottom);
                        stamper.GetOverContent(1).AddImage(image, true);

                        stamper.Close();

                        var reader2 = new PdfReader(outputPdfStream2);

                        PdfEncryptor.Encrypt(
                            reader2,
                            outputPdfStream2,
                            null,
                            Encoding.UTF8.GetBytes("test"),
                            PdfWriter.ALLOW_PRINTING,
                            true
                        );

                        reader2.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var a = ex;
            }
        }

        #endregion

        #region Docx to Image

        /// <summary>
        /// Convert tệp doc và docx sang Pdf sử dụng thư viện Interop của Microsoft.
        /// </summary>
        /// <param name="inputPath">Đường dẫn file Word.</param>
        /// <param name="outputPath">Đường dẫn file pdf sinh ra.</param>
        /// <remarks>
        /// - Yêu cầu cài đặt MS-Office trên server: sử dụng bản 2013 thì càng tốt.
        /// - Một số thiết lập cần thiết:
        ///   + Thiết lập application pool chạy quyền local system
        ///   + Create the directory C:\Windows\SysWOW64\config\systemprofile\Desktop (for the 32-bit version of Excel/Office on a 64-bit Windows computer) 
        ///      or C:\Windows\System32\config\systemprofile\Desktop (for a 32-bit version of Office on a 32-bit Windows computer or a 64-bit version of Office on a 64-bit Windows computer).
        ///   + For the Desktop directory, add Full control permissions for the relevant user (for example in Win7 and IIS 7 and DefaultAppPool set permissions for user IIS AppPool\DefaultAppPool).
        /// </remarks>
        public static void ConvertToPdf(string inputPath, string outputPath)
        {
            object fileName = inputPath;
            object objDNS = Word.WdSaveOptions.wdDoNotSaveChanges;
            object missing = System.Reflection.Missing.Value;

            Word.Document aDoc = null;

            var WordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                aDoc = WordApp.Documents.Open(fileName);
                if (aDoc == null)
                {
                    throw new Exception("Word không mở được tệp truyền vào.");
                }

                aDoc.ExportAsFixedFormat(outputPath, Word.WdExportFormat.wdExportFormatPDF);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (aDoc != null)
                {
                    ((Word._Document)aDoc).Close(ref objDNS, ref missing, ref missing);
                    aDoc = null;
                }
                if (WordApp != null)
                {
                    ((Word._Application)WordApp).Quit(ref objDNS, ref missing, ref missing);
                    WordApp = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #endregion
    }
}
