using System;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Models
{
	public class DocumentCopyModel
	{
		/// <summary>
		/// 
		/// </summary>
		public int DocumentCopyId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id bản sao văn bản cấp cha(khi có đồng xử lý)
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày tạo
		/// </summary>
		public DateTime? DateCreated { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày nhận
		/// </summary>
		public DateTime? DateReceived { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
		/// </summary>
		public Guid? DocTypeId { get; set; }
		
		/// <summary>
		/// Lấy hoặc thiết lập Id luồng văn bản, hồ sơ
		/// </summary>
		public int? WorkflowId { get; set; }

		/// <summary>
		/// Người gửi
		/// </summary>
		public int? UserSendId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id người đang giữ văn bản
		/// </summary>
		public int? UserCurrentId { get; set; }

		/// <summary>
		/// Tên người đang giữ
		/// </summary>
		public string UserCurrentName { get; set; }

		/// <summary>
		/// Phòng ban hiện tại
		/// </summary>
		public string CurrentDepartmentName { get; set; }

		/// <summary>
		/// <para>Lấy hoặc thiết lập Vết đường đi. Nhận giá trị có khuôn dạng List&lt;HistoryPath&gt; Histories.StringifyJs(false);</para>
		/// <para>Set: Sử dụng SetHistories(HistoryProcess history) để gán giá trị mới cho History.</para>
		/// </summary>
		public string History { get; set; }

		/// <summary>
		/// <para> Lấy hoặc thiết lập loại vệt</para>
		/// <para> Notes:</para>
		/// <para>   - 1: Hướng xử lý chính.</para>
		/// <para>   - 2: Hướng xử lý đồng xử lý.</para>
		/// <para>   - 4: Hướng xử lý xin ý kiến.</para>
		/// </summary>
		public int? DocumentCopyType { get; set; }
		
		/// <summary>
		/// <para>Trạng thái của văn bản copy</para>
		/// <para>CuongNT@bkav.com - 090413</para>
		/// </summary>
		/// <remarks>
		/// Kiểm soát chuyển trạng thái của DocumentCopy theo nguyên tắc hợp lệ.
		/// </remarks>
		public int? Status { get; set; }
		
		/// <summary>
		/// Lấy hoặc thiết lập Node hiện tại trong quy trình của văn bản
		/// </summary>
		public int? NodeCurrentId { get; set; }
		
		/// <summary>
		/// <para>Lấy hoặc thiết lập Quyền của Node hiện tại trong quy trình của văn bản.</para>
		/// <para>Dữ liệu thuộc kiểu: NodePermissions.</para>
		/// <para>(CuongNT - 240113)</para>
		/// </summary>
		public int? NodeCurrentPermission { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tên node hiện tại
		/// </summary>
		public string NodeCurrentName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày kết thúc hướng xử lý.
		/// </summary>
		public DateTime? DateFinished { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateOverdue { get; set; }
		
		/// <summary>
		/// DateModified
		/// </summary>
		public DateTime? DateModified { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập đường dẫn mở rộng của document copy Id
		/// </summary>
		public string DocumentCopyParentPath { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái thể hiện hồ sơ vừa mới được tiếp nhận hay không
		/// Dùng cho HSMC
		/// </summary>
		public bool? HasJustCreated { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách người nhận thông báo dạng: ;UserId1;UserId2;...
		/// </summary>
		public string UserThongBao { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách người tham gia xử lý văn bản dạng: ;UserId1;UserId2;...
		/// </summary>
		public string UserNguoiThamGia { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách người đã xem văn bản dạng: ;UserId1;UserId2;...
		/// </summary>
		public string UserNguoiDaXem { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách người xử lý ủy quyền  dạng: ;UserId1;UserId2;...
		/// </summary>
		public string UserGiamSat { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tất cả những người liên quan đến văn bản dạng: ;UserId1;UserId2;...
		/// </summary>
		public string DocumentUsers { get; set; }

		/// <summary>
		/// Thông tin xử lý: thông tin phát hành với văn bản đi và thông tin chuyển xử lý khi tiếp nhận văn bản đên.
		/// </summary>
		public string ProcessInfo { get; set; }
	}
}