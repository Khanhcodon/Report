using Bkav.eGovCloud.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : VoteSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 140716
    /// Author      : DungNVl
    /// Description :
    /// </summary>
    public class VoteSettings : ISettings
    {
        /// <summary>
        /// Trạng thái đã kích hoạt hay chưa
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Những người được phép tạo eGov
        /// </summary>
        public string UserCreates { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>
        public List<int> ListUserCreateVote()
        {
            return ParseUserIds(UserCreates);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static string UserCompareString(List<int> userIds)
        {
            return string.Format("{0}{1}{0}", ";", string.Join(";", userIds));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userStr"></param>
        /// <returns></returns>
        private List<int> ParseUserIds(string userStr)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(userStr))
            {
                return result;
            }

            var userIds = userStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            result = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => Int32.Parse(u)).ToList();

            return result;
        }
    }
}