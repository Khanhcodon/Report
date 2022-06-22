using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework.Utility;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BaseController - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : System.Web.Mvc.Controller
    /// Create Date : 310712
    /// Author      : TrungVH
    /// Description : Base controller cho tất cả các controller. Hỗ trợ ghi log khi hệ thống lỗi và 1 số hàm tiện ích
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class BaseController : Controller
    {
        private LogBll _logService;
        private UserBll _userService;
		private AdminGeneralSettings _generalSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        [DebuggerStepThrough]
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region Globalization and Localization

            // Is it View ?
            var view = filterContext.Result as ViewResultBase;
            if (view == null) // if not exit
            {
                return;
            }

            // var cultureName = filterContext.HttpContext.Request.UserLanguages[0]; // needs validation return "en-us" as default            
            var cultureName = CultureHelper.GetCurrentCultureName(); // e.g. "en-US" 

            // Is it default culture? exit
            if (cultureName == CultureHelper.GetDefaultCultureName())
            {
                return;
            }

            // Are views implemented separately for this culture?  if not exit
            var viewImplemented = CultureHelper.IsViewSeparate(cultureName);
            if (viewImplemented == false)
            {
                return;
            }

            var viewName = view.ViewName;

            int i;

            if (string.IsNullOrEmpty(viewName))
            {
                viewName = filterContext.RouteData.Values["action"] + "." + cultureName; // Index.en-US
            }
            else if ((i = viewName.LastIndexOf('.')) > 0)
            {
                // contains . like "Index.cshtml"                
                viewName = viewName.Substring(0, i + 1) + cultureName + viewName.Substring(i);
            }
            else
            {
                viewName += "." + cultureName; // e.g. "Index" ==> "Index.en-Us"
            }

            view.ViewName = viewName;

            #endregion

            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        [DebuggerStepThrough]
        protected override void ExecuteCore()
        {
            #region Globalization and Localization

            string cultureName;
            var cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = CultureHelper.GetValidCultureName(cultureCookie.Value);
                if (cultureName != cultureCookie.Value)
                {
                    if (Response.Cookies["_culture"] == null)
                    {
                        // Save culture in a cookie
                        cultureCookie = new HttpCookie("_culture")
                        {
                            HttpOnly = false,
                            Value = cultureName,
                            Expires = DateTime.Now.AddYears(1),
                            Secure = true
                        };
                        Response.Cookies.Add(cultureCookie);
                    }
                    else
                    {
                        Response.Cookies["_culture"].Value = cultureName;
                    }
                }
            }
            else
            {
                // Request.UserLanguages[0]; // obtain it from HTTP header AcceptLanguages
                cultureName = "en-US"; // CultureHelper.GetDefaultCultureName();

                // Save culture in a cookie
                cultureCookie = new HttpCookie("_culture")
                {
                    HttpOnly = false,
                    Value = cultureName,
                    Expires = DateTime.Now.AddYears(1),
                    Secure = true
                };
                Response.Cookies.Add(cultureCookie);
            }

            // Validate culture
            var validCulture = CultureHelper.GetValidCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = validCulture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            #endregion

            base.ExecuteCore();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return new JsonNetResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Data = data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Data = data,
                JsonRequestBehavior = behavior
            };
        }

#pragma warning disable 1591
        #region JsonNet - Return Json by Json.Net (NewtonJson)
        protected JsonNetResult JsonNet(object data)
        {
            return new JsonNetResult
            {
                Data = data
            };
        }

        protected JsonNetResult JsonNet(object data, string contentType)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType
            };
        }

        protected JsonNetResult JsonNet(object data, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = behavior
            };
        }

        protected JsonNetResult JsonNet(object data, string contentType, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                JsonRequestBehavior = behavior
            };
        }

        protected JsonNetResult JsonNet(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return new JsonNetResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Data = data
            };
        }

        protected JsonNetResult JsonNet(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Data = data,
                JsonRequestBehavior = behavior
            };
        }

        #endregion

        #region Exception Process

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    if (filterContext.Exception != null)
        //    {
        //        LogException(filterContext.Exception);
        //    }

        //    base.OnException(filterContext);
        //}

        protected virtual string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Log ngoại lệ
        /// </summary>
        /// <param name="ex"></param>
        protected void LogException(Exception ex)
        {
            _userService = DependencyResolver.Current.GetService<UserBll>();
            var currentUser = _userService.CurrentUser == null ? "" : _userService.CurrentUser.Username;
            _logService = DependencyResolver.Current.GetService<LogBll>();
            _logService.Error(currentUser + ": " + ex.Message, ex);
        }

        /// <summary>
        /// Log ngoại lệ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        protected void LogException(string message, Exception ex = null)
        {
            _userService = DependencyResolver.Current.GetService<UserBll>();
            var currentUser = _userService.CurrentUser == null ? "" : _userService.CurrentUser.Username;

            _logService = DependencyResolver.Current.GetService<LogBll>();
            _logService.Error(currentUser + ": " + message, ex);
        }

        /// <summary>
        /// Tạo log hành động
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="username"></param>
        /// <param name="userId"></param>
        protected void CreateActivityLog(ActivityLogType type, string content, string username, int? userId)
        {
			_generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
			if (!_generalSettings.SaveUserActivity) return;

			var activityLogService = DependencyResolver.Current.GetService<ActivityLogBll>();
            activityLogService.Create(type, content, Request.ServerVariables["REMOTE_ADDR"], username, userId);
        }

        /// <summary>
        /// Tạo log hành động
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        protected void CreateActivityLog(ActivityLogType type, string content)
        {
			_generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
			if (!_generalSettings.SaveUserActivity) return;

			var activityLogService = DependencyResolver.Current.GetService<ActivityLogBll>();
            activityLogService.Create(type, content, Request.ServerVariables["REMOTE_ADDR"], User.GetUserNameWithDomain(), User.GetUserId());
        }

        #endregion

        /// <summary>
        /// AccessDeniedView
        /// </summary>
        /// <returns></returns>
        protected ActionResult AccessDeniedView()
        {
            return RedirectToAction("AccessDenied", "Error", new { area = "", pageUrl = Request.RawUrl });
        }

        /// <summary>
        /// Notify báo thành công
        /// </summary>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification("Success", message, persistForTheNextRequest);
        }

        /// <summary>
        /// Notify báo lỗi
        /// </summary>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification("Error", message, persistForTheNextRequest);
        }

        /// <summary>
        /// Notify báo lỗi
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void ErrorNotification(EgovException exception, bool persistForTheNextRequest = true)
        {
            AddNotification("Error", exception.Message, persistForTheNextRequest);
        }

        /// <summary>
        /// Thêm notify
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void AddNotification(string type, string message, bool persistForTheNextRequest)
        {
            var dataKey = string.Format("notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentCultureName()
        {
            return CultureHelper.GetCurrentCultureName();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected CultureInfo GetCurrentCulture()
        {
            return CultureHelper.GetCurrentCulture();
        }
    }
}
