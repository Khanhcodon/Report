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
    /// Class : DocFieldtDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocFieldDal
    /// Create Date : 080912
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng DocField trong CSDL
    /// </summary>
    public class DocFieldDal : DataAccessBase, IDocFieldDal
    {
        private readonly IRepository<DocField> _docFieldRepository;
        /// <summary>
        /// Khởi tạo class <see cref="DocFieldDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public DocFieldDal(IDbCustomerContext context)
            : base(context)
        {
            _docFieldRepository = Context.GetRepository<DocField>();
        }

        #pragma warning disable 1591

        public IEnumerable<DocField> Gets(Expression<Func<DocField, bool>> spec = null, Func<IQueryable<DocField>, IQueryable<DocField>> preFilter = null, params Func<IQueryable<DocField>, IQueryable<DocField>>[] postFilters)
        {
            return _docFieldRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<T> GetsAs<T>(Expression<Func<DocField, T>> projector = null, Expression<Func<DocField, bool>> spec = null, Func<IQueryable<DocField>, IQueryable<DocField>> preFilter = null, params Func<IQueryable<DocField>, IQueryable<DocField>>[] postFilters)
        {
            return _docFieldRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public DocField Get(int id)
        {
            return _docFieldRepository.One(l => l.DocFieldId == id);
        }

        public void Create(DocField docField)
        {
            _docFieldRepository.Create(docField);
        }

        public void Update(DocField docField)
        {
            _docFieldRepository.Update(docField);
        }

        public void Delete(DocField docField)
        {
            _docFieldRepository.Delete(docField);
        }

        public void Delete(IList<DocField> docFields)
        {
            foreach (var docField in docFields)
            {
                _docFieldRepository.Delete(docField, false);
            }
            Context.SaveChanges();
        }

        public bool Exist(Expression<Func<DocField, bool>> spec)
        {
            return _docFieldRepository.Any(spec);
        }

        public int Count(Expression<Func<DocField, bool>> spec = null)
        {
            return _docFieldRepository.Count(spec);
        }
    }
}
