using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    public class WorkTimeSettingsModel
    {
        /// <summary>
        /// Thời gian bắt đầu làm việc buổi sáng
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.StartTime.Label")]
        public string AmStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc buổi sáng
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.EndTime.Label")]
        public string AmEndTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu làm việc buổi chiều
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.StartTime.Label")]
        public string PmStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc buổi chiều
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.EndTime.Label")]
        public string PmEndTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu làm việc buổi sáng thứ 7
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.StartTime.Label")]
        public string AmSaturdayStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc sáng thứ 7
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.EndTime.Label")]
        public string AmSaturdayEndTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu làm việc chiều thứ 7
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.StartTime.Label")]
        public string PmSaturdayStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc chiều thứ 7
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.EndTime.Label")]
        public string PmSatudayEndTime { get; set; }

        /// <summary>
        /// Làm việc chiều thứ 7
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.IsPmWorkingSaturday.Label")]
        public bool IsPmWorkingSaturday { get; set; }

        /// <summary>
        /// Sử dụng cách tính thời gian làm việc khi tính hạn xử lý công văn
        /// true: bình thường - Cộng thời gian bình thường
        /// false: Sử dụng cách tính của nhà nước:
        /// - Sau 15h chiều: Tính như bình thường
        /// - Trước 15h chiều: tính cả ngày hiện tại cũng là 1 ngày xử lý
        /// </summary>
        [LocalizationDisplayName("Setting.WorkTime.Fields.IsNormalTime.Label")]
        public bool IsNormalTime { get; set; }

        /// <summary>
        /// Kiểu tính ngày hẹn trả
        /// </summary>
        public int WorkTimeType { get; set; }
    }
}