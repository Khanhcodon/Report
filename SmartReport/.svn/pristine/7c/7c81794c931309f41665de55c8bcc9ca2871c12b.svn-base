using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : SettingQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 090812
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng Setting
    /// </summary>
    public static class SettingQuery
    {
        /// <summary>
        /// SettingId == settingId
        /// </summary>
        /// <param name="settingId">Id của cấu hình.</param>
        /// <returns></returns>
        public static Expression<Func<Setting, bool>> WithId(int settingId)
        {
            return s => s.SettingId == settingId;
        }

        /// <summary>
        /// SettingKey == settingKey
        /// </summary>
        /// <param name="settingKey">Key của cấu hình.</param>
        /// <returns></returns>
        public static Expression<Func<Setting, bool>> WithKey(string settingKey)
        {
            return s => s.SettingKey.ToLower() == settingKey.ToLower();
        }
    }
}
