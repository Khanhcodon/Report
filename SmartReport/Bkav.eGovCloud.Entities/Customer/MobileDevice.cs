using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Các thiết bị đăng nhập vào tài khoản
    /// </summary>
    public class MobileDevice
    {
        /// <summary>
        /// Id
        /// </summary>
        public int MobileDeviceId { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Hệ điều hành của thiết bị
        /// </summary>
        public int OS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DeviceOS OSEnum
        {
            get
            {
                return (DeviceOS)this.OS;
            }
        }

        /// <summary>
        /// Là MAC nếu đăng nhập trên desktop, là DeviceId nếu đăng nhập trên thiết bị di động.
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Tên thiết bị
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Phiên bản hệ điều hành
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Địa chỉ deviceid của thiết bị di động
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Ngày truy cập đầu tiên
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Lần truy cập gần nhất
        /// </summary>
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// Trạng thái token đăng nhập trên thiết bị đang hoạt động.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Ghi hoặc thiết lập IP người truy cập
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Thiết lập token đăng nhập hiện tại.
        /// </summary>
        public string LoginToken { get; set; }

        /// <summary>
        /// Ghi hoặc thiết lập trình duyệt người truy cập
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Chặn không cho phép thiết bị này đăng nhập
        /// </summary>
        public bool HasBlock { get; set; }

        /// <summary>
        /// Lịch sử đăng nhập trên thiết bị hiện tại.
        /// </summary>
        public string History { get; set; }
    }
}
