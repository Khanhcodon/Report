using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : TranferTypeBll - public - BLL</para>
    /// <para>Create Date : 221113</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng TransferType trong CSDL</para>
    /// </summary>
    public class TransferTypeBll : ServiceBase
    {
        private readonly IRepository<TransferType> _transfertypeRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="TransferTypeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="resourceService"></param>
        public TransferTypeBll(IDbCustomerContext context, AdminGeneralSettings generalSettings, ResourceBll resourceService)
            : base(context)
        {
            _transfertypeRepository = Context.GetRepository<TransferType>();
            _generalSettings = generalSettings;
            _resourceService = resourceService;
        }

        /// <summary>
        /// Xóa 1 hình thức chuyển
        /// </summary>
        /// <param name="transfertype">Thực thể hình thức chuyển</param>
        public void Delete(TransferType transfertype)
        {
            if (transfertype == null)
            {
                throw new ArgumentNullException("transfertype");
            }
            _transfertypeRepository.Delete(transfertype);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra một hình thức chuyển
        /// </summary>
        /// <param name="transfertypeId">Id hình thức chuyển</param>
        /// <returns>Entity hình thức chuyển</returns>
        public TransferType Get(int transfertypeId)
        {
            TransferType transfertype = null;
            if (transfertypeId > 0)
            {
                transfertype = _transfertypeRepository.Get(transfertypeId);
            }
            return transfertype;
        }

        /// <summary>
        /// Thêm mới hình thức chuyển
        /// </summary>
        /// <param name="transfertype">Thực thể hình thức chuyển</param>
        /// <returns></returns>
        public void Create(TransferType transfertype)
        {
            if (transfertype == null)
            {
                throw new ArgumentNullException("transfertype");
            }
            if (_transfertypeRepository.Exist(TransferTypeQuery.WithName(transfertype.TransferTypeName)))
            {
                throw new EgovException(string.Format("Hình thức chuyển ({0}) đã tồn tại!", transfertype.TransferTypeName));
            }
            _transfertypeRepository.Create(transfertype);
            Context.SaveChanges();
        }

        /// <summary>
        ///  Thêm mới hình thức chuyển
        /// </summary>
        /// <param name="transfertypes">Thực thể hình thức chuyển</param>
        /// <param name="ignoreExist">True: Bỏ qua nếu lĩnh vực đã tồn tại; False: Validate nếu đã tồn tại lĩnh vực</param>
        public void Create(IEnumerable<TransferType> transfertypes, bool ignoreExist)
        {
            if (transfertypes == null || !transfertypes.Any())
            {
                throw new ArgumentNullException("transfertypes");
            }

            var names = transfertypes.Select(x => x.TransferTypeName);
            var exist = _transfertypeRepository.GetsAs(p => p.TransferTypeName, p => names.Contains(p.TransferTypeName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == transfertypes.Count())
                {
                    throw new EgovException(_resourceService.GetResource("TransferType.Create.Exist"));
                }

                var list = transfertypes.Where(p => !exist.Contains(p.TransferTypeName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("TransferType.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(transfertypes);
            }
        }

        private void Create(IEnumerable<TransferType> transfertypes)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var transfertype in transfertypes)
            {
                _transfertypeRepository.Create(transfertype);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin hình thức chuyển
        /// </summary>
        /// <param name="transferType">Entity hình thức chuyển</param>
        /// <param name="oldTransferTypeName">Tên hình thức chuyển trước khi cập nhật</param>
        public void Update(TransferType transferType, string oldTransferTypeName)
        {
            if (transferType == null)
            {
                throw new ArgumentNullException("transferType");
            }
            if (_transfertypeRepository.Exist(TransferTypeQuery.WithName(transferType.TransferTypeName.Trim()).And(r => r.TransferTypeName.ToLower() != oldTransferTypeName.Trim().ToLower())))
            {
                throw new EgovException(string.Format("Hình thức chuyển ({0}) đã tồn tại!", transferType.TransferTypeName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các hình thức chuyển có phân trang
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="transfertypename">tên hình thức chuyển (dùng để tìm kiếm) Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các hình thức chuyển có tên gần giống với tên truyền vào</param>
        /// <returns>Danh sách các hình thức chuyển đã được phân trang</returns>
        public IEnumerable<TransferType> Gets(out int totalRecords,
                                                        int currentPage = 1,
                                                        int? pageSize = null,
                                                        string sortBy = "",
                                                        bool isDescending = false,
                                                        string transfertypename = "")
        {
            var spec = !string.IsNullOrWhiteSpace(transfertypename)
                        ? TransferTypeQuery.ContainsName(transfertypename)
                        : null;
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _transfertypeRepository.Count(spec);
            var sort = Context.Filters.CreateSort<TransferType>(isDescending, sortBy);
            return _transfertypeRepository.GetsReadOnly(spec, sort, Context.Filters.Page<TransferType>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Lấy ra tất cả các hình thức chuyển. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các hình thức chuyển đã được phân trang</returns>
        public IEnumerable<TransferType> Gets(Expression<Func<TransferType, bool>> spec = null)
        {
            return _transfertypeRepository.GetsReadOnly(spec);
        }
    }
}
