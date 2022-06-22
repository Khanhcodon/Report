using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocCatalog - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Doc_Catalog trong CSDL
    /// </summary>
    public class DocCatalog
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa văn bản, hồ sơ và danh mục
        /// </summary>
        public int DocCatalogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id danh mục
        /// </summary>
        public Guid CatalogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id giá trị danh mục
        /// </summary>
        public Guid CatalogValueId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị danh mục
        /// </summary>
        public string CatalogValue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id 
        /// </summary>
        public Guid FormId { get; set; }
    
        /// <summary>
        /// Lấy hoặc thiết lập Văn bản, hồ sơ
        /// </summary>
        public virtual Document Document { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị danh mục
        /// </summary>
        public virtual CatalogValue CatalogValues { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Form
        /// </summary>
        public virtual Form Form { get; set; }
    }
}
