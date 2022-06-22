using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Lucene - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Increase trong CSDL
    /// </summary>
    public class Lucene
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của nội dung cần đánh index
        /// </summary>
        public long LuceneId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tiêu đề nội dung cần đánh index (nếu là văn bản hồ sơ thì là Trích yếu, nếu là file thì là tên file)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id file đính kèm
        /// </summary>
        public int? ContentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian cập nhật cuối cùng
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra nội dung đánh index này là file
        /// </summary>
        public bool IsFile { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra nội dung đã được đánh index
        /// </summary>
        public bool IsIndexed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Document Document { get; set; }
    }
}
