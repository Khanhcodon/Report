using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Models
{
    public class FAQSettingsModel
    {
        private readonly ProcessFunctionBll _processFunctionService;
        private readonly PermissionSettingBll _permissionSettingService;
        private readonly FAQSetting _faqSettings;

        public FAQSettingsModel()
        {
            _processFunctionService = DependencyResolver.Current.GetService<ProcessFunctionBll>();
            _permissionSettingService = DependencyResolver.Current.GetService<PermissionSettingBll>();
            _faqSettings = DependencyResolver.Current.GetService<FAQSetting>();
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
        /// Check Active
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool HasPermisson(int userId)
        {
#if HoSoMotCuaEdition
            if (this.Active && _faqSettings.Active)
            {
                if (!PermissionSettingId.HasValue)
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