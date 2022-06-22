using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class LevelController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly ApiBll _apiService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly LevelBll _levelService;
        private readonly OfficeBll _officeService;
        private const string DEFAULT_SORT_BY = "AdministrativeName";

        public LevelController(
            ResourceBll resourceService,
           AdminGeneralSettings generalSettings,
            LevelBll levelService,
            ApiBll apiService,
            OfficeBll officeService)
            : base()
        {
            _apiService = apiService;
            _levelService = levelService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _officeService = officeService;
        }

        public ActionResult Index()
        {
            //var test = _apiService.GetApiEnums();
            int totalRecords;
            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchLevel];
            var isValid = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { isValid = true; }
            }

            var model = _levelService.GetLevels(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                levelName: search,
                currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.LevelName = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            if (isValid)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchLevel];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchLevel, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult SortAndPaging(
            string levelName, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {

            IEnumerable<LevelModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(levelName))
                {
                    levelName = levelName.Trim();
                }
                int totalRecords;
                model = _levelService.GetLevels(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            levelName: levelName, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(levelName, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.LevelName = levelName;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }
            return PartialView("_PartialList", model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var level = _levelService.Get(id);
            if (level == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Level.Message.Delete.Error"));
                ErrorNotification(_resourceService.GetResource("Common.Level.Message.Delete.Error"));
            }
            else
            {
                _levelService.Delete(level);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Level.Message.Delete.Success"));
                SuccessNotification(_resourceService.GetResource("Common.Level.Message.Delete.Success"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            LoadDropDownList();
            return View(new LevelModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(LevelModel model)
        {
            LoadDropDownList(model.Type);
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    _levelService.Create(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.CreateOrEdit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.CreateOrEdit.Success"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            else
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.CreateOrEdit.Fail"));
                ErrorNotification(_resourceService.GetResource("Common.CreateOrEdit.Fail"));
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var level = _levelService.Get(id);
            if (level == null)
            {
                return RedirectToAction("Index");
            }
            var model = level.ToModel();
            LoadDropDownList(model.Type);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(LevelModel model)
        {
            LoadDropDownList(model.Type);
            if (ModelState.IsValid)
            {
                var level = _levelService.Get(model.LevelId);
                if (level == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Level.Message.DeleteBeforeEdit.Exist"));
                    ErrorNotification(_resourceService.GetResource("Common.Level.Message.DeleteBeforeEdit.Exist"));
                    return RedirectToAction("Index");
                }
                var oldLevelName = level.AdministrativeName;
                try
                {
                    level = model.ToEntity(level);
                    _levelService.Update(level, oldLevelName);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Level.Message.Edit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.Level.Message.Edit.Success"));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private void LoadDropDownList(int? type = 0)
        {
            ViewBag.ListType = _resourceService.EnumToSelectList<LevelType>(type);
        }
    }
}
