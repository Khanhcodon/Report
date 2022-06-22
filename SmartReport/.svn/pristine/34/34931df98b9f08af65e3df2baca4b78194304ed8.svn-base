using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ConnectionDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDomainAliasDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DomainAlias trong CSDL
    /// </summary>
    public class DomainAliasDal : DataAccessBase, IDomainAliasDal
    {
        private readonly IRepository<DomainAlias> _domainAliasRepository;

        /// <summary>
        /// Khởi tạo class <see cref="DomainAliasDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public DomainAliasDal(IDbAdminContext context) : base(context)
        {
            _domainAliasRepository = Context.GetRepository<DomainAlias>();
        }

        #pragma warning disable 1591

        public IEnumerable<DomainAlias> Gets(Expression<Func<DomainAlias, bool>> spec = null)
        {
            return _domainAliasRepository.Find(spec);
        }

        public DomainAlias Get(int id)
        {
            return _domainAliasRepository.One(d => d.DomainAliasId == id);
        }

        public DomainAlias Get(string alias)
        {
            return _domainAliasRepository.One(d => d.Alias == alias);
        }

        public void Create(DomainAlias domainAlias)
        {
            _domainAliasRepository.Create(domainAlias);
        }

        public void Update(DomainAlias domainAlias)
        {
            _domainAliasRepository.Update(domainAlias);
        }

        public void Delete(DomainAlias domainAlias)
        {
            _domainAliasRepository.Delete(domainAlias);
        }

        public bool Exist(Expression<Func<DomainAlias, bool>> spec)
        {
            return _domainAliasRepository.Any(spec);
        }
    }
}
