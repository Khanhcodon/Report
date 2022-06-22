using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(PermissionSettingModelValidator))]
    public class PermissionSettingModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id function
        /// </summary>
        public int PermissionSettingId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên function
        /// </summary>
        [LocalizationDisplayName("PermissionSetting.CreateOrEdit.Fields.PermissionSettingName.Label")]
        public string PermissionSettingName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ thuộc phòng ban có quyền nhìn thấy function (json)
        /// </summary>
        public string DepartmentPositionHasPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ có quyền nhìn thấy function (json)
        /// </summary>
        public string PositionHasPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các người dùng có quyền nhìn thấy function (json)
        /// </summary>
        public string UserHasPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các người dùng có quyền nhìn thấy function
        /// </summary>

        public List<string> DepartmentPositionIds { get; set; }

        public List<int> PositionIds { get; set; }

        public List<int> UserIds { get; set; }
    }
}