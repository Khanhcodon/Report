using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Utils;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Entities.Customer
{
	/// <summary>
	/// Bkav Corp. - BSO - eGov - eOffice team
	/// Project: eGov Cloud v1.0
	/// Class : DocumentCopy - public - Entity
	/// Access Modifiers: 
	/// Create Date : 241212
	/// Author      : GiangPN
	/// Description : Entity tương ứng với bảng DocumentCopy trong CSDL
	/// </summary>
	public class DocumentCopy
	{
		private const string SEPARATOR_CHAR = ";";

		#region Static

		/// <summary>
		/// Trả về chuỗi string dùng để select xem user có dc xem văn bản không.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static string UserCompareString(int userId)
		{
			return string.Format("{0}{1}{0}", SEPARATOR_CHAR, userId);
		}

		/// <summary>
		/// Trả về chuỗi string dùng để select xem user có dc xem văn bản không.
		/// </summary>
		/// <param name="userIds"></param>
		/// <returns></returns>
		public static string UserCompareString(List<int> userIds)
		{
			if (userIds == null)
			{
				return "";
			}

			return string.Format("{0}{1}{0}", SEPARATOR_CHAR, string.Join(";", userIds));
		}

		#endregion

		#region Instance Properties

		#region Columns

		/// <summary>
		/// Lấy hoặc thiết lập Id bản sao
		/// </summary>
		public int DocumentCopyId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id bản sao văn bản cấp cha(khi có đồng xử lý)
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// Cấu hình key
		/// </summary>
		public string ReportKey { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày tạo
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày nhận
		/// </summary>
		public DateTime DateReceived { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
		/// </summary>
		public Guid DocTypeId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id văn bản, hồ sơ
		/// </summary>
		public Guid DocumentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id luồng văn bản, hồ sơ
		/// </summary>
		public int WorkflowId { get; set; }

		/// <summary>
		/// Người gửi
		/// </summary>
		public int? UserSendId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id người đang giữ văn bản
		/// </summary>
		public int UserCurrentId { get; set; }

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
		public int DocumentCopyType { get; set; }

		/// <summary>
		/// <para>Lấy hoặc thiết lập loại vệt</para>
		/// </summary>
		[JsonIgnore]
		public DocumentCopyTypes DocumentCopyTypeInEnum
		{
			get { return (DocumentCopyTypes)DocumentCopyType; }
		}

		/// <summary>
		/// <para>Trạng thái của văn bản copy</para>
		/// <para>CuongNT@bkav.com - 090413</para>
		/// </summary>
		/// <remarks>
		/// Kiểm soát chuyển trạng thái của DocumentCopy theo nguyên tắc hợp lệ.
		/// </remarks>
		public int Status { get; set; }

		/// <summary>
		/// <para>Trạng thái của văn bản copy</para>
		/// <para>(CuongNT@bkav.com - 050413)</para>
		/// </summary>
		[JsonIgnore]
		public DocumentStatus StatusInEnum
		{
			get
			{
				//var currentStatus = (DocumentStatus)Status;
				//if (!DocumentStatusMachineHelper.ValidateCurrentDocumentStatus(DocumentCopyTypeInEnum, currentStatus))
				//{
				//    throw new Exception(string.Format("Trạng thái {0} không hợp lệ", currentStatus.ToString()));
				//}
				return (DocumentStatus)Status;
			}
		}

		/// <summary>
		/// Lấy hoặc thiết lập Node hiện tại trong quy trình của văn bản
		/// </summary>
		public int? NodeCurrentId { get; set; }

		/// <summary>
		/// Văn bản/hồ sơ
		/// </summary>
		public virtual Document Document { get; set; }

		///// <summary>
		///// Văn bản/hồ sơ
		///// </summary>
		//public User UserCurrent { get; set; }

		/// <summary>
		/// <para>Lấy hoặc thiết lập Quyền của Node hiện tại trong quy trình của văn bản.</para>
		/// <para>Dữ liệu thuộc kiểu: NodePermissions.</para>
		/// <para>(CuongNT - 240113)</para>
		/// </summary>
		public int? NodeCurrentPermission { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày kết thúc hướng xử lý.
		/// </summary>
		public DateTime? DateFinished { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tên node hiện tại
		/// </summary>
		public string NodeCurrentName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateOverdue { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày cho ý kiến cuối cùng
		/// </summary>
		public DateTime? LastDateComment { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ý kiến cuối cùng
		/// </summary>
		public string LastComment { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập UserId cho ý kiến cuối cùng
		/// </summary>
		public int? LastUserIdComment { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Tên người cho ý kiến cuối cùng
		/// </summary>
		public string LastUserComment { get; set; }

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

		/// <summary>
		/// Thông tin xử lý: thông tin phát hành với văn bản đi và thông tin chuyển xử lý khi tiếp nhận văn bản đên.
		/// </summary>
		public string ProcessInfoPlus { get; set; }

		/// <summary>
		/// Ghi chú thông tin khác
		/// </summary>
		public string Note { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập mã cơ quan ban hành
		/// </summary>
		public string FormId { get; set; }

		/// <summary>
		/// ky BC
		/// </summary>
		public string TimeKey { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id cơ quan ban hành
		/// </summary>
		public string OrganizationCode { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id kỳ báo cáo
		/// </summary>
		public int? ActionLevelId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Thời gian bắt đầu kỳ báo cáo
		/// </summary>
		public DateTime? ActionLevelStartDate { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Thời gian kết thúc kỳ báo cáo
		/// </summary>
		public DateTime? ActionLevelEndDate { get; set; }

		/// <summary>
		/// 20200317 Cấu hình/ data handsontable các version (Tạo/ chuyển báo cao)
		/// </summary>
		[NotMapped]
        public string Diff { get; set; }

		#endregion

		#endregion

		private HistoryProcess _histories;

		/// <summary>
		/// Lấy hoặc thiết lập Vết đường đi
		/// </summary>
		public HistoryProcess Histories
		{
			get
			{
				if (string.IsNullOrEmpty(History))
				{
					return new HistoryProcess()
					{
						HistoryPath = new List<HistoryPath>()
					};
				}

				if (_histories == null)
				{
					_histories = Json2.ParseAs<HistoryProcess>(History);
					_histories.HistoryPath = _histories.HistoryPath.OrderBy(c => c.DateCreated).ToList();
					_histories.HistoryThongbao = _histories.HistoryThongbao.OrderBy(c => c.DateCreated).ToList();
					_histories.HistoryXinykien = _histories.HistoryXinykien.OrderBy(c => c.DateCreated).ToList();
				}
				return _histories;
			}
			set
			{
				var result = value;
				result.HistoryPath = result.HistoryPath.OrderBy(c => c.DateCreated).ToList();
				result.HistoryThongbao = result.HistoryThongbao.OrderBy(c => c.DateCreated).ToList();
				result.HistoryXinykien = result.HistoryXinykien.OrderBy(c => c.DateCreated).ToList();
				History = result.Stringify();
				_histories = result;
			}
		}

		/// <summary>
		/// <para>Trả về hướng xử lý của văn bản hiện tại thuộc vào.</para>
		/// <para>cuongnt@bkav.com - 280813</para>
		/// </summary>
		/// <returns>True nếu bản sao đang được xét là thuộc hướng xử lý chính. False là thuộc hướng đồng xử lý.</returns>
		/// <remarks>
		/// Văn bản chính: luôn là thuộc hướng xử lý chính.
		/// Văn bản sao: được coi là thuộc hướng đồng xử lý khi lần đầu tiên được copy từ bản chính và gửi cho ai đó. Các lần bàn giao sau, văn bản này tự nhiên sẽ trở thành thuộc hướng xử lý chính.
		/// </remarks>
		public bool IsHuongXuLyChinh()
		{
			return DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh ||
				(Histories.HistoryPath.Count > 1 && DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy);
		}

		/// <summary>
		/// Trả về path documentcopyId
		/// </summary>
		/// <returns></returns>
		public IEnumerable<int> DocumentCopyParentIds()
		{
			var result = new List<int>();
			if (!string.IsNullOrEmpty(DocumentCopyParentPath))
			{
				var parents = DocumentCopyParentPath.Split(new char[] { '\\' });
				if (parents.Any())
				{
					result.AddRange(parents.Where(i => i != "").Select(i => int.Parse(i)));
				}
			}
			return result;
		}

		/// <summary>
		/// Trả về kết quả kiểm tra người dùng có được quyền xử lý văn bản hay không.
		/// </summary>
		/// <param name="userId">Người dùng</param>
		/// <returns></returns>
		public bool IsCurrentUser(int userId)
		{
			return UserCurrentId == userId;
		}

		/// <summary>
		/// Trả về trạng thái xác định người hiện tại có tham gia xử lý không
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool HasThamGiaXuLy(int userId)
		{
			return UserThamGias().Any(u => u == userId);
		}

		/// <summary>
		/// Trả về kết quả kiểm tra người dùng có quyền xem văn bản hay không.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool HasQuyenXem(int userId)
		{
			return DocumentUserList().Any(u => u == userId);
		}

		/// <summary>
		/// Trả về trạng thái người dùng hiện tại đã xem văn bản hay chưa?
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool IsViewed(int userId)
		{
			return UserDaXems().Any(u => u == userId);
		}

		/// <summary>
		/// Trả về danh sách id người tham gia
		/// </summary>
		/// <returns></returns>
		public List<int> UserThamGias()
		{
			return ParseUserIds(UserNguoiThamGia);
		}

		/// <summary>
		/// Trả về danh sách Id người được xem thông báo
		/// </summary>
		/// <returns></returns>
		public List<int> UserThongBaos()
		{
			return ParseUserIds(UserThongBao);
		}

		/// <summary>
		/// Trả về danh sách Id người đã xem văn bản
		/// </summary>
		/// <returns></returns>
		public List<int> UserDaXems()
		{
			return ParseUserIds(UserNguoiDaXem);
		}

		/// <summary>
		/// Trả về danh sách Id người giám sát
		/// </summary>
		/// <returns></returns>
		public List<int> UserUyQuyens()
		{
			return ParseUserIds(UserGiamSat);
		}

		/// <summary>
		/// Trả về danh sách tất cả người liên quan đến văn bản.
		/// </summary>
		/// <returns></returns>
		public List<int> DocumentUserList()
		{
			return ParseUserIds(DocumentUsers);
		}

		private List<int> ParseUserIds(string userStr)
		{
			var result = new List<int>();
			if (string.IsNullOrEmpty(userStr))
			{
				return result;
			}

			var userIds = userStr.Split(new string[] { SEPARATOR_CHAR }, StringSplitOptions.RemoveEmptyEntries);
			result = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => Int32.Parse(u)).ToList();

			return result.Distinct().ToList();
		}
	}
}