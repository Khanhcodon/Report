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
    public class TemplateDal : DataAccessBase, ITemplateDal
    {
        private readonly IRepository<Template> _templateRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns></returns>
        public TemplateDal(IDbCustomerContext context)
            : base(context)
        {
            _templateRepository = context.GetRepository<Template>();
        }
#pragma warning disable 1591

        public IEnumerable<Entities.Customer.Template> Gets(Expression<Func<Template, bool>> spec = null,
                                    Func<IQueryable<Template>, IQueryable<Template>> preFilter = null,
                                    params Func<IQueryable<Template>, IQueryable<Template>>[] postFilters)
        {
            return _templateRepository.Find(spec, preFilter, postFilters);
        }

        public Entities.Customer.Template Get(int id)
        {
            return _templateRepository.One(id);
        }

        public void Delelte(Entities.Customer.Template entity)
        {
            _templateRepository.Delete(entity);
        }

        public void Update(Entities.Customer.Template entity)
        {
            _templateRepository.Update(entity);
        }

        public int Count(Expression<Func<Template, bool>> spec = null)
        {
            return _templateRepository.Count(spec);
        }

        public void Create(Template entity)
        {
            _templateRepository.Create(entity);
        }

        public bool Exist(Expression<Func<Template, bool>> spec)
        {
            return _templateRepository.Any(spec);
        }
    }
}