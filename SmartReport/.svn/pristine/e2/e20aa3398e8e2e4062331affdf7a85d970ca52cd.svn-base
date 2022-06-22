using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Entities.Customer.Settings
{
    /// <summary>
    ///
    /// </summary>
    public class WorkTimeSettings : ISettings
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public WorkTimeSettings()
        {
            AmStartTime = "8:0";
            AmEndTime = "11:30";
            PmStartTime = "13:30";
            PmEndTime = "17:30";
            AmSaturdayStartTime = "8:0";
            AmSaturdayEndTime = "11:30";
            WorkTimeType = 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amStartTime">Thời gian bắt đầu buổi sáng</param>
        /// <param name="amEndTime">Thời gian kết thúc buổi sáng</param>
        /// <param name="pmStartTime">Thời gian bắt đầu buổi chiều</param>
        /// <param name="pmEndTime">Thời gian kết thúc buổi chiều</param>
        /// <param name="amSaturdayStartTime">Thời gian bắt đầu buổi sáng ngày thứ 7</param>
        /// <param name="amSaturdayEndTime">Thời gian kết thúc buổi sáng ngày thứ 7</param>
        public WorkTimeSettings(
            string amStartTime,
            string amEndTime,
            string pmStartTime,
            string pmEndTime,
            string amSaturdayStartTime,
            string amSaturdayEndTime)
        {
            AmStartTime = amStartTime;
            AmEndTime = amEndTime;
            PmStartTime = pmStartTime;
            PmEndTime = amSaturdayStartTime;
            AmSaturdayStartTime = amSaturdayStartTime;
            AmSaturdayEndTime = amSaturdayEndTime;
        }

        /// <summary>
        /// Thời gian bắt đầu làm việc buổi sáng
        /// </summary>
        public string AmStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc buổi sáng
        /// </summary>
        public string AmEndTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu làm việc buổi chiều
        /// </summary>
        public string PmStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc buổi chiều
        /// </summary>
        public string PmEndTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu làm việc buổi sáng thứ 7
        /// </summary>
        public string AmSaturdayStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc sáng thứ 7
        /// </summary>
        public string AmSaturdayEndTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu làm việc chiều thứ 7
        /// </summary>
        public string PmSaturdayStartTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc làm việc chiều thứ 7
        /// </summary>
        public string PmSatudayEndTime { get; set; }

        /// <summary>
        /// Làm việc chiều thứ 7
        /// </summary>
        public bool IsPmWorkingSaturday { get; set; }

        /// <summary>
        /// Sử dụng cách tính thời gian làm việc khi tính hạn xử lý công văn
        /// true: bình thường - Cộng thời gian bình thường
        /// false: Sử dụng cách tính của nhà nước:
        /// - Sau 15h chiều: Tính như bình thường
        /// - Trước 15h chiều: tính cả ngày hiện tại cũng là 1 ngày xử lý
        /// </summary>
        public bool IsNormalTime { get; set; }

        /// <summary>
        /// Kiểu tính ngày hẹn trả
        /// </summary>
        public int WorkTimeType { get; set; }
    }
}