using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class IncreaseController : CustomController//BaseController
    {
        private readonly IncreaseBll _increaseService;
        private readonly ResourceBll _resourceService;
        private readonly BussinessDocFieldDocTypeGroupBll _bussinessDocFieldDocTypeGroupService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "Name";

        public IncreaseController(IncreaseBll increaseService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            BussinessDocFieldDocTypeGroupBll bussinessDocFieldDocTypeGroupService)
            : base()
        {
            _increaseService = increaseService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _bussinessDocFieldDocTypeGroupService = bussinessDocFieldDocTypeGroupService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Increase.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = string.Empty;
            int? groupId = null;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchIncrease];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    if (data["GroupId"] != null)
                    {
                        groupId = Convert.ToInt32(data["GroupId"].ToString());
                    }
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch
                {
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _increaseService.Gets(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                name: search,
                groupId: groupId,
                currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Name = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.AllGroup = GetAllGroup(groupId);

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage, groupId);
            }
            return View(model);
        }

        public ActionResult Search(string name, int pageSize, int? groupId = null)
        {
            IEnumerable<IncreaseModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    model = _increaseService.Gets(out totalRecords, pageSize: pageSize,
                            sortBy: DEFAULT_SORT_BY, isDescending: true,
                            name: name, groupId: groupId).ToListModel();
                    var sortAndPage = new SortAndPagingModel
                      {
                          PageSize = pageSize,
                          CurrentPage = 1,
                          IsSortDescending = true,
                          SortBy = DEFAULT_SORT_BY,
                          TotalRecordCount = totalRecords
                      };

                    CreateCookieSearch(name, sortAndPage, groupId);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.Name = name;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                    ViewBag.GroupId = groupId;
                }
            }
            return PartialView("_PartialList", model);
        }

        private void CreateCookieSearch(string name, SortAndPagingModel sortpage, int? groupId = null)
        {
            var data = new Dictionary<string, object> { { "Search", name }, { "SortAndPaging", sortpage }, { "GroupId", groupId } };
            var cookie = Request.Cookies[CookieName.SearchIncrease];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchIncrease, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Increase.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            ViewBag.AllGroup = GetAllGroup();
            return View(new IncreaseModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "IncreaseCreate")]
        public ActionResult Create(IncreaseModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Increase.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    if (model.HasCreatePacket)
                    {
                        var names = model.Name.Split(';').Distinct();
                        var list = new List<Increase>();
                        foreach (var name in names)
                        {
                            var item = entity.Clone();
                            item.Name = name;
                            list.Add(item);
                        }
                        _increaseService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _increaseService.Create(entity);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Created"));
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            ViewBag.AllGroup = GetAllGroup();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Increase.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var increase = _increaseService.Get(id);
            if (increase == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllGroup = GetAllGroup();
            return View(increase.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "IncreaseEdit")]
        public ActionResult Edit(IncreaseModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Increase.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var increase = _increaseService.Get(model.IncreaseId);
                if (increase == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Error"));
                    return RedirectToAction("Index");
                }
                var oldName = increase.Name;
                increase = model.ToEntity(increase);
                try
                {
                    _increaseService.Update(increase, oldName);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            ViewBag.AllGroup = GetAllGroup();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "IncreaseDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Increase.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var increase = _increaseService.Get(id);
            if (increase != null)
            {
                try
                {
                    _increaseService.Delete(increase);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ResetIncrease()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Increase.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var allIncreases = _increaseService.Gets(isReadOnly: false);
            if (allIncreases != null && allIncreases.Any())
            {
                try
                {
                    using (var trans = new TransactionScope(TransactionScopeOption.Required))
                    {
                        _increaseService.ResetIncrease(allIncreases);
                        trans.Complete();
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.ResetIncrease.Error"));
                    ErrorNotification(_resourceService.GetResource("Customer.Increase.ResetIncrease.Error"));
                    return RedirectToAction("Index");
                }
            }

            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Increase.ResetIncrease.Successed"));
            SuccessNotification(_resourceService.GetResource("Customer.Increase.ResetIncrease.Successed"));
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(string name, string sortBy,
            bool isSortDesc, int page, int pageSize, int? groupId = null)
        {
            IEnumerable<IncreaseModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _increaseService.Gets(out totalRecords,
                    sortBy: sortBy, currentPage: page, pageSize: pageSize,
                    isDescending: isSortDesc, name: name, groupId: groupId).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(name, sortAndPage, groupId);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Name = name;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                ViewBag.GroupId = groupId;
            }
            return PartialView("_PartialList", model);
        }

        private IEnumerable<SelectListItem> GetAllGroup(int? groupId = null)
        {
            var groups = _bussinessDocFieldDocTypeGroupService.GetsAs(p =>
              new
              {
                  p.Name,
                  p.BussinessDocFieldDocTypeGroupId
              });
            if (groups == null || !groups.Any())
            {
                throw new ApplicationException();
            }

            return groups.Select(p => new SelectListItem
            {
                Value = p.BussinessDocFieldDocTypeGroupId.ToString(),
                Text = p.Name,
                Selected = groupId.HasValue && p.BussinessDocFieldDocTypeGroupId == groupId.Value
            });
        }
    }
}
