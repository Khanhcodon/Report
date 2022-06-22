using Bkav.eGovCloud.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Bkav.eGovCloud.Core;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class HandleUpdateFilter : IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;

            if (controllerName == "Install" && actionName != "Update" && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // probably "progress" or "finalize" call
                return;
            }

            var appVersion = DataSettings.Current.AppVersion;
            var currentVersion = eGovVersions.CurrentVersion.Version;
            if (appVersion < currentVersion && controllerName != "Install")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", "Install" }, 
                        { "Action", "Update" } 
                    });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // nada
        }

    }
}
