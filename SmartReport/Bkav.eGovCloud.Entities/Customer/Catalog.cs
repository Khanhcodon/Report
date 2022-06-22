using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Catalog - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Catalog trong CSDL
    /// </summary>
    public class Catalog
    {
        private List<CatalogValue> _catalogValue;

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CatalogId { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public string DocfieldIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CatalogKey { get; set; }

        /// <summary>
        /// Tên các giá trị của danh mục.
        /// </summary>
        public List<string> ValueNames { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> CatalogKeys { get; set; }


        /// <summary>
        /// Lấy hoặc thiết lập Các giá trị của danh mục
        /// </summary>
        public virtual List<CatalogValue> CatalogValues
        {
            get { return _catalogValue ?? (_catalogValue = new List<CatalogValue>()); }
            set { _catalogValue = value; }
        }
    }
}
