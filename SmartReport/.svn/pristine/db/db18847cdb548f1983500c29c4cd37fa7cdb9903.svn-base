using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class TemplateController : CustomController//BaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly TemplateBll _templateService;
        private readonly DocFieldBll _docFieldService;
        private readonly FormBll _formService;
        private readonly TemplateKeyBll _templateKeyService;
        private readonly TemplateHelper _templateHelper;
        private readonly ResourceBll _resourceService;
        private readonly FileLocationBll _fileLocaltionService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly DocTypeBll _doctypeService;

        private const string DEFAULT_SORT_BY = "IsActive";

        public TemplateController(
            AdminGeneralSettings generalSettings,
            TemplateBll templateService,
            DocFieldBll docFieldService,
            FormBll formService,
            TemplateKeyBll templateKeyService,
            TemplateHelper templateHelper,
            ResourceBll resourceService,
            FileLocationBll fileLocaltionService,
            DocTypeFormBll doctypeFormService,
            DocTypeBll doctypeService)
            : base()
        {
            _generalSettings = generalSettings;
            _templateService = templateService;
            _docFieldService = docFieldService;
            _formService = formService;
            _templateKeyService = templateKeyService;
            _templateHelper = templateHelper;
            _resourceService = resourceService;
            _fileLocaltionService = fileLocaltionService;
            _doctypeFormService = doctypeFormService;
            _doctypeService = doctypeService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };
            int? type = null;
            var httpCookie = Request.Cookies[CookieName.SearchTemplate];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    if (data["Type"] != null)
                    {
                        type = int.Parse(data["Type"].ToString());
                    }
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _templateService.GetParents(out totalRecords, t => new TemplateModel
            {
                TemplateId = t.TemplateId,
                Name = t.Name,
                Type = t.Type,
                IsActive = t.IsActive
            }, pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                keySearch: search, currentPage: sortAndPage.CurrentPage, type: type);
            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.KeySearch = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.AllType = GetAllType(type);

            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            BindDocfields();
            var model = new TemplateModel() { IsActive = true };
            ViewBag.Permissions = GetPermissions(model.Permission);
            ViewBag.CommonTemplates = GetCommonTemplates();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TemplateCreate")]
        public ActionResult Create(TemplateModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var template = model.ToEntity();
                    var permission = 0;
                    foreach (var per in model.Permissions)
                    {
                        permission |= per;
                    }
                    template.Permission = permission;
                    template.Content = string.Empty;
                    _templateService.Create(template);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            BindDocfields(model.DocFieldId);
            ViewBag.Permissions = GetPermissions(model.Permission);
            ViewBag.CommonTemplates = GetCommonTemplates();

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TemplateDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var template = _templateService.Get(id);
            try
            {
                _templateService.Delete(template);
                if (template.ParentId == null)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", new { id = template.ParentId });
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotDelete"));
                if (template.ParentId == null)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", new { id = template.ParentId });
            }
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var template = _templateService.Get(id);
            if (template != null)
            {
                BindDocfields(template.DocFieldId);

                var childs = new List<TemplateModel> { template.ToModel() };

                childs.AddRange(_templateService.GetChildren(id, t => new TemplateModel
                {
                    TemplateId = t.TemplateId,
                    Name = t.Name,
                    Type = t.Type,
                    ContentFile = t.ContentFile,
                    IsActive = t.IsActive,
                    DocField = t.DocField,
                    DocType = t.Doctype
                }));
                ViewBag.Childs = childs;
                ViewBag.Permissions = GetPermissions(template.Permission);
                ViewBag.CommonTemplates = GetCommonTemplates(template.CommonTemplate);

                return View(template.ToModel());
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TemplateEdit")]
        public ActionResult Edit(TemplateModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionUpdate"));
                return RedirectToAction("`Index");
            }

            if (ModelState.IsValid)
            {
                var template = _templateService.Get(model.TemplateId);
                if (template == null)
                {
                    return RedirectToAction("Index");
                }
                try
                {
                    model.Content = template.Content;
                    model.ContentFile = template.ContentFile;
                    model.ContentFileLocalName = template.ContentFileLocalName;
                    template = model.ToEntity(template);
                    var permission = 0;
                    foreach (var per in model.Permissions)
                    {
                        permission |= per;
                    }
                    template.Permission = permission;

                    var commonTemplates = 0;
                    if (model.CommonTemplates != null)
                    {
                        foreach (var temp in model.CommonTemplates)
                        {
                            commonTemplates |= temp;
                        }
                        template.CommonTemplate = commonTemplates;
                    }

                    _templateService.Update(template);
                    return RedirectToAction("Index");
                }
                catch
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Update.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.Update.Error"));
                }
            }

            ViewBag.CommonTemplates = GetCommonTemplates(model.CommonTemplate);
            ViewBag.Permissions = GetPermissions(model.Permission);
            BindDocfields(model.DocFieldId);
            var childs = new List<TemplateModel> { model };
            childs.AddRange(_templateService.GetChildren(model.TemplateId, t => new TemplateModel
            {
                TemplateId = t.TemplateId,
                Name = t.Name,
                Type = t.Type,
                DocField = t.DocField,
                DocType = t.Doctype
            }));
            ViewBag.Childs = childs;
            return View(model);
        }

        public ActionResult Child(int id)
        {
            var template = _templateService.Get(id);
            if (template == null)
            {
                return PartialView("_Childs", new List<TemplateModel>());
            }
            var model = new List<TemplateModel> { template.ToModel() };
            model.AddRange(_templateService.GetChildren(id, t => new TemplateModel
            {
                TemplateId = t.TemplateId,
                Name = t.Name,
                Type = t.Type
            }));
            return PartialView("_Childs", model);
        }

        public ActionResult AddChild(int parentId)
        {
            var parent = _templateService.Get(parentId);
            if (parent == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var model = new TemplateModel { ParentId = parentId };
            InitConfigForm(parent.ToModel());
            BindKeys();
            ViewBag.Action = "AddChild";
            ViewBag.ValidateAntiForgeryToken = "TemplateAddChild";
            return PartialView("_Config", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "TemplateAddChild")]
        public ActionResult AddChild(TemplateModel model, int docfield = 0)
        {
            var parent = _templateService.Get(model.ParentId ?? 0);
            if (parent == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Error"));
                ErrorNotification(_resourceService.GetResource("Common.Error"));
                ViewBag.Action = "AddChild";
                ViewBag.ValidateAntiForgeryToken = "TemplateAddChild";
                return PartialView("_Config", model);
            }
            var entity = model.ToEntity();
            entity.Name = parent.Name;
            entity.DocFieldId = docfield;
            entity.IsActive = true;
            entity.Type = parent.Type;
            _templateService.Create(entity);
            return RedirectToAction("Edit", new { id = model.ParentId ?? 0 });
        }

        public ActionResult Config(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
                return RedirectToAction("Index");
            }

            var template = _templateService.Get(id);
            if (template == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var model = template.ToModel();
            model.Content = Microsoft.JScript.GlobalObject.unescape(model.Content);
            InitConfigForm(model);
            BindKeys();
            ViewBag.Action = "Config";
            ViewBag.ValidateAntiForgeryToken = "TemplateConfig";
            return PartialView("_Config", model);
        }

        public ActionResult ConfigTemplate(int id)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
                return RedirectToAction("Index");
            }

            var template = _templateService.Get(id);
            if (template == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var model = template.ToModel();
            InitConfigForm(model);
            BindKeys();
            model.Content = Microsoft.JScript.GlobalObject.unescape(model.Content);
            ViewBag.Action = "Config";
            return PartialView("_ConfigTemplate", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveTemplate(int templateId, string content)
        {
            if (!HasPermission())
            {
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var template = _templateService.Get(templateId);
                if (template != null)
                {
                    template.Content = content;
                    _templateService.Update(template);
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "TemplateConfig")]
        public ActionResult Config(TemplateModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Template.NotPermissionConfig"));
                ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionConfig"));
                ViewBag.Action = "Config";
                ViewBag.ValidateAntiForgeryToken = "TemplateConfig";
                return RedirectToAction("Index");
            }

            var template = _templateService.Get(model.TemplateId);
            if (template != null)
            {
                template.Content = model.Content;
                _templateService.Update(template);
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetKeys(Guid formId)
        {
            try
            {
                var catalogs = _formService.GetCatalogs(formId);
                var exfields = _formService.GetExfields(formId);
                var result = new List<SelectListItem>();
                result.AddRange(catalogs.Select(i => new SelectListItem
                {
                    Text = i.CatalogName,
                    Value = "{" + i.CatalogName.StripVietnameseChars().Trim().StripChars(new[] { '(', ')', ':' })
                                        .ReplaceCharGroups(
                                            new[] { " " },
                                            new[] { '_' })
                                        .ToLower()
                                + "_" + i.CatalogId.ToString("N")
                                + "_" + formId.ToString("N")
                                + "_form}"
                }));
                result.AddRange(exfields.Select(i => new SelectListItem
                {
                    Text = i.ExtendFieldName,
                    Value = "{" + i.ExtendFieldName.StripVietnameseChars().Trim().StripChars(new[] { '(', ')', ':' })
                                        .ReplaceCharGroups(
                                            new[] { " " },
                                            new[] { '_' })
                                        .ToLower()
                                + "_" + i.ExtendFieldId.ToString("N")
                                + "_" + formId.ToString("N")
                                + "_form}"
                }));
                return Json(new { success = result.StringifyJs() }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDocTypes(int docfieldId)
        {
            //var docfield = _docFieldService.Get(docfieldId);
            //if (docfield == null)
            //{
            //    return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            //}

            var doctypes = _doctypeService.Gets(docfieldId);

            var result = doctypes.Select(i => new SelectListItem
            {
                Text = i.DocTypeName,
                Value = i.DocTypeId.ToString("N")
            });
            return Json(new { success = result.StringifyJs() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetForms(Guid doctypeId)
        {
            var forms = _doctypeFormService.GetsAs(df => new { df.Form }, df => df.DocTypeId == doctypeId);
            var result = forms.Select(i => new SelectListItem
            {
                Text = i.Form.FormName,
                Value = i.Form.FormId.ToString("N")
            });
            return Json(new { success = result.StringifyJs() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(int id, bool status)
        {
            var template = _templateService.Get(id);
            if (template == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            template.IsActive = status;
            _templateService.Update(template);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeContentFile(int id, string fileName, string fileLocalName)
        {
            var template = _templateService.Get(id);
            if (template == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            template.ContentFile = fileName;
            template.ContentFileLocalName = fileLocalName;
            _templateService.Update(template);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActiveTemplate(int id, bool isActive)
        {
            var template = _templateService.Get(id);
            if (template == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            template.IsActive = isActive;
            _templateService.Update(template);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keySearch, int pageSize, int? type = null)
        {
            IEnumerable<TemplateModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(keySearch))
                    {
                        keySearch = keySearch.Trim();
                    }
                    int totalRecords;
                    model = _templateService.GetParents(out totalRecords, t => new TemplateModel
                    {
                        TemplateId = t.TemplateId,
                        Name = t.Name,
                        Type = t.Type,
                        IsActive = t.IsActive
                    }, pageSize: pageSize,
                        sortBy: DEFAULT_SORT_BY,
                        isDescending: true,
                        keySearch: keySearch,
                        type: type);
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                    CreateCookieSearch(keySearch, sortAndPage, type);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.KeySearch = keySearch;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                    ViewBag.AllType = GetAllType(type);
                }
            }
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            string keySearch, string sortBy, bool isSortDesc,
            int page, int pageSize, int? type = null)
        {
            IEnumerable<TemplateModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(keySearch))
                {
                    keySearch = keySearch.Trim();
                }
                int totalRecords;
                model = _templateService.GetParents(out totalRecords, t => new TemplateModel
                {
                    TemplateId = t.TemplateId,
                    Name = t.Name,
                    Type = t.Type,
                    IsActive = t.IsActive
                }, pageSize: pageSize,
                    sortBy: sortBy, isDescending: isSortDesc,
                    keySearch: keySearch, currentPage: page,
                    type: type);
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(keySearch, sortAndPage, type);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.KeySearch = keySearch;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                ViewBag.AllType = GetAllType(type);
            }

            return PartialView("_PartialList", model);
        }

        #region Private Method

        private void BindKeys()
        {
            ViewBag.DbKeys = _templateKeyService.Gets().Select(k => new SelectListItem
            {
                Value = k.Code,
                Text = k.Name
            });

            ViewBag.SpecialKeys = _templateHelper.GetSpecials().Select(k => new SelectListItem
            {
                Value = k.Code,
                Text = k.Name
            });

            ViewBag.QuestionKeys = _templateHelper.GetQuestionKeys().Select(k => new SelectListItem
            {
                Value = k.Code,
                Text = k.Name
            });

            ViewBag.DocumentOnlineKeys = _templateHelper.GetDocumentOnlines().Select(k => new SelectListItem
            {
                Value = k.Code,
                Text = k.Name
            });

            ViewBag.CommonKeys = _templateHelper.GetCommonKeys().Select(k => new SelectListItem
            {
                Value = k.Code,
                Text = k.Name
            });
        }

        private void CreateCookieSearch(string keySearch, SortAndPagingModel sortpage, int? type = null)
        {
            var data = new Dictionary<string, object> { { "Type", type }, { "Search", keySearch }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchTemplate];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchTemplate, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private void BindDocfields(int? selectedDocfieldId = null)
        {
            ViewBag.DocfieldId = _docFieldService.GetsAs(f => new { f.DocFieldId, f.DocFieldName }, f => f.IsActivated)
                .Select(f => new SelectListItem
                {
                    Value = f.DocFieldId.ToString(),
                    Text = f.DocFieldName,
                    Selected = selectedDocfieldId.HasValue && f.DocFieldId == selectedDocfieldId
                });
        }

        private void InitConfigForm(TemplateModel template)
        {
            var docfields = new List<SelectListItem>();
            var doctypes = new List<SelectListItem>();
            var forms = new List<SelectListItem>();
            if (template.DocType != null) // nếu là mẫu riêng
            {
                doctypes.Add(new SelectListItem
                        {
                            Text = template.DocType.DocTypeName,
                        });

                if (template.DocField != null)
                {
                    docfields.Add(new SelectListItem
                    {
                        Text = template.DocField.DocFieldName,
                    });
                }

                forms = _formService.GetDynamics(template.DocType.DocTypeId)
                    .Select(f => new SelectListItem
                    {
                        Text = f.FormName,
                        Value = f.FormId.ToString("N")
                    }).ToList();
            }
            else
            {
                var docfieldId = template.DocFieldId?? 0;
                var docfield = _docFieldService.Get(docfieldId);
                if (docfield == null)
                {
                    docfields = _docFieldService.GetsAs(f => new { f.DocFieldId, f.DocFieldName }, df => df.IsActivated)
                        .Select(df => new SelectListItem
                        {
                            Text = df.DocFieldName,
                            Value = df.DocFieldId.ToString()
                        }).ToList();
                }
                else
                {
                    docfields = new List<SelectListItem>{new SelectListItem
                    {
                        Text = docfield.DocFieldName,
                    }};

                    var doctypeList = _doctypeService.Gets(docfieldId);
                    doctypes = doctypeList.Select(dt => new SelectListItem
                        {
                            Value = dt.DocTypeId.ToString("N"),
                            Text = dt.DocTypeName
                        }).ToList();

                    if (doctypes.Count() == 1)
                    {
                        forms = _formService.GetDynamics(doctypeList.First().DocTypeId)
                        .Select(f => new SelectListItem
                        {
                            Text = f.FormName,
                            Value = f.FormId.ToString("N")
                        }).ToList();
                    }
                }
            }

            ViewBag.Docfield = docfields;
            ViewBag.DoctypeId = doctypes;
            ViewBag.Form = forms;
            ViewBag.ExistDoctype = GetExistDoctypeId(template.TemplateId).StringifyJs();
        }

        private List<string> GetExistDoctypeId(int parentTemplateId)
        {
            return _templateService.GetExistDoctypeId(parentTemplateId);
        }

        private List<SelectListItem> GetPermissions(int totalPermission)
        {
            return _resourceService.EnumToSelectList<DocumentProcessType>(totalPermission);
        }

        private List<SelectListItem> GetCommonTemplates(int? t = null)
        {
            return _resourceService.EnumToSelectList<CommonTemplate>(t);
        }

        private IEnumerable<SelectListItem> GetAllType(int? type = null)
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(TemplateType));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((TemplateType)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<TemplateType>((TemplateType)val),
                    Value = itemValue.ToString(),
                    Selected = type.HasValue && itemValue == type.Value

                });
            }

            return result;
        }

        #endregion Private Method
    }
}