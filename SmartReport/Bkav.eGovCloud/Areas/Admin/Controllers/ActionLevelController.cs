using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ActionLevelController : CustomController// BaseController
    {
        private readonly ActionLevelBll _actionLevelService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;

        private const string DEFAULT_SORT_BY = "ActionLevelCode";

        public ActionLevelController(ActionLevelBll actionLevelService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _resourceService = resourceService;
            _actionLevelService = actionLevelService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActionLevel.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.ActionLevel.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchActionLevel];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            var model = GetActionLevels(search, sortAndPage, isInvalidCookie);

            ViewBag.ActionLevelName = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return View(model);
        }

        private void CreateCookieSearch(string actionLevelKey, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", actionLevelKey }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchActionLevel];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchActionLevel, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(string actionLevelName, int pageSize)
        {
            IEnumerable<ActionLevelModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        SortBy = DEFAULT_SORT_BY
                    };
                    model = GetActionLevels(actionLevelName, sortAndPage);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.ActionLevelName = actionLevelName;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                }
            }
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(string actionLevelName,
            string sortBy, bool isSortDesc, int page, int pageSize)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                IsSortDescending = isSortDesc,
                SortBy = sortBy
            };
            var model = GetActionLevels(actionLevelName, sortAndPage);

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ActionLevelName = actionLevelName;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_PartialList", model);
        }

        private IEnumerable<ActionLevelModel> GetActionLevels(string actionLevelName, SortAndPagingModel sortAndPage, bool hasCreateCoookie = true)
        {
            int totalRecords;

            var model = _actionLevelService.GetsAs(out totalRecords,
                a => new ActionLevelModel { ActionLevelId = a.ActionLevelId, ActionLevelName = a.ActionLevelName, ActionLevelCode = a.ActionLevelCode, TemplateKey = a.TemplateKey },
                sortAndPage.CurrentPage,
                sortAndPage.PageSize,
                sortAndPage.SortBy,
                sortAndPage.IsSortDescending,
                actionLevelName);

            sortAndPage.TotalRecordCount = totalRecords;
            if (hasCreateCoookie)
            {
                CreateCookieSearch(actionLevelName, sortAndPage);
            }

            return model;
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActionLevel.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.ActionLevel.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            return View(new ActionLevelModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ActionLevelCreate")]
        public ActionResult Create(ActionLevelModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActionLevel.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.ActionLevel.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                entity.CreatedByUserId = User.GetUserId();
                entity.CreatedOnDate = DateTime.Now;
                try
                {
                    _actionLevelService.Create(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Created"));
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActionLevel.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.ActionLevel.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var actionLevel = _actionLevelService.Get(id);
            if (actionLevel == null)
            {
                return RedirectToAction("Index");
            }
            var model = actionLevel.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ActionLevelEdit")]
        public ActionResult Edit(ActionLevelModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActionLevel.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.ActionLevel.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var actionLevel = _actionLevelService.Get(model.ActionLevelId);
                if (actionLevel == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    return RedirectToAction("Index");
                }

                var oldActionLevel = actionLevel.ActionLevelName;
                actionLevel = model.ToEntity(actionLevel);
                actionLevel.LastModifiedByUserId = User.GetUserId();
                actionLevel.LastModifiedOnDate = DateTime.Now;
                try
                {
                    _actionLevelService.Update(actionLevel, oldActionLevel);
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ActionLevelDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActionLevel.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.ActionLevel.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var actionLevel = _actionLevelService.Get(id);
            if (actionLevel == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _actionLevelService.Delete(actionLevel);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                ErrorNotification(ex.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return RedirectToAction("Index");
        }
    }
}
