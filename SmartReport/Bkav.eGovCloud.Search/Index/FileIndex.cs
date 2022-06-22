using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Search
{
    internal class FileIndex: IContentIndex
    {
        private readonly AttachmentBll _attachmentService;
        private readonly FileManager _fileManager;

        internal FileIndex(AttachmentBll attachmentService)
        {
            _attachmentService = attachmentService;
            _fileManager = FileManager.Default;
        }

        public EgovIndex GetIndex(Lucene lucene)
        {
            if(lucene == null)
            {
                throw new ArgumentNullException("lucene");
            }
            if(!lucene.ContentId.HasValue)
            {
                throw new Exception("Id file đính kèm bị null");
            }
            var attachhment = _attachmentService.Get(lucene.ContentId.Value);
            if(attachhment == null)
            {
                return null;
            }
            if(attachhment.IsDeleted)
            {
                return null;
            }
            var content = GetContent(attachhment);
            if(string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            var document = lucene.Document;
            return new EgovIndex
                       {
                           Content = new List<string> {content},
                           CreatedDate = document.DateCreated,
                           DocField = document.ListDocFieldId.Count == 0 ? new List<int> { 0 } : document.ListDocFieldId,
                           DocType = document.DocTypeId.ToString(),
                           Id = lucene.LuceneId.ToString(CultureInfo.InvariantCulture),
                           Title = lucene.Title,
                           DocumentId = document.DocumentId.ToString(),
                           IsFile = lucene.IsFile,
                           ContentId = lucene.ContentId
                       };
        }

        private string GetContent(Attachment attachment)
        {
            string content;
            var fileName = attachment.AttachmentName;
            var fileExtension = Path.GetExtension(fileName);
            var file = _attachmentService.DownloadAttachmentForCreateIndex(attachment);
            switch (fileExtension)
            {
                case ".pdf":
                    var pdfParser = new PdfParser();
                    content = pdfParser.ExtractText(file);
                    break;
                case ".docx":
                    var dtt = new DocxParser();
                    using (var ms = new MemoryStream())
                    {
                        int count;
                        do
                        {
                            var buf = new byte[1024];
                            count = file.Read(buf, 0, 1024);
                            ms.Write(buf, 0, count);
                        } while (file.CanRead && count > 0);
                        content = dtt.ExtractText(ms);
                    }
                    break;
                case ".doc":
                    var temp = _fileManager.Create(file, ResourceLocation.Default.FileTemp,null, extension: "doc");
                    var loader = new DocParser(temp.FullName);
                    loader.LoadText(out content);
                    _fileManager.Delete(temp.FullName);
                    break;
                case ".txt":
                case ".xml":
                    StreamReader readFile;
                    using (readFile = new StreamReader(file))
                    {
                        content = readFile.ReadToEnd();
                    }
                    break;
                case ".html":
                case ".htm":
                    using (var stream = new StreamReader(file))
                    {
                        content = stream.ReadToEnd().StripHtml();
                    }
                    break;
                default:
                    content = "";
                    break;
            }
            return content;
        }
    }
}
