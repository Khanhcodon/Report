using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SmsBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : BLL tương ứng với bảng Sms trong CSDL
    /// </summary>
    public class SmsBll : ServiceBase
    {
        private readonly IRepository<Sms> _smsRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="generalSettings"></param>
        public SmsBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _smsRepository = Context.GetRepository<Sms>();
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Tạo mới đối tượng gủi sms 
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Sms entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Sms in null.");

            _smsRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới nhiều đối tượng gửi sms
        /// </summary>
        /// <param name="entities"></param>
        public void Create(IEnumerable<Sms> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException("Sms in null.");

            foreach (var entity in entities)
            {
                _smsRepository.Create(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật đối tượng sms
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Sms entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Sms in null.");

            Context.SaveChanges();
        }

        /// <summary>
        /// Cạp nhật nhiều đối tượng sms
        /// </summary>
        /// <param name="entities"></param>
        public void Update(IEnumerable<Sms> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException("Sms in null.");

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 đối tượng sms
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Sms entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Sms in null.");

            _smsRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nhiều đối tượng sms
        /// </summary>
        /// <param name="entities"></param>
        public void Delete(IEnumerable<Sms> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException("Sms in null.");

            foreach (var entity in entities)
            {
                _smsRepository.Delete(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy đối tượng sms theo id
        /// </summary>
        /// <param name="smsId">Id của đối tượng sms</param>
        /// <returns></returns>
        public Sms Get(int smsId)
        {
            return _smsRepository.Get(smsId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsId"></param>
        /// <returns></returns>
        public Sms Get(Guid smsId)
        {
            return _smsRepository.Get(smsId);
        }

        /// <summary>
        /// Tr
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Sms> GetPendings()
        {
            var fromDate = DateTime.Now.Subtract(TimeSpan.FromDays(3));
            return _smsRepository.Gets(false, s => !s.IsSent && !s.HasSendFail && s.DateCreated >= fromDate);
        }

        /// <summary>
        /// Lấy đối tượng sms theo điều kiện
        /// </summary>
        /// <param name="isReadOnly">Đối tượng này chỉ đọc hay có thể vừa đọc vừa ghi</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public Sms Get(bool isReadOnly = false, Expression<Func<Sms, bool>> spec = null)
        {
            return _smsRepository.Get(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy danh sách đôi tượng sms(chỉ đọc) theo điều kiện truyền vào 
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public IEnumerable<Sms> GetsReadOnly(Expression<Func<Sms, bool>> spec = null)
        {
            return _smsRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy danh sách đối tượng sms chỉ có thể đọc ghi theo điều kiện truyền vào
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Sms> Gets(Expression<Func<Sms, bool>> spec = null)
        {
            return _smsRepository.Gets(false, spec);
        }

        /// <summary>
        /// Lấy tất cả danh sách cơ quan ngoài. Kết quả sẽ được ánh xạ sang 1 dạng khác do người dùng cung cấp
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Sms, T>> projector)
        {
            return _smsRepository.GetsAs(projector, null);
        }

        /// <summary>
        /// Lấy ra tất cả nhật ký phù hợp với điều kiện truyền vào. 
        /// Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..Kết quả chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="from">Nhật ký được tạo ra từ ngày; để null sẽ bỏ qua điều kiện này</param>
        /// <param name="to">Nhật ký được tạo ra đến ngày; để null sẽ bỏ qua điều kiện này</param>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isSent"></param>
        /// <param name="findText"></param>
        /// <returns>Danh sách nhật ký</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(out int totalRecords, Expression<Func<Sms, TOutput>> projector,
            int currentPage = 1,
            int? pageSize = null, string sortBy = "", bool isDescending = true,
            DateTime? from = null, DateTime? to = null, bool? isSent = null, string findText = null)
        {

            Expression<Func<Sms, bool>> spec =
               p => (!from.HasValue || (from.HasValue && from.Value < p.DateCreated))
               && (!to.HasValue || (to.HasValue && to.Value >= p.DateCreated))
               && (!isSent.HasValue || (isSent.HasValue && isSent.Value == p.IsSent))
               && (string.IsNullOrEmpty(findText) || ((p.PhoneNumber.Contains(findText) || p.Message.Contains(findText))));


            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _smsRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Sms>(isDescending, sortBy);

            return _smsRepository.GetsAs(projector, spec, sort, Context.Filters.Page<Sms>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Trả về kết quả xác định tồn tại sms theo biểu thức không
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<Sms, bool>> spec)
        {
            return _smsRepository.Exist(spec);
        }
    }
}
