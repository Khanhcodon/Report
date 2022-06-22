using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CustomerCookieData - public - Entity
    /// Access Modifiers:
    /// Create Date : 050812
    /// Author      : TrungVH
    /// Description : Thông tin về người dùng trong cookie
    /// </summary>
    public class CustomerCookieData
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập người dùng
        /// </summary>
        public string UsernameWithDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nhóm người dùng
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Email người dùng
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên đầy đủ người dùng
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Trả về chuỗi dữ liệu json để lưu vào cookie
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Stringify();
        }

        /// <summary>
        /// Trả về chuỗi dữ liệu json để lưu vào cookie
        /// </summary>
        /// <returns></returns>
        public string ToCookieString()
        {
            return ToString();
        }
    }
}
