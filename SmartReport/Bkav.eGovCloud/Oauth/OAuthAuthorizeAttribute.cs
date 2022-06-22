using System;
using System.Linq;
using System.Web.Http.Controllers;
using DotNetOpenAuth.OAuth2;

namespace Bkav.eGovCloud.Oauth
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class OAuthAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        private readonly ResourceServer resourceServer;

        private string[] _scopes { get; set; }

        public OAuthAuthorizeAttribute(params object[] scopes)
        {
            if (scopes.Any(p => p.GetType().BaseType != typeof(Enum)))
            {
                throw new ArgumentException("scopes");
            }
            this._scopes = scopes.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
            var standardAccessTokenAnalyzer = new StandardAccessTokenAnalyzer(EncryptionKeys.GetAuthorizationServerSigningPublicKey(), EncryptionKeys.GetResourceServerEncryptionPrivateKey());
            this.resourceServer = new ResourceServer(standardAccessTokenAnalyzer);
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!this.AccessTokenIsAuthorizedForRequestedScopes())
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }

        protected virtual bool AccessTokenIsAuthorizedForRequestedScopes()
        {
            try
            {
                foreach (var scope in this._scopes)
                {
                    if (OAuthUtilities.SplitScopes(scope ?? string.Empty).IsSubsetOf(this.resourceServer.GetAccessToken().Scope))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
    }
}