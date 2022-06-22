using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : DomainQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng domain
    /// </summary>
    public static class DomainQuery
    {
        /// <summary>
        /// DomainId == domainId
        /// </summary>
        /// <param name="domainId">Id của domain.</param>
        /// <returns></returns>
        public static Expression<Func<Domain, bool>> WithId(int domainId)
        {
            return s => s.DomainId == domainId;
        }

        /// <summary>
        /// DomainName == domainName
        /// </summary>
        /// <param name="domainName">Tên của domain.</param>
        /// <returns></returns>
        public static Expression<Func<Domain, bool>> WithDomainName(string domainName)
        {
            return s => s.DomainName.ToLower() == domainName.ToLower();
        }
    }
}
