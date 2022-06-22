using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using System.Linq;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SearchSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 010413
    /// Author      : TrungVH
    /// Description : Entity cho phần cấu hình tìm kiếm văn bản, hồ sơ
    /// </summary>
    public class SearchSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết lập đường dẫn đến server tìm kiếm
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số lượng lấy ra khi tìm kiếm
        /// </summary>
        public int NumberSelected { get; set; }

        /// <summary>
        /// Danh sách user mặc định có quyền tìm kiếm tất cả HSMC
        /// </summary>
        public string UserIds { get; set; }

        /// <summary>
        /// Danh sách vị trí mặc định có quyền tìm kiếm tất cả HSMC
        /// </summary>
        public string DepartmentPositions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool HasPermission(User user)
        {
            var hasPermistionSearch = false;
            if (UserIds != null)
            {
                var userHasPermissionSearch = Json2.ParseAs<List<int>>(UserIds);
                if (userHasPermissionSearch.Exists(i => i == user.UserId))
                {
                    hasPermistionSearch = true;
                }
            }
            else if (DepartmentPositions != null)
            {
                var deptPosHasPermissionSearch = Json2.ParseAs<List<Dictionary<string, int>>>(DepartmentPositions);
                if (user.UserDepartmentJobTitless != null && user.UserDepartmentJobTitless.Any())
                {
                    hasPermistionSearch = user.UserDepartmentJobTitless.FirstOrDefault(u => deptPosHasPermissionSearch.Exists(d => u.DepartmentIdExt.IndexOf(d["DepartmentId"].ToString()) >= 0 && (d["PositionId"] == u.PositionId) || d["PositionId"] == 0)) != null;
                }
            }
            return hasPermistionSearch;
        }
    }
}