using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    ///<para></para> Bkav Corp. - BSO - eGov - eOffice team
    ///<para></para> Project: eGov Cloud v1.0
    ///<para></para> Enum : ActivityLogType - public - Entity
    ///<para></para> Access Modifiers: 
    ///<para></para> Create Date : 150414
    ///<para></para> Author      : TrungVH
    ///<para></para> Description : Loai nhật ký hành động
    /// </summary>
    public enum ActivityLogType
    {
        /// <summary>
        /// Ý kiến trong hướng xử lý chính.
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.dangnhap")]
        DangNhap = 1,

        /// <summary>
        /// Ý kiến hướng đồng xử lý trả lời.
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.dangxuat")]
        DangXuat = 2,

        /// <summary>
        /// Ý kiến hướng xin ý kiến trả lời
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.bangiao")]
        BanGiao = 3,

        /// <summary>
        /// Các ý kiến bổ sung hs, vb
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.thongbao")]
        ThongBao = 4,

        /// <summary>
        /// Ý kiến ký duyệt
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.huyvanban")]
        HuyVanBan = 5,

        /// <summary>
        /// Trạng thái xử lý hồ sơ: thành công hay không.
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.ketthucvanban")]
        KetThucVanBan = 6,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.phanloai")]
        PhanLoai = 7,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.phathanh")]
        PhatHanh = 8,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.kyduyet")]
        KyDuyet = 9,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.xinykien")]
        XinYKien = 10,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.guiykien")]
        GuiYKien = 11,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.tiepnhan")]
        TiepNhan = 12,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.xingiahan")]
        XinGiaHan = 13,

        /// <summary>
        /// Ý kiến khi kết thúc một bản sao
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.chuyenykiendonggop")]
        ChuyenYKienDongGop = 14,

        /// <summary>
        /// Gửi mail
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.sendmail")]
        SendMail = 15,

        /// <summary>
        /// Gửi tin nhắn
        /// </summary>
        [Description("egovcloud.enum.activitylogtype.sendsms")]
        SendSms = 16,

        /// <summary>
        /// Log admin
        /// </summary>
        [Description("")]
        Admin = 17,

        /// <summary>
        /// Log admin
        /// </summary>
        [Description("")]
        DinhChinh = 18,
    }
}
