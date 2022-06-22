using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IStoreDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StoreDoc trong CSDL
    /// </summary>
    public interface IStoreDocDal
    {
        /// <summary>
        /// Tạo mới sổ hồ sơ
        /// </summary>
        /// <param name="storeDoc">Entity văn bản sổ hồ sơ</param>
        void Create(StoreDoc storeDoc);
    }
}
