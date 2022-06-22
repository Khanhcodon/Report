using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class JWTUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="UserName"></param>
        /// <param name="JWTModulus"></param>
        /// <param name="JWTExponent"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string DoLoginJWT_VerifySignature(string accessToken, out string UserName,string JWTModulus, string JWTExponent, out long exp )
        {
            string msg = ""; UserName = ""; exp = 0;
            string[] tokenParts = accessToken.Split('.');
            try
            {
                if (string.IsNullOrWhiteSpace(JWTModulus)) return "Chưa cấu hình JWTModulus";

                if (string.IsNullOrWhiteSpace(JWTExponent)) return "Chưa cấu hình JWTExponent";

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(
                  new RSAParameters()
                  {
                      Modulus = FromBase64Url(JWTModulus),
                      Exponent = FromBase64Url(JWTExponent)
                  });
                SHA256 sha256 = SHA256.Create();
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenParts[0] + '.' + tokenParts[1]));

                RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                if (!rsaDeformatter.VerifySignature(hash, FromBase64Url(tokenParts[2]))) return "Không Verify được id_token";

                msg = DecodeJWT(accessToken, out UserName, out exp);
                if (msg.Length > 0) return msg;
            }
            catch (Exception ex)
            {
                return "Không Verify được id_token: " + ex.ToString();
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idToken"></param>
        /// <param name="UserName"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string DecodeJWT(string idToken, out string UserName, out long exp)
        {
            UserName = ""; exp = 0;
            try
            {
                string[] token = idToken.Split('.');
                string dummyData = token[1].Trim().Replace(" ", "+");
                if (dummyData.Length % 4 > 0) dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');

                JObject jsonDecoded = JObject.Parse(DecodeStringFromBase64(dummyData));

                if (jsonDecoded.GetValue("emails.work") == null) UserName = jsonDecoded.GetValue("sub").ToString();
                else UserName = jsonDecoded.GetValue("emails.work").ToString();

                exp = Convert.ToInt64(jsonDecoded.GetValue("exp"));
                if (exp == -1) return "exp = jsonDecoded.GetValue(\"exp\").ToNumber(-1);";
            }
            catch (Exception e)
            {
                return e.ToString();
            }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringToEncode"></param>
        /// <returns></returns>
        public static string EncodeStringToBase64(string stringToEncode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringToDecode"></param>
        /// <returns></returns>
        public static string DecodeStringFromBase64(string stringToDecode)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(stringToDecode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns></returns>
        public static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
    }
}
