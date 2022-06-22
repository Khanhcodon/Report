using System;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Models
{
    public class ApproverModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public int ApproverId { get; set; }

        /// <summary>
        /// Get or set the document copy id.
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Get or set the document id
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Get or set the approver id.
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// Get or set the approve's content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get or set the approved datetime
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Get or set the approved result
        /// </summary>
        public bool IsSuccess { get; set; }

        public bool IsDocSuccess { get; set; }

        public bool IsDraft { get; set; }

        /// <summary>
        /// Get or set the user send
        /// </summary>
        public virtual User UserSend { get; set; }
    }
}