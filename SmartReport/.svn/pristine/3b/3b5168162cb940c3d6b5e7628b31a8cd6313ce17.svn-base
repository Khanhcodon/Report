using System;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    /// Extension cho kiểu dữ liệu Guid
    /// </summary>
    public static class GuidExtension
    {
        /// <summary>
        /// Lấy mẫu 10 ký tự trong đối tượng Guid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string To10Char(this Guid input)
        {
            var strSplits = input.ToString().Split(new char[] { '-' });
            var result = "";
            foreach (var str in strSplits)
            {
                result += str[0].ToString() + str[1].ToString();
            }
            return result;
        }
    }
}
