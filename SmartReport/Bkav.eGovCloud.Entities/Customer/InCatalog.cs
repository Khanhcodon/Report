using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    public class InCatalog
    {
        private List<InCatalogValue> _catalogValue;

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        public string InCatalogName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid InCatalogId { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InCatalogKey { get; set; }

        /// <summary>
        /// Tên các giá trị của danh mục.
        /// </summary>
        public List<string> InCatalogNames { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> InCatalogKeys { get; set; }


        /// <summary>
        /// Lấy hoặc thiết lập Các giá trị của danh mục
        /// </summary>
        public virtual List<InCatalogValue> InCatalogValues
        {
            get { return _catalogValue ?? (_catalogValue = new List<InCatalogValue>()); }
            set { _catalogValue = value; }
        }
        //public List<CatalogValue> ListCatalog { get; set; }
    }
}
