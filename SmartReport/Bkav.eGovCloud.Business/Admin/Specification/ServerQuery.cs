using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : ServerQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng server
    /// </summary>
    public static class ServerQuery
    {
        /// <summary>
        /// ServerId == serverId
        /// </summary>
        /// <param name="serverId">Id của server.</param>
        /// <returns></returns>
        public static Expression<Func<Server, bool>> WithId(int serverId)
        {
            return s => s.ServerId == serverId;
        }

        /// <summary>
        /// PublicDomain == publicDomain
        /// </summary>
        /// <param name="publicDomain">Id của server.</param>
        /// <returns></returns>
        public static Expression<Func<Server, bool>> WithPublicDomain(string publicDomain)
        {
            return s => s.PublicDomain.ToLower() == publicDomain.ToLower();
        }
    }
}
