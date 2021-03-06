using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using Bkav.eGovCloud.Core.FileSystem;
using System.Text;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Business;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using ActiveQueryBuilder.Core;
using ActiveQueryBuilder.Web.Server;
using ActiveQueryBuilder.Core.QueryTransformer;
using MySql.Data.MySqlClient;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
	[EgovAuthorize]
	//[RequireHttps]
	public class FormController : CustomController
	{
		private readonly DocTypeBll _docTypeService;
		private readonly FormBll _formService;
		private readonly DocFieldBll _docfieldService;
		private readonly DocTypeFormBll _doctypeFormService;
		private readonly CatalogBll _catalogService;
        private readonly CatalogValueBll _catalogValueService;
        private readonly ExtendFieldBll _exfieldService;
        private readonly ResourceBll _resourceService;
        private readonly FormGroupBll _formgroupService;
        // 20191101 VuHQ REQ-02
        private readonly ConfigTypeBll _configTypeService;
        private readonly DepartmentBll _departmentService;

        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileUploadSettings _fileUploadSettings;

        private const string DEFAULT_SORT_BY = "IsActivated";

        public FormController(
            DocTypeBll docTypeBll,
            FormBll formService,
            DocFieldBll docfieldService,
            CatalogBll catalogService,
            ExtendFieldBll exfieldService,
            ResourceBll resourceService,
            FormGroupBll formgroupservice,
            DocTypeFormBll doctypeFormService,
            AdminGeneralSettings generalSettings,
			FileUploadSettings fileUploadSettings,
            ConfigTypeBll configTypeService,
            DepartmentBll departmentService,
            CatalogValueBll catalogValueService)
            : base()
        {
            _docTypeService = docTypeBll;
            _formService = formService;
            _docfieldService = docfieldService;
            _catalogService = catalogService;
            _exfieldService = exfieldService;
            _resourceService = resourceService;
            _formgroupService = formgroupservice;
            _generalSettings = generalSettings;
            _fileUploadSettings = fileUploadSettings;
            _doctypeFormService = doctypeFormService;
            _configTypeService = configTypeService;
            _departmentService = departmentService;
            _catalogValueService = catalogValueService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var search = new FormSearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchForm];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<FormSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _formService.GetsAs(out totalRecords, f => new FormModel
            {
                FormId = f.FormId,
                FormName = f.FormName,
                FormTypeId = f.FormTypeId,
                EmbryonicPath = f.EmbryonicPath,
                IsActivated = f.IsActivated,
                Description = f.Description
            }, pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                currentPage: sortAndPage.CurrentPage,
                formGroupId: search.FormGroupId,
                formTypeId: search.FormTypeId, search: search.SearchKey, docTypeId: search.DocTypeId);
            sortAndPage.TotalRecordCount = totalRecords;

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.Search = search;
            BindFormGroupAndFormType(search.FormGroupId, search.FormTypeId, search.DocTypeId, search.DocFieldId);
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return View(model);
        }

        private void BindFormGroupAndFormType(int? formGroupId = null, int? formTypeId = null, Guid? docTypeId = null, int? docFieldId = 0)
        {
            ViewBag.FormGroupId =
                _formgroupService.Gets()
                    .Select(
                        f =>
                            new SelectListItem
                            {
                                Selected = formGroupId.HasValue && formGroupId.Value == f.FormGroupId,
                                Text = f.FormGroupName,
                                Value = f.FormGroupId.ToString()
                            });

            ViewBag.FormTypeId = _formService.GetTypes().Select(
                f =>
                    new SelectListItem
                    {
                        Selected = formTypeId.HasValue && formTypeId.Value == f.FormTypeId,
                        Text = f.FormTypeName,
                        Value = f.FormTypeId.ToString()
                    });

            var doctypes = _docTypeService.GetAllFromCache();
            ViewBag.DocTypeId = doctypes.Where(dt => !docFieldId.HasValue || (dt.DocFieldId.HasValue && dt.DocFieldId.Value == docFieldId.Value))
                    .Select(dt => new SelectListItem()
                    {
                        Text = dt.DocTypeName,
                        Value = dt.DocTypeId.ToString(),
                        Selected = docTypeId.HasValue && docTypeId.Value.Equals(dt.DocTypeId)
                    });

            var docfields = doctypes.Where(df => df.DocFieldId.HasValue).Select(dt => new
            {
                DocFieldId = dt.DocFieldId,
                DocFieldName = dt.DocFieldName
            });

            ViewBag.DocFieldId = docfields.Distinct().Select(df => new SelectListItem()
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

        public ActionResult SortAndPaging(string sortBy,
                                                bool isSortDesc,
                                                int page,
                                                int pageSize,
                                                int? formGroupId,
                                                int? formTypeId
                                          )
        {
            int totalRecords;
            var model = _formService.GetsAs(out totalRecords, f => new FormModel
            {
                FormId = f.FormId,
                FormName = f.FormName,
                FormTypeId = f.FormTypeId,
                EmbryonicPath = f.EmbryonicPath,
                IsActivated = f.IsActivated,
                Description = f.Description
            }, pageSize: pageSize, sortBy: sortBy, isDescending: isSortDesc, currentPage: page, formGroupId: formGroupId,
                formTypeId: formTypeId);

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                IsSortDescending = isSortDesc,
                SortBy = sortBy,
                TotalRecordCount = totalRecords
            };

            var search = new FormSearchModel
            {
                FormGroupId = formGroupId,
                FormTypeId = formTypeId
            };
            CreateCookieSearch(search, sortAndPage);

            var allformgroup = _formgroupService.Gets();
            ViewBag.AllFormGroup = allformgroup.ToListModel();

            ViewBag.FormTypes = _formService.GetTypes();

            ViewBag.Search = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            return PartialView("_PartialList", model);
        }

        public ActionResult GetForm(FormSearchModel search, int pagesize)
        {
            var sortAndPage = new SortAndPagingModel
            {
                CurrentPage = 1,
                PageSize = pagesize,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY,
            };

            int totalRecords;
            var model =
                _formService.GetsAs(out totalRecords, f => new FormModel
                {
                    FormId = f.FormId,
                    FormName = f.FormName,
                    FormTypeId = f.FormTypeId,
                    EmbryonicPath = f.EmbryonicPath,
                    IsActivated = f.IsActivated,
                    Description = f.Description
                }, pageSize: sortAndPage.PageSize, sortBy: sortAndPage.SortBy,
                    isDescending: sortAndPage.IsSortDescending,
                    formGroupId: search.FormGroupId, formTypeId: search.FormTypeId, search: search.SearchKey, docTypeId: search.DocTypeId);

            sortAndPage.TotalRecordCount = totalRecords;

            CreateCookieSearch(search, sortAndPage);

            ViewBag.Search = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            return PartialView("_PartialList", model);
        }

        /// <summary>
        /// Tạo biểu mẫu mới cho loại hồ sơ: chỉ bao gồm các thông tin cơ bản.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var httpCookie = Request.Cookies[CookieName.SearchForm];
            var search = new FormSearchModel();
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                search = Json2.ParseAs<FormSearchModel>(data["Search"].ToString());
            }

            //BindFormGroupAndFormType(search.FormGroupId, search.FormTypeId, search.DocTypeId, search.DocFieldId);
            // default value của loại form là : 2 (Form động)
            BindFormGroupAndFormType(search.FormGroupId, 2, search.DocTypeId, search.DocFieldId);

            return View(new FormModel());
        }

        [HttpPost]
        public ActionResult Create(FormModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var form = model.ToEntity();
                form.FormTypeId = model.FormTypeId;
                form.IsPrimary = model.IsPrimary;
                form.CreatedByUserId = User.GetUserId();
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

                        _formService.Create(list);
                    }
                    else
                    {
                        form.Template = "f_" + RandomHelper.RandomString(15, false);
                        _formService.Create(form);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Created"));
                    // ViewData = null;
                    BindFormGroupAndFormType(model.FormGroupId, model.FormTypeId, model.DocTypeId);
                    return View(new FormModel());
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            BindFormGroupAndFormType(model.FormGroupId, model.FormTypeId, model.DocTypeId);
            return View(model);
        }

        /// <summary>
        /// Chỉnh sửa các thông tin cơ bản của biểu mẫu.
        /// </summary>
        /// <param name="id">The form guid id.</param>
        /// <returns>View</returns>
        public ActionResult Edit(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var form = _formService.Get(id);
            if (form == null)
            {
                return RedirectToAction("Index");
            }
            BindFormGroupAndFormType(form.FormGroupId, form.FormTypeId);

            // 20191101 VuHQ REQ-02
            var catalogs = _catalogService.Gets(c => c.IsActivated)
                .Select(p => p.CatalogName).ToList();

            // add catalog mở rộng: Organization (danh sách các department trực thuộc)
            catalogs.Add("Organization");
            ViewBag.Catalogs1 = JsonConvert.SerializeObject(catalogs);

            Expression<Func<ConfigType, bool>> spec = d => d.ParentId == null && d.IsActivated == true;
            List<ConfigType> configTypes = _configTypeService.Gets(spec).ToList();

            ViewBag.ConfigTypes =
                configTypes
                    .Select(
                        configType =>
                            new ConfigTypeModel
                            {
                                SubConfigTypes = _configTypeService.Gets(subConfigType => subConfigType.ParentId == configType.TypeId).ToList(),
                                TypeCode = configType.TypeCode,
                                DisplayCode = configType.DisplayCode,
                                PatternCode = configType.PatternCode
                            });
            ViewBag.HasTmp = form.IsActivated == 3;
            return View(form.ToModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var form = _formService.Get(model.FormId);
                if (form == null)
                {
                    return RedirectToAction("Index");
                }

                //model.EmbryonicLocationName = form.EmbryonicLocationName;
                //model.EmbryonicPath = form.EmbryonicPath;

                form = model.ToEntity(form);
                form.Template = "f_" + RandomHelper.RandomString(15, false);
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
                    dynamic defineValueObject = JsonConvert.DeserializeObject(model.DefineValueJson);

                    var handsonToJson = new HandsonToJson(defineFieldObject, defineConfigObject, defineValueObject);

                    // 20191225 VuHQ Cù Trọng Xoay
                    var xoayHeader = new Dictionary<string, string>();
                    header = handsonToJson.ConvertHeaderHandsonToJson(out xoayHeader);

                    // 20191225 VuHQ Cù Trọng Xoay
                    Dictionary<string, HeaderObject> xoayColumnSetting = new Dictionary<string, HeaderObject>();
                    var columnSetting = handsonToJson.ConvertHeaderHandsonToJsonExtra(out xoayColumnSetting, false);

                    // generate data
                    var data = handsonToJson.ConvertHandsonToJson(header, false);
                    var headerNested = defineFieldObject.mergedCells;

                    // 20191225 VuHQ Cù Trọng Xoay
                    var xoayData = new List<Object>();
                    if (defineValueObject.xoayInfo != null)
                        xoayData = handsonToJson.ConvertHandsonToJson(xoayHeader, true);

                    var extra = JsonConvert.SerializeObject(new 
                    { 
                        columnSetting = columnSetting,
                        headerSetting = defineFieldObject.data,
                        mergedCells = defineValueObject.mergedCells,
                        hiddenColumns = defineConfigObject.hiddenColumns,
                        //xoayHeaders = defineValueObject.xoayHeaders,
                        //xoayHeadersAscii = handsonToJson.XoayHeaders == null ? null : handsonToJson.XoayHeaders.Select(p => p.Value).ToList(),
                        //xoayInfo = defineValueObject.xoayInfo,
                        xoayObject = handsonToJson.XoayObject
                        //xoayColumnSetting = xoayColumnSetting,
                        //xoayData = xoayData
                    });
                    form.FormCode = JsonConvert.SerializeObject(new { 
                        header = header, data = data,
                        headerNested = headerNested, 
                        extra = JsonConvert.DeserializeObject(extra), 
                        colWidths = defineFieldObject.colWidths, 
                        mergedCells = defineValueObject.mergedCells,
                        readOnlys = defineValueObject.readOnlys,
                        classCells = defineValueObject.classCells
                    });

                    // tự động generate luôn Json (sử dụng để hiển thị form ở mobile)
                    form.Json = JsonConvert.SerializeObject(columnSetting);
                }
                // 20191114 VuHQ REQ-2 END
                _formService.Update(form);
                return RedirectToAction("Index");
            }
            BindFormGroupAndFormType(model.FormGroupId, model.FormTypeId);
            return View(model);
        }

        /// <summary>
        /// Xóa một biểu mẫu.
        /// </summary>
        /// <param name="id">The form guid id.</param>
        /// <param name="dtype">The doctype guid id.</param>
        /// <returns>Redirect to action => Index</returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken(Salt = "FormDelete")]
        public ActionResult Delete(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionDelete"));
                return RedirectToAction("Index", "Welcome");
            }

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
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Cấu hình biểu mẫu động.
        /// </summary>
        /// <param name="id">The form guid id.</param>
        /// <returns>View</returns>
        public ActionResult ConfigForm(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionConfigForm"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionConfigForm"));
                return RedirectToAction("Index");
            }

            var json = ViewBag.Json = _formService.GetJson(id);
            ViewBag.FormId = id;
            var controls = string.IsNullOrEmpty(json) ? null : DynamicFormHelper.Parse(json) as List<JsControl>;
            ViewBag.MaxRow = controls == null ? 10 : controls.Select(r => r.PosRow).Max();
            var form = _formService.Get(id);
            ViewBag.IsTemp = form != null && form.IsActivated == 3;
            ViewBag.DocFields = _docfieldService.GetsAs(f => new { f.DocFieldId, f.DocFieldName }, f => f.IsActivated)
                                    .Select(f => new SelectListItem
                                    {
                                        Value = f.DocFieldId.ToString(),
                                        Text = f.DocFieldName
                                    });
            ViewBag.Catalogs = _catalogService.Gets(c => c.IsActivated)
                .Select(c => new SelectListItem
                {
                    Value = c.CatalogId.ToString(),
                    Text = c.CatalogName
                });

            var catalogUseds = string.IsNullOrEmpty(json)
                                    ? new List<Guid>()
                                    : DynamicFormHelper.GetCatalogIds(json);
            ViewBag.CatalogUseds = JsonConvert.SerializeObject(catalogUseds);// catalogUseds.StringifyJs(false);//CuongNT - 080813: JsonConvert.SerializeObject(catalogUseds);

            var exfieldsUseds = string.IsNullOrEmpty(json)
                                    ? new List<Guid>()
                                    : DynamicFormHelper.GetExtendFieldIds(json);
            ViewBag.ExfieldUseds = JsonConvert.SerializeObject(exfieldsUseds); //exfieldsUseds.StringifyJs(false);//CuongNT - 080813: JsonConvert.SerializeObject(exfieldsUseds);
                                                                               //var doctype = _docTypeService.Get(dtype);
                                                                               //ViewBag.DoctypeName = doctype == null ? string.Empty : doctype.DocTypeName;
            return View();
        }

        public ActionResult ConfigFormDoctype(Guid id)
        {
            var forms = _doctypeFormService.GetForms(d => new
            {
                d.Form.FormId,
                d.Form.FormName,
                d.Form.FormTypeId,
                d.IsPrimary,
                d.Form.EmbryonicPath,
                d.Form.EmbryonicLocationName,
                d.Form.Template
            }, id).ToList();

            var form = forms.First();

            ViewBag.Key = form.Template;
            ViewBag.TemplatePath = "EmbryonicForm/" + form.EmbryonicLocationName;
            ViewBag.TemplateName = form.EmbryonicPath;

            return View("eReport1");
        }


        public JsonResult CreateFormMobile(Guid id)
        {
            var form = _formService.Get(id);
            var path = CommonHelper.MapPath("~/EmbryonicForm/" + form.EmbryonicLocationName);
            Dictionary<string, object> header = null;
            var name = "";
            using (Stream str = System.IO.File.Open(path, FileMode.Open))
            {
                var xlsxParser = new XlsxToJson(str);
                header = xlsxParser.ConvertHeaderXlsxToFormMobile();

                form.Json = JsonConvert.SerializeObject(header);
                _formService.Update(form);
            }

            return Json(new { error = false }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateETL(Guid id, string comment = "")
        {

            var form = _formService.Get(id);
            var path = CommonHelper.MapPath("~/EmbryonicForm/" + form.EmbryonicLocationName);
            Dictionary<string, string> header = null;
            var name = "";
            using (Stream str = System.IO.File.Open(path, FileMode.Open))
            {
                var xlsxParser = new XlsxToJson(str);
                header = xlsxParser.ConvertHeaderXlsxToJson();
                var isHeader = xlsxParser.GetHeaderAI();
                if (string.IsNullOrEmpty(isHeader))
                {
                    return Json(new { succress = false, message = "Không có dòng ngăn cách dữ liệu" }, JsonRequestBehavior.AllowGet);
                }
                var data = xlsxParser.ConvertXlsxToJson();
                var headerNested = xlsxParser.HeaderToJson();
                var dataHeader = xlsxParser.GetDataHeader();
                var headerMerge = xlsxParser.GetAddressMegre();
                form.FormCode = JsonConvert.SerializeObject(new { header = header, data = data, headerNested = headerNested, dataHeader = dataHeader, headerMerge = headerMerge });

                name = form.EmbryonicPath;
            }
            var keyBC = "yearkeyhalfkeyquarterkeymonthkeyweekkeydatekeyminutekey";
            // Check tên theo định dạng
            if (!name.Contains("##"))
            {
                return Json(new { succress = false, message = "Đề nghị tải file có trên đúng định dạng {Kỳ báo cáo}##{Tên file}" }, JsonRequestBehavior.AllowGet);
            }

            var strArr = name.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
            var listFile = new List<JsonFile>();
            var typeTime = "yearkey";
            if (strArr.Length > 1)
            {
                typeTime = strArr[0];
                name = strArr[1].Replace(".xlsx", "");

                if (!keyBC.Contains(typeTime))
                {
                    return Json(new { succress = false, message = "kỳ báo cáo không đúng định dạng" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                name = name.Replace(".xlsx", "");
            }

            for (int i = 0; i < header.Count; i++)
            {
                var column = header.ElementAt(i);
                if (column.Key == "!!")
                {
                    return Json(new { succress = false, message = "Không cho phép cột bị trống" }, JsonRequestBehavior.AllowGet);
                }

            }

            if (!string.IsNullOrEmpty(comment))
            {
                name = comment;
            }
            var nameFileJson = new JsonFile();
            nameFileJson.filename = XlsxToJson.ConvertToAscii(name) + "!!" + name;
            nameFileJson.typetime = typeTime;
            nameFileJson.column = header;
            nameFileJson.dbname = _generalSettings.BITranports;
            listFile.Add(nameFileJson);

            Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>()
            {
                "Tạo bảng:",
                JsonConvert.SerializeObject(listFile)
            });

            var succress = Post("AutoCreate", listFile);

            form.EmbryonicPath = name;

            _formService.Update(form);

            return Json(new { succress = succress, message = "Lỗi tạo form" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Tao table base tren form handsontable
        /// </summary>
        /// <returns></returns>
        public JsonResult CreateETLPlus(Guid id, string comment = "", string actionLevel = "")
        {
            var listFile = new List<JsonFile>();
            var form = _formService.Get(id);

            // get timekey name
            var timeKey = GetActionLevel().Where(p => p.Value.Equals(actionLevel)).FirstOrDefault();

            Dictionary<string, string> header = null;
            var name = "";

            
            if (form.FormCode == null) {
                return Json(new { succress = false, message = "Dữ liệu trên trong cấu hình không có" }, JsonRequestBehavior.AllowGet);
            }else {
                dynamic formCode = JsonConvert.DeserializeObject(form.FormCode);
                header = (Dictionary<string, string>)formCode.header.ToObject<Dictionary<string, string>>();
            }

            if (header == null)
            {
                return Json(new { succress = false, message = "Không cho phép cột bị trống" }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(comment))
            {
                return Json(new { succress = false, message = "Tên bảng chưa được định nghĩa" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                name = comment;
            }

            var asciiName = XlsxToJson.ConvertToAscii(name);
            if (asciiName.Length > 64)
            {
                return Json(new { succress = false, message = "Tên table không thể vượt quá 64 ký tự." }, JsonRequestBehavior.AllowGet);
            }

            var nameFileJson = new JsonFile();
            nameFileJson.filename = asciiName + "!!" + name;
            nameFileJson.typetime = timeKey.Key;
            nameFileJson.column = header;
            nameFileJson.dbname = _generalSettings.BITranports;

            listFile.Add(nameFileJson);

            Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>()
            {
                "Tạo bảng:",
                JsonConvert.SerializeObject(listFile)
            });

            var succress = Post("AutoCreate", listFile);

            return Json(new { succress = succress, message = "Lỗi tạo form" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RenderFormCode()
        {
            var forms = _formService.Gets(null, false);
            foreach (var form in forms)
            {
                try
                {
                    var path = CommonHelper.MapPath("~/EmbryonicForm/" + form.EmbryonicLocationName);
                    using (Stream str = System.IO.File.Open(path, FileMode.Open))
                    {
                        var xlsxParser = new XlsxToJson(str);
                        var header = xlsxParser.ConvertHeaderXlsxToJson();
                        var data = xlsxParser.ConvertXlsxToJson();
                        var headerNested = xlsxParser.HeaderToJson();
                        var dataHeader = xlsxParser.GetDataHeader();
                        var headerMerge = xlsxParser.GetAddressMegre();
                        form.FormCode = JsonConvert.SerializeObject(new { header = header, data = data, headerNested = headerNested, dataHeader = dataHeader, headerMerge = headerMerge });

                        _formService.Update(form);
                    }
                }
                catch (Exception ex)
                {
                    form.FormCode = JsonConvert.SerializeObject(ex.Message);

                    _formService.Update(form);
                }
            }

            return Json(new { succress = true, message = "Lỗi tạo form" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RenderFormCodeId(Guid id)
        {
            var form = _formService.Get(id);

            try
            {
                var path = CommonHelper.MapPath("~/EmbryonicForm/" + form.EmbryonicLocationName);
                using (Stream str = System.IO.File.Open(path, FileMode.Open))
                {
                    var xlsxParser = new XlsxToJson(str);
                    var header = xlsxParser.ConvertHeaderXlsxToJson();
                    var data = xlsxParser.ConvertXlsxToJson();
                    var headerNested = xlsxParser.HeaderToJson();
                    var dataHeader = xlsxParser.GetDataHeader();
                    var headerMerge = xlsxParser.GetAddressMegre();
                    form.FormCode = JsonConvert.SerializeObject(new { header = header, data = data, headerNested = headerNested, dataHeader = dataHeader, headerMerge = headerMerge });

                    _formService.Update(form);
                }
            }
            catch (Exception ex)
            {
                form.FormCode = JsonConvert.SerializeObject(ex.Message);

                _formService.Update(form);
            }

            return Json(new { succress = true, message = "Lỗi tạo form" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cấu hình biểu mẫu html.
        /// </summary>
        /// <param name="id">The form guid id.</param>
        /// <returns>View</returns>
        public ActionResult ConfigTemplate(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionConfigTemplate"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionConfigTemplate"));
                return RedirectToAction("Index");
            }

            var form = _formService.Get(id);
            if (form == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.FormId = id;
            //ViewBag.DoctypeId = form.DocTypeId;
            ViewBag.FormName = form.FormName;
            ViewBag.Contents = form.Template ?? string.Empty;
#if DEBUG
            return View("ConfigTemplateRelease", form);
#else
            return View("ConfigTemplateRelease", form);
#endif

        }

        public ActionResult eReport(Guid id)
        {
            var form = _formService.Get(id);
            if (form == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Key = form.Template;
            ViewBag.TemplatePath = "EmbryonicForm/" + form.EmbryonicLocationName;
            ViewBag.TemplateName = form.EmbryonicPath;
            return View("eReport2");
        }

        public ActionResult ConfigReport()
        {
            var qb = QueryBuilderStore.Get("BootstrapTheming");

            if (qb == null)
                qb = CreateQueryBuilder();

            return View(qb);
        }

        [HttpPost]
        public JsonResult GetDataConfigReport(string sql)
        {
            var data = Execute(sql);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        private IEnumerable<IDictionary<string, object>> Execute(string sql)
        {
            var data = _formService.GetDataForm(sql, null);
            return data;
        }

        public ActionResult CopyForm()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionCopyForm"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionCopyForm"));
                return RedirectToAction("Index");
            }

            var allformgroup = _formgroupService.Gets();
            ViewBag.AllFormGroup = allformgroup.ToListModel();
            ViewBag.FormGroups = allformgroup.Select(f => new SelectListItem { Value = f.FormGroupId.ToString(), Text = f.FormGroupName });
            return View();
        }

        public ActionResult ConfigEmbryonic(Guid formId)
        {
            var form = _formService.Get(formId);
            if (form == null)
            {
                return Content("Form mẫu không tồn tại");
            }

            List<JsControl> controls;
            if (!DynamicFormHelper.TryParse(form.Json, out controls))
            {
                controls = new List<JsControl>();
            }

            ViewBag.Controls = controls.Where(c => c.TypeId == 9 || c.TypeId == 10).Select(c => new SelectListItem()
            {
                Text = c.ControlName,
                Value = string.Format("#{0}_{1}#", c.ControlId.To10Char(), ParseControlNameForConfig(c.ControlName))
            });

            return View(controls);
        }

        private string ParseControlNameForConfig(string controlName)
        {
            return controlName.StripDelimiters().StripHtml().StripChars(new char[] { ':', ' ' })
                .StripVietnameseChars();
        }

        [HttpPost]
        public JsonResult CopyForm(string name, string des, string formId, string formgroupId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionCopyForm"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionCopyForm"));
                return new JsonResult { Data = false };
            }

            Guid fId;
            int groupId;
            if (name != string.Empty && Guid.TryParse(formId, out fId) && int.TryParse(formgroupId, out groupId))
            {
                try
                {
                    if (_formService.Copy(fId, name, des, groupId))
                    {
                        return new JsonResult { Data = true };
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Form.NotCopy") + ex.Message);
                    ErrorNotification(_resourceService.GetResource("Common.Form.NotCopy") + ex.Message);
                }
            }
            return new JsonResult { Data = false };
        }

        [HttpPost]
        public JsonResult Save(string json, Guid formid)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionSave"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionSave"));
                return new JsonResult { Data = false };
            }

            try
            {
                var result = _formService.UpdateForm(formid, json);
                return new JsonResult { Data = result };
            }
            catch
            {
                return new JsonResult { Data = false };
            }
        }

        [HttpPost]
        public JsonResult GetDoctypes(int docfieldId)
        {
            var doctypes = _docTypeService.GetsAs(dt => new { dt.DocTypeId, dt.DocTypeName }, dt => dt.DocFieldId == docfieldId && dt.IsActivated);
            var result = "{}";
            if (doctypes.Any())
            {
                var doctypeLst = doctypes.Select(dt => new SelectListItem { Value = dt.DocTypeId.ToString(), Text = dt.DocTypeName });
                result = doctypeLst.StringifyJs();//CuongNT - 080813: JsonConvert.SerializeObject(doctypeLst);
            }
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public JsonResult GetExfields(string doctypeId)
        {
            return new JsonResult { Data = "{}" };
        }

        [HttpPost]
        public JsonResult GetForms(string formgroupId)
        {
            int groupid;
            if (int.TryParse(formgroupId, out groupid))
            {
                var forms = _formService.GetsAs(f => new { f.FormId, f.FormName }, f => f.FormGroupId == groupid);
                if (forms.Any())
                {
                    var formList = forms.Select(f => new SelectListItem { Value = f.FormId.ToString(), Text = f.FormName });
                    return new JsonResult { Data = formList.StringifyJs() };//CuongNT - 080813: JsonConvert.SerializeObject(formList) };
                }
            }
            return new JsonResult { Data = "{}" };
        }

        [HttpPost]
        public JsonResult SaveTemplate(string content, Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermissionSaveTemplate"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermissionSaveTemplate"));
                return new JsonResult { Data = false };
            }

            var form = _formService.Get(id);
            if (form == null)
            {
                return new JsonResult { Data = false };
            }
            form.Template = Microsoft.JScript.GlobalObject.unescape(content);
            _formService.Update(form);
            return new JsonResult { Data = true };
        }

        public ActionResult Import()
        {
            ViewBag.Forms = _formService.GetAllDynamic(false);
            return View();
        }

        [HttpPost]
        public ActionResult Import(Guid formId, HttpPostedFileBase importFile)
        {
            var content = DynamicFormParser.ParseFromDocxNew(importFile.InputStream);
            var form = _formService.Get(formId);
            if (form != null)
            {
                form.Json = content;
                _formService.Update(form);
                CreateActivityLog(ActivityLogType.Admin, "Import file thành công");
            }

            ViewBag.Forms = _formService.GetAllDynamic(false);
            return View();
        }

        public ActionResult ImportData()
        {
            ViewBag.Forms = _formService.GetAllDynamic(false);
            return View();
        }

        [HttpPost]
        public ActionResult ImportData(Guid formId, HttpPostedFileBase importFile, string key)
        {
            var form = _formService.Get(formId);
            var formName = form.EmbryonicPath.Replace(".xlsx", "");
            var fileName = XlsxToJson.ConvertToAscii(formName);
            var xlsxParser = new XlsxToJson(importFile.InputStream);
            var listData = xlsxParser.ConvertXlsxToJsonGroupByKey(1, 0 , 2, 1, 1, key);

            var keyMin = int.Parse(listData.Min(d => d[key]).ToString());
            var keyMax = int.Parse(listData.Max(d => d[key]).ToString());
            for (int i = keyMin; i <= keyMax; i++)
            {
                Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>() { i.ToString() });
                var dataExcel = listData.Where(d => int.Parse(d[key].ToString()) == i);
                if (dataExcel != null && dataExcel.Any())
                {
                    var result = new LgspDataReportModel()
                    {
                        type = fileName,

                        data = new List<LgspDataReportModelArray>()
                       {
                          new LgspDataReportModelArray()
                          {

                                organizekey = "000.00.00.H49",
                                datekey = i.ToString(),
                                yearkey = i.ToString(),
                                halfkey = i.ToString(),
                                quarterkey = i.ToString(),
                                monthkey = i.ToString(),
                                weekkey =i.ToString(),
                                minutekey = DateTime.Now.ToString("yyyyMMddHHmm"),
                                status =  1,
                                   array = new LgspDataReportArray()
                                   {
                                       rows = dataExcel.ToList()
                                   }
                          }
                       }
                    };

                    var client = new HttpClient();

                    var appSettings = ConfigurationManager.AppSettings;
                    var url = appSettings["dashboardUrl"];
                    PostData("", result, client, url);
                }

            }
            return Redirect("ImportData");
        }
        public ActionResult ConverteGateForm()
        {
            try
            {
                _formService.ConvertFromEgateForm();
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Form.Convert.eGateForm.Success"));
                SuccessNotification(_resourceService.GetResource("Form.Convert.eGateForm.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, string.Format("{0}: {1}", _resourceService.GetResource("Form.Convert.eGateForm.Error"), ex.Message));
                ErrorNotification(string.Format("{0}: {1}", _resourceService.GetResource("Form.Convert.eGateForm.Error"), ex.Message));
            }

            return RedirectToAction("Index");
        }

        public async Task<bool> PostTask(string action, dynamic data)
        {
            var client = new HttpClient();
            var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
            var responseMessage = client.PostAsync("", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseMessage.Content.ReadAsStringAsync().Result);
                var isSuccess = (bool)result["success"];
                return isSuccess;
            }

            return false;
        }

        private bool PostData(string action, dynamic data, HttpClient client, string url)
        {
            
            Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>()
            {
                JsonConvert.SerializeObject(url)
            });
            Bkav.eGovCloud.Core.Logging.StaticLog.Log(new List<string>() { JsonConvert.SerializeObject(data) });

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
            if (data != null)
            {
                var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
                client.PostAsync("", content);
            }

            return false;
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

        // VuHQ 20191111 REQ-2 START
        public JsonResult getHeaderTemplates()
        {
            HandsonToJson handsonToJson = new HandsonToJson();
            return Json(new { headerTemplates = JsonConvert.SerializeObject(handsonToJson.GetHeaderTemplates()) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RenderConfigReport()
        {
            ViewBag.Catalogs = _catalogService.Gets(c => c.IsActivated)
                .Select(c => new SelectListItem
                {
                    Text = c.CatalogName
                });
            // do whatever you need to get your model
            return PartialView("_ConfigReport", new FormModel());
        }

        public JsonResult GetCatalogValues(string catalogName, int row, int col, string formId, bool isXoay, string[] catalogNames)
        {
            var catalogValues = new List<string>();
            var catalogValuesAscii = new List<string>();
            var catalogInfos = new List<CatalogValue>();
            var departments = new List<KeyValuePair<string, string>>();

            bool isExistCatalog = false;

            var form = _formService.Get(Guid.Parse(formId));
            if (!isXoay && form != null && form.DefineConfigJson != null)
            {
                dynamic defineConfigJson = JsonConvert.DeserializeObject(form.DefineConfigJson);
                if (defineConfigJson.catalogDefaults != null)
                {
                    foreach (var item in defineConfigJson.catalogDefaults)
                    {
                        if (int.Parse(item["row"].ToString()) == row && int.Parse(item["col"].ToString()) == col)
                        {
                            catalogValues = item["source"].ToObject<List<string>>();
                            isExistCatalog = true;
                            break;
                        }
                    }
                }
            }

            if (!isExistCatalog)
            {
                Department _department = new Department();

                if (catalogName.Equals("Organization"))
                {
                    var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();

                    if (currentDepartment != null)
                    {
                        var _departments = _departmentService.GetChildrens(currentDepartment.DepartmentId, true).Select(p => new KeyValuePair<string, string> (p.Emails, p.DepartmentName)).ToList();
                        if (_departments.Count == 0)
                        {
                            catalogValues.Add(currentDepartment.DepartmentName);
                            departments.Add(new KeyValuePair<string, string> (currentDepartment.Emails, currentDepartment.DepartmentName));
                        }
                        else
                        {
                            departments = _departments;
                            catalogValues = _departments.Select(p => p.Value).ToList();
                        }
                    }
                }
                else
                {
                    var catalogModel = _catalogService.Gets(c => c.CatalogName == catalogName);
                    var enumerable = catalogModel as Catalog[] ?? catalogModel.ToArray();
                    if (enumerable.Any() && enumerable.First().CatalogValues.Count > 0)
                        catalogValues = enumerable.First().CatalogValues.OrderBy(p => p.Order).Select(p => p.Value).ToList();
                }
            }

            foreach (var item in catalogValues)
            {
                catalogValuesAscii.Add(HandsonToJson.ConvertToAscii(item));
            }

            // 20200228 CatalogKey START
            catalogInfos = _catalogValueService.GetCatalogValueDetails(catalogValues).ToList();

            foreach (var department in departments)
            {
                catalogInfos.Add(new CatalogValue { CatalogKey = department.Key, Value = department.Value });
            }

            // 20200228 CatalogKey END

            return Json(new { 
                catalogValues = JsonConvert.SerializeObject(catalogValues), 
                catalogValuesAscii = JsonConvert.SerializeObject(catalogValuesAscii),
                catalogInfos = JsonConvert.SerializeObject(catalogInfos),
                row = row 
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCatalogValues2(string catalogName, int row, int col, string formId, bool isXoay, string[] catalogNames)
        {
            var catalogValues = new List<string>();
            var catalogValuesAscii = new List<string>();
            var catalogInfos = new List<CatalogValue>();
            var departments = new List<KeyValuePair<string, string>>();

            bool isExistCatalog = false;
            if (!isExistCatalog)
            {
                Department _department = new Department();

                if (catalogName.Equals("Organization"))
                {
                    var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();

                    if (currentDepartment != null)
                    {
                        var _departments = _departmentService.GetChildrens(currentDepartment.DepartmentId, true).Select(p => new KeyValuePair<string, string>(p.Emails, p.DepartmentName)).ToList();
                        if (_departments.Count == 0)
                        {
                            catalogValues.Add(currentDepartment.DepartmentName);
                            departments.Add(new KeyValuePair<string, string>(currentDepartment.Emails, currentDepartment.DepartmentName));
                        }
                        else
                        {
                            departments = _departments;
                            catalogValues = _departments.Select(p => p.Value).ToList();
                        }
                    }
                }
                else
                {
                    var catalogModel = _catalogService.Gets(c => c.CatalogName == catalogName);
                    var enumerable = catalogModel as Catalog[] ?? catalogModel.ToArray();
                    if (enumerable.Any() && enumerable.First().CatalogValues.Count > 0)
                        catalogValues = enumerable.First().CatalogValues.OrderBy(p => p.Order).Select(p => p.Value).ToList();
                }
            }

            foreach (var item in catalogValues)
            {
                catalogValuesAscii.Add(HandsonToJson.ConvertToAscii(item));
            }

            // 20200228 CatalogKey START
            catalogInfos = _catalogValueService.GetCatalogValueDetails(catalogValues).ToList();

            foreach (var department in departments)
            {
                catalogInfos.Add(new CatalogValue { CatalogKey = department.Key, Value = department.Value });
            }

            // 20200228 CatalogKey END

            return Json(new
            {
                catalogValues = JsonConvert.SerializeObject(catalogValues),
                catalogValuesAscii = JsonConvert.SerializeObject(catalogValuesAscii),
                catalogInfos = JsonConvert.SerializeObject(catalogInfos),
                row = row
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Với những dữ liệu cũ, chỉ có catalogvalues chưa có catalogvaluesascii thì sẽ chạy method này để generate
        /// </summary>
        /// <param name="catalogValues"></param>
        /// <returns></returns>
        public JsonResult GetCatalogValuesAscii(string strCatalogValues)
        {
            dynamic strCatalogValuesDeser = JsonConvert.DeserializeObject(strCatalogValues);
            List<string> catalogValues = strCatalogValuesDeser.ToObject<List<string>>();
            var catalogValuesAscii = new List<string>();

            foreach (var item in catalogValues)
            {
                catalogValuesAscii.Add(HandsonToJson.ConvertToAscii(item));
            }

            return Json(new
            {
                catalogValuesAscii = JsonConvert.SerializeObject(catalogValuesAscii)
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConvertFormCodeXlsToHandson()
        {
            var forms = _formService.Gets(p => p.DefineFieldJson == null, false);
            foreach (var form in forms)
            {
                try
                {
                    if (form.FormCode == null) continue;

                    dynamic formCode = JsonConvert.DeserializeObject(form.FormCode);

                    var handsonToJson = new HandsonToJson();
                    var columnSettings = handsonToJson.parseColumnSetting(form);

                    // generate extra -headerSetting
                    JArray headerSetting = new JArray();
                    if (formCode.headerMerge != null)
                        headerSetting = formCode.dataHeader;

                    // generate extra - mergedCells
                    dynamic headerNested = null;
                    if (formCode.headerMerge != null)
                        headerNested = JsonConvert.DeserializeObject(formCode.headerMerge.ToString());

                    // generate colWidths
                    var colWidths = new int[0] { };

                    var extra = JsonConvert.SerializeObject(new
                    {
                        columnSetting = columnSettings,
                        headerSetting = headerSetting,
                        mergedCells = headerNested
                    });

                    form.FormCode = JsonConvert.SerializeObject(new { header = formCode.header, data = formCode.data, headerNested = formCode.headerNested,
                        dataHeader = formCode.dataHeader, headerMerge = formCode.headerMerge, extra = JsonConvert.DeserializeObject(extra), colWidths = colWidths
                    });

                    // DefineFieldJson
                    var tempArr = formCode.data[0].ToString().Split(',');
                    var defineFieldJson = JsonConvert.SerializeObject(new
                    {
                        data = headerSetting,
                        mergedCells = headerNested,
                        countCols = tempArr.Length - 1,
                        colWidths = new JArray()
                    }); ;
                    form.DefineFieldJson = defineFieldJson;


                    // DefineConfigJson
                    //List<object> defineConfigJsonData = new List<object>();
                    //List<object> defineConfigJsonColumn = new List<object>();

                    //List<object> defineConfigJsonDataDetail;
                    //HeaderObject tempHeaderObj = null;

                    // tranpose fieldConfigJson plus START
                    // Calculate the width and height of the Array
                    var w = headerSetting.Count;
                    var h = JArray.Parse(headerSetting[0].ToString()).Count;

                    var headerDict = (Dictionary<string, string>)formCode.header.ToObject<Dictionary<string, string>>();
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
                            defineConfigJsonData[i,j] = headerSetting[j][i].ToString();
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
                        defineConfigJsonData[i, w + 2] = null;

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

                    form.DefineConfigJson = JsonConvert.SerializeObject(new { data = defineConfigJsonData, columns = defineConfigJsonColumn });

                    // defineValueJson
                    List<object> defineValueJsonData = new List<object>();
                    foreach(var item in formCode.data)
                    {
                        var dataDict = (Dictionary<string, string>)item.ToObject<Dictionary<string, string>>();
                        dataDict.Remove("pos");
                        defineValueJsonData.Add(dataDict.Values.ToList());
                    }

                    List<string> defineValueJsonColumn = new List<string>();
                    for (var i = 1; i < tempArr.Length; i++)
                    {
                        defineValueJsonColumn.Add("allowInvalid: true");
                    }
                    form.DefineValueJson = JsonConvert.SerializeObject(new { data = defineValueJsonData, columns = defineValueJsonColumn });

                    // set Table Name
                    form.TableName = form.EmbryonicPath;

                    _formService.Update(form);
                }
                catch (Exception ex)
                {
                    form.FormCode = JsonConvert.SerializeObject(ex.Message);
                    //_formService.Update(form);
                }
            }

            return Json(new { succress = true, message = "Convert success." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Từ FormCode, generate ra Json phục vụ hiển thị form tạo báo cáo trên mobile
        /// Điều kiện tiền đề: chạy hàm ở trên => ConvertFormCodeXlsToHandson
        /// </summary>
        /// <returns></returns>
        public JsonResult GenerateMobileForms()
        {
            var forms = _formService.Gets(p => p.Json == null, false);
            foreach (var form in forms)
            {
                try
                {
                    if (form.FormCode == null) continue;

                    dynamic formCode = JsonConvert.DeserializeObject(form.FormCode);
                    if (formCode.extra == null) continue;


                    form.Json = JsonConvert.SerializeObject(formCode.extra.columnSetting);
                    _formService.Update(form);
                }
                catch (Exception ex) { }
            }
            
            return Json(new { succress = true, message = "Generate success." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Từ file excel được upload lên, trả về dữ liệu data để fill vào handsontable khi [Tạo báo cáo]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDataFromExcel()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                // ----
                var xlsxParser = new XlsxToJson(file.InputStream);

                var strHeaderAI = Request.Form["HeaderAI"];
                bool haveHeader = true;
                var strHeaderAIArr = new string[0];
                if (string.IsNullOrEmpty(strHeaderAI))
                    haveHeader = false;
                else
                {
                    strHeaderAIArr = strHeaderAI.Split(',');
                    haveHeader = strHeaderAIArr.Length > 1;
                }

                var startTitle = string.IsNullOrEmpty(strHeaderAIArr[0]) ? 0 : int.Parse(strHeaderAIArr[0]);
                var endTitle = string.IsNullOrEmpty(strHeaderAIArr[1]) ? 0 : int.Parse(strHeaderAIArr[1]);
                var startData = string.IsNullOrEmpty(strHeaderAIArr[2]) ? 0 : int.Parse(strHeaderAIArr[2]);
                var endData = string.IsNullOrEmpty(strHeaderAIArr[3]) ? 0 : int.Parse(strHeaderAIArr[3]);

                var listData = xlsxParser.ConvertXlsxToJsonXin(1, startTitle, endTitle, startData, endData, "yearkey");

                if (listData.Count() > 0)
                {
                    // Không có header
                    if (listData[0].ContainsKey("Column_1"))
                    {
                        var listDataNoKey = new List<object>();
                        foreach(var data in listData)
                        {
                            object[] dataObject = data.Select(p => p.Value).ToArray();
                            listDataNoKey.Add(dataObject);
                        }
                        return Json(new { succress = true, data = JsonConvert.SerializeObject(listDataNoKey) });
                    }

                }
                return Json(new { succress = true, data = JsonConvert.SerializeObject(listData) });
            }
            return Json(new { succress = false, data = new JArray() });
        }
        [HttpPost]
        public JsonResult GetDataToWord() {
            string data = "<h1> Tessst oke</h1>";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                string fileName = file.FileName;
                object documentFormat = 8;
                string randomName = DateTime.Now.Ticks.ToString();
                object htmlFilePath = Server.MapPath("~/Temp/") + randomName + ".htm";
                string directoryPath = Server.MapPath("~/Temp/") + randomName + "_files";
                object fileSavePath = Server.MapPath("~/Temp/") + Path.GetFileName(file.FileName);

                //If Directory not present, create it.
                if (!Directory.Exists(Server.MapPath("~/Temp/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Temp/"));
                }

                //Upload the word document and save to Temp folder.
                file.SaveAs(fileSavePath.ToString());

                //var converter = new DocumentConverter();
                //var result = converter.ConvertToHtml(file.InputStream);
                //var html = result.Value; // The generated HTML
                //var warnings = result.Warnings; // Any warnings during conversion
                //data = html;
                //Open the word document in background.
                //Microsoft.Office.Interop.Word._Application applicationclass = new Microsoft.Office.Interop.Word.Application();
                //applicationclass.Documents.Open(ref fileSavePath);
                //applicationclass.Visible = true;
                //Microsoft.Office.Interop.Word.Document document = applicationclass.ActiveDocument;

                ////Save the word document as HTML file.
                //document.SaveAs(ref htmlFilePath, ref documentFormat);

                ////Close the word document.
                //document.Close();

                ////Read the saved Html File.
                //string wordHTML = System.IO.File.ReadAllText(htmlFilePath.ToString(), Encoding.UTF7);

                ////Loop and replace the Image Path.
                //foreach (Match match in Regex.Matches(wordHTML, "<v:imagedata.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase))
                //{
                //    wordHTML = Regex.Replace(wordHTML, match.Groups[1].Value, "Temp/" + match.Groups[1].Value);
                //}
                //data = wordHTML;
                ////Delete the Uploaded Word File.
                //System.IO.File.Delete(fileSavePath.ToString());
            }
            return Json(new { succress = true, data = data });
        }
        #region private
        private Dictionary<string, string> GetActionLevel()
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

        private QueryBuilder CreateQueryBuilder()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;User Id=root;password=123456a@;database=wso2_yenbai;Convert Zero Datetime=True;Character Set=utf8;Persist Security Info=True;port=3306");

            // Create an instance of the QueryBuilder object
            var queryBuilder = QueryBuilderStore.Factory.MySql("BootstrapTheming");

            // Denies metadata loading requests from the metadata provider
            //queryBuilder.MetadataLoadingOptions.OfflineMode = true;
            queryBuilder.MetadataProvider = new MySQLMetadataProvider
            {
                Connection = connection
            };

            //Set default query
            queryBuilder.SQL = @"Select fact_baocaonongnghiep.giatri As giatri ,
                                  fact_baocaonongnghiep.chitiet As chitiet ,
                                  fact_baocaonongnghiep.chitieu As chitieu ,
                                  fact_baocaonongnghiep.timekey As timekey ,
                                  dim_time.month_name As month_name ,
                                  dim_time.year_name As year_name ,
                                  dim_time.quarter As quarter ,
                                  dim_organize.level4_name As level4_name ,
                                  dim_organize.level3_name As level3_name ,
                                  dim_organize.level2_name As level2_name ,
                                  dim_organize.level1_name As level1_name 
                                  From fact_baocaonongnghiep
                                  Inner Join dim_organize On fact_baocaonongnghiep.organizekey =
                                  dim_organize.organizekey
                                  Inner Join dim_time On fact_baocaonongnghiep.timekey = dim_time.id";

            return queryBuilder;
        }
        private QueryTransformer CreateQueryTransformer(SQLQuery query)
        {
            var qt = QueryTransformerStore.Create("BootstrapTheming");

            qt.QueryProvider = query;
            qt.AlwaysExpandColumnsInQuery = true;

            return qt;
        }
        #endregion
        // VuHQ 20191111 REQ-2 END
    }

    public class JsonFile
    {
        public string filename { get; set; }
        public string typetime { get; set; }
        public string dbname { get; set; }
        public bool alter { get; set; }
        public Dictionary<string, string> column { get; set; }

        // 20191217 VuHQ REQ-2
        public Dictionary<string, Dictionary<string, string>> alter_columns { get; set; }
    }

    public class LgspDataReportModel
    {
        public LgspDataReportModel()
        {
        }
        public string type { get; set; }

        public List<LgspDataReportModelArray> data { get; set; }
    }

    public class LgspDataReportModelArray
    {
        public string monhoc { get; set; }
        public string hocky { get; set; }
        public string khoi { get; set; }
        public string caphoc { get; set; }
        public string muavu { get; set; }
        public string loaicay { get; set; }
        public string namhoc { get; set; }
        public string organizekey { get; set; }
        public string datekey { get; set; }
        public string weekkey { get; set; }
        public string monthkey { get; set; }
        public string quarterkey { get; set; }
        public string halfkey { get; set; }
        public string yearkey { get; set; }
        public string minutekey { get; set; }
        public int status { get; set; }

        public LgspDataReportArray array { get; set; }
    }
    public class LgspDataReportArray
    {
        public List<Dictionary<string, object>> rows { get; set; }
    }
}
