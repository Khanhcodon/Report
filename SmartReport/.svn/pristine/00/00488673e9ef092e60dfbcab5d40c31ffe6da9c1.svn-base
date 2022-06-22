using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Models
{
    public class DocumentContentModel
    {
        private ICollection<DocumentContentDetailModel> _documentContentDetails;

        /// <summary>
        /// Key
        /// </summary>
        public int DocumentContentId { get; set; }

        /// <summary>
        /// Get or set document id.
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Get or set the form id.
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Get or set the content name = form name
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// Get or set the document's content: form or html.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get or set the type of form: dynamic form or html form.
        /// </summary>
        public int FormTypeId { get; set; }

        /// <summary>
        /// Get or set the main form.
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phiên bản cuối cùng
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các phiên bản nội dung của vb,hs
        /// </summary>
        public virtual ICollection<DocumentContentDetailModel> DocumentContentDetails
        {
            get { return _documentContentDetails ?? (_documentContentDetails = new List<DocumentContentDetailModel>()); }
        }

        /// <summary>
        /// Url BWSS nếu là kqklmr
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// ContentUrl BWSS nếu là kqklmr
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// ContentUrl BWSS nếu là kqklmr
        /// </summary>
        public string ConfigTemplate { get; set; }

        ///// <summary>
        ///// ContentUrl BWSS nếu là kqklmr
        ///// </summary>
        //public string EditUrl { get; set; }

        /// <summary>
        /// ContentUrl BWSS nếu là kqklmr
        /// </summary>
        public string FormCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfigFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompilationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChildCompilationId { get; set; }

        // 20191125 VuHQ START REQ-5
        /// <summary>
        /// DefineFieldJson
        /// </summary>
        public string DefineFieldJson { get; set; }

        /// <summary>
        /// DefineConfigJson
        /// </summary>
        public string DefineConfigJson { get; set; }

        /// <summary>
        /// DefineValueJson
        /// </summary>
        public string DefineValueJson { get; set; }

        /// <summary>
        /// FormHeader
        /// </summary>
        public string FormHeader { get; set; }

        /// <summary>
        /// FormFooter
        /// </summary>
        public string FormFooter { get; set; }
        // 20191125 VuHQ END REQ-5

        // 20200210 VuHQ Phase 2 - REQ 0
        public string ExplicitTemplate { get; set; }

        // 20200210 VuHQ Phase 2 - REQ 0
        public string Compilation { get; set; }

        public int? FormCategoryId { get; set; }
    }
}