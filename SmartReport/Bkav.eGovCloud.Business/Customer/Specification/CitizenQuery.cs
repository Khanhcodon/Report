using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    ///
    /// </summary>
    internal class CitizenQuery
    {
        /// <summary>
        /// Id == id
        /// </summary>
        /// <param name="id">Id của người dùng.</param>
        /// <returns></returns>
        public static Expression<Func<Citizen, bool>> WithId(int id)
        {
            return u => u.Id == id;
        }

        /// <summary>
        /// Account == account
        /// </summary>
        /// <param name="account">account của người dùng.</param>
        /// <returns></returns>
        public static Expression<Func<Citizen, bool>> WithAccount(string account)
        {
            return u => u.Account.ToLower() == account.ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="citizenName"></param>
        /// <returns></returns>
        public static Expression<Func<Citizen, bool>> ContainsCitizenName(string citizenName)
        {
            return u => u.CitizenName.ToLower().Contains(citizenName.ToLower());
        }
    }
}