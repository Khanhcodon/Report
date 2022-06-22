using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentContent
    {
        private ICollection<DocumentContentDetail> _documentContentDetails;

        /// <summary>
        /// Key
        /// </summary>
        public int DocumentContentId { get; set; }

        /// <summary>
        /// Get or set document id.
        /// </summary>
        public Guid DocumentId { get; set; }

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
        /// Get or set the type of form: dynamic form or html form.
        /// </summary>
        public Entities.FormType FormTypeIdInEnum
        {
            get { return (Entities.FormType)FormTypeId; }
            set { FormTypeId = (int)value; }
        }

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
        public virtual ICollection<DocumentContentDetail> DocumentContentDetails
        {
            get { return _documentContentDetails ?? (_documentContentDetails = new List<DocumentContentDetail>()); }
            set { _documentContentDetails = value; }
        }

        /// <summary>
        /// EditUrl BWSS nếu là kqklmr
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// ContentUrl BWSS nếu là kqklmr
        /// </summary>
        public string ContentUrl { get; set; }

        ///// <summary>
        ///// ContentUrl BWSS nếu là kqklmr
        ///// </summary>
        //public string EditUrl { get; set; }
    }
}
