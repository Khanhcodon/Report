using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(CategoryDisaggreationValidator))]
    public class CategoryDisaggreationModel : PacketModel
    {
        public CategoryDisaggreationModel() : base() { }

        /// <summary>
        /// id cua phan ra danh muc
        /// </summary>
        /// 
        public Guid CategoryDisaggregationId { get; set; }

        /// <summary>
        /// id cua indicator
        /// </summary>
        public Guid IndicatorId { get; set; }

        /// <summary>
        /// ten danh muc phan ra
        /// </summary>
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string CategoryDisaggregationName { get; set; }

        /// <summary>
        /// mã danh mục
        /// </summary>
        
        public string CategoryDisaggregationCode { get; set; }

        /// <summary>
        /// kích hoạt
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.IsActived.Label")]
        public bool IsActivated { get; set; }


        /// <summary>
        /// 
        /// </summary>
        
        public int OrderType { get; set; }
        
        
        public IEnumerable<CategoryDisaggreationModel> CategoryDisagreationModels { get; set; }

    }
}