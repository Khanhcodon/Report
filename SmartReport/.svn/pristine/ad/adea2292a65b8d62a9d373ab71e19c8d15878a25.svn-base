using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Static Class : CatalogQuery - public - BLL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 231012
    /// <para></para> Author      : TienBV
    /// <para></para> Description : Sinh các điều kiện truy vấn cho bảng CatalogQuery
    /// </summary>
    public static class CatalogQuery
    {
        /// <summary>
        /// CatalogId == CatalogId
        /// </summary>
        /// <param name="guidId">the catalog guid id.</param>
        /// <returns></returns>
        public static Expression<Func<Catalog, bool>> WithId(Guid guidId)
        {
            return s => s.CatalogId == guidId;
        }

        /// <summary>
        /// CatalogName == catalogName
        /// </summary>
        /// <param name="catalogName">the catalog name key.</param>
        /// <returns></returns>
        public static Expression<Func<Catalog, bool>> WithKey(string catalogName)
        {
            return s => s.CatalogName.ToLower() == catalogName.ToLower();
        }


        /// <summary>
        /// ResourceKey == resourceKey
        /// </summary>
        /// <param name="catalogKey">Key của catalog name.</param>
        /// <returns></returns>
        public static Expression<Func<Catalog, bool>> ContainsKey(string catalogKey)
        {
            return s => s.CatalogName.ToLower().Contains(catalogKey.ToLower());
        }
    }
}
