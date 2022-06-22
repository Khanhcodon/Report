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
    public class IndicatorCatalogValueController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public IndicatorCatalogValueController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService, InCatalogValueBll inCatalogValueService)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermission"));
                return RedirectToAction("Index");
            }
            GetCatalog();
            ModelState.Clear();
            return View(new InCatalogValueModel());
        }

        public ActionResult IndexNew()
        {
            return View();
        }

        private void GetCatalog()
        {
            var category = _inCatalogService.GetsAs(c => new InCatalogModel { InCatalogId = c.InCatalogId, InCatalogName = c.InCatalogName });
            var tmp = category.Select(c => new SelectListItem { Value = c.InCatalogId.ToString(), Text = c.InCatalogName })
                .ToList();
            tmp.Insert(0, new SelectListItem() { Value = "", Text = "" });
            ViewBag.Category = tmp;
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InCatalogValueModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                if (model.InCatalogValueId == default(Guid))
                {
                    ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermissionCreate"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermissionCreate"));
                }
                else
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermissionUpdate"));
                    ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermissionUpdate"));
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
                if (model.InCatalogValueId == default(Guid))
                {
                    var catalog = model.ToEntity();
                    _inCatalogValueService.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalogValue.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalogValue.Created"));
                    model.InCatalogValueId = catalog.InCatalogValueId;
                }
                else
                {
                    var catalog = _inCatalogValueService.Get(model.InCatalogValueId);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _inCatalogValueService.Update(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalogValue.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalogValue.Updated"));

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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalogValue.NotPermissionDelete"));
                return RedirectToAction("Index");
            }
            var catalog = _inCatalogValueService.Get(CatalogValueId);
            if (catalog == null) return RedirectToAction("Index");
            try
            {
                _inCatalogValueService.Delete(catalog);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalogValue.Deleted"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalogValue.Deleted"));
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
                var listJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImportInCatalogModel>>(jsonConvert);
                foreach (var arr in listJson)
                {
                    var listPath = arr.duongdan.Split('\\').ToList();
                    var cPath = listPath.Count;
                    if (cPath <= 1) continue;
                    var catalog = _inCatalogService.Gets(c => c.InCatalogName == arr.danhmuc).FirstOrDefault();
                    var inCatalogId = catalog?.InCatalogId ?? default(Guid);
                    if (catalog == null)
                    {
                        var ca = new InCatalogModel { InCatalogKey = "", InCatalogName = arr.danhmuc };
                        catalog = ca.ToEntity();
                        _inCatalogService.Create(catalog);
                        inCatalogId = catalog.InCatalogId;
                    }
                    if (cPath == 2)
                    {
                        var model = new InCatalogValueModel
                        {
                            InCatalogId = inCatalogId,
                            InCatalogValueCode = arr.machitieu,
                            InCatalogValueName = arr.tenchitieu

                        };
                        _inCatalogValueService.Create(model.ToEntity());
                    }
                    else
                    {
                        for (var i = 1; i < cPath; i++)
                        {
                            var model = new InCatalogValueModel();
                            var i1 = i;
                            var catalogValueCheck = _inCatalogValueService.GetInCatalogValueByName(listPath[i1]).FirstOrDefault();
                            var child = cPath - 1 == i;
                            model.InCatalogId = inCatalogId;
                            var getParent = _inCatalogValueService.GetInCatalogValueByName(listPath[i1 - 1]).FirstOrDefault();
                            model.ParentId = getParent?.InCatalogValueId;
                            if (child)
                            {
                                model.InCatalogValueCode = arr.machitieu;
                                model.InCatalogValueName = arr.tenchitieu;
                                _inCatalogValueService.Create(model.ToEntity());
                            }
                            else
                            {
                                if (catalogValueCheck != null) continue;
                                model.InCatalogValueCode = "";
                                model.InCatalogValueName = listPath[i];
                                _inCatalogValueService.Create(model.ToEntity());
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
                var cloneList = categoryId == default(Guid) ? _inCatalogValueService.GetsParent().ToList() : _inCatalogValueService.Gets(c => c.ParentId == null && c.InCatalogId == categoryId);
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
                var parent = _inCatalogValueService.Get(id);
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
        private IEnumerable<Select2Model> GetDataSelect(InCatalogValue parent)
        {
            var list = new List<Select2Model>();
            _i = 0;
            list.Add(new Select2Model { id = parent.InCatalogValueId, text = parent.InCatalogValueName, level = 0 });
            GetNameByParent(parent.InCatalogValueId, parent.InCatalogValueName, _i, ref list);
            return list;
        }
        private List<Select2Model> GetAllDataSelect(Guid catalogId)
        {
            var list = new List<Select2Model>();
            var all = (catalogId == default(Guid) ? _inCatalogValueService.Gets(c => c.ParentId == null) : _inCatalogValueService.Gets(c => c.InCatalogId == catalogId && c.ParentId == null)).OrderBy(c => c.Order);
            foreach (var item in all)
            {
                //if (list.FindIndex(c => c.id == item.InCatalogValueId) > 0)
                //    continue;
                var data = GetDataSelect(item);
                list.AddRange(data);
            }
            //list = list.GroupBy(test => test.id)
            //           .Select(grp => grp.First()).OrderBy(c => c.text).ToList();
            return list;
        }
        private void GetNameByParent(Guid parentId, string title, int level, ref List<Select2Model> list)
        {
            var parentChild = _inCatalogValueService.Gets(c => c.ParentId == parentId);
            if (parentChild == null) return;
            _i++;
            level++;
            foreach (var child in parentChild)
            {
                //title = title + "/" + child.InCatalogValueName;
                list.Add(new Select2Model { id = child.InCatalogValueId, text = title + "/" + child.InCatalogValueName, level = level });
                GetNameByParent(child.InCatalogValueId, title + "/" + child.InCatalogValueName, level, ref list);
            }
        }
        private void GetNameByParent(Guid parentId, ref List<Select2Model> list, ref string title)
        {
            while (true)
            {
                var parent = _inCatalogValueService.Get(parentId);
                if (parent == null) break;
                _i++;
                title = parent.InCatalogValueName;
                Console.WriteLine(parent.InCatalogValueId);
                list.Add(new Select2Model { id = parent.InCatalogValueId, text = title, level = _i });
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
                var arr = _inCatalogValueService.GetsChildByParent(parentId);
                var indicatorCatalogs = arr as List<InCatalogValue> ?? arr.ToList();
                return Json(new { success = true, data = indicatorCatalogs }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
