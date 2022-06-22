using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreDocDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IStoreDocDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StoreDoc trong CSDL
    /// </summary>
    public class StoreDocDal : DataAccessBase, IStoreDocDal
    {
        private readonly IRepository<StoreDoc> _storeDocRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">CUstomer context</param>
        public StoreDocDal(IDbCustomerContext context)
            : base(context)
        {
            _storeDocRepository = Context.GetRepository<StoreDoc>();
        }

        #pragma warning disable 1591
        public void Create(StoreDoc storeDoc)
        {
            _storeDocRepository.Create(storeDoc);
        }
    }
}
