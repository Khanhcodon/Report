using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Search
{
    internal class DatabaseIndex: IContentIndex
    {
        private readonly DocumentContentBll _documentContentService;

        internal DatabaseIndex(DocumentContentBll documentContentService)
        {
            _documentContentService = documentContentService;
        }

        public EgovIndex GetIndex(Lucene lucene)
        {
            if (lucene == null)
            {
                throw new ArgumentNullException("lucene");
            }
            var documentContents = _documentContentService.GetsByDocumentId(lucene.DocumentId).ToList();
            if (!documentContents.Any())
            {
                return null;
            }
            var contents =
                documentContents.Where(dc => !string.IsNullOrWhiteSpace(dc.Content)).Select(
                    dc => dc.FormTypeIdInEnum == Entities.FormType.HtmlForm
                              ? Microsoft.JScript.GlobalObject.unescape(dc.Content).StripHtml()
                              : dc.FormTypeIdInEnum == Entities.FormType.DynamicForm
                                    ? dc.Content.GetStringDataFromJson()
                                    : dc.Content).ToList();
            var document = lucene.Document;
            return new EgovIndex
            {
                Content = contents,
                CreatedDate = document.DateCreated,
                DocField = document.ListDocFieldId.Count == 0 ? new List<int> {0} : document.ListDocFieldId,
                DocType = document.DocTypeId.ToString(),
                Id = lucene.LuceneId.ToString(CultureInfo.InvariantCulture),
                Title = lucene.Title,
                DocumentId = document.DocumentId.ToString(),
                IsFile = lucene.IsFile,
                ContentId = lucene.ContentId
            };
        }
    }
}
