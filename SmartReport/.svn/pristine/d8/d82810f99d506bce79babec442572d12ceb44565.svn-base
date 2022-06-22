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
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class FormGroupController : CustomController// BaseController
    {
        private readonly FormGroupBll _formGroupService;
        private readonly FormBll _formService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "FormGroupName";

        public FormGroupController(
            FormGroupBll formGroupService,
            ResourceBll resourceService,
             FormBll formService)
            : base()
        {
            _formGroupService = formGroupService;
            _resourceService = resourceService;
            _formService = formService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormGroup.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.FormGroup.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var sortAndPage = new SortAndPagingModel
            {
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SortFormGroup];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }
            var model = _formGroupService.Gets(sortAndPage.SortBy, sortAndPage.IsSortDescending).ToListModel();
            if (isInvalidCookie)
            {
                CreateCookieSearch(sortAndPage);
            }
            ViewBag.SortAndPage = sortAndPage;
            return View(model);
        }

        private void CreateCookieSearch(SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SortFormGroup];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SortFormGroup, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormGroup.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.FormGroup.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            return View(new FormGroupModel());
        }

        [HttpPost]
        public ActionResult Create(FormGroupModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormGroup.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.FormGroup.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    if (model.HasCreatePacket)
                    {
                        var tmpNames = model.FormGroupName.Split(';').Distinct();
                        var list = new List<FormGroup>();
                        foreach (string value in tmpNames)
                        {
                            var tmp = entity.Clone();
                            tmp.FormGroupName = value;
                            list.Add(tmp);
                        }
                        _formGroupService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _formGroupService.Create(entity);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Created"));
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
                catch (Exception ex)
                {
                    LogException(ex);
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormGroup.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.FormGroup.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var formGroup = _formGroupService.Get(id);
            if (formGroup == null)
            {
                return RedirectToAction("Index");
            }

            var allForms = _formService.GetsAs(p => new
            {
                p.FormId,
                p.FormName,
                p.FormGroupId
            });

            ViewBag.SelectedForms = allForms.Where(p => p.FormGroupId == id).Stringify();
            ViewBag.AllForms = allForms.Stringify();
            return View(formGroup.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(FormGroupModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormGroup.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.FormGroup.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var formGroup = _formGroupService.Get(model.FormGroupId);
                if (formGroup == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Error"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Error"));
                    return RedirectToAction("Index");
                }
                var oldName = formGroup.FormGroupName;
                formGroup = model.ToEntity(formGroup);
                try
                {
                    _formGroupService.Update(formGroup, oldName, model.FormIds);
                    CreateActivityLog(ActivityLogType.Admin, "Update form group thành công");
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            var allForms = _formService.GetsAs(p => new
            {
                p.FormId,
                p.FormName,
                p.FormGroupId
            });

            ViewBag.SelectedForms = allForms.Where(p => p.FormGroupId == model.FormGroupId).Stringify();
            ViewBag.AllForms = allForms.Stringify();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormGroup.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.FormGroup.NotPermissionDelete"));
                return RedirectToAction("Index", "Welcome");
            }

            var formGroup = _formGroupService.Get(id);
            if (formGroup != null)
            {
                try
                {
                    _formGroupService.Delete(formGroup);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(string name, string sortBy, bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<FormGroupModel> model = null;
            if (Request.IsAjaxRequest())
            {
                model = _formGroupService.Gets(sortBy, isSortDesc).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                };
                CreateCookieSearch(sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
            }
            return PartialView("_PartialList", model);
        }
    }
}
