using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class IndicatorValueDepartment
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id 
        /// </summary>
        public int IndicatorDepartmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên 
        /// </summary>
        public Guid IndicatorValueId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Tên 
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
