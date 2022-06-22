using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(IndicatorCatalogValidator))]
    public class IndicatorCatalogModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public int IndicatorCatalogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string IndicatorCatalogName { get; set; }

        [LocalizationDisplayName("IndicatorCatalog.CreateOrEdit.Fields.ParentId.Label")]
        public int? ParentId { get; set; }

        [LocalizationDisplayName("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogCode.Label")]
        public string IndicatorCatalogCode { get; set; }

        public int CategoryId { get; set; }
    }

    public class InCatalogParent
    {
        public int Key { get; set; }
        public string Title { get; set; }
    }
    public class Select2Model
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public int level { get; set; }
    }
    public class TreeSurveyModel
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public int level { get; set; }
        public List<TreeSurveyModel> surveyValue { get; set; }
    }
}