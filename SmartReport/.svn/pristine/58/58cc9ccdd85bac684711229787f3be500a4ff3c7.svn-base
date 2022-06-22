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
    /// Class : LuceneBll - public - DAL
    /// Access Modifiers: 
    /// Create Date : 280313
    /// Author      : TrungVH
    /// Description : Bll tương ứng với bảng Lucene trong CSDL
    /// </summary>
    public class LuceneBll : ServiceBase
    {
        private readonly IRepository<Lucene> _luceneRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        public LuceneBll(IDbCustomerContext context)
            : base(context)
        {
            _luceneRepository = Context.GetRepository<Lucene>();
        }

        /// <summary>
        /// Lấy ra nội dung cần đánh index phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public Lucene Get(Expression<Func<Lucene, bool>> spec = null)
        {
            return _luceneRepository.Get(false, spec);
        }

        /// <summary>
        /// Lấy ra danh sách nội dung cần đánh index phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public IEnumerable<Lucene> Gets(Expression<Func<Lucene, bool>> spec = null)
        {
            return _luceneRepository.Gets(false, spec);
        }

        /// <summary>
        /// Thêm mới nội dung cần đánh index
        /// </summary>
        /// <param name="lucene">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Create(Lucene lucene)
        {
            if (lucene == null)
            {
                throw new ArgumentNullException("lucene");
            }
            _luceneRepository.Create(lucene);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới nội dung cần đánh index
        /// </summary>
        /// <param name="lucenes">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Create(IEnumerable<Lucene> lucenes)
        {
            if (lucenes == null)
            {
                throw new ArgumentNullException("lucenes");
            }
            //  Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var lucene in lucenes)
            {
                //Create(lucene);
                _luceneRepository.Create(lucene);
            }
            Context.SaveChanges();
            //  Context.Configuration.AutoDetectChangesEnabled = true;
        }

        /// <summary>
        /// Cập nhật nội dung cần đánh index
        /// </summary>
        /// <param name="lucene">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(Lucene lucene)
        {
            if (lucene == null)
            {
                throw new ArgumentNullException("lucene");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nội dung cần đánh index
        /// </summary>
        /// <param name="lucene">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Delete(Lucene lucene)
        {
            if (lucene == null)
            {
                throw new ArgumentNullException("lucene");
            }
            _luceneRepository.Delete(lucene);
            Context.SaveChanges();
        }
    }
}
