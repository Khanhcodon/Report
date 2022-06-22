using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(CityValidator))]
    public class CityModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của tinh/thành phố
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên tỉnh/thành phố
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.City.CreateOrEdit.Fields.CityName.Label")]
        public string CityName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên tỉnh/thành phố
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.City.CreateOrEdit.Fields.CityCode.Label")]
        public string CityCode { get; set; }
    }
}