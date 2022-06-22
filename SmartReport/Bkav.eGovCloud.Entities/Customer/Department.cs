using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Department - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Department trong CSDL
    /// </summary>
    public class Department
    {
        private ICollection<Department> _departmentChildren;
        private ICollection<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositions;
        
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
        /// Cho phép đặt lịch
        /// </summary>
        public bool HasCalendar { get; set; }

        /// <summary>
        /// Danh sách mail nhận cảnh báo
        /// </summary>
        public string Emails { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các phòng ban con
        /// </summary>
        public virtual ICollection<Department> DepartmentChildren
        {
            get { return _departmentChildren ?? (_departmentChildren = new List<Department>()); }
            set { _departmentChildren = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id các user, chức danh và chức vụ
        /// </summary>
        public List<string> UserJobTitlesPositionIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<RateEmployee> RateEmployees {get;set;}

        /// <summary>
        /// Lấy hoặc thiết lập Phòng ban cha
        /// </summary>
        public virtual Department DepartmentParent { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách user(có chức danh) thuộc phòng ban
        /// </summary>
        public virtual ICollection<UserDepartmentJobTitlesPosition> UserDepartmentJobTitlesPositions
        {
            get { return _userDepartmentJobTitlesPositions ?? (_userDepartmentJobTitlesPositions = new List<UserDepartmentJobTitlesPosition>()); }
            set { _userDepartmentJobTitlesPositions = value; }
        }
        
    }
}
