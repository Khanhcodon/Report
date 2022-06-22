using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business;
using System.Collections.Generic;
using Newtonsoft.Json;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// Dùng cho đăng ký qua mạng
    /// </summary>
    public class OnlineRegistrationSettingsModel
    {
        private readonly ProcessFunctionBll _processFunctionService;
        private readonly PermissionSettingBll _permissionSettingService;
        private readonly OnlineRegistrationSettings _onlineRegistrationSettings;
        private readonly WorkflowBll _workflowService;
        private readonly WorkflowHelper _workflowHelper;
        private readonly DocTypeBll _doctypeService;

        public OnlineRegistrationSettingsModel()
        {
            _processFunctionService = DependencyResolver.Current.GetService<ProcessFunctionBll>();
            _permissionSettingService = DependencyResolver.Current.GetService<PermissionSettingBll>();
            _onlineRegistrationSettings = DependencyResolver.Current.GetService<OnlineRegistrationSettings>();
            _workflowService = DependencyResolver.Current.GetService<WorkflowBll>();
            _workflowHelper = DependencyResolver.Current.GetService<WorkflowHelper>();
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
        }

        /// <summary>
        /// Trạng thái kích hoạt
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Lây hoặc thiết tên hiển thị
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lây hoặc thiết lập api lấy dữ liệu
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm
        /// </summary>
        public int TreeGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm
        /// </summary>
        public int? PermissionSettingId { get; set; }

        /// <summary>
        /// Link trang chủ dịch vụ công
        /// </summary>
        public string OnlineLink { get; set; }

        /// <summary>
        /// Lấy ra các thủ tục mà người dùng hiện tai có quyền khởi tạo
        /// </summary>
        public string PrivilegedDoctypes(int userId) 
        {
            var doctypePermissions = _doctypeService.GetsByUserId(userId, CategoryBusinessTypes.Hsmc);
            if (doctypePermissions != null || doctypePermissions.Any())
            {
                return JsonConvert.SerializeObject(doctypePermissions.Select(dt=>dt.DocTypeId));
            }

            return "";
        }

        /// <summary>
        /// Check Active
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool HasPermisson(int userId)
        {
#if HoSoMotCuaEdition
            if (this.Active && _onlineRegistrationSettings.Active)
            {
                if (!PermissionSettingId.HasValue)
                {
                    return true;
                }
                var doctypePermissions = _doctypeService.GetsByUserId(userId, CategoryBusinessTypes.Hsmc);
                if (doctypePermissions != null || doctypePermissions.Any())
                {
                    return true;
                }

                var permissions = _permissionSettingService.GetCacheAllPermissionSettings()
                                .FirstOrDefault(p => p.PermissionSettingId == PermissionSettingId.Value);

                if (permissions != null)
                {
                    return _processFunctionService.HasPermission(
                              permissions.ListUserHasPermission,
                              permissions.ListPositionHasPermission,
                              permissions.ListDepartmentPositionHasPermission,
                              userId);
                }
            }
#endif
            return false;
        }
    }
}