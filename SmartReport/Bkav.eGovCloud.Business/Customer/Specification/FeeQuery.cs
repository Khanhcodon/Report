using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : CodeQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 200912
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng Fee
    /// </summary>
    public static class FeeQuery
    {
        /// <summary>
        /// FeeId == feeId
        /// </summary>
        /// <param name="feeId">Id của lệ phí.</param>
        /// <returns></returns>
        public static Expression<Func<Fee, bool>> WithId(int feeId)
        {
            return s => s.FeeId == feeId;
        }

        /// <summary>
        /// DocTypeId == doctypeId
        /// </summary>
        /// <param name="doctypeId">Id của loại hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<Fee, bool>> WithDocTypeId(Guid? doctypeId)
        {
            return s => doctypeId == Guid.Empty || s.DocTypeId == doctypeId;
        }

        /// <summary>
        /// FeeName == feename
        /// </summary>
        /// <param name="feename">Tên của lệ phí.</param>
        /// <returns></returns>
        public static Expression<Func<Fee, bool>> WithName(string feename)
        {
            return s => s.FeeName.Equals(feename);
        }

        /// <summary>
        /// DocumentTypeId == doctypeId and FeeTypeId = feeType
        /// </summary>
        /// <param name="doctypeId">Document type id</param>
        /// <param name="feeType">Enum.FeeType</param>
        /// <returns></returns>
        public static Expression<Func<Fee, bool>> WithDocTypeAndFeeType(Guid doctypeId, Entities.FeeType feeType)
        {
            return s => s.DocTypeId.Equals(doctypeId) && s.FeeTypeId.Equals((int)feeType);
        }
    }
}
