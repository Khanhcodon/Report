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
    /// Class : MailBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : BLL tương ứng với bảng Mail trong CSDL
    /// </summary>
    public class MailBll : ServiceBase
    {
        private readonly IRepository<Mail> _mailRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="generalSettings"></param>
        public MailBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _mailRepository = Context.GetRepository<Mail>();
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Tao moi 
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Mail entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Mail in null.");
            }
            _mailRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tao moi 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Mail entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Mail in null.");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Mail entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Mail in null.");
            }
            _mailRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void Delete(IEnumerable<Mail> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("Mail in null.");
            }
            foreach (var entity in entities)
            {
                _mailRepository.Delete(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsId"></param>
        /// <returns></returns>
        public Mail Get(int smsId)
        {
            return _mailRepository.Get(smsId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReadOnly"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public Mail Get(bool isReadOnly = false, Expression<Func<Mail, bool>> spec = null)
        {
            return _mailRepository.Get(isReadOnly, spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mail> GetsReadOnly(Expression<Func<Mail, bool>> spec = null)
        {
            return _mailRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mail> Gets(Expression<Func<Mail, bool>> spec = null)
        {
            return _mailRepository.Gets(false, spec);
        }

        /// <summary>
        /// Lấy tất cả danh sách cơ quan ngoài. Kết quả sẽ được ánh xạ sang 1 dạng khác do người dùng cung cấp
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Mail, T>> projector)
        {
            return _mailRepository.GetsAs(projector, null);
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
        public IEnumerable<TOutput> GetsAs<TOutput>(out int totalRecords, Expression<Func<Mail, TOutput>> projector, int currentPage = 1,
                                                int? pageSize = null, string sortBy = "", bool isDescending = true,
                                                DateTime? from = null, DateTime? to = null, bool? isSent = null, string findText = null)
        {
            Expression<Func<Mail, bool>> spec =
                p => (!from.HasValue || (from.HasValue && from.Value < p.DateCreated))
                && (!to.HasValue || (to.HasValue && to.Value >= p.DateCreated))
                && (!isSent.HasValue || (isSent.HasValue && isSent.Value == p.IsSent))
                && (string.IsNullOrEmpty(findText) || ((p.SendTo.Contains(findText) || p.Subject.Contains(findText))));

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _mailRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Mail>(isDescending, sortBy);

            return _mailRepository.GetsAs(projector, spec, sort, Context.Filters.Page<Mail>(currentPage, pageSize.Value));
        }
    }
}
