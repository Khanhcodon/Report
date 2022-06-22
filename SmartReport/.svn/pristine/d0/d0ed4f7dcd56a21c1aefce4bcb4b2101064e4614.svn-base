using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : ExtensionTimeDal - public - Dal </para>
    /// <para> Access Modifiers: IExtensionTimeDal</para>
    /// <para> Create Date : 290313</para>
    /// <para> Author : GiangPN@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> API tương tác với bảng gia hạn. </para>
    /// <para> ( GiangPN@bkav.com - 290313) </para>
    /// </summary>
    public class ExtensionTimeDal : DataAccessBase, IExtensionTimeDal
    {
        private readonly IRepository<ExtendedTime> _extensionTimeRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context"></param>
        public ExtensionTimeDal(IDbCustomerContext context)
            : base(context)
        {
            _extensionTimeRepository = context.GetRepository<ExtendedTime>();
        }

        #pragma warning disable 1591

        public void Create(ExtendedTime entity)
        {
            _extensionTimeRepository.Create(entity);
        }

        public void Create(IEnumerable<ExtendedTime> entities)
        {
            foreach (var entity in entities)
            {
                _extensionTimeRepository.Create(entity, false);
            }
            Context.SaveChanges();
        }

        public void Delete(IEnumerable<ExtendedTime> entities)
        {
            foreach (var entity in entities)
            {
                _extensionTimeRepository.Delete(entity, false);
            }
            Context.SaveChanges();
        }

        public IEnumerable<ExtendedTime> Gets(Expression<Func<ExtendedTime, bool>> spec)
        {
            return _extensionTimeRepository.Find(spec);
        }

        public bool Exist(Expression<Func<ExtendedTime, bool>> spec)
        {
            return _extensionTimeRepository.Any(spec);
        }
    }
}
