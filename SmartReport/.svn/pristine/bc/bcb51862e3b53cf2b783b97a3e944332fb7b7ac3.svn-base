using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ActiveQueryBuilder.Core;
using ActiveQueryBuilder.Web.Server;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class TemplateKeyPlusController : CustomController// BaseController
    {
        private readonly TemplateKeyBll _templateKeyService;
        private readonly TemplateKeyCategoryBll _templateKeyCategoryService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DocFieldBll _docFieldService;
        private readonly DocTypeBll _docTypeService;
        private readonly FormBll _formService;
        private readonly TemplateHelper _templateHelper;
        public string DEFAULT_SORT_BY = "Name";
        private readonly ReportQueryBll _reportQueryService;
        private readonly ReportQueryGroupBll _reportQueryGroupService;
        public TemplateKeyPlusController(
            ResourceBll resourceService,
            TemplateKeyBll templateKeyService,
            AdminGeneralSettings generalSettings,
            DocFieldBll docFieldService,
            DocTypeBll docTypeService,
            FormBll formService,
            TemplateHelper templateHelper,
            TemplateKeyCategoryBll templateKeyCategory,
            ReportQueryBll reportQueryService,
            ReportQueryGroupBll reportQueryGroupService)
            : base()
        {
            _resourceService = resourceService;
            _templateKeyService = templateKeyService;
            _generalSettings = generalSettings;
            _docFieldService = docFieldService;
            _docTypeService = docTypeService;
            _formService = formService;
            _templateHelper = templateHelper;
            _templateKeyCategoryService = templateKeyCategory;
            _reportQueryService = reportQueryService;
            _reportQueryGroupService = reportQueryGroupService;
        }

        //
        // GET: /Admin/TemplateKey/
        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }
            ViewBag.ListType = GetListTemplateKeysType();
            ViewBag.ListCategory = GetListTemplateKeyCategory();
            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchTemplateKey];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }
            int totalRecords;
            var model = _templateKeyService.GetsAs(out totalRecords, t => new TemplateKeyModel
            {
                TemplateKeyId = t.TemplateKeyId,
                Name = t.Name,
                Code = t.Code,
                IsActive = t.IsActive
            }, pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                keySearch: search,
                currentPage: sortAndPage.CurrentPage);

            sortAndPage.TotalRecordCount = totalRecords;

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }
            GetSpecialKey();
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.KeySearch = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            ViewBag.ListType = GetListTemplateKeysType();
            ViewBag.ListCategory = GetListTemplateKeyCategory();
            var model = new TemplateKeyModel { IsActive = true };
            var qb = CreateQueryBuilder(model.Sql);
            ViewData["ModelQuery"] = qb;
            GetQueryReport();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "TemplateKeyCreateOrEdit")]
        public ActionResult Create(TemplateKeyModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            ViewBag.ListType = GetListTemplateKeysType();
            ViewBag.ListCategory = GetListTemplateKeyCategory();
            model.HtmlTemplate = !string.IsNullOrWhiteSpace(model.HtmlTemplate) ? Microsoft.JScript.GlobalObject.unescape(model.HtmlTemplate) : model.HtmlTemplate;
           // var qb = CreateQueryBuilder(model.Sql);
            //ViewData["ModelQuery"] = qb;
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    entity.Type = (int)TemplateKeyType.Common;
                    _templateKeyService.Create(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TemplateKey.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TemplateKey.Created"));
                    return RedirectToAction("Create");
                }
                catch
                {
                }
            }
            GetQueryReport();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotPermissionEdit"));
                return RedirectToAction("Index");
            }
            ViewBag.ListType = GetListTemplateKeysType();
            ViewBag.ListCategory = GetListTemplateKeyCategory();
            var key = _templateKeyService.Get(id);
            if (key == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotExist"));
                return RedirectToAction("Index");
            }

            key.HtmlTemplate = !string.IsNullOrWhiteSpace(key.HtmlTemplate) ? Microsoft.JScript.GlobalObject.unescape(key.HtmlTemplate) : key.HtmlTemplate;
            var model = key.ToModel();
            //var sql = model.Sql;

            //var qb = CreateQueryBuilder(sql);
            //ViewData["ModelQuery"] = qb;
            GetQueryReport();
            return View(key.ToModel());
        }
        private List<SelectListItem> GetListTemplateKeysType()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(TemplateKeysType));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32((TemplateKeysType)val);
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription((TemplateKeysType)val),
                    Value = itemValue.ToString()
                });
            }
            return result;
        }

        private void GetQueryReport()
        {
            var reportQuery = _reportQueryService.Gets().Select(p => new ReportQueryModel
            {
                ReportQueryId = p.ReportQueryId,
                ReportQueryName = p.ReportQueryName
            });
            var reportQueryGroup = _reportQueryGroupService.Gets().Select(p => new ReportQueryGroupModel
            {
                ReportQueryGroupId = p.ReportQueryGroupId,
                ReportQueryGroupName = p.ReportQueryGroupName
            });
            ViewBag.ReportQuery = reportQuery;
            ViewBag.ReportQueryGroup = reportQueryGroup;
        }
        [HttpPost]
        public JsonResult GenerateReportQueryData(int id, string type, string sql)
        {
            string query = "", dt = "", errMsg = string.Empty;
            var tableId = 0;
            switch (type)
            {
                case "Report":
                    var report = _reportQueryService.Get(id);
                    if (report != null)
                    {
                        query = _reportQueryService.GenerateQueryByGroup(new List<ReportQuery> { report });
                        tableId = report.DataTableId ?? 0;
                        dt = JsonConvert.SerializeObject(ToDictionary(_reportQueryService.GetReportQueryData(query, tableId, out errMsg)));// JsonConvert.SerializeObject(_reportQueryService.GetReportQueryData(query, tableId, out errMsg),Formatting.Indented);//, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        return Json(new { Data = dt, Query = query, Mess = errMsg, JsonRequestBehavior.AllowGet });
                    }
                    else
                        goto case "NotReportQuery";
                case "ReportGroup":
                    var reportQueryGroup = _reportQueryGroupService.Get(id);
                    if (reportQueryGroup == null)
                        goto case "NotReportQuery";
                    var reportQueryIds = reportQueryGroup.Querys.Select(p => p.ReportQueryId).ToList();
                    if (reportQueryIds.Count == 0)
                        goto case "NotReportQuery";

                    var reportTmp = _reportQueryService.Get(reportQueryIds[0]);
                    tableId = reportTmp == null ? 0 : reportTmp.DataTableId ?? 0;

                    // var reportQuerys = _reportQueryService.Gets(p => reportQueryIds.Contains(p.ReportQueryId)).ToList();
                    var reportQuerys = new List<ReportQuery>();
                    foreach (var reportQueryId in reportQueryIds)
                    {
                        reportQuerys.Add(_reportQueryService.Get(reportQueryId));
                    }

                    query = _reportQueryService.GenerateQueryByGroup(reportQuerys);
                    dt = JsonConvert.SerializeObject(ToDictionary(_reportQueryService.GetReportQueryData(query, tableId, out errMsg)));//, Formatting.Indented);//, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    return Json(new { Data = dt, Query = query, Mess = errMsg, JsonRequestBehavior.AllowGet });
                case "NotReportQuery":
                    query = sql;
                    dt = JsonConvert.SerializeObject(ToDictionary(_reportQueryService.GetReportQueryData(query, out errMsg)));//, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    return Json(new { Data = dt, Query = query, Mess = errMsg, JsonRequestBehavior.AllowGet });

            }
            return Json(new { Data = "", Query = "", Mess = errMsg, JsonRequestBehavior.AllowGet });
        }
        public static List<Dictionary<string, object>> ToDictionary(DataTable table)
        {
            var columns = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
            List<Dictionary<string, object>> result;
            if (table.Rows.Count == 0)
            {
                var row = table.Columns.Cast<DataColumn>().ToDictionary<DataColumn, string, object>(col => col.ColumnName, col => "");
                result = new List<Dictionary<string, object>> { row };
            }
            else
                result = table.Rows.Cast<DataRow>()
                    .Select(dr => columns.ToDictionary(c => c, c => dr[c])).ToList();
            return result;
        }
        private IEnumerable<SelectListItem> GetListTemplateKeyCategory()
        {
            var enumValArray = _templateKeyCategoryService.Gets();
            foreach (var val in enumValArray)
            {
                var obj = new SelectListItem
                {
                    Text = val.Name,
                    Value = val.Id.ToString()
                };
                yield return obj;
            }
        }
        private QueryBuilder CreateQueryBuilder(string sql = null)
        {

            var connection = new MySqlConnection(_generalSettings.DashboardConnection);
            // Create an instance of the QueryBuilder object
            var qb = QueryBuilderStore.Get("BootstrapTheming");
            var queryBuilder = qb ?? QueryBuilderStore.Factory.MySql("BootstrapTheming");
            // Denies metadata loading requests from the metadata provider
            //queryBuilder.MetadataLoadingOptions.OfflineMode = true;
            queryBuilder.MetadataProvider = new MySQLMetadataProvider
            {
                Connection = connection
            };
            //Set default query
            queryBuilder.SQL = sql;
            return queryBuilder;
        }
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "TemplateKeyCreateOrEdit")]
        public ActionResult Edit(TemplateKeyModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotPermissionEdit"));
                return RedirectToAction("Index");
            }
            ViewBag.ListType = GetListTemplateKeysType();
            ViewBag.ListCategory = GetListTemplateKeyCategory();
            model.HtmlTemplate = !string.IsNullOrWhiteSpace(model.HtmlTemplate) ? Microsoft.JScript.GlobalObject.unescape(model.HtmlTemplate) : model.HtmlTemplate;
            //var qb = CreateQueryBuilder(model.Sql);
            //ViewData["ModelQuery"] = qb;
            GetQueryReport();
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _templateKeyService.Get(model.TemplateKeyId);
                    if (entity == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotExist"));
                    }
                    else
                    {
                        entity = model.ToEntity(entity);
                        _templateKeyService.Update(entity);
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.Updated.Success"));
                        SuccessNotification(_resourceService.GetResource("Customer.TemplateKey.Updated.Success"));
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TemplateKeyDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var key = _templateKeyService.Get(id);
            if (key == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotExist"));
            }
            try
            {
                _templateKeyService.Delete(key);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Search(string searchKey, int type, int pageSize)
        {
            IEnumerable<TemplateKeyModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(searchKey))
                    {
                        searchKey = searchKey.Trim();
                    }
                    int totalRecords;
                    var typeKey = type == 0 ? null : (int?)type;
                    model = _templateKeyService.GetsAs(out totalRecords, t => new TemplateKeyModel
                    {
                        TemplateKeyId = t.TemplateKeyId,
                        Name = t.Name,
                        Code = t.Code,
                        IsActive = t.IsActive
                    }, pageSize: pageSize,
                        sortBy: DEFAULT_SORT_BY, isDescending: false,
                        keySearch: searchKey, type: typeKey);
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                    CreateCookieSearch(searchKey, sortAndPage);
                    GetSpecialKey(searchKey);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.KeySearch = searchKey;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                }
            }
            return PartialView("_PatialList", model);
        }

        public ActionResult CustomKey()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TemplateKey.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.TemplateKey.NotPermission"));
                return RedirectToAction("Index");
            }

            var docfields = _docFieldService.GetsAs(f => new { f.DocFieldId, f.DocFieldName });
            ViewBag.Docfields = docfields
                .Select(df => new SelectListItem
                {
                    Text = df.DocFieldName,
                    Value = df.DocFieldId.ToString()
                });
            return View("CustomKey");
        }

        public JsonResult GetDocTypes(int docfieldId)
        {
            var doctypes = _docTypeService.GetsAs(dt => new { dt.DocTypeId, dt.DocTypeName }, dt => dt.DocFieldId == docfieldId)
                .Select(dt => new SelectListItem
                {
                    Value = dt.DocTypeId.ToString("N"),
                    Text = dt.DocTypeName
                });
            return Json(new { success = doctypes.StringifyJs(), JsonRequestBehavior.AllowGet });
        }

        public JsonResult GetForms(Guid doctypeId)
        {
            var forms = _formService.GetDynamics(doctypeId)
                .Select(f => new SelectListItem
                {
                    Text = f.FormName,
                    Value = f.FormId.ToString("N")
                });
            return Json(new { success = forms.StringifyJs(), JsonRequestBehavior.AllowGet });
        }

        public JsonResult GetKeys(Guid formId)
        {
            var form = _formService.Get(formId);
            if (form == null)
            {
                return Json(new { success = "[]" }, JsonRequestBehavior.AllowGet);
            }
            var result = new List<TemplateKeyModel>();
            List<JsControl> jsControls;
            if (DynamicFormHelper.TryParse(form.Json, out jsControls))
            {
                foreach (var control in jsControls)
                {
                    if (control.Type == ControlType.Label)
                    {
                        continue;
                    }
                    result.Add(new TemplateKeyModel
                    {
                        //DoctypeId = form.DocTypeId,
                        FormId = formId,
                        Name = control.ControlName,
                        Type = (int)control.Type,
                        KeyIdInForm = control.ControlId,
                        IsActive = true,
                        IsCustomKey = true
                    });
                }
                return Json(new { success = result.StringifyJs() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = "[]" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportKey(string keys)
        {
            try
            {
                var importKeys = Json2.ParseAs<List<TemplateKeyModel>>(keys);
                foreach (var key in importKeys)
                {
                    key.Code = TemplateHelper.GetDefaultCodeCustomKey(key.Name);
                    key.Sql = TemplateHelper.GetDefaultSqlCustomKey(key.Type);
                    key.HtmlTemplate = TemplateHelper.GetDefaultTemplateCustomKey(key.Type);
                    key.Type = (int)TemplateKeyType.Common;
                    _templateKeyService.Create(key.ToEntity());
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SortAndPaging(
            string searchKey, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<TemplateKeyModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(searchKey))
                {
                    searchKey = searchKey.Trim();
                }
                int totalRecords;
                model = _templateKeyService.GetsAs(out totalRecords, t => new TemplateKeyModel
                {
                    TemplateKeyId = t.TemplateKeyId,
                    Name = t.Name,
                    Code = t.Code,
                    IsActive = t.IsActive
                }, pageSize: pageSize,
                    sortBy: sortBy, isDescending: isSortDesc,
                    keySearch: searchKey, currentPage: page);
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };

                CreateCookieSearch(searchKey, sortAndPage);
                GetSpecialKey();

                ViewBag.SortAndPage = sortAndPage;
                ViewBag.KeySearch = searchKey;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            return PartialView("_PatialList", model);
        }

        #region private methods

        private void CreateCookieSearch(string keySearch, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", keySearch }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchTemplateKey];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchTemplateKey, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private void GetSpecialKey(string keySearch = "")
        {
            var specials = _templateHelper.GetSpecials().ToListModel();

            var questionKeys = _templateHelper.GetQuestionKeys().ToListModel();
            specials = questionKeys.Concat(specials);

            var onlineKeys = _templateHelper.GetDocumentOnlines().ToListModel();
            specials = onlineKeys.Concat(specials);

            var commonKeys = _templateHelper.GetCommonKeys().ToListModel();
            specials = commonKeys.Concat(specials);

            ViewBag.SpecialKeys = specials.Where(x => x.Name.ToLower().Contains(keySearch.ToLower()));
        }

        #endregion

        #region class

        private class ImportKeyElement
        {
            public Guid FormId { get; set; }

            public Guid KeyId { get; set; }

            public string Name { get; set; }
        }

        #endregion
    }
}
