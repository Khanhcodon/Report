using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentExtension - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DocumentExtension trong CSDL
    /// </summary>
    public class DocumentExtension
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày văn bản
        /// </summary>
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người gửi
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người ký sao
        /// </summary>
        public int? CopySignerId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số đến đi
        /// </summary>
        public string DocNumber { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số bản
        /// </summary>
        public int? NumberCopy { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số trang
        /// </summary>
        public int? TotalPage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản hồi báo
        /// </summary>
        public Guid? ReplyDocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ cá nhân
        /// </summary>
        public int? StorePrivateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phiên bản văn bản
        /// </summary>
        public int DocumentVersion { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người báo cáo
        /// </summary>
        public int? ReporterId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tiến độ (ý kiến xử lý mới nhất)
        /// </summary>
        public string Progress { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mức ưu tiên
        /// </summary>
        public int? Proprity { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Hồ sơ cá nhân
        /// </summary>
        public virtual StorePrivate StorePrivate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Văn bản
        /// </summary>
        public virtual Document Document { get; set; }
    }
}
