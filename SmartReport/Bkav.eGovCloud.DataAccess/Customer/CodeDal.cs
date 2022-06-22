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
    /// Class : CodeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ICodeDal
    /// Create Date : 190912
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Code trong CSDL
    /// </summary>
    public class CodeDal : DataAccessBase, ICodeDal
    {
        private readonly IRepository<Code> _codeRepository; 
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public CodeDal(IDbCustomerContext context)
            : base(context)
        {
            _codeRepository = Context.GetRepository<Code>();
        }

        #pragma warning disable 1591
        public IEnumerable<Code> Gets(Expression<Func<Code, bool>> spec = null, Func<IQueryable<Code>, IQueryable<Code>> preFilter = null, params Func<IQueryable<Code>, IQueryable<Code>>[] postFilters)
        {
            return _codeRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Code, TOutput>> projector, Expression<Func<Code, bool>> spec = null)
        {
            return _codeRepository.FindAs(projector, spec);
        }

        public Code Get(int id)
        {
            return _codeRepository.One(a => a.CodeId == id);
        }

        public void Create(Code code)
        {
            _codeRepository.Create(code);
        }

        public void Update(Code code)
        {
            _codeRepository.Update(code);
        }

        public void Delete(Code code)
        {
            _codeRepository.Delete(code);
        }

        public bool Exist(Expression<Func<Code, bool>> spec)
        {
            return _codeRepository.Any(spec);
        }

        public int Count(Expression<Func<Code, bool>> spec = null)
        {
            return _codeRepository.Count(spec);
        }
    }
}
