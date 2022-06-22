using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business;
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
    public class SurveyCatalogValueController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly SurveyCatalogBll _surveyCatalogService;
        private readonly SurveyCatalogValueBll _surveyCatalogValueService;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public SurveyCatalogValueController(
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            SurveyCatalogBll surveyCatalogService, SurveyCatalogValueBll surveyCatalogValueService)
            : base()
        {
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _surveyCatalogService = surveyCatalogService;
            _surveyCatalogValueService = surveyCatalogValueService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalogValue.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalogValue.NotPermission"));
                return RedirectToAction("Index");
            }
            GetCatalog();
            ModelState.Clear();
            return View(new SurveyCatalogValueModel());
        }

        private void GetCatalog()
        {
            var category = _surveyCatalogService.GetsAs(c => new SurveyCatalogModel { CatalogId = c.CatalogId, CatalogName = c.CatalogName });
            var tmp = category.Select(c => new SelectListItem { Value = c.CatalogId.ToString(), Text = c.CatalogName })
                .ToList();
            tmp.Insert(0, new SelectListItem() { Value = "", Text = "" });
            ViewBag.Category = tmp;
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SurveyCatalogValueModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                if (model.CatalogValueId == default(Guid))
                {
                    ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalogValue.NotPermissionCreate"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalogValue.NotPermissionCreate"));
                }
                else
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalogValue.NotPermissionUpdate"));
                    ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalogValue.NotPermissionUpdate"));
                }

                return RedirectToAction("Index");
            }
            GetCatalog();
            if (!ModelState.IsValid) return View(model);
            try
            {
                if (model.ParentId != null && model.ParentId != default(Guid))
                {
                    var list = new List<Select2Model>();
                    var title = "";
                    GetNameByParent((Guid)model.ParentId, ref list, ref title);
                    model.Level = list.Count;
                }
                //create
                if (model.CatalogValueId == default(Guid))
                {
                    var catalog = model.ToEntity();
                    _surveyCatalogValueService.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalogValue.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalogValue.Created"));
                    model.CatalogValueId = catalog.CatalogValueId;
                }
                else
                {
                    var catalog = _surveyCatalogValueService.Get(model.CatalogValueId);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _surveyCatalogValueService.Update(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalogValue.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalogValue.Updated"));

                }
                //return RedirectToAction("Index");
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                ErrorNotification(ex.Message);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(Guid CatalogValueId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalogValue.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalogValue.NotPermissionDelete"));
                return RedirectToAction("Index");
            }
            var catalog = _surveyCatalogValueService.Get(CatalogValueId);
            if (catalog == null) return RedirectToAction("Index");
            try
            {
                _surveyCatalogValueService.Delete(catalog);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalogValue.Deleted"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalogValue.Deleted"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public JsonResult ImportData(HttpPostedFileBase files)
        {
            try
            {
                var xlsxParser = new XlsxToJson(files.InputStream);
                var json = xlsxParser.ConvertXlsxToJson(1, 2, 1, 1);
                var jsonConvert = Newtonsoft.Json.JsonConvert.SerializeObject(json);
                var listJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImportSurveyCatalogModel>>(jsonConvert);
                foreach (var arr in listJson)
                {
                    var listPath = arr.duongdan.Split('\\').ToList();
                    var cPath = listPath.Count;
                    if (cPath <= 1) continue;
                    var catalog = _surveyCatalogService.Gets(c => c.CatalogName == arr.danhmuc).FirstOrDefault();
                    var surveyCatalogId = catalog?.CatalogId ?? default(Guid);
                    if (catalog == null)
                    {
                        var ca = new SurveyCatalogModel { CatalogKey = "", CatalogName = arr.danhmuc };
                        catalog = ca.ToEntity();
                        _surveyCatalogService.Create(catalog);
                        surveyCatalogId = catalog.CatalogId;
                    }
                    if (cPath == 2)
                    {
                        var model = new SurveyCatalogValueModel
                        {
                            CatalogId = surveyCatalogId,
                            CatalogKey = arr.machitieu,
                            Value = arr.tenchitieu

                        };
                        _surveyCatalogValueService.Create(model.ToEntity());
                    }
                    else
                    {
                        for (var i = 1; i < cPath; i++)
                        {
                            var model = new SurveyCatalogValueModel();
                            var i1 = i;
                            var catalogValueCheck = _surveyCatalogValueService.GetSurveyCatalogValueByName(listPath[i1]).FirstOrDefault();
                            var child = cPath - 1 == i;
                            model.CatalogId = surveyCatalogId;
                            var getParent = _surveyCatalogValueService.GetSurveyCatalogValueByName(listPath[i1 - 1]).FirstOrDefault();
                            model.ParentId = getParent?.CatalogValueId;
                            if (child)
                            {
                                model.CatalogKey = arr.machitieu;
                                model.Value = arr.tenchitieu;
                                _surveyCatalogValueService.Create(model.ToEntity());
                            }
                            else
                            {
                                if (catalogValueCheck != null) continue;
                                model.CatalogKey = "";
                                model.Value = listPath[i];
                                _surveyCatalogValueService.Create(model.ToEntity());
                            }

                        }
                    }

                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCatalogByCategory(Guid categoryId)
        {
            try
            {
                var cloneList = categoryId == default(Guid) ? _surveyCatalogValueService.GetsParent().ToList() : _surveyCatalogValueService.Gets(c => c.ParentId == null && c.CatalogId == categoryId);
                return Json(new { success = true, data = cloneList }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadDataSelect(Guid id)
        {
            try
            {
                var parent = _surveyCatalogValueService.Get(id);
                return Json(new { success = true, data = parent }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadSelect2(Guid catalogId)
        {
            try
            {
                return Json(new { success = true, data = GetAllDataSelect(catalogId) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        private int _i;
        private IEnumerable<Select2Model> GetDataSelect(SurveyCatalogValue parent)
        {
            var list = new List<Select2Model>();
            _i = 0;
            list.Add(new Select2Model { id = parent.CatalogValueId, text = parent.Value, level = 0 });
            GetNameByParent(parent.CatalogValueId, parent.Value, _i, ref list);
            return list;
        }
        private List<Select2Model> GetAllDataSelect(Guid catalogId)
        {
            var list = new List<Select2Model>();
            var all = (catalogId == default(Guid) ? _surveyCatalogValueService.Gets(c => c.ParentId == null) : _surveyCatalogValueService.Gets(c => c.CatalogId == catalogId && c.ParentId == null)).OrderBy(c => c.Order);
            foreach (var item in all)
            {
                var data = GetDataSelect(item);
                list.AddRange(data);
            }
            //list = list.GroupBy(test => test.id)
            //           .Select(grp => grp.First()).OrderBy(c => c.text).ToList();
            return list;
        }
        private void GetNameByParent(Guid parentId, string title, int level, ref List<Select2Model> list)
        {
            var parentChild = _surveyCatalogValueService.Gets(c => c.ParentId == parentId);
            if (parentChild == null) return;
            _i++;
            level++;
            foreach (var child in parentChild)
            {
                list.Add(new Select2Model { id = child.CatalogValueId, text = title + "/" + child.Value, level = level });
                GetNameByParent(child.CatalogValueId, title + "/" + child.Value, level, ref list);
            }
        }
        private void GetNameByParent(Guid parentId, ref List<Select2Model> list, ref string title)
        {
            while (true)
            {
                var parent = _surveyCatalogValueService.Get(parentId);
                if (parent == null) break;
                _i++;
                title = parent.Value;
                Console.WriteLine(parent.CatalogValueId);
                list.Add(new Select2Model { id = parent.CatalogValueId, text = title, level = _i });
                if (parent.ParentId != null)
                {
                    parentId = (Guid)parent.ParentId;
                    continue;
                }
                break;
            }
        }

        public JsonResult GetCatalogByCategoryChild(Guid parentId)
        {
            try
            {
                var arr = _surveyCatalogValueService.GetsChildByParent(parentId);
                var surveyCatalogs = arr as List<SurveyCatalogValue> ?? arr.ToList();
                return Json(new { success = true, data = surveyCatalogs }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
