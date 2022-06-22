using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class UserCached
    {
        private const string AVATAR_ROOT = "../AvatarProfile/{0}.jpg";
        private const string NO_AVATAR_PATH = "../AvatarProfile/noavatar.jpg";

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
        /// Lấy hoặc thiết lập Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên đệm
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra người dùng này đã được kích hoạt
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Avatar
        {
            get
            {
                var avartar = NO_AVATAR_PATH;
                try
                {
                    avartar = string.Format(AVATAR_ROOT, Username + "_" + DomainName);
                    var fullPath = CommonHelper.MapPath(avartar);
                    if (!System.IO.File.Exists(fullPath))
                    {
                        avartar = NO_AVATAR_PATH;
                    }
                    else
                    {
                        avartar += "?date=" + VersionDateTime.ToString("ddmmyyyyhhmmss");
                    }
                }
                catch {  }

                return avartar;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra người dùng này đang bị khóa tạm thời do đăng nhập nhiều lần không thành công
        /// </summary>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách id nhóm người dùng
        /// </summary>
        public List<int> RoleIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách id phòng ban và chức vụ (được phân cách nhau = dấu ,)
        /// </summary>
        public List<string> DepartmentJobTitlesId { get; set; }

        /// <summary>
        /// Có quyền đọc tất cả công văn
        /// </summary>
        public bool? CanReadEveryDocument { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập các câu hình của user dang json
        /// </summary>
        public string UserSetting { get; set; }

        /// <summary>
        /// Lưu thông tin các thiết bị được notify
        /// </summary>
        public string NotifyInfo { get; set; }

        /// <summary>
        /// Đối tượng parse từ notify info
        /// </summary>
        public NotifyInfo NotifyInfoModel
        {
            get
            {
                var result = new NotifyInfo();

                if (string.IsNullOrEmpty(NotifyInfo))
                {
                    return result;
                }

                try
                {
                    result = Json2.ParseAs<NotifyInfo>(NotifyInfo);
                }
                catch { }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? HasLimitByMac { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

		/// <summary>
		/// Trạng thái tin nhắn sử dụng cho chat
		/// </summary>
		public string Status { get; set; }
    }
}
