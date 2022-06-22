using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : entityBll - public - DAL
    /// Access Modifiers: 
    /// Create Date : 23082018
    /// Author      : Dungnvl
    /// Description : Bll tương ứng với bảng StoreDoc trong CSDL
    /// </summary>
    public class StoreDocBll : ServiceBase
    {
        private readonly IRepository<StoreDoc> _storeDocRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        public StoreDocBll(IDbCustomerContext context)
            : base(context)
        {
            _storeDocRepository = Context.GetRepository<StoreDoc>();
        }

        /// <summary>
        /// Lấy ra nội dung sổ hồ sơ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public StoreDoc Get(Expression<Func<StoreDoc, bool>> spec = null)
        {
            return _storeDocRepository.Get(false, spec);
        }

        /// <summary>
        /// Lấy ra danh sách nội dung sổ hồ sơ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public IEnumerable<StoreDoc> Gets(Expression<Func<StoreDoc, bool>> spec = null)
        {
            return _storeDocRepository.Gets(false, spec);
        }

        /// <summary>
        /// Thêm mới nội dung sổ hồ sơ
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Create(StoreDoc entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _storeDocRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới nội dung sổ hồ sơ
        /// </summary>
        /// <param name="entitys">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Create(IEnumerable<StoreDoc> entitys)
        {
            if (entitys == null)
            {
                throw new ArgumentNullException("entitys");
            }
            //  Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entity in entitys)
            {
                //Create(entity);
                _storeDocRepository.Create(entity);
            }
            Context.SaveChanges();
            //  Context.Configuration.AutoDetectChangesEnabled = true;
        }

        /// <summary>
        /// Cập nhật nội dung sổ hồ sơ
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(StoreDoc entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nội dung sổ hồ sơ
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Delete(StoreDoc entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _storeDocRepository.Delete(entity);
            Context.SaveChanges();
        }

		/// <summary>
		/// Kiểm tra record tồn tại theo điều kiện và trả về kết quả.
		/// </summary>
		/// <param name="spec"></param>
		/// <returns></returns>
		public bool Exist(Expression<Func<StoreDoc, bool>> spec)
		{
			return _storeDocRepository.Exist(spec);
		}
    }
}
