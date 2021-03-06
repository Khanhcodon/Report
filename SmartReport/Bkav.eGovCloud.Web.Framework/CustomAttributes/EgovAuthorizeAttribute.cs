using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.SsoHelper;
using System;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EgovAuthorizeAttribute - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : System.Web.MVC.FilterAttribute
    ///     * Implement: System.Web.MVC.IAuthorizationFilter
    /// Create Date : 300712
    /// Author      : TrungVH
    /// Description : Attribute kiểm tra xác thực người dùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    [System.Runtime.InteropServices.ComVisible(false)]
    public class EgovAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {

#pragma warning disable 1591

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            if (controllerName.Equals("Account", StringComparison.OrdinalIgnoreCase) && actionName.Equals("Login", StringComparison.OrdinalIgnoreCase))
            {
                // return;
            }

            var userService = DependencyResolver.Current.GetService<UserBll>();
            // var mobileDeviceService = DependencyResolver.Current.GetService<MobileDeviceBll>();

            var ctx = filterContext.HttpContext;
            var connectionSettings = SsoSettings.Instance;
            var bkavAuthCookie = ctx.Request.Cookies.Get(connectionSettings.BkavSSOCookieName);
            var ssoHelper = new EncryptData(connectionSettings.BkavSSOKeyVersion, connectionSettings.BkavSSOSecretKey);
            var userNameVala = "";

            if (bkavAuthCookie == null || !ssoHelper.IsValidCookie(bkavAuthCookie.Value))
            {
                userNameVala = GetUserNameFromValaToken(ctx);
                if (string.IsNullOrEmpty(userNameVala))
                {
                    ClearCookie(ctx, connectionSettings);
                    return;
                }  
            }

            if (ctx.User.Identity.IsAuthenticated)
            {
                var currentUser = userService.CurrentUser;
                if (currentUser == null || !currentUser.IsActivated)
                {
                    ClearCookie(ctx, connectionSettings);
                    filterContext.Result = new RedirectResult("~/Account/Logout");
                    return;
                }

                bool hasLimitDevice = false;
                var mac = GetMacCookie(ctx, out hasLimitDevice);
                var loginToken = bkavAuthCookie.Value;

                //var divices = mobileDeviceService.GetsFromCache(currentUser.UserId);
                //if (divices.Any())
                //{
                //    var hasActive = divices.Any(d => d.IsActive && !d.HasBlock && d.Serial == mac && d.LoginToken == loginToken);
                //    if (!hasActive && hasLimitDevice)
                //    {
                //        ClearCookie(ctx, connectionSettings);
                //        filterContext.Result = new RedirectResult("~/Account/Logout");
                //    }
                //}

                return;
            }

            var userInfoCookie = ctx.Request.Cookies.Get(connectionSettings.UserInfoCookie);

            // Lấy username trong cookie xác thực
            var userName = string.IsNullOrEmpty(userNameVala)? ssoHelper.GetOriginData(bkavAuthCookie.Value)["user"].ToString() : userNameVala;
            if(userName != ssoHelper.GetOriginData(bkavAuthCookie.Value)["user"].ToString())
            {
                ClearCookie(ctx, connectionSettings);
                filterContext.Result = new RedirectResult("~/Account/Logout");
                return;
            }

            // Kiểm tra info của người đăng nhập hiện tại có đúng với người của cookie xác thực hay không, nêu không đúng, set cookieinfo về null
            if (userInfoCookie != null)
            {
                var userInfoTicket = FormsAuthentication.Decrypt(userInfoCookie.Value);
                if (userName != userInfoTicket.Name)
                {
                    userInfoCookie = null;
                }
            }

            if (!string.IsNullOrEmpty(userNameVala))
            {
                userInfoCookie = null;
            }

            // Nếu userinfo cookie bằng null, tạo 1 cookie info mới để sử dụng
            // Trường hợp = null: khi sử dụng SSO với bên thứ 3 như Bmail, Bwss mà các bên đó đăng nhập trước và ném token cho eGov dùng
            if (userInfoCookie == null)
            {
                User user;

                //Todo: cần sửa lại chổ này, trong Framework không nên dính dáng tới kết nối cơ sở dữ liệu
                user = userService.Get(userName);
                if (user == null)
                {
                    return;
                }
                var cookieData = new CustomerCookieData
                {
                    UsernameWithDomain = user.UsernameEmailDomain,
                    UserId = user.UserId,
                    Email = user.UsernameEmailDomain,
                    FullName = user.FullName
                };

                userInfoCookie = FormsAuthentication.GetAuthCookie(userName, false);
                userInfoCookie.Name = connectionSettings.UserInfoCookie;
                var oldTicket = FormsAuthentication.Decrypt(userInfoCookie.Value);
                var newTicket = new FormsAuthenticationTicket(
                    oldTicket.Version,
                    oldTicket.Name,
                    oldTicket.IssueDate,
                    oldTicket.Expiration,
                    oldTicket.IsPersistent,
                    cookieData.ToCookieString()
                    );
                userInfoCookie.Value = FormsAuthentication.Encrypt(newTicket);
                
                ctx.Response.Cookies.Add(userInfoCookie);
            }

            var addr = new MailAddress(userName).User.ToLower();
            var bkavUserCookie = ctx.Request.Cookies[connectionSettings.BkavSSOCookieUsername];
            if (bkavUserCookie == null || bkavUserCookie.Value.ToLower() != addr)
            {
                bkavUserCookie = new HttpCookie(connectionSettings.BkavSSOCookieUsername);
                bkavUserCookie.Domain = connectionSettings.BkavSSOParentDomain;
                bkavUserCookie.Expires = bkavAuthCookie.Expires;
                bkavUserCookie.Value = addr;
                //bkavUserCookie.Secure = true;
                ctx.Response.Cookies.Add(bkavUserCookie);
            }
        }

        private void ClearCookie(HttpContextBase ctx, SsoSettings connectionSettings)
        {
            ctx.Response.Cookies.Add(new HttpCookie(connectionSettings.UserInfoCookie)
            {
                Expires = DateTime.Now.AddDays(-1),
                //Secure = true
            });
            ctx.Response.Cookies.Add(new HttpCookie(connectionSettings.BkavSSOCookieName)
            {
                Domain = connectionSettings.BkavSSOParentDomain,
                Expires = DateTime.Now.AddDays(-1),
                //Secure = true
            });
            ctx.Response.Cookies.Add(new HttpCookie(connectionSettings.BkavSSOCookieUsername)
            {
                Domain = connectionSettings.BkavSSOParentDomain,
                Expires = DateTime.Now.AddDays(-1),
                //Secure = true
            });

            ctx.Response.AddHeader("IsAuthenticated", "false");
        }

        private string GetMacCookie(HttpContextBase ctx, out bool hasLimitDevice)
        {
            var macCookie = ctx.Request.Cookies["user_mac"];
            var hldCookie = ctx.Request.Cookies["hld"];
            hasLimitDevice = hldCookie == null ? false : hldCookie.Value == "True";
            return macCookie == null ? "" : macCookie.Value;
        }

        public string GetUserNameFromValaToken(HttpContextBase ctx)
        {
            var tokenCookie = ctx.Request.Cookies.Get("token");
            var tokenstring = tokenCookie == null? "" : tokenCookie.Value;
            if (string.IsNullOrEmpty(tokenstring))
            {
                return "";
            }

            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(tokenstring);

            return "";//token.Payload["email"].ToString();
        }

    }
}
