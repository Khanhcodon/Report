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
    /// Class : DocFinishDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocFinishDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocFinish trong CSDL
    /// </summary>
    public class DocFinishDal : DataAccessBase, IDocFinishDal
    {
        private readonly IRepository<DocFinish> _docFinishRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocFinishDal(IDbCustomerContext context) : base(context)
        {
            _docFinishRepository = Context.GetRepository<DocFinish>();
        }

        #pragma warning disable 1591
        public IEnumerable<DocFinish> Gets(Expression<Func<DocFinish, bool>> spec = null, Func<IQueryable<DocFinish>, IQueryable<DocFinish>> preFilter = null, params Func<IQueryable<DocFinish>, IQueryable<DocFinish>>[] postFilters)
        {
            return _docFinishRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocFinish, TOutput>> projector, Expression<Func<DocFinish, bool>> spec = null)
        {
            return _docFinishRepository.FindAs(projector, spec);
        }

        public DocFinish Get(int id)
        {
            return _docFinishRepository.One(l => l.DocFinishId == id);
        }

        public bool Exist(Expression<Func<DocFinish, bool>> spec)
        {
            return _docFinishRepository.Any(spec);
        }

        public void Create(DocFinish entity)
        {
            _docFinishRepository.Create(entity);
        }

        public void Create(IEnumerable<DocFinish> entities)
        {
            foreach (var entity in entities)
            {
                _docFinishRepository.Create(entity, false);
            }
            Context.SaveChanges();
        }

        public void Delete(DocFinish docFinish)
        {
            _docFinishRepository.Delete(docFinish);
        }

        public void Delete(IEnumerable<DocFinish> docFinishs)
        {
            foreach (var docFinish in docFinishs)
            {
                _docFinishRepository.Delete(docFinish, false);
            }
            Context.SaveChanges();
        }

        public void Update(DocFinish entity)
        {
            _docFinishRepository.Update(entity);
        }

        public DocFinish Get(Expression<Func<DocFinish, bool>> spec)
        {
            return _docFinishRepository.One(spec);
        }

        public IQueryable<DocFinish> Raw()
        {
            return _docFinishRepository.Raw;
        }
    }
}
