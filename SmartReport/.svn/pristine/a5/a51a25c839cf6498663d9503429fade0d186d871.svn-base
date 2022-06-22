using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bảng công văn người đã xem.
    /// </summary>
    public class DocumentViewed
    {
        /// <summary>
        /// Lấy hoặc thiết lập key
        /// </summary>
        public int DocumentViewedId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập document id
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập document copy id
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập document copy
        /// </summary>
        public virtual DocumentCopy DocumentCopy { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập user
        /// </summary>
        public virtual User User { get; set; }
    }
}
