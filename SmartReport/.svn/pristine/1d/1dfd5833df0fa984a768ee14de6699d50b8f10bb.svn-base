using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TransferTypeController : CustomController//BaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly TransferTypeBll _transferTypeService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "TransferTypeName";

        public TransferTypeController(
            TransferTypeBll transferTypeService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _transferTypeService = transferTypeService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TransferType.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.TransferType.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var transfertypename = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchTransferType];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    transfertypename = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }
            int totalRecords;
            var model = _transferTypeService.Gets(out totalRecords, pageSize: sortAndPage.PageSize,
                                                                sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                                                                transfertypename: transfertypename).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(transfertypename, sortAndPage);
            }
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.TransferTypeName = transfertypename;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TransferType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.TransferType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            return View(new TransferTypeModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TransferTypeCreate")]
        public ActionResult Create(TransferTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TransferType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.TransferType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var transfertype = model.ToEntity();
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.TransferTypeName.Split(';').Distinct();
                        var list = new List<TransferType>();
                        foreach (var name in names)
                        {
                            var item = transfertype.Clone();
                            item.TransferTypeName = name;
                            list.Add(item);
                        }
                        _transferTypeService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _transferTypeService.Create(transfertype);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TransferType.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TransferType.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                return RedirectToAction("Create");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.TransferType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var transfertype = _transferTypeService.Get(id);
            if (transfertype == null)
            {
                return RedirectToAction("Index");
            }
            var model = transfertype.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TransferTypeEdit")]
        public ActionResult Edit(TransferTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.TransferType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var transfertype = _transferTypeService.Get(model.TransferTypeId);
                if (transfertype != null)
                {
                    var oldTransferTypeName = transfertype.TransferTypeName;
                    try
                    {
                        _transferTypeService.Update(model.ToEntity(transfertype), oldTransferTypeName);
                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        ErrorNotification(ex.Message);
                        return View(model);
                    }
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TransferType.Updated"));
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TransferTypeDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.TransferType.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var transfertype = _transferTypeService.Get(id);
            if (transfertype == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.TransferType.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _transferTypeService.Delete(transfertype);
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TransferType.Deleted"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string transfertypename, int pageSize)
        {
            IEnumerable<TransferTypeModel> model = null;
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
                    var httpCookie = Request.Cookies[CookieName.SearchTransferType];
                    if (httpCookie != null)
                    {
                        var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                        sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                    }

                    int totalRecords;
                    model = _transferTypeService.Gets(out totalRecords, pageSize: pageSize,
                                                                sortBy: DEFAULT_SORT_BY, isDescending: sortAndPage.IsSortDescending,
                                                                transfertypename: transfertypename).ToListModel();
                    sortAndPage.TotalRecordCount = totalRecords;
                    CreateCookieSearch(transfertypename, sortAndPage);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.TransferTypeName = transfertypename;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                }
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            string transfertypename,
            string sortBy,
            bool isSortDesc,
            int page,
            int pageSize)
        {
            IEnumerable<TransferTypeModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(transfertypename))
                {
                    transfertypename = transfertypename.Trim();
                }
                int totalRecords;
                model = _transferTypeService.Gets(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            transfertypename: transfertypename, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(transfertypename, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.TransferTypeName = transfertypename;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            return PartialView("_PartialList", model);
        }

        private void CreateCookieSearch(string transfertypename, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", transfertypename }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchTransferType];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchTransferType, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }
    }
}
