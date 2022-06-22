using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;

namespace Bkav.eGovCloud.Oauth
{
    [RequireHttps]
    public class OAuthController : Controller
    {
        private readonly AuthorizationServer authorizationServer =
            new AuthorizationServer(new AuthorizationServerHost());

        /// <summary>
        /// Cấp token
        /// </summary>
        /// <returns></returns>
        public ActionResult Token()
        {
            var request = this.authorizationServer.HandleTokenRequest(this.Request);
            return request.AsActionResult();
        }

        /// <summary>
        /// Authorize request
        /// </summary>
        /// <returns></returns>
        public ActionResult Authorize()
        {
            var pendingRequest = this.authorizationServer.ReadAuthorizationRequest(Request);
            if (pendingRequest == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Missing authorization request.");
            }
            var approval = this.authorizationServer
                .PrepareApproveAuthorizationRequest(pendingRequest, pendingRequest.Callback.ToString());
            var response = this.authorizationServer.Channel.PrepareResponse(approval);
            return response.AsActionResult();
        }
    }
}