using System.ComponentModel;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Kiểu kích hoạt lịch trình
    /// </summary>
    public enum DocTypeScheduleType
    {
        /// <summary>
        /// Kích hoạt hàng ngày
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangngay")]
        HangNgay = 3,

        /// <summary>
        /// Kích hoạt hàng tuần
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangtuan")]
        HangTuan = 4,

        /// <summary>
        /// Kích hoạt hàng tháng
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangthang")]
        HangThang = 5,

        /// <summary>
        /// Kích hoạt hàng quý
        /// </summary>
        [Description("Hàng quý")]
        HangQuy = 6,

        /// <summary>
        /// Kích hoạt hàng năm
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangnam")]
        HangNam = 7,
    }
}
