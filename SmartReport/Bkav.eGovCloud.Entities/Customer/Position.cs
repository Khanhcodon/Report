using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Position - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Position trong CSDL
    /// </summary>
    public class Position
    {
        private ICollection<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositions; 

        /// <summary>
        /// Lấy hoặc thiết lập Id chức vụ
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên chức vụ
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mức ưu tiên
        /// </summary>
        public int PriorityLevel { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quyền ký duyệt
        /// </summary>
        public bool IsApproved { get; set; }

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
