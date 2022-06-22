using System;
using System.Linq;
using System.Collections.Generic;
namespace Bkav.eGovCloud.Entities.Customer
{
	/// <summary>
	/// Bkav Corp. - BSO - eGov - eOffice team
	/// Project: eGov Cloud v1.0
	/// Class : Document - public - Entity
	/// Access Modifiers: 
	/// Create Date : 15082013
	/// Author      : DungHV
	/// Description : Entity tương ứng với bảng Address trong CSDL: lưu thông tin các cơ quan ngoài
	/// </summary>
	public class Address
	{
		/// <summary>
		/// Lấy hoặc thiết lập key
		/// </summary>
		public int AddressId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tên cơ quan ngoài
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập địa chỉ thư điện tử cơ quan ngoài
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập số điện thoại cơ quan ngoài
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập số mở rộng của Phone
		/// </summary>
		public string PhoneExt { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập số Fax của cơ quan ngoài
		/// </summary>
		public string Fax { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập địa chỉ cơ quan ngoài
		/// </summary>
		public string AddressString { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập mã định danh của 1 cơ qua khi liên thông văn bản qua eDoc
		/// </summary>
		public string EdocId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái xác định đâu là cơ quan mình
		/// </summary>
		public bool IsMe { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Website cơ quan
		/// </summary>
		public string Website { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Website cơ quan
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập nhóm các cơ quan
		/// </summary>
		public string GroupName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Cơ quan cha
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập việc phát hành qua Email
		/// </summary>
		public bool IsPublishEmail { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập việc phát hành qua Email
		/// </summary>
		public int LevelEdocId { get; set; }

		/// <summary>
		/// Cho phép lấy lại văn bản đã gửi liên thông
		/// </summary>
		public bool HasRecalled { get; set; }
	}
}
