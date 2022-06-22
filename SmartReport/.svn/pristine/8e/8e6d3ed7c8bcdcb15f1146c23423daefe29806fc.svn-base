using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DepartmentCached
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id phòng ban
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id phòng ban cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra phòng ban này đã được kích hoạt
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu phòng ban này đã được kích hoạt; ngược lại, <c>false</c>.
        /// </value>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị mở rộng của Id phòng ban
        /// </summary>
        /// <value>
        /// 	<c>Dạng:2.3.5</c>.
        /// </value>
        public string DepartmentIdExt { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị tên phòng ban dưới dạng đường dẫn đầy đủ(bao gồm cả tên các phòng ban cấp cha).
        /// </summary>
        /// <value>
        /// 	<c>Giá trị dạng: \Bkav\BSO\Phòng2</c>.
        /// </value>
        public string DepartmentPath { get; set; }

        /// <summary>
        /// Tên hiển thị lên giao diện người dùng
        /// </summary>
        public string DepartmentLabel { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị thể hiện vị trí phòng ban được sắp xếp.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị thể hiện cấp độ phòng ban trên cây.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo phòng ban
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo phòng ban
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người cập nhật phòng ban cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật phòng ban cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// Cho phép nhận mail cảnh báo
        /// </summary>
        public bool HasReceiveWarning { get; set; }

        /// <summary>
        /// Danh sách mail nhận cảnh báo
        /// </summary>
        public string Emails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasCalendar { get; set; }
    }
}
