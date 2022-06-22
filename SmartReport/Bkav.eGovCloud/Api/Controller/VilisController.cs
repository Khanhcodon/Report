using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Api.Controller
{
    /// <summary>
    /// REST-api cho Vilis
    /// </summary>
    public class VilisController : EgovApiBaseController
    {
        private readonly DocumentCopyBll _documentCopyService;
        private readonly DocumentBll _docService;
        private readonly UserBll _userService;

        public VilisController()
        {
            _documentCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _docService = DependencyResolver.Current.GetService<DocumentBll>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
        }

        /// <summary>
        /// Cập nhật trạng thái hồ sơ từ Vilis và trả về kết quả cập nhật.
        /// </summary>
        /// <param name="eGovId">Id hồ sơ của eGov cần cập nhật</param>
        /// <param name="status">Trạng thái hồ sơ, bao gồm:
        ///     - 1: Đang xử lý.
        ///     - 2: Đã duyệt
        ///     - 3: Đã trả kết quả 
        ///     - 4: Đã kết thúc
        ///     - 5: Đã hủy
        /// </param>
        /// <param name="note">Thông tin theo trạng thái hồ sơ:
        ///     - status = 1: Họ Tên cán bộ đang giữ - Tên phòng ban giữ
        ///     - status = 2: Họ Tên cán bộ duyệt hồ sơ
        ///     - status = 3: Thông tin công dân nhận kết quả: Họ tên - số điện thoại - CMND - Địa chỉ
        ///     - status = 4: Họ Tên cán bộ kết thúc hồ sơ
        ///     - status = 5: Để trống
        /// </param>
        /// <returns>Kết quả xác nhận cập nhật thành công hay không; true - Thành công; còn lại - false</returns>
        /// <remarks>
        /// eGovId là Id của hồ sơ bên eGov.
        /// Do đó, khi lấy danh sách hồ sơ từ eGov để đồng bộ sang Vilis, phía Vilis cần lưu lại Id này ở Hồ sơ tương ứng của eGov.
        /// Khi mà gọi Service cập nhật thông tin hồ sơ lại eGov, sẽ lấy Id đó để truyền vào đây.
        /// </remarks>
        public bool CapNhatTrangThaiHoSo(string eGovId, int status, string note)
        {
            Guid docId;
            if (!Guid.TryParse(eGovId, out docId))
            {
                // Log here

                return false;
            }

            var document = _docService.Get(docId);
            var now = DateTime.Now;

            if (document == null)
            {
                // Log here

                return false;
            }

            switch (status)
            {
                case 1: // Đang xử lý
                    break;
                case 2: // Đã duyệt
                    document.IsSuccess = true;
                    document.DateSuccess = now;
                    document.SuccessNote = note;

                    break;
                case 3: // Đã trả kết quả
                    document.IsReturned = true;
                    document.DateReturned = now;
                    document.ReturnNote = note;

                    break;
                case 4: // Đã kết thúc
                    document.Status = (byte)DocumentStatus.KetThuc;
                    document.DateFinished = now;

                    break;
                case 5:
                    document.Status = (byte)DocumentStatus.LoaiBo;
                    break;
                default: break;
            }

            try
            {
                _docService.Update(document);

                return true;
            }
            catch (Exception ex)
            {
                // Log here

                return false;
            }
        }

        /// <summary>
        /// Trả về danh sách các hồ sơ tiếp nhận trong khoảng thời gian yêu cầu.
        /// </summary>
        /// <param name="from">Từ thời gian</param>
        /// <param name="to">Đến thời gian</param>
        /// <returns>Danh sách hồ sơ nếu có; còn lại - null</returns>
        public IEnumerable<DocumentVilisDto> FindHoSoTTHCCongByTime(DateTime from, DateTime to)
        {
            var result = new List<DocumentVilisDto>();

            var docs = _docService.Gets(true, d => d.DateCreated >= from && d.DateCreated <= to
                            && d.Status != (int)DocumentStatus.DuThao && d.Status != (int)DocumentStatus.LoaiBo
                            && d.IsConverted == false
                            && d.DateAppointed.HasValue
                            && d.CategoryBusinessId == 4);

            if (!docs.Any())
            {
                return null;
            }

            var userCreatedIds = docs.Select(d => d.UserCreatedId);
            var userCreateds = _userService.Gets(userCreatedIds, isActivated: true);

            foreach (var doc in docs)
            {
                var userCreated = userCreateds.SingleOrDefault(u => u.UserId == doc.UserCreatedId);
                if (userCreated == null)
                {
                    continue;
                }

                var newVilis = new DocumentVilisDto()
                {
                    Id = 0,
                    eGovId = doc.DocumentId.ToString(),
                    MaSoHoSo = doc.DocCode,
                    NgayNopHoSo = doc.DateCreated,
                    NgayNhanHoSo = doc.DateCreated,
                    NgayHenTraKetQua = doc.DateAppointed.Value,
                    TenCoQuanTiepNhan = doc.InOutPlace,
                    TenThuTucHanhChinh = doc.DocTypeName,
                    NgayTraKetQua = null,
                    NgayNopHoSoGoc = null,
                    TenCanBoTiepNhan = userCreated.FullName,
                    ChucVuCanBoTiepNhan = "Cán bộ tiếp nhận",
                    HoTenNguoiNopHoSo = doc.CitizenName,
                    DiaChiThuongTruNguoiNop = doc.Address,
                    SoDienThoaiCoDinhNguoiNop = doc.Phone,
                    TenCanBoXuLyHienThoi = userCreated.FullName,
                    TrichYeu = doc.Compendium
                };
            }

            return result.Any() ? null : result;
        }
    }
}