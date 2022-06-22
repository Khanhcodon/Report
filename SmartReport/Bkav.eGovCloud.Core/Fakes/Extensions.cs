using System;
using System.Web;

namespace Bkav.eGovCloud.Core.Fakes
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Extentions - public - Core
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>1 thư viện mở rộng cho bộ Fake.</para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// Kiểm tra context có phải là FakeContext hay không
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <returns>True nếu là FakeContext và ngược lại</returns>
        public static bool IsFakeContext(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            return httpContext is FakeHttpContext;
        }

    }
}
