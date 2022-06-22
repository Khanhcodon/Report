using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Api.Dto
{
    public class DocumentCopyDto
    {
        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Doccode
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid? DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên thể loại văn bản, hồ sơ
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Loại văn bản
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Độ khẩn
        /// </summary>
        public byte UrgentId { get; set; }

        /// <summary>
        /// Ngay tao
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày hẹn trả (eGate), ngày giải quyết (eOffice)
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        /// <summary>
        /// Nơi đến đi.
        /// </summary>
        public string InOutPlace { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số trang
        /// </summary>
        public int? TotalPage { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string UserCurrentFullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày nhận
        /// </summary>
        public DateTime DateReceived { get; set; }

        /// <summary>
        /// <para>Trạng thái của văn bản copy</para>
        /// <para>CuongNT@bkav.com - 090413</para>
        /// </summary>
        /// <remarks>
        /// Kiểm soát chuyển trạng thái của DocumentCopy theo nguyên tắc hợp lệ.
        /// </remarks>
        public int DocCopyStatus { get; set; }

        /// <summary>
        /// Người kí
        /// </summary>
        public string UserNameSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày kí duyệt
        /// </summary>
        public DateTime? DateSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết tên người trả kết quả (eGate)
        /// </summary>
        public string UserNameReturned { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày kết thúc xử lý (eOffice), ngày trả kết quả (eGate)
        /// </summary>
        public DateTime? DateReturned { get; set; }

        //Yeu cau bo sung
        public IEnumerable<Supplementary> SupplementaryModels { get; set; }

        /// <summary>
        /// Ky duyet
        /// </summary>
        public IEnumerable<Approver> Approvers { get; set; }

        /// <summary>
        /// Tien do lien thong
        /// </summary>
        public IEnumerable<LienThongTracesModel> LienThongs { get; set; }

        /// <summary>
        /// Luu tru tat ca cacComments
        /// </summary>
        public IEnumerable<CommentModel> CommentList { get; set; }

        /// <summary>
        /// In out code
        /// </summary>
        public string InOutCode { get; set; }
    }
}