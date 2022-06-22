using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(WardValidator))]
    public class WardModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của xã/phường
        /// </summary>
        public int WardId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã của quận/huyện
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Ward.CreateOrEdit.Fields.District.Label")]
        public string DistrictCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên xã/phường
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Ward.CreateOrEdit.Fields.WardName.Label")]
        public string WardName { get; set; }
    }
}