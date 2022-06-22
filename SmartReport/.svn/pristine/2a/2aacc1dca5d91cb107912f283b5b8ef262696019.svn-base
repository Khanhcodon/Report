using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : DocFieldQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 060912
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng DocField
    /// </summary>
    public static class DocFieldQuery
    {
        /// <summary>
        /// DocFieldId == docFieldId
        /// </summary>
        /// <param name="docFieldId">Id của lĩnh vực.</param>
        /// <returns></returns>
        public static Expression<Func<DocField, bool>> WithId(int docFieldId)
        {
            return s => s.DocFieldId == docFieldId;
        }

        /// <summary>
        /// DocFieldName == docFieldName
        /// </summary>
        /// <param name="docFieldName">Tên lĩnh vực.</param>
        /// <returns></returns>
        public static Expression<Func<DocField, bool>> WithDocFieldName(string docFieldName)
        {
            return s => s.DocFieldName == docFieldName;
        }

        /// <summary>
        /// DocFieldName == docFieldName
        /// </summary>
        /// <param name="categoryBusinessId">Tên lĩnh vực.</param>
        /// <returns></returns>
        public static Expression<Func<DocField, bool>> WithCateogryBusinessId(int categoryBusinessId)
        {
            return t =>
                   categoryBusinessId == 0 || ((CategoryBusinessTypes)t.CategoryBusinessId & (CategoryBusinessTypes)categoryBusinessId) == (CategoryBusinessTypes)categoryBusinessId;
        }

        /// <summary>
        /// DocFieldName == DocFieldName
        /// </summary>
        /// <param name="docFieldName">Tên lĩnh vực</param>
        /// <returns></returns>
        public static Expression<Func<DocField, bool>> ContainsKey(string docFieldName)
        {
            return s => s.DocFieldName.ToLower().Contains(docFieldName.ToLower());
        }
    }
}
