using Bkav.eGovCloud.Entities;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Api.Dto
{
	/// <summary>
	/// Thông tin hồ sơ liên thông
	/// </summary>
	public class DocumentDto
	{
		public DocumentDto()
		{
			this.Relateds = new List<RelatedDocument>();
			this.Attachments = new List<AttachmentDto>();
		}

		/// <summary>
		/// Id hồ sơ
		/// </summary>
		public string DocumentId { get; set; }

		/// <summary>
		/// Trích yếu hoặc tiêu đề hồ sơ
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Thông tin tổ chức gửi hồ sơ
		/// </summary>
		public Organization Sender { get; set; }

		/// <summary>
		/// Danh sách các tổ chức nhận hồ sơ
		/// </summary>
		public List<Organization> Receivers { get; set; }

		/// <summary>
		/// Số hồ sơ. Ví dụ: 12
		/// </summary>
		public string CodeNumber { get; set; }

		/// <summary>
		/// Ký hiệu hồ sơ. Ví dụ: BTTT-THH
		/// </summary>
		public string CodeNotation { get; set; }

		/// <summary>
		/// Nơi ban hành. Ví dụ: Hà Nội
		/// </summary>
		public string Place { get; set; }

		/// <summary>
		/// Ngày ban hành. Định dạng dd/MM/yyyy
		/// </summary>
		public string PromulgationDate { get; set; }

		/// <summary>
		/// Mã loại hồ sơ
		/// </summary>
		public string TypeCode { get; set; }

		/// <summary>
		/// Tên loại hồ sơ
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// Tên người ký, chịu trách nhiệm ban hành hồ sơ
		/// </summary>
		public string SignerName { get; set; }

		/// <summary>
		/// Hạn xử lý hồ sơ. Định dạng dd/MM/yyyy. Mặc định là empty.
		/// </summary>
		public string DueDate { get; set; }

		/// <summary>
		/// Độ ưu tiên. Theo thứ tự Lớn dần từ 0 đến 3. Mặc 
		/// </summary>
		public int Priority { get; set; }

		/// <summary>
		/// Danh sách hồ sơ liên quan
		/// </summary>
		public List<RelatedDocument> Relateds { get; set; }

		/// <summary>
		/// Ngày hẹn trả. Định dạng dd/MM/yyyy.
		/// </summary>
		public string DateAppointed { get; set; }

		/// <summary>
		/// Danh sách lệ phí
		/// </summary>
		public List<FeeDto> Fees { get; set; }

		/// <summary>
		/// Danh sách giấy tờ liên quan
		/// </summary>
		public List<DocumentPaper> DocumentPapers { get; set; }

		/// <summary>
		/// Thông tin công dân
		/// </summary>
		public CitizenDto CitizenInfo { get; set; }

		/// <summary>
		/// Danh sách đính kèm bao gồm cả form động (nếu có)
		/// </summary>
		public List<AttachmentDto> Attachments { get; set; }

		/// <summary>
		/// Nghiệp vụ liên thông
		/// </summary>
		public Bussiness Bussiness { get; set; }

		public List<ResponseFor> ResponseFor { get; set; }
	}

	public class DocumentBWSSDto
	{
		public DocumentBWSSDto()
		{
		}

		/// <summary>
		/// Id hồ sơ
		/// </summary>
		public string DocumentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số kí hiệu (eOffice), mã hồ sơ (eGate)
		/// </summary>
		public string DocCode { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id thể loại
		/// </summary>
		public string DocTypeId { get; set; }

		/// <summary>
		/// Tên Thể loại
		/// </summary>
		public string DocTypeName { get; set; }

		/// <summary>
		/// Trích yếu
		/// </summary>
		public string Compendium { get; set; }

		/// <summary>
		/// cơ quan ban hành
		/// </summary>
		public string Organization { get; set; }

		/// <summary>
		/// Mã cơ quan ban hành
		/// </summary>
		public string OrganizationCode { get; set; }

		/// <summary>
		/// Độ khẩn
		/// </summary>
		public byte UrgentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày văn bản
		/// </summary>
		public DateTime? DatePublished { get; set; }

		/// <summary>
		/// Id thể loại
		/// </summary>
		public string CategoryId { get; set; }

		/// <summary>
		/// Tên thể loại
		/// </summary>
		public string CategoryName { get; set; }
	}
	
	/// <summary>
	/// Trạng thái văn bản liên thông đến
	/// </summary>
	public class DocumentStatusDto
	{
		/// <summary>
		/// Số ký hiệu văn bản
		/// </summary>
		public string DocCode { get; set; }

		/// <summary>
		/// Mã định danh Đơn vị gửi
		/// </summary>
		public string OrganId { get; set; }

		/// <summary>
		/// Trạng thái văn bản hiện tại
		/// <para>
		/// 1: Văn bản đã đến.
		/// 2: Văn bản đã từ chối
		/// 3: Văn bản đã tiếp nhận
		/// 4: Văn bản đã phân công xử lý
		/// 5: Văn bản đang xử lý
		/// 6: Văn bản đã hoàn thành
		/// 13: Yêu cầu lấy lại văn bản
		/// 15: Lấy lại thành công
		/// 16: Từ chối laays lại
		/// </para>
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// Người cập nhật trạng thái
		/// </summary>
		public string UserUpdate { get; set; }

		/// <summary>
		/// Thời gian thay đổi trạng thái
		/// </summary>
		public DateTime TimeStamp { get; set; }

		/// <summary>
		/// Thông báo
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Mã định danh văn bản
		/// </summary>
		public string DocumentId { get; set; }
	}

	/// <summary>
	/// Nghiệp vụ liên thông văn bản
	/// </summary>
	public class Bussiness
	{
		/// <summary>
		/// Nghiệp vụ liên thông
		/// <para>
		/// 0 - Văn bản mới
		/// 1 - Thu hồi 
		/// 2 - Cập nhật 
		/// 3 - Thay thế
		/// </para>
		/// </summary>
		public int BussinessDocType { get; set; }

		/// <summary>
		/// Lý do cần điều chỉnh
		/// </summary>
		public string BussinessDocReason { get; set; }
	}

	public class ResponseFor
	{
		public string OrganId { get; set; }

		public string Code { get; set; }

		public string PromulgationDate { get; set; }

		public string DocumentId { get; set; }
	}
}
