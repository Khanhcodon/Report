using System;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Xml;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// 
    /// </summary>
    public class HTMLtoDOCX
    {
        /// <summary>
        /// 
        /// </summary>
        public HTMLtoDOCX()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HTMLString"></param>
        /// <param name="DOCXSavePath"></param>
        public static string CreateFileFromHTML(string HTMLString, string DOCXSavePath = "")
        {
            if (DOCXSavePath == "")
            {
                DOCXSavePath = Path.Combine(ResourceLocation.Default.FileTemp, Guid.NewGuid() + ".docx");
            }

            SaveDOCX(DOCXSavePath, HTMLString, true, "portrait");
            return DOCXSavePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlainText"></param>
        /// <param name="DOCXSavePath"></param>
        public void CreateFileFromText(string PlainText, string DOCXSavePath)
        {
            SaveDOCX(DOCXSavePath, PlainText, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="xmlPath"></param>
        public void CreateFileFromXml(string path, string xmlPath)
        {
            SaveXMLDOCX(path, xmlPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyText"></param>
        public static string GetBase64DOCX(string bodyText)
        {
            var result = "";

            var filePath = Path.Combine(ResourceLocation.Default.FileTemp, Guid.NewGuid() + ".docx");
            SaveDOCX(filePath, bodyText, true);
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                result = stream.ToBase64String();
                File.Delete(filePath);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyText"></param>
        public static Stream GetStreamDOCX(string bodyText)
        {
            var filePath = Path.Combine(ResourceLocation.Default.FileTemp, Guid.NewGuid() + ".docx");
            SaveDOCX(filePath, bodyText, true);
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                return stream;
            }
        }

        //The base for this method was taken from the CreateDOCX project on openxmldeveloper.org
        //by Doug Mahugh. His orignal article can be found at:
        //http://openxmldeveloper.org/archive/2006/07/20/388.aspx
        //The original source code for that article appears to have come from:
        //http://blogs.msdn.com/dmahugh/archive/2006/06/27/649007.aspx
        //The following 3 comment lines are his, and have been left intact.
        /// <summary>
        /// The SaveDOCX method can be used as the starting point for a document-creation method in a class library, WinForm app, web page, or web service.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="BodyText"></param>
        /// <param name="IncludeHTML"></param>
        /// <param name="orienttation"></param>
        private static void SaveDOCX(string fileName, string BodyText, bool IncludeHTML, string orienttation = "landscape")
        {

            // Tham chiếu schema xml của word 2007
            var WordprocessingML = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            // Tạo gói package
            Package pkgOutputDoc = null;
            pkgOutputDoc = Package.Open(fileName, FileMode.Create, FileAccess.ReadWrite);

            // Tạo xml document của word.
            var xmlWordDocument = new XmlDocument();
            var tagDocument = xmlWordDocument.CreateElement("w:document", WordprocessingML);
            xmlWordDocument.AppendChild(tagDocument);

            var tagBody = xmlWordDocument.CreateElement("w:body", WordprocessingML);
            tagDocument.AppendChild(tagBody);

            var pageSizeElement = xmlWordDocument.CreateElement("w:sectPr", WordprocessingML);
            var landscapeElement = xmlWordDocument.CreateElement("w:pgSz", WordprocessingML);
            var orient = landscapeElement.Attributes.Append(xmlWordDocument.CreateAttribute("w:orient", WordprocessingML));
            orient.Value = orienttation;

            var width = landscapeElement.Attributes.Append(xmlWordDocument.CreateAttribute("w:w", WordprocessingML));
            width.Value = "12240";
            var height = landscapeElement.Attributes.Append(xmlWordDocument.CreateAttribute("w:h", WordprocessingML));
            height.Value = "15840";

            pageSizeElement.AppendChild(landscapeElement);
            tagBody.AppendChild(pageSizeElement);

            if (IncludeHTML)
            {
                var relationshipNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

                var tagAltChunk = xmlWordDocument.CreateElement("w:altChunk", WordprocessingML);
                var RelID = tagAltChunk.Attributes.Append(xmlWordDocument.CreateAttribute("r:id", relationshipNamespace));
                RelID.Value = "rId2";
                tagBody.AppendChild(tagAltChunk);
            }
            else
            {
                XmlElement tagParagraph = xmlWordDocument.CreateElement("w:p", WordprocessingML);
                tagBody.AppendChild(tagParagraph);
                XmlElement tagRun = xmlWordDocument.CreateElement("w:r", WordprocessingML);
                tagParagraph.AppendChild(tagRun);
                XmlElement tagText = xmlWordDocument.CreateElement("w:t", WordprocessingML);
                tagRun.AppendChild(tagText);

                // insert text into the start part, as a "Text" node ...
                XmlNode nodeText = xmlWordDocument.CreateNode(XmlNodeType.Text, "w:t", WordprocessingML);
                nodeText.Value = BodyText;
                tagText.AppendChild(nodeText);
            }

            // save the main document part (document.xml) ...
            var docuri = new Uri("/word/document.xml", UriKind.Relative);
            var docpartDocumentXML = pkgOutputDoc.CreatePart(docuri, "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml");
            var streamStartPart = new StreamWriter(docpartDocumentXML.GetStream(FileMode.Create, FileAccess.Write));

            xmlWordDocument.Save(streamStartPart);
            streamStartPart.Close();
            pkgOutputDoc.Flush();

            // create the relationship part
            pkgOutputDoc.CreateRelationship(docuri, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument", "rId1");
            pkgOutputDoc.Flush();

            var uriBase = new Uri("/word/document.xml", UriKind.Relative);
            var partDocumentXML = pkgOutputDoc.GetPart(uriBase);

            var uri = new Uri("/word/websiteinput.html", UriKind.Relative);
            var Origem = Encoding.UTF8.GetBytes(BodyText);
            var altChunkpart = pkgOutputDoc.CreatePart(uri, "text/html");
            using (Stream targetStream = altChunkpart.GetStream())
            {
                targetStream.Write(Origem, 0, Origem.Length);
            }

            var relativeAltUri = PackUriHelper.GetRelativeUri(uriBase, uri);
            partDocumentXML.CreateRelationship(relativeAltUri, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk", "rId2");
            


            // Đóng package
            pkgOutputDoc.Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="document"></param>
        private static void SaveXMLDOCX(string document, string fileName)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, isEditable: true))
            {
                string documentText;
                using (StreamReader reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    documentText = reader.ReadToEnd();
                }

                documentText = documentText.Replace("##Nhin troi mua do##", "Nhìn trời mưa đổ");
                documentText = documentText.Replace("##Thay dau buot them trong long##", "Thấy đau buốt thêm trong lòng");

                documentText = documentText.Replace("##Chan son got nho##", "Chân son gót nhỏ");
                documentText = documentText.Replace("##Troi xanh da an bai##", "Trời xanh đã an bài");

                documentText = documentText.Replace("##Yeu nhau nhu buom say hoa##", "Yêu nhau như bướm say hoa");
                documentText = documentText.Replace("##Dep nhu giac mong##", "Đẹp như giấc mộng");

                documentText = documentText.Replace("##Nam sau mua gio vi dau##", "Năm sau mưa gió vì đâu");
                documentText = documentText.Replace("##Xa dan nam thu ba##", "Xa dần năm thứ ba");

                using (StreamWriter writer = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    writer.Write(documentText);
                }

                wordDoc.MainDocumentPart.Document.Save();
                wordDoc.Close();
            }

        }
    }
}
