using System.ComponentModel;
using Bkav.eGovCloud.Core.Template;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public enum CommonTemplateKeyEnum
    {
        /// <summary>
        /// Key dung chung lấy ngày tháng năm hiện tại
        /// </summary>
        [Description("egovcloud.enum.CommonTemplateKeyEnum.NgayThangNam")]
        [DatabaseValue("{common_ngay_thang_nam}")]
        NgayThangNam,

        /// <summary>
        /// Key dung chung lấy Thứ ngày tháng năm
        /// </summary>
        [Description("egovcloud.enum.CommonTemplateKeyEnum.ThuNgayThangNam")]
        [DatabaseValue("{common_thu_ngay_thang_nam}")]
        ThuNgayThangNam,
    }
}
