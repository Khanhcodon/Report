using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
namespace Bkav.eGovCloud.Core.License
{
    /// <summary>
    /// Lớp hỗ trợ license
    /// </summary>
    public class LicenseUtil
    {
        /// <summary>
        /// Trả về key tự sinh bất kỳ cho license.
        /// </summary>
        public static string GetRandomLicenseRSAKey
        {
            get
            {
                return new RSAKeyValue().GetXml().InnerXml;
            }
        }
    }
}
