using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Newtonsoft.Json.Converters;
using Microsoft.AspNet.SignalR;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.NotificationService.SignalRMessaging;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class AddressBookController : Controller
    {
        private readonly UserBll _userService;
        private readonly DepartmentBll _departmentService;
        private readonly JobTitlesBll _jobtitleService;
        private SsoSettings _ssoSettings;
        private readonly PositionBll _positionService;

        public AddressBookController(UserBll userService, DepartmentBll departmentService, JobTitlesBll jobtitleService, PositionBll positionService)
        {
            _userService = userService;
            _departmentService = departmentService;
            _jobtitleService = jobtitleService;
            _positionService = positionService;
        }

        public ActionResult Index()
        {
            _ssoSettings = SsoSettings.Instance;
            ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;

            return View();
        }

        /// <summary>
        /// Trả về danh sách tất cả chức danh
        /// </summary>
        /// <returns>Json object tất cả các chức danh</returns>
        public JsonResult GetAllJobTitlies()
        {
            var result = _jobtitleService.GetCacheAllJobtitles()
                .Select(u => new { value = u.JobTitlesId, label = u.JobTitlesName, isApprover = u.IsApproved, order = u.PriorityLevel });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   Trả về danh sách user trong cơ quan
        /// </summary>
        /// <returns>Json object danh sách tất cả user.</returns>
        public JsonResult GetAllUserInfos()
        {
            var result = _userService.GetAllCached(true)
                .Select(u => new
                {
                    value = u.UserId,
                    label = u.Username + " - " + u.FullName,
                    fullname = u.FullName,
                    username = u.Username,
                    avatar = u.Avatar,
                    phone = u.Phone
                })
                .OrderBy(u => u.username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}