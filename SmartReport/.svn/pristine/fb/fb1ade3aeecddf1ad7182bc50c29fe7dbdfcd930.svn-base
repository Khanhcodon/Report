using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Business.Common;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    ///
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(TimeJobValidator))]
    public class TimeJobModel
    {
        private readonly ResourceBll _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

        public TimeJobModel()
        {
            JobInterval = DateTime.Now;
            DateLastJobRun = null;
            DateNextJobStartAfter = DateTime.Now;
            DateNextJobStartBefore = DateTime.Now;
            IsActive = true;
        }

        /// <summary>
        /// Key
        /// </summary>
        public int TimeJobId { get; set; }

        /// <summary>
        /// Lấy và thiết lập tên timerJob
        /// </summary>
        [LocalizationDisplayName("TimeJob.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy và thiết lập loại lịch trình
        /// </summary>
        [LocalizationDisplayName("TimeJob.CreateOrEdit.Fields.TimerJobType.Label")]
        public int TimerJobType { get; set; }

        /// <summary>
        /// Lấy và thiết lập
        /// </summary>
        public DateTime JobInterval { get; set; }

        /// <summary>
        /// Lấy và thiết lập thời điểm cuối cùng chạy timerJob
        /// </summary>
        public DateTime? DateLastJobRun { get; set; }

        /// <summary>
        /// Lấy và thiết lập thời điếm tiếp theo chạy timerJob (trước)
        /// </summary>
        public DateTime DateNextJobStartAfter { get; set; }

        /// <summary>
        /// Lấy và thiết lập thời điểm tiếp theo chạy timerJob (sau)
        /// </summary>
        public DateTime DateNextJobStartBefore { get; set; }

        /// <summary>
        /// Lấy và thiết lập kiểu kích hoạt timerjob
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// Lấy và thiết lập cấu hình của timerJob
        /// </summary>
        public string ScheduleConfig { get; set; }

        /// <summary>
        /// Lấy và thiết lập trạng thái sử dụng timerJob
        /// </summary>
        [LocalizationDisplayName("TimeJob.CreateOrEdit.Fields.IsActive.Label")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái job đang được thực thi
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kích hoạt
        /// </summary>
        public ScheduleType ScheduleTypeEnum
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

        /// <summary>
        /// Lấy hoặc thiết lập loại lịch trình
        /// </summary>
        public EgovJobEnum TimerJobTypeEnum
        {
            get
            {
                return (EgovJobEnum)TimerJobType;
            }
            set
            {
                TimerJobType = (int)value;
            }
        }

        /// <summary>
        /// Tên định kì
        /// </summary>
        public string ScheduleTypeString
        {
            get
            {
                return _resourceService.GetEnumDescription<ScheduleType>(ScheduleTypeEnum);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TimerJobTypeString
        {
            get
            {
                return _resourceService.GetEnumDescription<EgovJobEnum>(TimerJobTypeEnum);
            }
        }

        public string OtherConfig { get; set; }
    }
}