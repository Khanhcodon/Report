using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : ResourceQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng resource
    /// </summary>
    public static class ResourceQuery
    {
        /// <summary>
        /// ResourceId == resourceId
        /// </summary>
        /// <param name="resourceId">Id của resource.</param>
        /// <returns></returns>
        public static Expression<Func<Resource, bool>> WithId(int resourceId)
        {
            return s => s.ResourceId == resourceId;
        }

        /// <summary>
        /// ResourceKey == resourceKey
        /// </summary>
        /// <param name="resourceKey">Key của resource.</param>
        /// <returns></returns>
        public static Expression<Func<Resource, bool>> WithKey(string resourceKey)
        {
            return s => s.ResourceKey.ToLower() == resourceKey.ToLower();
        }

        /// <summary>
        /// ResourceKey == resourceKey
        /// </summary>
        /// <param name="resourceKey">Key của resource.</param>
        /// <returns></returns>
        public static Expression<Func<Resource, bool>> ContainsKey(string resourceKey)
        {
            return s => s.ResourceKey.ToLower().Contains(resourceKey.ToLower());
        }
    }
}
