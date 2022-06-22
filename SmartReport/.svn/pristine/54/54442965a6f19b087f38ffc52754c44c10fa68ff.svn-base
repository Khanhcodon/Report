using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : RoleQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 100912
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng role
    /// </summary>
    public static class RoleQuery
    {
        /// <summary>
        /// RoleId == roleId
        /// </summary>
        /// <param name="roleId">Id của nhóm người dùng.</param>
        /// <returns></returns>
        public static Expression<Func<Role, bool>> WithId(int roleId)
        {
            return s => s.RoleId == roleId;
        }

        /// <summary>
        /// RoleKey == roleKey
        /// </summary>
        /// <param name="roleKey">Mã nhóm người dùng.</param>
        /// <returns></returns>
        public static Expression<Func<Role, bool>> WithRoleKey(string roleKey)
        {
            return s => s.RoleKey == roleKey;
        }

        /// <summary>
        /// IsActivated == isActivated
        /// </summary>
        /// <param name="isActivated">Kích hoạt.</param>
        /// <returns></returns>
        public static Expression<Func<Role, bool>> WithIsActivated(bool? isActivated = null)
        {
            return r => !isActivated.HasValue || r.IsActivated == isActivated;
        }
    }
}
