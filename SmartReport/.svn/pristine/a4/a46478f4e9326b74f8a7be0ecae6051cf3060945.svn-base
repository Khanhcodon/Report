using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : AccountQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng account
    /// </summary>
    public static class AccountQuery
    {
        /// <summary>
        /// AccountId == accountId
        /// </summary>
        /// <param name="accountId">Id của account.</param>
        /// <returns></returns>
        public static Expression<Func<Account, bool>> WithId(int accountId)
        {
            return s => s.AccountId == accountId;
        }

        /// <summary>
        /// UsernameEmailDomain == usernameEmailDomain
        /// </summary>
        /// <param name="usernameEmailDomain">Tên đăng nhập dạng email.</param>
        /// <returns></returns>
        public static Expression<Func<Account, bool>> WithUsernameEmailDomain(string usernameEmailDomain)
        {
            return s => s.UsernameEmailDomain == usernameEmailDomain;
        }

        /// <summary>
        /// IsActivated == isActivated
        /// </summary>
        /// <param name="isActivated">Kích hoạt.</param>
        /// <returns></returns>
        public static Expression<Func<Account, bool>> WithIsActivated(bool isActivated)
        {
            return s => s.IsActivated == isActivated;
        }
    }
}
