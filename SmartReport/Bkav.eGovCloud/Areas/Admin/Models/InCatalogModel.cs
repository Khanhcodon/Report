using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(InCatalogValidator))]
    public class InCatalogModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public Guid InCatalogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string InCatalogName { get; set; }

        [LocalizationDisplayName("IndicatorCatalog.CreateOrEdit.Fields.ParentId.Label")]
        public string InCatalogKey { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        public IEnumerable<InCatalogModel> InCatalogs { get; set; }

    }
}