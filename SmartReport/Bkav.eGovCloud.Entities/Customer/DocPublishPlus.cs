using System;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Văn bản phát hành
    /// </summary>
    public class DocPublishPlus
    {
        /// <summary>
        /// Lấy hoặc thiết lập key
        /// </summary>
        public int DocumentPublishPlusId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hồ sơ id
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập documentcopyId
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hồ sơ id
        /// </summary>
        public Guid DoctypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã hồ sơ id
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày phát hành
        /// </summary>
        public DateTime DatePublished { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ nhận
        /// </summary>
        public string AddressName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xác định là văn bản hay hsmc
        /// </summary>
        public bool IsHsmc { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người phát hành
        /// </summary>
        public int UserPublishId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người phát hành
        /// </summary>
        public string UserPublishName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có gửi liên thông hay không
        /// </summary>
        public bool HasLienThong { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái gửi lỗi.
        /// Thông tin gửi lỗi lưu trong trường Note.
        /// </summary>
        /// <remarks>
        /// Khi tool liên thông lấy thông tin văn bản để gửi sẽ tiến hành kiểm tra và 
        /// update lại vào đây để tránh việc lỗi vẫn lấy lại nhiều lần.
        /// Không cần thêm số lần gửi lỗi vì tool check liên tục, thêm số lần không có ý nghiax
        /// </remarks>
        public bool HasSendFail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ gửi liên thông
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày gửi đi liên thông
        /// </summary>
        public DateTime? DateSent { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đang gửi liên thông
        /// </summary>
        public bool IsPending { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có yêu cầu hồi báo hay không
        /// </summary>
        public bool HasRequireResponse { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hạn hồi báo
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã hồi báo hay chưa
        /// </summary>
        public bool IsResponsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hồi báo
        /// </summary>
        public DateTime? DateResponsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã văn bản hồi báo
        /// </summary>
        public string DocCodeResponsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id văn bản hồi báo
        /// </summary>
        public int? DocumentCopyResponsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã định danh của cơ quan nhận
        /// </summary>
        public string AddressCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tiến độ liên thông
        /// </summary>
        public string Traces { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái gửi đến CĐ-ĐH
        /// </summary>
        public bool? IsSentCDDH { get; set; }

		/// <summary>
		/// Trạng thái gửi liên thông (sau thay cho IsPending, IsResponsed,...)
		/// <para>
		/// 1 - Bình thường; 13 - Yêu cầu thu hồi; 15 - Thu hồi thành công; 16 - Từ chối thu hồi
		/// </para>
		/// </summary>
		public int Status { get; set; }

        /// <summary>
        /// Trạng thái Json liên thông với chính phủ
        /// Lưu trữ dạng json
        /// </summary>
        public string JSonStatus { get; set; }

        /// <summary>
        /// DataSend
        /// </summary>
        public string DataSend { get; set; }

    }
}
