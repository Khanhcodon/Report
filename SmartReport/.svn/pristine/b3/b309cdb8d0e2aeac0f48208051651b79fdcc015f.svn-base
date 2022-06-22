using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : StoreQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 180912
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng Store
    /// </summary>
    public static class StoreQuery
    {
        /// <summary>
        /// storeId == storeId
        /// </summary>
        /// <param name="storeId">Id của sổ hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<Store, bool>> WithId(int storeId)
        {
            return s => s.StoreId == storeId;
        }

        /// <summary>
        /// StoreName == StoreName
        /// </summary>
        /// <param name="storeName">Tên sổ hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<Store, bool>> WithStoreName(string storeName)
        {
            return s => s.StoreName == storeName;
        }

        /// <summary>
        /// CategoryBusinessId == categoryBusinessId
        /// </summary>
        /// <param name="categoryBusinessId">Mã danh mục nghiệp vụ</param>
        /// <returns></returns>
        public static Expression<Func<Store, bool>> WithCategoryBusinessId(int categoryBusinessId)
        {
            //return u => !categoryBusinessId.HasValue || u.CategoryBusinessId == categoryBusinessId;
            return t =>
                   categoryBusinessId == 0 || ((CategoryBusinessTypes)t.CategoryBusinessId & (CategoryBusinessTypes)categoryBusinessId) == (CategoryBusinessTypes)categoryBusinessId;
        }

        /// <summary>
        /// StoreName == storeName
        /// </summary>
        /// <param name="storeName">Tên sổ hồ sơ</param>
        /// <returns></returns>
        public static Expression<Func<Store, bool>> ContainsKey(string storeName)
        {
            return s => s.StoreName.ToLower().Contains(storeName.ToLower());
        }
    }
}
