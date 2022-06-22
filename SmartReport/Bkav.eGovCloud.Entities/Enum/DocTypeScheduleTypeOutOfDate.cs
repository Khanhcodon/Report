using System.ComponentModel;


namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// kiểu lịch trình quá hạn
    /// </summary>
    public enum DocTypeScheduleTypeOutOfDate
    {
        /// <summary>
        /// Kích hoạt kiểu ngày 
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangngayoutofdate")]
        HangNgayOutOfDate = 3,

        /// <summary>
        /// Kích hoạt kiểu tuần
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangtuanoutofdatedate")]
        HangTuanOutOfDate = 4,

        /// <summary>
        /// Kích hoặt kiểu tháng
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangthangoutofdatedate")]
        HangThangDueDatOutOfDate = 5,

        /// <summary>
        /// Kích hoạt kiểu quý
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangquyoutofdatedate")]
        HangQuyOutOfDate = 6,

        /// <summary>
        /// Kích hoạt kiểu năm
        /// </summary>
        [Description("egovcloud.enum.scheduletype.hangnamoutofdatedate")]
        HangNamOutOfDate = 7,
    }
}
