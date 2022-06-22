using System;
using System.ComponentModel.DataAnnotations;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SurveyCatalogValue - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Modify:     : TienBV
    /// Description : Entity tương ứng với bảng SurveyCatalogValue trong CSDL
    /// </summary>
    public class SurveyCatalogValue
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id giá trị danh mục
        /// </summary>
        [Key]
        public Guid CatalogValueId { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public int CatalogId { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public string CatalogGuidId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CatalogId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CatalogKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị danh mục
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh mục cha
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Cấp lồng (nested) so với root
        /// </summary>
        public int Level { get; set; }
    }
}
