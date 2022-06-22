
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Signature - public - Entity
    /// Access Modifiers: 
    /// Create Date : 150414
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng Signature trong CSDL: chữ ký của người dùng
    /// </summary>
    public class Signature
    {
        /// <summary>
        /// Lấy hoặc thiết lập mã cấu hình chữ ký người dùng
        /// </summary>
        public int SignatureId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên chữ ký người dùng
        /// </summary>
        public string SignatureName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí đặt chữ ký
        /// </summary>
        public int SignaturePosition { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập từ cần tìm
        /// </summary>
        public string SearchWord { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chữ ký là hình anh hay dạng text
        /// </summary>
        public bool IsTypeImage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ảnh
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép hiển thị thông tin của chứng thư số
        /// </summary>
        public bool IsDispplayCertificate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho :true - tìm từ cuối ; false - tìm từ đầu
        /// </summary>
        public bool IsFindType { get; set; }

        /// <summary>
        /// Thiết lập người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trường định dạng của anh
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Người dùng
        /// </summary>
        public virtual User User { get; set; }
    }
}
