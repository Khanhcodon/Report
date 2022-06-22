using System;

namespace Bkav.eGovCloud.Core.Document
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Class : DocumentPermissions - public - Core
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 230113
    /// Author      : CuongNT@bkav.com
    /// </author>
    /// <summary>
    /// <para>Các nghiệp vụ tác động lên văn bản/hồ sơ</para>
    /// (CuongNT@bkav.com - 230113)
    /// </summary>
    [Flags]
    public enum DocumentPermissions
    {
        /// <summary>
        /// Quyền khởi tạo văn bản
        /// 1
        /// </summary>
        KhoiTaoVanBan = 0x00000001,

        /// <summary>
        /// Quyền mở xem một văn bản
        /// 2
        /// </summary>
        XemVanBan = 0x00000002,

        /// <summary>
        /// Quyền đính kèm thêm tệp cho văn bản
        /// 4
        /// </summary>
        DinhKem = 0x00000004,

        /// <summary>
        /// Quyền sửa nội dung văn bản (biểu mẫu động, biểu mẫu html...)
        /// 8
        /// </summary>
        SuaVanBan = 0x00000008,

        /// <summary>
        /// Quyền gửi ý kiến xử lý lên văn bản
        /// 16
        /// </summary>
        GuiYKien = 0x00000010,

        /// <summary>
        /// Quyền bàn giao văn bản tới cán bộ khác trên quy trình động
        /// 32
        /// </summary>
        BanGiao = 0x00000020,

        /// <summary>
        /// Quyền thông báo văn bản cho các cán bộ, cơ quan khác biết
        /// 64
        /// </summary>
        ThongBao = 0x00000040,

        /// <summary>
        /// Quyền được phép xin ý kiến từ cán bộ khác
        /// 128
        /// </summary>
        XinYKien = 0x00000080,

        /// <summary>
        /// Quyền phân loại lại văn bản sang quy trình xử lý khác
        /// 256
        /// </summary>
        PhanLoai = 0x00000100,

        /// <summary>
        /// Quyền tạo mới văn bản và tự động đính kèm văn bản hiện tại thành liên quan
        /// 512
        /// </summary>
        TraLoiVanBan = 0x00000200,

        /// <summary>
        /// Quyền lấy lại văn bản đã bàn giao cho cán bộ khác
        /// 1024
        /// </summary>
        LayLaiVanBan = 0x00000400,

        /// <summary>
        /// Quyền xác nhận đã nhận bản giấy sau khi nhận bản điện tử
        /// 2048
        /// </summary>
        XacNhanBanGiao = 0x00000800,

        /// <summary>
        /// Quyền xác nhận cán bộ đang đăng nhập là người sẽ xử lý văn bản. Loại bỏ văn bản khỏi danh sách đang giữ của cán bộ khác.
        /// 4096
        /// </summary>
        XacNhanXuLy = 0x00001000,

        /// <summary>
        /// Quyền được yêu cầu công dân bổ sung thêm hồ sơ trước khi tiếp tục xử lý
        /// 8192
        /// </summary>
        YeuCauBoSung = 0x00002000,

        /// <summary>
        /// Quyền tiếp nhận bộ hồ sơ bổ sung của dân
        /// 16384
        /// </summary>
        TiepNhanBoSung = 0x00004000,

        /// <summary>
        /// Quyền cho ý kiến duyệt đồng ý hay từ chối xử lý tiếp văn bản
        /// 32768
        /// </summary>
        KiDuyet = 0x00008000,

        /// <summary>
        /// Quyền trả kết quả xử lý cho dân
        /// 65536
        /// </summary>
        TraKetQua = 0x00010000,

        /// <summary>
        /// Quyền gia hạn thêm thời gian xử lý cho văn bản
        /// 131072
        /// </summary>
        XinGiaHanXuLy = 0x00020000,

        /// <summary>
        /// Quyền dừng thời gian xử lý một văn bản
        /// 262144
        /// </summary>
        DungXuLy = 0x00040000,

        /// <summary>
        /// Quyền kết thúc quá trình xử lý một văn bản
        /// 524288
        /// </summary>
        KetThucXuLy = 0x00080000,

        /// <summary>
        /// Quyền hủy bỏ văn bản. Văn bản coi như là chưa từng tạo ra.
        /// 1048576
        /// </summary>
        HuyVanBan = 0x00100000,

        /// <summary>
        /// Quyền lưu sổ hồ sơ cá nhân
        /// 2097152
        /// </summary>
        LuuHoSoCaNhan = 0x00200000,

        /// <summary>
        /// Quyền lưu sổ hồ sơ quản lý cơ quan
        /// 4194304
        /// </summary>
        LuuSo = 0x00400000,

        /// <summary>
        /// Quyền phát hành văn bản
        /// 8388608
        /// </summary>
        PhatHanh = 0x00800000,

        /// <summary>
        /// Quyền cập nhật kết quả xử lý cuối
        /// 16777216
        /// </summary>
        CapNhatKetQuaXuLyCuoi = 0x01000000,

        /// <summary>
        /// Quyền lưu văn bản khi đang xử lý(edit)
        /// 33554432
        /// </summary>
        Luuvanban = 0x02000000,

        /// <summary>
        /// Quyền Trả lời ý kiến đóng góp của văn bản xin ý kiến hoặc văn bản đồng xử lý
        /// 67108864
        /// </summary>
        TraLoiYKien = 0x04000000,

        /// <summary>
        /// Quyền cho phép cấp phép đăng ký kinh doanh
        /// 134217728
        /// </summary>
        CapPhep = 0x08000000,

        /// <summary>
        /// Quyền cho phép đổi thời hạn xử lý khi phân loại
        /// 268435456
        /// </summary>
        DoiHanXuLyKhiPhanLoai = 0x10000000,

        /// <summary>
        /// Mở lại văn bản đã kết thúc
        /// 536870912
        /// </summary>
        MoLaiVanBan = 0x20000000,

        /// <summary>
        /// Hẹn tiếp công dân
        /// 1073741824
        /// </summary>
        DanhLaiSoDen = 0x40000000,

        ///// <summary>
        ///// Đánh lại số đến
        ///// 1073741824 * 2
        ///// </summary>
        //HenTiep = (uint)0x80000000,

        /// <summary>
        /// Các quyền cần kiểm tra khi xử lý context menu
        /// </summary>
        ContextPermission = HuyVanBan | CapNhatKetQuaXuLyCuoi | GuiYKien | KetThucXuLy | TiepNhanBoSung | TraKetQua | XacNhanBanGiao |
                            XacNhanXuLy | XemVanBan | XinYKien | YeuCauBoSung | KiDuyet | ThongBao | MoLaiVanBan,//| LayLaiVanBan 

        /// <summary>
        /// Các quyền cần kiểm tra khi xử lý context menu cho nhieu van ban
        /// </summary>
        ContextPermissionForMany = HuyVanBan | KetThucXuLy | TraKetQua | XacNhanBanGiao | XacNhanXuLy | YeuCauBoSung | KiDuyet | MoLaiVanBan,

        /// <summary>
        /// Các quyền cần kiểm tra trên toolbar chi tiết văn bản
        /// </summary>
        ToolbarPermission = BanGiao | HuyVanBan | CapNhatKetQuaXuLyCuoi | DungXuLy | XinGiaHanXuLy | GuiYKien | LuuHoSoCaNhan | KetThucXuLy | PhanLoai |
                            SuaVanBan | ThongBao | TiepNhanBoSung | TraKetQua | XacNhanBanGiao | XacNhanXuLy | XemVanBan | XinYKien | YeuCauBoSung |
                            KiDuyet | Luuvanban | TraLoiVanBan | CapPhep | DoiHanXuLyKhiPhanLoai | DanhLaiSoDen,
                                 //| HenTiep (qua int)

        /// <summary>
        /// Tất cả các quyền trên văn bản cần kiểm tra
        /// </summary>
        AllPermission = XemVanBan | DinhKem | SuaVanBan | GuiYKien | BanGiao | ThongBao | XinYKien | PhanLoai | TraLoiVanBan |
                        LayLaiVanBan | XacNhanBanGiao | XacNhanXuLy | YeuCauBoSung | TiepNhanBoSung | KiDuyet | TraKetQua | XinGiaHanXuLy | DungXuLy | KetThucXuLy |
                        HuyVanBan | LuuHoSoCaNhan | LuuSo | PhatHanh | CapNhatKetQuaXuLyCuoi | Luuvanban | DoiHanXuLyKhiPhanLoai,//KhoiTaoVanBan
    }
}
