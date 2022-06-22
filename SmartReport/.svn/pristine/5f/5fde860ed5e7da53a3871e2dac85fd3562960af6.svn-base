using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.Document
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Enum : DocTypePermission - public - Core
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 230113
    /// Author      : CuongNT@bkav.com
    /// </author>
    /// <summary>
    /// <para>Các quyền được phép thực hiện trên loại hồ sơ</para>
    /// (CuongNT@bkav.com - 230113)
    /// </summary>
    [Flags]
    [Serializable]
    public enum DocTypePermissions
    {
        /// <summary>
        /// Loại hồ sơ được phép thực hiện nghiệp vụ Xác nhận xử lý
        /// </summary>
        [Description("Được phép xác nhận xử lý")]
        DuocPhepXacNhanXuLy =           0x00000001,

        /// <summary>
        /// Loại hồ sơ bắt buộc phải thực hiện nghiệp vụ Cập nhật kết quả xử lý cuối
        /// </summary>
        [Description("Bắt buộc cập nhật kết quả xử lý cuối cùng")]
        BatBuocCapNhatKetQuaXuLyCuoi =  0x00000002,

        // TienBV: bỏ check quyền này ở loại hồ sơ và chỉ lấy ở quy trình xử lý.
        // Do hiện tại để thế này có cấu hình và cũng thừa.
        ///// <summary>
        ///// Loại hồ sơ được phép thực hiện nghiệp vụ Yêu cầu bổ sung
        ///// </summary>
        //[Description("Được phép yêu cầu bổ sung")]
        //DuocPhepYeuCauBoSung =          0x00000004,

        /// <summary>
        /// Loại hồ sơ được phép cấp phép đăng ký kinh doanh
        /// </summary>
        [Description("Cho phép cấp phép đăng ký kinh doanh")]
        DuocPhepCapPhep = 0x00000008,

        /// <summary>
        /// Loại hồ sơ được phép tiếp nhận công chứng
        /// </summary>
        [Description("Cho phép cấp phép tiếp nhận công chứng: mỗi giấy tờ sinh thành một hồ sơ")]
        DuocPhepCongChung = 0x00000010,

        /// <summary>
        /// Cho phép liên thông đến cơ quan khác
        /// </summary>
        [Description("Cho phép liên thông đến cơ quan khác")]
        DuocPhepLienThong = 0x00000020,

        /// <summary>
        /// Cho phép nhìn thấy tất cả ý kiến xử lý của các hướng rẽ nhánh
        /// </summary>
        [Description("Cho phép nhìn thấy tất cả ý kiến xử lý của các hướng rẽ nhánh")]
        DuocPhepXemYKienCacHuongReNhanh = 0x00000040
    }
}
