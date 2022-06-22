using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : DocTypeQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 240912
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng DocType
    /// </summary>
    public static class DocTypeQuery
    {
        /// <summary>
        /// DocTypeId == docTypeId
        /// </summary>
        /// <param name="docTypeId">Id của loại hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<DocType, bool>> WithId(Guid docTypeId)
        {
            return s => s.DocTypeId == docTypeId;
        }

        /// <summary>
        /// DocFieldId == docTypeId
        /// </summary>
        /// <param name="docFieldId">Id của lĩnh vực.</param>
        /// <returns></returns>
        public static Expression<Func<DocType, bool>> WithDocFieldId(int? docFieldId)
        {
            return s => !docFieldId.HasValue || s.DocFieldId == docFieldId;
        }

        /// <summary>
        /// CategoryId == categoryId
        /// </summary>
        /// <param name="categoryId">Id của thể loại văn bản.</param>
        /// <returns></returns>
        public static Expression<Func<DocType, bool>> WithCategoryId(int? categoryId)
        {
            return s => !categoryId.HasValue || s.CategoryId == categoryId;
        }

        /// <summary>
        /// CategoryId == categoryId
        /// </summary>
        /// <param name="categoryBusinessId">Id của thể loại văn bản.</param>
        /// <returns></returns>
        public static Expression<Func<DocType, bool>> WithCategoryBusinessId(int? categoryBusinessId)
        {
            return s => !categoryBusinessId.HasValue || s.CategoryBusinessId == categoryBusinessId;
        }

		/// <summary>
		/// CategoryId == categoryId
		/// </summary>
		/// <param name="actionLevel">Id của thể loại văn bản.</param>
		/// <returns></returns>
		public static Expression<Func<DocType, bool>> WithActionLevel(int? actionLevel)
		{
			return s => !actionLevel.HasValue || s.ActionLevel == actionLevel;
		}

		/// <summary>
		/// IsActivated == isActivated
		/// </summary>
		/// <param name="isActivated">Trạng thái kích hoạt.</param>
		/// <returns></returns>
		public static Expression<Func<DocType, bool>> WithIsActivated(bool? isActivated)
        {
            return s => !isActivated.HasValue || s.IsActivated == isActivated;
        }

        /// <summary>
        /// DocTypeName == docTypeName
        /// </summary>
        /// <param name="docTypeName">Tên loại hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<DocType, bool>> WithDocTypeName(string docTypeName)
        {
            return s => s.DocTypeName == docTypeName;
        }

        /// <summary>
        /// DocTypeCode == docTypeCode
        /// </summary>
        /// <param name="docTypeCode">Mã loại hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<DocType, bool>> WithDocTypeCode(string docTypeCode)
        {
            return s => s.DocTypeCode == docTypeCode;
        }

        /// <summary>
        /// Điều kiện docTypeName gần giống với docTypeName truyền vào
        /// </summary>
        /// <param name="docTypeName">docTypeName</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<DocType, bool>> ContainsDocTypeName(string docTypeName)
        {
            return u => docTypeName == null || docTypeName == string.Empty || u.DocTypeName.Contains(docTypeName);
        }

        /// <summary>
        /// Điều kiện docTypeCode gần giống với docTypeCode truyền vào
        /// </summary>
        /// <param name="docTypeCode">docTypeCode</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<DocType, bool>> ContainsDocTypeCode(string docTypeCode)
        {
            return u => docTypeCode == null || docTypeCode == string.Empty || u.DocTypeCode.Contains(docTypeCode);
        }

        /// <summary>
        /// DocTypeName == docTypeName
        /// </summary>
        /// <param name="docTypeName">Tên loại hồ sơ</param>
        /// <returns></returns>
        public static Expression<Func<DocType, bool>> ContainsKey(string docTypeName)
        {
            return s => s.DocTypeName.ToLower().Contains(docTypeName.ToLower());
        }
    }
}
