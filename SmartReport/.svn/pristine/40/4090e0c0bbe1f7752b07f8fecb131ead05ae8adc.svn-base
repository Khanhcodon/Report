using System;
using System.ComponentModel;
namespace Bkav.eGovCloud.Core.Template
{
    /// <summary>
    /// Danh sách vai trò sử dụng mẫu phiếu.
    /// <para> Giá trị tương ứng với NodePermission</para>
    /// </summary>
    [Flags]
    public enum TemplatePermission
    {
        /// <summary>
        /// Quyền khởi tạo văn bản/tiếp nhận hồ sơ
        /// </summary>
        [Description("Khởi tạo")]
        KhoiTao = 1,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Tiếp nhận thông tin bổ sung cho hồ sơ từ dân
        /// </summary>
        [Description("Bổ sung")]
        NhanBoSung = 512,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Cho ý kiến duyệt Đồng ý/Từ chối lên văn bản/hồ sơ
        /// </summary>
        [Description("Kí duyệt")]
        KiDuyet = 1024,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Trả kết quả xử lý cho dân
        /// </summary>
        [Description("Trả kết quả")]
        TraKetQua = 2048
    }
}
