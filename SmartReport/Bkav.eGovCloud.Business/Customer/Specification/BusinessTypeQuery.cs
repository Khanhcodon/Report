using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : BusinessTypeQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng BusinessType
    /// </summary>
    public static class BusinessTypeQuery
    {
        /// <summary>
        /// BusinessTypeId == businessTypeId
        /// </summary>
        /// <param name="businessTypeId">Id của loại doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<BusinessType, bool>> WithId(int businessTypeId)
        {
            return s => s.BusinessTypeId == businessTypeId;
        }


        /// <summary>
        /// BusinessTypeName == businessTypename
        /// </summary>
        /// <param name="businessTypename">Tên của doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<BusinessType, bool>> WithName(string businessTypename)
        {
            return s => s.BusinessTypeName == businessTypename;
        }
    }
}
