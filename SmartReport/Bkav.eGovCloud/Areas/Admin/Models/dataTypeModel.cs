using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;



namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(dataTypeValidator))]
    public class dataTypeModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public Guid dataTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string nameID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string dataTypeDescription { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string dataTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string distribute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.IsActived.Label")]
        public bool IsActivated { get; set; }

        
        public IEnumerable<dataTypeModel> Indicators { get; set; }
    }
}