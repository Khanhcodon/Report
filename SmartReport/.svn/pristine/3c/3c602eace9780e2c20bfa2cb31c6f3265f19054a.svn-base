using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : ExtensionTimeBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 290313</para>
    /// <para> Author : GiangPN@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý gia hạn</para>
    /// <para> ( GiangPN@bkav.com - 290313) </para>
    /// </summary>
    public class ExtensionTimeBll : ServiceBase
    {
        private readonly IRepository<Renewals> _extensionTimeRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context.</param>
        public ExtensionTimeBll(IDbCustomerContext context)
            : base(context)
        {
            _extensionTimeRepository = Context.GetRepository<Renewals>();
        }

        /// <summary>
        /// <para> Thêm gia hạn.</para>
        /// <para> (GiangPN@bkav.com 290313)</para>
        /// </summary>
        /// <param name="entity">Entitie</param>
        public void Create(Renewals entity)
        {
            _extensionTimeRepository.Create(entity);
            Context.SaveChanges();
        }
    }
}
