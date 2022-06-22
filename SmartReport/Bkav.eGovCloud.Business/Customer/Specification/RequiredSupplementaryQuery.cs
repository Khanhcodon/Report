using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : RequiredSupplementaryQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 200912
    /// Author      : TienBV
    /// Description : Các điều kiện truy vấn cho bảng RequiredSupplementary
    /// </summary>
    public static class RequiredSupplementaryQuery
    {
        /// <summary>
        /// PaperId == paperId
        /// </summary>
        /// <param name="id">id của giấy tờ.</param>
        /// <returns></returns>
        public static Expression<Func<RequiredSupplementary, bool>> WithId(int id)
        {
            return s => s.RequiredSupplementaryId == id;
        }

        /// <summary>
        /// DocTypeId == doctypeId
        /// </summary>
        /// <param name="doctypeId">Id của loại hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<RequiredSupplementary, bool>> WithDocTypeId(Guid? doctypeId)
        {
            return s => s.DocTypeId == doctypeId;
        }

        /// <summary>
        /// Name == name
        /// </summary>
        /// <param name="name">Tên của giấy tờ.</param>
        /// <returns></returns>
        public static Expression<Func<RequiredSupplementary, bool>> WithName(string name)
        {
            return s => s.Name == name;
        }

        /// <summary>
        /// DocTypeId == docTypeId || DocFieldId == docfieldId
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="docfieldId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal static Expression<Func<RequiredSupplementary, bool>> WithDoctypeAndDocField(Guid docTypeId, int? docfieldId, int userId)
        {
            return s => !s.UserId.HasValue &&
                ((s.DocTypeId.HasValue && s.DocTypeId == docTypeId)
                    || (s.DocFieldId.HasValue && s.DocFieldId == docfieldId));
        }

        /// <summary>
        /// UserId == userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal static Expression<Func<RequiredSupplementary, bool>> WithUserId(int userId)
        {
            return s => s.UserId.HasValue && s.UserId == userId;
        }
    }
}
