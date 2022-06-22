using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Domain.License;
using Bkav.eGovCloud.Core.License;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CheckLicenseAttribute - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : System.Web.MVC.ActionFilterAttribute
    ///     * Implement: System.Web.MVC.IActionFilter
    /// Create Date : 110614
    /// Author      : TrungVH
    /// Description : Attribute kiểm tra xác thực bản quyền
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class CheckLicenseAttribute : ActionFilterAttribute, IActionFilter
    {
        private LicenseSettings _license;
        private const string _licenseHasExpiredURL = "~/Home/LicenseHasExpired";

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            _license = LicenseSettings.Current;
            var isValid = false;

            if (_license != null)
            {
                if (_license.IsFreeMode)
                {
                    isValid = true;
                }
                else
                {
                    var licenseInfo = _license.Infomation;
                    if (licenseInfo != null)
                    {
                        // Lấy cuối ngày hết hạn
                        var toDate = licenseInfo.ToDate.AddDays(1).AddSeconds(-1);

                        if (toDate.CompareTo(DateTime.Now) >= 0)
                        {
                            var userService = DependencyResolver.Current.GetService<UserBll>();
                            var totalUser = userService.GetTotalUser();
                            if (totalUser <= licenseInfo.TotalUser)
                            {
                                isValid = true;
                            }
                            else
                            {
                                licenseInfo.Message = "Bản quyền sử dụng phần mềm đã bị vi phạm do số lượng người dùng vượt quá mức cho phép.";
                            }
                        }
                        else
                        {
                            licenseInfo.Message = "Bản quyền sử dụng phần mềm đã hết thời gian có hiệu lực.";
                        }

                        if (isValid && DateTime.Now.Subtract(licenseInfo.LastUpdate).TotalDays > 3)
                        {
                            isValid = false;
                            licenseInfo.Message = "Bản quyền sử dụng phần mềm không xác thực được với máy chủ.";
                        }

                        if (isValid)
                        {
                            var remain = licenseInfo.ToDate.Subtract(DateTime.Now).TotalDays;
                            if (remain <= 3)
                            {
                                licenseInfo.Message = string.Format("Bản quyền sử dụng phần mềm chỉ còn {0} ngày sử dụng.", remain);
                            }
                        }

                        licenseInfo.IsValid = isValid;
                    }
                }
            }

            isValid = true;

            if (isValid)
            {
                return;
            }

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            if (controllerName != "Home" && actionName != "LicenseHasExpired")
            {
                filterContext.Result = new RedirectResult(_licenseHasExpiredURL);
            }

            OnActionExecuting(filterContext);
        }
    }
}
