
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Validation;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.SingleSignOnService;
using Bkav.eGovCloud.Web.Framework;
using Bkav.SsoHelper;
using CaptchaMvc.HtmlHelpers;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using StackExchange.Profiling;
using System.Net.Security;
using System.Net;
using System.Collections.Specialized;
using Bkav.eGovCloud.Core.Logging;
using System.Dynamic;
//using System.IdentityModel.Tokens.Jwt;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class AccountController : CustomerBaseController
    {
        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly PasswordPolicySettings _passwordPolicySettings;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly DocTypeBll _docTypeService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly SignatureBll _signatureService;
        private readonly UserConnectionBll _userConnectionService;
        private readonly AuthorizeBll _authorizeService;
        private readonly DocFieldBll _docFieldService;
        private readonly SsoSettings _ssoSettings;
        private readonly ConnectionSettings _connectionSettings;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly RoleBll _roleService;
        private readonly PermissionBll _permissionService;
        private readonly LanguageSettings _languageSettings;
        private readonly PrinterBll _printerService;
        private readonly MemoryCacheManager _cache;
        private readonly MobileDeviceBll _mobileDeviceService;
        private readonly NotificationSettings _notificationSettings;
        private readonly InfomationBll _infoService;
        private readonly OtpBll _otpService;
        private readonly TemplateBll _templateService;
        private SendSmsHelper _smsHelper;
        private SendEmailHelper _mailHelper;
        private OtpSettings _otpSettings;
        private SmsBll _smsService;
        private readonly NotificationBll _notificationService;
        private SSOSettings _sSOSettings;

        public AccountController(UserBll userService,
                                ResourceBll resourceService,
                                AuthenticationSettings authenticationSettings,
                                PasswordPolicySettings passwordPolicySettings,
                                FileUploadSettings fileUploadSettings,
                                DocTypeBll docTypeService,
                                AdminGeneralSettings generalSettings,
                                SignatureBll signatureService,
                                UserConnectionBll userConnectionService,
                                AuthorizeBll authorizeService,
                                DocFieldBll docFieldService,
                                RoleBll roleService,
                                PermissionBll permissionService,
                                InfomationBll infoService,
                                Helper.UserSetting helperUserSetting,
                                LanguageSettings languageSettings,
                                ConnectionSettings connectionSettings,
                                NotificationSettings notificationSettings,
                                PrinterBll printerService,
                                MobileDeviceBll mobileDeviceService,
                                MemoryCacheManager cache,
                                OtpBll otpService,
                                TemplateBll templateService,
                                SmsBll smsService,
                                OtpSettings otpSettings,
                                NotificationBll notificationService,
                                SSOSettings sSOSettings)
        {
            _userService = userService;
            _resourceService = resourceService;
            _authenticationSettings = authenticationSettings;
            _passwordPolicySettings = passwordPolicySettings;
            _fileUploadSettings = fileUploadSettings;
            _docTypeService = docTypeService;
            _generalSettings = generalSettings;
            _signatureService = signatureService;
            _userConnectionService = userConnectionService;
            _authorizeService = authorizeService;
            _docFieldService = docFieldService;
            _roleService = roleService;
            _permissionService = permissionService;
            _helperUserSetting = helperUserSetting;
            _languageSettings = languageSettings;
            _connectionSettings = connectionSettings;
            _printerService = printerService;
            _infoService = infoService;
            _mobileDeviceService = mobileDeviceService;
            _notificationSettings = notificationSettings;
            _cache = cache;
            _ssoSettings = SsoSettings.Instance;
            _smsHelper = DependencyResolver.Current.GetService<SendSmsHelper>();
            _mailHelper = DependencyResolver.Current.GetService<SendEmailHelper>();
            _otpService = otpService;
            _templateService = templateService;
            _otpSettings = otpSettings;
            _smsService = smsService;
            _sSOSettings = sSOSettings;
            _notificationService = notificationService;
        }

        #region đăng nhập

        #region Login Normal

        public ActionResult LoginByApp(string userName, string token, string url = "")
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var user = _userService.Get(userName);
                if (user != null && token == "bc6cc915-616c-4ba6-a5a1-1bc29ad9303f")
                {
                    ClearCookie();

                    var cookieData = new CustomerCookieData
                    {
                        UsernameWithDomain = user.UsernameEmailDomain,
                        UserId = user.UserId,
                        Email = user.UsernameEmailDomain,
                        FullName = user.FullName
                    };
                    AddCookie(user.UsernameEmailDomain, false, cookieData, out token);
                    if (!string.IsNullOrEmpty(url))
                    {
                        return Redirect(url);
                    }
                    return RedirectToAction("Index", "Mobile");
                }
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public JsonResult LogoutReturn(SSOLogout data)
        {
           
            //dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
            var logout_token = data.logout_token;
            var username = "";
            long exp = 0;
            var appSettings = ConfigurationManager.AppSettings;
            var JWTExponent = appSettings["JWTExponent"] ?? "";
            var JWTModulus = appSettings["JWTModulus"] ?? "";
            if (logout_token != null)
            {
                JWTUtil.DoLoginJWT_VerifySignature(logout_token, out username, JWTModulus, JWTExponent, out exp);
            }
            return Json(new { username= username, exp = exp }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoginSSO(string returnUrl)
        {
            //su dung sso de login 

            if (_sSOSettings.IsActive && !string.IsNullOrWhiteSpace(_sSOSettings.ApiUrl))
            {
                //
                string backUrl = string.Format("{0}{2}={1}", Request.GetFullDomainUrl(), returnUrl, _sSOSettings.CallBackUrl);

                string url = string.Format("{2}/oauth2/authorize?response_type=code&scope=openid&client_id={0}&redirect_uri={1}", _sSOSettings.ClientID, backUrl, _sSOSettings.ApiUrl);

                if (!string.IsNullOrEmpty(_sSOSettings.Type))
                {
                    if (_sSOSettings.Type == "YB")
                    {
                        backUrl = string.Format("{0}{2}", Request.GetFullDomainUrl(), returnUrl, _sSOSettings.CallBackUrl);
                        url = string.Format("{2}/oauth2/authorize?response_type=code&scope=openid&acr_values=LoA1&client_id={0}&redirect_uri={1}", _sSOSettings.ClientID, backUrl, _sSOSettings.ApiUrl);
                    }
                }
                return Redirect(url);
            }
            else
                return Redirect("Login?returnUrl=" + returnUrl);
        }
        public ActionResult LoginSSOCallBack(string returnUrl, string code, string session_state)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(session_state))
                return RedirectToAction("LoginSSO");
            try
            {
                ClearCookie();
                //luu code va state
                var sso_state = new HttpCookie("sso_state");
                sso_state.Value = session_state;
                InitCookie(sso_state, true);
                //
                string backUrl = string.Format("{0}{2}={1}", Request.GetFullDomainUrl(), returnUrl, _sSOSettings.CallBackUrl);
                if (!string.IsNullOrEmpty(_sSOSettings.Type))
                {
                    if (_sSOSettings.Type == "YB")
                    {
                        backUrl = string.Format("{0}{2}", Request.GetFullDomainUrl(), returnUrl, _sSOSettings.CallBackUrl);
                    }
                }
                ServicePointManager.ServerCertificateValidationCallback = new
                    RemoteCertificateValidationCallback(delegate { return true; });
                using (WebClient wc = new WebClient())
                {
                    NameValueCollection values = new NameValueCollection();
                    values.Add("grant_type", "authorization_code");
                    values.Add("code", code);
                    values.Add("redirect_uri", backUrl);
                    string token = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _sSOSettings.ClientID, _sSOSettings.SecretKey)));
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers[HttpRequestHeader.Authorization] = "Basic " + token;
                    string stoken = "";
                    int expire = 0;
                    try
                    {

                        byte[] result = wc.UploadValues(_sSOSettings.ApiUrl + "/oauth2/token", "POST", values);
                        StaticLog.Log(new List<string>() { "vao day:" + result });

                        dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(System.Text.Encoding.UTF8.GetString(result));
                        stoken = data.id_token;
                        int.TryParse(Convert.ToString(data.expires_in), out expire);
                    }
                    catch (Exception exx)
                    {
                        return RedirectToAction("LoginSSO");
                    }

                    if (!string.IsNullOrWhiteSpace(stoken))
                    {

                        var sso_code = new HttpCookie("sso_code");
                        sso_code.Value = stoken;
                        InitCookie(sso_code, true);

                        var token_news = stoken.Split('.');
                        string dataUser = "";
                        if (token_news.Length == 3)
                        {
                            dataUser = token_news[1];
                        }

                        // byte[] dataBytes = Convert.FromBase64String(dataUser);
                        // var dt = System.Text.Encoding.UTF8.GetString(dataBytes);
                        var dt = base64urlDecode(dataUser);

                        var dataSSO = Newtonsoft.Json.JsonConvert.DeserializeObject<SSOObject>(dt);
                        var dataSSOYB = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(dt);
                        StaticLog.Log(new List<string>() { dt, returnUrl });
                        var userName = dataSSO.sub;
                        if (!string.IsNullOrEmpty(_sSOSettings.Type))
                        {
                            if (_sSOSettings.Type == "YB")
                            {
                                var dataSSOs = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(dt);
                                var email = dataSSOYB["emails.work"];
                                StaticLog.Log(new List<string>() { Newtonsoft.Json.JsonConvert.SerializeObject(email) });

                                userName = Newtonsoft.Json.JsonConvert.SerializeObject(email).Replace("@yenbai.gov.vn", "").Trim('"'); ;

                            }
                        }
                        string loginToken = "";
                        StaticLog.Log(new List<string>() { userName });

                        var user = DoLoginSSO(userName, out loginToken, expire);
                        if (!string.IsNullOrEmpty(loginToken))
                        {
                            var allowDevice = CheckDevices(user, loginToken);
                            if (allowDevice)
                            {
                                _cache.Remove(CacheParam.UserCurrent);
                                if (_sSOSettings.Type == "YB")
                                {
                                    returnUrl = returnUrl.Replace(Request.GetFullDomainUrl(), "");
                                }
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                ClearCookie();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StaticLog.Log(new List<string>() { ex.Message });
            }
            return RedirectToAction("Login");
        }
        public string base64urlDecode(string encoded) {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(encoded.Replace("_", "/").Replace("-", "+") + new string('=', (4 - encoded.Length % 4) % 4)));
        }
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Request.IsMobileOrTablet() ? RedirectToRoute("Mobile") : RedirectToAction("", "", new { area = "" });
            }

            if (returnUrl != null && returnUrl.ToLower().Contains("http"))
            {
                returnUrl = "";
            }

            // Hiển thị thông tin đăng nhập gần nhất
            var userInfoCookie = Request.Cookies.Get(_ssoSettings.UserInfoCookie);
            if (userInfoCookie != null && !string.IsNullOrEmpty(userInfoCookie.Value))
            {
                var lastUserLogin = FormsAuthentication.Decrypt(userInfoCookie.Value);
                var userData = Json2.ParseAs<CustomerCookieData>(lastUserLogin.UserData);
                ViewBag.LastUserAvatar = _helperUserSetting.GetUserAvatar(userData.UsernameWithDomain.Split('@')[0]);
                ViewBag.LastUserInfo = userData;
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Domain = Request.GetDomainName();
            ViewBag.SsoDomain = _authenticationSettings.SingleSignOnDomain;
            ViewBag.ConnectionSettings = _connectionSettings;
            ViewBag.PasswordPolicySettings = _passwordPolicySettings;
            ViewBag.AllowUsersToAutomaticallyLogin = _authenticationSettings.AllowUsersToAutomaticallyLogin;
            ViewBag.DefaultDomain = _authenticationSettings.DefaultDomain;
            ViewBag.DocumentDomain = _ssoSettings.BkavSSOParentDomain;

            // Mặc dưới client sẽ lấy địa chỉ Mac khi đăng nhập
            // Đăng nhập mới check đến user
            ViewBag.LimitByMAC = true;

            var infomation = _infoService.GetCurrentOfficeName();
            ViewBag.OfficeName = infomation;

            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginModel model)
        {
            var useCaptcha = _passwordPolicySettings.EnableCaptcha;
            if (Session["UseCaptcha"] != null)
            {
                model.UseCaptcha = (bool)Session["UseCaptcha"];
            }
            if (useCaptcha && model.UseCaptcha && !this.IsCaptchaValid("Captcha is not valid"))
            {
                model.UseCaptcha = (bool)(Session["UseCaptcha"] ?? false);
            }
            if (useCaptcha && model.UseCaptcha && !this.IsCaptchaValid("Captcha is not valid"))
            {
                return Json(new { success = false, error = _resourceService.GetResource("Captcha.NotValid") });
            }
            Session["UseCaptcha"] = false;
            var isMobile = Request.Browser.IsMobileDevice;

#if QuanTriTapTrungEdition
            model.IsQTTT = true;
#endif

            try
            {
                var domainName = string.IsNullOrEmpty(_authenticationSettings.DefaultDomain) ? Request.GetDomainName() : _authenticationSettings.DefaultDomain;
                if (!model.Username.IsMatchEmail())
                {
                    model.Username = model.Username + "@" + domainName;
                }

                string loginToken;
                User user;

                // Kiểm tra đăng nhập super admin
                user = LoginSuperAdmin(model.Username, model.Password, domainName, model.RememberMe, out loginToken);

                if (user == null)
                {
                    if (model.IsQTTT)
                    {
                        user = LoginQTTT(model.Username, model.Password, model.RememberMe, out loginToken);
                    }
                    else
                    {
                        user = LoginDomainUser(model.Username, model.Password, domainName, model.RememberMe, out loginToken);
                    }
                }

                if (string.IsNullOrEmpty(loginToken))
                {
                    return Json(new { success = false, error = _resourceService.GetResource("User.Login.Failed") });
                }

                var allowDevice = CheckDevices(user, loginToken, model.MAC);

                if (!allowDevice)
                {
                    ClearCookie();
                    return Json(new { success = false, deviceNotAllowed = true, error = "Bạn không được quyền đăng nhập trên thiết bị  này." });
                }

                _cache.Remove(CacheParam.UserCurrent);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message == "EnableCaptcha")
                {
                    Session["UseCaptcha"] = true;
                }
                return Json(new { success = false, error = ex.Message });
            }
        }

        private User LoginSuperAdmin(string userName, string password, string domain, bool isRemember, out string token)
        {
            var superAdminAccount = ConfigurationManager.AppSettings["superAdminAccount"];
            var superAdminPassword = ConfigurationManager.AppSettings["superAdminPassword"];
            var domainName = string.IsNullOrEmpty(_authenticationSettings.DefaultDomain)
                                    ? Request.GetDomainName()
                                    : _authenticationSettings.DefaultDomain;

            token = "";

            if (string.IsNullOrEmpty(superAdminAccount) || string.IsNullOrEmpty(superAdminPassword))
            {
                return null;
            }

            if (!superAdminAccount.IsMatchEmail())
            {
                superAdminAccount = superAdminAccount + "@" + domainName;
            }

            // Kiem tra super admin
            if (userName != superAdminAccount || password != superAdminPassword)
            {
                return null;
            }

            var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var hash = Generate.GetInputPasswordHash(password, salt);
            var superAdmin = _userService.Get(userName);
            if (superAdmin == null)
            {
                var now = DateTime.Now;
                superAdmin = new User
                {
                    Username = userName,
                    UsernameEmailDomain = userName,
                    DomainName = domain,
                    FirstName = userName,
                    LastName = userName,
                    FullName = userName,
                    Gender = true,
                    IsLockedOut = false,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedOnDate = now,
                    VersionDateTime = now,
                    PasswordLastModifiedOnDate = now,
                    IsActivated = true,
                    RoleIds = _roleService.GetAllRoleIds().ToList(),
                    IgnorePermissionIds = _permissionService.GetAllPermissonsInSystem().Select(d => d.PermissionId).ToList(),
                    DenyPermissionIds = new List<int>(),
                    DepartmentJobTitlesId = new List<string>(),
                    GrantPermissionIds = new List<int>()
                };
                _userService.Create(superAdmin);
            }

            var cookieData = new CustomerCookieData
            {
                UsernameWithDomain = superAdmin.UsernameEmailDomain,
                UserId = superAdmin.UserId,
                Email = superAdmin.UsernameEmailDomain,
                FullName = superAdmin.FullName,
            };

            AddCookie(superAdmin.UsernameEmailDomain, isRemember, cookieData, out token, true);
            CreateActivityLog(ActivityLogType.DangNhap, string.Format(_resourceService.GetResource("Account.Login.Success"), superAdmin.UsernameEmailDomain), superAdmin.UsernameEmailDomain, superAdmin.UserId);

            return superAdmin;
        }

        private User LoginDomainUser(string userName, string password, string domain, bool isRemember, out string token)
        {
            var user = _authenticationSettings.EnableLdap
                                ? _userService.DoAuthenticateLdap(userName, password)
                                    : _authenticationSettings.UseLoginMail
                                        ? _userService.DoAuthenticatePOP3IMAP(userName, password)
                                        : _userService.DoAuthenticate(userName, password);

            if (user == null)
            {
                token = "";
                return null;
            }

            var cookieData = new CustomerCookieData
            {
                UsernameWithDomain = user.UsernameEmailDomain,
                UserId = user.UserId,
                Email = user.UsernameEmailDomain,
                FullName = user.FullName
            };

            AddCookie(user.UsernameEmailDomain, isRemember, cookieData, out token);

            CreateActivityLog(ActivityLogType.DangNhap, string.Format(_resourceService.GetResource("Account.Login.Success"), user.UsernameEmailDomain), user.UsernameEmailDomain, user.UserId);

            return user;
        }

        private User LoginQTTT(string userName, string passWord, bool remember, out string token)
        {
            LoginStatus status = null;
            User user = null;
            token = "";

            user = _authenticationSettings.EnableLdap
                              ? _userService.DoAuthenticateLdap(userName, passWord)
                                  : _authenticationSettings.UseLoginMail
                                      ? _userService.DoAuthenticatePOP3IMAP(userName, passWord)
                                      : _userService.DoAuthenticate(userName, passWord);
            status = new LoginStatus() { Success = user != null };

            if (!status.Success)
            {
                return null;
            }

            var userConnection = DbConnectionHelper.GetDbConnection(userName, System.Web.HttpContext.Current.Request.GetDomainAndPort());

            var dbConnection = new StackExchange.Profiling.Data.EFProfiledDbConnection(
                                ConnectionUtil.TestConnection(userConnection.ConnectionRaw, null, null, null, null, null, userConnection.DatabaseTypeIdInEnum),
                                MiniProfiler.Current);

            using (var context = new EfContext(dbConnection))
            {
                var userDal = context.GetRepository<User>();
                user = userDal.Get(true, u => u.UsernameEmailDomain.Equals(userName, StringComparison.OrdinalIgnoreCase));
                if (user != null)
                {
                    var cookieData = new CustomerCookieData
                    {
                        UsernameWithDomain = user.UsernameEmailDomain,
                        UserId = user.UserId,
                        Email = user.UsernameEmailDomain,
                        FullName = user.FullName
                    };

                    AddCookie(user.UsernameEmailDomain, remember, cookieData, out token);
                }
            }

            return user;
        }

        private bool CheckDevices(User user, string token, string mac = null)
        {
            var clientIp = Request.GetIP();
            var clientOs = Request.GetOS();
            var clientBrowser = Request.Browser.Type;
            var isMobile = Request.Browser.IsMobileDevice;
            var deviceName = Request.GetOSName();

            var userSettingModel = _helperUserSetting.GetUserSetting(user);
            var hasLimitDevice = user.HasLimitByMac ?? _authenticationSettings.LimitByMAC;

            var serial = mac ?? GetMacCookie();
            if (string.IsNullOrEmpty(serial))
            {
                // Trường hợp người dùng đăng nhập lần đầu tiên vào hệ thống.
                // Và thiết bị chưa cài Mac => tự sinh 1 token gắn với máy đó.
                // Token này được gắn vào cookie.
                // Trong các lần đăng nhập tiếp theo => update Mac nếu lấy dc.
                serial = Guid.NewGuid().ToString("N");
            }

            var device = new MobileDevice()
            {
                UserId = user.UserId,
                CreatedDate = DateTime.Now,
                Browser = clientBrowser,
                DeviceName = deviceName,
                IP = clientIp,
                LastUpdate = DateTime.Now,
                LoginToken = token,
                OS = clientOs,
                Serial = serial ?? "",
                Token = ""
            };

            var result = _mobileDeviceService.CheckDevices(user.UserId, device, hasLimitDevice, isMobile, currentToken: GetMacCookie());
            if (result != null)
            {
                var hldCookie = new HttpCookie("hld");
                hldCookie.Value = hasLimitDevice.ToString();
                InitCookie(hldCookie, true);

                SetMacCookie(serial);
            }

            return result != null && result.IsActive;
        }
        private User DoLoginSSO(string userName, out string token, int expire)
        {
            var user = _userService.DoAuthenticateSSO(userName);

            if (user == null)
            {
                token = "";
                return null;
            }

            var cookieData = new CustomerCookieData
            {
                UsernameWithDomain = user.UsernameEmailDomain,
                UserId = user.UserId,
                Email = user.UsernameEmailDomain,
                FullName = user.FullName
            };

            AddCookie(user.UsernameEmailDomain, false, cookieData, out token, false);

            PushNotifies(user);

            CreateActivityLog(ActivityLogType.DangNhap, string.Format(_resourceService.GetResource("Account.Login.Success"), user.UsernameEmailDomain), user.UsernameEmailDomain, user.UserId);

            return user;
        }
        #endregion

        #region Login OpenId

        public ActionResult LoginViaOpenId()
        {
            if (_authenticationSettings.EnableOpenId)
            {
                using (var openId = new OpenIdRelyingParty())
                {
                    var response = openId.GetResponse();
                    if (response != null)
                    {
                        switch (response.Status)
                        {
                            case AuthenticationStatus.Authenticated:
                                var user = _userService.GetByOpenId(response.ClaimedIdentifier, true);
                                if (user == null)
                                {
                                    ModelState.AddModelError(string.Empty, _resourceService.GetResource("User.Login.OpenID.Failed"));
                                }
                                else
                                {
                                    var client = GetPartnerClient();
                                    var token = client.WriteTokenIsAuthenticated(user.Username);
                                    ViewBag.Token = token;
                                    ViewBag.Domain = Request.GetDomainName();
                                }
                                break;

                            case AuthenticationStatus.Canceled:
                                ModelState.AddModelError(string.Empty, _resourceService.GetResource("User.Login.OpenID.Canceled"));
                                break;

                            case AuthenticationStatus.Failed:
                                ModelState.AddModelError(string.Empty, _resourceService.GetResource("User.Login.OpenID.Failed"));
                                break;
                        }
                    }
                }
                ViewBag.SsoDomain = _authenticationSettings.SingleSignOnDomain;
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult LoginViaOpenId(string loginIdentifier)
        {
            if (!Identifier.IsValid(loginIdentifier))
            {
                ModelState.AddModelError(string.Empty, _resourceService.GetResource("User.Login.OpenID.Identifier.IsNotValid"));
                return View();
            }

            using (var openId = new OpenIdRelyingParty())
            {
                var request = openId.CreateRequest(Identifier.Parse(loginIdentifier));
                request.AddExtension(new ClaimsRequest
                {
                    BirthDate = DemandLevel.NoRequest,
                    Email = DemandLevel.Require,
                    FullName = DemandLevel.Require
                });
                return request.RedirectingResponse.AsActionResult();
            }
        }

        #endregion

        #region Login Ldap

        [HttpPost]
        public JsonResult LoginViaLdap(string userName, string passWord)
        {
            var token = string.Empty;
            User user = null;
            var isAuthenticate = false;
            try
            {
                user = _userService.DoAuthenticateLdap(userName, passWord);
                isAuthenticate = user != null;
            }
            catch (EgovException ex)
            {
                CreateActivityLog(ActivityLogType.DangNhap, string.Format("Đăng nhập với tài khoản {0} không thành công", userName), string.Empty, null);
                return Json(new { token, success = false, message = ex.Message });
            }
            if (isAuthenticate)
            {
                var client = GetPartnerClient();
                token = client.WriteTokenIsAuthenticated(user.UsernameEmailDomain);
                client.Close();
            }
            else
            {
                CreateActivityLog(ActivityLogType.DangNhap, string.Format("Đăng nhập với tài khoản {0} không thành công", userName), string.Empty, null);
            }
            return Json(new { token, success = isAuthenticate, message = string.Empty });
        }

        #endregion

        public void PushNotifies(User currentUser)
        {
            var userId = currentUser.UserId;
            var notify = _notificationService.GetWarningNotify(userId);
            if (notify == null)
                return;

            var connections = _userConnectionService.Gets(userId).Select(c => c.UserConnectionId);
            var deviceTokens = _mobileDeviceService.GetsFromCache(userId).Select(d => d.Token);
            notify.ConnectionIds = connections;
            notify.DeviceIds = deviceTokens;

            notify.SenderAvatar = _helperUserSetting.GetUserAvatar(currentUser.Username);

            _notificationService.Queue(new List<Business.Objects.Notify>() { notify });
        }

        public ActionResult NotAllowedDevice(string u)
        {
            var infomation = _infoService.GetCurrentOfficeName();
            ViewBag.OfficeName = infomation;
            ViewBag.UserName = u;

            return View();
        }

        #endregion đăng nhập

        #region Đăng xuất

        public ActionResult Logout()
        {
            var userId = User.GetUserId();
            if (userId > 0)
            {
                var mac = GetMacCookie();

                // Logout thiết bị
                _mobileDeviceService.LogOut(userId, mac);

                CreateActivityLog(ActivityLogType.DangXuat, string.Format("{0} đăng xuất khỏi hệ thống", User.GetUserNameWithDomain()), User.GetUserNameWithDomain(), User.GetUserId());
                Session.Clear();
            }

            var sso_code = Request.Cookies["sso_code"] == null ? "" : Request.Cookies["sso_code"].Value;
            var sso_state = Request.Cookies["sso_state"] == null ? "" : Request.Cookies["sso_state"].Value;

            Session.Clear();
            ClearCookie();

#if QuanTriTapTrungEdition

            ViewBag.Domain = Request.GetDomainName();
            ViewBag.SsoDomain = _authenticationSettings.SingleSignOnDomain;
            ViewBag.Username = User.GetUserName();
#endif
            if (!_sSOSettings.IsActive)
            {
                return Redirect("/Account/Login");
            }
            if (!string.IsNullOrWhiteSpace(sso_state) && !string.IsNullOrWhiteSpace(sso_code))
            {
                var urlLogout = string.Format("{0}/oidc/logout?id_token_hint={1}&state={2}&post_logout_redirect_uri={3}{4}",
                    _sSOSettings.ApiUrl, sso_code, sso_state, Request.GetFullDomainUrl(), _sSOSettings.CallBackUrl + "=");
                if (_sSOSettings.Type == "YB")
                {
                    urlLogout = string.Format("{0}/oidc/logout?id_token_hint={1}&state={2}&post_logout_redirect_uri={3}{4}",
                    _sSOSettings.ApiUrl, sso_code, sso_state, Request.GetFullDomainUrl(), _sSOSettings.CallBackUrl);
                }
                StaticLog.Log(new List<string>() { urlLogout });
                return Redirect(urlLogout);
            }
            return View();
        }

        [HttpPost]
        public JsonResult LogoutNormal()
        {
            Session.Clear();

#if QuanTriTapTrungEdition
            FormsAuthentication.SignOut();
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName) { Expires = DateTime.Now.AddDays(-1) });
#endif
            foreach (var cookieName in CookieName.AllCookieName)
            {
                var cookie = Request.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }

            var ssoCookie = new HttpCookie(_ssoSettings.BkavSSOCookieName);
            ssoCookie.Domain = _ssoSettings.BkavSSOParentDomain;
            ssoCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(ssoCookie);

            return Json(new { success = true });
        }

        public JsonpResult SignOut()
        {
            Session.Clear();
            //FormsAuthentication.SignOut();
            //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName) { Expires = DateTime.Now.AddDays(-1) });
            foreach (var cookieName in CookieName.AllCookieName)
            {
                var cookie = Request.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }
            return this.Jsonp(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Cập nhật giá trị cookie

        [HttpPost]
        public void RemoveCookie(string name)
        {
            var cookie = Request.Cookies[name];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
        }

        [HttpPost]
        public void CreateCookie(string name, string value)
        {
            //newBkavAuthen vs bkavAuthen
            if (name.ToLower().Contains(_ssoSettings.BkavSSOCookieName.ToLower()))
            {
                var user = _userService.CurrentEditableUser;
                var userSetting = user.NotifyInfoModel;

                userSetting.MailLastestToken = value;

                var json = userSetting.StringifyJs();
                _userService.UpdateNotifyInfo(json, user);
            }
            var cookie = new HttpCookie(name);
            cookie.Value = value;
            InitCookie(cookie, true);
        }

        #endregion

        #region thay đổi mật khẩu

        /// <summary>
        /// get:thay đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            ViewBag.MailType = _connectionSettings.MailType;
            ViewBag.MailLogin = _authenticationSettings.UseLoginMail;
            var user = _userService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        /// <summary>
        /// Post thay đổi mật khẩu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "ChangePassword")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.CurrentPassword.Equals(model.NewPassword, StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new Exception(_resourceService.GetResource("Account.ChangePassword.IsEqual"));
                    }
#if QuanTriTapTrungEdition
                    using (var transactionUser = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        if (_userService.ChangePassword(User.GetUserId(), model.CurrentPassword, model.NewPassword))
                        {
                            using (var transactionService = new TransactionScope(TransactionScopeOption.Required))
                            {
                                var client = GetCustomerClient();
                                var status = client.ChangePassword(User.GetUserNameWithDomain(),
                                                                   model.CurrentPassword,
                                                                   model.NewPassword,
                                                                   _passwordPolicySettings.EnableHistory,
                                                                   _passwordPolicySettings.HistoryCount);

                                if (status.Success)
                                {
                                    SuccessNotification(_resourceService.GetResource("Account.ChangePassword.Success"));
                                }
                                else
                                {
                                    ErrorNotification(status.Message);
                                }
                                transactionService.Complete();
                            }
                        }
                        transactionUser.Complete();
                    }
#else
                    _userService.ChangePassword(User.GetUserId(), model.CurrentPassword, model.NewPassword);
#endif
                    return View(model);
                }
                catch (EgovException ex)
                {
                    ErrorNotification(ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        #endregion thay đổi mật khẩu

        #region thông tin cá nhân

        /// <summary>
        /// Get:thông tin cá nhân
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProfileConfig()
        {
            var user = _userService.CurrentEditableUser;
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            ShowAvatar(user.Username, user.DomainName);

            ViewBag.GenderList = GendersList();
            ViewBag.Status = "";

            var model = user.ToModel();
            model.Code = "";
            model.Email = user.Email;
            model.Phone = user.Phone;
            return View(model);
        }

        /// <summary>
        /// post:thông tin cá nhân
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "ProfileConfig")]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileConfig(UserProfileModel model)
        {
            var user = _userService.CurrentEditableUser;

            if (!string.IsNullOrEmpty(model.Email) && !IsValidEmail(model.Email))
            {
                // Message
                ViewBag.EmailValidate = "Email không đúng định dạng";
                ViewBag.GenderList = GendersList();

                ShowAvatar(user.Username, user.DomainName);
                return View(model);
            }

            user = model.ToEntity(user);
            user.LastModifiedByUserId = User.GetUserId();
            user.LastModifiedOnDate = DateTime.Now;
            var fullName = model.LastName + " " + model.FirstName;
            try
            {
                _userService.UpdateUserProfile(fullName, model.FirstName, model.LastName, model.Gender, model.Phone,
                                                model.Email, model.Fax, model.Address);

#if QuanTriTapTrungEdition
                var client = GetCustomerClient();
                client.UpdateUser(user.Username,
                                        fullName,
                                        model.Gender,
                                        model.Phone,
                                        model.Fax,
                                        model.Address,
                                        user.OpenId,
                                        user.IsActivated,
                                        user.DomainName);
                client.Close();
#endif
            }
            catch (EgovException ex)
            {
                ErrorNotification(ex.Message);
                ViewBag.GenderList = GendersList();
                ShowAvatar(user.Username, user.DomainName);
                return View(model);
            }

            SuccessNotification(_resourceService.GetResource("User.Updated"));
            ViewBag.GenderList = GendersList();
            ShowAvatar(user.Username, user.DomainName);
            return View(model);
        }

        /// <summary>
        /// cập nhật thông tin người dùng
        /// </summary>
        /// <param name="fullName">họ và tên</param>
        /// <param name="firstName">tên</param>
        /// <param name="lastName">họ và tên đẹm</param>
        /// <param name="gender">giới tính</param>
        /// <param name="phone">Số diện thoại</param>
        /// <param name="email">email</param>
        /// <param name="fax">số fax</param>
        /// <param name="address">địa chỉ</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateProfileConfig(string fullName,
                                            string firstName,
                                            string lastName,
                                            bool gender,
                                            string phone,
                                            string email,
                                            string fax,
                                            string address)
        {
            var user = _userService.CurrentEditableUser;
            if (ModelState.IsValid)
            {
                var model = new UserProfileModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = gender,
                    Phone = phone,
                    Email = email,
                    Fax = fax,
                    Address = address
                };
                user = model.ToEntity(user);
                user.LastModifiedByUserId = User.GetUserId();
                user.LastModifiedOnDate = DateTime.Now;
                try
                {
                    _userService.UpdateUserProfile(fullName,
                                                    firstName,
                                                    lastName,
                                                    gender,
                                                    phone,
                                                    email,
                                                    fax,
                                                    address);
#if QuanTriTapTrungEdition
                    var client = GetCustomerClient();
                    client.UpdateUser(user.Username,
                                    fullName,
                                    gender,
                                    phone,
                                    fax,
                                    address,
                                    user.OpenId,
                                    user.IsActivated,
                                    user.DomainName);
                    client.Close();
#endif
                }
                catch (EgovException ex)
                {
                    ErrorNotification(ex.Message);
                    ShowAvatar(user.Username, user.DomainName);
                    return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                SuccessNotification(_resourceService.GetResource("User.Updated"));
                ShowAvatar(user.Username, user.DomainName);
                return Json(new { message = _resourceService.GetResource("User.Updated") }, JsonRequestBehavior.AllowGet);
            }
            ShowAvatar(user.Username, user.DomainName);
            return Json(new { message = _resourceService.GetResource("User.UpdateError") }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// upload ảnh lên server
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AvatarUpload()
        {
            HttpPostedFileBase file = Request.Files["file"];
            var user = _userService.CurrentEditableUser;
            bool success = false;
            if (file != null && file.ContentLength > 0)
            {
                if (file.InputStream.Length > _fileUploadSettings.ProfilePictureMaximumSizeBytes)
                {
                    return Json(new { success, message = _resourceService.GetResource("Account.Avartar.Fields.FileUploadMaximumSizeBytes") + " (" + _fileUploadSettings.ProfilePictureMaximumSizeBytes + " KB)" });
                }

                var ext = Path.GetExtension(file.FileName);
                if (!_fileUploadSettings.ProfilePictureAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
                {
                    return Json(new { success, message = _resourceService.GetResource("Account.Avartar.Fields.FileUploadAllowedExtensions") });
                }

                var path = string.Format(CommonHelper.MapPath(_generalSettings.Avatar), user.Username + "_" + user.DomainName);

                Helper.ResizeAndCropImage.CropAndCropResizeImage(file.InputStream,
                                                                _fileUploadSettings.ProfilePictureMaximumWidth,
                                                                _fileUploadSettings.ProfilePictureMaximumHeight,
                                                                path);
                success = true;

                var avatarUrl = string.Format(_generalSettings.Avatar, user.Username + "_" + user.DomainName) + "?v=" + DateTime.Now.ToString("ddmmyyyyhhmmss");
                SaveUserAvatar(user, avatarUrl);

                return Json(new { success, Avatar = avatarUrl });
            }

            return Json(new { success, message = "Error" });
        }

        private void SaveUserAvatar(User user, string avatar)
        {
            var userSettingModel = _helperUserSetting.GetUserCurrentSetting(true);
            if (userSettingModel != null)
            {
                userSettingModel.Avatar = avatar;
            }

            var json = userSettingModel.StringifyJs();
            _userService.UpdateUserSetting(json);
        }

        [HttpPost]
        public JsonResult SaveAbsent(bool hasAbsent, string startAbsent, string endAbsent)
        {
            var userSettingModel = _helperUserSetting.GetUserCurrentSetting(true);
            if (userSettingModel != null)
            {
                userSettingModel.HasAbsent = hasAbsent;
                userSettingModel.StartAbsent = startAbsent;
                userSettingModel.EndAbsent = endAbsent;
            }

            var user = _userService.CurrentEditableUser;
            user.UserSetting = userSettingModel.StringifyJs();
            user.Address = hasAbsent ? String.Format("Vắng mặt từ {0} đến {1}.", startAbsent, endAbsent) : "";

            _userService.SaveChanges();

            return Json(new { hasAbsent = hasAbsent, data = new { start = startAbsent, end = endAbsent } });
        }

        #endregion thông tin cá nhân

        #region Cấu hình User

        /// <summary>
        /// todo:lấy doctype sao lại để chỗ này phải để trong DocumentController
        ///     => ai làm chỗ  này thì sửa lại
        /// Lấy văn bản mặc định của người dùng
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDocDefaultByUser()
        {
            var userId = User.GetUserId();
            return Json(_docTypeService.GetsByUserId(userId).Select(p => new { p.DocTypeId, p.DocTypeName }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get:cấu hình người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserSetting()
        {
            string documentProfilesKey = String.Empty;
            string generalConfigsJson = String.Empty;
            ViewBag.ShortKey = GetShortKey();

            //hồ sơ văn bản
            var model = _helperUserSetting.GetUserCurrentSetting();
            if (model != null)
            {
                if (model.DocumentProfilesKey != null && model.DocumentProfilesKey.Any())
                {
                    documentProfilesKey = model.DocumentProfilesKey.Stringify();
                }
                ViewBag.DocumentProfilesList = documentProfilesKey;

                //các phím mặc định
                if (model.GeneralKeyConfigs != null && model.GeneralKeyConfigs.Any())
                {
                    generalConfigsJson = model.GeneralKeyConfigs.Stringify();
                }
                ViewBag.GeneralConfigsList = generalConfigsJson;
                return View(model);
            }

            ViewBag.DocumentProfilesList = documentProfilesKey;
            ViewBag.GeneralConfigsList = generalConfigsJson;
            return View();
        }

        /// <summary>
        /// cập nhật cấu hình của người dùng
        /// </summary>
        /// <param name="newDocTypeShortKey"></param>
        /// <param name="newGeneralConfigsShortKey"></param>
        /// <returns></returns>
        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "UpdateUserSetting")]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateUserSetting(string newDocTypeShortKey, string newGeneralConfigsShortKey)
        {
            bool success = false;
            try
            {
                var documentDefaultSetting = new List<DocumentDefaultSetting>();
                var generalConfigsSetting = new List<GeneralConfigsSetting>();
                int countDocDefault = 0;

                if (!string.IsNullOrEmpty(newDocTypeShortKey))
                {
                    documentDefaultSetting = Json2.ParseAs<List<DocumentDefaultSetting>>(newDocTypeShortKey);
                    countDocDefault = documentDefaultSetting.Count();
                    if (countDocDefault > 0)
                    {
                        if (documentDefaultSetting.Any(s => string.IsNullOrWhiteSpace(s.KeyName) || s.KeyName.Length > 1))
                        {
                            return Json(new { success, messageError = _resourceService.GetResource("UserSetting.KeyName.Null") });
                        }

                        for (int i = 0; i < countDocDefault - 1; i++)
                        {
                            for (int j = i + 1; j < countDocDefault; j++)
                            {
                                if (documentDefaultSetting[i].Argument == documentDefaultSetting[j].Argument)
                                {
                                    return Json(new { success, messageError = _resourceService.GetResource("UserSetting.Argument.Exist") });
                                }
                                if (documentDefaultSetting[i].KeyName == documentDefaultSetting[j].KeyName && documentDefaultSetting[i].ShortKey == documentDefaultSetting[j].ShortKey)
                                {
                                    return Json(new { success, messageError = _resourceService.GetResource("UserSetting.ShortKeyAndKeyName.Exist") });
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(newGeneralConfigsShortKey))
                {
                    generalConfigsSetting = Json2.ParseAs<List<GeneralConfigsSetting>>(newGeneralConfigsShortKey);
                    var countGeneral = generalConfigsSetting.Count();
                    if (countGeneral > 0)
                    {
                        if (generalConfigsSetting.Any(s => string.IsNullOrWhiteSpace(s.KeyName) || s.KeyName.Length > 1))
                        {
                            return Json(new { success, messageError = _resourceService.GetResource("UserSetting.ShortKeyAndKeyName.Exist") });
                        }

                        for (int i = 0; i < countGeneral - 1; i++)
                        {
                            for (int j = i + 1; j < countGeneral; j++)
                            {
                                if (generalConfigsSetting[i].KeyName == generalConfigsSetting[j].KeyName && generalConfigsSetting[i].ShortKey == generalConfigsSetting[j].ShortKey)
                                {
                                    return Json(new { success, messageError = _resourceService.GetResource("UserSetting.ShortKeyAndKeyName.Exist") });
                                }
                            }
                        }

                        if (countDocDefault > 0)
                        {
                            for (int i = 0; i < countDocDefault; i++)
                            {
                                for (int j = i + 1; j < countGeneral; j++)
                                {
                                    if (documentDefaultSetting[i].KeyName == generalConfigsSetting[j].KeyName && documentDefaultSetting[i].ShortKey == generalConfigsSetting[j].ShortKey)
                                    {
                                        return Json(new { success, messageError = _resourceService.GetResource("UserSetting.ShortKeyAndKeyName.Exist") });
                                    }
                                }
                            }
                        }
                    }
                }

                var userSettingModel = _helperUserSetting.GetUserCurrentSetting(true);
                if (userSettingModel != null)
                {
                    userSettingModel.DocumentProfilesKey = documentDefaultSetting;
                    userSettingModel.GeneralKeyConfigs = generalConfigsSetting;
                }
                else
                {
                    userSettingModel = new UserSettingModel
                    {
                        DocumentProfilesKey = documentDefaultSetting,
                        GeneralKeyConfigs = generalConfigsSetting
                    };
                }

                var json = userSettingModel.StringifyJs();
                _userService.UpdateUserSetting(json);
                success = true;
                return Json(new { success, messageSuccess = _resourceService.GetResource("UserSetting.Update") });
            }
            catch (EgovException ex)
            {
                return Json(new { success, messageError = ex.Message });
            }
        }

        /// <summary>
        /// cấu hình: cho phép hiển thị tóm tăt văn bản, phân trang, số trang hiển thị trên trang chủ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GeneralSettings()
        {
            int listPageSizeHomeCount = _generalSettings.ListPageSizeHome.Count();
            int defaultPageSizeHome = _generalSettings.DefaultPageSizeHome;
            var defaultDisplayNotifyType = _notificationSettings.DocumentNotifyType;

            ViewBag.QuickViewTypes = GetDocumentQuickViewTypes();
            ViewBag.FontSizes = GetFontSizeSettings();
            ViewBag.MudimMethodType = GetMudimMethod();
            ViewBag.Printers = GetPrinters();
            var model = _helperUserSetting.GetUserCurrentSetting();
            if (model != null)
            {
                if (string.IsNullOrWhiteSpace(model.ListPageSizeHome) && listPageSizeHomeCount != 0)
                {
                    model.ListPageSizeHome = String.Join(",", _generalSettings.ListPageSizeHome);
                }
                if (model.DefaultPageSizeHome == 0)
                {
                    model.DefaultPageSizeHome = defaultPageSizeHome;
                }
            }
            else
            {
                //model = null => ???
                model = new UserSettingModel();
                model.DefaultPageSizeHome = defaultPageSizeHome;
                model.IsLoadPageScroll = _generalSettings.IsLoadPageScroll;
                if (listPageSizeHomeCount != 0)
                {
                    model.ListPageSizeHome = String.Join(",", _generalSettings.ListPageSizeHome);
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult ChangeTheme()
        {
            return View();
        }

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "GeneralSettings")]
		[ValidateAntiForgeryToken]
		public ActionResult GeneralSettings(UserSettingModel model)
		{
			ViewBag.QuickViewTypes = GetDocumentQuickViewTypes();
			ViewBag.FontSizes = GetFontSizeSettings();
			ViewBag.MudimMethodType = GetMudimMethod();
			ViewBag.Printers = GetPrinters();

			if (ModelState.IsValid)
			{
				var userSettingModel = _helperUserSetting.GetUserCurrentSetting(true);
				if (userSettingModel != null)
				{
					model.DocumentProfilesKey = userSettingModel.DocumentProfilesKey;
					model.GeneralKeyConfigs = userSettingModel.GeneralKeyConfigs;
				}

				var json = model.StringifyJs();
				_userService.UpdateUserSetting(json);
				SuccessNotification(_resourceService.GetResource("UserSetting.Update"));
				return View(model);
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult NotifySettings()
		{
			var userId = _userService.CurrentUser.UserId;

			LogException("NotifySettings " + userId);
			var setting = _helperUserSetting.GetNotifyInfo(userId);

			#region User's Devices

			setting.MobileDevices = _mobileDeviceService.Gets(x => x.UserId == userId);

			#endregion

			var model = Json2.ParseAs<NotifyInfoModel>(Json2.Stringify(setting));

			return View(model);
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "NotifySettings")]
		[ValidateAntiForgeryToken]
		public ActionResult NotifySettings(NotifyInfoModel model)
		{
			if (ModelState.IsValid)
			{
				var json = model.StringifyJs();
				_userService.UpdateNotifyInfo(json);

				SuccessNotification(_resourceService.GetResource("NotifySettings.Update"));
				return View(model);
			}

			return View(model);
		}

		public ActionResult TransferSettings()
		{
			var model = _helperUserSetting.GetUserCurrentSetting();
			if (string.IsNullOrWhiteSpace(model.TransferConfigs))
			{
				var cols = new List<TransferColumnConfig>
				{
					new TransferColumnConfig
				{
					TransferColumn = TransferColumn.UserName,
					IsActive = true,
					Position = 0
				},
					new TransferColumnConfig
					{
						TransferColumn = TransferColumn.Account,
						IsActive = true,
						Position = 1
					},
					new TransferColumnConfig
					{
						TransferColumn = TransferColumn.Position,
						IsActive = true,
						Position = 2
					},
					new TransferColumnConfig
				{
					TransferColumn = TransferColumn.Dept,
					IsActive = true,
					Position = 3
				}
				};
				ViewBag.TransferConfigs = Json2.Stringify(cols);
			}
			else
			{
				ViewBag.TransferConfigs = model.TransferConfigs;
			}
			return View();
		}

		//[HttpPost]
		//public ActionResult TransferSettings()
		//{

		//}
		[HttpPost]
		public JsonResult SetPopUpSize(int width, int height)
		{
			try
			{
				var model = _helperUserSetting.GetUserCurrentSetting(true);
				if (model != null)
				{
					model.PopUpWidth = width;
					model.PopUpHeight = height;
					var json = model.StringifyJs();
					_userService.UpdateUserSetting(json);
				}
				return Json(new { result = "success" });
			}
			catch
			{
				return Json(new { result = "error" });
			}
		}

		[HttpPost]
		public JsonResult PinDocType(Guid docTypeId)
		{
			try
			{
				var model = _helperUserSetting.GetUserCurrentSetting(true);
				if (model != null)
				{
					if (model.PinnedDocTypes == null)
					{
						model.PinnedDocTypes = new List<Guid>();
					}
					if (model.PinnedDocTypes.IndexOf(docTypeId) > -1)
					{
						model.PinnedDocTypes.Remove(docTypeId);
					}
					else
					{
						model.PinnedDocTypes.Add(docTypeId);
					}
					var json = model.StringifyJs();
					_userService.UpdateUserSetting(json);
				}
				return Json(true);
			}
			catch (Exception e)
			{
				return Json(new { error = true, message = e.Message });
			}
		}

		[HttpPost]
		public JsonResult SetUserConfig(SettingParameters setting)
		{
			try
			{
				var model = _helperUserSetting.GetUserCurrentSetting(true);

				if (setting.QuickView.HasValue)
				{
					model.QuickView = setting.QuickView.Value;
				}

				if (setting.FontSize.HasValue)
				{
					model.FontSize = setting.FontSize.Value;
				}

				if (setting.DisplayPopupTransferTheoLo.HasValue)
				{
					model.DisplayPopupTransferTheoLo = setting.DisplayPopupTransferTheoLo.Value;
				}

				if (setting.IgnoreConfirmRelation.HasValue)
				{
					model.IgnoreConfirmRelation = setting.IgnoreConfirmRelation.Value;
				}

				if (setting.PrinterId.HasValue)
				{
					model.PrinterId = setting.PrinterId.Value;
				}

				var json = model.StringifyJs();
				_userService.UpdateUserSetting(json);
			}
			catch (EgovException ex)
			{
				ErrorNotification(ex.Message);
			}

			return null;
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "AccountSetMobileUserConfig")]
		[ValidateAntiForgeryToken]
		public JsonResult SetMobileUserConfig(string startApp, int pageSize, int fontFamily, int notifyType, int fontSize,
			bool showAvatar, bool usePopup, string language)
		{
			try
			{
				var model = _helperUserSetting.GetUserCurrentSetting(true);
				model.MStartApp = startApp;
				model.MPageSize = pageSize;
				model.MFontFamily = (byte)fontFamily;
				model.MNotifyType = (byte)notifyType;
				model.MFontSize = (byte)fontSize;
				model.MUseAvatar = showAvatar;
				model.MUsePopup = usePopup;
				model.Language = (Language)Enum.Parse(typeof(Language), language, true);

				var json = model.StringifyJs();
				_userService.UpdateUserSetting(json);
				return Json(new { result = "success" });
			}
			catch (EgovException ex)
			{
				ErrorNotification(ex.Message);
				return Json(new { result = "error", info = ex.Message });
			}
		}

		#endregion Cấu hình User

		#region Chữ ký người dùng

		/// <summary>
		/// Lấy danh sách chữ ký của người dùng
		/// </summary>
		/// <returns></returns>
		public ActionResult Signature()
		{
			var userId = User.GetUserId();
			var model = _signatureService.Gets(p => p.UserId == userId);
			return View(model.ToListModel());
		}

		[HttpGet]
		public ActionResult CreateSignature()
		{
			ViewBag.IsCreate = true;
			return View(new SignatureModel());
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "CreateSignature")]
		[ValidateAntiForgeryToken]
		public ActionResult CreateSignature(SignatureModel model)
		{
			ViewBag.IsCreate = true;
			if (ModelState.IsValid)
			{
				if (model.IsTypeImage)
				{
					var imageBase64 = Request.Form["base64"];
					var exten = Request.Form["extension"];
					if (string.IsNullOrWhiteSpace(imageBase64))
					{
						ErrorNotification(_resourceService.GetResource("Account.Avartar.Fields.FileUploadAllowedExtensions"));
						return View(model);
					}
					model.Image = imageBase64;
					model.ImageExtension = exten;
				}

				var signature = model.ToEntity();
				try
				{
					signature.UserId = User.GetUserId();
					_signatureService.Create(signature);
					SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Signature.Created"));
					return RedirectToAction("Signature");
				}
				catch (EgovException ex)
				{
					ErrorNotification(ex.Message);
					return View(model);
				}
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult EditSignature(int id)
		{
			ViewBag.IsCreate = false;
			var signature = _signatureService.Get(id);
			if (signature == null)
			{
				ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Signature.NotFoundObject"));
				return RedirectToAction("Signature");
			}
			var model = signature.ToModel();
			return View(model);
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "EditSignature")]
		[ValidateAntiForgeryToken]
		public ActionResult EditSignature(SignatureModel model)
		{
			ViewBag.IsCreate = false;
			if (ModelState.IsValid)
			{
				try
				{
					var signature = _signatureService.Get(model.SignatureId);
					if (signature == null)
					{
						ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Signature.NotFoundObject"));
						return RedirectToAction("Signature");
					}

					if (model.IsTypeImage)
					{
						var base64 = Request.Form["base64"];
						var extension = Request.Form["extension"];
						if (!string.IsNullOrWhiteSpace(base64) && !string.IsNullOrWhiteSpace(extension))
						{
							model.Image = base64;
							model.ImageExtension = extension;
						}
						else if (string.IsNullOrEmpty(model.Image))
						{
							ErrorNotification(_resourceService.GetResource("Signature.Fields.SelectImage"));
							return View(model);
						}
					}
					else
					{
						model.Image = string.Empty;
						model.ImageExtension = string.Empty;
					}

					signature = model.ToEntity(signature);
					_signatureService.Update(signature);
					SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Signature.Updated"));
					return RedirectToAction("Signature");
				}
				catch (EgovException ex)
				{
					ErrorNotification(ex.Message);
					return View(model);
				}
			}
			return View(model);
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "DeleteSignature")]
		[ValidateAntiForgeryToken]
		public JsonResult DeleteSignature(int id)
		{
			var signature = _signatureService.Get(id);
			if (signature == null)
			{
				ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Signature.NotFoundObject"));
				return Json(new { success = false, message = _resourceService.GetResource("Bkav.eGovCloud.Signature.NotFoundObject") });
			}
			_signatureService.Delete(signature);
			return Json(new { success = true, message = _resourceService.GetResource("Bkav.eGovCloud.Signature.Deleted") });
		}

		[HttpPost]
		public JsonResult SignatureImage()
		{
			HttpPostedFileBase file = Request.Files["file"];
			bool success = false;
			if (file != null && file.ContentLength > 0)
			{
				if (file.InputStream.Length > _fileUploadSettings.FileUploadMaximumSizeBytes)
				{
					return Json(new { success, message = _resourceService.GetResource("Signature.Fields.FileUploadMaximumSizeBytes") });
				}

				var ext = Path.GetExtension(file.FileName);

				if (!_fileUploadSettings.ProfilePictureAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
				{
					return Json(new { success, message = _resourceService.GetResource("Signature.Fields.FileUploadAllowedExtensions") });
				}

				var extension = Path.GetExtension(file.FileName);
				var base64 = StreamImageToBase64(file.InputStream, extension);
				success = true;
				return Json(new { success, base64, extension });
			}

			return Json(new { success, message = _resourceService.GetResource("Signature.Fields.Error") });
		}

		#endregion Chữ ký người dùng

		#region ủy quyền

		public ActionResult Authorizes()
		{
			var model = _authorizeService.Gets(User.GetUserId());
			return View(model.ToListModel());
		}

		public ActionResult CreateAuthorize()
		{
			var model = new AuthorizeModel
			{
				AuthorizeUserId = User.GetUserId()
			};
			ViewBag.IsCreate = true;
			BindDataAuthorize();
			return View(model);
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "CreateAuthorize")]
		[ValidateAntiForgeryToken]
		public ActionResult CreateAuthorize(AuthorizeModel model)
		{
			ViewBag.IsCreate = true;
			if (ModelState.IsValid)
			{
				try
				{
					//Xư lý danh sách loại văn bản ủy quyền cho người nhận
					string docTypes = string.Empty;
					if (!string.IsNullOrWhiteSpace(model.DocTypeId))
					{
						var objdocType = Json2.ParseAs<List<Guid>>(model.DocTypeId);
						if (objdocType != null && objdocType.Any())
						{
							docTypes = model.DocTypeId;
						}
					}

					var authorize = model.ToEntity();
					authorize.DocTypeId = docTypes;
					authorize.Permission = 0;
					//Xử lý quyền thao tác văn bản đối với người ủy quyền
					if (model.Permissions != null && model.Permissions.Any())
					{
						foreach (var permission in model.Permissions)
						{
							authorize.Permission |= permission;
						}
					}
					_authorizeService.Create(authorize);
					SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Authorize.Created"));
					return RedirectToAction("Authorizes", "Account");
				}
				catch
				{
					ErrorNotification("Lấy danh sách loại văn bản lỗi");
					BindDataAuthorize();
					return View(model);
				}
			}

			BindDataAuthorize();
			return View(model);
		}

		[HttpGet]
		public ActionResult EditAuthorize(int id)
		{
			var result = _authorizeService.Get(id, User.GetUserId());
			if (result == null)
			{
				ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Authorize.NotFoundObject"));
				return RedirectToAction("Authorizes", "Account");
			}

			BindDataAuthorize();
			ViewBag.IsCreate = false;
			var model = result.ToModel();
			return View(model);
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "EditAuthorize")]
		[ValidateAntiForgeryToken]
		public ActionResult EditAuthorize(AuthorizeModel model)
		{
			ViewBag.IsCreate = false;
			if (ModelState.IsValid)
			{
				try
				{
					//Xư lý danh sách loại văn bản ủy quyền cho người nhận
					string docTypes = string.Empty;
					if (!string.IsNullOrWhiteSpace(model.DocTypeId))
					{
						var objdocType = Json2.ParseAs<List<Guid>>(model.DocTypeId);
						if (objdocType != null && objdocType.Any())
						{
							docTypes = model.DocTypeId;
						}
					}

					var authorize = _authorizeService.Get(model.AuthorizeId, User.GetUserId());
					if (authorize == null)
					{
						ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Authorize.NotFoundObject"));
						return RedirectToAction("Authorizes", "Account");
					}

					authorize = model.ToEntity(authorize);
					authorize.DocTypeId = docTypes;
					authorize.Permission = 0;
					if (model.Permissions != null && model.Permissions.Any())
					{
						foreach (var permission in model.Permissions)
						{
							authorize.Permission |= permission;
						}
					}
					_authorizeService.Update(authorize);
					SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Authorize.Updated"));
					return RedirectToAction("Authorizes", "Account");
				}
				catch
				{
					ErrorNotification("Lấy danh sách loại văn bản lỗi");
					BindDataAuthorize();
					return View(model);
				}
			}

			BindDataAuthorize();
			return View(model);
		}

		[HttpPost]
		// [ValidateAntiForgeryToken(Salt = "DeleteAuthorize")]
		[ValidateAntiForgeryToken]
		public JsonResult DeleteAuthorize(int id)
		{
			var model = _authorizeService.Get(id, User.GetUserId());
			if (model == null)
			{
				return Json(new { success = false, message = _resourceService.GetResource("Bkav.eGovCloud.Authorize.NotFoundObject") });
			}
			_authorizeService.Delete(model);
			return Json(new { success = true, message = _resourceService.GetResource("Bkav.eGovCloud.Authorize.DeleteSuccessed") });
		}

		#endregion ủy quyền

		#region Reset mật khẩu

		[HttpGet]
		public ActionResult ResetPasswordSettings()
		{
			var currentOtps = _otpService.Gets(User.GetUserId());
			ViewBag.Otps = currentOtps;
			return View();
		}

		[HttpPost]
		public ActionResult ResetPasswordSettings(int otpId)
		{
			var otp = _otpService.Get(otpId);
			if (otp == null)
			{
				return RedirectToAction("ResetPassword");
			}

			var user = _userService.Get(otp.UserId);
			if (user == null)
			{
				return RedirectToAction("ResetPassword");
			}

			var isViaEmail = false;
			var newPass = _otpService.GenerateOTP(isString: true);

			using (var trans = new TransactionScope())
			{
				var mailTemplateId = _otpSettings.ResetPassMailTemplateId;
				var mailTemplate = _templateService.Get(mailTemplateId);
				if (mailTemplate == null)
				{
					return RedirectToAction("ResetPassword", new { r = false, v = isViaEmail });
				}

				var content = GetContent(user.UserId, mailTemplate, null, newPass);
				if (string.IsNullOrWhiteSpace(content))
				{
					return RedirectToAction("ResetPassword", new { r = false, v = isViaEmail });
				}

				var reset = ResetPassword(user, newPass);
				if (!reset)
				{
					return RedirectToAction("ResetPassword", new { r = false, v = isViaEmail });
				}

				if (!string.IsNullOrEmpty(otp.Email))
				{
					var subject = mailTemplate.TitleMail;
					_mailHelper.Send(subject, otp.Email, content, true);
					isViaEmail = true;
				}
				else if (!string.IsNullOrEmpty(otp.Sms))
				{
					Sms sms = new Sms()
					{
						PhoneNumber = user.Phone,
						Message = content,
						IsSent = true,
						DateCreated = DateTime.Now,
						DateSend = DateTime.Now,
						UserName = user.Username,
						UserSendId = user.UserId,
					};
					_smsService.Create(sms);
					_smsHelper.SendSms(user.Phone, content);
				}

				ViewBag.SuccessStatus = "Reset mật khẩu thành công.";

				trans.Complete();
			}

			return RedirectToAction("ResetPassword", new { r = true, v = isViaEmail });
		}

		[HttpPost]
		public JsonResult SendActiveMail(string email, string pass)
		{
			if (string.IsNullOrEmpty(pass))
			{
				return Json(new { success = false, message = "Bạn phải nhập mật khẩu hiện tại." }, JsonRequestBehavior.AllowGet);
			}

			if (!email.IsEmailAddress())
			{
				return Json(new { success = false, message = "Địa chỉ thư điện tử không đúng." }, JsonRequestBehavior.AllowGet);
			}

			var user = _userService.CurrentEditableUser;
			var inputPwdHash = Generate.GetInputPasswordHash(pass, user.PasswordSalt);
			if (!user.PasswordHash.SequenceEqual(inputPwdHash))
			{
				return Json(new { success = false, message = "Mật khẩu hiện tại không đúng." }, JsonRequestBehavior.AllowGet);
			}

			//Kiem tra xem user da ton tai chua
			//var otp = _otpService.CheckExistOtp(user.UserId);
			if (!_otpService.CheckExistOtp(user.UserId))
			{
				var code = _otpService.GenerateOTP(false);
				var hasSentMail = SendOtpMail(email, code, user.UserId);
				if (!hasSentMail)
				{
					return Json(new { success = false, message = "Hệ thống không thể gửi được thư điện tử, vui lòng thử lại." }, JsonRequestBehavior.AllowGet);
				}

				var newOtp = new Otp()
				{
					Content = "",
					Email = email,
					Sms = "",
					Status = false,
					ActivedCode = code,
					ActivedUrl = "",
					DateCreated = DateTime.Now,
					DateLimit = DateTime.Now,
					UserId = user.UserId
				};

				_otpService.CreateOTP(newOtp);
			}
			else
			{
				//Kiem tra xem ma code da duoc kich hoat chua
				if (!_otpService.IsEmailActive(user.UserId, email))
				{
					var code = _otpService.GenerateOTP(false);
					var hasSentSms = SendOtpMail(email, code, user.UserId);
					if (!hasSentSms)
					{
						return Json(new { success = false, message = "Hệ thống không thể gửi được thư điện tử, vui lòng thử lại." }, JsonRequestBehavior.AllowGet);
					}
					//_otpService.UpdateEmailAndPhone(user.UserId, email, null);
				}
				else
				{
					return Json(new { success = false, message = "Email này đã được kích hoạt" }, JsonRequestBehavior.AllowGet);
				}
			}

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult SendActiveSms(string phone, string pass)
		{
			if (string.IsNullOrEmpty(pass))
			{
				return Json(new { success = false, message = "Bạn phải nhập mật khẩu hiện tại." }, JsonRequestBehavior.AllowGet);
			}

			var user = _userService.CurrentEditableUser;
			var inputPwdHash = Generate.GetInputPasswordHash(pass, user.PasswordSalt);
			if (!user.PasswordHash.SequenceEqual(inputPwdHash))
			{
				return Json(new { success = false, message = "Mật khẩu hiện tại không đúng." }, JsonRequestBehavior.AllowGet);
			}

			//var otp = _otpService.CheckExistOtp(user.UserId);
			if (!_otpService.CheckExistOtp(user.UserId))
			{
				var code = _otpService.GenerateOTP(false);
				var hasSentMail = SendOtpSms(phone, code, user.UserId);
				if (!hasSentMail)
				{
					return Json(new { success = false, message = "Hệ thống không thể gửi được tin nhắn, vui lòng thử lại." }, JsonRequestBehavior.AllowGet);
				}

				var newOtp = new Otp()
				{
					Content = "",
					Email = "",
					Sms = phone,
					Status = false,
					ActivedCode = code,
					ActivedUrl = "",
					DateCreated = DateTime.Now,
					DateLimit = DateTime.Now,
					UserId = user.UserId
				};

				_otpService.CreateOTP(newOtp);
			}
			else
			{
				//Kiem tra xem ma code da duoc kich hoat chua
				if (!_otpService.IsSmsActive(user.UserId, phone))
				{
					var code = _otpService.GenerateOTP(false);
					var hasSentSms = SendOtpSms(phone, code, user.UserId);
					if (!hasSentSms)
					{
						return Json(new { success = false, message = "Hệ thống không thể gửi được tin nhắn, vui lòng thử lại." }, JsonRequestBehavior.AllowGet);
					}
					//_otpService.UpdateEmailAndPhone(user.UserId, null, phone);
				}
				else
				{
					return Json(new { success = false, message = "Số điện thoại này đã được kích hoạt" }, JsonRequestBehavior.AllowGet);
				}
			}

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult ActiveOtpMail(string code, string email)
		{
			if (code.IsEmpty())
			{
				return Json(new { success = false, message = "Mã không được để trống." }, JsonRequestBehavior.AllowGet);
			}
			var otp = _otpService.GetByUserId(User.GetUserId());
			if (!otp.ActivedCode.Equals(code, StringComparison.OrdinalIgnoreCase))
			{
				return Json(new { success = false, message = "Mã không đúng." }, JsonRequestBehavior.AllowGet);
			}
			_otpService.UpdateEmailAndPhone(User.GetUserId(), email, null);
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult ActiveOtpSms(string code, string phone)
		{
			if (code.IsEmpty())
			{
				return Json(new { success = false, message = "Mã không được để trống." }, JsonRequestBehavior.AllowGet);
			}
			var otp = _otpService.GetByUserId(User.GetUserId());
			if (!otp.ActivedCode.Equals(code, StringComparison.OrdinalIgnoreCase))
			{
				return Json(new { success = false, message = "Mã không đúng." }, JsonRequestBehavior.AllowGet);
			}
			_otpService.UpdateEmailAndPhone(User.GetUserId(), null, phone);
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult ResetPassword(bool v = false, bool r = false)
		{
			var message = "";
			if (r)
			{
				message = "";
				if (v)
				{
					message += "Vui lòng kiểm tra địa chỉ email bạn đã đăng ký để lấy mật khẩu mới.";
				}
				else
				{
					message += "Vui lòng kiểm tra số điện thoại bạn đã đăng ký để lấy mật khẩu mới.";
				}
			}

			ViewBag.SuccessStatus = message;
			ViewBag.FailStatus = "";
			ViewBag.Otps = new List<Otp>();
			ViewBag.HasAccount = false;
			ViewBag.HasSuccess = r;
			return View();
		}

		[HttpPost]
		public ActionResult ResetPassword(string userName)
		{
			ViewBag.HasAccount = false;
			ViewBag.HasSuccess = false;
			if (!IsValidEmail(userName))
			{
				ViewBag.FailStatus = "Tài khoản không đúng định dạng";
				return View();
			}

			var user = _userService.Get(userName, isActivated: true);
			if (user == null)
			{
				ViewBag.FailStatus = "Tài khoản hoặc email không đúng, vui lòng thử lại.";
				return View();
			}

			var otps = _otpService.GetByUserId(user.UserId);
			if (otps == null)
			{
				ViewBag.FailStatus = "Bạn chưa thiết lập hình thức đặt lại mật khẩu nào cho tài khoản này.";
				return View();
			}

			ViewBag.HasAccount = true;
			ViewBag.Otps = otps;
			ViewBag.Avatar = _helperUserSetting.GetUserAvatar(user.Username);
			ViewBag.FullName = user.FullName;

			return View();
		}

		[HttpPost]
		public ActionResult ResetPasswordResult(int otpId)
		{
			var otp = _otpService.Get(otpId);
			if (otp == null)
			{
				return RedirectToAction("ResetPassword");
			}

			var user = _userService.Get(otp.UserId);
			if (user == null)
			{
				return RedirectToAction("ResetPassword");
			}

			var isViaEmail = false;
			var newPass = _otpService.GenerateOTP(isString: true);

			using (var trans = new TransactionScope())
			{
				var mailTemplateId = _otpSettings.ResetPassMailTemplateId;
				var mailTemplate = _templateService.Get(mailTemplateId);
				if (mailTemplate == null)
				{
					return RedirectToAction("ResetPassword", new { r = false, v = isViaEmail });
				}

				var content = GetContent(user.UserId, mailTemplate, null, newPass);
				if (string.IsNullOrWhiteSpace(content))
				{
					return RedirectToAction("ResetPassword", new { r = false, v = isViaEmail });
				}

				var reset = ResetPassword(user, newPass);
				if (!reset)
				{
					return RedirectToAction("ResetPassword", new { r = false, v = isViaEmail });
				}

				if (!string.IsNullOrEmpty(otp.Email))
				{
					var subject = mailTemplate.TitleMail;
					_mailHelper.Send(subject, otp.Email, content, true);
					isViaEmail = true;
				}
				else if (!string.IsNullOrEmpty(otp.Sms))
				{
					Sms sms = new Sms()
					{
						PhoneNumber = user.Phone,
						Message = content,
						IsSent = true,
						DateCreated = DateTime.Now,
						DateSend = DateTime.Now,
						UserName = user.Username,
						UserSendId = user.UserId,
					};
					_smsService.Create(sms);
					_smsHelper.SendSms(user.Phone, content);
				}

				ViewBag.SuccessStatus = "Reset mật khẩu thành công.";

				trans.Complete();
			}

			return RedirectToAction("ResetPassword", new { r = true, v = isViaEmail });
		}
		#endregion

		#region Lịch sử đăng nhập

		[HttpGet]
		public ActionResult LoginHistory()
		{
			var user = _userService.CurrentUser;
			var currentIP = Request.GetIP();

			ViewBag.CurrentIP = currentIP;
			ViewBag.Mac = GetMacCookie();
			var devices = _mobileDeviceService.Gets(user.UserId).OrderByDescending(m => m.LastUpdate);
			ViewBag.Devices = devices.ToList();

			var userSettingModel = _helperUserSetting.GetUserCurrentSetting(editable: false);
			ViewBag.LimitByIp = userSettingModel.LimitByIp ? true : _authenticationSettings.LimitByIP;
			ViewBag.LimitByMac = user.HasLimitByMac ?? _authenticationSettings.LimitByMAC;

			return View();
		}

		[HttpPost]
		public JsonResult ActiveDevice(int deviceId, bool hasBlock)
		{
			var curentUserId = _userService.CurrentUser.UserId;

			try
			{
				var logoutDevice = _mobileDeviceService.ActiveDevice(deviceId, hasBlock, curentUserId);
				var mac = GetMacCookie();
				if (logoutDevice == "" || logoutDevice == mac)
				{
					return Json(new { success = true, requireLogout = true }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(new { success = true, hasBlock = hasBlock }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception ex)
			{
				return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public JsonResult ChangeDevicePermission(bool? limitByIP, bool? limitByMAC)
		{
			var user = _userService.CurrentEditableUser;
			if (user != null)
			{
				user.HasLimitByMac = limitByMAC ?? false;
				_userService.SaveChanges();
				_userService.ClearCache();
			}

			return Json(true, JsonRequestBehavior.AllowGet);
		}

		#endregion

		#region Thông tin phiên bản

		[HttpGet]
		public ActionResult VersionInfo()
		{
			var appVersion = DataSettings.Current.AppVersion;
			var currentVersion = eGovVersions.CurrentVersion;
			ViewBag.AppVersion = appVersion.ToString();
			ViewBag.CurrentVersion = currentVersion.Version.ToString();
			return View();
		}

		#endregion

		#region private method

		/// <summary>
		/// các phím hỗ trợ
		/// </summary>
		/// <returns>danh sách các phím hỗ trợ </returns>
		private List<string> GetShortKey()
		{
			return new List<string> { "Select", "Alt", "Ctrl" };
		}

		private void AddCookie(string userNameWithDomain, bool remember, CustomerCookieData userDataString, out string token, bool isSuperAdmin = false)
		{
			token = "";
			HttpCookie authCookie;
			FormsAuthenticationTicket ticket;
			FormsAuthenticationTicket newTicket;
			if (isSuperAdmin)
			{
				authCookie = FormsAuthentication.GetAuthCookie(userNameWithDomain, remember);
				ticket = FormsAuthentication.Decrypt(authCookie.Value);
				newTicket = new FormsAuthenticationTicket(
					ticket.Version,
					ticket.Name,
					ticket.IssueDate,
					ticket.Expiration,
					ticket.IsPersistent,
					userDataString.ToCookieString()
				);
				authCookie.Value = FormsAuthentication.Encrypt(newTicket);
				SetResponseCookie(authCookie);
				token = authCookie.Value;
				return;
			}

			//bkavAuthen
			var ssoHelper = new EncryptData(_ssoSettings.BkavSSOKeyVersion, _ssoSettings.BkavSSOSecretKey);
			var exprire = remember ? DateTime.UtcNow.AddDays(Convert.ToInt32(_ssoSettings.BkavSSOExpire)) : DateTime.UtcNow.AddDays(1);

			var exp = (long)(exprire - new DateTime(1970, 1, 1)).TotalMilliseconds;

			var ssoCookie = new HttpCookie(_ssoSettings.BkavSSOCookieName);
			ssoCookie.Value = ssoHelper.CreateCookieString(userNameWithDomain, exp.ToString());
			InitCookie(ssoCookie, remember);
			token = ssoCookie.Value;

			// bkavUser
			var bkavUserCookie = new HttpCookie(_ssoSettings.BkavSSOCookieUsername);
			bkavUserCookie.Value = userNameWithDomain;
			InitCookie(bkavUserCookie, remember);

			// eGovUserInfo
			var userInfoCookie = new HttpCookie(_ssoSettings.UserInfoCookie);
			authCookie = FormsAuthentication.GetAuthCookie(userNameWithDomain, remember);
			ticket = FormsAuthentication.Decrypt(authCookie.Value);
			newTicket = new FormsAuthenticationTicket(
				ticket.Version,
				ticket.Name,
				ticket.IssueDate,
				ticket.Expiration,
				ticket.IsPersistent,
				userDataString.ToCookieString()
				);
			userInfoCookie.Value = FormsAuthentication.Encrypt(newTicket);
			InitCookie(userInfoCookie, remember);

			//#if QuanTriTapTrungEdition
			//            // Gán user principal
			//            var identity = new CustomerIdentity(newTicket);
			//            var principal = new CustomerPrincipal(identity);
			//            HttpContext.User = principal;
			//#endif
		}

		private void SetMacCookie(string Mac)
		{
			if (string.IsNullOrEmpty(Mac))
			{
				return;
			}

			// eGovUserInfo
			var macCookie = new HttpCookie("user_mac");
			macCookie.Value = Mac;
			InitCookie(macCookie, true);
		}

		private string GetMacCookie()
		{
			return Request.Cookies["user_mac"] == null ? "" : Request.Cookies["user_mac"].Value;
		}

		private void ClearCookie()
		{
			var ssoCookie = new HttpCookie(_ssoSettings.BkavSSOCookieName);
			ssoCookie.Domain = _ssoSettings.BkavSSOParentDomain;
			ssoCookie.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(ssoCookie);

			foreach (var cookieName in CookieName.AllCookieName)
			{
				var cookie = Request.Cookies[cookieName];
				if (cookie != null)
				{
					cookie.Expires = DateTime.Now.AddDays(-1);
					Response.Cookies.Add(cookie);
				}
			}
		}

		private HttpCookie InitCookie(HttpCookie httpCookie, bool remember)
		{
			httpCookie.Expires = remember ? DateTime.UtcNow.ToLocalTime().AddDays(_ssoSettings.BkavSSOExpire) : DateTime.UtcNow.ToLocalTime().AddDays(1);
			//httpCookie.Domain = _ssoSettings.BkavSSOParentDomain;
			httpCookie = SetResponseCookie(httpCookie);
			return httpCookie;
		}

		private HttpCookie SetResponseCookie(HttpCookie httpCookie)
		{
#if !DEBUG
            httpCookie.Secure = _authenticationSettings.HttpsOnly;
#endif
			httpCookie.HttpOnly = false; //False: Cho phép client cập nhật/xóa cookie

			Response.Cookies.Add(httpCookie);
			return httpCookie;
		}

		private void ShowAvatar(string userName, string domainName)
		{
			string avatarImg = "~/AvatarProfile/" + userName + "_" + domainName + ".jpg";
			var showAvatar = System.IO.File.Exists(Server.MapPath(avatarImg));
			ViewBag.ShowAvatar = showAvatar;

			if (showAvatar)
			{
				ViewBag.Width = _fileUploadSettings.ProfilePictureMaximumWidth;
				ViewBag.Height = _fileUploadSettings.ProfilePictureMaximumHeight;
				ViewBag.Avatar = "/AvatarProfile/" + userName + "_" + domainName + ".jpg?date=" + DateTime.Now.ToString("ddmmyyyyhhmmss");
			}
		}

		/// <summary>
		/// lấy danh sách giới tính
		/// </summary>
		/// <returns>danh sách giơi tính</returns>
		private List<SelectListItem> GendersList()
		{
			return new List<SelectListItem> {
							new SelectListItem{Value="true",Text=_resourceService.GetResource("Account.Sex.Male")},
							new SelectListItem{Value="false",Text=_resourceService.GetResource("Account.Sex.Female")}
			};
		}

		private List<SelectListItem> GetPrinters()
		{
			var printers = _printerService.Gets();
			var results = new List<SelectListItem>();
			foreach (var printer in printers)
			{
				results.Add(new SelectListItem
				{
					Value = printer.PrinterId.ToString(),
					Text = printer.ShareName
				});
			}
			return results;
		}

		/// <summary>
		/// Chuyển ảnh thành base64
		/// </summary>
		/// <param name="stream">Luồng của ảnh</param>
		/// <param name="ext"> Định dạng của ảnh</param>
		/// <returns></returns>
		private string StreamImageToBase64(Stream stream, string ext)
		{
			var bitmap = new System.Drawing.Bitmap(stream);
			var format = GetFormat(ext);
			using (var memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, format);
				var base64 = Convert.ToBase64String(memoryStream.ToArray());
				return base64;
			}
		}

		/// <summary>
		/// Lấy định dạng của ảnh
		/// </summary>
		/// <param name="ext">Định dạng</param>
		/// <returns></returns>
		private System.Drawing.Imaging.ImageFormat GetFormat(string ext)
		{
			var tmp = System.Drawing.Imaging.ImageFormat.Jpeg;
			switch (ext.ToLower())
			{
				case ".bmp":
					tmp = System.Drawing.Imaging.ImageFormat.Bmp;
					break;

				case ".emf":
					tmp = System.Drawing.Imaging.ImageFormat.Emf;
					break;

				case ".exif":
					tmp = System.Drawing.Imaging.ImageFormat.Exif;
					break;

				case ".gif":
					tmp = System.Drawing.Imaging.ImageFormat.Gif;
					break;

				case ".icon":
					tmp = System.Drawing.Imaging.ImageFormat.Icon;
					break;

				case ".jpeg":
					tmp = System.Drawing.Imaging.ImageFormat.Jpeg;
					break;

				case ".memorybmp":
					tmp = System.Drawing.Imaging.ImageFormat.MemoryBmp;
					break;

				case ".png":
					tmp = System.Drawing.Imaging.ImageFormat.Png;
					break;

				case ".wmf":
					tmp = System.Drawing.Imaging.ImageFormat.Wmf;
					break;

				default:
					tmp = System.Drawing.Imaging.ImageFormat.Jpeg;
					break;
			}

			return tmp;
		}

		/// <summary>
		/// Danh sách cấu hình hiển thị tóm tắt văn bản
		/// </summary>
		/// <returns></returns>
		private List<SelectListItem> GetDocumentQuickViewTypes()
		{
			return new List<SelectListItem>()
				  {
						new SelectListItem{Value= ((byte)Bkav.eGovCloud.Models.QuickViewType.Hide).ToString(), Text=Bkav.eGovCloud.Models.QuickViewType.Hide.ToString()},
						new SelectListItem{Value= ((byte)Bkav.eGovCloud.Models.QuickViewType.Below).ToString(), Text=Bkav.eGovCloud.Models.QuickViewType.Below.ToString()},
						new SelectListItem{Value= ((byte)Bkav.eGovCloud.Models.QuickViewType.Right).ToString(), Text=Bkav.eGovCloud.Models.QuickViewType.Right.ToString()}
				  };
		}

		private List<SelectListItem> GetFontSizeSettings()
		{
			return new List<SelectListItem>()
				  {
						new SelectListItem{Value= ((byte)Bkav.eGovCloud.Models.FontSizeType.Nho).ToString(), Text=Bkav.eGovCloud.Models.FontSizeType.Nho.ToString()},
						new SelectListItem{Value= ((byte)Bkav.eGovCloud.Models.FontSizeType.Vua).ToString(), Text=Bkav.eGovCloud.Models.FontSizeType.Vua.ToString()},
						new SelectListItem{Value=( (byte)Bkav.eGovCloud.Models.FontSizeType.Lon).ToString(), Text=Bkav.eGovCloud.Models.FontSizeType.Lon.ToString()}
				  };
		}

		private string GetAllDocTypes()
		{
			// var docTypes = _docTypeService.GetsByUserId(User.GetUserId());
			var docTypes = _docTypeService.GetAllFromCache();
			return docTypes.Select(dt => new
			{
				DocTypeName = dt.DocTypeName,
				DocTypeId = dt.DocTypeId
			}).StringifyJs();
		}

		private void BindDataAuthorize()
		{
			ViewBag.AllUsers = GetAllUsers();
			ViewBag.AllDocTypes = GetAllDocTypes();
			ViewBag.AllPermission = _resourceService.EnumToSelectList<PermissionTypes>();
		}

		private string GetAllUsers()
		{
			var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
			return allUsers.Select(
								u =>
								new
								{
									value = u.UserId,
									label = u.Username + " - " + u.FullName,
									fullname = u.FullName,
									username = u.Username,
									firstpositionid = 0
								}).StringifyJs();
		}

		/// <summary>
		/// Danh sách thiết lập mudim method
		/// </summary>
		/// <returns></returns>
		private List<SelectListItem> GetMudimMethod()
		{
			return new List<SelectListItem>{
					   new SelectListItem{Text="Telex",Value="2"},
					   new SelectListItem{Text="VNI",Value="1"},
					   new SelectListItem{Text="Viqr",Value="3"},
					   new SelectListItem{Text="Multi",Value="4"}
			};
		}

		/// <summary>
		/// Lấy danh sách các cấu hình hiển thị notify
		/// </summary>
		/// <returns></returns>
		private List<SelectListItem> GetListDocumentNotifyType()
		{
			return new List<SelectListItem> {
					new SelectListItem{Value= ((byte)DocumentNotifyType.Hide).ToString(), Text=DocumentNotifyType.Hide.ToString()},
					new SelectListItem{Value= ((byte)DocumentNotifyType.ShowNotifyInProcess).ToString(), Text=DocumentNotifyType.ShowNotifyInProcess.ToString()},
					new SelectListItem{Value= ((byte)DocumentNotifyType.All).ToString(), Text=DocumentNotifyType.All.ToString()}
			};
		}

		/// <summary>
		/// Lấy danh sách các cấu hình hiển thị notify
		/// </summary>
		/// <returns></returns>
		private List<SelectListItem> GetListBmailNotifyType()
		{
			return new List<SelectListItem> {
					new SelectListItem{Value= ((byte)MailNotifyType.Hide).ToString(), Text=MailNotifyType.Hide.ToString()},
					new SelectListItem{Value= ((byte)MailNotifyType.Inbox).ToString(), Text=MailNotifyType.Inbox.ToString()},
					new SelectListItem{Value= ((byte)MailNotifyType.Option).ToString(), Text=MailNotifyType.Option.ToString()},
					new SelectListItem{Value= ((byte)MailNotifyType.All).ToString(), Text=MailNotifyType.All.ToString()}
			};
		}

		private CustomerServiceClient GetCustomerClient()
		{
			var ssoUrl = Path.Combine(_authenticationSettings.SingleSignOnDomain, "Customer");

			//Todo: cần chuẩn lại chổ này
			ssoUrl = ssoUrl.Replace("https", "http");
			var client = new CustomerServiceClient("CustomerEndpoint", ssoUrl);
			return client;
		}

		private SingleSignOnPartnerServiceClient GetPartnerClient()
		{
			var ssoUrl = Path.Combine(_authenticationSettings.SingleSignOnDomain, "Partner");
			ssoUrl = ssoUrl.Replace("https", "http");
			var client = new SingleSignOnPartnerServiceClient("PartnerEndpoint", ssoUrl);
			return client;
		}

		private bool IsValidEmail(string email)
		{
			return email.IsEmailAddress();
		}

		private void ShowAndCheckCode(UserProfileModel model, int userId, string code)
		{
			if (_otpService.IsEmailActive(userId, model.Email)
				|| _otpService.IsSmsActive(userId, model.Phone))
			{
				model.IsShowCode = false;
				return;

			}

			if (!string.IsNullOrEmpty(model.Code))
			{

				//Kiểm tra xem còn thời gian kích hoạt không
				if (_otpService.IsTimeActive(userId, model.Code))
				{
					//Kiểm tra xem code đã nhập đúng chưa
					if (_otpService.CheckActivedCodeValidate(userId, model.Code, model.Email, model.Phone))
					{
						ViewBag.SuccessStatus = "Kích hoạt tài khoản thành công!";
						ViewBag.ShowEnterCode = false;

					}
					else
					{
						ViewBag.FailStatus = "Sai mã code";
						//Nếu sai mã code thì gửi sms và mail để kích hoạt lại
						int time = _otpSettings.TimeLimit;
						CheckOtpEmail(model, code, userId, time);
						CheckOtpSms(model, code, userId, time);
					}
				}
				else
				{
					ViewBag.FailStatus = "Mã code này đã hết thời gian sử dụng";
				}

			}
			else
			{
				ViewBag.FailStatus = "Vui lòng nhập mã code kích hoạt";
			}
		}

		private bool SendOtpMail(string email, string code, int userId)
		{
			try
			{
				var mailTemplateId = _otpSettings.ActiveMailTemplateId;
				var mailTemplate = _templateService.Get(mailTemplateId);
				if (mailTemplate != null)
				{
					var subject = mailTemplate.TitleMail;
					var contentMail = GetContent(userId, mailTemplate, code, null);
					_mailHelper.Send(subject, email, contentMail, true);
					int timeLimit = _otpSettings.TimeLimit;
					_otpService.UpdateCode(userId, code, timeLimit);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		private bool SendOtpSms(string sms, string code, int userId)
		{
			try
			{
				int smsTemplateId = _otpSettings.ActiveSmsTemplateId;
				var smsTemplate = _templateService.Get(smsTemplateId);
				if (smsTemplate != null)
				{
					string contentSms = GetContent(userId, smsTemplate, code, null);
					_smsHelper.SendSms(sms, contentSms);
					int timeLimit = _otpSettings.TimeLimit;
					_otpService.UpdateCode(userId, code, timeLimit);
					return true;
				}
			}
			catch
			{
				return false;
			}
			return false;
		}

		private void CheckOtpSms(UserProfileModel model, string code, int userId, int time)
		{
			if (model.Phone.IsEmpty() || _otpService.IsSmsActive(userId, model.Phone))
			{
				return;
			}
			int smsTemplateId = _otpSettings.ActiveSmsTemplateId;
			var smsTemplate = _templateService.Get(smsTemplateId);
			if (smsTemplate != null)
			{
				string contentSms = GetContent(userId, smsTemplate, code, null);
				_smsHelper.SendSms(model.Phone, contentSms);
				_otpService.UpdateCode(userId, code, time);
				ViewBag.PhoneValidateSuccess = "Tin nhắn đã được gửi.";
				model.IsShowCode = true;
			}
		}

		private void CheckOtpEmail(UserProfileModel model, string code, int userId, int time)
		{
			if (model.Email.IsEmpty() || _otpService.IsEmailActive(userId, model.Email))
			{
				return;
			}

			int mailTemplateId = _otpSettings.ActiveMailTemplateId;
			var mailTemplate = _templateService.Get(mailTemplateId);
			if (mailTemplate != null)
			{
				string subject = mailTemplate.TitleMail;
				string contentMail = GetContent(userId, mailTemplate, code, null);
				_mailHelper.Send(subject, model.Email, contentMail, true);//Gửi email
				_otpService.UpdateCode(userId, code, time);
				ViewBag.EmailValidateSuccess = "Email đã được gửi.";
				model.IsShowCode = true;
			}
		}

		private string GetContent(int userId, Template template, string code, string pass)
		{
			string account = _userService.GetUserName(userId);
			if (!string.IsNullOrEmpty(code))
			{
				return !string.IsNullOrEmpty(template.Content) ? template.Content.Replace("{account}", account).Replace("{code}", code) : code;
			}

			else if (!string.IsNullOrEmpty(pass))
			{
				return !string.IsNullOrEmpty(template.Content) ? template.Content.Replace("{account}", account).Replace("{pass}", pass) : pass;
			}
			return "";
		}

		private bool ResetPassword(User user, string newPassword)
		{
			var result = false;
#if QuanTriTapTrungEdition
            using (var transactionUser = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                if (_userService.ResetPassword(user.UserId, newPassword))
                {
                    using (var transactionService = new TransactionScope(TransactionScopeOption.Required))
                    {
                        var client = GetClientService();

                        var status = client.ResetPassword(user.UsernameEmailDomain, newPassword,
                                                           _passwordPolicySettings.EnableHistory,
                                                           _passwordPolicySettings.HistoryCount);

                        if (!status.Success)
                        {
                            return false;
                        }
                        result = true;
                        transactionService.Complete();
                    }
                }

                transactionUser.Complete();
            }
#else
			result = _userService.ResetPassword(user.UserId, newPassword);
#endif

			return result;
		}

#if QuanTriTapTrungEdition

        private CustomerServiceClient GetClientService()
        {
            var ssoUrl = _authenticationSettings.SingleSignOnDomain;
            ssoUrl = Path.Combine(ssoUrl, "Customer");
            ssoUrl = ssoUrl.ToLower().Replace("https", "http");
            var client = new CustomerServiceClient("CustomerEndpoint", ssoUrl);
            return client;
        }
#endif

        #endregion private method
    }

    public class SSOLogout
    {
        public string logout_token { get; set; }
    }

    public class SSOObject
    {
        public string at_hash { get; set; }
        public string aud { get; set; }
        public string c_hash { get; set; }
        public string sub { get; set; }
        public int nbf { get; set; }
        public string azp { get; set; }
        public string email { get; set; }
        public List<string> amr { get; set; }
        public string iss { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string sid { get; set; }
    }
}