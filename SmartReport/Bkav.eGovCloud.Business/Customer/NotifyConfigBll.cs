using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
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
    public class NotifyConfigBll : ServiceBase
    {
        private readonly IRepository<NotifyConfig> _notifyConfigRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public NotifyConfigBll(IDbCustomerContext context)
            : base(context)
        {
            _notifyConfigRepository = Context.GetRepository<NotifyConfig>();
        }

        /// <summary>
        /// Tạo mới đối tượng gủi NotifyConfig 
        /// </summary>
        /// <param name="entity"></param>
        public void Create(NotifyConfig entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("NotifyConfig in null.");
            }

            _notifyConfigRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới nhiều đối tượng gửi NotifyConfig
        /// </summary>
        /// <param name="entities"></param>
        public void Create(IEnumerable<NotifyConfig> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("NotifyConfig in null.");
            }

            foreach (var entity in entities)
            {
                _notifyConfigRepository.Create(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật đối tượng NotifyConfig
        /// </summary>
        /// <param name="entity"></param>
        public void Update(NotifyConfig entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("NotifyConfig in null.");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cạp nhật nhiều đối tượng NotifyConfig
        /// </summary>
        /// <param name="entities"></param>
        public void Update(IEnumerable<NotifyConfig> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("NotifyConfig in null.");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 đối tượng NotifyConfig
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(NotifyConfig entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("NotifyConfig in null.");
            }

            _notifyConfigRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nhiều đối tượng NotifyConfig
        /// </summary>
        /// <param name="entities"></param>
        public void Delete(IEnumerable<NotifyConfig> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("NotifyConfig in null.");
            }

            foreach (var entity in entities)
            {
                _notifyConfigRepository.Delete(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy đối tượng NotifyConfig theo id
        /// </summary>
        /// <param name="NotifyConfigId">Id của đối tượng NotifyConfig</param>
        /// <returns></returns>
        public NotifyConfig Get(int NotifyConfigId)
        {
            return _notifyConfigRepository.Get(NotifyConfigId);
        }

        /// <summary>
        /// Lấy đối tượng NotifyConfig theo điều kiện
        /// </summary>
        /// <param name="isReadOnly">Đối tượng này chỉ đọc hay có thể vừa đọc vừa ghi</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public NotifyConfig Get(Expression<Func<NotifyConfig, bool>> spec = null, bool isReadOnly = false)
        {
            return _notifyConfigRepository.Get(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy danh sách đối tượng NotifyConfig chỉ có thể đọc ghi theo điều kiện truyền vào
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<NotifyConfig> Gets(Expression<Func<NotifyConfig, bool>> spec = null)
        {
            return _notifyConfigRepository.Gets(false, spec);
        }
    }
}
