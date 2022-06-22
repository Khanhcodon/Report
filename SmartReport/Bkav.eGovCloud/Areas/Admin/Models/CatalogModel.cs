using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(CatalogValidator))]
    public class CatalogModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public Guid CatalogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("Catalog.CreateOrEdit.Fields.CatalogName.Label")]
        public string CatalogName { get; set; }

        [LocalizationDisplayName("Catalog.Index.List.Column.CatalogKey")]
        public string CatalogKey { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Tên các giá trị của các đối tượng catalog.
        /// </summary>
        public List<string> ValueNames { get; set; }

        public List<string> CatalogKeys { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<Guid> CatalogValueIds { get; set; }

        public CatalogModel()
        {
            IsActivated = true;
            ValueNames = new List<string>();
            CatalogValueIds = null;
        }
    }
}