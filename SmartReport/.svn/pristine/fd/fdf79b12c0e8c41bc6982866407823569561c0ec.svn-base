using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : DomainAliasQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng domainalias
    /// </summary>
    public static class DomainAliasQuery
    {
        /// <summary>
        /// DomainAliasId == domainAliasId
        /// </summary>
        /// <param name="domainAliasId">Id của alias.</param>
        /// <returns></returns>
        public static Expression<Func<DomainAlias, bool>> WithId(int domainAliasId)
        {
            return s => s.DomainAliasId == domainAliasId;
        }

        /// <summary>
        /// Alias == alias
        /// </summary>
        /// <param name="alias">Http alias.</param>
        /// <returns></returns>
        public static Expression<Func<DomainAlias, bool>> WithAlias(string alias)
        {
            return s => s.Alias == alias;
        }

        /// <summary>
        /// IsPrimary == isPrimary
        /// </summary>
        /// <param name="isPrimary">Là alias chính</param>
        /// <returns></returns>
        public static Expression<Func<DomainAlias, bool>> WithIsPrimary(bool isPrimary)
        {
            return s => s.IsPrimary == isPrimary;
        }

        /// <summary>
        /// IsActivated == isActivated
        /// </summary>
        /// <param name="isActivated">Kích hoạt.</param>
        /// <returns></returns>
        public static Expression<Func<DomainAlias, bool>> WithIsActivated(bool isActivated)
        {
            return s => s.IsActivated == isActivated;
        }
    }
}
