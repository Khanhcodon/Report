using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(DistrictValidator))]
    public class DistrictModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của quận/huyện
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của tinh/thành phố
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.District.CreateOrEdit.Fields.City.Label")]
        public string CityCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên quận/huyện
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.District.CreateOrEdit.Fields.DistrictName.Label")]
        public string DistrictName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã quận/huyện
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.District.CreateOrEdit.Fields.DistrictCode.Label")]
        public string DistrictCode { get; set; }
    }
}