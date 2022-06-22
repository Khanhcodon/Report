using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    public class ScopeAreaController : CustomController
    {
        private readonly ScopeAreaBll _scopeAreaService;
        private readonly ClientBll _clientService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "Key";

        public ScopeAreaController(
            ClientBll clientService,
            AdminGeneralSettings generalSettings,
            ScopeAreaBll scopeAreaService,
            ResourceBll resourceService)
            : base()
        {
            _clientService = clientService;
            _scopeAreaService = scopeAreaService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        private string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        private string GetClientsInScopeArea(int id)
        {
            var clients = _scopeAreaService.GetClients();
            return clients.Select(
                                c =>
                                new
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    Domain = c.Domain,
                                    Ip = c.Ip,
                                }).StringifyJs();
        }

        private string GetClients()
        {
            var clients = _clientService.Gets();
            return clients.Select(
                                c =>
                                new
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    Domain = c.Domain,
                                    Ip = c.Ip,
                                }).StringifyJs();
        }

        private void BindData(ScopeAreaModel model)
        {
            var scopes = Enum.GetValues(typeof(Scope)).Cast<Scope>().ToList();
            var scopeList = new List<ScopeModel>();
            foreach (var scope in scopes)
            {
                var scopeModel = new ScopeModel(scope.ToString(), GetDescription(scope));
                scopeList.Add(scopeModel);
            }
            ViewBag.Scopes = scopeList;
            ViewBag.ClientsInScopeArea = model.ClientIds.StringifyJs();
            ViewBag.Clients = GetClients();
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchScopeArea];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchScopeArea, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
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
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchScopeArea];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { }
            }

            int totalRecords;
            var model = _scopeAreaService.Gets(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                key: search, currentPage: sortAndPage.CurrentPage).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Key = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return View(model);
        }

        public ActionResult Create()
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = new ScopeAreaModel();
            BindData(model);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ScopeAreaModel model)
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
                    if (entity.ClientIds != null && entity.ClientIds.Any())
                    {
                        foreach (var clientId in entity.ClientIds)
                        {
                            var clientScope = new ClientScopeModel(clientId, entity.Id);
                            _scopeAreaService.AddClient(clientScope.ToEntity());
                        }
                    }
                    _scopeAreaService.Create(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.Created.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.ScopeArea.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.Created.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.ScopeArea.Created.Error"));
                }
            }
            BindData(model);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var scopeArea = _scopeAreaService.Get(id);
            if (scopeArea == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.NotExist"));
                ErrorNotification(_resourceService.GetResource("Common.ScopeArea.NotExist"));
                return RedirectToAction("Index");
            }

            var model = scopeArea.ToModel();
            BindData(model);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ScopeAreaModel model)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var scopeArea = _scopeAreaService.Get(model.Id);
            if (scopeArea == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.NotExist"));
                ErrorNotification(_resourceService.GetResource("Common.ScopeArea.NotExist"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    scopeArea = model.ToEntity(scopeArea);
                    var clients = _scopeAreaService.GetClients(model.Id);
                    if (clients != null && clients.Any())
                    {
                        foreach (var client in clients)
                        {
                            if (!scopeArea.ClientIds.Any(i => i == client.Id))
                            {
                                var clientScope = _scopeAreaService.GetClientScope(scopeArea.Id, client.Id);
                                _scopeAreaService.DeleteClientScope(clientScope);
                            }
                        }
                    }

                    if (model.ClientIds != null && model.ClientIds.Any())
                    {
                        foreach (var newId in model.ClientIds)
                        {
                            var flag = false;
                            foreach (var client in clients)
                            {
                                if (client.Id == newId)
                                {
                                    flag = true;
                                }
                            }
                            if (!flag)
                            {
                                var clientScope = new ClientScopeModel(newId, scopeArea.Id);
                                _scopeAreaService.AddClient(clientScope.ToEntity());
                            }
                        }
                    }

                    _scopeAreaService.Update(scopeArea);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.ScopeArea.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.ScopeArea.Updated.Error"));
                }
            }
            BindData(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //if (!HasPermission("ApiPermission"))
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            var scopeArea = _scopeAreaService.Get(id);
            if (scopeArea == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.NotExist"));
                ErrorNotification(_resourceService.GetResource("Common.ScopeArea.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _scopeAreaService.Delete(scopeArea);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Common.ScopeArea.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.ScopeArea.Deleted.Error"));
                ErrorNotification(_resourceService.GetResource("Common.ScopeArea.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string key, int pageSize)
        {
            IEnumerable<ScopeAreaModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        key = key.Trim();
                    }

                    int totalRecords;
                    model = _scopeAreaService.Gets(out totalRecords, pageSize: pageSize,
                                                                sortBy: DEFAULT_SORT_BY, isDescending: false,
                                                                key: key).ToListModel();
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.Key = key;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;

                    CreateCookieSearch(key, sortAndPage);
                }
            }
            return PartialView("_List", model);
        }

        public ActionResult SortAndPaging(
            string key, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<ScopeAreaModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = key.Trim();
                }
                int totalRecords;
                model = _scopeAreaService.Gets(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            key: key, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Key = key;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                CreateCookieSearch(key, sortAndPage);
            }
            return PartialView("_List", model);
        }
    }
}
