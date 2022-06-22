using System;

namespace Bkav.eGovCloud.Core.Workflow
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
    /// Các quyền được phép thực thi tại một nút trên quy trình bàn giao
    /// </summary>
    [Flags]
    public enum NodePermissions
    {
        /// <summary>
        /// Quyền khởi tạo văn bản/tiếp nhận hồ sơ
        /// </summary>
        QuyenKhoiTao = 1,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Dừng xử lý
        /// </summary>
        QuyenDungXuLy = 2,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Kết thúc xử lý
        /// </summary>
        QuyenKetThucXuLy = 4,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Phân loại lại văn bản/hồ sơ để chạy trên quy trình mới
        /// </summary>
        QuyenPhanLoai = 8,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Đánh lại số đến cho văn bản đến
        /// </summary>
        QuyenDanhLaiSoDen = 16,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Thay đổi được nội dung văn bản
        /// </summary>
        QuyenThayDoiNoiDung = 32,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Lưu sổ và phát hành văn bản
        /// </summary>
        QuyenLuuSoPhatHanh = 64,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Lưu sổ và phát hành văn bản nội bộ
        /// </summary>
        QuyenLuuSoVaPhatHanhNoiBo = 128,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Yêu cầu bổ sung hồ sơ các thông tin cần xác thực thêm
        /// </summary>
        QuyenYeuCauBoSung = 256,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Tiếp nhận thông tin bổ sung cho hồ sơ từ dân
        /// </summary>
        QuyenTiepNhanBoSung = 512,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Cho ý kiến duyệt Đồng ý/Từ chối lên văn bản/hồ sơ
        /// </summary>
        QuyenKiDuyet = 1024,

        /// <summary>
        /// Quyền thực thi nghiệp vụ Trả kết quả xử lý cho dân
        /// </summary>
        QuyenTraKetQua = 2048,

        /// <summary>
        /// QUyền thực thi nghiệp vụ Cập nhật kết quả xử lý cuối cùng cho văn bản/hồ sơ
        /// </summary>
        QuyenCapNhatKetQuaXuLyCuoi = 4096,

        /// <summary>
        /// Quyền cho phép thay đổi thời hạn xử lý khi thay đổi luồng (phân loại)
        /// </summary>
        QuyenChoPhepCapNhatHanXuLyKhiThayDoiLuong = 8192,

        /// <summary>
        /// Quyền cho phép cán bộ tiếp nhận có thể nhìn thấy hồ sơ của nhau
        /// </summary>
        QuyenChoPhepCanBoTiepNhanNhinThayHoSoCuaNhau = 16384,

        /// <summary>
        /// Quyền cho phép cán bộ khởi tạo lấy số ký hiệu luôn (với HSMC là mặc định).
        /// </summary>
        QuyenCapSoKyHieuKhiKhoiTao = 32768,

        /// <summary>
        /// Quyền cho phép cán bộ trả kết quả đặt lịch tiếp công dân
        /// </summary>
        QuyenDatLichTiepCongDanKhiKetThucHoSo = 65572,

        /// <summary>
        /// Quyền cấp số trước
        /// </summary>
        QuyenCapSoTruoc = 131144
    }
}
