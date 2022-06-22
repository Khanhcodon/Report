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
    public class DocFieldController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DocFieldBll _docFieldService;
        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;
        private readonly StoreBll _storeService;
        private readonly WorkflowBll _workflowService;
        private readonly DocTypeBll _docTypeService;

        private const string DEFAULT_SORT_BY = "DocFieldName";

        public DocFieldController(
            DocFieldBll docFieldService,
            AdminGeneralSettings generalSettings,
            ResourceBll resourceService,
            UserBll userService,
            StoreBll storeService,
            WorkflowBll workflowService,
             DocTypeBll docTypeService)
            : base()
        {
            _docFieldService = docFieldService;
            _generalSettings = generalSettings;
            _resourceService = resourceService;
            _userService = userService;
            _storeService = storeService;
            _workflowService = workflowService;
            _docTypeService = docTypeService;
        }

        public ActionResult Index()
        {
            // var test = _docFieldService.GetDocFields(1);
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocField.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var categoryBusinessId = 0;
            var name = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY,
            };
            var httpCookie = Request.Cookies[CookieName.SearchDocfield];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    categoryBusinessId = data["categoryBusinessId"] == null ? 0 : int.Parse(data["categoryBusinessId"].ToString());
                    name = data["Name"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }
            int totalRecords;
            var model = _docFieldService.GetsAs(out totalRecords, d => new DocFieldModel
                {
                    DocFieldId = d.DocFieldId,
                    DocFieldName = d.DocFieldName,
                    IsActivated = d.IsActivated
                },
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                pageSize: sortAndPage.PageSize,
                currentPage: sortAndPage.CurrentPage,
                categoryBusinessId: categoryBusinessId,
                name: name);

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Name = name;
            BindCategoryBusiness(categoryBusinessId);
            if (isInvalidCookie)
            {
                CreateCookieSearch(categoryBusinessId, name, sortAndPage);
            }
            return View(model);
        }

        private void BindCategoryBusiness(int? categoryBusinessId = null)
        {
            var listCategoryBusiness = _resourceService.EnumToSelectList<CategoryBusinessTypes>(categoryBusinessId);
            ViewBag.AllCategoryBusiness = listCategoryBusiness;
        }

        private void CreateCookieSearch(int categoryBusinessId, string name, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Name", name }, { "categoryBusinessId", categoryBusinessId }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchDocfield];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchDocfield, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocField.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            BindCreate();
            var model = new DocFieldModel { IsActivated = true };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DocFieldModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocField.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                model.CategoryBusinessId = 0;
                //TrinhNVd: model.CategoryBusiness = null
                var categoryBusiness = _resourceService.EnumToSelectList<CategoryBusinessTypes>().Select(x => x.Value);
                model.CategoryBusiness = categoryBusiness.Select(int.Parse).ToList();
                foreach (var cate in model.CategoryBusiness)
                {
                    model.CategoryBusinessId |= cate;
                }

                var entityCreate = model.ToEntity();
                var userCreate = _userService.CurrentUser;
                entityCreate.CreatedByUserId = userCreate.UserId;
                entityCreate.CreatedOnDate = DateTime.Now;
                entityCreate.LastModifiedByUserId = userCreate.UserId;
                entityCreate.LastModifiedOnDate = DateTime.Now;

                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.DocFieldName.Split(';').Distinct();
                        var list = new List<DocField>();
                        foreach (var name in names)
                        {
                            var item = entityCreate.Clone();
                            item.DocFieldName = name;
                            list.Add(item);
                        }
                        _docFieldService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _docFieldService.Create(entityCreate);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Created.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Created.Success"));
                    ModelState.Clear();
                    BindCategoryBusiness(entityCreate.CategoryBusinessId);
                    BindStore();
                    return View(new DocFieldModel
                    {
                        DocFieldName = "",
                        IsActivated = true,
                        CategoryBusinessId = entityCreate.CategoryBusinessId
                    });
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Created.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Created.Error"));
                }
            }

            BindStore();
            BindCategoryBusiness();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocField.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var docField = _docFieldService.Get(id);
            if (docField == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                return RedirectToAction("Index");
            }

            ViewBag.DocFieldId = id;
            BindCreate();
            var model = docField.ToModel();

            var allDocType = _docTypeService.GetsAs(p => new
            {
                p.DocTypeId,
                p.DocTypeName,
                p.DocFieldId
            });
            ViewBag.SelectedDocType = allDocType.Where(p => p.DocFieldId == id).Stringify();
            ViewBag.AllDocTypes = allDocType.Stringify();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DocFieldModel model)
        {
            //Hopcv: 190614
            ////Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocField.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var docField = _docFieldService.Get(model.DocFieldId);
            if (docField == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                model.CategoryBusinessId = 0;
                foreach (var cate in model.CategoryBusiness)
                {
                    model.CategoryBusinessId |= cate;
                }

                var oldDocFieldName = docField.DocFieldName;
                docField.LastModifiedByUserId = User.GetUserId();
                docField.LastModifiedOnDate = DateTime.Now;
                try
                {
                    _docFieldService.Update(model.ToEntity(docField), oldDocFieldName, model.DocTypeIds);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Updated.Error"));
                }
            }

            ViewBag.DocFieldId = model.DocFieldId;
            BindCategoryBusiness(model.CategoryBusinessId);
            BindStore();
            var allDocType = _docTypeService.GetsAs(p => new
            {
                p.DocTypeId,
                p.DocTypeName,
                p.DocFieldId
            });
            ViewBag.SelectedDocType = allDocType.Where(p => p.DocFieldId == model.DocFieldId).Stringify();
            ViewBag.AllDocTypes = allDocType.Stringify();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocField.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var docField = _docFieldService.Get(id);
            if (docField == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _docFieldService.Delete(docField);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Deleted.Success"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Deleted.Error"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(
            string sortBy, bool isSortDesc,
            int page, int pageSize,
            int categoryBusinessId, string name)
        {
            IEnumerable<DocFieldModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _docFieldService.GetsAs(out totalRecords, d => new DocFieldModel
                {
                    DocFieldId = d.DocFieldId,
                    DocFieldName = d.DocFieldName,
                    IsActivated = d.IsActivated
                }, sortBy: sortBy,
                isDescending: isSortDesc,
                pageSize: pageSize,
                currentPage: page,
                categoryBusinessId: categoryBusinessId,
                name: name);

                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(categoryBusinessId, name, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                BindCategoryBusiness(categoryBusinessId);
            }
            ViewBag.Name = name;
            return PartialView("_PartialList", model);
        }

        public ActionResult GetByCategoryBusiness(int categoryBusinessId, string name)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchDocfield];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
            }

            int totalRecords;
            var model = _docFieldService.GetsAs(out totalRecords, d => new DocFieldModel
            {
                DocFieldId = d.DocFieldId,
                DocFieldName = d.DocFieldName,
                IsActivated = d.IsActivated
            },
            sortBy: sortAndPage.SortBy,
            isDescending: sortAndPage.IsSortDescending,
            pageSize: sortAndPage.PageSize,
            currentPage: sortAndPage.CurrentPage,
            categoryBusinessId: categoryBusinessId,
            name: name);

            BindCategoryBusiness(categoryBusinessId);

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Name = name;

            CreateCookieSearch(categoryBusinessId, name, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult FindWorkflow(int docFieldId, DocTypeWorkflowSearchModel search)
        {
            // var workflowIds = _docFieldService.GetWorkFlows(p => p.WorkflowId, p => p.DocFieldId == docFieldId);
            var workflowIds = _docFieldService.Raw
                        .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                        p => p.DocFieldId, x => x.DocFieldId,
                        (p, x) => new { DocField = p, DocfieldDoctypeWorkflow = x })
                        .Where(p => p.DocfieldDoctypeWorkflow.DocFieldId == docFieldId)
                        .Select(p => p.DocfieldDoctypeWorkflow.WorkflowId).ToList();
            var allWorkflows = _workflowService.GetsAs(p => new WorkflowModel
            {
                WorkflowId = p.WorkflowId,
                WorkflowName = p.WorkflowName,
                IsActivated = p.IsActivated
            },
             p => !workflowIds.Contains(p.WorkflowId)
                 && (!search.IsActivatated.HasValue || (search.IsActivatated.HasValue && p.IsActivated == search.IsActivatated.Value))
                 && (string.IsNullOrEmpty(search.SearchKey) || (!string.IsNullOrEmpty(search.SearchKey) && p.WorkflowName.Contains(search.SearchKey))));
            ViewBag.Search = search;
            ViewBag.AllWorkflows = allWorkflows;
            ViewBag.DocFieldId = docFieldId;
            return PartialView("_PartialWorkflow");
        }

        public ActionResult AddDocFieldWorkflow(int id)
        {
            var workflowIds = _docFieldService.Raw
                  .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                  p => p.DocFieldId, x => x.DocFieldId,
                  (p, x) => new { DocField = p, DocfieldDoctypeWorkflow = x })
                  .Where(p => p.DocfieldDoctypeWorkflow.DocFieldId == id)
                  .Select(p => p.DocfieldDoctypeWorkflow.WorkflowId).ToList();
            var notExist = _workflowService.GetsAs(p => new WorkflowModel
            {
                WorkflowId = p.WorkflowId,
                WorkflowName = p.WorkflowName,
                IsActivated = p.IsActivated
            }, p => !workflowIds.Contains(p.WorkflowId));

            ViewBag.DocFieldId = id;
            ViewBag.AllWorkflows = notExist;
            return PartialView("AddDocFieldWorkflow");
        }

        [HttpPost]
        public JsonResult UpdateDocFieldWorkflow(int id, List<int> workflowIds)
        {
            if (workflowIds == null || !workflowIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.UpdateDocFieldWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.DocField.UpdateDocFieldWorkflow.Success") });
            }
            try
            {
                _docFieldService.UpdateWorkflows(id, workflowIds);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.UpdateDocFieldWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.DocField.UpdateDocFieldWorkflow.Success") });
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                return Json(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.UpdateDocFieldWorkflow.Error"));
                return Json(new { error = _resourceService.GetResource("Customer.DocField.UpdateDocFieldWorkflow.Error") });
            }
        }

        [HttpPost]
        public JsonResult ChangeDocFieldWorkflowStatus(int docFieldId, int workflowId, bool status)
        {
            try
            {
                _docFieldService.ChangeActivatedWorkflows(docFieldId, workflowId, status);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.ChangeDocFieldWorkflowStatus.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.DocField.ChangeDocFieldWorkflowStatus.Success") });
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                return Json(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.ChangeDocFieldWorkflowStatus.Error"));
                return Json(new { error = _resourceService.GetResource("Customer.DocField.ChangeDocFieldWorkflowStatus.Error") });
            }
        }

        [HttpPost]
        public ActionResult DeleteDocFieldWorkFlow(int id, int workflowId)
        {
            try
            {
                _docFieldService.DeleteWorkflows(id, new List<int> { workflowId });
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.DeleteDocFieldWorkFlow.Success"));
                SuccessNotification(_resourceService.GetResource("Customer.DocField.DeleteDocFieldWorkFlow.Success"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocField.DeleteDocFieldWorkFlow.Error"));
                SuccessNotification(_resourceService.GetResource("Customer.DocField.DeleteDocFieldWorkFlow.Error"));
            }

            return RedirectToAction("DocFieldWorkflow", new { id = id });
        }

        public ActionResult DocFieldWorkflow(int id)
        {
            var docField = _docFieldService.Get(id);
            if (docField == null)
            {
                return RedirectToAction("Index");
            }

            var docFieldWorkflows = _workflowService.Raw
                       .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                           p => p.WorkflowId, x => x.WorkflowId,
                           (p, x) => new { Workflow = p, DocfieldDoctypeWorkflow = x })
                       .Where(p => p.DocfieldDoctypeWorkflow.DocFieldId == id)
                       .Select(p => new WorkflowModel
                       {
                           WorkflowId = p.Workflow.WorkflowId,
                           WorkflowName = p.Workflow.WorkflowName,
                           IsActivated = p.DocfieldDoctypeWorkflow.IsActivated
                       }).ToList();

            ViewBag.DocFieldId = id;
            ViewBag.DocFieldWorkflows = docFieldWorkflows;

            return PartialView("DocFieldWorkflow");
        }

        private void BindCreate()
        {
            var categoryBusinessId = 0;
            var httpCookie = Request.Cookies[CookieName.SearchDocfield];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                categoryBusinessId = data["categoryBusinessId"] == null ? 0 : int.Parse(data["categoryBusinessId"].ToString());
            }

            BindCategoryBusiness(categoryBusinessId);
            BindStore();
        }

        public void BindStore()
        {
            var stores = _storeService.GetsAs(p => new { value = p.StoreId, label = p.StoreName });
            ViewBag.AllStores = stores.Stringify();
        }
    }
}
