using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class Ad_UnitModel
    {
        /// <summary>
        /// 
        /// </summary>
        //[FluentValidation.Attributes.Validator(typeof(UnitValidator))]
        
        public Guid IdUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string Unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string Exchange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string OriginalUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.Use.Label")]
        public bool? Use { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public IEnumerable<Ad_UnitModel> Ad_UnitModels { get; set; }
    }
}