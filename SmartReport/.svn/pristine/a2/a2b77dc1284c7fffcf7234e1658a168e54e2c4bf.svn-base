
using System;
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDepartmentJobTitlesPosition - public - Entity
    /// Access Modifiers: 
    /// Create Date : 151012
    /// Author      : GiangPN
    /// Description : Entity tương ứng với bảng UserDepartmentJobTitlesPosition trong CSDL. Bảng nối giữa phòng ban - user - chức danh-chức vụ.
    /// </summary>
    public class UserDepartmentJobTitlesPosition
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa người dùng, phòng ban, chức danh và chức vụ
        /// </summary>
        public int UserDepartmentJobTitlesPositionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id phòng ban
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị mở rộng của Id phòng ban
        /// </summary>
        /// <value>
        /// 	<c>Dạng:2.3.5</c>.
        /// </value>
        public string DepartmentIdExt { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id chức danh
        /// </summary>
        public int JobTitlesId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id chức vụ
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị chỉ ra đây là phòng ban chức vụ chính của cán bộ
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị chỉ ra cán bộ này là quản trị cho phòng ban này
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Trạng thái cho phép cán bộ nhận văn bản đến khi phát hành.
        /// </summary>
        public bool HasReceiveDocument { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã định danh để gửi liên thông
        /// </summary>
        public string EdocId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phòng ban
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chức vụ
        /// </summary>
        public virtual JobTitles JobTitles { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người dùng
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chức vụ
        /// </summary>
        public virtual Position Position { get; set; }
    }
}
