using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Core.ReadFile;
using ClosedXML.Excel;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class CitizenController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly CitizenBll _citizenService;
        private readonly ResourceBll _resourceService;
        private const string DefaultSortBy = "CitizenName";

        public CitizenController(
            AdminGeneralSettings generalSettings,
            CitizenBll citizenService,
            ResourceBll resourceService)
            : base()
        {
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _citizenService = citizenService;
        }

        public ActionResult Index()
        {
            var citizenName = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DefaultSortBy
            };

            var httpCookie = Request.Cookies[CookieName.SearchCitizen];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    citizenName = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _citizenService.Gets(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                citizenName: citizenName,
                currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.CitizenName = citizenName;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            if (isInvalidCookie)
            {
                CreateCookieSearch(citizenName, sortAndPage);
            }

            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchCitizen];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchCitizen, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(string citizenName, int pageSize)
        {
            IEnumerable<CitizenModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    model = _citizenService.Gets(out totalRecords,
                        pageSize: pageSize,
                        sortBy: DefaultSortBy,
                        isDescending: false,
                        citizenName: citizenName).ToListModel();
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DefaultSortBy,
                        TotalRecordCount = totalRecords
                    };
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.CitizenName = citizenName;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                    CreateCookieSearch(citizenName, sortAndPage);
                }
            }

            return PartialView("_List", model);
        }

        public ActionResult SortAndPaging(string citizenName,
                                            string sortBy,
                                            bool isSortDesc,
                                            int page,
                                            int pageSize)
        {
            IEnumerable<CitizenModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _citizenService.Gets(out totalRecords,
                    pageSize: pageSize,
                    sortBy: sortBy,
                    isDescending: isSortDesc,
                    citizenName: citizenName,
                    currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.CitizenName = citizenName;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                CreateCookieSearch(citizenName, sortAndPage);
            }
            return PartialView("_List", model);
        }

        public ActionResult Edit(int id)
        {
            ////Hopcv: 190614
            ////Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new { error = _resourceService.GetResource("Customer.User.NotPermissionResetPassword") });
            //}

            var citizen = _citizenService.GetById(id);
            if (citizen == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.NotExist"));
                ErrorNotification(_resourceService.GetResource("Admin.Citizen.NotExist"));
                return RedirectToAction("Index");
            }

            var model = citizen.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CitizenModel model)
        {
            ////Hopcv: 190614
            ////Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new { error = _resourceService.GetResource("Customer.User.NotPermissionResetPassword") });
            //}

            if (ModelState.IsValid)
            {
                var citizen = _citizenService.GetById(model.Id);
                if (citizen == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Admin.Citizen.NotExist"));
                    return RedirectToAction("Index");
                }

                var passwordHash = citizen.PasswordHash;
                var passwordSalt = citizen.PasswordSalt;
                try
                {
                    citizen = model.ToEntity(citizen);
                    citizen.PasswordHash = passwordHash;
                    citizen.PasswordSalt = passwordSalt;
                    _citizenService.Update(citizen);
                    SuccessNotification(_resourceService.GetResource("Admin.Citizen.Edit.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.Edit.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.Edit.Error"));
                    ErrorNotification(_resourceService.GetResource("Admin.Citizen.Edit.Error"));
                }
            }

            GetModelError();
            return View(model);
        }

        public JsonResult ChangeActivated(int id, bool activated)
        {
            ////Hopcv: 190614
            ////Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new { error = _resourceService.GetResource("Customer.User.NotPermissionResetPassword") });
            //}

            var citizen = _citizenService.GetById(id);
            if (citizen == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.NotExist"));
                return Json(new
                {
                    error =
                        _resourceService.GetResource("Admin.Citizen.NotExist")
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                citizen.IsActivated = activated;
                _citizenService.Update(citizen);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.SetActive.Success"));
                return Json(new
                {
                    success = _resourceService.GetResource("Admin.Citizen.SetActive.Success")
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.SetActive.Error"));
                return Json(new
                {
                    error = _resourceService.GetResource("Admin.Citizen.SetActive.Error")
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ResetPassword(int id, string defaultPassword = null)
        {
            ////Hopcv: 190614
            ////Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new { error = _resourceService.GetResource("Customer.User.NotPermissionResetPassword") });
            //}
            string message = string.Empty;
            try
            {
                var citizen = _citizenService.GetById(id);
                if (citizen == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.NotExist"));
                    return Json(new { error = _resourceService.GetResource("Admin.Citizen.NotExist") });
                }

                var newPassword = string.IsNullOrWhiteSpace(defaultPassword) ? Generate.GenerateRandomString(8) : defaultPassword;
                using (var transactionUser = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    _citizenService.ResetPassword(citizen, newPassword);
                    transactionUser.Complete();
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.ResetPassword.Success"));
                return Json(new { success = _resourceService.GetResource("Admin.Citizen.ResetPassword.Success"), password = newPassword });
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Citizen.ResetPassword.Error"));
                return Json(new { error = _resourceService.GetResource("Admin.Citizen.ResetPassword.Error") });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var citizen = _citizenService.GetById(id);
            if (citizen != null)
            {
                try
                {
                    _citizenService.Delete(citizen);
                    CreateActivityLog(ActivityLogType.Admin, "Xóa công dân / doanh nghiệp thành công!");
                    SuccessNotification(_resourceService.GetResource("Xóa công dân / doanh nghiệp thành công!"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult ImportCitizen()
        {
            return View();
        }
		
    }
}