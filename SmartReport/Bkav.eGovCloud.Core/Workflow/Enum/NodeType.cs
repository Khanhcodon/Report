using System;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Enum : NodeType - public - Core
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 230113
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Kiểu nút trong quy trình bàn giao động. Trong một quy trình có các nút. Mỗi nút thuộc một trong các kiểu này.</para>
    /// (CuongNT@bkav.com - 230113)
    /// </summary>
    [Flags]
    public enum NodeType
    {
        /// <summary>
        /// Là nút thông thường
        /// </summary>
        NutThongThuong = 1,

        /// <summary>
        /// Là nút dừng tính thời gian xử lý
        /// </summary>
        NutDungXuLy = 2,

        /// <summary>
        /// Là nút liên thông. Nút đại diện cho một cơ quan làm việc liên thông với cơ quan hiện tại.
        /// </summary>
        NutLienThong = 4,

        /// <summary>
        /// Là nút để xin ý kiến. Khác nút bàn giao thông thường.
        /// </summary>
        NutXinYKien = 8
    }
}
