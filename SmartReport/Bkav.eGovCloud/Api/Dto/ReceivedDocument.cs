using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// Công văn từ EO, BWSS
    /// </summary>
    [DataContract]
    public class ReceivedDocument
    {
        public ReceivedDocument()
        {
        }
        public ReceivedDocument(ReceivedDocument doc)
        {
            this.DocTypeId = doc.DocTypeId;
            this.Compendium = doc.Compendium;
            this.Comment = doc.Comment;
            this.Attachments = doc.Attachments;
            this.From = doc.From;
            this.To = doc.To;
            this.Content = doc.Content;
            this.Url = doc.Url;
            this.ContentUrl = doc.ContentUrl;
            this.NodeReceived = doc.NodeReceived;
            this.RelationDocumentCopysId = doc.RelationDocumentCopysId;
        }
        /// <summary>
        /// Trích yếu
        /// </summary>
        [DataMember]
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        [DataMember]
        public string Compendium { get; set; }

        /// <summary>
        /// Ý kiến xử lý
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// File đính kèm, dạng base64
        /// </summary>
        [DataMember]
        public IEnumerable<ReceivedAttachment> Attachments { get; set; }

        /// <summary>
        /// Người gửi
        /// </summary>
        [DataMember]
        public string From { get; set; }

        /// <summary>
        /// Người nhận
        /// </summary>
        [DataMember]
        public IEnumerable<string> To { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// EditLink BWSS cuả KQKLMR
        /// </summary>
        [DataMember]
        public string Url { get; set; }

        /// <summary>
        /// ContentLink BWSS cuả KQKLMR
        /// </summary>
        [DataMember]
        public string ContentUrl { get; set; }

        ///// <summary>
        ///// EditLink BWSS cuả KQKLMR
        ///// </summary>
        //[DataMember]
        //public string EditUrl { get; set; }

        /// <summary>
        /// NodeReceived
        /// </summary>
        [DataMember]
        public int NodeReceived { get; set; }

        /// <summary>
        /// RelationDocument
        /// </summary>
        [DataMember]
        public IEnumerable<int> RelationDocumentCopysId { get; set; }
    }
}