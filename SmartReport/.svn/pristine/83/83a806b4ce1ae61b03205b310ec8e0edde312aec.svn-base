using System;
using System.Collections.Generic;
using System.Globalization;
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
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class CodeController : CustomController// BaseController
    {
        private readonly CodeBll _codeService;
        private readonly IncreaseBll _increaseService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DepartmentBll _departmentService;
        private readonly BussinessDocFieldDocTypeGroupBll _bussinessDocFieldDocTypeGroupService;

        private const string DEFAULT_SORT_BY = "CodeName";

        public CodeController(CodeBll codeService,
            IncreaseBll increaseService,
            ResourceBll resourceService,
            DepartmentBll departmentService,
            AdminGeneralSettings generalSettings,
             BussinessDocFieldDocTypeGroupBll bussinessDocFieldDocTypeGroupService)
            : base()
        {
            _resourceService = resourceService;
            _codeService = codeService;
            _generalSettings = generalSettings;
            _increaseService = increaseService;
            _departmentService = departmentService;
            _bussinessDocFieldDocTypeGroupService = bussinessDocFieldDocTypeGroupService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Code.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Code.NotPermission"));
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
            var httpCookie = Request.Cookies[CookieName.SearchCode];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    groupId = data["GroupId"] == null ? 0 : int.Parse(data["GroupId"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            var model = GetCodes(search, sortAndPage, groupId, isInvalidCookie);

            ViewBag.CodeName = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.AllGroup = GetAllGroup(groupId);
            return View(model);
        }

        private void CreateCookieSearch(string codeKey, SortAndPagingModel sortpage, int? groupId = null)
        {
            var data = new Dictionary<string, object> { { "GroupId", groupId }, { "Search", codeKey }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchCode];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchCode, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(string codeName, int pageSize, int? groupId = null)
        {
            IEnumerable<CodeModel> model = null;
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
                    model = GetCodes(codeName, sortAndPage, groupId);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.CodeName = codeName;
                    ViewBag.GroupId = groupId;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                }
            }
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(string codeName,
            string sortBy, bool isSortDesc, int page, int pageSize, int? groupId = null)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                IsSortDescending = isSortDesc,
                SortBy = sortBy
            };
            var model = GetCodes(codeName, sortAndPage, groupId);

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.CodeName = codeName;
            ViewBag.GroupId = groupId;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_PartialList", model);
        }

        private IEnumerable<CodeModel> GetCodes(string codeKey, SortAndPagingModel sortAndPage, int? groupId = null, bool hasCreateCoookie = true)
        {
            int totalRecords;
            if (groupId == 0)
            {
                groupId = null;
            }

            var model = _codeService.GetsAs(out totalRecords,
                c => new CodeModel { CodeId = c.CodeId, CodeName = c.CodeName, Template = c.Template },
                sortAndPage.CurrentPage,
                sortAndPage.PageSize,
                sortAndPage.SortBy,
                sortAndPage.IsSortDescending,
                codeKey, groupId);

            sortAndPage.TotalRecordCount = totalRecords;
            if (hasCreateCoookie)
            {
                CreateCookieSearch(codeKey, sortAndPage, groupId);
            }

            return model;
        }

        private string GetAllDepartments()
        {
            var result = "[]";
            var allDepartments = _departmentService.GetCacheAllDepartments(true);
            if (allDepartments != null)
            {
                result = allDepartments
                            .Select(u =>
                                new
                                {
                                    value = u.DepartmentId,
                                    label = u.DepartmentName,
                                    parentid = u.ParentId.HasValue ? u.ParentId : 0,
                                    data = u.DepartmentName,
                                    metadata = new { id = u.DepartmentId },
                                    state = "leaf",
                                    extvalue = u.DepartmentIdExt,
                                    path = u.DepartmentPath,
                                    order = u.Order,
                                    level = u.Level,
                                    attr = new { id = u.DepartmentId, rel = u.IsActivated ? "dept" : "dept-deactivated" }
                                }
                            )
                            .OrderBy(d => d.label).StringifyJs();
            }
            return result;
        }

        private void RebindDepartmentIncrease(int? increaseId = null)
        {
            ViewBag.AllDepartments = GetAllDepartments();
            ViewBag.Increase = _increaseService.GetsAs(i => new
            {
                i.Name,
                i.IncreaseId,
                i.Value
            }).Select(
                        i =>
                        new SelectListItem
                        {
                            Text = i.Name + " (" + i.Value + ")",
                            Value = i.IncreaseId.ToString(CultureInfo.InvariantCulture),
                            Selected = increaseId.HasValue && i.IncreaseId == increaseId,
                        });
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Code.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Code.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            ViewBag.AllGroup = GetAllGroup();
            RebindDepartmentIncrease();
            return View(new CodeModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "CodeCreate")]
        public ActionResult Create(CodeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Code.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Code.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var increase = _increaseService.Get(model.IncreaseId);
                if (increase == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Notfoud"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Notfoud") + model.IncreaseId);
                    RebindDepartmentIncrease(model.IncreaseId);
                    return View(model);
                }
                var entity = model.ToEntity();
                entity.CreatedByUserId = User.GetUserId();
                entity.CreatedOnDate = DateTime.Now;
                entity.NumberLastest = increase.Value;
                try
                {
                    _codeService.Create(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.Created"));
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            ViewBag.AllGroup = GetAllGroup();
            RebindDepartmentIncrease(model.IncreaseId);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Code.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Code.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var code = _codeService.Get(id);
            if (code == null)
            {
                return RedirectToAction("Index");
            }
            var model = code.ToModel();
            RebindDepartmentIncrease(model.IncreaseId);
            ViewBag.AllGroup = GetAllGroup();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "CodeEdit")]
        public ActionResult Edit(CodeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Code.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Code.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var code = _codeService.Get(model.CodeId);
                if (code == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.NotExist"));
                    return RedirectToAction("Index");
                }

                var oldCode = code.CodeName;
                if (model.IncreaseId != code.IncreaseId)
                {
                    var increase = _increaseService.Get(model.IncreaseId);
                    if (increase == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Notfoud"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Notfoud") + model.IncreaseId);
                        RebindDepartmentIncrease(model.IncreaseId);
                        return View(model);
                    }
                    model.NumberLastest = increase.Value;
                }
                code = model.ToEntity(code);
                code.LastModifiedByUserId = User.GetUserId();
                code.LastModifiedOnDate = DateTime.Now;
                try
                {
                    _codeService.Update(code, oldCode);
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            ViewBag.AllGroup = GetAllGroup();
            RebindDepartmentIncrease(model.IncreaseId);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "CodeDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Code.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Code.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var code = _codeService.Get(id);
            if (code == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _codeService.Delete(code);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.Deleted.Success"));
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
