using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IStorePrivateUserDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 081013
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StorePrivateUser trong CSDL
    /// </summary>
    public interface IStorePrivateUserDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping giữa hồ sơ cá nhân và người tham gia. Nếu điều kiện bằng null thì sẽ lấy ra tất cả mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<StorePrivateUser> Gets(Expression<Func<StorePrivateUser, bool>> spec = null);

        /// <summary>
        /// Tạo mới mapping giữa hồ sơ cá nhân và người tham gia
        /// </summary>
        /// <param name="storePrivateUser">Entity storePrivateUser</param>
        void Create(StorePrivateUser storePrivateUser);

        /// <summary>
        /// Tạo mới nhiều mapping giữa hồ sơ cá nhân và người tham gia
        /// </summary>
        /// <param name="storePrivateUsers">Danh sách storePrivateUsers</param>
        void Create(IEnumerable<StorePrivateUser> storePrivateUsers);

        /// <summary>
        /// Xóa mapping giữa nhồ sơ cá nhân và người tham gia
        /// </summary>
        /// <param name="storePrivateUser">Entity storePrivateUser</param>
        void Delete(StorePrivateUser storePrivateUser);

        /// <summary>
        /// Xóa nhiều mapping giữa hồ sơ cá nhân và người tham gia
        /// </summary>
        /// <param name="storePrivateUsers">Danh sách entity storePrivateUsers</param>
        void Delete(IEnumerable<StorePrivateUser> storePrivateUsers);
    }
}
