using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
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
using System.Text;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.DataAccess;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    //  [EgovAuthorize]
    public class DocTypeGovController : CustomController
    {
        private const string DEFAULT_SORT_BY = "DocTypeName";
        private readonly CategoryBll _categoryService;
        private readonly CodeBll _codeService;
        private readonly DepartmentBll _departmentService;
        private readonly DocFieldBll _docFieldService;
        private readonly DocTypeBll _docTypeService;
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
        private readonly dataTypeBll _dataTypeService;
        private readonly Ad_LocalityBll _localityService;
        private readonly CategoryDisaggregationsBll _categoryDisaggregationsService;
        public DocTypeGovController(FeeBll feeService,
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
                                 dataTypeBll dataTypeService,
                                 Ad_LocalityBll localityService,
                                 CategoryDisaggregationsBll categoryDisaggregationsService
            )
            : base()
        {
            _doctypeFeeService = doctypeFeeService;
            _feeService = feeService;
            _doctypePaperService = doctypePaperService;
            _paperService = paperService;
            _officeService = officeService;
            _lawService = lawService;
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
            _dataTypeService = dataTypeService;
            _localityService = localityService;
            _categoryDisaggregationsService = categoryDisaggregationsService;
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

        [HttpPost]
        [ValidateInput(false)]
        public bool Config(string jsonMapping, Guid id)
        {
            try
            {
                var forms = _doctypeformService.GetsAs(f => new
                {
                    f.Form.FormId,
                    f.Form.FormName,
                    f.Form.Description,
                    f.IsPrimary,
                    f.IsActive,
                    f.Form.MappingMaDinhDanhCP
                }, f => f.DocTypeId == id).Select(f => new Form
                {
                    FormId = f.FormId,
                    FormName = f.FormName,
                    Description = f.Description,
                    IsPrimary = f.IsPrimary,
                    IsActivated = f.IsActive ? 1 : 0,
                    MappingMaDinhDanhCP = f.MappingMaDinhDanhCP
                }).FirstOrDefault();
                var form = _formService.Get(forms.FormId);

                var jo = JObject.Parse(jsonMapping);
                var timeKey = jo.Properties().First().Name;

                var existingMapping = form.MappingMaDinhDanhCP;
                if (String.IsNullOrEmpty(existingMapping))
                {
                    form.MappingMaDinhDanhCP = jsonMapping;
                }
                else
                {
                    JObject rss = JObject.Parse(existingMapping);

                    var existingNode = rss[timeKey];
                    if (existingNode == null)
                    {
                        var newMapping = JObject.Parse(jsonMapping);
                        rss.Merge(newMapping, new JsonMergeSettings
                        {
                            // union array values together to avoid duplicates
                            MergeArrayHandling = MergeArrayHandling.Union
                        });
                        //rss.Merge(newMapping);
                        //rss.Add(newMapping);
                    }
                    else
                    {
                        rss[timeKey] = JObject.Parse(jsonMapping)[timeKey];
                    }
                    form.MappingMaDinhDanhCP = rss.Stringify();
                }
               

                //dynamic data = System.Web.Helpers.Json.Decode(jsonMapping);
               
                _formService.Update(form);
                SuccessNotification("Lưu thành công", true);
                return true;
            }
            catch (Exception ex)
            {
                ErrorNotification("Lưu thất bại", true);
                throw;
            }

        }

        public ActionResult Config(Guid id)
        {
            ViewBag.DoctypeId = id;
            MappingMaDinhDanh mappingMaDinhDanh = new MappingMaDinhDanh();
            mappingMaDinhDanh.ListDinhDanhCP = "[]";
            mappingMaDinhDanh.ListMaDinhDanhDB = "[]";
            mappingMaDinhDanh.MappingMaDinhDanhCP = "[]";
            return View(mappingMaDinhDanh);
        }

        //public ActionResult Config(Guid id)
        //{
        //    MappingMaDinhDanh mappingMaDinhDanh = new MappingMaDinhDanh();
        //    try
        //    {

        //        var forms = _doctypeformService.GetsAs(f => new
        //        {
        //            f.Form.FormId,
        //            f.Form.FormName,
        //            f.Form.Description,
        //            f.IsPrimary,
        //            f.IsActive,
        //            f.Form.MappingMaDinhDanhCP
        //        }, f => f.DocTypeId == id).Select(f => new Form
        //        {
        //            FormId = f.FormId,
        //            FormName = f.FormName,
        //            Description = f.Description,
        //            IsPrimary = f.IsPrimary,
        //            IsActivated = f.IsActive ? 1 : 0,
        //            MappingMaDinhDanhCP = f.MappingMaDinhDanhCP
        //        }).FirstOrDefault();
        //        mappingMaDinhDanh.MappingMaDinhDanhCP = forms.MappingMaDinhDanhCP;

        //        DocType docType = _docTypeService.Get(id);
        //        ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
        //        if (docType == null)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        var checkedStoreIds = _docTypeService.GetStoreIds(docType.DocTypeId);
        //        var storeIdDefault = _docTypeService.GetStoreIdDefault(docType.DocTypeId);
        //        ReBindDataWhenError(docType.CategoryBusinessId, checkedStoreIds, storeIdDefault);
        //        if (!docType.IsAllowOnline.HasValue)
        //        {
        //            docType.IsAllowOnline = false;
        //        }
        //        ViewBag.DoctypeId = id;
        //        ViewBag.StoreIds = checkedStoreIds;
        //        var model = docType.ToModel();
        //        model.StoreIdDefault = storeIdDefault;
        //        var sModel = new DocTypeFormModel();
        //        sModel.DocType = model;
        //        sModel.Form = GetFormWith(model);
        //        dynamic formCodeCompilation = JsonConvert.DeserializeObject(sModel.Form.FormCodeCompilation);
        //        var schema = (Dictionary<string, ConfigCompilation>)JsonConvert.DeserializeObject<Dictionary<string, ConfigCompilation>>(formCodeCompilation.summaryConfigJsonForm.schema.ToString());
        //        IEnumerable<dynamic> result = new List<dynamic>();
        //        var connection = new MySqlConnection("server=1.53.252.175;User Id=anhtuan;password=anhtuan89;database=wso2_yenbai;port=3306");
        //        using (var context = new EfContext(connection))
        //        {
        //            result = context.RawQuery(schema.Where(x => x.Key == "Form_sql").FirstOrDefault().Value.text, null);
        //        };

        //        mappingMaDinhDanh.ListMaDinhDanh = JsonConvert.SerializeObject(result);
        //        mappingMaDinhDanh.DefineValueJson = sModel.Form.DefineValueJson;
        //        return View(mappingMaDinhDanh);
        //    }
        //    catch (Exception)
        //    {
        //        mappingMaDinhDanh.ListMaDinhDanh = "[]";
        //        mappingMaDinhDanh.DefineValueJson = "[]";
        //        mappingMaDinhDanh.MappingMaDinhDanhCP = "[]";
        //        return View(mappingMaDinhDanh);
        //    }
        //}

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult GetFiledConfig(Guid id, string timeKey)
        {
            MappingMaDinhDanh mappingMaDinhDanh = new MappingMaDinhDanh();
            try
            {

                var forms = _doctypeformService.GetsAs(f => new
                {
                    f.Form.FormId,
                    f.Form.FormName,
                    f.Form.Description,
                    f.IsPrimary,
                    f.IsActive,
                    f.Form.MappingMaDinhDanhCP
                }, f => f.DocTypeId == id).Select(f => new Form
                {
                    FormId = f.FormId,
                    FormName = f.FormName,
                    Description = f.Description,
                    IsPrimary = f.IsPrimary,
                    IsActivated = f.IsActive ? 1 : 0,
                    MappingMaDinhDanhCP = f.MappingMaDinhDanhCP
                }).FirstOrDefault();
                if (!String.IsNullOrEmpty(forms.MappingMaDinhDanhCP))
                {
                    JObject rss = JObject.Parse(forms.MappingMaDinhDanhCP);

                    var existingNode = rss[timeKey];
                    mappingMaDinhDanh.MappingMaDinhDanhCP = existingNode.Stringify();
                }
                else
                {
                    mappingMaDinhDanh.MappingMaDinhDanhCP = "[]";
                }

                DocType docType = _docTypeService.Get(id);
                ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
                if (docType == null)
                {
                    return new JsonResult();
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
                var model = docType.ToModel();
                model.StoreIdDefault = storeIdDefault;
                var sModel = new DocTypeFormModel();
                sModel.DocType = model;
                sModel.Form = GetFormWith(model);

                var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
                var organizekey = currentDepartment.Emails;

                IEnumerable<dynamic> result = new List<dynamic>();
                var d = JsonConvert.DeserializeObject<dynamic>(sModel.Form.FormCodeCompilation);
                var config = d.summaryConfigJsonForm;
                var query = new StringBuilder();
                if (config.schema.Form_sql != null)
                    query = new StringBuilder(config.schema.Form_sql["default"].ToString());
                var strQuery = query.ToString().Replace("dashboard:", "");
                var paras = new List<MySqlParameter>();
                paras.Add(new MySqlParameter("TimeKey", timeKey));
                paras.Add(new MySqlParameter("OrganizeKey", organizekey));

                var connection = new MySqlConnection("server=1.53.252.175;User Id=anhtuan;password=anhtuan89;database=wso2_yenbai;port=3306");
                using (var context = new EfContext(connection))
                {
                    result = context.RawQuery(strQuery, paras.ToArray());
                };
                //return new JsonResult { Data = result };
                mappingMaDinhDanh.ListDinhDanhCP = sModel.Form.DefineValueJson;
                mappingMaDinhDanh.ListMaDinhDanhDB = JsonConvert.SerializeObject(result);
                return new JsonResult { Data = mappingMaDinhDanh };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = null
                };
                //mappingMaDinhDanh.ListMaDinhDanh = "[]";
                //mappingMaDinhDanh.DefineValueJson = "[]";
                //mappingMaDinhDanh.MappingMaDinhDanhCP = "[]";
                ////return View(mappingMaDinhDanh);
            }
        }

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

        public ActionResult Index(int? categoryBusinessId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermission"));
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            if (categoryBusinessId == 32)
            {
                return View("../DocTypeGov/IndexSurvey");
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
            categoryBusinessId = 64;
            var model = GetDocTypeModels(search, sortAndPage.SortBy, sortAndPage.IsSortDescending,
                                         sortAndPage.CurrentPage, sortAndPage.PageSize, categoryBusinessId);

            ViewBag.AllDocFields = _docFieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId })
                                        .OrderBy(d => d.DocFieldName)
                                        .StringifyJs();

            ViewBag.Search = search;
            ViewBag.CategoryBusinessId = BindCategoryBusiness(4);

            ViewBag.CategoryBusinessIdValue = categoryBusinessId;
            if (GetActionLevel() != null)
            {
                ViewBag.ListActionLevel = GetActionLevel();
            }


            return View(model);
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

        private IEnumerable<DocTypeModel> GetDocTypeModels(DocTypeSearchModel search, string sortBy,
       bool isSortDesc, int page, int pageSize, int? categoryBusinessId = null)
        {
            int totalRecords;
            if (categoryBusinessId != null)
            {
                categoryBusinessId = 64;
            }
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
                .Select(t => new DocTypeModel
                {
                    DocTypeId = t.DocTypeId,
                    DocTypeName = t.DocTypeName,
                    DocTypeCode = t.DocTypeCode,
                    IsActivated = t.IsActivated,
                    DocFieldName = t.DocFieldName,
                    ActionLevel = t.ActionLevel,
                    CompendiumDefault = t.CompendiumDefault
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
        private List<SelectListItem> BindCategoryBusiness(int? categoryBusinessId = null)
        {
            var result = _resourceService.EnumToSelectList<CategoryBusinessTypes>(64);
            return result;
        }
        private void CreateCookieSearch(DocTypeSearchModel search, SortAndPagingModel sortpage, int? categoryBusinessId)
        {
            if (categoryBusinessId != null)
            {
                categoryBusinessId = 64;
            }
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

        private void BindData(DocTypeScheduleType scheduleType = DocTypeScheduleType.HangNam)
        {
            ViewBag.AllScheduleType = GetListScheduleType(scheduleType);
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
        public ActionResult ImportDocTypes()
        {
            return View();
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
                    CategoryBusinessId = 64
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
        private Store CreateNewStore(string storeName, string codePattern)
        {
            var increasement = EnsureIncrease(storeName);
            var code = EnsureCode(storeName, codePattern, increasement.IncreaseId);
            var storeCode = new List<StoreCode>();
            storeCode.Add(new StoreCode() { CodeId = code.CodeId });
            var store = new Store()
            {
                StoreName = storeName,
                CategoryBusinessId = 64,
                StoreCodes = storeCode
            };
            _storeService.Create(store);

            return store;
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
                CategoryBusinessId = 64,
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
        [HttpPost]
        public ActionResult ChangeIsActivateBatch(string sModel, int? categoryBusinessId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive"));
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive"));
            //    return RedirectToAction("Index");
            //}
            if (categoryBusinessId != null)
            {
                categoryBusinessId = 64;
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
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeCreate")]
        public ActionResult CreatePlus(DocTypeFormModel sModel)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

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
                docTypeCreate.CategoryBusinessId = 64;
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

                return Redirect("/Admin/DocTypeGov/EditPlus/" + finalDocTypes.FirstOrDefault().DocTypeId.ToString());
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            LoadDropDownList();
            ReBindDataWhenError();
            PrepareFormModel();
            BindData(sModel.TimeJob.ScheduleTypeEnum);
            return RedirectToAction("Index");
        }
        public ActionResult CreatePlus()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}
            LoadDropDownList();
            ReBindDataWhenError(64);
            PrepareFormModel();

            var sModel = new DocTypeFormModel();
            sModel.DocType = new DocTypeModel() { IsActivated = true, IsAllowOnline = true };
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
        public ActionResult GetManufacturer(string Item)
        {
            var levelId = Convert.ToInt32(Item);
            var offfices = _officeService.GetOfficesByLevelId(levelId);
            return Json(offfices);
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
        public ActionResult EditPlus(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

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

            var model = docType.ToModel();
            model.StoreIdDefault = storeIdDefault;

            var sModel = new DocTypeFormModel();
            sModel.DocType = model;
            sModel.Form = GetFormWith(model);

            LoadDropDownList(docType.LevelId, docType.OfficeId, docType.ActionLevel, sModel.Form.FormId);

            BindFormGroupAndFormType(sModel.Form.FormGroupId, sModel.Form.FormTypeId, sModel.Form.DocTypeId);
            ViewBag.HasTmp = sModel.Form.IsActivated == 3;

            // 20191101 VuHQ REQ-02

            //type table
            var typedatas = new List<string>();
            typedatas = _dataTypeService.Gets(c => c.IsActivated).Select(p => p.nameID).ToList();

            ViewBag.TypeDatas = JsonConvert.SerializeObject(typedatas);

            //type name table
            var typeDataNames = new List<string>();
            typeDataNames = _dataTypeService.Gets(c => c.IsActivated).Select(p => p.dataTypeName).ToList();

            ViewBag.TypeDataNames = JsonConvert.SerializeObject(typeDataNames);

            //type name table
            var localityDataIds = new List<string>();
            localityDataIds = _localityService.Gets(c => c.IsActive).Select(p => p.Id).ToList();

            ViewBag.LocalityDataIds = JsonConvert.SerializeObject(localityDataIds);
            //locality table
            var localityDatas = new List<string>();
            localityDatas = _localityService.Gets(c => c.IsActive).Select(p => p.LocalityName).ToList();

            ViewBag.Localitys = JsonConvert.SerializeObject(localityDatas);
            //end type name table

            // phan to
            var categoryDisaggregationNames = new List<string>();
            categoryDisaggregationNames = _categoryDisaggregationsService.Gets(c => c.IsActivated).Select(p => p.CategoryDisaggregationName).ToList();

            ViewBag.CategoryDisaggregationNames = JsonConvert.SerializeObject(categoryDisaggregationNames);

            var categoryDisaggregationIds = new List<string>();
            categoryDisaggregationIds = _categoryDisaggregationsService.Gets(c => c.IsActivated).Select(p => p.CategoryDisaggregationCode).ToList();

            ViewBag.CategoryDisaggregationIds = JsonConvert.SerializeObject(categoryDisaggregationIds);

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
                var schema = (Dictionary<string, ConfigCompilation>)JsonConvert.DeserializeObject<Dictionary<string, ConfigCompilation>>(formCodeCompilation.summaryConfigJsonForm.schema.ToString());
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

            return View(sModel);
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
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "DocTypeEdit")]
        public ActionResult EditPlus(DocTypeFormModel sModel, FormCollection formCollection)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

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
                model.CategoryBusinessId = 64;
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

                if (!EditForm(sModel, formCollection, out msg, true))
                {
                    ReBindDataWhenError(model.CategoryBusinessId);
                    ErrorNotification(msg);
                    BindData(sModel.TimeJob.ScheduleTypeEnum);
                    return View(sModel);
                }

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.Updated"));
                SuccessNotification(_resourceService.GetResource("Customer.DocType.Updated"));
                return Redirect("/Admin/DocTypeGov/EditPlus/" + docType.DocTypeId.ToString());
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            ReBindDataWhenError(model.CategoryBusinessId);
            BindData(sModel.TimeJob.ScheduleTypeEnum);
            return View(sModel);
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

            return RedirectToAction("DocTypeWorkflowPlus", new { id = id });
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
        [HttpPost]
        public ActionResult Delete(Guid id, int? categoryBusinessId = null)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionDelete"));
            //    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionDelete"));
            //    return RedirectToAction("Index", new { categoryBusinessId = categoryBusinessId });
            //}

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

        private bool EditForm(DocTypeFormModel sModel, FormCollection formCollection, out string msg, bool? IsBCCP)
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
                    var columnSetting = handsonToJson.ConvertHeaderHandsonToJsonExtra(out xoayColumnSetting, IsBCCP);

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
                    form.ConfigFunction = handsonToJson.BuildQuery(header, false);

                    // tự động generate luôn Json (sử dụng để hiển thị form ở mobile)
                    form.Json = JsonConvert.SerializeObject(columnSetting);

                    // 20191216 VuHQ push API phần thay đổi cấu hình START
                    Dictionary<string, string> addList = new Dictionary<string, string>();
                    Dictionary<string, string> modifyList = new Dictionary<string, string>();

                    if (!string.IsNullOrEmpty(sModel.Form.TableName))
                    {
                        if (oldHeader.Count > 0)
                        {
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
                //if (listFile.Count != 0)
                //{
                //    Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>()
                //    {
                //        "Tạo bảng:",
                //        JsonConvert.SerializeObject(listFile)
                //    });
                //    var succress = Post("AutoCreate", listFile);
                //}
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
        [HttpPost]
        public JsonResult ChangeIsPrimary(Guid formid, Guid doctypeid, bool isprimary)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsPrimary"));
            //    return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsPrimary") });
            //}

            _doctypeformService.Create(new DocTypeForm
            {
                DocTypeId = doctypeid,
                FormId = formid,
                IsPrimary = isprimary,
                IsActive = true
            });

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
                compilations.Add("Form_Compilation_Match_Off", new ConfigCompilation { typeOther = "boolean", title = "Sử dụng so sánh", text = "true" });
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
        private Dictionary<string, ConfigCompilation> generateJsonFormGeneralPrefix()
        {
            var compilations = new Dictionary<string, ConfigCompilation>();

            //compilations.Add("Form_GeneralCompilationHeader_TableName", new ConfigCompilation { typeOther = "string", title = "Danh sách bảng wso2", htmlClass = "form-control", enumOther = GetTableNamesBySchema().ToArray() });

            return compilations;
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

        private void ReBindDataWhenError(int? categoryBusinessId = null, List<int> storeIdCheckeds = null, int storeIdDefault = 0)
        {
            ViewBag.AllCategoryBusiness = BindCategoryBusiness(categoryBusinessId);
            //ViewBag.AllCategorys = GetAllCategorys(categoryBusinessId == null ? (int)CategoryBusinessTypes.VbDen : categoryBusinessId.Value);
            ViewBag.AllCategorys = GetAllCategorys(4);

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

           // ViewBag.AllDocFields = GetAllDocFields(categoryBusinessId == null ? (int)CategoryBusinessTypes.VbDen : categoryBusinessId.Value);
            ViewBag.AllDocFields = GetAllDocFields(4);
            ViewBag.AllCodes = GetAllCodes();
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
    }
}