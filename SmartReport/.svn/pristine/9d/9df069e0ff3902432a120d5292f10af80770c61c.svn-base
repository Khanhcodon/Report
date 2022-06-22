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
    /// Class : DocTypeFormDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IIncreaseDal
    /// Create Date : 081013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng DocTypeForm trong CSDL
    /// </summary>
    public class DocTypeFormDal : DataAccessBase, IDocTypeFormDal
    {
        private readonly IRepository<DocTypeForm> _doctypeformRepository;
        /// <summary>
        /// Khởi tạo class <see cref="FormGroupDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public DocTypeFormDal(IDbCustomerContext context)
            : base(context)
        {
            _doctypeformRepository = Context.GetRepository<DocTypeForm>();
        }

#pragma warning disable 1591
        public IEnumerable<DocTypeForm> Gets(Expression<Func<DocTypeForm, bool>> spec = null, Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>> preFilter = null, params Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>>[] postFilters)
        {
            return _doctypeformRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocTypeForm, TOutput>> projector, Expression<Func<DocTypeForm, bool>> spec = null, Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>> preFilter = null,
            params Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>>[] postFilters)
        {
            return _doctypeformRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public void Create(DocTypeForm docTypeForm)
        {
            _doctypeformRepository.Create(docTypeForm);
        }

        public void Delete(DocTypeForm docTypeForm)
        {
            _doctypeformRepository.Delete(docTypeForm);
        }

        public void Update(DocTypeForm docTypeForm)
        {
            _doctypeformRepository.Update(docTypeForm);
        }

        public DocTypeForm Get(int id)
        {
            return _doctypeformRepository.One(id);
        }
    }
}
