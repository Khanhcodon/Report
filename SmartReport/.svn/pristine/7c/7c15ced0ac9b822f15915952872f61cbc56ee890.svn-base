using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using System.Net;
using System;
using System.Net.Sockets;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    /// <summary>
    /// <para>Author: HopCV</para>
    /// <para>Create date: 180614</para>
    /// Custom kiểm tra permission người dùng có quyền truy cập vào action
    /// Quy ước : PermissionKey = controllerName + actionName làm PermissionKey trong bảng permisston
    /// Nếu là trang index nên điền đầy đủ thông tin như: IndexCustom để tránh trường hợp khi url của trang index
    /// </summary>    
    [EgovAuthorize]
    //[RequireHttps]
    public class CustomController : BaseController
    {
        #region property

        /// <summary>
        /// Key kiểm tra 
        /// </summary>
        private string _permissionKey;
        private List<string> _accessIps;

        protected string PermissionKey
        {
            get
            {
                return _permissionKey;
            }
        }

        #endregion

        private readonly PermissionBll _permissionSerice;
        private readonly InfomationBll _infoService;
        private readonly UserBll _userService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly AuthenticationSettings _authenSettings;
        private readonly MemoryCacheManager _cacheManager;

        protected bool IsSuperAdmin = false;

        public CustomController()
            : base()
        {
            _permissionSerice = DependencyResolver.Current.GetService<PermissionBll>();
            _generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
            _authenSettings = DependencyResolver.Current.GetService<AuthenticationSettings>();
            _cacheManager = DependencyResolver.Current.GetService<MemoryCacheManager>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _accessIps = GetAccessIps();

            var isSuperAdmin = false; // _permissionSerice.HasPemission("DomainIndex"); // Quản trị domain
#if QuanTriTapTrungEdition
            isSuperAdmin = _permissionSerice.HasPemission("DomainIndex");
#endif
            _infoService = DependencyResolver.Current.GetService<InfomationBll>();
            var infomation = _infoService.GetCurrentOfficeName();
            ViewBag.OfficeName = infomation;
            ViewBag.IsSuperAdmin = isSuperAdmin;
            IsSuperAdmin = isSuperAdmin;
            ViewBag.Domains = !isSuperAdmin ? new List<Domain>() : GetDomains();
            ViewBag.Avatar = GetUserAvatar();
            ViewBag.AuthenDomain = _authenSettings.DefaultDomain;

            var isDevVersion = false;
#if DEBUG
            isDevVersion = true;
#endif
            ViewBag.IsDevVersion = isDevVersion;
        }

        private List<string> GetAccessIps()
        {
            var accessIpSetings = CommonHelper.GetAppSetting("AccessIps", "");
            if (string.IsNullOrWhiteSpace(accessIpSetings))
            {
                return null;
            }

            return accessIpSetings.Split(',').ToList();
        }


        private IEnumerable<Domain> GetDomains()
        {
            var userName = _userService.CurrentUser.UsernameEmailDomain;
            var _domain = DependencyResolver.Current.GetService<DomainBll>();
            return _domain.GetsByUser(userName);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            var actionName = actionDescriptor.ActionName;
            var controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
            _permissionKey = controllerName + actionName;

            base.OnActionExecuting(filterContext);
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        /// <summary>
        /// Kiểm tra người dùng có quyền thao tác với acion này không. (nếu không truyền PermissionKey thì hệ thống sẽ tự lấy PermissionKey theo action)
        /// </summary>
        /// <returns>True : có quyền ; Fale: không có quyền</returns>
        protected bool HasPermission(string permissionKey = "")
        {
            if (!string.IsNullOrWhiteSpace(permissionKey))
            {
                _permissionKey = permissionKey;
            }
            bool result = _permissionSerice.HasPemission(PermissionKey);
            if (!result)
            {
                return false;
            }
            var url = Request.RawUrl;
            var ip = GetLocalIPAddress();
            if (url.Contains("dashboard/publish") || url.Contains("publish"))
            {
                return true;
            }

            var clientIp = Request.ServerVariables["REMOTE_ADDR"];
            if (_accessIps == null)
            {
                return true;
            }

            if (!_accessIps.Any())
            {
                return true;
            }

            result =  _accessIps.Contains(ip);
            
            return result;
            //if (!string.IsNullOrWhiteSpace(permissionKey))
            //{
            //    _permissionKey = permissionKey;
            //}

            ////Dùng để kiểm tra truy cập của ngươì dùng trước khi truy cập vào action
            //bool result = _permissionSerice.HasPemission(PermissionKey);
            //return result;
        }

        /// <summary>
        /// Kiểm tra hệ thống có sử dụng nghiệp vụ HSMC hay không
        /// </summary>
        /// <returns></returns>
        protected bool HasUseHSMC()
        {
            var result = false;
#if HoSoMotCuaEdition
            result = true;
#endif
            return result;
        }

        /// <summary>
        /// Xóa toàn bộ cache của hệ thống
        /// </summary>
        /// <param name="returnUrl">Link</param>
        protected void ClearAllCache(string returnUrl = null)
        {
            _cacheManager.Clear();
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/Admin";
            }

            Response.Redirect(returnUrl);
        }

        private string GetUserAvatar()
        {
            var avartar = string.Empty;
            try
            {
                avartar = string.Format(_generalSettings.Avatar, _userService.CurrentUser.Username);
            }
            catch { }

            return avartar;
        }

        /// <summary>
        /// HopCV
        /// Lấy và hiển thị các lỗi khi thêm mới hoặc cập nhật
        /// note:hàm này chỉ dùng khi bug thêm mới hoặc cập nhật thông tin
        /// </summary>
        protected void GetModelError()
        {
            var errorList = ModelState.ToDictionary(
                           p => p.Key,
                           p => p.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            if (errorList != null && errorList.Any())
            {
                foreach (var item in errorList)
                {
                    if (item.Value != null && item.Value.Any())
                    {
                        ModelState.AddModelError(item.Key, string.Join("; ", item.Value));
                    }
                }
            }
        }
    }

    /// <summary>
    /// <para>Author: HopCV</para>
    /// <para>Create date: 180614</para>
    /// Custom kiểm tra permission người dùng có quyền truy cập vào action
    /// Quy ước : PermissionKey = controllerName + actionName làm PermissionKey trong bảng permisston
    /// Nếu là trang index nên điền đầy đủ thông tin như: IndexCustom để tránh trường hợp khi url của trang index
    /// </summary>
    [EgovAuthorize]
    //[RequireHttps]
    public class CustomAsyncController : AsyncController
    {
        #region property

        /// <summary>
        /// Key kiểm tra 
        /// </summary>
        private string _permissionKey;

        protected string PermissionKey
        {
            get
            {
                return _permissionKey;
            }
        }

        #endregion

        private readonly PermissionBll _permissionSerice;
        private readonly UserBll _userService;
        private AdminGeneralSettings _generalSettings;
        private readonly MemoryCacheManager _cacheManager;

        public CustomAsyncController()
            : base()
        {
            _permissionSerice = DependencyResolver.Current.GetService<PermissionBll>();
            _generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
            _cacheManager = DependencyResolver.Current.GetService<MemoryCacheManager>();
            _userService = DependencyResolver.Current.GetService<UserBll>();

            ViewBag.Domains = _cacheManager.Get<IEnumerable<Domain>>(CacheParam.DomainNameKey);
            ViewBag.Avatar = GetUserAvatar();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            var actionName = actionDescriptor.ActionName;
            var controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
            _permissionKey = controllerName + actionName;

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền thao tác với acion này không.
        /// </summary>
        /// <returns>True : có quyền ; Fale: không có quyền</returns>
        protected bool HasPermission(string permissionKey = "")
        {
            if (!string.IsNullOrWhiteSpace(permissionKey))
            {
                _permissionKey = permissionKey;
            }
            //Dùng để kiểm tra truy cập của ngươì dùng trước khi truy cập vào action. (nếu không truyền PermissionKey thì hệ thống sẽ tự lấy PermissionKey theo action)
            bool result = _permissionSerice.HasPemission(PermissionKey);
            return result;
        }

        /// <summary>
        /// Kiểm tra hệ thống có sử dụng nghiệp vụ HSMC hay không
        /// </summary>
        /// <returns></returns>
        protected bool HasUseHSMC()
        {
            return _generalSettings.HasUseHSMC;
        }

        private string GetUserAvatar()
        {
            var avartar = string.Empty;
            try
            {
                avartar = string.Format(_generalSettings.Avatar, _userService.CurrentUser.Username);
            }
            catch { }

            return avartar;
        }

        /// <summary>
        /// HopCV
        /// Lấy và hiển thị các lỗi khi thêm mới hoặc cập nhật
        /// note:hàm này chỉ dùng khi bug thêm mới hoặc cập nhật thông  tin
        /// </summary>
        protected void GetModelError()
        {
            var errorList = ModelState.ToDictionary(
                         p => p.Key,
                         p => p.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            if (errorList != null && errorList.Any())
            {
                foreach (var item in errorList)
                {
                    if (item.Value != null && item.Value.Any())
                    {
                        ModelState.AddModelError(item.Key, string.Join("; ", item.Value));
                    }
                }
            }
        }
    }
}
