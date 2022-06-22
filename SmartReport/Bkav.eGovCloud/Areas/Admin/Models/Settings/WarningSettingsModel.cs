using System.Collections.Generic;
using System.Linq;
namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    public class WarningSettingsModel
    {
        /// <summary>
        /// Kích hoạt gửi cảnh báo
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Cho phép gửi mail đến người vi phạm
        /// </summary>
        public bool HasSentToInfringer { get; set; }

        /// <summary>
        /// Gửi cảnh báo tới các phòng ban, đơn vị
        /// </summary>
        public bool HasSentToDepartment { get; set; }

        /// <summary>
        /// Thời điểm gửi cảnh báo trước hạn tính theo ngày, số ngày cách nhau = dấu ;
        /// Ví dụ: 1;3;4
        /// </summary>
        public string MomentToSentOverdue { get; set; }

        /// <summary>
        /// Danh sách thời điểm gửi cảnh báo trước hạn
        /// </summary>
        public List<int> MomentToSentOverdues
        {
            get
            {
                var result = new List<int>();
                if (string.IsNullOrEmpty(MomentToSentOverdue))
                {
                    return result;
                }

                result.AddRange(MomentToSentOverdue.Split(new char[] { ';' }).Select(m => int.Parse(m)));

                return result;
            }
        }

        /// <summary>
        /// Số lượng hồ sơ quá hạn cho phép
        /// </summary>
        public int NumberToWarning { get; set; }

        /// <summary>
        /// Số phần trăm hồ sơ quá hạn cho phép
        /// </summary>
        public int PercenToWarning { get; set; }

        /// <summary>
        /// Danh sách email nhận cảnh báo khi vi phạm ngưỡng
        /// </summary>
        public string SentMailOverdueTo { get; set; }

        /// <summary>
        /// Danh sách email nhận cảnh báo khi vi phạm ngưỡng
        /// </summary>
        public List<string> SentMailOverdueToes
        {
            get
            {
                if (string.IsNullOrEmpty(SentMailOverdueTo))
                {
                    return new List<string>();
                }

                return SentMailOverdueTo.Split(new char[] { ';' }).ToList();
            }
        }

        /// <summary>
        /// Danh sách người dùng được bỏ qua gửi cảnh báo
        /// </summary>
        public string UserToIgnore { get; set; }

        /// <summary>
        /// Danh sách người dùng được bỏ qua gửi cảnh báo
        /// </summary>
        public List<int> UserToIgnores
        {
            get
            {
                var result = new List<int>();
                if (string.IsNullOrEmpty(UserToIgnore))
                {
                    return result;
                }

                result.AddRange(UserToIgnore.Split(new char[] { ';' }).Select(m => int.Parse(m)));

                return result;
            }
        }

        /// <summary>
        /// Mẫu email
        /// </summary>
        public string Subject { get; set; }
    }
}
