using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Business.Common;
using System.Web.Mvc;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    ///
    /// </summary>
    public class DocTypeTimeJobModel
    {
        private readonly ResourceBll _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

        public DocTypeTimeJobModel()
        {
            IsActive = false;
            IsActiveAlert = false;
            ScheduleTypeEnum = DocTypeScheduleType.HangNam;
            ScheduleTypeEnumDueDate = DocTypeScheduleTypeDueDate.HangNamDueDate;
            ScheduleTypeEnumOutOfDate = DocTypeScheduleTypeOutOfDate.HangNamOutOfDate;
            ScheduleConfig = "{\"Type\": \"HangNam\", \"FromMonth\": 1, \"FromDayOfMonth\": 1, \"FromHour\": 1, \"FromMinute\": 1}";
            ScheduleConfigDueDate = "{\"Type\": \"HangNamDueDate\", \"FromMonth\": 1, \"FromDayOfMonth\": 1, \"FromHour\": 1, \"FromMinute\": 1}";
            ScheduleConfigOutOfDate = "{\"Type\": \"HangNamOutOfDate\", \"FromMonth\": 1, \"FromDayOfMonth\": 1, \"FromHour\": 1, \"FromMinute\": 1}";
        }

        public DocTypeTimeJobModel(Guid docTypeId) : this()
        {
            DocTypeId = docTypeId;
        }

        /// <summary>
        /// Key
        /// </summary>
        public int DocTypeTimeJobId { get; set; }

        /// <summary>
        /// DocTypeId
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy và thiết lập kiểu kích hoạt timerjob
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
        /// Lấy và thiết lập cấu hình của timerJob
        /// </summary>
        public string ScheduleConfig { get; set; }

        /// <summary>
        /// lấy và thiết lập cấu hình của cảnh báo đến hạn
        /// </summary>
        public string ScheduleConfigDueDate { get; set; }

        /// <summary>
        /// cảnh báo và thiết lập cấu hình cảnh báo quá hạn
        /// </summary>
        public string ScheduleConfigOutOfDate { get; set; }
        /// <summary>
        /// Lấy và thiết lập trạng thái sử dụng timerJob
        /// </summary>
        [LocalizationDisplayName("TimeJob.CreateOrEdit.Fields.IsActive.Label")]
        public bool IsActive { get; set; }

        /// <summary>
        /// lấy thiết lập trạng thái của timejob Alert
        /// </summary>
        [LocalizationDisplayName("TimeJob.CreateOrEdit.Fields.IsActiveAlert.Label")]


        /// <summary>
        /// 
        /// </summary>
        public bool IsActiveAlertOut { get; set; }

        public bool IsActiveAlert { get; set;  }
        /// <summary>
        /// Lấy hoặc thiết lập kích hoạt
        /// </summary>
        
        public DocTypeScheduleType ScheduleTypeEnum
        {
            get
            {
                return (DocTypeScheduleType)ScheduleType;
            }
            set
            {
                ScheduleType = (int)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DocTypeScheduleTypeDueDate ScheduleTypeEnumDueDate
        {
            get
            {
                return (DocTypeScheduleTypeDueDate)ScheduleTypeDueDate;
            }
            set
            {
                ScheduleTypeDueDate = (int)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DocTypeScheduleTypeOutOfDate ScheduleTypeEnumOutOfDate
        {
            get
            {
                return (DocTypeScheduleTypeOutOfDate)ScheduleTypeOutOfDate;
            }
            set
            {
                ScheduleTypeOutOfDate = (int)value;
            }
        }

        /// <summary>
        /// Tên định kì
        /// </summary>
        public string ScheduleTypeString
        {
            get
            {
                return _resourceService.GetEnumDescription<DocTypeScheduleType>(ScheduleTypeEnum);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ScheduleTypeStringDueDate
        {
            get
            {
                return _resourceService.GetEnumDescription<DocTypeScheduleTypeDueDate>(ScheduleTypeEnumDueDate);
            }
        }

        public string ScheduleTypeStringOutOfDate
        {
            get
            {
                return _resourceService.GetEnumDescription<DocTypeScheduleTypeOutOfDate>(ScheduleTypeEnumOutOfDate);
            }
        }
    }
}