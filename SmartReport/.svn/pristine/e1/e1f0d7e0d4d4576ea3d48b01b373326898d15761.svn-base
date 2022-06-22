using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.History
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class HistoryProcess
    {
        /// <summary>
        /// 
        /// </summary>
        public HistoryProcess()
        {
            HistoryPath = new List<HistoryPath>();
            HistoryThongbao = new List<HistoryThongbao>();
            HistoryXinykien = new List<HistoryXinykien>();
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("HistoryPath")]
        public List<HistoryPath> HistoryPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("HistoryThongbao")]
        public List<HistoryThongbao> HistoryThongbao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("HistoryXinykien")]
        public List<HistoryXinykien> HistoryXinykien { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public static class HistoryProcessExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oThis"></param>
        /// <param name="userSendId"></param>
        /// <param name="dateCreated"></param>
        /// <returns></returns>
        public static T GetOne<T>(this List<T> oThis, int userSendId, DateTime dateCreated) where T : HistoryBase
        {
            return oThis.SingleOrDefault(
                    c => c.UserSendId == userSendId && c.DateCreated.AddMinutes(1) >= DateTime.Now &&
                    c.DateCreated.ToString("yyyyMMddhhmmss") == dateCreated.ToString("yyyyMMddhhmmss"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oThis"></param>
        /// <param name="dateCreated"></param>
        /// <returns></returns>
        public static T GetOne<T>(this List<T> oThis, DateTime dateCreated) where T : HistoryBase
        {
            return oThis.SingleOrDefault(
                    c => c.DateCreated.AddMinutes(1) >= DateTime.Now &&
                    c.DateCreated.ToString("yyyyMMddhhmmss") == dateCreated.ToString("yyyyMMddhhmmss"));
        }
    }
}
