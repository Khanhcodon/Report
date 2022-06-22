using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ClientController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ClientBll _clientService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "Name";

        public ClientController(
            AdminGeneralSettings generalSettings,
            ClientBll clientService,
            ResourceBll resourceService
            )
            : base()
        {
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _clientService = clientService;
        }

        public ActionResult Index()
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY,
            };

            var httpCookie = Request.Cookies[CookieName.SearchClient];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception)
                {
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _clientService.Gets(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                name: search,
                currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Name = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchClient];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchClient, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult Edit(int id)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            var client = _clientService.Get(id);
            if (client == null)
            {
                ErrorNotification(_resourceService.GetResource("Common.Client.NotExist"));
                return RedirectToAction("Index");
            }
            var model = client.ToModel();
            ViewBag.Scopes = Enum.GetValues(typeof(Scope)).Cast<Scope>().ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ClientModel model)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            var client = _clientService.Get(model.Id);
            if (client == null)
            {
                ErrorNotification(_resourceService.GetResource("Common.Client.NotExist"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client = model.ToEntity(client);
                    _clientService.Update(client);
                    SuccessNotification(_resourceService.GetResource("Common.Client.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Common.Client.Updated.Error"));
                }
            }

            return View(model);
        }

        public ActionResult Create()
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            ViewBag.Scopes = Enum.GetValues(typeof(Scope)).Cast<Scope>().ToList();
            return View(new ClientModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ClientModel model)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    _clientService.Create(entity);
                    SuccessNotification(_resourceService.GetResource("Common.Client.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Common.Client.Created.Error"));
                }
            }
            return View(model);
        }

        public ActionResult Search(string key, int pageSize)
        {
            IEnumerable<ClientModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    model = _clientService.Gets(out totalRecords,
                        pageSize: pageSize,
                        sortBy: DEFAULT_SORT_BY, isDescending: false,
                        name: key).ToListModel();

                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };

                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.Name = key;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;

                    CreateCookieSearch(key, sortAndPage);
                }
            }
            return PartialView("_List", model);
        }

        public ActionResult SortAndPaging(
            string key, string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            IEnumerable<ClientModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _clientService.Gets(out totalRecords,
                    pageSize: pageSize,
                    sortBy: sortBy,
                    isDescending: isSortDesc,
                    name: key,
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
                ViewBag.Name = key;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;

                CreateCookieSearch(key, sortAndPage);
            }
            return PartialView("_List", model);
        }

        public JsonResult SetActive(int id, bool actived)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var client = _clientService.Get(id);
            if (client == null)
            {
                return Json(new { error = _resourceService.GetResource("Common.Client.NotExist") }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                client.IsActivated = actived;
                _clientService.Update(client);
                return Json(new { success = _resourceService.GetResource("Common.User.Message.SetActive.Success") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = _resourceService.GetResource("Common.User.Message.SetActive.Error") }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            var client = _clientService.Get(id);
            if (client == null)
            {
                ErrorNotification(_resourceService.GetResource("Common.Client.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                //todo: xem chỗ nào map thì xóa trước khi xóa client
                //_clientService.Delete(client);
                SuccessNotification(_resourceService.GetResource("Common.Client.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(_resourceService.GetResource("Common.Client.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }
    }
}