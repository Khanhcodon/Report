using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bảng lưu trữ thông tin của hồ sơ đăng ký qua mạng
    /// </summary>
    public class DocumentOnline
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Mã hồ sơ
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Id loại hồ sơ tương ứng
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual DocType Doctype { get; set; }

        /// <summary>
        /// Tên thủ tục
        /// </summary>
        public string DoctypeName { get; set; }

        /// <summary>
        /// Id hồ sơ khi trực tiếp nhận sang egov
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Ngày nhận đăng ký qua mạng
        /// </summary>
        public DateTime DateReceived { get; set; }

        /// <summary>
        /// Ngày hẹn trả
        /// </summary>
        public DateTime DateAppoint { get; set; }

        /// <summary>
        /// Trạng thái xử lý hồ sơ
        /// 1. Chờ duyệt - Default
        /// 2. Đang xử lý
        /// 3. Chờ bổ sung
        /// 4. Chờ thanh toán lệ phí
        /// 5. Chờ trả kết quả
        /// 6. Đã trả kết quả
        /// 7. Bị từ chối
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Thông tin công dân,doanh nghiệp
        /// </summary>
        public string PersonInfo { get; set; }

        /// <summary>
        /// Số CMT
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Chuỗi Json chứa thông tin tờ khai
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// File
        /// </summary>
        public IEnumerable<File> Files { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DocPaper> DocPapers { get; set; }

        /// <summary>
        /// Comment của người xử lý
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Nơi nhận trả kết quả
        /// </summary>
        public int? TypeReturned { get; set; }

        /// <summary>
        /// Trạng thái đã xem hay chưa
        /// 0. Chưa xem
        /// 1. Đã xem
        /// </summary>
        public bool IsViewed { get; set; }

        /// <summary>
        /// TOken
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Supplementary> Supplementaries { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DocumentPayment> DocumentPayments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsSupplemented { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsPayment { get; set; }
    }
}