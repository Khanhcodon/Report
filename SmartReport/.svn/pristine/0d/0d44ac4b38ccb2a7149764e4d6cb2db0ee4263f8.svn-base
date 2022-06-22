using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocExtendField - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Doc_ExtendField trong CSDL
    /// </summary>
    public class DocExtendField
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa văn bản, hồ sơ và trường mở rộng
        /// </summary>
        public int DocExtendFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id trường mở rộng
        /// </summary>
        public Guid ExtendFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị trường mở rộng
        /// </summary>
        public string ExtendFieldValue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên trường mở rộng
        /// </summary>
        public string ExtendFieldName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id form
        /// </summary>
        public Guid FormId { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập Văn bản, hồ sơ
        /// </summary>
        public virtual Document Document { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trường mở rộng
        /// </summary>
        public virtual ExtendField ExtendField { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Form
        /// </summary>
        public virtual Form Form { get; set; }
    }
}
