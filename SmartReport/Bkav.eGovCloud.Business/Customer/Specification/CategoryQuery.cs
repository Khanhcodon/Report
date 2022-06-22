using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : CategoryQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 2409
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng Category
    /// </summary>
    public static class CategoryQuery
    {
        /// <summary>
        /// CategoryId == CategoryId
        /// </summary>
        /// <param name="categoryId">Id của thể loại văn bản.</param>
        /// <returns></returns>
        public static Expression<Func<Category, bool>> WithId(int categoryId)
        {
            return s => s.CategoryId == categoryId;
        }

        /// <summary>
        /// CategoryName == CategoryName
        /// </summary>
        /// <param name="categoryName">Tên thể loại văn bản.</param>
        /// <returns></returns>
        public static Expression<Func<Category, bool>> WithCategoryName(string categoryName)
        {
            return s => s.CategoryName == categoryName;
        }

        /// <summary>
        /// CategoryName == CategoryName
        /// </summary>
        /// <param name="categoryBusinessId">Tên lĩnh vực.</param>
        /// <returns></returns>
        public static Expression<Func<Category, bool>> WithCateogryBusinessId(int categoryBusinessId)
        {
            return t =>
                   categoryBusinessId == 0 || ((CategoryBusinessTypes)t.CategoryBusinessId & (CategoryBusinessTypes)categoryBusinessId) == (CategoryBusinessTypes)categoryBusinessId;
        }

        /// <summary>
        /// CategoryName == CategoryName
        /// </summary>
        /// <param name="categoryName">Tên thể loại văn bản</param>
        /// <returns></returns>
        public static Expression<Func<Category, bool>> ContainsKey(string categoryName)
        {
            return s => s.CategoryName.ToLower().Contains(categoryName.ToLower());
        }
    }
}
