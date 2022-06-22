using Bkav.eGovCloud.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class HandleInstallFilter : IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;

            if (controllerName == "Install" && actionName != "Index" && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // probably "progress" or "finalize" call
                return;
            }

            if (!DataSettings.DatabaseIsInstalled() && controllerName != "Install")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", "Install" }, 
                        { "Action", "Index" } 
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
