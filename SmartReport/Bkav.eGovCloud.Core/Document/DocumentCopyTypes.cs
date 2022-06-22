using System;
using System.ComponentModel;

namespace Bkav.eGovCloud.Core.Document
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Enum : DocumentCopyTypes - public - Core
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 230113
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Kiểu văn bản đang giữ: Xử lý chính, Đồng xử lý, Chờ cho ý kiến.</para>
    /// (CuongNT@bkav.com - 230113)
    /// </summary>
    [Flags]
    public enum DocumentCopyTypes
    {
        /// <summary>
        /// <para>1 - Hướng xử lý chính</para>
        /// </summary>
        [Description("Văn bản xử lý chính")]
        XuLyChinh = 0x00000001,

        /// <summary>
        /// <para>2 - Hướng đồng xử lý</para>
        /// </summary>
        [Description("Văn bản đồng xử lý")]
        DongXuLy = 0x00000002,

        /// <summary>
        /// <para>4 - Hướng xin ý kiến</para>
        /// </summary>
        [Description("Văn bản xin ý kiến")]
        XinYKien = 0x00000004,

        /// <summary>
        /// <para>8 - Hướng thông báo.</para>
        /// <para>(CuongNT@bkav.com - 040413)</para>
        /// </summary>
        [Description("Văn bản thông báo")]
        ThongBao = 0x00000008,
        
        /// <summary>
        /// <para>32 - Hướng duyệt gia hạn</para>
        /// <para>(CuongNT@bkav.com - 040413)</para>
        /// </summary>
        [Description("Văn bản duyệt gia hạn")]
        DuyetGiaHan = 0x00000020,

        /// <summary>
        /// <para>64 - Hướng chờ cập nhật kết quả dừng xử lý (Chờ tiếp nhận bổ sung)</para>
        /// <para>(CuongNT@bkav.com - 090413)</para>
        /// </summary>
        [Description("Văn bản chờ tiếp nhận bổ sung")]
        ChoKetQuaDungXuLy = 0x00000040
    }
}
