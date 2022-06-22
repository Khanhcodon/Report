using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Loại giấy tờ
    /// </summary>
    public enum FeeType
    {
        /// <summary>
        /// Lệ phí được thu khi tiếp nhận
        /// </summary>
        [Description("egovcloud.enum.feetype.indextimerelapsed")]
        TiepNhan = 1,

        /// <summary>
        /// Lệ phí thường bổ sung
        /// </summary>
        [Description("egovcloud.enum.feetype.thuongbosung")]
        ThuongBosung = 2,

        /// <summary>
        /// Lệ phí trả cho dân khi trả kết quả
        /// </summary>
        [Description("egovcloud.enum.feetype.tracongdan")]
        TraCongDan = 3
    }
}
