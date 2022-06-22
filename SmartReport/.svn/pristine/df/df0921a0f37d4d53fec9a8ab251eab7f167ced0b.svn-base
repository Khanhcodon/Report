using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocTimelineDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Modify Date : 240912
    /// Editor      : GiangPN
    /// Bổ sung các phương thức thêm, sửa, xóa.
    /// Description : DAL tương ứng với bảng DocType trong CSDL
    /// </summary>
    public class DocTypeDal : DataAccessBase, IDocTypeDal
    {
        private readonly IRepository<DocType> _doctypeRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocTypeDal(IDbCustomerContext context)
            : base(context)
        {
            _doctypeRepository = Context.GetRepository<DocType>();
        }

        #pragma warning disable 1591

        public IEnumerable<DocType> Gets(Expression<Func<DocType, bool>> spec = null,
                                            Func<IQueryable<DocType>, IQueryable<DocType>> preFilter = null,
                                            params Func<IQueryable<DocType>, IQueryable<DocType>>[] postFilters)
        {
            return _doctypeRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocType, TOutput>> projector, Expression<Func<DocType, bool>> spec = null)
        {
            return _doctypeRepository.FindAs(projector, spec);
        }

        public DocType Get(Guid docTypeId)
        {
            return _doctypeRepository.One(l => l.DocTypeId == docTypeId);
        }

        public DocType Get(int categoryId)
        {
            return _doctypeRepository.One(l => l.CategoryId == categoryId);
        }

        public void Create(DocType doc)
        {
            _doctypeRepository.Create(doc);
        }

        public void Update(DocType doc)
        {
            _doctypeRepository.Update(doc);
        }

        public void Delete(DocType doc)
        {
            _doctypeRepository.Delete(doc);
        }

        public bool Exist(Expression<Func<DocType, bool>> spec)
        {
            return _doctypeRepository.Any(spec);
        }

        public int Count(Expression<Func<DocType, bool>> spec = null)
        {
            return _doctypeRepository.Count(spec);
        }
    }
}
