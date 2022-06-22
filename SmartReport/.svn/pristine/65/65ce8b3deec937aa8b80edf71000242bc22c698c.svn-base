using System;
using System.Collections.Generic;
using SolrNet.Attributes;

namespace Bkav.eGovCloud.Search
{
    public class EgovIndex
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrUniqueKey("documentid")]
        public string DocumentId { get; set; }

        [SolrField("title")]
        public string Title { get; set; }

        [SolrField("content")]
        public List<string> Content { get; set; }

        [SolrField("contentid")]
        public int? ContentId { get; set; }

        [SolrField("docfield")]
        public List<int> DocField { get; set; }

        [SolrField("doctype")]
        public string DocType { get; set; }

        [SolrField("createddate")]
        public DateTime CreatedDate { get; set; }

        [SolrField("isfile")]
        public bool IsFile { get; set; }
    }
}
