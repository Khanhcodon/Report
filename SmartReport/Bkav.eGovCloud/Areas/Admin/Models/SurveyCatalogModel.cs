using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(SurveyCatalogValidator))]
    public class SurveyCatalogModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public Guid CatalogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("SurveyCatalog.CreateOrEdit.Fields.SurveyCatalogName.Label")]
        public string CatalogName { get; set; }

        [LocalizationDisplayName("SurveyCatalog.CreateOrEdit.Fields.ParentId.Label")]
        public string CatalogKey { get; set; }

        public IEnumerable<SurveyCatalogModel> SurveyCatalogs { get; set; }

    }
}