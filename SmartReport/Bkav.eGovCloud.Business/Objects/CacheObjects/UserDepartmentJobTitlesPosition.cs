using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đối tượng cache quan hệ người dùng phòng ban chức vụ
    /// </summary>
    [Serializable]
    public class UserDepartmentJobTitlesPositionCached
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
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

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
        /// Account cán bộ
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Tên cán bộ
        /// </summary>
        public string UserFullName { get; set; }


        /// <summary>
        /// Email của phòng ban
        /// </summary>
        public string Emails { get; set; }

        /// <summary>
        /// Email của phòng ban
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị thể hiện vị trí phòng ban được sắp xếp.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị thể hiện cấp độ phòng ban trên cây.
        /// </summary>
        public int Level { get; set; }
    }
}
