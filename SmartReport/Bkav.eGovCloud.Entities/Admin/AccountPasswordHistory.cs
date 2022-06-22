using System;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : User - public - Entity
    /// Access Modifiers: 
    /// Create Date : 050912
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng PasswordHistory trong CSDL
    /// </summary>
    public class AccountPasswordHistory
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của lịch sử thay đổi mật khẩu
        /// </summary>
        public int AccountPasswordHistoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập của người dùng
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Muối của mật khẩu
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu đã được băm với muối
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo lịch sử thay đổi mật khẩu
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Account Account { get; set; }
    }
}
