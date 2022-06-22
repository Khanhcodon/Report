using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(BusinessTypeValidator))]
    public class BusinessTypeModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của loại doanh nghiệp
        /// </summary>
        public int BusinessTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên loại doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessType.CreateOrEdit.Fields.BusinessTypeName.Label")]
        public string BusinessTypeName { get; set; }
    }
}