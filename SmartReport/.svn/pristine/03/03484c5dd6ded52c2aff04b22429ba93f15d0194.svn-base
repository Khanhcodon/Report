using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
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
    public class WorkflowController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly WorkflowBll _workflowService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocFieldBll _docFieldService;
        private readonly UserBll _userService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly InterfaceConfigBll _interfaceConfigService;

        private const string TEMPLALTE_PATH = "~/Content/TemplateUICategoryBusiness";
        private const string DEFAULT_SORT_BY = "CreatedOnDate";

        public WorkflowController(
            WorkflowBll workflowService,
            AdminGeneralSettings generalSettings,
            ResourceBll resourceService,
            DocTypeBll docTypeService,
            UserBll userService,
            DepartmentBll departmentService,
            PositionBll positionService,
            DocFieldBll docFieldService,
             InterfaceConfigBll interfaceConfigService)
            : base()
        {
            _workflowService = workflowService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _docTypeService = docTypeService;
            _userService = userService;
            _departmentService = departmentService;
            _positionService = positionService;
            _docFieldService = docFieldService;
            _interfaceConfigService = interfaceConfigService;
        }

        #region workflow

        public ActionResult Index()
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var search = new WorkflowSearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchWorkflow];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<WorkflowSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { }
            }

            int totalRecords;
            var model = _workflowService.GetsAs(out totalRecords,
                                                p => new WorkflowModel
                                                {
                                                    WorkflowId = p.WorkflowId,
                                                    WorkflowName = p.WorkflowName,
                                                    CreatedOnDate = p.CreatedOnDate,
                                                    IsActivated = p.IsActivated
                                                },
                                                pageSize: sortAndPage.PageSize,
                                                sortBy: sortAndPage.SortBy,
                                                isDescending: sortAndPage.IsSortDescending,
                                                currentPage: sortAndPage.CurrentPage,
                                                workflowName: search.WorkflowName,
                                                isActivated: search.IsActivated,
                                                docFieldId: search.DocFieldId,
                                                docTypeId: search.DocTypeId);

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;
            BindFilter();
            return View(model);
        }

        private void CreateCookieSearch(WorkflowSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchWorkflow];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchWorkflow, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            // cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(WorkflowSearchModel search, int pageSize)
        {
            IEnumerable<WorkflowModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    model = _workflowService.GetsAs(out totalRecords,
                                                   p => new WorkflowModel
                                                   {
                                                       WorkflowId = p.WorkflowId,
                                                       WorkflowName = p.WorkflowName,
                                                       CreatedOnDate = p.CreatedOnDate,
                                                       IsActivated = p.IsActivated
                                                   },
                                                   pageSize: pageSize,
                                                   sortBy: DEFAULT_SORT_BY,
                                                   isDescending: true,
                                                   workflowName: search.WorkflowName,
                                                   isActivated: search.IsActivated,
                                                   docFieldId: search.DocFieldId,
                                                   docTypeId: search.DocTypeId);
                    sortAndPage = new SortAndPagingModel
                   {
                       PageSize = pageSize,
                       CurrentPage = 1,
                       IsSortDescending = true,
                       SortBy = DEFAULT_SORT_BY,
                       TotalRecordCount = totalRecords
                   };
                }
                else
                {
                    sortAndPage = new SortAndPagingModel
                   {
                       PageSize = pageSize,
                       CurrentPage = 1,
                       IsSortDescending = false,
                       SortBy = DEFAULT_SORT_BY,
                       TotalRecordCount = 0
                   };
                }
            }

            ViewBag.Search = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            WorkflowSearchModel search, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<WorkflowModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _workflowService.GetsAs(out totalRecords,
                                                p => new WorkflowModel
                                                {
                                                    WorkflowId = p.WorkflowId,
                                                    WorkflowName = p.WorkflowName,
                                                    CreatedOnDate = p.CreatedOnDate,
                                                    IsActivated = p.IsActivated
                                                },
                                                pageSize: pageSize,
                                                sortBy: sortBy,
                                                isDescending: isSortDesc,
                                                currentPage: page,
                                                workflowName: search.WorkflowName,
                                                isActivated: search.IsActivated,
                                                docFieldId: search.DocFieldId,
                                                docTypeId: search.DocTypeId);

                sortAndPage = new SortAndPagingModel
               {
                   PageSize = pageSize,
                   CurrentPage = page,
                   IsSortDescending = isSortDesc,
                   SortBy = sortBy,
                   TotalRecordCount = totalRecords
               };
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            var model = new WorkflowModel() { IsActivated = true };
            BindInterfaceConfig();
            return View(model);
        }

        [HttpPost]
        public JsonResult Create(WorkflowModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    try
                    {
                        var workflowTypesTemp = Json2.ParseAs<List<WorkflowType>>(entity.WorkflowTypeJson);
                        foreach (var workflowType in workflowTypesTemp)
                        {
                            if (workflowType.Id == Guid.Empty)
                            {
                                workflowType.Id = Guid.NewGuid();
                            }
                        }
                        entity.WorkflowTypeJson = workflowTypesTemp.Stringify();
                    }
                    catch
                    {
                        entity.WorkflowTypeJson = string.Empty;
                    }

                    if (model.HasCreatePacket)
                    {
                        var names = model.WorkflowName.Split(';').Distinct();
                        var list = new List<Workflow>();
                        foreach (var name in names)
                        {
                            var item = entity.Clone();
                            list.Add(item);
                        }
                        _workflowService.Create(list);
                    }
                    else
                    {
                        _workflowService.Create(entity);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Created.Success"));
                    return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Created.Success") });
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    return Json(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Created.Error"));
                    return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Created.Error") });
                }
            }

            GetModelError();
            return null;
        }

        public ActionResult Edit(int id)
        {
            var model = _workflowService.Get(id);
            if (model == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.NotFound"));
                ErrorNotification(_resourceService.GetResource("Admin.Workflow.NotFound"));
                return RedirectToAction("Index");
            }
            ViewBag.WorkflowId = id;
            BindInterfaceConfig();
            return View(model.ToModel());
        }

        [HttpPost]
        public JsonResult Edit(WorkflowModel model)
        {
            ViewBag.WorkflowId = model.WorkflowId;
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _workflowService.Get(model.WorkflowId);
                    if (entity == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.NotFound"));
                        return Json(new { error = _resourceService.GetResource("Admin.Workflow.NotFound") });
                    }
                    var oldJson = entity.Json;
                    entity = model.ToEntity(entity);
                    try
                    {
                        var workflowTypesTemp = Json2.ParseAs<List<WorkflowType>>(entity.WorkflowTypeJson);
                        foreach (var workflowType in workflowTypesTemp)
                        {
                            if (workflowType.Id == Guid.Empty)
                            {
                                workflowType.Id = Guid.NewGuid();
                            }
                        }
                        entity.WorkflowTypeJson = workflowTypesTemp.Stringify();
                    }
                    catch
                    {
                        entity.WorkflowTypeJson = string.Empty;
                    }

                    entity.Json = oldJson;
                    _workflowService.Update(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Updated.Success"));
                    return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Updated.Success") });
                }
                catch (EgovException ex)
                {
                    return Json(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Updated.Error"));
                    return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Updated.Error") });
                }
            }

            GetModelError();
            return null;
        }

        public FileResult ExportJsonToFile(int id)
        {
            var workflow = _workflowService.Get(id);
            if (workflow == null)
            {
                throw new Exception("Workflow is not eixst.");
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(workflow.Json);
            writer.Flush();
            stream.Position = 0;
            var name = string.Format("{0}.json", workflow.WorkflowName.StripVietnameseChars());
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        public JsonResult UpdateJson(int id, HttpPostedFileBase files)
        {
            var workflow = _workflowService.Get(id);
            if (workflow == null)
            {
                throw new Exception("Workflow is not eixst.");
            }

            if (files == null || files.ContentLength == 0)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFile"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFile") });
            }
            var extension = System.IO.Path.GetExtension(files.FileName);
            var extSupport = new[] { @".json", @".txt" };
            if (!extSupport.Contains(extension.ToLower()))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Extension"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Extension") });
            }

            var reader = new StreamReader(files.InputStream);
            var json = reader.ReadToEnd();

            if (string.IsNullOrEmpty(json))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.UpdateJson.Error"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.UpdateJson.Error") });
            }

            try
            {
                var tmp = Json2.ParseAs<Bkav.eGovCloud.Core.Workflow.Path>(json);
                workflow.Json = json;
                _workflowService.Update(workflow);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.UpdateJson.Success"));
                return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.UpdateJson.Success") });
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.UpdateJson.Error"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.UpdateJson.Error") });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = _workflowService.Get(id);
                if (model == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.NotFound"));
                    ErrorNotification(_resourceService.GetResource("Admin.Workflow.NotFound"));
                    return RedirectToAction("Index");
                }

                var hasUseWorkflow = _workflowService.HasUseWorkflow(id);
                if (hasUseWorkflow)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.HasUseWorkflow"));
                    ErrorNotification(_resourceService.GetResource("Admin.Workflow.HasUseWorkflow"));
                }
                else
                {
                    _workflowService.Delete(model);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.Deleted.Success"));
                    SuccessNotification(_resourceService.GetResource("Admin.Workflow.Deleted.Success"));
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.Deleted.Error"));
                ErrorNotification(_resourceService.GetResource("Admin.Workflow.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int workflowId, bool status)
        {
            var model = _workflowService.Get(workflowId);
            if (model == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.NotFound"));
                return Json(new { error = _resourceService.GetResource("Admin.Workflow.NotFound") });
            }

            try
            {
                model.IsActivated = status;
                _workflowService.Update(model);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.ChangeStatus.Success"));
                return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.ChangeStatus.Success") });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.ChangeStatus.Error"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.ChangeStatus.Error") });
            }
        }

        public ActionResult Workflow(int id, int nodeId)
        {
            var workflow = _workflowService.Get(id);
            if (workflow == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.NotFound"));
                ErrorNotification(_resourceService.GetResource("Admin.Workflow.NotFound"));
                return RedirectToAction("Index");
            }
            if (string.IsNullOrEmpty(workflow.Json))
            {
                return View();
            }
            var pathOutput = workflow.JsonInObject;

            var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
            if (nodeOutput == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId);
                return RedirectToAction("Index");
            }

            ViewBag.GetTimeType = TimeType();
            ViewBag.AllPositions = GetAllJobTitless();
            ViewBag.ViewOption = GetViewOption(nodeOutput.ViewOption);
            ViewBag.SelectedNodes = nodeOutput.Address.StringifyJs();
            ViewBag.AllNodesInPath = pathOutput.Nodes.Select(u => new
                                                                {
                                                                    u.Id,
                                                                    u.NodeName,
                                                                }).StringifyJs();
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.AllDepartments = GetAllDepartment();
            ViewBag.MaxDepartmentLevel = _departmentService.GetMaxLevel();
            ViewBag.PathId = id;
            ViewBag.AllDeptUserPosition = GetAllDeptUserPosition();

            if (nodeOutput.TimeInNode == 0)
            {
                // mặc định là 1 ngày
                nodeOutput.TimeInNode = 24;
            }

            if (nodeOutput.TimeType == 0)
            {
                nodeOutput.TimeInNode = nodeOutput.TimeInNode / 24;
            }
            return View(nodeOutput.ToModel());
        }

        [HttpPost]
        public JsonResult Workflow(NodeModel model)
        {
            // TODO: (CuongNT-?-CuongNT-030713) Xem lại việc sử dụng thuộc tính PathId
            if (model.PathId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Node.PathIdError"));
                LogException(new ArgumentException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Node.PathIdError")));
                return Json(new { error = true });
            }

            var workflow = _workflowService.Get(model.PathId);
            if (workflow == null)
            {
                return Json(new { success = true });
            }
            var pathOutput = workflow.JsonInObject;
            var nodeOutput = pathOutput.Nodes.SingleOrDefault(t => t.Id == model.Id);
            if (nodeOutput == null)
            {
                return Json(new { success = true });
            }
            var oldNodeName = nodeOutput.NodeName;
            model.ViewOption = (string.IsNullOrEmpty(model.DocTypeView)
                ? 0
                : model.DocTypeView == "DocTypeIgnore"
                    ? 4096
                    : 8192)
                               + (string.IsNullOrEmpty(model.PageNumberView)
                                   ? 0
                                   : model.PageNumberView == "PageNumberIgnore" ? 32 : 2048) +
                               (string.IsNullOrEmpty(model.DocFieldView)
                                   ? 0
                                   : model.DocFieldView == "DocFieldIgnore" ? 1 : 64) +
                               (string.IsNullOrEmpty(model.KeywordView)
                                   ? 0
                                   : model.KeywordView == "KeywordIgnore" ? 2 : 128) +
                               (string.IsNullOrEmpty(model.GroupView)
                                   ? 0
                                   : model.GroupView == "GroupIgnore" ? 4 : 256) +
                               (string.IsNullOrEmpty(model.SendTypeView)
                                   ? 0
                                   : model.SendTypeView == "SenTypeIgnore" ? 8 : 512) +
                               (string.IsNullOrEmpty(model.DestinationView)
                                   ? 0
                                   : model.DestinationView == "DestinationIgnore" ? 16 : 1024);

            model.TimeInNode = model.TimeType == 0 ? model.TimeInNode * 24 : model.TimeInNode;
            var newNode = model.ToEntity(nodeOutput);
            pathOutput.Nodes.Remove(nodeOutput);
            pathOutput.Id = model.PathId;
            pathOutput.IsActivated = workflow.IsActivated;
            pathOutput.Name = workflow.WorkflowName;
            var listAddress = Json2.ParseAs<List<Core.Workflow.Address>>(model.NodeAddress);
            newNode.Address = listAddress;
            pathOutput.Nodes.Add(newNode);
            pathOutput.Nodes = pathOutput.Nodes.OrderBy(t => t.Id).ToList();
            if (oldNodeName != model.NodeName)
            {
                //Cập nhật lại tất cả Address của các Node
                foreach (var node in pathOutput.Nodes)
                {
                    var address = node.Address.SingleOrDefault(t => t.NodeFrom == model.Id);
                    if (address != null)
                    {
                        address.NodeNameFrom = model.NodeName;
                    }
                }
            }
            workflow.Json = pathOutput.Stringify();
            try
            {
                _workflowService.Update(workflow);
            }
            catch (EgovException ex)
            {
                return Json(new { error = true, message = ex.Message });
            }

            return Json(new { success = true });
        }

        public JsonResult GetTemplateCategoryBusiness(string name)
        {
            var template = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                template = FileManager.Default.Exist(name, Server.MapPath(TEMPLALTE_PATH))
                         ? FileManager.Default.ReadString(name, Server.MapPath(TEMPLALTE_PATH))
                         : string.Empty;
            }

            return Json(template, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfigTemplateNode(int workflowId, int nodeId)
        {
            var workflow = _workflowService.Get(workflowId);
            if (workflow == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + workflowId);
                return RedirectToAction("Index", "Error");
            }
            if (string.IsNullOrEmpty(workflow.Json))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotConfig"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotConfig"));
                return RedirectToAction("Index", "Error");
            }
            var pathOutput = workflow.JsonInObject;
            var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
            if (nodeOutput == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId);
                return RedirectToAction("Index", "Error");
            }

            ViewBag.WorkflowId = workflowId;
            ViewBag.NodeId = nodeId;
            BindInterfaceConfig(nodeOutput.TemplateId);

            return View();
        }

        [HttpPost]
        public JsonResult ConfigTemplateNode(int workflowId, int nodeId, int? templateId = null)
        {
            var workflow = _workflowService.Get(workflowId);
            if (workflow == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + workflowId });
            }

            if (string.IsNullOrEmpty(workflow.Json))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotSetting"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotSetting") });
            }

            var pathOutput = workflow.JsonInObject;
            var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
            if (nodeOutput == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId });
            }

            nodeOutput.TemplateId = templateId;
            workflow.Json = pathOutput.Stringify();
            _workflowService.Update(workflow);
            return Json(new { success = true });
        }

        public ActionResult ConfigWorkflow(int id)
        {
            var workflow = _workflowService.Get(id);
            ViewBag.Id = id;
            ViewBag.Json = workflow.Json;

            ViewBag.AllWorkflow = _workflowService.Gets(isIncludeDocType: false, isActivated: true);

            return View();
        }

        public JsonResult GetWorkflow(int id)
        {
            var workflow = _workflowService.Get(id);
            return workflow == null
                       ? Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id }, JsonRequestBehavior.AllowGet)
                       : Json(new { workflow = workflow.Json }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveWorkflow(int id, string json)
        {

            var workflow = _workflowService.Get(id);
            if (workflow == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id);
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
            }
            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    var data = Json2.ParseAs<Bkav.eGovCloud.Core.Workflow.Path>(json);
                }
                catch (Exception)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotValid"));
                    return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotValid") });
                }
            }
            workflow.Json = json;
            workflow.LastModifiedByUserId = User.GetUserId();
            workflow.LastModifiedOnDate = DateTime.Now;
            _workflowService.Update(workflow);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult CopyWorkflow(int id)
        {
            var workflow = _workflowService.Get(id);
            if (workflow == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
            }

            var copy = workflow.Clone();
            copy.WorkflowId = 0;
            copy.WorkflowName = string.Format("Copy-{0}", copy.WorkflowName);
            _workflowService.Create(copy);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Created.Success"));
            return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.Created.Success") });
        }

        /// <summary>
        /// Đồng bộ ID của các quy trình được import sang
        /// </summary>
        /// <returns></returns>
        public JsonResult SyncWorkflowId()
        {
            try
            {
                _workflowService.UpdateWorkflowId();
                return Json(new { success = "Thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "Lỗi!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FixSyncError()
        {
            try
            {
                _workflowService.FixSyncError();
                return Json(new { success = "Thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "Lỗi!" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region doctype

        public ActionResult FindDoctype(int workflowId, string searchName, int? categoryBusinessId = null, int? docfieldId = null)
        {
            var docTypeIds = _docTypeService.Raw
                        .Join(_docTypeService.RawDocfieldDoctypeWorkflow,
                            p => p.DocTypeId, x => x.DocTypeId,
                            (p, x) => new { DocTypeId = p.DocTypeId, WorkflowId = x.WorkflowId })
                        .Where(p => p.WorkflowId == workflowId)
                        .Select(p => p.DocTypeId).ToList();

            var allDoctypes = _docTypeService.GetsAs(p => new
            {
                value = p.DocTypeId,
                label = p.DocTypeName,
                categoryBusinessId = p.CategoryBusinessId,
                docfieldId = p.DocFieldId
            },
             p => !docTypeIds.Contains(p.DocTypeId)
                 && (!categoryBusinessId.HasValue || categoryBusinessId.Value == 0 || (categoryBusinessId.HasValue && p.CategoryBusinessId == categoryBusinessId.Value))
                 && (!docfieldId.HasValue || docfieldId.Value == 0 || (docfieldId.HasValue && p.DocFieldId == docfieldId.Value))
                 && (string.IsNullOrEmpty(searchName) || (!string.IsNullOrEmpty(searchName) && p.DocTypeName.Contains(searchName))));

            //  ViewBag.Search = search;
            ViewBag.AllDocTypes = allDoctypes.Stringify();
            ViewBag.WorkflowId = workflowId;
            return PartialView("_PartialDocType");
        }

        public ActionResult AddDocTypeWorkflow(int id)
        {
            var docTypeIds = _docTypeService.Raw
                       .Join(_docTypeService.RawDocfieldDoctypeWorkflow,
                           p => p.DocTypeId, x => x.DocTypeId,
                           (p, x) => new { DocTypeId = p.DocTypeId, WorkflowId = x.WorkflowId })
                       .Where(p => p.WorkflowId == id)
                       .Select(p => p.DocTypeId).ToList();

            var notExist = _docTypeService.GetsAs(p => new
            {
                value = p.DocTypeId,
                label = p.DocTypeName,
                categoryBusinessId = p.CategoryBusinessId,
                docfieldId = p.DocFieldId.HasValue ? p.DocFieldId.Value : 0
            }, p => p.IsActivated && !docTypeIds.Contains(p.DocTypeId));

            ViewBag.WorkflowId = id;
            ViewBag.AllDocTypes = notExist.Stringify();
            BindCategoryBusiness();
            BindDocField();
            return PartialView("AddDocTypeWorkflow");
        }

        [HttpPost]
        public JsonResult UpdateDocTypeWorkflow(int id, List<Guid> docTypeIds)
        {
            if (docTypeIds == null || !docTypeIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success") });
            }

            var exist = _workflowService.Exist(p => p.WorkflowId == id);
            if (!exist)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.NotFoundWorkflow"));
                return Json(new { success = _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.NotFoundWorkflow") });
            }

            try
            {
                var existMap = _workflowService.GetDocFieldDocTypeWorkflows(p => p.DocTypeId.HasValue && docTypeIds.Contains(p.DocTypeId.Value));
                var existDocTypeIds = new List<Guid>();
                if (existMap != null && existMap.Any())
                {
                    foreach (var item in existMap)
                    {
                        item.IsActivated = false;
                    }
                    _workflowService.UpdateDocFieldDocTypeWorkflow(existMap);

                    existDocTypeIds = existMap.Where(p => p.WorkflowId == id).Select(p => p.DocTypeId.Value).ToList();
                }

                var notExist = docTypeIds.Where(p => !existDocTypeIds.Contains(p));
                if (notExist != null && notExist.Any())
                {
                    var obj = new List<DocfieldDoctypeWorkflow>();
                    foreach (var docTypeId in notExist)
                    {
                        obj.Add(new DocfieldDoctypeWorkflow
                        {
                            WorkflowId = id,
                            IsActivated = true,
                            DocTypeId = docTypeId
                        });
                    }
                    _workflowService.CreateDocFieldDocTypeWorkflow(obj);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success") });
            }
            catch (EgovException ex)
            {
                return Json(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error"));
                return Json(new { error = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error") });
            }
        }

        [HttpPost]
        public JsonResult ChangeDocTypeWorkflowStatus(Guid docTypeId, int workflowId, bool status)
        {
            try
            {
                _docTypeService.ChangeActivatedWorkflows(docTypeId, workflowId, status);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Success") });
            }
            catch (EgovException ex)
            {
                return Json(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Error"));
                return Json(new { error = _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Error") });
            }
        }

        [HttpPost]
        public ActionResult DeleteDocTypeWorkFlow(int id, Guid docTypeId)
        {
            try
            {
                _docTypeService.DeleteWorkflows(docTypeId, new List<int> { id });
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.DocType.Deleted"));
                SuccessNotification(_resourceService.GetResource("Common.DocType.Deleted"));
            }
            catch (EgovException ex)
            {
                ErrorNotification(ex.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return RedirectToAction("DocTypeWorkflow", new { id = id });
        }

        public ActionResult DocTypeWorkflow(int id)
        {
            var exist = _workflowService.Exist(p => p.WorkflowId == id);
            if (!exist)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.NotFound"));
                ErrorNotification(_resourceService.GetResource("Admin.Workflow.NotFound"));
                return RedirectToAction("workflow");
            }

            var docTypeWorkflows = _docTypeService.Raw
                .Join(_docTypeService.RawDocfieldDoctypeWorkflow,
                    p => p.DocTypeId, x => x.DocTypeId,
                    (p, x) => new { DocType = p, DocfieldDoctypeWorkflow = x })
                .Where(p => p.DocfieldDoctypeWorkflow.WorkflowId == id)
                .Select(p => new DocTypeModel
                {
                    DocTypeId = p.DocType.DocTypeId,
                    DocTypeName = p.DocType.DocTypeName,
                    IsActivated = p.DocfieldDoctypeWorkflow.IsActivated
                }).ToList();

            ViewBag.WorkflowId = id;
            ViewBag.DocTypeWorkflows = docTypeWorkflows;

            return PartialView("DocTypeWorkflow");
        }

        #endregion

        #region docfield

        public ActionResult FindDocField(int workflowId, string searchName, int? categoryBusinessId = null)
        {
            var docFieldIds = _docFieldService.Raw
                 .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                     p => p.DocFieldId, x => x.DocFieldId,
                     (p, x) => new { DocField = p, DocfieldDoctypeWorkflow = x })
                 .Where(p => p.DocfieldDoctypeWorkflow.WorkflowId == workflowId)
                 .Select(p => p.DocField.DocFieldId).ToList();

            var allDocFields = _docFieldService.GetsAs(p => new
            {
                value = p.DocFieldId,
                label = p.DocFieldName,
                state = p.IsActivated,
                categoryBusinessId = p.CategoryBusinessId
            },
             p => !docFieldIds.Contains(p.DocFieldId)
                 && ((!categoryBusinessId.HasValue || categoryBusinessId == 0) || (categoryBusinessId.HasValue && p.CategoryBusinessId == categoryBusinessId.Value))
                 && (string.IsNullOrEmpty(searchName) || (!string.IsNullOrEmpty(searchName) && p.DocFieldName.Contains(searchName))));

            ViewBag.AllDocFields = allDocFields.Stringify();
            ViewBag.WorkflowId = workflowId;
            BindCategoryBusiness(categoryBusinessId);
            return PartialView("_PartialDocField");
        }

        public ActionResult AddDocFieldWorkflow(int id)
        {
            var docFieldIds = _docFieldService.Raw
                .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                    p => p.DocFieldId, x => x.DocFieldId,
                    (p, x) => new { DocField = p, DocfieldDoctypeWorkflow = x })
                .Where(p => p.DocfieldDoctypeWorkflow.WorkflowId == id)
                .Select(p => p.DocField.DocFieldId).ToList();

            var notExist = _docFieldService.GetsAs(p => new
            {
                value = p.DocFieldId,
                label = p.DocFieldName,
                categoryBusinessId = p.CategoryBusinessId
            }, p => p.IsActivated && !docFieldIds.Contains(p.DocFieldId));

            ViewBag.WorkflowId = id;
            ViewBag.AllDocFields = notExist.Stringify();
            BindCategoryBusiness();

            return PartialView("AddDocFieldWorkflow");
        }

        [HttpPost]
        public JsonResult UpdateDocFieldWorkflow(int id, List<int> docFieldIds)
        {
            if (docFieldIds == null || !docFieldIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success") });
            }

            var exist = _workflowService.Exist(p => p.WorkflowId == id);
            if (!exist)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.NotFoundWorkflow"));
                return Json(new { success = _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.NotFoundWorkflow") });
            }

            try
            {
                var existMap = _workflowService.GetDocFieldDocTypeWorkflows(p => p.DocFieldId.HasValue && docFieldIds.Contains(p.DocFieldId.Value));
                var existDocTypeIds = new List<int>();
                if (existMap != null && existMap.Any())
                {
                    foreach (var item in existMap)
                    {
                        item.IsActivated = false;
                    }
                    _workflowService.UpdateDocFieldDocTypeWorkflow(existMap);

                    existDocTypeIds = existMap.Where(p => p.WorkflowId == id).Select(p => p.DocFieldId.Value).ToList();
                }

                var notExist = docFieldIds.Where(p => !existDocTypeIds.Contains(p));
                if (notExist != null && notExist.Any())
                {
                    var obj = new List<DocfieldDoctypeWorkflow>();
                    int lastDocFieldid = 0;
                    foreach (var docFieldId in notExist)
                    {
                        obj.Add(new DocfieldDoctypeWorkflow
                        {
                            WorkflowId = id,
                            IsActivated = true,
                            DocFieldId = docFieldId
                        });
                        lastDocFieldid = docFieldId;
                    }
                    _workflowService.CreateDocFieldDocTypeWorkflow(obj);
                    _docFieldService.ChangeActivatedWorkflows(lastDocFieldid, id, true);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.Workflow.UpdateDocTypeWorkflow.Success") });
            }
            catch (EgovException ex)
            {
                return Json(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error"));
                return Json(new { error = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error") });
            }
        }

        [HttpPost]
        public JsonResult ChangeDocFieldWorkflowStatus(int docFieldId, int workflowId, bool status)
        {
            try
            {
                _docFieldService.ChangeActivatedWorkflows(docFieldId, workflowId, status);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Workflow.ChangeDocFieldWorkflowStatus.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.Workflow.ChangeDocFieldWorkflowStatus.Success") });
            }
            catch (EgovException ex)
            {
                return Json(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = _resourceService.GetResource("Customer.Workflow.ChangeDocFieldWorkflowStatus.Error") });
            }
        }

        [HttpPost]
        public ActionResult DeleteDocFieldWorkFlow(int id, int docFieldId)
        {
            try
            {
                _docFieldService.DeleteWorkflows(docFieldId, new List<int> { id });
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.DocType.Deleted"));
                SuccessNotification(_resourceService.GetResource("Common.DocType.Deleted"));
            }
            catch (EgovException ex)
            {
                ErrorNotification(ex.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return RedirectToAction("DocFieldWorkflow", new { id = id });
        }

        public ActionResult DocFieldWorkflow(int id)
        {
            var exist = _workflowService.Exist(p => p.WorkflowId == id);
            if (!exist)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Workflow.NotFound"));
                ErrorNotification(_resourceService.GetResource("Admin.Workflow.NotFound"));
                return RedirectToAction("workflow");
            }

            var docFieldWorkflows = _docFieldService.Raw
                .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                    p => p.DocFieldId, x => x.DocFieldId,
                    (p, x) => new { DocField = p, DocfieldDoctypeWorkflow = x })
                .Where(p => p.DocfieldDoctypeWorkflow.WorkflowId == id)
                .Select(p => new DocFieldModel
                {
                    DocFieldId = p.DocField.DocFieldId,
                    DocFieldName = p.DocField.DocFieldName,
                    IsActivated = p.DocfieldDoctypeWorkflow.IsActivated
                }).ToList();

            ViewBag.WorkflowId = id;
            ViewBag.DocFieldWorkflows = docFieldWorkflows;

            return PartialView("DocFieldWorkflow");
        }

        #endregion

        #region Private Method

        private void BindCategoryBusiness(int? categoryBusinessId = null)
        {
            var listCategoryBusiness = _resourceService.EnumToSelectList<CategoryBusinessTypes>(categoryBusinessId);
            ViewBag.AllCategoryBusiness = listCategoryBusiness;
        }

        private void BindDocField()
        {
            var allDocFields = _docFieldService.GetsAs(p => new
            {
                value = p.DocFieldId,
                label = p.DocFieldName,
                categoryBusinessId = p.CategoryBusinessId
            }, p => p.IsActivated);

            ViewBag.AllDocFields = allDocFields.Stringify();
        }

        private void BindDocTypes()
        {
            var allDocTypes = _docTypeService.GetsAs(p => new
            {
                value = p.DocTypeId,
                label = p.DocTypeName,
                docfieldId = p.DocFieldId
            }, p => p.IsActivated);

            ViewBag.AllDocTypes = allDocTypes.Stringify();
        }

        private void BindFilter()
        {
            BindDocField();
            BindDocTypes();
        }

        private IEnumerable<SelectListItem> TimeType()
        {
            return new List<SelectListItem>{
                     new SelectListItem{Value="0",Text="Ngày"},
                     new SelectListItem{Value="1",Text="Giờ"}
                };
        }

        private string GetAllJobTitless()
        {
            return
                _positionService.GetCacheAllPosition().Select(
                    u => new { value = u.PositionId, label = u.PositionName }).StringifyJs();
        }

        private static List<int> GetViewOption(int giatri)
        {
            var result = new List<int>();
            for (int i = 13; i >= 0; i--)
            {
                if (giatri >= Math.Pow(2, i))
                {
                    giatri = giatri - (int)Math.Pow(2, i);
                    result.Add(i);
                }
            }
            return result;
        }

        private string GetAllUsers()
        {
            return _userService.GetAllCached().Select(u =>
                new
                {
                    value =
                        u.UserId,
                    label =
                        u.Username + " - " +
                        u.FullName,
                    fullname =
                        u.FullName,
                    username =
                        u.Username,
                    objecttype =
                        "user",
                    storevalue =
                        "$userid:" +
                        u.UserId.ToString(CultureInfo.InvariantCulture)
                }).OrderBy(
                    u => u.username)
                .StringifyJs();
        }

        private string GetAllDepartment()
        {
            var alldepartments =
                _departmentService.GetCacheAllDepartments(true);
            return alldepartments.Select(u => new
            {
                value = u.DepartmentId,
                parentid = u.ParentId.HasValue ? u.ParentId : 0,
                data = u.DepartmentName,
                metadata = new { id = u.DepartmentId },
                idext = u.DepartmentIdExt,
                state = "leaf",
                attr = new { id = u.DepartmentId, rel = "dept" },
                pathname = u.DepartmentPath
            }).StringifyJs();
        }

        private string GetAllDeptUserPosition()
        {
            return _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
                t =>
                new
                {
                    departmentid = t.DepartmentId,
                    userid = t.UserId,
                    positionid = t.PositionId,
                    idext = t.DepartmentIdExt
                }).StringifyJs();
        }

        private void BindInterfaceConfig(int? interfaceCfgId = null)
        {
            var allCfg = _interfaceConfigService.GetsAs(p =>
                new
                {
                    p.InterfaceConfigName,
                    p.InterfaceConfigId
                }).Select(p => new SelectListItem
                {
                    Text = p.InterfaceConfigName,
                    Value = p.InterfaceConfigId.ToString(),
                    Selected = interfaceCfgId.HasValue && interfaceCfgId.Value == p.InterfaceConfigId
                });

            ViewBag.AllInterfaceConfig = allCfg;
        }

        #endregion Private Method
    }
}
