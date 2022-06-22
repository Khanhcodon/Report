using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Cache người dùng hiện tại
    /// </summary>
    [Serializable]
    public class CurrentUserCached
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập dạng email (@domainname)
        /// </summary>
        public string UsernameEmailDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên domain
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra người dùng này đã được kích hoạt
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Có quyền đọc tất cả công văn
        /// </summary>
        public bool? CanReadEveryDocument { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quyền xem report của cả cơ quan
        /// </summary>
        public bool? HasViewReport { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập các câu hình của user dang json
        /// </summary>
        public string UserSetting { get; set; }

        /// <summary>
        /// Lưu thông tin các thiết bị được notify
        /// </summary>
        public string NotifyInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? HasLimitByMac { get; set; }
        
        /// <summary>
        /// Danh sách người dùng phòng ban
        /// </summary>
        public IEnumerable<UserDepartmentJobTitlesPositionCached> UserDepartmentJobTitless { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PermissionCache> Permissions { get; set; }

        /// <summary>
        /// Convert sang user
        /// </summary>
        /// <returns></returns>
        public User ToUser()
        {
            return AutoMapper.Mapper.Map<CurrentUserCached, User>(this);
        }

        /// <summary>
        /// Avatar
        /// </summary>
        public string Avatar { get; set; }
    }
}
