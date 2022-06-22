using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bảng tiêu chí vi phạm
    /// </summary>
    public class RateEmployee
    {
        /// <summary>
        /// Id
        /// </summary>
        public int RateEmployeeId { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Id cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Tên tiêu chí
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Điểm đánh giá
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// Kích hoạt
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RateEmployee ParentRateEmployee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<RateEmployee> RateEmployeeChildrens { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<CheckInfringe> CheckInfringes { get; set; }

    }
}
