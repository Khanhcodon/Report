using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DocTypeTimeJob
    {
        /// <summary>
        /// 
        /// </summary>
        public int DocTypeTimeJobId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// lấy và thiết lập kiểu kích hoạt timejobDueDate
        /// </summary>
        public int ScheduleTypeDueDate { get; set; }

        /// <summary>
        /// lấy và thiết lập kiểu kích hoạt timejobOutOfDate
        /// </summary>
        public int ScheduleTypeOutOfDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ScheduleConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ScheduleConfigDueDate { get; set; }

        /// <summary>
        /// cảnh báo và thiết lập cấu hình cảnh báo quá hạn
        /// </summary>
        public string ScheduleConfigOutOfDate { get; set; }

        /// <summary>
        /// lấy thiết lập trạng thái của timejob Alert
        /// </summary>
        public bool IsActiveAlert { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActiveAlertOut { get; set; }
    }
}
