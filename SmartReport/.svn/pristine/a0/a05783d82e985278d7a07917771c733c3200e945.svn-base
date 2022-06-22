using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : CatalogQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 251113
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng Catalog
    /// </summary>
    public static class CatalogQuery
    {
        /// <summary>
        /// CatalogId == catalogId
        /// </summary>
        /// <param name="catalogId">Id của danh mục.</param>
        /// <returns></returns>
        public static Expression<Func<Catalog, bool>> WithId(Guid catalogId)
        {
            return s => s.CatalogId == catalogId;
        }


        /// <summary>
        /// CatalogName == catalogName
        /// </summary>
        /// <param name="catalogName">Tên của danh mục.</param>
        /// <returns></returns>
        public static Expression<Func<Catalog, bool>> WithName(string catalogName)
        {
            return s => s.CatalogName == catalogName;
        }

        /// <summary>
        /// CatalogName == catalogName
        /// </summary>
        /// <param name="catalogName">Tên danh mục.</param>
        /// <returns></returns>
        public static Expression<Func<Catalog, bool>> ContainsName(string catalogName)
        {
            return s => s.CatalogName.ToLower().Contains(catalogName.ToLower());
        }
    }
}
