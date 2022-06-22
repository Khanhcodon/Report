using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    [FluentValidation.Attributes.Validator(typeof(SearchSettingsValidator))]
    public class SearchSettingsModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập đường dẫn đến server tìm kiếm
        /// </summary>
        [LocalizationDisplayName("Setting.Search.Fields.ServerUrl.Label")]
        public string ServerUrl { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập số lượng lấy ra khi tìm kiếm
        /// </summary>
        [LocalizationDisplayName("Setting.Search.Fields.NumberSelected.Label")]
        public int NumberSelected { get; set; }

        /// <summary>
        /// Danh sách user mặc định dùng máy in
        /// </summary>
        [LocalizationDisplayName("Setting.Search.Fields.UserIds.Label")]
        public string UserIds { get; set; }

        /// <summary>
        /// Danh sách vị trí mặc định dùng máy in
        /// </summary>
        [LocalizationDisplayName("Setting.Search.Fields.DepartmentPositions.Label")]
        public string DepartmentPositions { get; set; }
    }
}