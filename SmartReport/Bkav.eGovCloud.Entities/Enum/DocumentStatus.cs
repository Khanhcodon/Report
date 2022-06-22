using System;

namespace Bkav.eGovCloud.Entities
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Enum : NodePermissions - public - Core
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 230113
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Trạng thái văn bản/hồ sơ</para>
    /// (CuongNT@bkav.com - 230113)
    /// </summary>
    [Flags]
    public enum DocumentStatus
    {
        /// <summary>
        /// Đang dự thảo
        /// </summary>
        DuThao = 1,

        /// <summary>
        /// Đang xử lý
        /// </summary>
        DangXuLy = 2,

        /// <summary>
        /// Đã kết thúc
        /// </summary>
        KetThuc = 4,

        /// <summary>
        /// Đã bị  loại bỏ
        /// </summary>
        LoaiBo = 8, 

        /// <summary>
        /// Đang dừng xử lý
        /// </summary>
        DungXuLy = 16,

        /// <summary>
        /// Van ban lien thong trung so
        /// </summary>
        LienThongTrungSo = 32,
    }
}
