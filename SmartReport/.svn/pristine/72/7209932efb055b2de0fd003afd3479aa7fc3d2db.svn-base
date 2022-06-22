using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Customer.Specification;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : DailyProcessBll - public - BLL</para>
    /// <para>Create Date : 241113</para>
    /// <para>Author      : TienBv</para>
    /// <para>Description : BLL tương ứng với bảng DailyProcess trong CSDL</para>
    /// </summary>
    public class DailyProcessBll : ServiceBase
    {
        private readonly IRepository<DailyProcess> _processrRepository;

        /// <summary>
        /// Khởi tạo class <see cref="DailyProcessBll"/>.
        /// <param name="context">Context</param>
        /// </summary>
        public DailyProcessBll(IDbCustomerContext context)
            : base(context)
        {
            _processrRepository = Context.GetRepository<DailyProcess>();
        }

        /// <summary>
        /// Trả về danh sách các xử lý trong ngày của cán bộ. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="type">Loại xử lý</param>
        /// <param name="count">Số lượng hồ sơ đc lấy ra ( gán 0 để lấy tất cả)</param>
        /// <param name="from">Từ thời gian</param>
        /// <param name="to">Đến thời gian</param>
        /// <returns></returns>
        public IEnumerable<DailyProcess> Gets(int userId, DocumentProcessType type, int count, DateTime from, DateTime to)
        {
            if (userId < 0)
            {
                throw new ArgumentException("userId không hợp lệ");
            }
            Expression<Func<DailyProcess, bool>> spec;
            switch (type)
            {
                case DocumentProcessType.TiepNhan:
                    spec = DailyProcessQuery.IsCreated(userId, from, to);
                    break;
                case DocumentProcessType.BanGiao:
                    spec = DailyProcessQuery.IsTransfered(userId, from, to);
                    break;
                case DocumentProcessType.KyDuyet:
                    spec = DailyProcessQuery.IsApproved(userId, from, to);
                    break;
                case DocumentProcessType.TraKetQua:
                    spec = DailyProcessQuery.IsReturned(userId, from, to);
                    break;
                case DocumentProcessType.TiepNhanBoSung:
                    spec = DailyProcessQuery.IsSupplemented(userId, from, to);
                    break;
                default:
                    spec = DailyProcessQuery.WithUserId(userId, from, to);
                    break;
            }
            var sort = Context.Filters.CreateSort<DailyProcess>(false, "ProcessType", "DateCreated");
            var page = count == 0 ? null : Context.Filters.Page<DailyProcess>(1, count);
            var docs = _processrRepository.GetsReadOnly(spec, sort, page);//.OrderBy(d => d.ProcessType).ThenByDescending(d => d.DateCreated);
            return docs;
        }

        /// <summary>
        /// Trả về các xử lý hàng ngày của hồ sơ theo loại và người xử lý
        /// </summary>
        /// <param name="userId">Người xử lý</param>
        /// <param name="docCopyId">Hồ sơ cần lấy</param>
        /// <param name="type">Loại</param>
        /// <param name="isReadOnly">Giá trị xác định kết quả trả về chỉ để đọc hay không</param>
        /// <returns></returns>
        public IEnumerable<DailyProcess> Gets(int userId, int docCopyId, DocumentProcessType type, bool isReadOnly = true)
        {
            return _processrRepository.Gets(isReadOnly, d => d.UserId == userId && d.DocumentCopyId == docCopyId && d.ProcessType == (int)type);
        }

        /// <summary>
        /// Trả về tất cả các xử lý hằng ngày của một hồ sơ
        /// </summary>
        /// <param name="documentCopyId">Hồ sơ cần xem</param>
        /// <param name="from">Lấy từ một thời điểm nào đó, để null để lấy tất</param>
        /// <returns>Danh sách tất cả các xử lý hằng ngày của một hồ sơ, kết quả chỉ để đọc</returns>
        public IEnumerable<DailyProcess> Gets(int documentCopyId, DateTime? from = null)
        {
            return _processrRepository.Gets(true, d => d.DocumentCopyId == documentCopyId && (!from.HasValue || d.DateCreated >= from.Value));
        }

        /// <summary>
        /// Trả về tất cả các xử lý hằng ngày của một danh sách hồ sơ
        /// </summary>
        /// <param name="documentCopyIds">Hồ sơ cần xem</param>
        /// <param name="from">Lấy từ một thời điểm nào đó, để null để lấy tất</param>
        /// <returns>Danh sách tất cả các xử lý hằng ngày của một hồ sơ, kết quả chỉ để đọc</returns>
        public IEnumerable<DailyProcess> Gets(List<int> documentCopyIds, DateTime? from = null)
        {
            return _processrRepository.Gets(true, d => documentCopyIds.Contains(d.DocumentCopyId) && (!from.HasValue || d.DateCreated >= from.Value));
        }

        /// <summary>
        /// Xóa một xử lý trong ngày
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(DailyProcess entity)
        {
            _processrRepository.Delete(entity);
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        public void Create(DailyProcess entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!_processrRepository.Exist(DailyProcessQuery.Exists(entity.UserId, entity.DocumentCopyId, entity.ProcessType)))
            {
                _processrRepository.Create(entity);
                Context.SaveChanges();
            }
        }
    }
}
