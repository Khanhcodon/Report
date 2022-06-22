using System;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimerJob - public - Entity
    /// Access Modifiers: 
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : Entity tương ứng với bảng timerJob trong CSDL
    /// </summary>
    public class TimerJob
    {
        /// <summary>
        /// Key
        /// </summary>
        public int TimerJobId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên timerJob
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại lịch trình
        /// </summary>
        public int TimerJobType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cấu hình timer
        /// </summary>
        public string TimerJobConfig { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập domain áp dụng cho timerJob
        /// </summary>
        public int DomainId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời điểm cuối cùng chạy timerJob
        /// </summary>
        public DateTime? DateLastJobRun { get; set; }

        /// <summary>
        /// Lấy và thiết lập 
        /// </summary>
        public DateTime JobInterval { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời điếm tiếp theo chạy timerJob (trước)
        /// </summary>
        public DateTime DateNextJobStartAfter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời điểm tiếp theo chạy timerJob (sau)
        /// </summary>
        public DateTime DateNextJobStartBefore { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu kích hoạt timerjob
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cấu hình của timerJob
        /// </summary>
        public string ScheduleConfig { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng timerJob
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái job đang được thực thi
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập domain chạy time job
        /// </summary>
        public virtual Domain Domain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại lịch trình
        /// </summary>
        public virtual TimerJobType TimerJobTypeEnum
        {
            get
            {
                return (TimerJobType)TimerJobType;
            }

            set
            {
                ScheduleType = (int)value;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập kích hoạt
        /// </summary>
        public virtual ScheduleType ScheduleTypeEnum
        {
            get
            {
                return (ScheduleType)ScheduleType;
            }

            set
            {
                ScheduleType = (int)value;
            }
        }
    }
}
