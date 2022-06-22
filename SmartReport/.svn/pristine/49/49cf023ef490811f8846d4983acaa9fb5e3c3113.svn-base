using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Dữ liệu trả về từ OnePay
    /// </summary>
    public class DomesticPaymentResponse
    {
        /// <summary>
        /// Mã xác định giao dịch có thành công hay không.
        /// 0 - Thành công; còn lại - khác 0
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// Lệ phí thanh toán
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Ngôn ngữ hiển thị khi thanh toán; vn-tiếng việt; en-tiếng anh
        /// </summary>
        public string Localed { get; set; }

        /// <summary>
        /// Lệnh giao dịch, Mặc định là “queryDR”
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Phiên bản giao dịch, Mặc định là 1
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Loại giao dịch: nội địa, quốc tế
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Thông tin thanh toán
        /// </summary>
        public string OrderInfo { get; set; }

        /// <summary>
        /// Id mã khách hàng của OnePay
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthorizeId { get; set; }

        /// <summary>
        /// Mã giao dịch, 
        /// </summary>
        public string MerchTxnRef { get; set; }

        /// <summary>
        /// Một số duy nhất được sinh ra từ cổng thanh toán trên giao dịch. Nó được lưu trên cổng thanh toán như ánh xạ cho phép
        /// người sử dụng thực hiện các chức năng như refund hay capture.
        /// </summary>
        public string TransactionNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AcqResponseCode { get; set; }

        /// <summary>
        /// Mã giao dịch được sinh ra bởi cổng thanh toán để chỉ trạng thái giao dịch.
        /// Giá trị là “0” (zero) cho biết giao dịch đã xử lý thành công. Tất cả các giá trị khác
        /// là giao dịch thất bại
        /// </summary>
        public int TxnResponseCode { get; set; }

        /// <summary>
        /// Thông tin về lỗi giao dịch (nếu có)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Chuỗi mã hóa danh sách các tham số trả về từ OnePay, giúp kiểm chứng thông tin có chính xác hay không.
        /// </summary>
        public string vpc_SecureHash { get; set; }
        
        /// <summary>
        /// Thời điểm giao dịch
        /// </summary>
        public DateTime DateTransaction { get; set; }
    }
}
