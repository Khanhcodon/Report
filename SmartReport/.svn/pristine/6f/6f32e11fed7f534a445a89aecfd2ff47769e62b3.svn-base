using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Loại bổ sung
    /// </summary>
    public enum SupplementType
    {
        /// <summary>
        /// 
        /// </summary>
        Default = 0,

        /// <summary>
        /// Tính lại thời gian: sau khi tiếp nhận bổ sung thành công chuyển về đầu quy trình (tiếp nhận).
        /// </summary>
        [Description("egovcloud.enum.supplementtype.reset")]
        Reset = 1,

        /// <summary>
        /// Tiếp tục xử lý: sau khi tiếp nhận bổ sung chuyển tiếp cho cán bộ thụ lý.
        /// </summary>
        [Description("egovcloud.enum.supplementtype.continue")]
        Continue = 2,

        /// <summary>
        /// <para></para> Thêm ngày cố đinh: yêu cầu dân bổ sung trong 1 khoảng thời gian, quá khoảng thời gian này sẽ hủy hồ sơ.
        /// <para></para> Nếu bổ sung thành công chuyển về thụ lý xử lý tiếp.
        /// </summary>
        [Description("egovcloud.enum.supplementtype.fixeddays")]
        FixedDays = 3
    }
}
