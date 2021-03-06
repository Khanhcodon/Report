using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
using Bkav.eGovOnline.Business.Customer;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Utils;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Bkav.eGovCloud.Entities.Enum;
using Newtonsoft.Json.Linq;
using Bkav.eGovCloud.Entities.Admin.TimerJobSchedules;
using Bkav.eGovCloud.Business.Tasks;
using FluentScheduler;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Core.Caching;
using System.Data;
using System.IO;
using System.Text;
using Bkav.eGovCloud.Models;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public partial class DocTypeController : CustomController
    {
        private const string DEFAULT_SORT_BY = "DocTypeName";
        private readonly CategoryBll _categoryService;
        private readonly CodeBll _codeService;
        private readonly DepartmentBll _departmentService;
        private readonly DocFieldBll _docFieldService;
        private readonly DocTypeBll _docTypeService;
        private readonly ReportModeBll _reportModeService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly PositionBll _positionService;
        private readonly ResourceBll _resourceService;
        private readonly StoreBll _storeService;
        private readonly UserBll _userService;
        private readonly WorkflowBll _workflowService;
        private readonly FormBll _formService;
        private readonly FormGroupBll _formgroupService;
        private readonly PaperBll _paperService;
        private readonly OfficeBll _officeService;//egovOnline
        private readonly DocTypeFormBll _doctypeformService;
        private readonly Bkav.eGovOnline.Business.Customer.LawBll _lawService;//eGovOnline
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly DoctypePaperBll _doctypePaperService;//eGovOnline

        private readonly FeeBll _feeService;
        private readonly DoctypeFeeBll _doctypeFeeService;
        private readonly DoctypeTemplateBll _doctypeTemplateService;
        private readonly OnlineTemplateBll _onlineTemplateService;

        // 20191128 VuHQ REQ-5
        private readonly CatalogBll _catalogService;

        // 20200210 VuHQ Phase 2 - REQ 0
        private readonly TemplateKeyBll _templateKeyService;
        private readonly TemplateKeyCategoryBll _templateKeyCategoryService;

        private readonly StatisticBll _statisticService;

        private readonly ReportKeyBll _reportKeyService;

        private const string TEMPLALTE_PATH = "~/Content/TemplateUICategoryBusiness";

        private readonly IncreaseBll _increaseService;

        private const int CATEGORY_BUSINESS_EXPLICIT = 8;
        private const int CATEGORY_BUSINESS_REPORT = 16;
        private const int CATEGORY_BUSINESS_GENERAL = 32; // Báo cáo tổng hợp

        private readonly AdminGeneralSettings _adminSetting;
        private readonly eGovCloud.Controllers.DocumentHelper _documentHelper;
        private readonly DocTypeTimeJobBll _docTypeTimeJobService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly ActionLevelBll _actionLevelService;
        private readonly SurveyCatalogBll _surveyCatalogService;

        public DocTypeController(FeeBll feeService,
                                 DoctypeFeeBll doctypeFeeService,
                                 DoctypePaperBll doctypePaperService,
                                 DocTypeBll docTypeService,
                                 CodeBll codeService, UserBll userService,
                                 DepartmentBll departmentService,
                                 CategoryBll categoryService,
                                 StoreBll storeService,
                                 PositionBll positionService,
                                 DocFieldBll docFieldService,
                                 ResourceBll resourceService,
                                 WorkflowBll workflowService,
                                 AdminGeneralSettings generalSettings,
                                 FormBll formService,
                                 FormGroupBll formgroupService,
                                 DocTypeFormBll doctypeformService,
                                 LawBll lawService,
                                 ReportModeBll reportModeService,
                                 OfficeBll officeService,
                                 PaperBll paperService,
                                 DoctypeTemplateBll doctypeTemplateService,
                                OnlineTemplateBll onlineTemplateService,
                                IncreaseBll increaseService,
                                DocTypeFormBll doctypeFormService,
                                CatalogBll catalogService,
                                TemplateKeyBll templateKeyService,
                                ReportKeyBll reportKeyService,
                                 TemplateKeyCategoryBll templateKeyCategoryService,
                                 AdminGeneralSettings adminSetting,
                                 eGovCloud.Controllers.DocumentHelper documentHelper,
                                 DocTypeTimeJobBll docTypeTimeJobService,
                                 InCatalogBll inCatalogService,
                                 InCatalogValueBll inCatalogValueService,
                                 ActionLevelBll actionLevelService,
                                 SurveyCatalogBll surveyCatalogService
            )
            : base()
        {
            _doctypeFeeService = doctypeFeeService;
            _feeService = feeService;
            _doctypePaperService = doctypePaperService;
            _paperService = paperService;
            _officeService = officeService;
            _lawService = lawService;
            _reportModeService = reportModeService;
            _docTypeService = docTypeService;
            _categoryService = categoryService;
            _docFieldService = docFieldService;
            _generalSettings = generalSettings;
            _storeService = storeService;
            _resourceService = resourceService;
            _workflowService = workflowService;
            _codeService = codeService;
            _positionService = positionService;
            _userService = userService;
            _departmentService = departmentService;
            _formService = formService;
            _formgroupService = formgroupService;
            _doctypeformService = doctypeformService;
            _doctypeTemplateService = doctypeTemplateService;
            _onlineTemplateService = onlineTemplateService;
            _increaseService = increaseService;
            _doctypeFormService = doctypeFormService;
            // 20191128 VuHQ REQ-5
            _catalogService = catalogService;

            // 20200210 VuHQ Phase 2 - REQ 0
            _templateKeyService = templateKeyService;
            _statisticService = DependencyResolver.Current.GetService<StatisticBll>();
            _reportKeyService = reportKeyService;
            _templateKeyCategoryService = templateKeyCategoryService;
            _adminSetting = adminSetting;
            _documentHelper = documentHelper;
            _docTypeTimeJobService = docTypeTimeJobService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _actionLevelService = actionLevelService;
            _surveyCatalogService = surveyCatalogService;
        }

        #region "Module Admin"

        public ActionResult Index(int? categoryBusinessId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }
            if (categoryBusinessId == 32)
            {
                ViewBag.Catalog = _surveyCatalogService.Gets("").Select(sc => new { value = sc.CatalogId, text = sc.CatalogName });
                return Survey();
            }
            var search = new DocTypeSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            HttpCookie httpCookie = Request.Cookies[CookieName.SearchDocType];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<DocTypeSearchModel>(data[$"Search_{categoryBusinessId}"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data[$"SortAndPaging_{categoryBusinessId}"].ToString());
                }
                catch { }
            }

            var model = GetDocTypeModels(search, sortAndPage.SortBy, sortAndPage.IsSortDescending,
                                         sortAndPage.CurrentPage, sortAndPage.PageSize, categoryBusinessId);

            ViewBag.AllDocFields = _docFieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId })
                                        .OrderBy(d => d.DocFieldName)
                                        .StringifyJs();

            ViewBag.AllReportModel = _reportModeService.GetsAs(d => new { d.ReportModeId, d.Name })
                                      .OrderBy(d => d.Name)
                                      .StringifyJs();

            ViewBag.Search = search;
            ViewBag.CategoryBusinessId = BindCategoryBusiness(4);

            ViewBag.CategoryBusinessIdValue = categoryBusinessId;
            if (GetActionLevel() != null) {
                ViewBag.ListActionLevel = GetActionLevel();
            }
           

            return View(model);
        }

        public ActionResult IndexAic(int? docfieldId,int? reportmodeId, int? actionLevel = 1, int levelId = 1)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //	CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermission"));
            //	ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermission"));
            //	return RedirectToAction("Index", "Welcome");
            //}

            var search = new DocTypeSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            HttpCookie httpCookie = Request.Cookies[CookieName.SearchDocType];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<DocTypeSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { }
            }

            var docfields = _docFieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId, d.Order }).OrderBy(df => df.Order);
            docfieldId = docfieldId ?? docfields.First().DocFieldId;

            search.DocFieldId = docfieldId;
            search.ActionLevel = null;

            var doctypes = GetDocTypeModels(search, sortAndPage.SortBy, sortAndPage.IsSortDescending,
                                         sortAndPage.CurrentPage, sortAndPage.PageSize);

            var reportmodes = _reportModeService.GetsAs(d => new { d.ReportModeId, d.Name });
            reportmodeId = reportmodeId ?? reportmodes.First().ReportModeId;

            ViewBag.AllDocFields = docfields.Select(df => new SelectListItem()
            {
                Selected = docfieldId.HasValue ? df.DocFieldId == docfieldId.Value : false,
                Text = df.DocFieldName,
                Value = df.DocFieldId.ToString()
            });

            ViewBag.AllReportModel = reportmodes.Select(df => new SelectListItem()
            {
                Selected = reportmodeId.HasValue ? df.ReportModeId == reportmodeId.Value : false,
                Text = df.Name,
                Value = df.ReportModeId.ToString()
            });

            var reportModels = _reportModeService.GetsAs(d => new { d.ReportModeId, d.Name, d.ReportMode })
                                   .OrderBy(d => d.Name);



            ViewBag.Total = _docTypeService.CountHSMC();

            var counts = doctypes.GroupBy(dt => dt.ActionLevel).Select(g => new { actionLevel = g.Key, count = g.Count() }).ToList();
            ViewBag.LevelCount = counts.Stringify();

            ViewBag.Search = search;
            ViewBag.ActionLevel = actionLevel;
            ViewBag.ListActionLevel = GetActionLevel();

            var model = doctypes.Where(dt => dt.ActionLevel == actionLevel && dt.LevelId == levelId);
            return View("IndexAic", model);
        }
     
        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            LoadDropDownList();
            ReBindDataWhenError(4);
            return View(new DocTypeModel { IsActivated = true, IsAllowOnline = true });
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeCreate")]
        public ActionResult Create(DocTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                DocType docTypeCreate = model.ToEntity();
                docTypeCreate.CreatedByUserId = User.GetUserId();
                docTypeCreate.CreatedOnDate = DateTime.Now;
                docTypeCreate.LastModifiedOnDate = DateTime.Now;
                docTypeCreate.DocTypePermission = 0;
                docTypeCreate.TotalRegisted = 1;//eGovOnline
                docTypeCreate.TotalViewed = 1;//eGovOnline
                docTypeCreate.Unsigned = ConverToUnsign(model.DocTypeName);//eGovOnline
                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        docTypeCreate.DocTypePermission |= per;
                    }
                }
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.DocTypeName.Split(';').Distinct();
                        var list = new List<DocType>();
                        foreach (var name in names)
                        {
                            var item = docTypeCreate.Clone();
                            item.DocTypeName = name;
                            list.Add(item);
                        }
                        _docTypeService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _docTypeService.Create(docTypeCreate);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Created"));
                    SuccessNotification(_resourceService.GetResource("Customer.DocType.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                    LoadDropDownList();
                    ReBindDataWhenError(docTypeCreate.CategoryBusinessId);
                    return View(model);
                }

                ModelState.Clear();
                LoadDropDownList();
                ReBindDataWhenError();
                ViewBag.StoreIds = model.StoreIds;
                var modelCreate = new DocTypeModel
                {
                    IsActivated = true,
                    IsAllowOnline = true,
                    CategoryId = model.CategoryId,
                    CategoryBusinessId = model.CategoryBusinessId,
                    DocFieldId = model.DocFieldId,//eGovOnline
                    OfficeId = model.OfficeId,//eGovOnline
                    LevelId = model.LevelId,//eGovOnline
                    Content = model.Content,//eGovOnline
                    ActionLevel = model.ActionLevel,//eGovOnline
                    DocTypeName = "",
                    CompendiumDefault = "",
                    DocTypePermission = docTypeCreate.ToModel().DocTypePermission,
                };
                return View(modelCreate);
            }

            LoadDropDownList();
            ReBindDataWhenError();
            return View(model);
        }

        public ActionResult GetForm(Guid doctypeId, FormSearchModel search)
        {
            var docformIds = _doctypeformService.GetsAs(f => f.FormId, f => f.DocTypeId == doctypeId);
            var allform = _formService.Gets(f =>
                (!search.FormGroupId.HasValue || (search.FormGroupId.HasValue && f.FormGroupId == search.FormGroupId))
                && (!search.FormTypeId.HasValue || (search.FormTypeId.HasValue && f.FormTypeId == search.FormTypeId))
                && (string.IsNullOrEmpty(search.SearchKey) || (!string.IsNullOrEmpty(search.SearchKey) && f.FormName.Contains(search.SearchKey)))
                && !docformIds.Contains(f.FormId)).ToListModel();
            var hasPrimary = _doctypeformService.Exist(f => f.DocTypeId == doctypeId && f.IsPrimary);
            ViewBag.AllForm = allform;
            ViewBag.Search = search;
            ViewBag.DocTypeId = doctypeId;
            ViewBag.HasPrimary = hasPrimary;
            return PartialView("_PartialForm");
        }

        public ActionResult Edit(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            DocType docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            if (docType == null)
            {
                return RedirectToAction("Index");
            }

            var checkedStoreIds = _docTypeService.GetStoreIds(docType.DocTypeId);
            var storeIdDefault = _docTypeService.GetStoreIdDefault(docType.DocTypeId);
            ReBindDataWhenError(docType.CategoryBusinessId, checkedStoreIds, storeIdDefault);
            if (!docType.IsAllowOnline.HasValue)
            {
                docType.IsAllowOnline = false;
            }

            ViewBag.DoctypeId = id;
            ViewBag.StoreIds = checkedStoreIds; // docType.DocTypeStores.Select(t => t.StoreId).ToList();

            LoadDropDownList(docType.LevelId, docType.OfficeId, docType.ActionLevel);
            var model = docType.ToModel();
            model.StoreIdDefault = storeIdDefault;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeEdit")]
        public ActionResult Edit(DocTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            LoadDropDownList(model.LevelId, model.OfficeId, model.ActionLevel);

            if (ModelState.IsValid)
            {
                DocType docType = _docTypeService.Get(model.DocTypeId);
                if (docType == null)
                    return RedirectToAction("Index");
                string oldDocTypeName = docType.DocTypeName;
                string oldDocTypeCode = docType.DocTypeCode;
                docType.LastModifiedByUserId = User.GetUserId();
                docType.LastModifiedOnDate = DateTime.Now;
                model.Unsigned = ConverToUnsign(model.DocTypeName);
                model.DocTypePermission = 0;
                model.TotalViewed = 1;
                model.TotalRegisted = 1;
                model.DocTypeLaws = docType.DocTypeLaws;
                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        model.DocTypePermission |= per;
                    }
                }
                try
                {
                    docType = model.ToEntity(docType);

                    _docTypeService.Update(docType, oldDocTypeName, oldDocTypeCode);
                    _docTypeService.UpdateStoreIdDefault(docType, model.StoreIdDefault);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Updated"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Updated"));
                return RedirectToAction("Index");
            }
            ReBindDataWhenError(model.CategoryBusinessId);
            return View(model);
        }

        public ActionResult Form(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionForm"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionForm"));
                return RedirectToAction("Index");
            }

            var forms = _doctypeformService.GetsAs(f => new
            {
                f.Form.FormId,
                f.Form.FormName,
                f.Form.Description,
                f.IsPrimary,
                f.IsActive
            }, f => f.DocTypeId == id).Select(f => new Form
            {
                FormId = f.FormId,
                FormName = f.FormName,
                Description = f.Description,
                IsPrimary = f.IsPrimary,
                IsActivated = f.IsActive ? 1 : 0
            });
            DocType docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            ViewBag.AllForm = forms.ToListModel();
            ViewBag.DocTypeId = id;
            return View();
        }

        public ActionResult FormPlus(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionForm"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionForm"));
                return RedirectToAction("Index");
            }

            var forms = _doctypeformService.GetsAs(f => new
            {
                f.Form.FormId,
                f.Form.FormName,
                f.Form.Description,
                f.IsPrimary,
                f.IsActive
            }, f => f.DocTypeId == id).Select(f => new Form
            {
                FormId = f.FormId,
                FormName = f.FormName,
                Description = f.Description,
                IsPrimary = f.IsPrimary,
                IsActivated = f.IsActive ? 1 : 0
            });
            DocType docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            ViewBag.AllForm = forms.ToListModel();
            ViewBag.DocTypeId = id;
            return View();
        }

        public ActionResult AddForm(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionAddForm"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionAddForm"));
                return RedirectToAction("Index");
            }

            var allformgroup = _formgroupService.Gets();
            var allformtype = _formService.GetTypes();
            ViewBag.DoctypeId = id;
            ViewBag.AllFormGroup = allformgroup.ToListModel();
            ViewBag.FormTypes = allformtype;
            //var formgroupfirtid = allformgroup.First().FormGroupId;
            //var formtypefirtid = allformtype.First().FormTypeId;
            var docforms = _doctypeformService.GetsAs(f => f.FormId, f => f.DocTypeId == id);
            var hasPrimary = _doctypeformService.Exist(f => f.DocTypeId == id && f.IsPrimary);
            var allform =
                _formService.GetsAs(
                    f =>
                        new FormModel
                        {
                            FormId = f.FormId,
                            FormName = f.FormName,
                            EmbryonicPath = f.EmbryonicPath,
                            Description = f.Description,
                            FormTypeId = f.FormTypeId
                        },
                    f =>
                        // f.FormGroupId == formgroupfirtid && f.FormTypeId == formtypefirtid &&
                        !docforms.Contains(f.FormId));
            //var search = new FormSearchModel
            //{
            //    FormGroupId = formgroupfirtid,
            //    FormTypeId = formtypefirtid,
            //};
            ViewBag.AllForm = allform;
            //  ViewBag.Search = search;
            ViewBag.HasPrimary = hasPrimary;
            return PartialView("AddForm");
        }
        [HttpGet]
        public JsonResult GetSurveyConfig(Guid doctypeId) {
            var list = _docTypeService.Get(doctypeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult Update(Guid doctypeId, string Link)
        {
            _docTypeService.Update(doctypeId, Link);
            return Json("Done", JsonRequestBehavior.AllowGet);
        }
        #region Thêm quy  trình

        public ActionResult FindWorkflow(Guid doctypeId, DocTypeWorkflowSearchModel search)
        {
            var docTypeWorkflowIds = _docTypeService.GetWorkFlows(p => p.WorkflowId, p => p.DocTypeId == doctypeId);
            var allWorkflows = _workflowService.GetsAs(p => new WorkflowModel
            {
                WorkflowId = p.WorkflowId,
                WorkflowName = p.WorkflowName,
                IsActivated = p.IsActivated
            },
             p => !docTypeWorkflowIds.Contains(p.WorkflowId)
                 && (!search.IsActivatated.HasValue || (search.IsActivatated.HasValue && p.IsActivated == search.IsActivatated.Value))
                 && (string.IsNullOrEmpty(search.SearchKey) || (!string.IsNullOrEmpty(search.SearchKey) && p.WorkflowName.Contains(search.SearchKey))));
            ViewBag.Search = search;
            ViewBag.AllWorkflows = allWorkflows;
            ViewBag.DoctypeId = doctypeId;
            return PartialView("_PartialWorkflow");
        }

        public ActionResult AddDocTypeWorkflow(Guid id)
        {
            var docTypeWorkflowIds = _docTypeService.GetWorkFlows(p => p.WorkflowId, p => p.DocTypeId == id);
            Expression<Func<Workflow, bool>> spec = null;
            if (docTypeWorkflowIds != null && docTypeWorkflowIds.Any())
            {
                spec = p => !docTypeWorkflowIds.Contains(p.WorkflowId);
            }
            var notExist = _workflowService.GetsAs(p => new WorkflowModel
            {
                WorkflowId = p.WorkflowId,
                WorkflowName = p.WorkflowName,
                IsActivated = p.IsActivated
            }, spec);

            ViewBag.DoctypeId = id;
            ViewBag.AllWorkflows = notExist;
            return PartialView("AddDocTypeWorkflow");
        }

        [HttpPost]
        public JsonResult UpdateDocTypeWorkflow(Guid id, List<int> workflowIds)
        {
            if (workflowIds == null || !workflowIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success") });
            }
            try
            {
                _docTypeService.UpdateWorkflows(id, workflowIds);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success") });
            }
            catch (EgovException ex)
            {
                LogException(ex);

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
                LogException(ex);
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
        public ActionResult DeleteDocTypeWorkFlow(Guid id, int workflowId)
        {
            try
            {
                _docTypeService.DeleteWorkflows(id, new List<int> { workflowId });
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.DocType.Deleted"));
                SuccessNotification(_resourceService.GetResource("Common.DocType.Deleted"));
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

            return RedirectToAction("DocTypeWorkflow", new { id = id });
        }

        public ActionResult DocTypeWorkflow(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionAddForm"));
            //    return RedirectToAction("Index");
            //}

            var docType = _docTypeService.Get(id);
            if (docType == null)
            {
                return RedirectToAction("Index");
            }

            var docTypeWorkflows = _workflowService.Raw
                      .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                          p => p.WorkflowId, x => x.WorkflowId,
                          (p, x) => new { Workflow = p, DocfieldDoctypeWorkflow = x })
                      .Where(p => p.DocfieldDoctypeWorkflow.DocTypeId == id)
                      .Select(p => new WorkflowModel
                      {
                          WorkflowId = p.Workflow.WorkflowId,
                          WorkflowName = p.Workflow.WorkflowName,
                          IsActivated = p.DocfieldDoctypeWorkflow.IsActivated
                      }).ToList();

            ViewBag.DoctypeId = id;
            ViewBag.DocTypeWorkflows = docTypeWorkflows;
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;

            return PartialView("DocTypeWorkflow");
        }

        public ActionResult DocTypeWorkflowPlus(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionAddForm"));
            //    return RedirectToAction("Index");
            //}

            var docType = _docTypeService.Get(id);
            if (docType == null)
            {
                return RedirectToAction("Index");
            }

            var docTypeWorkflows = _workflowService.Raw
                      .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
                          p => p.WorkflowId, x => x.WorkflowId,
                          (p, x) => new { Workflow = p, DocfieldDoctypeWorkflow = x })
                      .Where(p => p.DocfieldDoctypeWorkflow.DocTypeId == id)
                      .Select(p => new WorkflowModel
                      {
                          WorkflowId = p.Workflow.WorkflowId,
                          WorkflowName = p.Workflow.WorkflowName,
                          IsActivated = p.DocfieldDoctypeWorkflow.IsActivated
                      }).ToList();

            ViewBag.DoctypeId = id;
            ViewBag.DocTypeWorkflows = docTypeWorkflows;
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;

            return PartialView("DocTypeWorkflowPlus");
        }

        #endregion

        [HttpPost]
        public ActionResult Delete(Guid id, int? categoryBusinessId = null)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionDelete"));
                return RedirectToAction("Index", new { categoryBusinessId = categoryBusinessId });
            }

            DocType docType = _docTypeService.Get(id);
            if (docType != null)
            {
                try
                {
                    _docTypeService.Delete(docType);

                    // 20200429 SuBD START remove job
                    var timeJob = _docTypeTimeJobService.Get(id);
                    if (timeJob != null)
                    {
                        if (timeJob.IsActive)
                        {
                            var scheduler = new DocTypeScheduler();
                            scheduler.StopSchedule(id);
                        }

                        _docTypeTimeJobService.Delete(timeJob);
                    }
                    // 20200429 SuBD END

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.DocType.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Common.DocType.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index", new { categoryBusinessId = categoryBusinessId });
        }

        [HttpPost]
        public ActionResult DeleteForm(Guid id, Guid dtype)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionDeleteForm"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionDeleteForm"));
                return RedirectToAction("Index");
            }

            var doctypeform = _doctypeformService.Get(id, dtype);
            if (doctypeform != null)
            {
                _doctypeformService.Delete(doctypeform);
            }
            return RedirectToAction("Form", new { id = dtype });
        }

        #region HuyNP-21.7.2020-Task 1-Bổ sung chức năng với các eform sử dụng chung một biểu cho nhiều kỳ báo cáo (năm, quý, tháng,..) chỉ cần làm chuẩn 1 form cho 1 kỳ và copy được cho các kỳ tiếp theo-START
        private string GetDocTypeCodeAndNameCopy(string nameDocTypeOld, int loai = 0)
        {
            // TRƯỜNG HỢP ĐẦU TIÊN KHÔNG CÓ MẪU COPY SAU
            // DocTypeCode định dạng FileName_C(*)
            // DocTypeName định dạng FileName_Copy(*)

            var fileNameCopyFormat = nameDocTypeOld+"{0}";
            var fileName = string.Format(fileNameCopyFormat, "");
            int i = 1;
            while (CheckExistDocTypeCodeAndName(fileName, loai)) {
                // Thay đổi giá trị của fileNameSearch
                var nameCopy = loai == 0 ? "_C" : "_Copy";
                var copyLoop = string.Concat(Enumerable.Repeat(nameCopy, i));
                //var valueUpdate = (i == 0) ? nameCopy : nameCopy+"(" + (i) + ")";
                fileName = string.Format(fileNameCopyFormat, copyLoop);
                i++;
            }
            return fileName;
        }
        private bool CheckExistDocTypeCodeAndName(string nameDocTypeOld, int loai=0)
        {
            // LOẠI 0 DOCTYPECODE
            if(loai == 0)
            {
                var searchCode = _docTypeService.Gets(g => g.DocTypeCode == nameDocTypeOld).OrderByDescending(o => o.DocTypeCode).Select(s => s.DocTypeCode).FirstOrDefault();
                return searchCode.HasValue() || searchCode == "" ? true : false;
            }
            // LOẠI 1 DOCTYPENAME
            else
            {
                var searchName = _docTypeService.Gets(g => g.DocTypeName == nameDocTypeOld).OrderByDescending(o => o.DocTypeName).Select(s => s.DocTypeName).FirstOrDefault();
                return searchName.HasValue() || searchName == "" ? true : false;
            }
        }

        private void CopyDocFieldDocTypeWorkflow(Guid docTypeIdOld, Guid docTypeIdNew)
        {
            try
            {
                var docTypeWorkFlowOldList = _workflowService.GetDocFieldDocTypeWorkflows(g => g.DocTypeId == docTypeIdOld).ToList();
                if (docTypeWorkFlowOldList != null && docTypeWorkFlowOldList.Count > 0)
                {
                    foreach (var docTypeWorkFlowOld in docTypeWorkFlowOldList)
                    {
                        var doctypeWorkflow = new DocfieldDoctypeWorkflow()
                        {
                            IsActivated = docTypeWorkFlowOld.IsActivated,
                            DocFieldId = docTypeWorkFlowOld.DocFieldId,
                            DocTypeId = docTypeIdNew,
                            WorkflowId = docTypeWorkFlowOld.WorkflowId
                        };

                        _workflowService.CreateDocFieldDocTypeWorkflow(new List<DocfieldDoctypeWorkflow>() { doctypeWorkflow });
                    }
                }
            }
            catch (Exception exception)
            {
                LogException(exception);
                CreateActivityLog(ActivityLogType.Admin, exception.Message);
                ErrorNotification(exception.Message);
            }
        }
        [HttpPost]
        public ActionResult CopyPlus(Guid id, int? categoryBusinessId = null)
        {
            // KIỂM TRA QUYỀN COPY
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCopy"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCopy"));
                return RedirectToAction("Index", new { categoryBusinessId });
            }
            DocType docTypeOffical = _docTypeService.Get(id);
            DocType docTypeCopy = docTypeOffical.Clone();
            FormModel formOffical = GetFormWith(docTypeOffical.ToModel())??new FormModel();
            FormModel formCopy = formOffical.Clone();
            try { 
                // LẤY GIỮ LIỆU DOCTYPE DỰA VÀO ID
                if (docTypeCopy == null)
                {
                    return RedirectToAction("Index");
                }

                // THAY ĐỔI MỘT SỐ THÔNG TIN 
                // KIỂM TRA MÃ BÁO CÁO VÀ LẤY MÃ BÁO CÁO TÙY CHỈNH
                docTypeCopy.DocTypeName = GetDocTypeCodeAndNameCopy(docTypeCopy.DocTypeName, 1);
                docTypeCopy.DocTypeCode = GetDocTypeCodeAndNameCopy(docTypeCopy.DocTypeCode, 0);
                docTypeCopy.CreatedByUserId = User.GetUserId();
                docTypeCopy.CreatedOnDate = DateTime.Now;
                docTypeCopy.LastModifiedOnDate = DateTime.Now;
                docTypeCopy.Unsigned = ConverToUnsign(docTypeCopy.DocTypeName);
  
                docTypeCopy.TotalViewed = 1;
                docTypeCopy.TotalRegisted = 1;
                docTypeCopy.DocTypeLaws = docTypeCopy.DocTypeLaws;
                docTypeCopy.IsAllowOnline = true;
                docTypeCopy.CategoryBusinessId = 4;
                docTypeCopy.DocTypeId = Guid.Empty;
                DocType finalDocTypeCopy = _docTypeService.CreateNReturn(docTypeCopy);
                
                // CLONE TIME JOB
                var timeJobOffical = _docTypeTimeJobService.Get(docTypeOffical.DocTypeId);
                var timeJobCopy = timeJobOffical?.Clone();
                var timeJobModel = timeJobCopy?.ToModel()?? new DocTypeTimeJobModel(finalDocTypeCopy.DocTypeId);
                var timeJobEntity = timeJobModel.ToEntity();

                // START-TẠO FORM COLLECTION
                var docTypeFormModelCopy = new DocTypeFormModel
                {
                    DocType = finalDocTypeCopy.ToModel(),
                    Form = formCopy,
                    TimeJob = timeJobModel
                };
                if(docTypeFormModelCopy.DocType.DocFieldId != null) { 
                    var finalForms = CreateFrom(docTypeFormModelCopy);
                    foreach (Form form in finalForms)
                    {
                        ChangeIsPrimary(form.FormId, finalDocTypeCopy.DocTypeId, true);
                        form.Template = formOffical.Template;
                        form.FormCode = formOffical.FormCode;
                        form.VersionDateTime = DateTime.Now;
                        _formService.Update(form);
                    }
                }
                // END-TẠO FORM COLLECTION
                // START-SAO CHÉP WORKFLOW 
                CopyDocFieldDocTypeWorkflow(id, finalDocTypeCopy.DocTypeId);
                // END-SAO CHÉP WORKFLOW
                // START-TẠO TIMEJOB
                if (timeJobEntity != null && timeJobEntity.IsActive) { 
                    _docTypeTimeJobService.Create(timeJobEntity);
                    var scheduler = new DocTypeScheduler();
                    scheduler.StopSchedule(docTypeCopy.DocTypeId);
                    scheduler.RunSchedule(new[] { docTypeCopy.DocTypeId }, timeJobEntity);
                }
                // END-TẠO TIMEJOB
                LoadDropDownList();
                ReBindDataWhenError();
                PrepareFormModel();
                BindData(timeJobModel.ScheduleTypeEnum);

                //int? docFieldId = docTypeCopy.DocFieldId;
                //DocField docField = _docFieldService.Get((int)docFieldId);
                //if (docField == null)
                //{
                //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                //    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.NotExist"));
                //}
                //else
                //{
                //    _docFieldService.Delete(docField);
                //}
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Copied"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Copied"));
            }
            catch (Exception exception) {
                LogException(exception);
                CreateActivityLog(ActivityLogType.Admin, exception.Message);
                ErrorNotification(exception.Message);
                LoadDropDownList();
                ReBindDataWhenError(docTypeCopy.CategoryBusinessId);
                PrepareFormModel();
                var timeJobException = _docTypeTimeJobService.Get(docTypeCopy.DocTypeId);
                var model = new DocTypeFormModel
                {
                    DocType = new DocTypeModel { IsActivated = true, IsAllowOnline = true },
                    Form = new FormModel(),
                    TimeJob = timeJobException.ToModel()
                };
                BindData();
                return View(model);
            }
            return RedirectToAction("Index", new { categoryBusinessId });
        }

        [HttpPost]
        public ActionResult CopyExplicit(Guid id, int? categoryBusinessId = null)
        {
            // KIỂM TRA QUYỀN COPY
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCopy"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCopy"));
                return RedirectToAction("Index", new { categoryBusinessId });
            }
            DocType docTypeOffical = _docTypeService.Get(id);
            DocType docTypeCopy = docTypeOffical.Clone();
            FormModel formOffical = GetFormWith(docTypeOffical.ToModel()) ?? new FormModel();
            FormModel formCopy = formOffical.Clone();
            try
            {
                // LẤY GIỮ LIỆU DOCTYPE DỰA VÀO ID
                if (docTypeCopy == null)
                {
                    return RedirectToAction("Index");
                }

                // THAY ĐỔI MỘT SỐ THÔNG TIN 
                // KIỂM TRA MÃ BÁO CÁO VÀ LẤY MÃ BÁO CÁO TÙY CHỈNH
                docTypeCopy.DocTypeName = GetDocTypeCodeAndNameCopy(docTypeCopy.DocTypeName, 1);
                docTypeCopy.DocTypeCode = GetDocTypeCodeAndNameCopy(docTypeCopy.DocTypeCode, 0);
                docTypeCopy.CreatedByUserId = User.GetUserId();
                docTypeCopy.CreatedOnDate = DateTime.Now;
                docTypeCopy.LastModifiedOnDate = DateTime.Now;
                docTypeCopy.Unsigned = ConverToUnsign(docTypeCopy.DocTypeName);
                docTypeCopy.TotalViewed = 1;
                docTypeCopy.TotalRegisted = 1;
                docTypeCopy.DocTypeLaws = docTypeCopy.DocTypeLaws;
                docTypeCopy.IsAllowOnline = true;
                docTypeCopy.DocTypePermission = 0;
                docTypeCopy.CategoryBusinessId = CATEGORY_BUSINESS_EXPLICIT;
                docTypeCopy.DocTypeId = Guid.Empty;
                // TẠO MỚI DOCTYPE
                DocType finalDocTypeCopy = _docTypeService.CreateNReturn(docTypeCopy);

                // CLONE TIME JOB
                var timeJobOffical = _docTypeTimeJobService.Get(docTypeOffical.DocTypeId);
                var timeJobCopy = timeJobOffical?.Clone();
                var timeJobModel = timeJobCopy?.ToModel() ?? new DocTypeTimeJobModel(finalDocTypeCopy.DocTypeId);
                var timeJobEntity = timeJobModel.ToEntity();

                // START-TẠO FORM COLLECTION
                var docTypeFormModelCopy = new DocTypeFormModel
                {
                    DocType = finalDocTypeCopy.ToModel(),
                    Form = formCopy,
                    TimeJob = timeJobModel
                };
                if (docTypeFormModelCopy.DocType.DocFieldId != null)
                {
                    var finalForms = CreateFrom(docTypeFormModelCopy);
                    foreach (Form form in finalForms)
                    {
                        ChangeIsPrimary(form.FormId, finalDocTypeCopy.DocTypeId, true);
                        form.Template = formOffical.Template;
                        form.FormCode = formOffical.FormCode;
                        form.VersionDateTime = DateTime.Now;
                        _formService.Update(form);
                    }
                }
                // END-TẠO FORM COLLECTION
                // START-SAO CHÉP WORKFLOW 
                CopyDocFieldDocTypeWorkflow(id, finalDocTypeCopy.DocTypeId);
                // END-SAO CHÉP WORKFLOW
                // START-TẠO TIMEJOB
                if (timeJobEntity != null && timeJobEntity.IsActive)
                {
                    _docTypeTimeJobService.Create(timeJobEntity);
                    var scheduler = new DocTypeScheduler();
                    scheduler.StopSchedule(docTypeCopy.DocTypeId);
                    scheduler.RunSchedule(new[] { docTypeCopy.DocTypeId }, timeJobEntity);
                }
                // END-TẠO TIMEJOB
                LoadDropDownList();
                ReBindDataWhenError();
                PrepareFormModel();
                BindData(timeJobModel.ScheduleTypeEnum);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Copied"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Copied"));
            }
            catch (Exception exception)
            {
                LogException(exception);
                CreateActivityLog(ActivityLogType.Admin, exception.Message);
                ErrorNotification(exception.Message);
                LoadDropDownList();
                ReBindDataWhenError(docTypeCopy.CategoryBusinessId);
                PrepareFormModel();
                var timeJobException = _docTypeTimeJobService.Get(docTypeCopy.DocTypeId);
                var model = new DocTypeFormModel
                {
                    DocType = new DocTypeModel { IsActivated = true, IsAllowOnline = true },
                    Form = new FormModel(),
                    TimeJob = timeJobException.ToModel()
                };
                BindData();
                return View(model);
            }
            return RedirectToAction("Index", new { categoryBusinessId });
        }

        #endregion HuyNP-21.7.2020-Task 1-Thêm chức năng copy cho báo cáo-END

        #region HuyNP-22.7.2020-Task 2-Export JSON báo cáo-START
        [HttpPost]
        public ActionResult ExportJSON(Guid id, int? categoryBusinessId = null) {
            var docType = _docTypeService.Gets(g=>g.DocTypeId == id && ((categoryBusinessId != null && g.CategoryBusinessId == categoryBusinessId) || categoryBusinessId == null)).FirstOrDefault();
            var form = GetFormWith(docType.ToModel()).ToEntity();
            var timeJob = _docTypeTimeJobService.Get(docType.DocTypeId);
            var docTypeForm = _doctypeFormService.Gets(g => g.DocTypeId == id).ToList();
            var docFieldDocTypeWorkflow = _workflowService.GetDocFieldDocTypeWorkflows(g => g.DocTypeId == id).ToList();
            // DÙNG DOCTYPE VÀ FORM
            var data = new List<dynamic> {
                docType,
                form
            };
            var fileName = docType.DocTypeName + ".json";
            byte[] fileBytes = GetFile(data, fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public byte[] GetFile(List<dynamic> docType, string fileName)
        {
            var fullName = Server.MapPath("~/Temp/"+ fileName);
            using (FileStream fs = System.IO.File.Create(fullName))
            {
                string jsonData = JsonConvert.SerializeObject(docType, Formatting.None);
                //serialize object directly into file stream
                byte[] bytes = Encoding.UTF8.GetBytes(jsonData);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                DeleteFileIfExists(fullName);
                return bytes;
            }
        }
        private void DeleteFileIfExists(string path)
        {

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        #endregion HuyNP-22.7.2020-Task 2-Có thể copy eform từ dự án này sang dự án khác. Xuất JSON-END

        public ActionResult Search(DocTypeSearchModel search, int pageSize, int categoryBusinessId)
        {
            IEnumerable<DocTypeModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    model = GetDocTypeModels(search, DEFAULT_SORT_BY, false, 1, pageSize, categoryBusinessId);
                }
                else
                {
                    ViewBag.SortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = 0
                    };
                }
            }
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;

            //ViewBag.CategoryBusinessId = BindCategoryBusiness(4);
            ViewBag.CategoryBusinessIdValue = categoryBusinessId;
            ViewBag.ListActionLevel = GetActionLevel();

            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            DocTypeSearchModel search, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<DocTypeModel> model = null;
            if (Request.IsAjaxRequest())
            {
                model = GetDocTypeModels(search, sortBy, isSortDesc, page, pageSize);
                ViewBag.ListActionLevel = GetActionLevel();
            }
            else
            {
                ViewBag.SortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = 1,
                    IsSortDescending = false,
                    SortBy = DEFAULT_SORT_BY,
                    TotalRecordCount = 0
                };
                ViewBag.ListActionLevel = GetActionLevel();
            }
            return PartialView("_PartialList", model);
        }

        #region quản lý quy trình cũ

        //public ActionResult ConfigWorkflow()
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionConfigWorkflow"));
        //        return RedirectToAction("Index");
        //    }

        //    var allCategoryBusiness = BindCategoryBusiness().StringifyJs();
        //    var allDocFields = _docFieldService.GetsAs(df => new { df.DocFieldId, df.DocFieldName, df.CategoryBusinessId }).StringifyJs();
        //    var allDocType = _docTypeService.GetsAs(d => new { d.DocTypeId, d.DocTypeName, d.CategoryBusinessId, d.DocFieldId, d.IsActivated })
        //        .OrderBy(d => d.DocTypeName).StringifyJs();
        //    var allWorkflow =
        //        _workflowService.GetsAs(w => new { w.WorkflowId, w.WorkflowName, w.DocTypeId, w.Template, w.IsActivated, w.ExpireProcess, w.WorkflowTypeJson })
        //            .OrderBy(w => w.WorkflowName).StringifyJs();

        //    ViewBag.AllCategoryBusiness = allCategoryBusiness;
        //    ViewBag.AllDocFields = allDocFields;
        //    ViewBag.AllDocType = allDocType;
        //    ViewBag.AllWorkflow = allWorkflow;
        //    return View();
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeCreateWorkflow")]
        //public JsonResult CreateWorkflow(Guid doctypeId, string workflowName, string expireprocess, string workflowTypes)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionCreateWorkflow") });
        //    }

        //    var doctype = _docTypeService.Get(doctypeId);
        //    if (doctype == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Doctype.NotFindId") + doctypeId });
        //    }
        //    var workflowTypesTemp = Json2.ParseAs<List<WorkflowType>>(workflowTypes);
        //    foreach (var workflowType in workflowTypesTemp)
        //    {
        //        if (workflowType.Id == Guid.Empty)
        //        {
        //            workflowType.Id = Guid.NewGuid();
        //        }
        //    }
        //    workflowTypes = Json2.Stringify(workflowTypesTemp);
        //    var workflow = new Workflow
        //                       {
        //                           IsActivated = false,
        //                           DocTypeId = doctypeId,
        //                           WorkflowName = workflowName,
        //                           Json = string.Empty,
        //                           CreatedOnDate = DateTime.Now,
        //                           CreatedByUserId = User.GetUserId(),
        //                           VersionDateTime = DateTime.Now,
        //                           ExpireProcess = int.Parse(expireprocess),
        //                           WorkflowTypeJson = workflowTypes
        //                       };
        //    _workflowService.Create(workflow);
        //    return Json(new { id = workflow.WorkflowId });
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeCopyWorkflow")]
        //public JsonResult CopyWorkflow(int id, Guid doctypeId)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionCopyWorkflow") });
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
        //    }
        //    var doctype = _docTypeService.Get(doctypeId);
        //    if (doctype == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Doctype.NotFindId") + doctypeId });
        //    }

        //    //HopCV
        //    //Nếu loại văn bản chưa có  quy trình nào thì active quy trình lên
        //    var exist = _workflowService.GetsAs(p => p.WorkflowId, p => p.DocTypeId == doctypeId);
        //    var copy = new Workflow
        //                   {
        //                       IsActivated = (exist != null && exist.Any()) ? false : true,
        //                       DocTypeId = doctypeId,
        //                       WorkflowName = workflow.WorkflowName,
        //                       Json = workflow.Json,
        //                       Template = workflow.Template,
        //                       CreatedOnDate = DateTime.Now,
        //                       CreatedByUserId = User.GetUserId(),
        //                       VersionDateTime = DateTime.Now,
        //                       ExpireProcess = workflow.ExpireProcess
        //                   };
        //    _workflowService.Create(copy);
        //    return Json(new { id = copy.WorkflowId });
        //}

        //public ActionResult Workflow(int id, int nodeId)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionWorkflow"));
        //        return RedirectToAction("Index");
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id);
        //        return RedirectToAction("Index", "Error");
        //    }
        //    if (string.IsNullOrEmpty(workflow.Json))
        //    {
        //        return View();
        //    }
        //    var pathOutput = workflow.JsonInObject;

        //    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
        //    if (nodeOutput == null)
        //    {
        //        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId);
        //        return RedirectToAction("Index", "Error");
        //    }

        //    ViewBag.GetTimeType = TimeType();
        //    ViewBag.AllPositions = GetAllJobTitless();
        //    ViewBag.ViewOption = GetViewOption(nodeOutput.ViewOption);
        //    ViewBag.SelectedNodes = nodeOutput.Address.StringifyJs();
        //    ViewBag.AllNodesInPath = pathOutput.Nodes.Select(u => new
        //    {
        //        u.Id,
        //        u.NodeName,
        //    }).StringifyJs();
        //    ViewBag.AllUsers = GetAllUsers();
        //    ViewBag.AllDepartments = GetAllDepartment();
        //    ViewBag.MaxDepartmentLevel = _departmentService.GetMaxLevel();
        //    ViewBag.PathId = id;
        //    ViewBag.AllDeptUserPosition = GetAllDeptUserPosition();
        //    if (nodeOutput.TimeType == 0)
        //    {
        //        nodeOutput.TimeInNode = nodeOutput.TimeInNode / 24;
        //    }
        //    return View(nodeOutput.ToModel());
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeWorkflow")]
        //public JsonResult Workflow(NodeModel model)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = true, message = _resourceService.GetResource("Customer.DocType.NotPermissionUpdateWorkflow") });
        //    }

        //    // TODO: (CuongNT-?-CuongNT-030713) Xem lại việc sử dụng thuộc tính PathId
        //    if (model.PathId <= 0)
        //    {
        //        LogException(new ArgumentException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Node.PathIdError")));
        //        return Json(new { error = true });
        //    }

        //    var workflow = _workflowService.Get(model.PathId);
        //    if (workflow == null)
        //    {
        //        return Json(new { success = true });
        //    }
        //    var pathOutput = workflow.JsonInObject;
        //    var nodeOutput = pathOutput.Nodes.SingleOrDefault(t => t.Id == model.Id);
        //    if (nodeOutput == null)
        //    {
        //        return Json(new { success = true });
        //    }
        //    var oldNodeName = nodeOutput.NodeName;
        //    model.ViewOption = (string.IsNullOrEmpty(model.DocTypeView)
        //        ? 0
        //        : model.DocTypeView == "DocTypeIgnore"
        //            ? 4096
        //            : 8192)
        //                       + (string.IsNullOrEmpty(model.PageNumberView)
        //                           ? 0
        //                           : model.PageNumberView == "PageNumberIgnore" ? 32 : 2048) +
        //                       (string.IsNullOrEmpty(model.DocFieldView)
        //                           ? 0
        //                           : model.DocFieldView == "DocFieldIgnore" ? 1 : 64) +
        //                       (string.IsNullOrEmpty(model.KeywordView)
        //                           ? 0
        //                           : model.KeywordView == "KeywordIgnore" ? 2 : 128) +
        //                       (string.IsNullOrEmpty(model.GroupView)
        //                           ? 0
        //                           : model.GroupView == "GroupIgnore" ? 4 : 256) +
        //                       (string.IsNullOrEmpty(model.SendTypeView)
        //                           ? 0
        //                           : model.SendTypeView == "SenTypeIgnore" ? 8 : 512) +
        //                       (string.IsNullOrEmpty(model.DestinationView)
        //                           ? 0
        //                           : model.DestinationView == "DestinationIgnore" ? 16 : 1024);

        //    model.TimeInNode = model.TimeType == 0 ? model.TimeInNode * 24 : model.TimeInNode;
        //    var newNode = model.ToEntity(nodeOutput);
        //    pathOutput.Nodes.Remove(nodeOutput);
        //    pathOutput.Id = model.PathId;
        //    pathOutput.IsActivated = workflow.IsActivated;
        //    pathOutput.Name = workflow.WorkflowName;
        //    var listAddress = Json2.ParseAs<List<Core.Workflow.Address>>(model.NodeAddress);
        //    newNode.Address = listAddress;
        //    pathOutput.Nodes.Add(newNode);
        //    pathOutput.Nodes = pathOutput.Nodes.OrderBy(t => t.Id).ToList();
        //    if (oldNodeName != model.NodeName)
        //    {
        //        //Cập nhật lại tất cả Address của các Node
        //        foreach (var node in pathOutput.Nodes)
        //        {
        //            var address = node.Address.SingleOrDefault(t => t.NodeFrom == model.Id);
        //            if (address != null)
        //            {
        //                address.NodeNameFrom = model.NodeName;
        //            }
        //        }
        //    }
        //    workflow.Json = pathOutput.Stringify();
        //    try
        //    {
        //        _workflowService.Update(workflow);
        //    }
        //    catch (EgovException ex)
        //    {
        //        return Json(new { error = true, message = ex.Message });
        //    }

        //    return Json(new { success = true });
        //}
        //[HttpPost]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeUpdateWorkflow")]
        //public JsonResult UpdateWorkflow(int id, string workflowName, string expireprocess, string workflowTypes)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionUpdateWorkflow") });
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
        //    }
        //    var workflowTypesTemp = Json2.ParseAs<List<WorkflowType>>(workflowTypes);
        //    foreach (var workflowType in workflowTypesTemp)
        //    {
        //        if (workflowType.Id == Guid.Empty)
        //        {
        //            workflowType.Id = Guid.NewGuid();
        //        }
        //    }
        //    workflowTypes = Json2.Stringify(workflowTypesTemp);
        //    workflow.WorkflowName = workflowName;
        //    workflow.ExpireProcess = int.Parse(expireprocess);
        //    workflow.WorkflowTypeJson = workflowTypes;
        //    _workflowService.Update(workflow);
        //    return Json(new { id = workflow.WorkflowId });
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeDeleteWorkflow")]
        //public JsonResult DeleteWorkflow(int id)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionDeleteWorkflow") });
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
        //    }
        //    _workflowService.Delete(workflow);
        //    return Json(new { success = true });
        //}

        //public JsonResult GetWorkflow(int id)
        //{
        //    var workflow = _workflowService.Get(id);
        //    return workflow == null
        //               ? Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id }, JsonRequestBehavior.AllowGet)
        //               : Json(new { workflow = workflow.Json }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeSaveWorkflow")]
        //public JsonResult SaveWorkflow(int id, string json)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionSaveWorkflow") });
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
        //    }
        //    if (!string.IsNullOrWhiteSpace(json))
        //    {
        //        try
        //        {
        //            var data = Json2.ParseAs<Path>(json);
        //        }
        //        catch (Exception)
        //        {
        //            return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotValid") });
        //        }
        //    }
        //    workflow.Json = json;
        //    workflow.LastModifiedByUserId = User.GetUserId();
        //    workflow.LastModifiedOnDate = DateTime.Now;
        //    _workflowService.Update(workflow);
        //    return Json(new { success = true });
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeSetIsActiveWorkflow")]
        //public JsonResult SetIsActiveWorkflow(int id, bool isActivated)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionSetIsActiveWorkflow") });
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
        //    }
        //    if (isActivated)
        //    {
        //        var doctype = _docTypeService.Get(workflow.DocTypeId);
        //        var allWorkflowOfDocType = doctype.Workflows;
        //        var currentWorkflowIsActive =
        //            allWorkflowOfDocType.SingleOrDefault(w => w.DocTypeId == workflow.DocTypeId && w.IsActivated);
        //        if (currentWorkflowIsActive != null)
        //        {
        //            currentWorkflowIsActive.IsActivated = false;
        //        }
        //        workflow.IsActivated = true;
        //    }
        //    else
        //    {
        //        workflow.IsActivated = false;
        //    }
        //    _workflowService.Update(workflow);
        //    return Json(new { id = workflow.WorkflowId });
        //}

        //public string GetWorkflowTemplate(int id)
        //{
        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        return null;
        //    }
        //    return workflow.Template;
        //}

        //public string GetNodeTemplate(int workflowId, int nodeId)
        //{
        //    var workflow = _workflowService.Get(workflowId);
        //    if (workflow == null)
        //    {
        //        return null;
        //    }
        //    if (string.IsNullOrEmpty(workflow.Json))
        //    {
        //        return null;
        //    }
        //    var pathOutput = workflow.JsonInObject;
        //    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
        //    if (nodeOutput == null)
        //    {
        //        return null;
        //    }
        //    return nodeOutput.Template;
        //}

        //public ActionResult ConfigTemplateWorkflow(int id)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateWorkflow"));
        //        return RedirectToAction("Index", "Error");
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id);
        //        return RedirectToAction("Index", "Error");
        //    }
        //    ViewBag.CategoryBusiness = workflow.DocType.CategoryBusinessIdInEnum.ToString();
        //    ViewBag.WorkflowId = id;
        //    ViewBag.Template = workflow.Template;
        //    ViewBag.TemplateCategoryBusiness = FileManager.Default.Exist(ViewBag.CategoryBusiness, Server.MapPath(TEMPLALTE_PATH))
        //        ? FileManager.Default.ReadString(ViewBag.CategoryBusiness, Server.MapPath(TEMPLALTE_PATH))
        //        : string.Empty;
        //    ViewBag.OtherWorkflows = _workflowService.GetsAs(
        //        w => new { w.WorkflowId, w.WorkflowName, w.DocType.DocTypeName },
        //        w => w.WorkflowId != id && w.DocType.CategoryBusinessId == workflow.DocType.CategoryBusinessId).StringifyJs();
        //    return View();
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeConfigTemplateWorkflow")]
        //public JsonResult ConfigTemplateWorkflow(int id, string template)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateWorkflow") });
        //    }

        //    var workflow = _workflowService.Get(id);
        //    if (workflow == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
        //    }
        //    workflow.Template = template;
        //    _workflowService.Update(workflow);
        //    return Json(new { success = true }); ;
        //}

        //public ActionResult ConfigTemplateNode(int workflowId, int nodeId)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateNode"));
        //        return RedirectToAction("Index", "Error");
        //    }

        //    var workflow = _workflowService.Get(workflowId);
        //    if (workflow == null)
        //    {
        //        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + workflowId);
        //        return RedirectToAction("Index", "Error");
        //    }
        //    if (string.IsNullOrEmpty(workflow.Json))
        //    {
        //        ErrorNotification("Chưa cấu hình quy trình");
        //        return RedirectToAction("Index", "Error");
        //    }
        //    var pathOutput = workflow.JsonInObject;
        //    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
        //    if (nodeOutput == null)
        //    {
        //        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId);
        //        return RedirectToAction("Index", "Error");
        //    }

        //    ViewBag.CategoryBusiness = workflow.DocType.CategoryBusinessIdInEnum.ToString();
        //    ViewBag.WorkflowId = workflowId;
        //    ViewBag.TemplateWorkflow = workflow.Template;
        //    ViewBag.NodeId = nodeId;
        //    ViewBag.TemplateNode = nodeOutput.Template;
        //    ViewBag.OtherNodes =
        //        pathOutput.Nodes.Where(n => n.Id != nodeId)
        //            .Select(n => new { n.Id, n.NodeName })
        //            .StringifyJs();

        //    return View();
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        ////[ValidateAntiForgeryToken(Salt = "DocTypeConfigTemplateNode")]
        //public JsonResult ConfigTemplateNode(int workflowId, int nodeId, string template)
        //{
        //    //Hopcv: 190614
        //    //Kiểm tra quyền
        //    if (!HasPermission())
        //    {
        //        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateNode") });
        //    }

        //    var workflow = _workflowService.Get(workflowId);
        //    if (workflow == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + workflowId });
        //    }
        //    if (string.IsNullOrEmpty(workflow.Json))
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotSetting") });
        //    }
        //    var pathOutput = workflow.JsonInObject;
        //    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
        //    if (nodeOutput == null)
        //    {
        //        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId });
        //    }
        //    nodeOutput.Template = template;
        //    workflow.Json = pathOutput.Stringify();
        //    _workflowService.Update(workflow);
        //    return Json(new { success = true }); ;
        //}

        #endregion

        public JsonResult CategoryBusinessChange(int categoryBusinessId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCategoryBusinessChange"));
                return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionCategoryBusinessChange") });
            }

            var result = Json(
                           new
                           {
                               AllCategorys =
                               GetAllCategorys(categoryBusinessId).Select(
                                   u => new { u.CategoryId, u.CategoryName }).StringifyJs(),
                               AllDocFields =
                               GetAllDocFields(categoryBusinessId).Select(
                                   u => new { u.DocFieldId, u.DocFieldName }).StringifyJs()
                           }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public JsonResult ChangeIsPrimary(Guid formid, Guid doctypeid, bool isprimary)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsPrimary"));
                return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsPrimary") });
            }

            _doctypeformService.Create(new DocTypeForm
            {
                DocTypeId = doctypeid,
                FormId = formid,
                IsPrimary = isprimary,
                IsActive = true
            });

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeIsActive(Guid formid, Guid doctypeid, bool status)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive"));
                return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive") });
            }

            var doctypeform = _doctypeformService.Get(formid, doctypeid);
            if (doctypeform == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            doctypeform.IsActive = status;
            _doctypeformService.Update(doctypeform);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateForm(Guid doctypeId, Guid formMainId, string formsId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionUpdateForm") });
            }

            if (formMainId.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                _doctypeformService.Create(new DocTypeForm
                {
                    DocTypeId = doctypeId,
                    FormId = formMainId,
                    IsPrimary = true
                });
            }

            if (formsId != "")
            {
                var forms = formsId.Split(',');
                foreach (var formId in forms)
                {
                    _doctypeformService.Create(new DocTypeForm
                    {
                        DocTypeId = doctypeId,
                        FormId = Guid.Parse(formId),
                        IsPrimary = false
                    });
                }
            }
            //return RedirectToAction("Form", new { id = doctypeId });
            CreateActivityLog(ActivityLogType.Admin, "Update Form thành công");
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Paper(Guid id)
        {
            ViewBag.DoctypeId = id;
            var model = _doctypePaperService.GetPapersOfDoctype(id);
            var docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            return View(model);
        }

        public ActionResult DisplayPapers(Guid id)
        {
            return View(_doctypePaperService.GetPapersNotOfDoctype(id).ToListModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPaper(Guid doctypeId, int paperId)
        {
            var doctypePaper = new DoctypePaper(doctypeId, paperId, true);
            _doctypePaperService.Create(doctypePaper);
            CreateActivityLog(ActivityLogType.Admin, "Add Paper thành công");
            return RedirectToAction("Paper", "Admin/Doctype", new { id = doctypeId });
        }

        [HttpPost]
        public ActionResult DeleteDoctypePaper(Guid doctypeId, int doctypePaperId)
        {
            try
            {
                var doctypePaper = _doctypePaperService.Get(doctypePaperId);
                _doctypePaperService.Delete(doctypePaper);
                CreateActivityLog(ActivityLogType.Admin, "Xóa doctype paper thành công");
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Paper", "Doctype", new { id = doctypeId });
        }

        //[HttpPost]
        //public ActionResult ChangePaperRequire(Guid doctypeId, int doctypePaperId, bool isRequired)
        //{
        //    try
        //    {
        //        var doctypePaper = _doctypePaperService.Get(doctypePaperId);
        //        if (doctypePaper != null)
        //        {
        //            doctypePaper.IsRequired = isRequired;
        //            _doctypePaperService.Update(doctypePaper);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorNotification(ex.Message);
        //    }
        //    return RedirectToAction("Paper", "Doctype", new { id = doctypeId });
        //}

        [HttpPost]
        public JsonResult ChangePaperRequire(int doctypePaperId)
        {
            var result = false;
            var doctypePaper = _doctypePaperService.Get(doctypePaperId);
            if (doctypePaper != null)
            {
                if (doctypePaper.IsRequired.HasValue)
                {
                    doctypePaper.IsRequired ^= true;
                }
                else
                {
                    //Trường hợp chuyển từ hệ thống cũ sang IsRequired = null
                    doctypePaper.IsRequired = true;
                }
                _doctypePaperService.Update(doctypePaper);
                result = true;
            }

            return Json(result);
        }

        public ActionResult Fee(Guid id)
        {
            ViewBag.DoctypeId = id;
            var model = _doctypeFeeService.GetFeesOfDoctype(id);
            var docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            return View(model);
        }

        public ActionResult DisplayFees(Guid id)
        {
            return View(_doctypeFeeService.GetFeesNotOfDoctype(id).ToListModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddFee(Guid doctypeId, int feeId)
        {
            var doctypeFee = new DoctypeFee(doctypeId, feeId, true);
            _doctypeFeeService.Create(doctypeFee);
            return RedirectToAction("Fee", "Admin/Doctype", new { id = doctypeId });
        }

        [HttpPost]
        public ActionResult DeleteDoctypeFee(Guid doctypeId, int doctypeFeeId)
        {
            try
            {
                var doctypeFee = _doctypeFeeService.Get(doctypeFeeId);
                _doctypeFeeService.Delete(doctypeFee);
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Fee", "Doctype", new { id = doctypeId });
        }

        #region "Doctype-EgovOnline"

        /// <summary>
        /// Danh sách văn bản quy phạm của thủ tục hành chính
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DocTypeLaw(Guid id)
        {
            ViewBag.DoctypeId = id;
            var doctype = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = doctype.CategoryBusinessId;
            if (doctype == null)
            {
                return RedirectToAction("Index");
            }
            var model = doctype.ToModel();
            return View(model);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateDocTypeLaw(Guid docTypeId, int[] lawIds)
        {
            if (lawIds != null && lawIds.Any())
            {
                var docType = _docTypeService.Get(docTypeId);
                if (docType == null)
                {
                    return Json(new { error = _resourceService.GetResource("Customer.DocType.NotExist") });
                }
                try
                {
                    var laws = _lawService.Gets(p => lawIds.Contains(p.LawId));
                    var lawIdToAdds = new List<int>();
                    var result = new List<Law>();
                    foreach (var law in laws)
                    {
                        if (!docType.DocTypeLaws.Any(d => d.LawId == law.LawId))
                        {
                            result.Add(law);
                            lawIdToAdds.Add(law.LawId);
                        };
                    }

                    _docTypeService.AddDoctypeLaws(docType.DocTypeId, lawIdToAdds);
                    return Json(new
                    {
                        success = _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Success"),
                        data = result.Select(p => new { p.LawId, p.NumberSign, p.SubContent })
                    });
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Error"));
                    return Json(new { error = _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Error") });
                }
            }

            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Success"));
            return Json(new { success = _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Success") });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteDocTypeLaw(Guid docTypeId, int lawId)
        {
            try
            {
                _docTypeService.DeleteDocTypeLaw(docTypeId, lawId);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Success"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Error"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Error"));
            }
            return RedirectToAction("DocTypeLaw", "DocType", new { id = docTypeId });
        }

        /// <summary>
        /// Danh sách văn bản quy phạm của thủ tục hành chính
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DocTypeTemplate(Guid id)
        {
            ViewBag.DoctypeId = id;
            ViewBag.CategoryBusinessId = 4;
            return View(_doctypeTemplateService.GetsByDoctypeId(id).ToListModel());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <param name="doctypeTemplateId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddDoctypeTemplate(Guid doctypeId, int onlineTemplateId, string onlineTemplateName)
        {
            if (!_doctypeTemplateService.Exist(x => x.DoctypeId == doctypeId && x.OnlineTemplateId == onlineTemplateId))
            {
                var doctypeTemplate = new DoctypeTemplate
                {
                    DoctypeId = doctypeId,
                    OnlineTemplateId = onlineTemplateId,
                    Name = onlineTemplateName
                };
                _doctypeTemplateService.Create(doctypeTemplate);
            }
            return Json(new { success = true });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DisplayOnlineTemplates()
        {
            return View(_onlineTemplateService.Gets().ToListModel());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <param name="doctypeTemplateId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteDoctypeTemplate(Guid doctypeId, int doctypeTemplateId)
        {
            try
            {
                var doctypeTemplate = _doctypeTemplateService.Get(doctypeTemplateId);
                _doctypeTemplateService.Delete(doctypeTemplate);
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("DocTypeTemplate", "DocType", new { id = doctypeId });
        }

        [HttpPost]
        public ActionResult GetManufacturer(string Item)
        {
            var levelId = Convert.ToInt32(Item);
            var offfices = _officeService.GetOfficesByLevelId(levelId);
            return Json(offfices);
        }

        #endregion "Doctype-EgovOnline"

        #region Báo cáo tường minh

        public JsonResult ExecuteTemplateKeys(List<string> templateKeyNames, Guid formId, int timeKey)
        {
            var form = _formService.Get(formId);
            var templateKey = new TemplateKey();
            var templateKeyNameDecode = string.Empty;

            foreach (string templateKeyName in templateKeyNames)
            {
                templateKeyNameDecode = HttpUtility.HtmlDecode(templateKeyName);

                templateKey = _templateKeyService.Gets(p => p.Name.Equals(templateKeyNameDecode.Replace("@@", "").Replace("@@", ""))).FirstOrDefault();

                if (templateKey != null)
                {
                    string pattern = @"@([^=<> ')(\s]+)(|(?=\s)|$)";
                    // Create a Regex
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(templateKey.Sql);
                    List<object> paramTemplateKeys = new List<object>();

                    while (match.Success)
                    {
                        paramTemplateKeys.Add(new SqlParameter(match.Groups[0].Value, timeKey));
                        //match = match.NextMatch();
                        break;
                    }

                    var arrPara = paramTemplateKeys.ToArray();

                    var templateKeyData = _templateKeyService.GetValueByQuery(templateKey, arrPara);

                    if (templateKeyData.Count() > 0)
                        form.ExplicitTemplate = form.ExplicitTemplate.Replace(templateKeyName, templateKeyData.ElementAt(0).Values.ElementAt(0).ToString());
                }
            }

            return Json(new { succress = true, expiciteTemplate = form.ExplicitTemplate }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataTemplateKeys(string templateKeyName, Guid formId, string timeKey)
        {
            var form = _formService.Get(formId);
            var templateKeyNameDecode = HttpUtility.HtmlDecode(templateKeyName);
            if (string.IsNullOrWhiteSpace(templateKeyNameDecode)) return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            TemplateKey templateKey = null;

            if (templateKeyNameDecode.IndexOf("@@", StringComparison.Ordinal) >= 0)
            {
                templateKeyNameDecode = templateKeyNameDecode.Replace("@@", "").Replace("@@", "");
                templateKey = _templateKeyService.Gets(p => p.Name.Equals(templateKeyNameDecode)).FirstOrDefault();
            }
            else if (templateKeyNameDecode.IndexOf("##", StringComparison.Ordinal) >= 0)
            {
                templateKeyNameDecode = templateKeyNameDecode.Replace("##", "").Replace("##", "");
                templateKey = _templateKeyService.Gets(p => p.Name.Equals(templateKeyNameDecode)).FirstOrDefault();
            }

            if (templateKey == null) return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            const string pattern = @"@([^=<> ')(\s]+)(|(?=\s)|$)";
            // Create a Regex
            var regex = new Regex(pattern);
            var match = regex.Match(templateKey.Sql);
            var paramTemplateKeys = new List<object>();

            while (match.Success)
            {
                paramTemplateKeys.Add(new SqlParameter(match.Groups[0].Value, timeKey));
                break;
            }
            var arrPara = paramTemplateKeys.ToArray();
            if (templateKey.Type == 8)
            {
                DataTable dt;
                templateKey.Sql = Regex.Replace(templateKey.Sql,"@TimeKey", timeKey,RegexOptions.IgnoreCase);
                var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
                var organizeKey = currentDepartment?.Emails;

                templateKey.Sql = Regex.Replace(templateKey.Sql,
                    "@OrganizeKey", $"'{organizeKey}'",RegexOptions.IgnoreCase);
                templateKey.Sql = templateKey.Sql.Replace("@OrganizeKey", $"'{organizeKey}'");
                using (var dbConn = new MySqlConnection(_adminSetting.DashboardConnection))
                {
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = templateKey.Sql;
                    dt = new DataTable();
                    try
                    {
                        dbConn.Open();
                        using (var da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception e)
                    {
                        //
                    }
                    dbConn.Close();
                }

                var arr = JsonConvert.SerializeObject(dt);// dt.AsEnumerable().ToList().ConvertAll(o => (object)o);

                return Json(new { Success = true, result = arr, ExpiciteTemplate = form.ExplicitTemplate, Type = 8, HtmlTemplate = templateKey.HtmlTemplate }, JsonRequestBehavior.AllowGet);
            }
            IEnumerable<dynamic> list = _templateKeyService.GetListByQuery(templateKey, arrPara).ToList();
            if (templateKey.Type == Convert.ToInt32(TemplateKeysType.Display))
                return Json(new { Success = true, result = list, ExpiciteTemplate = form.ExplicitTemplate, Type = 8, HtmlTemplate = templateKey.HtmlTemplate }, JsonRequestBehavior.AllowGet);
            if (templateKey.Type != Convert.ToInt32(TemplateKeysType.Query))
                return Json(new { Success = true, ExpiciteTemplate = form.ExplicitTemplate },
                    JsonRequestBehavior.AllowGet);
            if (!list.Any())
                return Json(new { Success = true, ExpiciteTemplate = form.ExplicitTemplate },
                    JsonRequestBehavior.AllowGet);

            var keyValuePair = new Dictionary<string, object>(list.ToArray()[0]).FirstOrDefault(x => x.Key == templateKeyNameDecode);


            if (keyValuePair.Key == null)
                return Json(new { Success = false, ExpiciteTemplate = form.ExplicitTemplate }, JsonRequestBehavior.AllowGet);
            var obj = new TemplateKeyResult
            {
                Success = true,
                ExpiciteTemplate = form.ExplicitTemplate,
                HtmlTemplate = templateKey.HtmlTemplate,
                Type = 4,
                NewValue = keyValuePair.Value?.ToString()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateExplicit()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
            }

            LoadDropDownList();
            ReBindDataWhenError(4);
            PrepareFormModel();

            var sModel = new DocTypeFormModel();
            sModel.DocType = new DocTypeModel() { IsActivated = true, IsAllowOnline = true };
            sModel.Form = new FormModel();

            // 20200513 SuBD START
            sModel.TimeJob = new DocTypeTimeJobModel();
            BindData();
            // 20200513 SuBD END

            LoadTemplateAndReportKeys();

            return View(sModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateExplicit(DocTypeFormModel sModel)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
            }

            //lấy ra model dạng DocType sau khi thay đổi cơ chế post ==> logic về như cũ
            var model = sModel.DocType;

            if (ModelState.IsValid)
            {

                DocType docTypeCreate = model.ToEntity();
                docTypeCreate.CreatedByUserId = User.GetUserId();
                docTypeCreate.CreatedOnDate = DateTime.Now;
                docTypeCreate.LastModifiedOnDate = DateTime.Now;
                docTypeCreate.DocTypePermission = 0;
                docTypeCreate.TotalRegisted = 1; //eGovOnline
                docTypeCreate.TotalViewed = 1; //eGovOnline
                docTypeCreate.Unsigned = ConverToUnsign(model.DocTypeName); //eGovOnline

                //Báo cáo tường minh có categoryBusinessId = 8
                docTypeCreate.CategoryBusinessId = CATEGORY_BUSINESS_EXPLICIT;
                docTypeCreate.IsAllowOnline = true;

                var finalDocTypes = new List<DocType>();
                var finalForm = new Form();

                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        docTypeCreate.DocTypePermission |= per;
                    }
                }
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.DocTypeName.Split(';').Distinct();
                        var list = new List<DocType>();
                        foreach (var name in names)
                        {
                            var item = docTypeCreate.Clone();
                            item.DocTypeName = name;
                            list.Add(item);
                            //Chuẩn bị trước key để tạo liên kết giữa báo cáo và biểu mẫu
                        }
                        finalDocTypes = _docTypeService.CreateNReturn(list, model.IgnoreExist);
                    }
                    else
                    {
                        finalDocTypes = new List<DocType> { _docTypeService.CreateNReturn(docTypeCreate) };
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Created"));
                    SuccessNotification(_resourceService.GetResource("Customer.DocType.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                    LoadDropDownList();
                    LoadTemplateAndReportKeys();
                    ReBindDataWhenError(docTypeCreate.CategoryBusinessId);

                    PrepareFormModel();

                    // 20200513 SuBD START
                    var timeJob = sModel.TimeJob;
                    // 20200513 SuBD END

                    sModel = new DocTypeFormModel();
                    sModel.DocType = new DocTypeModel { IsActivated = true, IsAllowOnline = true };
                    sModel.Form = new FormModel();

                    // 20200513 SuBD START
                    sModel.TimeJob = timeJob;
                    BindData();
                    // 20200513 SuBD END

                    return View(sModel);
                }

                ModelState.Clear();
                LoadDropDownList();
                ReBindDataWhenError(4);
                ViewBag.StoreIds = model.StoreIds;
                var modelCreate = new DocTypeModel
                {
                    IsActivated = true,
                    IsAllowOnline = true,
                    CategoryId = model.CategoryId,
                    CategoryBusinessId = model.CategoryBusinessId,
                    DocFieldId = model.DocFieldId, //eGovOnline
                    OfficeId = model.OfficeId, //eGovOnline
                    LevelId = model.LevelId, //eGovOnline
                    Content = model.Content, //eGovOnline
                    ActionLevel = model.ActionLevel, //eGovOnline
                    DocTypeName = model.DocFieldName,
                    CompendiumDefault = model.CompendiumDefault,
                    DocTypePermission = docTypeCreate.ToModel().DocTypePermission,
                };

                var finalForms = CreateFrom(sModel);
                sModel.DocType = modelCreate;

                foreach (Form form in finalForms)
                {
                    foreach (DocType docType in finalDocTypes)
                    {
                        ChangeIsPrimary(form.FormId, docType.DocTypeId, true);
                    }
                }

                // 20200513 SuBD START
                if (sModel.TimeJob != null && sModel.TimeJob.IsActive)
                {
                    var timeJob = sModel.TimeJob.ToEntity();
                    foreach (var docType in finalDocTypes)
                    {
                        timeJob.DocTypeId = docType.DocTypeId;
                        _docTypeTimeJobService.Create(timeJob);
                    }

                    var scheduler = new DocTypeScheduler();

                    scheduler.RunSchedule(finalDocTypes.Select(d => d.DocTypeId), timeJob);
                }
                // 20200513 SuBD END
                // HuyNP - 16.7.2020 - Task 1 - Màn hình sau lưu báo cáo thuyết minh không nhảy sang màn chi tiết mà chuyển sang trang danh sách-START
                return Redirect("/Admin/DocType/EditExplicit/" + finalDocTypes.FirstOrDefault().DocTypeId.ToString());
                // HuyNP - 16.7.2020 - Task 1 - Màn hình sau lưu báo cáo thuyết minh không nhảy sang màn chi tiết mà chuyển sang trang danh sách-END
            }
            else
            {
                LoadTemplateAndReportKeys();
            }

            LoadDropDownList();
            ReBindDataWhenError();
            PrepareFormModel();
            BindData(sModel.TimeJob.ScheduleTypeEnum);
            return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
        }

        public ActionResult EditExplicit(Guid id)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
            }

            DocType docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            if (docType == null)
            {
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
            }

            var checkedStoreIds = _docTypeService.GetStoreIds(docType.DocTypeId);
            var storeIdDefault = _docTypeService.GetStoreIdDefault(docType.DocTypeId);
            ReBindDataWhenError(docType.CategoryBusinessId, checkedStoreIds, storeIdDefault);
            if (!docType.IsAllowOnline.HasValue)
            {
                docType.IsAllowOnline = false;
            }

            ViewBag.DoctypeId = id;
            ViewBag.StoreIds = checkedStoreIds;

            LoadDropDownList(docType.LevelId, docType.OfficeId, docType.ActionLevel);
            var model = docType.ToModel();
            model.StoreIdDefault = storeIdDefault;

            var sModel = new DocTypeFormModel();
            sModel.DocType = model;
            sModel.Form = GetFormWith(model);

            BindFormGroupAndFormType(sModel.Form.FormGroupId, sModel.Form.FormTypeId, sModel.Form.DocTypeId);
            ViewBag.HasTmp = sModel.Form.IsActivated == 3;

            LoadTemplateAndReportKeys();

            // 20200210 VuHQ END Phase 2 - REQ-0

            // 20200513 SuBD START
            var timeJob = _docTypeTimeJobService.Get(id);
            sModel.TimeJob = timeJob?.ToModel() ?? new DocTypeTimeJobModel(id);

            BindData(sModel.TimeJob.ScheduleTypeEnum);
            // 20200513 SuBD END
            // HuyNP - 16.7.2020 - Task 2 - Sửa tab chi tiết ở báo cáo thuyết minh và báo cáo số liệu hiện thị sai
            ViewBag.CategoryBusinessId = sModel.DocType.CategoryBusinessId;
            return View(sModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeEdit")]
        public ActionResult EditExplicit(DocTypeFormModel sModel)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
            }

            LoadDropDownList();
            PrepareFormModel();

            var model = sModel.DocType;

            LoadDropDownList(model.LevelId, model.OfficeId, model.ActionLevel);

            if (ModelState.IsValid)
            {
                DocType docType = _docTypeService.Get(model.DocTypeId);
                if (docType == null)
                    return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
                string oldDocTypeName = docType.DocTypeName;
                string oldDocTypeCode = docType.DocTypeCode;
                docType.LastModifiedByUserId = User.GetUserId();
                docType.LastModifiedOnDate = DateTime.Now;
                model.Unsigned = ConverToUnsign(model.DocTypeName);
                model.DocTypePermission = 0;
                model.TotalViewed = 1;
                model.TotalRegisted = 1;
                model.DocTypeLaws = docType.DocTypeLaws;
                model.IsAllowOnline = true;
                model.CategoryBusinessId = CATEGORY_BUSINESS_EXPLICIT;
                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        model.DocTypePermission |= per;
                    }
                }
                try
                {
                    docType = model.ToEntity(docType);
                    _docTypeService.Update(docType, oldDocTypeName, oldDocTypeCode);
                    _docTypeService.UpdateStoreIdDefault(docType, model.StoreIdDefault);

                    // 20200513 SuBD START
                    var timeJob = _docTypeTimeJobService.Get(docType.DocTypeId);
                    var timeJobModel = sModel.TimeJob;
                    var timeJobEntity = timeJobModel.ToEntity();
                    if (timeJob != null)
                    {
                        timeJob.IsActive = timeJobEntity.IsActive;
                        timeJob.ScheduleType = timeJobEntity.ScheduleType;
                        timeJob.ScheduleConfig = timeJobEntity.ScheduleConfig;
                        _docTypeTimeJobService.Update(timeJob);
                    }
                    else
                    {
                        _docTypeTimeJobService.Create(timeJobEntity);
                    }

                    var scheduler = new DocTypeScheduler();
                    scheduler.StopSchedule(docType.DocTypeId);

                    if (timeJobEntity.IsActive)
                    {
                        scheduler.RunSchedule(new[] { docType.DocTypeId }, timeJobEntity);
                    }
                    // 20200513 SuBD END
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(ex.Message);
                    BindData(sModel.TimeJob.ScheduleTypeEnum);
                    return View(sModel);
                }

                sModel.DocType = model;
                string msg = string.Empty;

                if (!EditForm(sModel, null, out msg, false))
                {
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(msg);
                    BindData(sModel.TimeJob.ScheduleTypeEnum);
                    return View(sModel);
                }

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Updated"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Updated"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
            }
            else
            {
                LoadTemplateAndReportKeys();
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Updated"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_EXPLICIT });
            }

            ReBindDataWhenError(model.CategoryBusinessId);
            BindData(sModel.TimeJob.ScheduleTypeEnum);
            return View(sModel);
        }

        #endregion

        #region Loại báo cáo danh sách
        [HttpPost]
        public JsonResult GetCategoryTemplateKey()
        {
            return Json(new { success = true, result = GetTreeTemplateKey() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetExplicitTemplate(List<int> values)
        {
            if (values == null || !values.Any())
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            var data = _reportKeyService.GetsAs(c => new { c.ReportKeyId, c.Name }, c => values.Contains(c.ReportKeyId)).Select(c => new DocTypeReportKey { Id = c.ReportKeyId, Name = c.Name });
            return Json(new { success = true, result = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateReport()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
            }

            LoadDropDownList();
            ReBindDataWhenError(4);
            PrepareFormModel();

            var sModel = new DocTypeFormModel();
            sModel.DocType = new DocTypeModel() { IsActivated = true, IsAllowOnline = true };
            sModel.Form = new FormModel();

            // 20200210 VuHQ START Phase 2 - REQ-0
            // Load TemplateKey
            int totalRecords = 0;
            var templateKeys = _templateKeyService.GetsAs(out totalRecords, t => new TemplateKeyModel
            {
                TemplateKeyId = t.TemplateKeyId,
                Name = t.Name,
                Code = t.Code,
                IsActive = t.IsActive
            }, pageSize: null,
                sortBy: "Name",
                isDescending: false,
                keySearch: "",
                currentPage: 1,
                type: 4); // TemplateKey cho báo cáo tường minh có type = 4

            ViewBag.TemplateKeys = templateKeys;





            var reportKeys = _reportKeyService.GetsAs(d => new { d.ReportKeyId, d.Name }).Select(d => new SelectListItem()
            {
                Text = d.Name,
                Value = d.ReportKeyId.ToString()
            });

            ViewBag.ReportKeys = reportKeys;

            // 20200210 VuHQ END Phase 2 - REQ-0

            return View(sModel);
        }

        [HttpPost]
        public JsonResult GetDataHeaderOrFooterTemplate(string templateKeyName, Guid formId, int timeKey)
        {
            if (formId == new Guid())
                return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            var form = _formService.Get(formId);
            var templateKeyNameDecode = HttpUtility.HtmlDecode(templateKeyName);
            if (string.IsNullOrWhiteSpace(templateKeyNameDecode))
                return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            templateKeyNameDecode = templateKeyNameDecode.Replace("@@", "").Replace("@@", "");
            var templateKey = _templateKeyService.Get(p => p.Name.Equals(templateKeyNameDecode));
            if (templateKey == null) return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            const string pattern = @"@([^=<> ')(\s]+)(|(?=\s)|$)";
            // Create a Regex
            var regex = new Regex(pattern);
            var match = regex.Match(templateKey.Sql);
            var paramTemplateKeys = new List<object>();
            while (match.Success)
            {
                paramTemplateKeys.Add(new SqlParameter(match.Groups[0].Value, timeKey));
                break;
            }
            var arrPara = paramTemplateKeys.ToArray();
            IEnumerable<dynamic> list = _templateKeyService.GetListByQuery(templateKey, arrPara).ToList();
            if (templateKey.Type == Convert.ToInt32(TemplateKeysType.Display))
                return Json(new
                {
                    Success = true,
                    result = list,
                    ExpiciteTemplate = form.ExplicitTemplate,
                    Type = 8,
                    HtmlTemplate = templateKey.HtmlTemplate
                }, JsonRequestBehavior.AllowGet);
            if (templateKey.Type != Convert.ToInt32(TemplateKeysType.Query))
                return Json(new { Success = true, ExpiciteTemplate = form.ExplicitTemplate }, JsonRequestBehavior.AllowGet);
            if (!list.Any())
                return Json(new { Success = true, ExpiciteTemplate = form.ExplicitTemplate }, JsonRequestBehavior.AllowGet);
            var keyValuePair = new Dictionary<string, object>(list.ToArray()[0]).FirstOrDefault(x => x.Key == templateKeyNameDecode);
            if (keyValuePair.Key == null)
                return Json(new { Success = false, ExpiciteTemplate = form.ExplicitTemplate }, JsonRequestBehavior.AllowGet);
            var obj = new TemplateKeyResult
            {
                Success = true,
                ExpiciteTemplate = form.ExplicitTemplate,
                HtmlTemplate = templateKey.HtmlTemplate,
                Type = 4,
                NewValue = keyValuePair.Value?.ToString()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<TemplateKeyTree> GetTreeTemplateKey()
        {
            var enumValArray = _templateKeyCategoryService.Gets();
            foreach (var val in enumValArray)
            {
                var obj = new TemplateKeyTree()
                {
                    CategoryId = val.Id,
                    CategoryName = val.Name,
                    ChidrenList = _templateKeyService.Gets(x => x.CategoryId == val.Id).Select(c => new ChidrenTemplate
                    { TemplateKeyId = c.TemplateKeyId, Code = c.Code, Name = c.Name })
                };
                yield return obj;
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateReport(DocTypeFormModel sModel)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
            }

            //lấy ra model dạng DocType sau khi thay đổi cơ chế post ==> logic về như cũ
            var model = sModel.DocType;

            if (ModelState.IsValid)
            {

                DocType docTypeCreate = model.ToEntity();
                docTypeCreate.CreatedByUserId = User.GetUserId();
                docTypeCreate.CreatedOnDate = DateTime.Now;
                docTypeCreate.LastModifiedOnDate = DateTime.Now;
                docTypeCreate.DocTypePermission = 0;
                docTypeCreate.TotalRegisted = 1; //eGovOnline
                docTypeCreate.TotalViewed = 1; //eGovOnline
                docTypeCreate.Unsigned = ConverToUnsign(model.DocTypeName); //eGovOnline

                //Báo cáo danh sáchh có categoryBusinessId = 16
                docTypeCreate.CategoryBusinessId = CATEGORY_BUSINESS_REPORT;
                docTypeCreate.IsAllowOnline = true;

                var finalDocTypes = new List<DocType>();
                var finalForm = new Form();

                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        docTypeCreate.DocTypePermission |= per;
                    }
                }
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.DocTypeName.Split(';').Distinct();
                        var list = new List<DocType>();
                        foreach (var name in names)
                        {
                            var item = docTypeCreate.Clone();
                            item.DocTypeName = name;
                            list.Add(item);
                            //Chuẩn bị trước key để tạo liên kết giữa báo cáo và biểu mẫu
                        }
                        finalDocTypes = _docTypeService.CreateNReturn(list, model.IgnoreExist);
                    }
                    else
                    {
                        finalDocTypes = new List<DocType> { _docTypeService.CreateNReturn(docTypeCreate) };
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Created"));
                    SuccessNotification(_resourceService.GetResource("Customer.DocType.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                    LoadDropDownList();
                    ReBindDataWhenError(docTypeCreate.CategoryBusinessId);

                    PrepareFormModel();
                    sModel = new DocTypeFormModel();
                    sModel.DocType = new DocTypeModel { IsActivated = true, IsAllowOnline = true };
                    sModel.Form = new FormModel();
                    return View(sModel);
                }

                ModelState.Clear();
                LoadDropDownList();
                ReBindDataWhenError();
                ViewBag.StoreIds = model.StoreIds;
                var modelCreate = new DocTypeModel
                {
                    IsActivated = true,
                    IsAllowOnline = true,
                    CategoryId = model.CategoryId,
                    CategoryBusinessId = model.CategoryBusinessId,
                    DocFieldId = model.DocFieldId, //eGovOnline
                    OfficeId = model.OfficeId, //eGovOnline
                    LevelId = model.LevelId, //eGovOnline
                    Content = model.Content, //eGovOnline
                    ActionLevel = model.ActionLevel, //eGovOnline
                    DocTypeName = model.DocFieldName,
                    CompendiumDefault = model.CompendiumDefault,
                    DocTypePermission = docTypeCreate.ToModel().DocTypePermission,
                };
                sModel.Form.ExplicitTemplate = JsonConvert.SerializeObject(sModel.Form.ReportKeyId);
                var finalForms = CreateFrom(sModel);
                sModel.DocType = modelCreate;

                foreach (Form form in finalForms)
                {
                    foreach (DocType docType in finalDocTypes)
                    {
                        ChangeIsPrimary(form.FormId, docType.DocTypeId, true);
                    }
                }
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
            }

            LoadDropDownList();
            ReBindDataWhenError();
            PrepareFormModel();
            return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
        }

        public ActionResult EditReport(Guid id)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
            }

            DocType docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            if (docType == null)
            {
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
            }

            var checkedStoreIds = _docTypeService.GetStoreIds(docType.DocTypeId);
            var storeIdDefault = _docTypeService.GetStoreIdDefault(docType.DocTypeId);
            ReBindDataWhenError(docType.CategoryBusinessId, checkedStoreIds, storeIdDefault);
            if (!docType.IsAllowOnline.HasValue)
            {
                docType.IsAllowOnline = false;
            }

            ViewBag.DoctypeId = id;
            ViewBag.StoreIds = checkedStoreIds;

            LoadDropDownList(docType.LevelId, docType.OfficeId, docType.ActionLevel);
            var model = docType.ToModel();
            model.StoreIdDefault = storeIdDefault;

            var sModel = new DocTypeFormModel();
            sModel.DocType = model;
            sModel.Form = GetFormWith(model);

            BindFormGroupAndFormType(sModel.Form.FormGroupId, sModel.Form.FormTypeId, sModel.Form.DocTypeId);
            ViewBag.HasTmp = sModel.Form.IsActivated == 3;

            // 20200210 VuHQ START Phase 2 - REQ-0
            // Load TemplateKey
            int totalRecords = 0;
            var templateKeys = _templateKeyService.GetsAs(out totalRecords, t => new TemplateKeyModel
            {
                id = t.TemplateKeyId,
                Name = t.Name,
                Code = t.Code,
                IsActive = t.IsActive
            }, pageSize: null,
                sortBy: "Name",
                isDescending: false,
                keySearch: "",
                currentPage: 1,
                type: 4); // TemplateKey cho báo cáo tường minh có type = 4
            var reportKeys = _reportKeyService.GetsAs(d => new { d.ReportKeyId, d.Name }).Select(d => new SelectListItem()
            {
                Text = d.Name,
                Value = d.ReportKeyId.ToString()
            });

            ViewBag.ReportKeys = reportKeys;
            ViewBag.TemplateKeys = templateKeys;

            //totalRecords = 0;
            //var result = _statisticService.GiamSatTong_SoVanBanDi(model.StoreId, model.From, model.To, model.GroupBy);

            ViewBag.Statistics = templateKeys;

            // 20200210 VuHQ END Phase 2 - REQ-0

            return View(sModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeEdit")]
        public ActionResult EditReport(DocTypeFormModel sModel)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
            }

            LoadDropDownList();
            ReBindDataWhenError(4);
            PrepareFormModel();
            sModel.Form.ExplicitTemplate = JsonConvert.SerializeObject(sModel.Form.ReportKeyId);
            var model = sModel.DocType;

            LoadDropDownList(model.LevelId, model.OfficeId, model.ActionLevel);

            if (ModelState.IsValid)
            {
                DocType docType = _docTypeService.Get(model.DocTypeId);
                if (docType == null)
                    return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
                string oldDocTypeName = docType.DocTypeName;
                string oldDocTypeCode = docType.DocTypeCode;
                docType.LastModifiedByUserId = User.GetUserId();
                docType.LastModifiedOnDate = DateTime.Now;
                model.Unsigned = ConverToUnsign(model.DocTypeName);
                model.DocTypePermission = 0;
                model.TotalViewed = 1;
                model.TotalRegisted = 1;
                model.DocTypeLaws = docType.DocTypeLaws;
                model.IsAllowOnline = true;
                model.CategoryBusinessId = CATEGORY_BUSINESS_REPORT;
                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        model.DocTypePermission |= per;
                    }
                }
                try
                {
                    docType = model.ToEntity(docType);
                    _docTypeService.Update(docType, oldDocTypeName, oldDocTypeCode);
                    _docTypeService.UpdateStoreIdDefault(docType, model.StoreIdDefault);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(ex.Message);
                    return View(sModel);
                }

                sModel.DocType = model;
                string msg = string.Empty;

                if (!EditForm(sModel, null, out msg,false))
                {
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(msg);
                    return View(sModel);
                }

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Updated"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Updated"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_REPORT });
            }


            var reportKeys = _reportKeyService.GetsAs(d => new { d.ReportKeyId, d.Name }).Select(d => new SelectListItem()
            {
                Text = d.Name,
                Value = d.ReportKeyId.ToString()
            });

            ViewBag.ReportKeys = reportKeys;
            ReBindDataWhenError(model.CategoryBusinessId);
            return View(sModel);
        }

        [HttpPost]
        public ActionResult ChangeIsActivateBatch(string sModel, int? categoryBusinessId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive"));
                return RedirectToAction("Index");
            }

            var list = JsonConvert.DeserializeObject<Dictionary<Guid, bool>>(sModel);
            foreach (var item in list)
            {
                var docType = _docTypeService.Get(item.Key);
                if (docType == null) continue;
                docType.IsActivated = item.Value;

                _docTypeService.Update(docType, docType.DocTypeName, docType.DocTypeCode);
            }
            
            return RedirectToAction("Index", new { categoryBusinessId });
        }
        #endregion

        #region Private Method

        private void LoadTemplateAndReportKeys()
        {
            // 20200210 VuHQ START Phase 2 - REQ-0
            // Load TemplateKey
            int totalRecords = 0;
            var queryTemplateKeys = _templateKeyService.GetsAs(out totalRecords, t => new TemplateKeyModel
            {
                TemplateKeyId = t.TemplateKeyId,
                Name = t.Name,
                Code = t.Code,
                IsActive = t.IsActive
            }, pageSize: null,
                sortBy: "Name",
                isDescending: false,
                keySearch: "",
                currentPage: 1,
                type: 4); // TemplateKey cho báo cáo tường minh có type = 4

            ViewBag.QueryTemplateKeys = queryTemplateKeys.Select(d => new SelectListItem()
            {
                Text = d.Code,
                Value = d.TemplateKeyId.ToString()
            }); ;

            var htmlTemplateKeys = _templateKeyService.GetsAs(out totalRecords, t => new TemplateKeyModel()
            {
                TemplateKeyId = t.TemplateKeyId,
                Name = t.Name,
                Code = t.Code,
                IsActive = t.IsActive
            }, pageSize: null,
                sortBy: "Name",
                isDescending: false,
                keySearch: "",
                currentPage: 1,
                type: 8); // TemplateKey truy vấn hiển thị html

            ViewBag.HtmlTemplateKeys = htmlTemplateKeys.Select(d => new SelectListItem()
            {
                Text = d.Code,
                Value = d.TemplateKeyId.ToString()
            });

            var reportKeys = _reportKeyService.GetsAs(d => new { d.ReportKeyId, d.Name }).Select(d => new SelectListItem()
            {
                Text = d.Name,
                Value = d.ReportKeyId.ToString()
            });

            ViewBag.ReportKeys = reportKeys;

            // 20200210 VuHQ END Phase 2 - REQ-0
        }

        private IEnumerable<SelectListItem> TimeType()
        {
            return new List<SelectListItem>{
                     new SelectListItem{Value="0",Text="Ngày"},
                     new SelectListItem{Value="1",Text="Giờ"}
                };
        }

        private string ConverToUnsign(string text)
        {
            text = text.ToUpper();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                text = text.Replace(convert[i], To[i]);
            }
            return text;
        }

        private void LoadDropDownList(int? levelId = 0, int? officeId = 0, int? type = 0, Guid? formId = null)
        {
            ViewBag.ListActionLevel = GetActionLevel();
            ViewBag.ListOffice = new SelectList(_officeService.GetOfficesByLevelId(levelId.HasValue ? (int)levelId : 0), "OfficeId", "OfficeName", officeId);
            ViewBag.ListLevel = _resourceService.EnumToSelectList<LevelType>(levelId);

            // 20200213 VuHQ Phase 2 REQ-0 START
            Expression<Func<Form, bool>> spec = null;

            Form form = null;
            if (formId.HasValue)
            {
                form = _formService.Get((Guid)formId);
                spec = p => p.FormId != formId;
            }

            ViewBag.ListForm = _formService.GetsAs(d => new { d.FormId, d.FormName }, spec).Select(d => new SelectListItem()
            {
                Text = d.FormName,
                Value = d.FormId.ToString(),
                Selected = form != null && form.FormId.Equals(d.FormId)
            });
            // 20200213 VuHQ Phase 2 REQ-0 END
        }

        private List<SelectListItem> GetActionLevel()
        {
            return _actionLevelService.GetsAs(a => new SelectListItem { Text = a.ActionLevelName, Value = a.ActionLevelCode }).ToList();
            /* return new List<SelectListItem>() {
                new SelectListItem() {Text = "Năm", Value = "1" },
                new SelectListItem() {Text = "6 Tháng", Value = "2" },
                new SelectListItem() {Text = "Quý", Value = "3" },
                new SelectListItem() {Text = "Tháng", Value = "4" },
                new SelectListItem() {Text = "Tuần", Value = "5" },
                new SelectListItem() {Text = "Ngày", Value = "6" },
                new SelectListItem() {Text = "Khẩn cấp", Value = "7" },
                new SelectListItem() {Text = "9 Tháng", Value = "8" },
            }; */
        }
        private Dictionary<string, string> GetActionLevelKey()
        {
            var _dict = new Dictionary<string, string>();
            _dict.Add("yearkey", "1");
            _dict.Add("halfkey", "2");
            _dict.Add("quarterkey", "3");
            _dict.Add("monthkey", "4");
            _dict.Add("weekkey", "5");
            _dict.Add("datekey", "6");
            _dict.Add("minutekey", "7");
            _dict.Add("ninekey", "8");

            return _dict;
        }

        private IEnumerable<CategoryModel> GetAllCategorys(int? categoryBusinessId = null)
        {
            if (categoryBusinessId == null)
            {
                return _categoryService.GetsAs(c => new CategoryModel { CategoryId = c.CategoryId, CategoryName = c.CategoryName }).OrderBy(t => t.CategoryName);
            }

            return
                _categoryService.GetsAs(
                    c => new CategoryModel { CategoryId = c.CategoryId, CategoryName = c.CategoryName },
                    t =>
                        ((CategoryBusinessTypes)t.CategoryBusinessId & (CategoryBusinessTypes)categoryBusinessId) ==
                        (CategoryBusinessTypes)categoryBusinessId).
                    OrderBy(t => t.CategoryName);
        }

        private IEnumerable<DocFieldModel> GetAllDocFields(int? categoryBusinessId = null)
        {
            if (categoryBusinessId == null)
            {
                return
                    _docFieldService.GetsAs(
                        d => new DocFieldModel { DocFieldId = d.DocFieldId, DocFieldName = d.DocFieldName })
                        .OrderBy(t => t.DocFieldName);
            }

            return
                _docFieldService.GetsAs(d => new DocFieldModel { DocFieldId = d.DocFieldId, DocFieldName = d.DocFieldName },
                    t =>
                    ((CategoryBusinessTypes)t.CategoryBusinessId & (CategoryBusinessTypes)categoryBusinessId) == (CategoryBusinessTypes)categoryBusinessId).
                    OrderBy(t => t.DocFieldName);
        }

        private IEnumerable<CodeModel> GetAllCodes()
        {
            return _codeService.GetsAs(c => new CodeModel { CodeId = c.CodeId, Template = c.Template });
        }

        private string GetAllJobTitless()
        {
            return
                _positionService.GetCacheAllPosition().Select(
                    u => new { value = u.PositionId, label = u.PositionName }).StringifyJs();
        }

        private void ReBindDataWhenError(int? categoryBusinessId = null, List<int> storeIdCheckeds = null, int storeIdDefault = 0)
        {
            ViewBag.AllCategoryBusiness = BindCategoryBusiness(categoryBusinessId);
            ViewBag.AllReportModel = _reportModeService.Gets().ToList();
            ViewBag.AllCategorys = GetAllCategorys(categoryBusinessId == null ? (int)CategoryBusinessTypes.VbDen : categoryBusinessId.Value);

            var stores = _storeService.GetsAs(s => new StoreModel { StoreId = s.StoreId, StoreName = s.StoreName, Checked = false }).OrderByDescending(s => s.Checked).ThenBy(s => s.StoreName);
            if (storeIdCheckeds != null && storeIdCheckeds.Any())
            {
                foreach (var store in stores)
                {
                    if (storeIdCheckeds.Contains(store.StoreId))
                    {
                        store.Checked = true;
                        if (storeIdDefault != 0 && store.StoreId == storeIdDefault)
                        {
                            store.IsDefault = true;
                        }
                    }
                }
            }
            //stores.OrderBy(s => s.Checked).OrderByDescending(s => s.StoreName).ToList();

            ViewBag.AllStores = stores;

            ViewBag.AllDocFields = GetAllDocFields(categoryBusinessId == null ? (int)CategoryBusinessTypes.VbDen : categoryBusinessId.Value);
            ViewBag.AllCodes = GetAllCodes();
        }

        private List<SelectListItem> BindCategoryBusiness(int? categoryBusinessId = null)
        {
            var result = _resourceService.EnumToSelectList<CategoryBusinessTypes>(categoryBusinessId);
            return result;
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

        private IEnumerable<DocTypeModel> GetDocTypeModels(DocTypeSearchModel search, string sortBy,
             bool isSortDesc, int page, int pageSize, int? categoryBusinessId = null)
        {
            var allForm = _formService.Gets(null);
            var allFormDoctype = _doctypeformService.Gets(null);
            int totalRecords;
            var model = _docTypeService.GetsAs(out totalRecords,
                d => new { d.DocTypeId, d.DocTypeName, d.DocTypeCode, d.IsActivated, d.DocField.DocFieldName, d.ActionLevel, d.CompendiumDefault },
                pageSize: pageSize,
                sortBy: sortBy,
                isDescending: isSortDesc,
                currentPage: page,
                docFieldId: search.DocFieldId,
                actionLevel: search.ActionLevel,
                isActivated: search.IsActivated,
                docTypeName: search.DocTypeName,
                docTypeCode: search.DocTypeCode,
                categoryBusinessId: categoryBusinessId)
                .Join(allFormDoctype, p => p.DocTypeId, pc => pc.DocTypeId, (p, pc) => new { p, pc })
                .Join(allForm, ppc => ppc.pc.FormId, c => c.FormId, (ppc, c) => new { ppc, c })
                .Select(t => new DocTypeModel
                {
                    DocTypeId = t.ppc.p.DocTypeId,
                    DocTypeName = t.ppc.p.DocTypeName,
                    DocTypeCode = t.ppc.p.DocTypeCode,
                    IsActivated = t.ppc.p.IsActivated,
                    DocFieldName = t.ppc.p.DocFieldName,
                    ActionLevel = t.ppc.p.ActionLevel,
                    CompendiumDefault = t.ppc.p.CompendiumDefault,
                    FormCategoryId = t.c.FormCategoryId
                });

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                IsSortDescending = isSortDesc,
                SortBy = sortBy,
                TotalRecordCount = totalRecords
            };

            CreateCookieSearch(search, sortAndPage, categoryBusinessId);

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;
            return model;
        }

        private void CreateCookieSearch(DocTypeSearchModel search, SortAndPagingModel sortpage, int? categoryBusinessId)
        {
            var data = new Dictionary<string, object> { { $"Search_{categoryBusinessId}", search }, { $"SortAndPaging_{categoryBusinessId}", sortpage } };
            HttpCookie cookie = Request.Cookies[CookieName.SearchDocType];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchDocType, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }

            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private IEnumerable<WorkflowModel> GetAllWorkflows()
        {
            return _workflowService.GetsAs(c => new WorkflowModel { WorkflowId = c.WorkflowId, WorkflowName = c.WorkflowName });
        }

        #endregion Private Method

        #endregion "Module Admin"

        #region Import

        public ActionResult ImportDocTypes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportDocTypes(HttpPostedFileBase importFile, string sheetName)
        {
            try
            {
                var now = DateTime.Now;
                if (importFile != null)
                {
                    var xlsxParser = new XlsxToJson(importFile.InputStream);
                    var json = xlsxParser.ConvertXlsxToJson(1, 2, 1, 1);
                    var jsonConvert = Newtonsoft.Json.JsonConvert.SerializeObject(json);
                    var listJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocTypeImport>>(jsonConvert);
                    string currentDocfield = "";
                    Workflow workFlow = null;
                    Store store = null;
                    DocField docField = null;

                    foreach (var item in listJson)
                    {
                        try
                        {
                            var doctypeName = item.doctypename;
                            if (string.IsNullOrEmpty(doctypeName) || doctypeName.Equals("Tên loại hồ sơ", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            var doctype = _docTypeService.Gets(d => d.DocTypeName.Equals(doctypeName, StringComparison.OrdinalIgnoreCase));
                            if (doctype.Any())
                            {
                                continue;
                            }

                            var docfieldName = item.docfieldname;

                            if (currentDocfield != docfieldName)
                            {
                                // Tạo lĩnh vực
                                docField = EnsureDocField(docfieldName);
                            }

                            // Tạo quy trình
                            // workFlow = CreateWorkflow(doctypeName);

                            // Lĩnh vực - quy trình
                            // _docFieldService.UpdateWorkflows(docField.DocFieldId, new List<int>() { workFlow.WorkflowId });

                            var storeCode = "$N$";

                            // Tạo sổ hồ sơ cho lĩnh vực
                            store = EnsureStore(doctypeName, storeCode);

                            var form = EnsureForm(doctypeName, item.docfieldname, item.formcode, item.fileName, item.filePath, item.configForm);

                            var level = item.level;
                            var levelValue = GetLevelValue(level);
                            CreateNewDoctype(doctypeName, docField, workFlow, form, store, levelValue, int.Parse(item.caphanhchinh));
                        }
                        catch (Exception ex)
                        {
                            LogException("Lỗi: RowNumber = " + item.docfieldname + ", Message = " + ex.Message);
                        }

                        // break; //test dòng đầu
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return View();
        }
        private int GetLevelValue(string level)
        {
            var levelValue = 1;
            if (!int.TryParse(level, out levelValue))
            {
                return 2;
            }
            if (levelValue < 0 || levelValue > 4)
            {
                return 2;
            }
            return levelValue;
        }

        private void CreateNewDoctype(string doctypeName, DocField docField, Workflow workflow, Form form, Store store, int level, int caphanhchinh)
        {
            if (docField == null || form == null)
            {
                return;
            }

            // Add sổ vào loại hồ sơ
            var storeIds = new List<int>() { store.StoreId };

            var doctype = new DocType()
            {
                DocTypeName = doctypeName,
                DocFieldId = docField.DocFieldId,
                CategoryBusinessId = 4,
                CategoryId = 1,
                ActionLevel = level,
                LevelId = caphanhchinh,
                CreatedByUserId = User.GetUserId(),
                CreatedOnDate = DateTime.Now,
                IsActivated = true,
                LastModifiedByUserId = User.GetUserId(),
                LastModifiedOnDate = DateTime.Now,
                StoreIds = string.Join(";", storeIds),
            };

            _docTypeService.Create(doctype);

            // thêm form
            _doctypeformService.Create(new DocTypeForm()
            {
                FormId = form.FormId,
                DocTypeId = doctype.DocTypeId,
                IsActive = true,
                IsPrimary = true
            });

            //var doctypeWorkflow = new DocfieldDoctypeWorkflow()
            //{
            //	DocFieldId = docField.DocFieldId,
            //	DocTypeId = doctype.DocTypeId,
            //	WorkflowId = workflow.WorkflowId
            //};

            //_workflowService.CreateDocFieldDocTypeWorkflow(new List<DocfieldDoctypeWorkflow>() { doctypeWorkflow });
        }

        private Form EnsureForm(string doctypeName, string docfield, string formCode, string fileName, string filePath, string configForm)
        {
            var formName = "Biểu mẫu " + " " + doctypeName;

            Form result;
            FormGroup formGroup;
            var formGroups = _formgroupService.Gets(d => d.FormGroupName.Equals(docfield, StringComparison.OrdinalIgnoreCase));
            if (!formGroups.Any())
            {
                formGroup = new FormGroup()
                {
                    FormGroupName = docfield
                };

                _formgroupService.Create(formGroup);
            }
            else
            {
                formGroup = formGroups.First();
            }

            // Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
            var forms = _formService.Gets(d => d.FormName.Equals(formName, StringComparison.OrdinalIgnoreCase) && d.FormTypeId == (int)Bkav.eGovCloud.Entities.FormType.DynamicForm);
            if (!forms.Any())
            {
                result = new Form()
                {
                    FormName = formName,
                    FormTypeId = (int)Bkav.eGovCloud.Entities.FormType.DynamicForm,
                    IsActivated = 1,
                    IsPrimary = true,
                    LastModifiedByUserId = User.GetUserId(),
                    LastModifiedOnDate = DateTime.Now,
                    CreatedByUserId = User.GetUserId(),
                    CreatedOnDate = DateTime.Now,
                    Description = "",
                    FormGroupId = formGroup.FormGroupId,
                    EmbryonicPath = fileName,
                    EmbryonicLocationName = filePath,
                    ConfigForm = configForm
                };

                _formService.Create(result);
            }
            else
            {
                result = forms.First();
            }
            return result;
        }

        private Store EnsureStore(string doctypeName, string codePattern)
        {
            if (string.IsNullOrWhiteSpace(doctypeName))
            {
                return null;
            }

            Store result;
            var storeName = "Sổ " + doctypeName;

            // Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
            var stores = _storeService.Gets(d => d.StoreName.Equals(storeName, StringComparison.OrdinalIgnoreCase));
            if (!stores.Any())
            {
                result = CreateNewStore(storeName, codePattern);
            }
            else
            {
                result = stores.First();
            }
            return result;
        }

        private Store CreateNewStore(string storeName, string codePattern)
        {
            var increasement = EnsureIncrease(storeName);
            var code = EnsureCode(storeName, codePattern, increasement.IncreaseId);
            var storeCode = new List<StoreCode>();
            storeCode.Add(new StoreCode() { CodeId = code.CodeId });
            var store = new Store()
            {
                StoreName = storeName,
                CategoryBusinessId = 4,
                StoreCodes = storeCode
            };
            _storeService.Create(store);

            return store;
        }

        private Code EnsureCode(string storeName, string codePattern, int increaseId)
        {
            var codeName = "Mẫu số " + storeName;
            Code result;
            // Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
            var codes = _codeService.GetsFromCache().Where(d => d.CodeName.Equals(codeName, StringComparison.OrdinalIgnoreCase));
            if (!codes.Any())
            {
                result = new Code()
                {
                    CodeName = codeName,
                    CreatedByUserId = User.GetUserId(),
                    CreatedOnDate = DateTime.Now,
                    LastModifiedByUserId = User.GetUserId(),
                    LastModifiedOnDate = DateTime.Now,
                    NumberLastest = 0,
                    Template = codePattern,
                    IncreaseId = increaseId,
                    BussinessDocFieldDocTypeGroupId = 3
                };

                _codeService.Create(result);

                var hsmcCategory = _categoryService.Get(16);
                if (hsmcCategory != null && hsmcCategory.CategoryName.Equals("Hồ sơ một cửa", StringComparison.OrdinalIgnoreCase))
                {
                    if (hsmcCategory.CodeIds == null)
                    {
                        hsmcCategory.CodeIds = new List<int>();
                    }
                    hsmcCategory.CodeIds.Add(result.CodeId);
                    _categoryService.Update(hsmcCategory, hsmcCategory.CategoryName);
                }
            }
            else
            {
                result = codes.First();
            }

            return result;
        }

        private Increase EnsureIncrease(string storeName)
        {
            var increaseName = "Nhảy số " + storeName;

            Increase result;
            // Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
            var incs = _increaseService.Gets(d => d.Name.Equals(increaseName, StringComparison.OrdinalIgnoreCase));
            if (!incs.Any())
            {
                result = new Increase()
                {
                    Name = increaseName,
                    BussinessDocFieldDocTypeGroupId = 3,
                    Value = 1
                };

                _increaseService.Create(result);
            }
            else
            {
                result = incs.First();
            }

            return result;
        }

        private Workflow CreateWorkflow(string doctypeName)
        {
            var workflowName = "Quy trình " + doctypeName;
            Workflow workflow;
            var workflows = _workflowService.Gets(w => w.WorkflowName.Equals(workflowName, StringComparison.OrdinalIgnoreCase));
            if (workflows.Any())
            {
                workflow = workflows.First();
                if (!workflow.IsActivated)
                {
                    workflow.IsActivated = true;
                    _workflowService.Update(workflow);
                }
            }
            else
            {
                workflow = new Workflow()
                {
                    IsActivated = true,
                    WorkflowName = workflowName,
                    CreatedByUserId = User.GetUserId(),
                    CreatedOnDate = DateTime.Now,
                    ExpireProcess = 240,
                    LastModifiedByUserId = User.GetUserId(),
                    LastModifiedOnDate = DateTime.Now,
                    Json = @"{""ID"":""210"",""NAME"":""Quy trình chuẩn hồ sơ một cửa"",""TYPE"":0,""ISACTIVATE"":false,""NODE"":[{""ID"":1,""NAME"":""Tiếp nhận"",""START"":true,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":120.0,""RIGHT"":280.0,""TOP"":55.0,""BOTTOM"":125.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""b06def57-5cf7-47c5-a40d-8e9f93b50bfb"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":1,""STARTLEFT"":200.0,""STARTTOP"":90.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""50c466b3-d390-4dc4-9d2a-5b0bf2a4f4d7"",""NAME"":""Chuyển cán bộ Tiếp nhận khác"",""NEXT"":1,""CURRENT"":1,""STARTLEFT"":200.0,""STARTTOP"":90.0,""ENDLEFT"":263.0,""ENDTOP"":116.0,""TYPE"":""Circle"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":18,""Type"":""TheoViTri""}]}]},{""ID"":2,""NAME"":""Thụ lý"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":true,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":577.0,""RIGHT"":737.0,""TOP"":56.0,""BOTTOM"":126.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""0cde30e1-abc3-435b-a3da-79dce42cdde3"",""NAME"":""Chuyển Lãnh đạo Phòng duyệt"",""NEXT"":3,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""a2818c74-e1be-4633-a01a-217bf8b70661"",""NAME"":""Chuyển Bộ phận Bổ sung"",""NEXT"":5,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":197.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""44a5fb43-f54d-4001-b893-700bfc363426"",""NAME"":""Chuyển cán bộ Tiếp nhận"",""NEXT"":1,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":200.0,""ENDTOP"":90.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""2965ce01-6502-408c-83e1-3001714acdef"",""NAME"":""Chuyển Liên thông"",""NEXT"":7,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":1004.0,""ENDTOP"":284.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":19,""Type"":""TheoViTri""}]}]},{""ID"":3,""NAME"":""Lãnh đạo phòng"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":true,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":572.0,""RIGHT"":732.0,""TOP"":248.0,""BOTTOM"":318.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""18677ce1-8b4c-4381-bf87-3c87ba2a276e"",""NAME"":""Chuyển Lãnh đạo Sở duyệt"",""NEXT"":4,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":653.0,""ENDTOP"":455.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""44192b73-7a1f-4130-bdd3-0cc15f20af15"",""NAME"":""Chuyển Bộ phận Bổ sung"",""NEXT"":5,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":197.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""e6c3db58-5299-4854-b65b-2a9feff955fc"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""7f698c85-7848-4956-97d9-db0ce955c391"",""NAME"":""Chuyển Liên thông"",""NEXT"":7,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":1004.0,""ENDTOP"":284.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""de3aca03-5416-4f12-b44b-283c120f98fb"",""NAME"":""Chuyển Lãnh đạo Phòng khác"",""NEXT"":3,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":711.0,""ENDTOP"":285.0,""TYPE"":""Circle"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":20,""Type"":""TheoViTri""}]}]},{""ID"":4,""NAME"":""Lãnh đạo Sở"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":true,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":573.0,""RIGHT"":733.0,""TOP"":420.0,""BOTTOM"":490.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""43f484ba-7471-40a5-8ae5-ccc3947733ae"",""NAME"":""Chuyển Trả kết Quả"",""NEXT"":6,""CURRENT"":4,""STARTLEFT"":653.0,""STARTTOP"":455.0,""ENDLEFT"":198.0,""ENDTOP"":453.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""0d1b194b-f383-4459-bb77-17d3cee83ac5"",""NAME"":""Chuyển Lãnh đạo Phòng"",""NEXT"":3,""CURRENT"":4,""STARTLEFT"":653.0,""STARTTOP"":455.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""18cc7e0d-fe0f-49e5-8163-a293b7ea0956"",""NAME"":""Chuyển Lãnh đạo Sở khác"",""NEXT"":4,""CURRENT"":4,""STARTLEFT"":653.0,""STARTTOP"":455.0,""ENDLEFT"":728.0,""ENDTOP"":451.0,""TYPE"":""Circle"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViHienTai"",""DepId"":409,""PositionId"":21,""Type"":""TheoViTri""}]}]},{""ID"":5,""NAME"":""Bổ sung"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":true,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":117.0,""RIGHT"":277.0,""TOP"":248.0,""BOTTOM"":318.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""04f82af0-b369-4a4c-82be-b84eacb9a7ce"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":5,""STARTLEFT"":197.0,""STARTTOP"":283.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""bfc305d9-b676-4045-8a38-672c5eb6d219"",""NAME"":""Chuyển Lãnh đạo Phòng"",""NEXT"":3,""CURRENT"":5,""STARTLEFT"":197.0,""STARTTOP"":283.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""9fb37f66-4536-49d8-9d1e-7d2e1d775c87"",""NAME"":""Chuyển Trả kết quả"",""NEXT"":6,""CURRENT"":5,""STARTLEFT"":197.0,""STARTTOP"":283.0,""ENDLEFT"":198.0,""ENDTOP"":453.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":22,""Type"":""TheoViTri""}]}]},{""ID"":6,""NAME"":""Trả kết quả"",""START"":false,""CLOSE"":true,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":true,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":118.0,""RIGHT"":278.0,""TOP"":418.0,""BOTTOM"":488.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""84681680-fc9a-4c04-9b54-09ca165088b7"",""NAME"":""Chuyển Lãnh đạo Sở"",""NEXT"":4,""CURRENT"":6,""STARTLEFT"":198.0,""STARTTOP"":453.0,""ENDLEFT"":653.0,""ENDTOP"":455.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""d5eb4ea1-9f00-4681-b4aa-a1c458ddc0b3"",""NAME"":""Chuyển Bộ phận Bổ sung"",""NEXT"":5,""CURRENT"":6,""STARTLEFT"":198.0,""STARTTOP"":453.0,""ENDLEFT"":197.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":23,""Type"":""TheoViTri""}]}]},{""ID"":7,""NAME"":""Liên thông"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":true,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":924.0,""RIGHT"":1084.0,""TOP"":249.0,""BOTTOM"":319.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""15152f97-e3bd-4dbd-ab1a-3b5f81a88ced"",""NAME"":""Chuyển Lãnh đạo Phòng"",""NEXT"":3,""CURRENT"":7,""STARTLEFT"":1004.0,""STARTTOP"":284.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""a2a21e76-caea-46d3-af63-b41cccde6f3e"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":7,""STARTLEFT"":1004.0,""STARTTOP"":284.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":19,""Type"":""TheoViTri""},{""Position"":""DonViHienTai"",""DepId"":409,""PositionId"":24,""Type"":""TheoViTri""}]}]}]}"
                };
                _workflowService.Create(workflow);
            }

            return workflow;
        }

        private DocField EnsureDocField(string docfieldName)
        {
            if (string.IsNullOrWhiteSpace(docfieldName))
            {
                return null;
            }

            DocField result;

            // Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
            var docfields = _docFieldService.Gets(d => d.DocFieldName.Equals(docfieldName, StringComparison.OrdinalIgnoreCase));
            if (!docfields.Any())
            {
                result = new DocField()
                {
                    DocFieldName = docfieldName,
                    CreatedByUserId = User.GetUserId(),
                    CreatedOnDate = DateTime.Now,
                    IsActivated = true,
                    LastModifiedOnDate = DateTime.Now,
                    LastModifiedByUserId = User.GetUserId(),
                    CategoryBusinessId = 4
                };

                _docFieldService.Create(result);
            }
            else
            {
                result = docfields.First();
                if (!result.IsActivated)
                {
                    result.IsActivated = true;
                    _docFieldService.Update(result, result.DocFieldName);
                }
            }

            return result;
        }

        #endregion

        #region Báo cáo tổng hợp
        /// <summary>
        /// Load lại dữ liệu jsonform của báo cáo tổng hợp khi chọn bảng của wso2
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGeneralCompilation(string tableName, Guid formId)
        {
            var strJsonFormMobile = string.Empty;

            if (string.IsNullOrEmpty(tableName))
                return Json(new { succress = false, data = new JArray() });

            var form = _formService.Get(formId);
            if (form != null)
                strJsonFormMobile = form.Json;

            var compilationMain = JsonConvert.SerializeObject(new { schema = generateJsonFormGeneralMain(strJsonFormMobile, tableName), form = new[] { "*" } });

            var compilationSuffix1Dict = generateJsonFormGeneralSuffix(tableName, true);
            var compilationSuffix1Str = "{\"Method\": {\"type\": \"array\", \"items\": {\"type\": \"object\", \"title\": \"Công thức {{idx}}\", \"properties\": " +
                JsonConvert.SerializeObject(compilationSuffix1Dict) + "}}}";

            var compilationSuffix2Dict = generateJsonFormGeneralSuffix(tableName, false);
            var compilationSuffix2Str = "{\"Query\": {\"type\": \"array\", \"items\": {\"type\": \"object\", \"title\": \"Truy vấn {{idx}}\", \"properties\": " +
                    JsonConvert.SerializeObject(compilationSuffix2Dict, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) + "}}}";
            var compilationSuffix1 = JsonConvert.SerializeObject(new { schema = JsonConvert.DeserializeObject(compilationSuffix1Str), form = new[] { "*" } });
            var compilationSuffix2 = JsonConvert.SerializeObject(new { schema = JsonConvert.DeserializeObject(compilationSuffix2Str), form = new[] { "*" } });

            return Json(new { succress = true, compilationMain = compilationMain, compilationSuffix1 = compilationSuffix1, compilationSuffix2 = compilationSuffix2 });
        }

        public ActionResult CreateGeneral()
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
            }
            LoadDropDownList();

            ReBindDataWhenError(CATEGORY_BUSINESS_GENERAL);
            PrepareFormModel();

            var sModel = new DocTypeFormModel();
            sModel.DocType = new DocTypeModel() { IsActivated = true, IsAllowOnline = true };
            sModel.Form = new FormModel();

            // Khi tạo mới báo cáo tổng hợp thì chỉ generate ra dropdown chọn table name của wso2
            sModel.Form.FormCodeCompilation = JsonConvert.SerializeObject(
                new
                {
                    compilationPrefix = new
                    {
                        schema = generateJsonFormGeneralPrefix(),
                        form = new[] { "*" }
                    }
                }
            );

            return View(sModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeCreate")]
        public ActionResult CreateGeneral(DocTypeFormModel sModel, GeneralCompilationDetail generalCompilationDetail)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
            }

            //lấy ra model dạng DocType sau khi thay đổi cơ chế post ==> logic về như cũ
            var model = sModel.DocType;

            if (ModelState.IsValid)
            {

                DocType docTypeCreate = model.ToEntity();
                docTypeCreate.CreatedByUserId = User.GetUserId();
                docTypeCreate.CreatedOnDate = DateTime.Now;
                docTypeCreate.LastModifiedOnDate = DateTime.Now;
                docTypeCreate.DocTypePermission = 0;
                docTypeCreate.TotalRegisted = 1;//eGovOnline
                docTypeCreate.TotalViewed = 1;//eGovOnline
                docTypeCreate.Unsigned = ConverToUnsign(model.DocTypeName);//eGovOnline
                                                                           //taida 10122019: 1 số giá trị default còn thiếu
                docTypeCreate.CategoryBusinessId = CATEGORY_BUSINESS_GENERAL;
                docTypeCreate.IsAllowOnline = true;

                var finalDocTypes = new List<DocType>();
                var finalForm = new Form();

                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        docTypeCreate.DocTypePermission |= per;
                    }
                }
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.DocTypeName.Split(';').Distinct();
                        var list = new List<DocType>();
                        foreach (var name in names)
                        {
                            var item = docTypeCreate.Clone();
                            item.DocTypeName = name;
                            list.Add(item);
                            //Chuẩn bị trước key để tạo liên kết giữa báo cáo và biểu mẫu
                        }
                        finalDocTypes = _docTypeService.CreateNReturn(list, model.IgnoreExist);
                    }
                    else
                    {
                        finalDocTypes = new List<DocType> { _docTypeService.CreateNReturn(docTypeCreate) };
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Created"));
                    SuccessNotification(_resourceService.GetResource("Customer.DocType.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                    LoadDropDownList();
                    ReBindDataWhenError(docTypeCreate.CategoryBusinessId);
                    PrepareFormModel();

                    return View(sModel);
                }

                ModelState.Clear();
                LoadDropDownList();
                ReBindDataWhenError(docTypeCreate.CategoryBusinessId);
                ViewBag.StoreIds = model.StoreIds;
                var modelCreate = new DocTypeModel
                {
                    IsActivated = true,
                    IsAllowOnline = true,
                    CategoryId = model.CategoryId,
                    CategoryBusinessId = model.CategoryBusinessId,
                    DocFieldId = model.DocFieldId,//eGovOnline
                    OfficeId = model.OfficeId,//eGovOnline
                    LevelId = model.LevelId,//eGovOnline
                    Content = model.Content,//eGovOnline
                    ActionLevel = model.ActionLevel,//eGovOnline
                    DocTypeName = model.DocFieldName,
                    CompendiumDefault = model.CompendiumDefault,
                    DocTypePermission = docTypeCreate.ToModel().DocTypePermission,
                };

                sModel.Form.GeneralCompilationDetail = generalCompilationDetail;
                var finalForms = CreateFrom(sModel);
                sModel.DocType = modelCreate;

                foreach (Form form in finalForms)
                {
                    foreach (DocType docType in finalDocTypes)
                    {
                        ChangeIsPrimary(form.FormId, docType.DocTypeId, true);
                    }
                }

                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
            }

            LoadDropDownList();
            ReBindDataWhenError();
            PrepareFormModel();
            return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
        }

        public ActionResult EditGeneral(Guid id)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
            }

            DocType docType = _docTypeService.Get(id);
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            if (docType == null)
            {
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
            }

            var checkedStoreIds = _docTypeService.GetStoreIds(docType.DocTypeId);
            var storeIdDefault = _docTypeService.GetStoreIdDefault(docType.DocTypeId);
            ReBindDataWhenError(docType.CategoryBusinessId, checkedStoreIds, storeIdDefault);
            if (!docType.IsAllowOnline.HasValue)
            {
                docType.IsAllowOnline = false;
            }

            ViewBag.DoctypeId = id;
            ViewBag.StoreIds = checkedStoreIds; // docType.DocTypeStores.Select(t => t.StoreId).ToList();

            var model = docType.ToModel();
            model.StoreIdDefault = storeIdDefault;

            var sModel = new DocTypeFormModel();
            sModel.DocType = model;
            sModel.Form = GetFormWith(model);

            LoadDropDownList(docType.LevelId, docType.OfficeId, docType.ActionLevel, sModel.Form.FormId);

            BindFormGroupAndFormType(sModel.Form.FormGroupId, sModel.Form.FormTypeId, sModel.Form.DocTypeId);
            ViewBag.HasTmp = sModel.Form.IsActivated == 3;

            return View(sModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeEdit")]
        public ActionResult EditGeneral(DocTypeFormModel sModel, FormCollection formCollection, GeneralCompilationDetail generalCompilationDetail)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
            }

            //prepare some data temp

            LoadDropDownList();
            ReBindDataWhenError(4);
            PrepareFormModel();

            var model = sModel.DocType;

            LoadDropDownList(model.LevelId, model.OfficeId, model.ActionLevel);

            if (ModelState.IsValid)
            {
                DocType docType = _docTypeService.Get(model.DocTypeId);
                if (docType == null)
                    return RedirectToAction("Index");
                string oldDocTypeName = docType.DocTypeName;
                string oldDocTypeCode = docType.DocTypeCode;
                docType.LastModifiedByUserId = User.GetUserId();
                docType.LastModifiedOnDate = DateTime.Now;
                model.Unsigned = ConverToUnsign(model.DocTypeName);
                model.DocTypePermission = 0;
                model.TotalViewed = 1;
                model.TotalRegisted = 1;
                model.DocTypeLaws = docType.DocTypeLaws;
                //10122019 taida: IsAllowOnline với biểu mẫu tạo từ báo cáo luôn là true
                model.IsAllowOnline = true;
                //10122019 taida: CategoryBusinessId với biểu mẫu tạo từ báo cáo luôn mặc định là 4
                model.CategoryBusinessId = CATEGORY_BUSINESS_GENERAL;
                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        model.DocTypePermission |= per;
                    }
                }
                try
                {
                    docType = model.ToEntity(docType);
                    _docTypeService.Update(docType, oldDocTypeName, oldDocTypeCode);
                    _docTypeService.UpdateStoreIdDefault(docType, model.StoreIdDefault);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(ex.Message);
                    return View(sModel);
                }

                sModel.DocType = model;
                string msg = string.Empty;

                sModel.Form.GeneralCompilationDetail = generalCompilationDetail;
                if (!EditForm(sModel, formCollection, out msg,false))
                {
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(msg);
                    return View(sModel);
                }

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Updated"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Updated"));
                return RedirectToAction("Index", new { categoryBusinessId = CATEGORY_BUSINESS_GENERAL });
            }
            ReBindDataWhenError(model.CategoryBusinessId);
            return View(sModel);
        }
        #endregion

        #region Clone tạo biểu mẫu
        public ActionResult CreatePlus()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            LoadDropDownList();
            ReBindDataWhenError(4);
            PrepareFormModel();
            var listReportMode =  _reportModeService.Gets().ToList();
            listReportMode.Insert(0, new ReportModes() { Name = "Chưa có", ReportModeId = 0 });
            ViewBag.AllReportModel = listReportMode;
            var sModel = new DocTypeFormModel();
            sModel.DocType = new DocTypeModel() { IsActivated = true, IsAllowOnline = true, ReportModeId = 0 };
            sModel.Form = new FormModel();

            // 20200317 HoangDX START
            sModel.TimeJob = new DocTypeTimeJobModel();
            BindData();
            // 20200317 HoangDX END

            // 20200217 VuHQ START
            var targetCompilations = generateConfigJsonForm(true, sModel.Form.Json);
            var targetConfigJsonForm = new { schema = targetCompilations, form = new[] { "*" } };

            var summaryCompilations = generateConfigJsonForm(false, sModel.Form.Json);
            var summaryConfigJsonForm = new { schema = summaryCompilations, form = new[] { "*" } };

            var FormCodeCompilation = JsonConvert.SerializeObject(new
            {
                targetConfigJsonForm = targetConfigJsonForm,
                summaryConfigJsonForm = summaryConfigJsonForm
            });

            sModel.Form.FormCategoryId = 1; // Loại báo cáo thường (không phải chỉ tiêu hoặc tổng hợp)
            sModel.Form.FormCodeCompilation = FormCodeCompilation;

            // 20200217 VuHQ END
            return View(sModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeCreate")]
        public ActionResult CreatePlus(DocTypeFormModel sModel)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            //lấy ra model dạng DocType sau khi thay đổi cơ chế post ==> logic về như cũ
            var model = sModel.DocType;

            if (ModelState.IsValid)
            {

                DocType docTypeCreate = model.ToEntity();
                docTypeCreate.CreatedByUserId = User.GetUserId();
                docTypeCreate.CreatedOnDate = DateTime.Now;
                docTypeCreate.LastModifiedOnDate = DateTime.Now;
                docTypeCreate.DocTypePermission = 0;
                docTypeCreate.TotalRegisted = 1;//eGovOnline
                docTypeCreate.TotalViewed = 1;//eGovOnline
                docTypeCreate.Unsigned = ConverToUnsign(model.DocTypeName);//eGovOnline
                                                                           //taida 10122019: 1 số giá trị default còn thiếu
                docTypeCreate.CategoryBusinessId = 4;
                docTypeCreate.IsAllowOnline = true;

                var finalDocTypes = new List<DocType>();
                var finalForm = new Form();

                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        docTypeCreate.DocTypePermission |= per;
                    }
                }
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.DocTypeName.Split(';').Distinct();
                        var list = new List<DocType>();
                        foreach (var name in names)
                        {
                            var item = docTypeCreate.Clone();
                            item.DocTypeName = name;
                            list.Add(item);
                            //Chuẩn bị trước key để tạo liên kết giữa báo cáo và biểu mẫu
                        }
                        finalDocTypes = _docTypeService.CreateNReturn(list, model.IgnoreExist);
                    }
                    else
                    {
                        finalDocTypes = new List<DocType> { _docTypeService.CreateNReturn(docTypeCreate) };
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Created"));
                    SuccessNotification(_resourceService.GetResource("Customer.DocType.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                    LoadDropDownList();
                    ReBindDataWhenError(docTypeCreate.CategoryBusinessId);

                    PrepareFormModel();

                    // 20200317 HoangDX START
                    var timeJob = sModel.TimeJob;

                    sModel = new DocTypeFormModel();
                    sModel.DocType = new DocTypeModel { IsActivated = true, IsAllowOnline = true };
                    sModel.Form = new FormModel();
                    sModel.TimeJob = timeJob;
                    BindData();
                    // 20200317 HoangDX END
                    return View(sModel);
                }

                ModelState.Clear();
                LoadDropDownList();
                ReBindDataWhenError(docTypeCreate.CategoryBusinessId);
                ViewBag.StoreIds = model.StoreIds;
                var modelCreate = new DocTypeModel
                {
                    IsActivated = true,
                    IsAllowOnline = true,
                    CategoryId = model.CategoryId,
                    CategoryBusinessId = model.CategoryBusinessId,
                    DocFieldId = model.DocFieldId,//eGovOnline
                    ReportModeId = model.ReportModeId,
                    OfficeId = model.OfficeId,//eGovOnline
                    LevelId = model.LevelId,//eGovOnline
                    Content = model.Content,//eGovOnline
                    ActionLevel = model.ActionLevel,//eGovOnline
                    DocTypeName = model.DocFieldName,
                    CompendiumDefault = model.CompendiumDefault,
                    DocTypePermission = docTypeCreate.ToModel().DocTypePermission,
                };

                var finalForms = CreateFrom(sModel);
                sModel.DocType = modelCreate;

                foreach (Form form in finalForms)
                {
                    foreach (DocType docType in finalDocTypes)
                    {
                        ChangeIsPrimary(form.FormId, docType.DocTypeId, true);
                    }
                }

                // 20200317 HoangDX START
                if (sModel.TimeJob != null && sModel.TimeJob.IsActive)
                {
                    var timeJob = sModel.TimeJob.ToEntity();
                    foreach (var docType in finalDocTypes)
                    {
                        timeJob.DocTypeId = docType.DocTypeId;
                        _docTypeTimeJobService.Create(timeJob);
                    }

                    var scheduler = new DocTypeScheduler();

                    scheduler.RunSchedule(finalDocTypes.Select(d => d.DocTypeId), timeJob);
                }
                // 20200317 HoangDX END
                return Redirect("/Admin/DocType/EditPlus/" + finalDocTypes.FirstOrDefault().DocTypeId.ToString());
            }

            LoadDropDownList();
            ReBindDataWhenError();
            PrepareFormModel();
            BindData(sModel.TimeJob.ScheduleTypeEnum);
            return RedirectToAction("Index");
        }

        private List<Form> CreateFrom(DocTypeFormModel sModel)
        {
            var finalForms = new List<Form>();

            // set data cho FormName và Description
            sModel.Form.FormName = sModel.DocType.DocTypeName;
            sModel.Form.Description = sModel.DocType.CompendiumDefault;

            var model = sModel.Form;
            var form = model.ToEntity();

            form.IsActivated = 1;
            form.FormTypeId = 2;
            form.IsPrimary = true; //model.IsPrimary;
            form.CreatedByUserId = User.GetUserId();
            var docFieldname = _docFieldService.Get(sModel.DocType.DocFieldId ?? 0).DocFieldName.TrimStart();
            form.FormGroupId = GetFormGroupIDWith(docFieldname);

            // 20200218 VuHQ START
            if (model.Compilation != null)
            {
                var targetConfigCompilations = generateConfigJsonForm(true, sModel.Form.Json);
                targetConfigCompilations.ElementAt(0).Value.text = model.Compilation.Field;
                targetConfigCompilations.ElementAt(1).Value.text = model.Compilation.Value;
                targetConfigCompilations.ElementAt(2).Value.text = model.Compilation.Match;
                targetConfigCompilations.ElementAt(3).Value.text = model.Compilation.Select;
                targetConfigCompilations.ElementAt(4).Value.text = model.Compilation.Display;

                var summaryConfigCompilations = generateConfigJsonForm(false, sModel.Form.Json);

                form.FormCodeCompilation = JsonConvert.SerializeObject(new
                {
                    targetConfigJsonForm = new { schema = targetConfigCompilations, form = new[] { "*" } },
                    summaryConfigJsonForm = new { schema = summaryConfigCompilations, form = new[] { "*" } }
                });
            }
            else if (model.GeneralCompilationHeader != null && !string.IsNullOrEmpty(model.GeneralCompilationHeader.TableName))
            {
                var compilationPrefix = generateJsonFormGeneralPrefix();
                compilationPrefix.ElementAt(0).Value.text = model.GeneralCompilationHeader.TableName;

                var compilationMain = generateJsonFormGeneralMain(model.Json, model.GeneralCompilationHeader.TableName);
                compilationMain.ElementAt(0).Value.text = model.GeneralCompilationHeader.Select;
                compilationMain.ElementAt(1).Value.text = model.GeneralCompilationHeader.Display;
                compilationMain.ElementAt(2).Value.text = model.GeneralCompilationHeader.Formula;

                // trường hợp đặc biệt START
                // binding dữ liệu vào jsonform type array START
                var compilationSuffix1Dict = generateJsonFormGeneralSuffix(model.GeneralCompilationHeader.TableName, true);
                var compilationSuffix2Dict = generateJsonFormGeneralSuffix(model.GeneralCompilationHeader.TableName, false);

                var compilationSuffix1Str = "{\"Method\": {\"type\": \"array\", \"items\": {\"type\": \"object\", \"title\": \"Công thức {{idx}}\", \"properties\": " +
                JsonConvert.SerializeObject(compilationSuffix1Dict) + "}}}";

                var compilationSuffix2Str = "{\"Query\": {\"type\": \"array\", \"items\": {\"type\": \"object\", \"title\": \"Truy vấn {{idx}}\", \"properties\": " +
                        JsonConvert.SerializeObject(compilationSuffix2Dict, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) + "}}}";
                // binding dữ liệu vào jsonform type array END

                form.FormCodeCompilation = JsonConvert.SerializeObject(new
                {
                    compilationPrefix = new { schema = compilationPrefix, form = new[] { "*" } },
                    compilationMain = new { schema = compilationMain, form = new[] { "*" } },
                    compilationSuffix1 = new
                    {
                        schema = JsonConvert.DeserializeObject(compilationSuffix1Str),
                        value = new
                        {
                            Method = model.GeneralCompilationDetail.Method
                        },
                        form = new[] { "*" }
                    },
                    compilationSuffix2 = new
                    {
                        schema = JsonConvert.DeserializeObject(compilationSuffix2Str),
                        value = new
                        {
                            Query = model.GeneralCompilationDetail.Query
                        },
                        form = new[] { "*" }
                    },
                });
            }
            // 20200218 VuHQ END

            try
            {
                if (model.HasCreatePacket)
                {
                    var names = model.FormName.Split(';').Distinct().ToList();
                    var list = new List<Form>();
                    var leng = names.Count();
                    for (var i = 0; i < leng; i++)
                    {
                        var item = form.Clone();
                        item.FormName = names[i];
                        if (item.IsActivated == 1 && item.IsPrimary)
                        {
                            if (i != leng - 1)
                            {
                                item.IsActivated = 3;
                            }
                        }

                        item.Template = "f_" + RandomHelper.RandomString(15, false);
                        list.Add(item);
                    }

                    finalForms = _formService.CreateNReturn(list);
                }
                else
                {
                    form.Template = "f_" + RandomHelper.RandomString(15, false);
                    finalForms = new List<Form>() { _formService.CreateNReturn(form) };
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Created"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Created"));
                // ViewData = null;
                BindFormGroupAndFormType(model.FormGroupId, model.FormTypeId, model.DocTypeId);
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }

            return finalForms;
        }

        public ActionResult EditPlus(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            DocType docType = _docTypeService.Get(id);
            var reportModelId = docType.ReportModeId;
            if (reportModelId == null)
            {
                var listReportMode = _reportModeService.Gets().ToList();
                listReportMode.Insert( 0, new ReportModes() { Name = "Chưa có", ReportModeId = 0 });
            }
            ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
            if (docType == null)
            {
                return RedirectToAction("Index");
            }

            var checkedStoreIds = _docTypeService.GetStoreIds(docType.DocTypeId);
            var storeIdDefault = _docTypeService.GetStoreIdDefault(docType.DocTypeId);
            ReBindDataWhenError(docType.CategoryBusinessId, checkedStoreIds, storeIdDefault);
            if (!docType.IsAllowOnline.HasValue)
            {
                docType.IsAllowOnline = false;
            }

            ViewBag.DoctypeId = id;
            ViewBag.StoreIds = checkedStoreIds; // docType.DocTypeStores.Select(t => t.StoreId).ToList();

            var model = docType.ToModel();
            model.StoreIdDefault = storeIdDefault;

            var sModel = new DocTypeFormModel();
            sModel.DocType = model;
            sModel.Form = GetFormWith(model);

            LoadDropDownList(docType.LevelId, docType.OfficeId, docType.ActionLevel, sModel.Form.FormId);

            BindFormGroupAndFormType(sModel.Form.FormGroupId, sModel.Form.FormTypeId, sModel.Form.DocTypeId);
            ViewBag.HasTmp = sModel.Form.IsActivated == 3;

            // 20191101 VuHQ REQ-02

            // 20190106 VuHQ START Khi edit, danh sách catalog và catalog detail được lưu trữ từ trước
            var catalogs = new List<string>();
            if (string.IsNullOrEmpty(sModel.Form.DefineConfigJson))
            {
                catalogs = _catalogService.Gets(c => c.IsActivated).Select(p => p.CatalogName).ToList();

                // add catalog mở rộng: Organization (danh sách các department trực thuộc)
                catalogs.Add("Organization");
            }
            else
            {
                dynamic defineConfigJson = JsonConvert.DeserializeObject(sModel.Form.DefineConfigJson);
                if (defineConfigJson.catalogDetail != null && defineConfigJson.catalogDetail.Count != 0)
                    catalogs = defineConfigJson.catalogDetail.ToObject<List<string>>();
                else
                {
                    catalogs = _catalogService.Gets(c => c.IsActivated).Select(p => p.CatalogName).ToList();

                    // add catalog mở rộng: Organization (danh sách các department trực thuộc)
                    catalogs.Add("Organization");
                }
            }
            // 20190106 VuHQ START Khi edit, danh sách catalog và catalog detail được lưu trữ từ trước


            ViewBag.Catalogs = JsonConvert.SerializeObject(catalogs);

            // 20200428 SuBD START
            var timeJob = _docTypeTimeJobService.Get(id);
            sModel.TimeJob = timeJob?.ToModel() ?? new DocTypeTimeJobModel(id);

            BindData(sModel.TimeJob.ScheduleTypeEnum);

            BindDataDueDate(sModel.TimeJob.ScheduleTypeEnumDueDate);

            BindDataOutOfDate(sModel.TimeJob.ScheduleTypeEnumOutOfDate);
            // 20200428 SuBD END

            // Load danh mục chỉ tiêu InCatalog START
            var inCatalogs = _inCatalogService.Gets(c => c.IsActivated).Select(p => p.InCatalogName).ToList();
            ViewBag.InCatalogs = JsonConvert.SerializeObject(inCatalogs);
            // Load danh mục chỉ tiêu InCatalog END

            // với trường hợp báo cáo số liệu - tổng hợp cũ
            // add và set mặc định match_off = true
            if (!string.IsNullOrEmpty(sModel.Form.FormCodeCompilation))
            {
                dynamic formCodeCompilation = JsonConvert.DeserializeObject(sModel.Form.FormCodeCompilation);
                var schema = (Dictionary<string, ConfigCompilation>)JsonConvert.DeserializeObject <Dictionary<string, ConfigCompilation>>(formCodeCompilation.summaryConfigJsonForm.schema.ToString());
                var tempSchema = new Dictionary<string, ConfigCompilation>();

                if (schema.Count() > 0 && !schema.ContainsKey("Form_Compilation_Match_Off"))
                {
                    tempSchema.Add(schema.ElementAt(0).Key, schema.ElementAt(0).Value);
                    tempSchema.Add("Form_Compilation_Match_Off", new ConfigCompilation { typeOther = "boolean", title = "Sử dụng so sánh", text = "true" });
                    for (var i = 0; i < schema.Count; i++)
                    {
                        if (i != 0)
                            tempSchema.Add(schema.ElementAt(i).Key, schema.ElementAt(i).Value);
                    }
                }

                formCodeCompilation = JsonConvert.SerializeObject(new
                {
                    targetConfigJsonForm = formCodeCompilation.targetConfigJsonForm,
                    summaryConfigJsonForm = new { schema = tempSchema.Count == 0 ? schema : tempSchema, form = new[] { "*" } }
                });

                sModel.Form.FormCodeCompilation = formCodeCompilation;

            }
            // HuyNP - 16.7.2020 - Task 2 - Sửa tab chi tiết ở báo cáo thuyết minh và báo cáo số liệu hiện thị sai
            ViewBag.CategoryBusinessId = sModel.DocType.CategoryBusinessId;


            //select list department
            var listDepartment = _departmentService.GetReadOnlys();
            var listSelect = new List<SelectListItem>();
            //listSelect.Add(new SelectListItem()
            //{
            //    Selected = true,
            //    Text = "Chưa chọn đơn vị",
            //    Value = ""
            //});
            foreach (var item in listDepartment)
            {
                listSelect.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.DepartmentId.ToString(),
                    Text = item.DepartmentName
                });
            }
            ViewBag.ListSelectDepartment = listSelect;
            //end select and list department

            return View(sModel);
        }

        /// <summary>
        /// Load danh sách giá trị danh mục chỉ tiêu trực thuộc theo tên giá trị danh mục chỉ tiêu (InCatalogValue)
        /// </summary>
        /// <param name="inCatalogValueName"></param>
        /// <returns></returns>
        public JsonResult LoadSubInCatalogValues(string inCatalogValueName)
        {
            //var subIncatalogValues = new List<string>();
            dynamic subIncatalogValues = string.Empty;
            var rootInCatalogValues = _inCatalogValueService.Gets(p => p.InCatalogValueName == inCatalogValueName).FirstOrDefault();
            if (rootInCatalogValues != null)
            {
                subIncatalogValues = _inCatalogValueService.Gets(p => p.ParentId == rootInCatalogValues.InCatalogValueId).Select(p => new
                {
                    name = p.InCatalogValueName,
                    level = p.Level
                }).ToList();
            }
            
            return Json(new
            {
                inCatalogValues = JsonConvert.SerializeObject(subIncatalogValues)
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInCatalogValues(string inCatalogName, int row, int col, string formId, bool isXoay, string[] catalogNames)
        {
            var inCatalogValues = new List<string>();
            var inCatalogValuesAscii = new List<string>();
            var inCatalogInfos = new List<InCatalogValue>();
            var departments = new List<KeyValuePair<string, string>>();

            bool isExistCatalog = false;

            var form = _formService.Get(Guid.Parse(formId));
            if (!isXoay && form != null && form.DefineConfigJson != null)
            {
                dynamic defineConfigJson = JsonConvert.DeserializeObject(form.DefineConfigJson);
                if (defineConfigJson.inCatalogDefaults != null)
                {
                    foreach (var item in defineConfigJson.inCatalogDefaults)
                    {
                        if (int.Parse(item["row"].ToString()) == row && int.Parse(item["col"].ToString()) == col)
                        {
                            inCatalogValues = item["source"].ToObject<List<string>>();
                            isExistCatalog = true;
                            break;
                        }
                    }
                }
            }

            if (!isExistCatalog)
            {
                var inCatalogModel = _inCatalogService.Gets(c => c.InCatalogName == inCatalogName);
                var enumerable = inCatalogModel as InCatalog[] ?? inCatalogModel.ToArray();
                if (enumerable.Any() && enumerable.First().InCatalogValues.Count > 0)
                    inCatalogValues = enumerable.First().InCatalogValues.OrderBy(p => p.Order).Select(p => p.InCatalogValueName).ToList();
            }

            foreach (var item in inCatalogValues)
            {
                inCatalogValuesAscii.Add(HandsonToJson.ConvertToAscii(item));
            }

            // 20200228 CatalogKey START
            inCatalogInfos = _inCatalogValueService.GetInCatalogValueDetails(inCatalogValues).ToList();
            // 20200228 CatalogKey END

            return Json(new
            {
                inCatalogValues = JsonConvert.SerializeObject(inCatalogValues),
                inCatalogValuesAscii = JsonConvert.SerializeObject(inCatalogValuesAscii),
                inCatalogInfos = JsonConvert.SerializeObject(inCatalogInfos),
                row = row
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInCatalogValues2(string inCatalogName, int row, int col, string formId, bool isXoay, string[] catalogNames)
        {
            var inCatalogValues = new List<string>();
            var inCatalogValuesAscii = new List<string>();
            var inCatalogInfos = new List<InCatalogValue>();
            var departments = new List<KeyValuePair<string, string>>();

            bool isExistCatalog = false;

            //var form = _formService.Get(Guid.Parse(formId));
            //if (!isXoay && form != null && form.DefineConfigJson != null)
            //{
            //    dynamic defineConfigJson = JsonConvert.DeserializeObject(form.DefineConfigJson);
            //    if (defineConfigJson.inCatalogDefaults != null)
            //    {
            //        foreach (var item in defineConfigJson.inCatalogDefaults)
            //        {
            //            if (int.Parse(item["row"].ToString()) == row && int.Parse(item["col"].ToString()) == col)
            //            {
            //                inCatalogValues = item["source"].ToObject<List<string>>();
            //                isExistCatalog = true;
            //                break;
            //            }
            //        }
            //    }
            //}

            if (!isExistCatalog)
            {
                var inCatalogModel = _inCatalogService.Gets(c => c.InCatalogName == inCatalogName);
                var enumerable = inCatalogModel as InCatalog[] ?? inCatalogModel.ToArray();
                if (enumerable.Any() && enumerable.First().InCatalogValues.Count > 0)
                    inCatalogValues = enumerable.First().InCatalogValues.OrderBy(p => p.Order).Select(p => p.InCatalogValueName).ToList();
            }

            foreach (var item in inCatalogValues)
            {
                inCatalogValuesAscii.Add(HandsonToJson.ConvertToAscii(item));
            }

            // 20200228 CatalogKey START
            inCatalogInfos = _inCatalogValueService.GetInCatalogValueDetails(inCatalogValues).ToList();
            // 20200228 CatalogKey END

            return Json(new
            {
                inCatalogValues = JsonConvert.SerializeObject(inCatalogValues),
                inCatalogValuesAscii = JsonConvert.SerializeObject(inCatalogValuesAscii),
                inCatalogInfos = JsonConvert.SerializeObject(inCatalogInfos),
                row = row
            }, JsonRequestBehavior.AllowGet);
        }
        private FormModel GetFormWith(DocTypeModel model)
        {
            var id = model.DocTypeId;
            var forms = _doctypeFormService.GetForms(d => new
            {
                d.Form.FormId,
                d.Form.FormTypeId,
                d.Form.FormGroupId,
                d.Form.FormName,
                d.Form.Description,
                d.Form.Json,
                d.Form.IsPrimary,
                d.Form.Template,
                d.Form.EmbryonicPath,
                d.Form.IsActivated,
                d.Form.EmbryonicLocationName,
                // 20191128 VuHQ START REQ-5
                d.Form.CompilationId,
                d.Form.ConfigFunction,
                d.Form.ChildCompilationId,
                d.Form.DefineFieldJson,
                d.Form.DefineConfigJson,
                d.Form.DefineValueJson,
                d.Form.FormHeader,
                d.Form.FormFooter,
                d.Form.TableName,
                // 20191128 VuHQ END REQ-5
                d.Form.ExplicitTemplate,
                d.Form.FormCodeCompilation,
                d.Form.FormCategoryId,
                d.Form.FormCode
            }, id).ToList();

            if (forms.Count > 0 && forms.First() != null)
            {
                var form = forms.First();

                ViewBag.Key = form.Template;
                ViewBag.TemplatePath = "EmbryonicForm/" + form.EmbryonicLocationName;
                ViewBag.TemplateName = form.EmbryonicPath;

                var formCodeCompilation = string.Empty;
                if (model.CategoryBusinessId == CATEGORY_BUSINESS_GENERAL)
                {
                    if (string.IsNullOrEmpty(form.FormCodeCompilation))
                    {
                        var compilationPrefix = new { schema = generateJsonFormGeneralPrefix(), form = new[] { "*" } };

                        formCodeCompilation = JsonConvert.SerializeObject(new
                        {
                            compilationPrefix = generateJsonFormGeneralPrefix()
                        });
                    }
                    else
                    {
                        formCodeCompilation = form.FormCodeCompilation;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(form.FormCodeCompilation))
                    {
                        var targetConfigJsonForm = new { schema = generateConfigJsonForm(true, form.Json), form = new[] { "*" } };
                        var summaryConfigJsonForm = new { schema = generateConfigJsonForm(false, form.Json), form = new[] { "*" } };

                        formCodeCompilation = JsonConvert.SerializeObject(new
                        {
                            targetConfigJsonForm = targetConfigJsonForm,
                            summaryConfigJsonForm = summaryConfigJsonForm
                        });
                    }
                    else
                    {
                        dynamic temp = JsonConvert.DeserializeObject(form.FormCodeCompilation);
                        if (temp.targetConfigJsonForm == null)
                        {
                            // Trường hợp data cũ chưa bao gồm loại mẫu tổng hợp
                            formCodeCompilation = JsonConvert.SerializeObject(new
                            {
                                targetConfigJsonForm = temp,
                                summaryConfigJsonForm = new { schema = generateConfigJsonForm(false, form.Json), form = new[] { "*" } }
                            });
                        }
                        else
                            formCodeCompilation = form.FormCodeCompilation;
                    }
                }


                return new FormModel()
                {
                    FormId = form.FormId,
                    FormTypeId = form.FormTypeId,
                    FormGroupId = form.FormGroupId ?? 0,
                    FormName = form.FormName,
                    Description = form.Description,
                    Json = form.Json,
                    IsPrimary = form.IsPrimary,
                    Template = form.Template,
                    EmbryonicPath = form.EmbryonicPath,
                    IsActivated = form.IsActivated,
                    EmbryonicLocationName = form.EmbryonicLocationName,
                    // 20191128 VuHQ START REQ-5
                    CompilationId = form.CompilationId,
                    ConfigFunction = form.ConfigFunction,
                    ChildCompilationId = form.ChildCompilationId,
                    DefineFieldJson = form.DefineFieldJson,
                    DefineConfigJson = form.DefineConfigJson,
                    DefineValueJson = form.DefineValueJson,
                    FormHeader = form.FormHeader,
                    FormFooter = form.FormFooter,
                    TableName = form.TableName,
                    // 20191128 VuHQ END REQ-5
                    ExplicitTemplate = form.ExplicitTemplate,
                    FormCodeCompilation = formCodeCompilation,
                    FormCategoryId = form.FormCategoryId == null ? 1 : form.FormCategoryId,
                    FormCode = form.FormCode
                };
            }
            else
            {
                return new FormModel();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeEdit")]
        public ActionResult EditPlus(DocTypeFormModel sModel, FormCollection formCollection)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            //prepare some data temp

            LoadDropDownList();
            ReBindDataWhenError(4);
            PrepareFormModel();

            var model = sModel.DocType;

            LoadDropDownList(model.LevelId, model.OfficeId, model.ActionLevel);

            if (ModelState.IsValid)
            {
                DocType docType = _docTypeService.Get(model.DocTypeId);
                if (docType == null)
                    return RedirectToAction("Index");
                string oldDocTypeName = docType.DocTypeName;
                string oldDocTypeCode = docType.DocTypeCode;
                docType.LastModifiedByUserId = User.GetUserId();
                docType.LastModifiedOnDate = DateTime.Now;
                model.Unsigned = ConverToUnsign(model.DocTypeName);
                model.DocTypePermission = 0;
                model.TotalViewed = 1;
                model.TotalRegisted = 1;
                model.DocTypeLaws = docType.DocTypeLaws;
                //10122019 taida: IsAllowOnline với biểu mẫu tạo từ báo cáo luôn là true
                model.IsAllowOnline = true;
                //10122019 taida: CategoryBusinessId với biểu mẫu tạo từ báo cáo luôn mặc định là 4
                model.CategoryBusinessId = 4;
                if (model.DoctypePermissions != null)
                {
                    foreach (int per in model.DoctypePermissions)
                    {
                        model.DocTypePermission |= per;
                    }
                }
                try
                {
                    docType = model.ToEntity(docType);
                    _docTypeService.Update(docType, oldDocTypeName, oldDocTypeCode);
                    _docTypeService.UpdateStoreIdDefault(docType, model.StoreIdDefault);

                    var timeJob = _docTypeTimeJobService.Get(docType.DocTypeId);
                    var timeJobModel = sModel.TimeJob;
                    var timeJobEntity = timeJobModel.ToEntity();
                    if (timeJob != null)
                    {
                        timeJob.IsActive = timeJobEntity.IsActive;
                        timeJob.ScheduleType = timeJobEntity.ScheduleType;
                        timeJob.ScheduleConfig = timeJobEntity.ScheduleConfig;

                        timeJob.IsActiveAlert = timeJobEntity.IsActiveAlert;                               
                        timeJob.ScheduleTypeDueDate = timeJobEntity.ScheduleTypeDueDate;
                        timeJob.ScheduleConfigDueDate = timeJobEntity.ScheduleConfigDueDate;

                        timeJob.IsActiveAlertOut = timeJobEntity.IsActiveAlertOut;
                        timeJob.ScheduleTypeOutOfDate = timeJobEntity.ScheduleTypeOutOfDate;
                        timeJob.ScheduleConfigOutOfDate = timeJobEntity.ScheduleConfigOutOfDate;

                        _docTypeTimeJobService.Update(timeJob);
                    }
                    else
                    {
                        _docTypeTimeJobService.Create(timeJobEntity);
                    }

                    var scheduler = new DocTypeScheduler();
                    scheduler.StopSchedule(docType.DocTypeId);

                    if (timeJobEntity.IsActive)
                    {
                        scheduler.RunSchedule(new[] { docType.DocTypeId }, timeJobEntity);
                    }
                    if (timeJobEntity.IsActiveAlert)
                    {
                        scheduler.RunScheduleDueTime(new[] { docType.DocTypeId }, timeJobEntity);
                    }
                    if (timeJobEntity.IsActiveAlertOut)
                    {
                        scheduler.RunScheduleOutOfTime(new[] { docType.DocTypeId }, timeJobEntity);
                    }
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(ex.Message);
                    BindData(sModel.TimeJob.ScheduleTypeEnum);
                    return View(sModel);
                }

                sModel.DocType = model;
                string msg = string.Empty;

                if (!EditForm(sModel, formCollection, out msg,false))
                {
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(msg);
                    BindData(sModel.TimeJob.ScheduleTypeEnum);
                    return View(sModel);
                }

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Updated"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Updated"));
                return Redirect("/Admin/DocType/");
            }
            ReBindDataWhenError(model.CategoryBusinessId);
            BindData(sModel.TimeJob.ScheduleTypeEnum);
            return View(sModel);
        }

       [HttpPost]
        public JsonResult GetIndexSheet(int index = 1) {
            if (index < 0) {
                return Json( new { success =  false } );
            }
            Session.Remove("indexSheet");
            Session["indexSheet"] = index;
            return Json( new { success = true } );
        }

        ///
        /// Client Import File Excel
        ///
        [HttpPost]
        public JsonResult GenHandsonConfigsByExcelClient()
        {
            // data trả về bao gồm
            // 1. DefineFieldJson
            // 2. DefineTypeJson
            // 3. DefineValueJson
            // 4. TableName
            // 5. FormCode
            int indexSheet;
            if (Session["indexSheet"] != null)
            {
                indexSheet = Convert.ToInt32(Session["indexSheet"]);
            }
            else
            {
                indexSheet = 1;
            }

            var defineFieldJson = string.Empty;
            var defineTypeJson = string.Empty;
            var defineValueJson = string.Empty;
            List<object> defineValueJsonDataFormat = new List<object>();
            List<object> defineValueJsonDataWidth = new List<object>();
            var listSheets = new List<Sheet>();
            var json = string.Empty;
            var tableName = string.Empty;
            var formCode = string.Empty;
            try
            {
                for (var index = 0; index < Request.Files.Count; index++)
                {
                    HttpPostedFileBase file = Request.Files[index]; //Uploaded file
                                                                    //Use the following properties to get file's name, size and MIMEType
                    int fileSize = file.ContentLength;

                    string mimeType = file.ContentType;

                    var xlsxParser = new XlsxToJson(file.InputStream);
                    listSheets = xlsxParser.GetSheetCount(file.InputStream);
                    var header = xlsxParser.ConvertHeaderXlsxToJson(indexSheet);
                    var data = xlsxParser.ConvertXlsxToJson(indexSheet);
                    var dataFormat = xlsxParser.ConvertXlsxToJsonAndFormat(indexSheet);
                    var dataWidth = xlsxParser.ConvertXlsxToJsonAndWidth(indexSheet);
                    var headerNested = xlsxParser.HeaderToJson(indexSheet);
                    var dataHeader = xlsxParser.GetDataHeader(indexSheet);
                    var headerMerge = xlsxParser.GetAddressMegre(indexSheet);
                    
                    Session.Remove("indexSheet");
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new
            {
                succress = false,
                defineFieldJson = defineFieldJson,
                defineTypeJson = defineTypeJson,
                defineValueJson = defineValueJson,
                tableName = tableName,
                cellsClass = defineValueJsonDataFormat,
                columnWidth = defineValueJsonDataWidth,
                listSheet = listSheets,
            });
        }
        #region generate DefineFieldJson, DefineValueJson, DefineTypeJson from Excel
        /// <summary>
        /// Từ file excel được upload lên, trả về dữ liệu data để fill vào handsontable khi [Tạo báo cáo]
        /// </summary>
        /// <returns></returns>    
        [HttpPost]
        public JsonResult GenHandsonConfigsByExcel()
        {
            // data trả về bao gồm
            // 1. DefineFieldJson
            // 2. DefineTypeJson
            // 3. DefineValueJson
            // 4. TableName
            // 5. FormCode
            int indexSheet;
            if (Session["indexSheet"] != null)
            {
                 indexSheet = Convert.ToInt32(Session["indexSheet"]);
            }
            else {
                 indexSheet = 1;
            }
          
            var defineFieldJson = string.Empty;
            var defineTypeJson = string.Empty;
            var defineValueJson = string.Empty;
            List<object> defineValueJsonDataFormat = new List<object>();
            List<object> defineValueJsonDataWidth = new List<object>();
            var listSheets = new List<Sheet>();
            var json = string.Empty;
            var tableName = string.Empty;
            var formCode = string.Empty;
            try
            {
                for (var index = 0; index < Request.Files.Count; index++)
                {
                    HttpPostedFileBase file = Request.Files[index]; //Uploaded file
                                                                    //Use the following properties to get file's name, size and MIMEType
                    int fileSize = file.ContentLength;

                    string mimeType = file.ContentType;

                    var xlsxParser = new XlsxToJson(file.InputStream);
                    listSheets = xlsxParser.GetSheetCount(file.InputStream);
                    var header = xlsxParser.ConvertHeaderXlsxToJson(indexSheet);
                    var data = xlsxParser.ConvertXlsxToJson(indexSheet);
                    var dataFormat = xlsxParser.ConvertXlsxToJsonAndFormat(indexSheet);
                    var dataWidth = xlsxParser.ConvertXlsxToJsonAndWidth(indexSheet);
                    var headerNested = xlsxParser.HeaderToJson(indexSheet);
                    var dataHeader = xlsxParser.GetDataHeader(indexSheet);
                    var headerMerge = xlsxParser.GetAddressMegre(indexSheet);
                    formCode = JsonConvert.SerializeObject(new {
                                                    header = header, data = data, dataFormat = dataFormat, dataWidth = dataWidth,
                                                    headerNested = headerNested, dataHeader = dataHeader, headerMerge = headerMerge });

                    dynamic _formCode = JsonConvert.DeserializeObject(formCode);

                    var handsonToJson = new HandsonToJson();
                    var columnSettings = handsonToJson.parseColumnSetting(new Form() { FormCode = formCode });

                    // generate extra -headerSetting
                    JArray headerSetting = new JArray();
                    if (_formCode.headerMerge != null)
                        headerSetting = _formCode.dataHeader;

                    // generate extra - mergedCells
                    //if (_formCode.headerMerge != null)
                    //    headerNested = JsonConvert.DeserializeObject(_formCode.headerMerge.ToString());

                    // generate colWidths
                    var colWidths = new int[0] { };

                    var extra = JsonConvert.SerializeObject(new
                    {
                        columnSetting = columnSettings,
                        headerSetting = headerSetting,
                        mergedCells = _formCode.headerMerge == null ? null : JsonConvert.DeserializeObject(_formCode.headerMerge.ToString())
                    });

                    //dataWidth       
                    foreach (var item in _formCode.dataWidth)
                    {
                        var dataDict = (Dictionary<string, string>)item.ToObject<Dictionary<string, string>>();
                        defineValueJsonDataWidth.Add(dataDict.Values.ToList());
                    }
                    //end dataWidth

                    formCode = JsonConvert.SerializeObject(new
                    {
                        header = _formCode.header,
                        data = _formCode.data,
                        dataFormat = _formCode.dataFormat,
                        headerNested = _formCode.headerNested,
                        dataHeader = _formCode.dataHeader,
                        headerMerge = _formCode.headerMerge,
                        extra = JsonConvert.DeserializeObject(extra),
                        colWidths = defineValueJsonDataWidth[0]
                        //colWidths = colWidths
                    });

                    // DefineFieldJson
                    var tempArr = _formCode.data[0].ToString().Split(',');
                    defineFieldJson = JsonConvert.SerializeObject(new
                    {
                        data = headerSetting,
                        mergedCells = _formCode.headerMerge == null ? null : JsonConvert.DeserializeObject(_formCode.headerMerge.ToString()),
                        countCols = tempArr.Length - 1,
                        colWidths = defineValueJsonDataWidth[0]
                    }); ;

                    // tranpose fieldConfigJson plus START
                    // Calculate the width and height of the Array
                    var w = headerSetting.Count;
                    var h = JArray.Parse(headerSetting[0].ToString()).Count;

                    var headerDict = (Dictionary<string, string>)_formCode.header.ToObject<Dictionary<string, string>>();
                    //var temp1 = (Dictionary<string, string>)JsonConvert.DeserializeObject<Dictionary<string, string>>(formCode.header.ToString());

                    /**
                        * @var {Number} i Counter
                        * @var {Number} j Counter
                        * @var {Array} t Transposed data is stored in this array.
                        */
                    string[,] defineConfigJsonData = new string[h, w + 6];
                    List<object> defineConfigJsonColumn = new List<object>();
                    var headerTemplates = handsonToJson.GetHeaderTemplates();

                    // Loop through every item in the outer array (height)
                    for (var i = 0; i < h; i++)
                    {
                        // Loop through every item per item in outer array (width)
                        for (var j = 0; j < w; j++)
                        {
                            // Save transposed data.
                            defineConfigJsonData[i, j] = headerSetting[j][i].ToString();
                        }

                        //// kieu du lieu
                        var headerTemplate = headerTemplates.Where(p => p.TypeDB == headerDict.ElementAt(i).Value).First();

                        var typeNameArr = headerTemplate.TypeName.Split('-');
                        if (typeNameArr.Length > 1)
                        {
                            // kieu du lieu
                            defineConfigJsonData[i, w] = typeNameArr[0].Trim();

                            // chi tiet
                            defineConfigJsonData[i, w + 1] = typeNameArr[1].Trim();
                        }
                        else
                        {
                            // kieu du lieu
                            defineConfigJsonData[i, w] = headerTemplate.TypeName;

                            // chi tiet
                            defineConfigJsonData[i, w + 1] = null;
                        }

                        // gia tri mac dinh
                        defineConfigJsonData[i, w + 2] = headerTemplate.TypeDB == "double" ? "0,0.00" : "";

                        // bat buoc
                        defineConfigJsonData[i, w + 3] = "true";


                        // readonly
                        defineConfigJsonData[i, w + 4] = "false";

                        // pham vi
                        defineConfigJsonData[i, w + 5] = null;
                    }


                    for (var i = 0; i < w; i++)
                    {
                        defineConfigJsonColumn.Add("readOnly : true");
                    }

                    defineConfigJsonColumn.Add("");
                    defineConfigJsonColumn.Add("allowInvalid: true");
                    defineConfigJsonColumn.Add("allowInvalid: true");
                    defineConfigJsonColumn.Add("");
                    defineConfigJsonColumn.Add("");
                    defineConfigJsonColumn.Add("");

                    // plus END

                    defineTypeJson = JsonConvert.SerializeObject(new { data = defineConfigJsonData, columns = defineConfigJsonColumn });

                    // defineValueJson
                    List<object> defineValueJsonData = new List<object>();
                    foreach (var item in _formCode.data)
                    {
                        var dataDict = (Dictionary<string, string>)item.ToObject<Dictionary<string, string>>();
                        dataDict.Remove("pos");
                        defineValueJsonData.Add(dataDict.Values.ToList());
                    }
                    //dataFormat         
                    foreach (var item in _formCode.dataFormat)
                    {
                        var dataDict = (Dictionary<string, string>)item.ToObject<Dictionary<string, string>>();
                        defineValueJsonDataFormat.Add(dataDict.Values.ToList());
                    }

                    List<string> defineValueJsonColumn = new List<string>();
                    for (var i = 1; i < tempArr.Length; i++)
                    {
                        defineValueJsonColumn.Add("allowInvalid: true");
                    }
                    defineValueJson = JsonConvert.SerializeObject(new { data = defineValueJsonData, columns = defineValueJsonColumn,
                        cellsClass = defineValueJsonDataFormat, classCells = defineValueJsonDataFormat, columnWidth = defineValueJsonDataWidth });

                    string _tableName = string.Empty;
                    var _fileNames = file.FileName.Split(new string[] { "##" }, StringSplitOptions.None);
                    _tableName = _fileNames.Count() > 1 ? _fileNames[1] : _fileNames[0];
                    tableName = _tableName.Replace(".xlsx", string.Empty).Replace(".xls", string.Empty);
                    Session.Remove("indexSheet");
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { succress = false, defineFieldJson = defineFieldJson, defineTypeJson = defineTypeJson,
                defineValueJson = defineValueJson, tableName = tableName, cellsClass = defineValueJsonDataFormat,
                columnWidth = defineValueJsonDataWidth, listSheet = listSheets,
            });
        }
        #endregion

        private int GetFormGroupIDWith(string docFieldName)
        {
            var datas = _formgroupService.GetsAs(p => new
            {
                p.FormGroupId
            }, p => p.FormGroupName == docFieldName);

            if (datas == null || datas.Count() == 0)
            {
                var formGroup = new FormGroup() { FormGroupName = docFieldName };
                _formgroupService.Create(formGroup);
                return formGroup.FormGroupId;
            }
            return datas.ElementAt(0).FormGroupId;
        }

        private bool EditForm(DocTypeFormModel sModel, FormCollection formCollection, out string msg, bool? isBCCP)
        {
            // set data cho FormName và Description
            sModel.Form.FormName = sModel.DocType.DocTypeName;
            sModel.Form.Description = sModel.DocType.CompendiumDefault;

            var model = sModel.Form;
            var form = _formService.Get(model.FormId);

            if (form != null)
            {
                var oldFormCode = form.FormCode;
                var listFile = new List<JsonFile>();
                var nameFileJson = new JsonFile();

                var createdBy = form.CreatedByUserId;
                form = model.ToEntity(form);
                form.CreatedByUserId = createdBy;
                form.Template = "f_" + RandomHelper.RandomString(15, false);
                var docFieldname = _docFieldService.Get(sModel.DocType.DocFieldId ?? 0).DocFieldName.TrimStart();
                form.FormGroupId = GetFormGroupIDWith(docFieldname);
                //10122019 taida: biếu mẫu tạo cùng báo cáo luôn được active
                form.IsActivated = 1;

                // 20191114 VuHQ REQ-2 START
                // 1.generate header
                // 2.generate data
                // 3.generate data.headerNested
                if (!string.IsNullOrEmpty(model.DefineFieldJson) && !string.IsNullOrEmpty(model.DefineConfigJson)
                    && !string.IsNullOrEmpty(model.DefineValueJson))
                {
                    Dictionary<string, string> header = null;

                    dynamic defineFieldObject = JsonConvert.DeserializeObject(model.DefineFieldJson);
                    dynamic defineConfigObject = JsonConvert.DeserializeObject(model.DefineConfigJson);

                    // get old defineFieldObject& defineConfigObject
                    var oldHeader = new Dictionary<string, string>();

                    if (!string.IsNullOrEmpty(oldFormCode))
                    {
                        dynamic oldFormCodeDeser = JsonConvert.DeserializeObject(oldFormCode);
                        oldHeader = JsonConvert.DeserializeObject<Dictionary<string, string>>(oldFormCodeDeser.header.ToString());
                    }

                    dynamic defineValueObject = JsonConvert.DeserializeObject(model.DefineValueJson);

                    var handsonToJson = new HandsonToJson(defineFieldObject, defineConfigObject, defineValueObject);

                    // 20191225 VuHQ Cù Trọng Xoay
                    var xoayHeader = new Dictionary<string, string>();
                    header = handsonToJson.ConvertHeaderHandsonToJson(out xoayHeader);

                    // 20191225 VuHQ Cù Trọng Xoay
                    Dictionary<string, HeaderObject> xoayColumnSetting = new Dictionary<string, HeaderObject>();
                    var columnSetting = handsonToJson.ConvertHeaderHandsonToJsonExtra(out xoayColumnSetting, isBCCP);

                    // generate data
                    var data = handsonToJson.ConvertHandsonToJson(header, false);
                    var headerNested = defineFieldObject.mergedCells;

                    // 20191225 VuHQ Cù Trọng Xoay
                    var extra = JsonConvert.SerializeObject(new
                    {
                        columnSetting = columnSetting,
                        headerSetting = defineFieldObject.data,
                        mergedCells = defineValueObject.mergedCells,
                        hiddenColumns = defineConfigObject.hiddenColumns,
                        autoSizeColumns = defineConfigObject.autoSizeColumns,
                        xoayObject = handsonToJson.XoayObject
                    });
                    form.FormCode = JsonConvert.SerializeObject(new
                    {
                        header = header,
                        data = data,
                        headerNested = headerNested,
                        extra = JsonConvert.DeserializeObject(extra),
                        colWidths = defineFieldObject.colWidths,
                        mergedCells = defineValueObject.mergedCells,
                        readOnlys = defineValueObject.readOnlys,
                        classCells = defineValueObject.classCells
                    });

                    // tự động generate luôn Json (sử dụng để hiển thị form ở mobile)
                    form.Json = JsonConvert.SerializeObject(columnSetting);

                    // 20191216 VuHQ push API phần thay đổi cấu hình START
                    Dictionary<string, string> addList = new Dictionary<string, string>();
                    Dictionary<string, string> modifyList = new Dictionary<string, string>();

                    if (!string.IsNullOrEmpty(sModel.Form.TableName))
                    {
                        if (oldHeader.Count > 0)
                        {
                            //for (int i = 1; i <= header.Count; i++)
                            //{
                            //    if (i > oldHeader.Count)
                            //        addList.Add(header.ElementAt(i - 1).Key.Split(new string[] { "!!" }, StringSplitOptions.None)[0], header.ElementAt(i - 1).Value);
                            //    else if (header.ElementAt(i - 1).Value != oldHeader.ElementAt(i - 1).Value)
                            //        modifyList.Add(header.ElementAt(i - 1).Key.Split(new string[] { "!!" }, StringSplitOptions.None)[0], header.ElementAt(i - 1).Value);
                            //}
                            
                            for (int i = 1; i <= header.Count(); i++)
                            {
                                var key = header.ElementAt(i - 1).Key;
                                var value = header.ElementAt(i - 1).Value;
                                var keyName = header.ElementAt(i - 1).Key.Split(new string[] { "!!" }, StringSplitOptions.None)[0];
                                if (oldHeader.ContainsKey(key))
                                {
                                    if (oldHeader[key] != value)
                                    {
                                        modifyList.Add(keyName, value);
                                    }
                                }
                                else
                                {
                                    addList.Add(keyName, value);
                                }
                            }
                        }

                        //if (defineFieldObject.newColumns != null)
                        //{
                        //    var newColumns = defineFieldObject.newColumns;
                        //    if (oldHeader.Count != newColumns.Count)
                        //    {
                        //        foreach (int columnIndex in newColumns)
                        //        {
                        //            addList.Add(header.ElementAt(columnIndex - 1).Key.Split(new string[] { "!!" }, StringSplitOptions.None)[0], header.ElementAt(columnIndex - 1).Value);
                        //        }
                        //    }
                        //}

                        var timeKey = GetActionLevelKey().Where(p => p.Value.Equals(sModel.DocType.ActionLevel.ToString())).FirstOrDefault();

                        var asciiName = XlsxToJson.ConvertToAscii(sModel.Form.TableName);

                        if (asciiName.Length > 64)
                        {
                            msg = "Tên table không thể vượt quá 64 ký tự.";
                            return false;
                        }

                        if (addList.Count != 0 || modifyList.Count != 0)
                        {
                            nameFileJson.filename = asciiName + "!!" + sModel.Form.TableName;
                            nameFileJson.typetime = timeKey.Key;
                            nameFileJson.column = header;
                            nameFileJson.alter = true;
                            nameFileJson.alter_columns = new Dictionary<string, Dictionary<string, string>>
                            {
                                {
                                    "add", addList
                                },
                                {
                                    "modify", modifyList
                                }
                            };
                            nameFileJson.dbname = _generalSettings.BITranports;
                            listFile.Add(nameFileJson);
                        }
                        // 20191216 VuHQ push API phần thay đổi cấu hình END
                    }
                }
                // 20191114 VuHQ REQ-2 END

                // 20200213 Phase 2 - REQ-0 START
                if (model.Compilation != null)
                {
                    var targetConfigCompilations = generateConfigJsonForm(true, form.Json);

                    targetConfigCompilations.ElementAt(0).Value.text = model.Compilation.Field;
                    targetConfigCompilations.ElementAt(1).Value.text = model.Compilation.Value;
                    targetConfigCompilations.ElementAt(2).Value.text = model.Compilation.Match;
                    targetConfigCompilations.ElementAt(3).Value.text = model.Compilation.Select;
                    targetConfigCompilations.ElementAt(4).Value.text = model.Compilation.Display;

                    var targetConfigJsonForm = new { schema = targetConfigCompilations, form = new[] { "*" } };

                    var summaryConfigCompilations = generateConfigJsonForm(false, form.Json);
                    foreach (var summaryDetail in summaryConfigCompilations)
                    {
                        summaryDetail.Value.text = formCollection[summaryDetail.Key];
                    }

                    var summaryConfigJsonForm = new { schema = summaryConfigCompilations, form = new[] { "*" } };

                    form.FormCodeCompilation = JsonConvert.SerializeObject(new
                    {
                        targetConfigJsonForm = targetConfigJsonForm,
                        summaryConfigJsonForm = summaryConfigJsonForm
                    });
                }
                else if (model.GeneralCompilationHeader != null && !string.IsNullOrEmpty(model.GeneralCompilationHeader.TableName))
                {
                    var compilationPrefix = generateJsonFormGeneralPrefix();
                    compilationPrefix.ElementAt(0).Value.text = model.GeneralCompilationHeader.TableName;

                    var compilationMain = generateJsonFormGeneralMain(form.Json, model.GeneralCompilationHeader.TableName);
                    compilationMain.ElementAt(0).Value.text = model.GeneralCompilationHeader.Select;
                    compilationMain.ElementAt(1).Value.text = model.GeneralCompilationHeader.Display;
                    compilationMain.ElementAt(2).Value.text = model.GeneralCompilationHeader.Formula;

                    // trường hợp đặc biệt START
                    // binding dữ liệu vào jsonform type array START
                    var compilationSuffix1Dict = generateJsonFormGeneralSuffix(model.GeneralCompilationHeader.TableName, true);
                    var compilationSuffix2Dict = generateJsonFormGeneralSuffix(model.GeneralCompilationHeader.TableName, false);

                    var compilationSuffix1Str = "{\"Method\": {\"type\": \"array\", \"items\": {\"type\": \"object\", \"title\": \"Công thức {{idx}}\", \"properties\": " +
                    JsonConvert.SerializeObject(compilationSuffix1Dict) + "}}}";

                    var compilationSuffix2Str = "{\"Query\": {\"type\": \"array\", \"items\": {\"type\": \"object\", \"title\": \"Truy vấn {{idx}}\", \"properties\": " +
                            JsonConvert.SerializeObject(compilationSuffix2Dict, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) + "}}}";
                    // binding dữ liệu vào jsonform type array END

                    form.FormCodeCompilation = JsonConvert.SerializeObject(new
                    {
                        compilationPrefix = new { schema = compilationPrefix, form = new[] { "*" } },
                        compilationMain = new { schema = compilationMain, form = new[] { "*" } },
                        compilationSuffix1 = new
                        {
                            schema = JsonConvert.DeserializeObject(compilationSuffix1Str),
                            value = new
                            {
                                Method = model.GeneralCompilationDetail.Method
                            },
                            form = new[] { "*" }
                        },
                        compilationSuffix2 = new
                        {
                            schema = JsonConvert.DeserializeObject(compilationSuffix2Str),
                            value = new
                            {
                                Query = model.GeneralCompilationDetail.Query
                            },
                            form = new[] { "*" }
                        },
                    });
                }
                // 20200213 Phase 2 - REQ-0 END

                _formService.Update(form);

                // 20191114 VuHQ REQ-2 START
                if (listFile.Count != 0)
                {
                    Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>()
                    {
                        "Sửa bảng:",
                        JsonConvert.SerializeObject(listFile)
                    });
                    var succress = Post("AutoCreate", listFile);
                }
                // 20191114 VuHQ REQ-2 END
            }
            else
            {
                var finalForms = CreateFrom(sModel);

                foreach (Form mform in finalForms)
                {
                    ChangeIsPrimary(mform.FormId, sModel.DocType.DocTypeId, false);
                }
                var finalForm = finalForms.First();
                finalForm.Template = "f_" + RandomHelper.RandomString(15, false);
                _formService.Update(finalForms.First());

                sModel.Form = new FormModel()
                {
                    FormId = finalForm.FormId,
                    FormTypeId = finalForm.FormTypeId,
                    FormGroupId = finalForm.FormGroupId ?? 0,
                    FormName = finalForm.FormName,
                    Description = finalForm.Description,
                    Json = finalForm.Json,
                    IsPrimary = finalForm.IsPrimary,
                    Template = finalForm.Template,
                    EmbryonicPath = finalForm.EmbryonicPath,
                    //10122019 taida: biếu mẫu tạo cùng báo cáo luôn được active
                    //IsActivated = finalForm.IsActivated,
                    IsActivated = 1,
                    EmbryonicLocationName = finalForm.EmbryonicLocationName,
                };
            }
            BindFormGroupAndFormType(model.FormGroupId, model.FormTypeId);

            msg = string.Empty;
            return true;
        }

        public JsonResult DeleteFormNRef(Guid id, Guid docTypeId)
        {
            //xóa liên kết trước khi xóa biểu mẫu
            var doctypeform = _doctypeformService.Get(id, docTypeId);
            if (doctypeform != null)
            {
                _doctypeformService.Delete(doctypeform);
            }

            //xóa biểu mẫu
            var form = _formService.Get(id);
            if (form != null)
            {
                try
                {
                    _formService.Detele(form);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// VuHQ giải quyết sự vụ restore lại field DefineFieldJson của Form bị mất dữ liệu
        /// Điều kiện: dữ liệu DefineConfigJson != '' và FormCode != ''
        /// </summary>
        /// <returns></returns>
        public JsonResult RestoreDefineFieldJsons()
        {
            var forms = _formService.Gets(p => string.IsNullOrEmpty(p.DefineFieldJson) 
            && !string.IsNullOrEmpty(p.DefineConfigJson)
            && !string.IsNullOrEmpty(p.FormCode), false);
            foreach (var form in forms)
            {
                dynamic formCode = JsonConvert.DeserializeObject(form.FormCode);
                var tempArr = formCode.data.Count == 0 ? new string[0] : formCode.data[0].ToString().Split(',');
                var defineFieldJson = JsonConvert.SerializeObject(new
                {
                    data = formCode.extra.headerSetting,
                    mergedCells = formCode.headerNested == null ? null : JsonConvert.DeserializeObject(formCode.headerNested.ToString()),
                    countCols = tempArr.Length - 1,
                    colWidths = formCode.colWidths
                });
                form.DefineFieldJson = defineFieldJson;
                _formService.Update(form);
            }

            return Json(new { success = true, msg = "Restore thành công." }, JsonRequestBehavior.AllowGet);
        }

        private void PrepareFormModel()
        {
            var httpCookie = Request.Cookies[CookieName.SearchForm];
            var search = new FormSearchModel();
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                search = Json2.ParseAs<FormSearchModel>(data["Search"].ToString());
            }
            BindFormGroupAndFormType(search.FormGroupId, search.FormTypeId, search.DocTypeId, search.DocFieldId);
        }
        private void BindFormGroupAndFormType(int? formGroupId = null, int? formTypeId = null, Guid? docTypeId = null, int? docFieldId = 0)
        {
            ViewBag.FormGroupIds =
                _formgroupService.Gets()
                    .Select(
                        f =>
                            new SelectListItem
                            {
                                Selected = formGroupId.HasValue && formGroupId.Value == f.FormGroupId,
                                Text = f.FormGroupName,
                                Value = f.FormGroupId.ToString()
                            });

            ViewBag.FormTypeIds = _formService.GetTypes().Select(
                f =>
                    new SelectListItem
                    {
                        Selected = formTypeId.HasValue && formTypeId.Value == f.FormTypeId,
                        Text = f.FormTypeName,
                        Value = f.FormTypeId.ToString()
                    });

            var doctypes = _docTypeService.GetAllFromCache();
            /*ViewBag.DocTypeIds = doctypes.Where(dt => !docFieldId.HasValue || (dt.DocFieldId.HasValue && dt.DocFieldId.Value == docFieldId.Value))
                    .Select(dt => new SelectListItem()
                    {
                        Text = dt.DocTypeName,
                        Value = dt.DocTypeId.ToString(),
                        Selected = docTypeId.HasValue && docTypeId.Value.Equals(dt.DocTypeId)
                    });*/

            var docfields = doctypes.Where(df => df.DocFieldId.HasValue).Select(dt => new
            {
                DocFieldId = dt.DocFieldId,
                DocFieldName = dt.DocFieldName
            });

            ViewBag.DocFieldIds = docfields.Distinct().Select(df => new SelectListItem()
            {
                Text = df.DocFieldName,
                Value = df.DocFieldId.Value.ToString(),
                Selected = docFieldId.HasValue && docFieldId.Value.Equals(df.DocFieldId)
            });
        }

        private void CreateCookieSearch(FormSearchModel search, SortAndPagingModel sortAndPage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortAndPage } };
            HttpCookie cookie = Request.Cookies[CookieName.SearchForm];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchForm, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private bool Post(string action, dynamic data)
        {
            var client = new HttpClient();

            var appSettings = ConfigurationManager.AppSettings;
            var url = appSettings["createETL"];
            Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>()
            {
                JsonConvert.SerializeObject(url)
            });
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
            if (data != null)
            {
                var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
                var responseMessage = client.PostAsync("", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseMessage.Content.ReadAsStringAsync().Result);
                    var isSuccess = (bool)result["success"];
                    return isSuccess;
                }
            }
            else
            {
                var responseMessage = client.PostAsync(action, null).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                }
            }

            return false;
        }

        /// <summary>
        /// 20202018 VuHQ
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, ConfigCompilation> generateConfigJsonForm(bool isTargetConfig, string strJsonFormMobile)
        {
            var compilations = new Dictionary<string, ConfigCompilation>();

            if (isTargetConfig)
            {
                compilations.Add("Form_Compilation_Field", new ConfigCompilation { typeOther = "string", title = "Field điều kiện" });
                compilations.Add("Form_Compilation_Value", new ConfigCompilation { typeOther = "string", title = "Value điều kiện" });
                compilations.Add("Form_Compilation_Match", new ConfigCompilation { typeOther = "string", title = "Field so sánh" });
                compilations.Add("Form_Compilation_Select", new ConfigCompilation { typeOther = "string", title = "Field chỉ tiêu" });
                compilations.Add("Form_Compilation_Display", new ConfigCompilation { typeOther = "string", title = "Field hiển thị chỉ tiêu" });
            }
            else if (!string.IsNullOrEmpty(strJsonFormMobile))
            {
                compilations.Add("Form_sql", new ConfigCompilation { typeOther = "textarea", title = "Câu truy vấn", htmlClass = "form-control", });
                compilations.Add("Form_Compilation_Match_Off", new ConfigCompilation { typeOther = "boolean", title = "Sử dụng so sánh",text = "true"});
                //compilations.Add("Form_Compilation_Field", new ConfigCompilation { type = "string", title = "Field điều kiện" });
                //compilations.Add("Form_Compilation_Value", new ConfigCompilation { type = "string", title = "Value điều kiện" });
                compilations.Add("Form_Compilation_Target_Match", new ConfigCompilation { typeOther = "string", title = "Field so sánh chỉ tiêu" });
                compilations.Add("Form_Compilation_Display_Match", new ConfigCompilation { typeOther = "string", title = "Field so sánh hiển thị" });

                Dictionary<string, HeaderObject> jsonFormMobile = new Dictionary<string, HeaderObject>();
                try
                {
                    jsonFormMobile = JsonConvert.DeserializeObject<Dictionary<string, HeaderObject>>(strJsonFormMobile);
                }
                catch
                {
                }

                foreach (var field in jsonFormMobile)
                {
                    compilations.Add("Form_" + field.Key.Split(new string[] { "!!" }, StringSplitOptions.None)[0], new ConfigCompilation
                    {
                        typeOther = "string",
                        title = field.Key.Split(new string[] { "!!" }, StringSplitOptions.None)[1]
                    });
                }
            }

            return compilations;
        }

        private List<string> GetTableNamesBySchema()
        {
            List<string> list = new List<string>();
            DataTable dt;
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" SELECT TABLE_NAME FROM information_schema.tables where TABLE_SCHEMA = '{0}'", "wso2_yenbai");
            dt = executeQuery(query.ToString());
            if (dt != null)
            {
                list = dt.AsEnumerable()
                           .Select(r => r.Field<string>("TABLE_NAME"))
                           .ToList();
            }

            return list;
        }

        private List<string> GetFieldNamesByTable(string tableName)
        {
            List<string> list = new List<string>();
            DataTable dt;
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}'", "wso2_yenbai", tableName);
            dt = executeQuery(query.ToString());

            if (dt != null)
            {
                list = dt.AsEnumerable()
                           .Select(r => r.Field<string>("COLUMN_NAME"))
                           .ToList();
            }

            return list;
        }

        private DataTable executeQuery(string query)
        {
            DataTable dt;
            using (var dbConn = new MySqlConnection(_adminSetting.DashboardConnection))
            {
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = query;
                dt = new DataTable();
                try
                {
                    dbConn.Open();
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception e)
                {
                    Core.Logging.StaticLog.Log(new List<string>() { e.Message });
                    return null;
                }
            }

            return dt;
        }

        private List<string> GetFieldNamesByHandsonConfig(string strJsonFormMobile)
        {
            List<string> list = new List<string>();
            Dictionary<string, HeaderObject> jsonFormMobile = JsonConvert.DeserializeObject<Dictionary<string, HeaderObject>>(strJsonFormMobile);

            foreach (var field in jsonFormMobile)
            {
                list.Add(field.Key.Split(new string[] { "!!" }, StringSplitOptions.None)[1]);
            }
            return list;
        }
        private Dictionary<string, ConfigCompilation> generateJsonFormGeneralPrefix()
        {
            var compilations = new Dictionary<string, ConfigCompilation>();

            //compilations.Add("Form_GeneralCompilationHeader_TableName", new ConfigCompilation { typeOther = "string", title = "Danh sách bảng wso2", htmlClass = "form-control", enumOther = GetTableNamesBySchema().ToArray() });

            return compilations;
        }

        private Dictionary<string, ConfigCompilation> generateJsonFormGeneralMain(string strJsonFormMobile, string selectedTableName)
        {
            var compilations = new Dictionary<string, ConfigCompilation>();

            //if (string.IsNullOrEmpty(selectedTableName))
            //    compilations.Add("Form_GeneralCompilationHeader_Select", new ConfigCompilation { typeOther = "string", title = "Field so sánh chỉ tiêu", htmlClass = "form-control", enumOther = new string[0] });
            //else
            //    compilations.Add("Form_GeneralCompilationHeader_Select", new ConfigCompilation
            //    {
            //        typeOther = "string",
            //        title = "Field so sánh chỉ tiêu",
            //        htmlClass = "form-control",
            //        enumOther = GetFieldNamesByTable(selectedTableName).ToArray()
            //    });

            //if (string.IsNullOrEmpty(strJsonFormMobile))
            //    compilations.Add("Form_GeneralCompilationHeader_Display", new ConfigCompilation { typeOther = "string", title = "Field so sánh hiển thị", enumOther = new string[0] });
            //else
            //    compilations.Add("Form_GeneralCompilationHeader_Display", new ConfigCompilation { typeOther = "string", title = "Field so sánh hiển thị", enumOther = GetFieldNamesByHandsonConfig(strJsonFormMobile).ToArray() });

            //if (string.IsNullOrEmpty(selectedTableName))
            //    compilations.Add("Form_GeneralCompilationHeader_Formula", new ConfigCompilation { typeOther = "string", title = "Cột công thức", enumOther = new string[0] });
            //else
            //    compilations.Add("Form_GeneralCompilationHeader_Formula", new ConfigCompilation
            //    {
            //        typeOther = "string",
            //        title = "Cột công thức",
            //        enumOther = GetFieldNamesByTable(selectedTableName).ToArray()
            //    });

            return compilations;
        }

        private List<string> getGeneralMethods()
        {
            var list = new List<string>();

            list.Add("Lũy kế");
            list.Add("Cùng kỳ");
            list.Add("Cùng kỳ (%)");
            list.Add("Kỳ trước");
            list.Add("Kỳ trước %");

            return list;
        }

        private Dictionary<string, ConfigCompilation> generateJsonFormGeneralSuffix(string selectedTableName, bool isSuffix1)
        {
            var generalSuffix = string.Empty;
            var compilations = new Dictionary<string, ConfigCompilation>();

            //if (isSuffix1)
            //{
            //    // phương thức
            //    compilations.Add("MethodName", new ConfigCompilation
            //    {
            //        typeOther = "string",
            //        title = "Phương thức",
            //        htmlClass = "form-control",
            //        enumOther = getGeneralMethods().ToArray()
            //    });


            //    compilations.Add("DBFieldName", new ConfigCompilation
            //    {
            //        typeOther = "string",
            //        title = "DB Field",
            //        htmlClass = "form-control",
            //        enumOther = GetFieldNamesByTable(selectedTableName).ToArray()
            //    });
            //}
            //else
            //{
            //    compilations.Add("QueryString", new ConfigCompilation
            //    {
            //        typeOther = "textarea",
            //        title = "Câu truy vấn",
            //        htmlClass = "form-control"
            //    });
            //}

            return compilations;
        }

        #endregion
        // chưa có file ???
        // 

        // 20200317 HoangDX START
        private void BindData(DocTypeScheduleType scheduleType = DocTypeScheduleType.HangNam)
        {
            ViewBag.AllScheduleType = GetListScheduleType(scheduleType);
        }

        private void BindDataDueDate(DocTypeScheduleTypeDueDate scheduleType = DocTypeScheduleTypeDueDate.HangNamDueDate)
        {
            ViewBag.AllScheduleTypeDueDate = GetListScheduleTypeDueDate(scheduleType);
        }
        
        private void BindDataOutOfDate(DocTypeScheduleTypeOutOfDate scheduleType = DocTypeScheduleTypeOutOfDate.HangNamOutOfDate)
        {
            ViewBag.AllScheduleTypeOutOfDate = GetListScheduleTypeOutOfDate(scheduleType);
        }

        private List<SelectListItem> GetListScheduleTypeOutOfDate(DocTypeScheduleTypeOutOfDate scheduleTypeOutOfDate)
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(DocTypeScheduleTypeOutOfDate));
            foreach (var val in enumValArray)
            {
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription((DocTypeScheduleTypeOutOfDate)val),
                    Value = val.ToString(),
                    Selected = (DocTypeScheduleTypeOutOfDate)val == scheduleTypeOutOfDate
                });
            }
            return result;
        }

        private List<SelectListItem> GetListScheduleTypeDueDate(DocTypeScheduleTypeDueDate scheduleTypeDueDate)
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(DocTypeScheduleTypeDueDate));
            foreach(var val in enumValArray)
            {
                result.Add(new SelectListItem {
                    Text = _resourceService.GetEnumDescription((DocTypeScheduleTypeDueDate)val),
                    Value = val.ToString(),
                    Selected = (DocTypeScheduleTypeDueDate)val == scheduleTypeDueDate
                });
            }
            return result;
        }


        private List<SelectListItem> GetListScheduleType(DocTypeScheduleType scheduleType)
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(DocTypeScheduleType));
            foreach (var val in enumValArray)
            {
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription((DocTypeScheduleType)val),
                    Value = val.ToString(),
                    Selected = (DocTypeScheduleType)val == scheduleType
                });
            }
            return result;
        }
        // 20200317 HoangDX END
    }
}

public class DocTypeImport
{
    public string caphanhchinh { get; set; }
    public string doctypename { get; set; }
    public string docfieldname { get; set; }
    public string level { get; set; }
    public string formcode { get; set; }
    public string filePath { get; set; }
    public string fileName { get; set; }
    public string configForm { get; set; }
}
public class ReportModels {
    public int ReportModelId { get; set; }
    public string Name { get; set; }
}