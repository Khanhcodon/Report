using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JobTitles - public - Entity
    /// Access Modifiers:
    /// Create Date : 121012
    /// Author      : GiangPN
    /// Description : Entity tương ứng với bảng JobTitles trong CSDL. Dùng lưu trữ hệ thống các chức danh.
    /// </summary>
    public class JobTitles
    {
        private ICollection<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositions;

        /// <summary>
        /// Lấy hoặc thiết lập Id chức danh
        /// </summary>
        public int JobTitlesId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên chức danh
        /// </summary>
        public string JobTitlesName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quyền được ký duyệt
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mức ưu tiên
        /// </summary>
        public int PriorityLevel { get; set; }
        
        /// <summary>
        /// Xác định có phải là văn thư hay không
        /// </summary>
        public bool IsClerical { get; set; }

        /// <summary>
        /// Thiết lập quyền có thể tiếp nhận văn bản đến hay không
        /// </summary>
        public bool CanGetDocumentCome { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<UserDepartmentJobTitlesPosition> UserDepartmentJobTitlesPositions
        {
            get { return _userDepartmentJobTitlesPositions ?? (_userDepartmentJobTitlesPositions = new List<UserDepartmentJobTitlesPosition>()); }
            set { _userDepartmentJobTitlesPositions = value; }
        }
    }
}