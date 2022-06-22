using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Core.Lunar
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LunarYearUtils - public - Core
    /// Access Modifiers: 
    /// Create Date : 030713
    /// Author      : GiangPN
    /// </author>
    /// <summary> 
    /// <para>1 thư viện ở rộng cho việc xử lý lịch âm dương</para>
    /// (GiangPN@bkav.com - 030713)
    /// </summary>
    public static class LunarDateExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static LunarDate ToLunarDate(this DateTime d)
        {
            return LunarUtils.Solar2Lunar(d);
        }
    }
}
