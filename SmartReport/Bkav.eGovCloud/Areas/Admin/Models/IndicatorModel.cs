using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// 
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(IndicatorValidator))]
    public class IndicatorModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public Guid IndicatorId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string IndicatorName { get; set; }

        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.IsActived.Label")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string IndicatorDesctiption { get; set; }

        public IEnumerable<IndicatorModel> Indicators { get; set; }
    }
}