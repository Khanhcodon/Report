
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Code - public - Entity
    /// Access Modifiers: 
    /// Create Date : 160715
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng ShareFolder trong CSDL
    /// </summary>
    public class ShareFolder
    {
        /// <summary>
        /// 
        /// </summary>
        public int ShareFolderId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục chia sẻ
        /// </summary>
        public string Directory  { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tài khoàn đăng nhập vào thư mục chia sẻ
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu đăng nhập vào thư mục chia sẻ
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục được chia sẻ qua mạng
        /// </summary>
        public bool IsNetwork { get; set; }
    }
}
