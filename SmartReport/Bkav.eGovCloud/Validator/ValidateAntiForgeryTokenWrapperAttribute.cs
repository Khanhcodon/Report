using System;
using System.Web.Mvc;
using System.Linq;

namespace Bkav.eGovCloud.Validator
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateAntiForgeryTokenWrapperAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly ValidateAntiForgeryTokenAttribute _validator;

        private readonly AcceptVerbsAttribute _verbs;

        public ValidateAntiForgeryTokenWrapperAttribute(HttpVerbs verbs) : this(verbs, null) { }

        public ValidateAntiForgeryTokenWrapperAttribute(HttpVerbs verbs, string salt)
        {
            this._verbs = new AcceptVerbsAttribute(verbs);
            this._validator = new ValidateAntiForgeryTokenAttribute()
                {
                    //Salt = salt
                };
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string httpMethodOverride = filterContext.HttpContext.Request.GetHttpMethodOverride();
            if (this._verbs.Verbs.Contains(httpMethodOverride, StringComparer.OrdinalIgnoreCase))
            {
                this._validator.OnAuthorization(filterContext);
            }
        }
    }
}