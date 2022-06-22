using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Oauth;
using DotNetOpenAuth.OAuth2;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ClientValidator))]
    public class ClientModel : IClientDescription
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string Secret { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }

        public string Ip { get; set; }

        public bool IsActivated { get; set; }

        public ClientType ClientType { get; set; }

        public static DatabaseKeyNonceStore KeyNonceStore
        {
            get
            {
                return new DatabaseKeyNonceStore();
            }
        }

        public Uri DefaultCallback
        {
            get { return string.IsNullOrEmpty(this.Domain) ? null : new Uri(this.Domain); }
        }

        public bool HasNonEmptySecret
        {
            get { return !string.IsNullOrEmpty(this.Secret); }
        }

        /// <summary>
        /// Kiểm tra callback uri có đúng với uri đã đăng ký không
        /// </summary>
        /// <param name="callback">callback uri</param>
        /// <returns></returns>
        public bool IsCallbackAllowed(Uri callback)
        {
            if (string.IsNullOrEmpty(this.Domain))
            {
                return true;
            }
            var acceptableCallbackPattern = new Uri(this.Domain);
            return string.Equals(acceptableCallbackPattern.GetLeftPart(UriPartial.Authority).ToLower(), callback.GetLeftPart(UriPartial.Authority).ToLower());
        }

        /// <summary>
        /// Kiểm tra đúng secret ko
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public bool IsValidClientSecret(string secret)
        {
            var result = false;
            if (this.Secret.Equals(secret) && this.IsActivated == true)
            {
                result = true;
            }
            return result;
        }
    }
}