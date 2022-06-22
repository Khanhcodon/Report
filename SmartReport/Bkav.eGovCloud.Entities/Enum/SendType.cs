using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Hình thức gửi
    /// </summary>
    public enum SendType
    {
        /// <summary>
        /// Nhan truc tiep tu eGov
        /// </summary>
         [Description("Nhận trên phần mềm")]
        eGov = 0,

        /// <summary>
        /// Bưu điện
        /// </summary>
        [Description("egovcloud.enum.sendtype.buudien")]
        BuuDien = 1,

        /// <summary>
        /// Email
        /// </summary>
        [Description("egovcloud.enum.sendtype.email")]
        Email = 2,

        /// <summary>
        /// Fax
        /// </summary>
        [Description("egovcloud.enum.sendtype.fax")]
        Fax = 3,

        /// <summary>
        /// Trao tay
        /// </summary>
        [Description("egovcloud.enum.sendtype.traotay")]
        TraoTay = 4,

        /// <summary>
        /// Họp không giấy
        /// </summary>
        [Description("Họp không giấy")]
        HopKhongGiay = 5
    }
}
