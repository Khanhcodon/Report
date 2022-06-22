using System.ComponentModel;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Các kiểu lịch trình
    /// </summary>
    public enum TimerJobType
    {
        /// <summary>
        /// Cảnh báo hồ sơ quá hạn
        /// </summary>
        [Description("egovcloud.enum.timerjobtype.warning")]
        Warning = 1,

        /// <summary>
        /// Đánh chỉ mục tìm kiếm trên solr
        /// </summary>
        [Description("egovcloud.enum.timerjobtype.searchindex")]
        SearchIndex = 2,

        /// <summary>
        /// Xóa bỏ các file tạm
        /// </summary>
        [Description("egovcloud.enum.timerjobtype.deletetempfile")]
        DeleteTempFile = 3,

        /// <summary>
        /// Backup cơ sở dữ liệu
        /// </summary>
        [Description("Backup cơ sở dữ liệu")]
        BackupDatabase = 4,

        /// <summary>
        /// Khôi phục cơ sở dữ liệu
        /// </summary>
        [Description(" Khôi phục cơ sở dữ liệu")]
        RestoreDatabase = 5
    }
}
