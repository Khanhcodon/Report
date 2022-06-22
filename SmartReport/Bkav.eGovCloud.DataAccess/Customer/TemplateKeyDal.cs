using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : TemplateKeyDal - public - Dal </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 13</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý biểu mẫu động </para>
    /// <para> ( TienBV@bkav.com - 13) </para>
    /// </summary>
    public class TemplateKeyDal : DataAccessBase, ITemplateKeyDal
    {
        private readonly IRepository<TemplateKey> _templateKeyRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns></returns>
        public TemplateKeyDal(IDbCustomerContext context)
            : base(context)
        {
            _templateKeyRepository = context.GetRepository<TemplateKey>();
        }

#pragma warning disable 1591

        public IEnumerable<Entities.Customer.TemplateKey> Gets(Expression<Func<TemplateKey, bool>> spec = null,
                                    Func<IQueryable<TemplateKey>, IQueryable<TemplateKey>> preFilter = null,
                                    params Func<IQueryable<TemplateKey>, IQueryable<TemplateKey>>[] postFilters)
        {
            return _templateKeyRepository.Find(spec, preFilter, postFilters);
        }

        public Entities.Customer.TemplateKey Get(int id)
        {
            return _templateKeyRepository.One(id);
        }

        public void Delelte(Entities.Customer.TemplateKey entity)
        {
            _templateKeyRepository.Delete(entity);
        }

        public void Update(Entities.Customer.TemplateKey entity)
        {
            _templateKeyRepository.Update(entity);
        }

        public int Count(Expression<Func<TemplateKey, bool>> spec = null)
        {
            return _templateKeyRepository.Count(spec);
        }

        public void Create(TemplateKey entity)
        {
            _templateKeyRepository.Create(entity);
        }

        public bool Exist(Expression<Func<TemplateKey, bool>> spec)
        {
            return _templateKeyRepository.Any(spec);
        }

        public TemplateKey Get(string keyCode)
        {
            return _templateKeyRepository.One(k => k.Code.Equals(keyCode) && k.IsActive);
        }

        public IEnumerable<IDictionary<string, object>> GetValue(TemplateKey templateKey, params object[] parameters)
        {
            IEnumerable<IDictionary<string, object>> result;
            var query = templateKey.Sql;
            result = Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }
    }
}