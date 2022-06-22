using System;

namespace Bkav.eGovCloud.Entities.Customer
{
	/// <summary>
	/// Bkav Corp. - BSO - eGov - eOffice team
	/// Project: eGov Cloud v1.0
	/// Class : DocRelation - public - Entity
	/// Access Modifiers: 
	/// Create Date : 200513
	/// Author      : GiangPN
	/// Description : Entity tương ứng với bảng DocRelation trong CSDL
	/// </summary>
	public class DocRelationModel
	{
		/// <summary>
		/// Lấy hoặc thiết lập Id hồ sơ liên quan
		/// </summary>
		public Guid RelationId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id văn bản/hồ sơ copy liên quan
		/// </summary>
		public int RelationCopyId { get; set; }

		/// <summary>
		/// <para> Lấy hoặc thiết lập loại văn bản liên quan</para>
		/// <para> Notes:</para>
		/// <para>   - 1: Liên quan thông thường</para>
		/// <para>   - 2: Liên quan khi trả lời văn bản/hồ sơ</para>
		/// <para>   - 3: Liên quan khi hồi báo</para>
		/// </summary>
		public int RelationType { get; set; }

		/// <summary>
		/// Xác định văn bản nào người nhận mới dc xem.
		/// </summary>
		public bool IsAddNext { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Trích yếu của văn bản liên quan
		/// </summary>
		public string Compendium { get; set; }

		/// <summary>
		/// Tên công dân: dùng cho hs một cửa
		/// </summary>
		public string CitizenName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số kí hiệu (eOffice), mã hồ sơ (eGate) của văn bản liên quan
		/// </summary>
		public string DocCode { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập số đến đi
		/// </summary>
		public string InOutCode { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày tạo của văn bản liên quan
		/// </summary>
		public DateTime? DateCreated { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tên thể loại văn bản/hồ sơ của văn bản liên quan
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// Đã xóa khỏi văn bản hay chưa
		/// </summary>
		public bool IsRemoved { get; set; }

		/// <summary>
		/// Văn bản liên quan mới
		/// </summary>
		public bool IsNew { get; set; }
    }
}
