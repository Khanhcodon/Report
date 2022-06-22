using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Category - public - Entity
    /// Access Modifiers: 
    /// Create Date : 25052020
    /// Author      : TuDv
    /// Description : Entity tương ứng với bảng IndicatorCatalog trong CSDL. Bảng IndicatorCatalog là bảng lưu trữ chỉ tiêu.
    /// </summary>
    public class IndicatorCatalog
    {

        /// <summary>
        /// Lấy hoặc thiết lập Id 
        /// </summary>
        public int IndicatorCatalogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên 
        /// </summary>
        public string IndicatorCatalogName { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Tên 
        /// </summary>
        public string IndicatorCatalogCode { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập Id phân loại danh mục
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh mục cha
        /// </summary>
        public int? ParentId { get; set; }

    }
}
