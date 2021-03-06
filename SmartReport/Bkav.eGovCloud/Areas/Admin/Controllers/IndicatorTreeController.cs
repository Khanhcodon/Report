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
using OfficeOpenXml;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class IndicatorTreeController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly UnitBll _unitService;
        private readonly dataTypeBll _dataTypeSerivice;
        private readonly DisaggregationBll _disaggregationService;
        private readonly InCatalogBll _dim_catalogSerivice;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public IndicatorTreeController(IndicatorCatalogBll indicatorCatalogService,
           ResourceBll resourceService,
           AdminGeneralSettings generalSettings,
           CategoryBll categoryService,
           InCatalogBll inCatalogService, 
           InCatalogValueBll inCatalogValueService,
           UnitBll unitService,
           dataTypeBll dataTypeSerivice,
           DisaggregationBll disaggregationService,
           InCatalogBll dim_catalogSerivice)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _unitService = unitService;
            _dataTypeSerivice = dataTypeSerivice;
            _disaggregationService = disaggregationService;
            _dim_catalogSerivice = dim_catalogSerivice;
        }

        public ActionResult Index()
        {
            //var listIncataLogs = _disaggregationService.Gets(i => i.IsActivated == true);
            var listIncataLogs = _dim_catalogSerivice.Gets(i => i.IsActivated == true);
            var listIncataLog = new List<SelectListItem>();
            listIncataLog.Add(new SelectListItem()
            {
                Selected = true,
                Value = "00000000-0000-0000-0000-000000000000",
                Text = "Chọn danh mục chỉ tiêu"
            });
            foreach (var item in listIncataLogs)
            {
                listIncataLog.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.InCatalogId.ToString(),
                    Text = item.InCatalogName
                });
            }

            ViewBag.ListIncataLog = listIncataLog;
            GetParentCataLog();
            GetUnit();
            GetDataType();

            if (TempData["ErrorParent"] != null)
            {
                ViewBag.ErrorParent = TempData["ErrorParent"];
            }else
            {
                ViewBag.ErrorParent = null;
            }
            return View();
        }

        public JsonResult GetCatalogs(Guid inCatalogId)
        {
            var stringIndicator = inCatalogId.ToString();
            var inCatalogValue = _inCatalogValueService.Gets(c => c.InCatalogIds.Contains(stringIndicator));

            var indicatorValues = inCatalogValue.Select(d => new {
                id = d.InCatalogValueId.ToString(),
                name = d.InCatalogValueName,
                parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
                order = d.Order,
                level = d.Level,
                //catalog = d.InCatalogId.ToString(),
                text = d.InCatalogValueName,
                code = d.InCatalogValueName,
                //unit = d.Unit.ToString()
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTree()
        {
            var inCatalogValue = _inCatalogValueService.Gets(c => c.Active == true);

            var indicatorValues = inCatalogValue.Select(d => new {
                id = d.InCatalogValueId.ToString(),
                name = d.InCatalogValueName,
                parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
                order = d.Order,
                level = d.Level,
                //catalog = d.InCatalogId.ToString(),
                text = d.InCatalogValueName,
                code = d.InCatalogValueName,
                //unit = d.Unit.ToString()
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInCataLog(Guid inCatalogtId)
        {
            var inCatalogValue = _inCatalogValueService.Gets(c => c.InCatalogId == inCatalogtId);

            var indicatorValues = _inCatalogValueService.Gets().Select(d => {
                //var checkedValue = inCatalogValue.Cont
                return new
                {
                    id = d.InCatalogValueId.ToString(),
                    name = d.InCatalogValueName,
                    parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
                    order = d.Order,
                    level = d.Level,
                    //state = new
                    //{
                    //    selected = checkedValue
                    //},
                    catalog = d.InCatalogId.ToString(),
                    text = d.InCatalogValueCode + " " + d.InCatalogValueName,
                    code = d.InCatalogValueName,
                    //indicatorDepartCheck = checkedValue
                };
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InCatalogValueModel model)
        { 
            GetCatalog();
            if (!ModelState.IsValid) return RedirectToAction("Index");
            try
            {
                if (model.ParentId != null && model.ParentId != default(Guid))
                {
                    var list = new List<Select2Model>();
                    var title = "";
                    GetNameByParent((Guid)model.ParentId, ref list, ref title);
                    model.Level = list.Count;
                }else
                {
                    model.Level = 0;
                }
                //create
                if (model.InCatalogValueId == default(Guid))
                {
                    if(model.Unit != null)
                    {
                        var UnitName_ = _unitService.GetName(model.Unit);
                        model.UnitName = UnitName_.Unit ;
                    }
                    if(model.Type != null)
                    {
                        var TypeName_ = _dataTypeSerivice.GetsSelect(model.Type);
                        model.TypeName = TypeName_.dataTypeName;
                    }
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
                return RedirectToAction("Index");
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                ErrorNotification(ex.Message);
            }
            return View(model);
        }


        public void GetParentCataLog()
        {
            var listParentIncataLogs = _inCatalogValueService.Gets();
            var listParentIncataLog = new List<SelectListItem>();
            listParentIncataLog.Add(new SelectListItem()
            {
                Selected = true,
                Value = "",
                Text = "Chọn danh mục chỉ tiêu"
            });
            foreach (var item in listParentIncataLogs)
            {
                listParentIncataLog.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.InCatalogValueId.ToString(),
                    Text = item.InCatalogValueName
                });
            }
            ViewBag.ListParentCatalogValue = listParentIncataLog;
        }

        public void GetUnit()
        {
            var listUnits = _unitService.GetsSelects();
            var listSelectUnit = new List<SelectListItem>();
            listSelectUnit.Add(new SelectListItem()
            {
                Selected = true,
                Value = "",
                Text = "Chọn đơn vị tính"
            });
            foreach (var item in listUnits)
            {
                listSelectUnit.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.IdUnit.ToString(),
                    Text = item.Unit
                });
            }
            ViewBag.ListUnits = listSelectUnit;
        }


        public void GetDataType()
        {
            var listDataType = _dataTypeSerivice.GetsSelects();
            var listSelectDataType = new List<SelectListItem>();
            listSelectDataType.Add(new SelectListItem()
            {
                Selected = true,
                Value = "",
                Text = "Chọn Loại số liệu"
            });
            foreach (var item in listDataType)
            {
                listSelectDataType.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.dataTypeId.ToString(),
                    Text = item.dataTypeName
                });
            }
            ViewBag.ListDataType = listSelectDataType;
        }

        private void GetCatalog()
        {
            var category = _inCatalogService.GetsAs(c => new InCatalogModel { InCatalogId = c.InCatalogId, InCatalogName = c.InCatalogName });
            var tmp = category.Select(c => new SelectListItem { Value = c.InCatalogId.ToString(), Text = c.InCatalogName })
                .ToList();
            tmp.Insert(0, new SelectListItem() { Value = "", Text = "" });
            ViewBag.Category = tmp;
        }
        private int _i;
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

        public JsonResult Edit(Guid id)
        {
            var categoryDis = _inCatalogValueService.Get(id);
            if (categoryDis == null)
            {
                return null;
            }
            var model = categoryDis.ToModel();    
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RenderDetail(Guid id)
        {
            var indica = _inCatalogValueService.Get(id);
            if(indica == null)
            {
                return null;
            }
            var detailIndica = new DetailIndica();

            detailIndica.DescriptionName = indica.Description;
            detailIndica.inCode = indica.InCatalogValueCode;
            detailIndica.inName = indica.InCatalogValueName;
            var unit = _unitService.GetName(indica.Unit);
            if(unit != null)
            {
                detailIndica.UnitName =  unit.Unit;
            }
            var parentId = _inCatalogValueService.GetsSelect(indica.ParentId);
            if(parentId != null)
            {
                detailIndica.ParentName = parentId.InCatalogValueName;
            }
            var dataTypeid = _dataTypeSerivice.GetsSelect(indica.Type);
            if(dataTypeid != null)
            {
                detailIndica.TypeDes = dataTypeid.dataTypeName;
            }       
            return Json(detailIndica, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(List<Guid> model)
        {
            foreach(var item in model)
            {
                try
                {
                    var inCatalogValue = _inCatalogValueService.Get(item);
                    _inCatalogValueService.Delete(inCatalogValue);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                }
                
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult ExportToExcel(HttpPostedFileBase excelfile)
        {
            var i = 0;
            List<string> dataList = new List<string>();
            if (excelfile != null || excelfile.ContentLength == 0 && excelfile.FileName.EndsWith("xls")
                || excelfile.FileName.EndsWith("xlsx"))
            {
                if (excelfile.ContentType == "application/vnd.ms-excel" ||
                    excelfile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = excelfile.FileName;
                    string targetpath = Server.MapPath("~/TempFile/" + filename);
                    excelfile.SaveAs(targetpath);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var package = new ExcelPackage(new System.IO.FileInfo(targetpath));
                    int startColumn = 1;
                    int startRow = 2;

                    ExcelWorksheet wordSheet = package.Workbook.Worksheets[0];
                    object indiCode = null;
                    var indiTree = new InCatalogValueModel();
                    do
                    {
                        indiCode = wordSheet.Cells[startRow + i, startColumn].Value;
                        object indiName = wordSheet.Cells[startRow + i, startColumn + 1].Value;
                        object indiParentCode = wordSheet.Cells[startRow + i, startColumn + 2].Value; // ma chi tieu
                        object indiType = wordSheet.Cells[startRow + i, startColumn + 3].Value;
                        object indiUnit = wordSheet.Cells[startRow + i, startColumn + 4].Value;
                        object indiDes = wordSheet.Cells[startRow + i, startColumn + 5].Value;
                        object indiPeriod = wordSheet.Cells[startRow + i, startColumn + 6].Value;
                        object cateDis = wordSheet.Cells[startRow + i, startColumn + 7].Value;
                        object indiDepends = wordSheet.Cells[startRow + i, startColumn + 8].Value;
                        object indiLimitMin = wordSheet.Cells[startRow + i, startColumn + 9].Value;
                        object indiLimitMax = wordSheet.Cells[startRow + i, startColumn + 10].Value;
                        object indiFunMath = wordSheet.Cells[startRow + i, startColumn + 11].Value;
                        object indiNumbePeriod = wordSheet.Cells[startRow + i, startColumn + 12].Value;


                        if (indiCode != null && indiName != null)
                        {
                            // save model
                            indiTree.InCatalogValueCode = indiCode.ToString();
                            indiTree.InCatalogValueName = indiName.ToString(); 
                            // read data
                            if (indiParentCode != null)
                            {
                                var strSaveParent = indiParentCode.ToString();
                                var indiParentId = _inCatalogValueService.Gets(d => d.InCatalogValueCode == strSaveParent).FirstOrDefault(); // ma chi tieu cha
                                if(indiParentId != null)
                                {
                                    indiTree.ParentId = indiParentId.InCatalogValueId;
                                    var list = new List<Select2Model>();
                                    var title = "";
                                    if (indiParentId.ParentId != null && indiParentId.ParentId != default(Guid))
                                    {                                      
                                        GetNameByParent((Guid)indiParentId.ParentId, ref list, ref title);
                                        indiTree.Level = list.Count;
                                    }
                                    else
                                    {
                                        indiTree.Level = 0;
                                    }
                                }
                                else
                                {
                                    TempData["ErrorParent"] = strSaveParent;
                                    var count = i;
                                    for(var k = 0; k <= count; k++)
                                    {
                                        indiCode = wordSheet.Cells[startRow + k, startColumn].Value;
                                        var indiStrDel = indiCode.ToString();
                                        var modelIndi = _inCatalogValueService.Gets(d => d.InCatalogValueCode == indiStrDel).FirstOrDefault();
                                        if(modelIndi != null)
                                        {
                                            var delEx = _inCatalogValueService.Get(modelIndi.InCatalogValueId);
                                            _inCatalogValueService.Delete(delEx);
                                        }           
                                    }
                                    return RedirectToAction("Index", "IndicatorTree");
                                }
                            }
                            else
                            {
                                indiTree.ParentId = null;
                            }
                            

                            if(indiType != null)
                            {
                                var strSaveType = indiType.ToString();
                                indiTree.TypeName = strSaveType;
                                var indiTypeId = _dataTypeSerivice.Gets(d => d.dataTypeName == strSaveType).FirstOrDefault();// loai so lieu
                                indiTree.Type = indiTypeId.dataTypeId;
                            }
                            else
                            {
                                indiTree.Type = null;
                            }

                            if(indiUnit != null)
                            {
                                var strSaveUnit = indiUnit.ToString();
                                indiTree.UnitName = strSaveUnit;
                                var indiUnitId = _unitService.Gets(d => d.Unit == strSaveUnit).FirstOrDefault(); // don vi tinh
                                if(indiUnitId != null)
                                {
                                    indiTree.Unit = indiUnitId.IdUnit;
                                }
                                else
                                {
                                    //tạo mới đơn vị tính
                                    var unitModel = new Ad_UnitModel();
                                    unitModel.IdUnit = Guid.NewGuid();
                                    unitModel.Unit = strSaveUnit;
                                    unitModel.Exchange = strSaveUnit;
                                    unitModel.OriginalUnit = strSaveUnit;
                                    unitModel.Description = strSaveUnit;
                                    unitModel.Use = true;

                                    var toModelUnit = unitModel.ToEntity();
                                    _unitService.Create(toModelUnit);
                                }
                            } else
                            {
                                indiTree.Unit = null;
                            }
                            if(indiDes != null)
                            {
                                var indiDesOnly = indiDes.ToString();// mo ta
                                indiTree.Description = indiDesOnly;
                            }
                            else
                            {
                                indiTree.Description = null;
                            }
                            
                            if(indiPeriod != null)
                            {
                                indiTree.PeriodTypeIds = indiPeriod.ToString();
                            }
                            else
                            {
                                indiTree.PeriodTypeIds = null;
                            }

                            if (indiDepends != null)
                            {
                                indiTree.RegressionIds = indiDepends.ToString();
                            }
                            else
                            {
                                indiTree.RegressionIds = null;
                            }
                            // ky cong bo
                            if(cateDis != null)
                            {
                                var cateDisSlip = cateDis.ToString().Split('/');// danh muc phan to
                                var strCateDis = "";
                                var count = cateDisSlip.Count();                            
                                for(var j = 0; j < count; j++)
                                {
                                    if(j == count - 1)
                                    {
                                        var item = cateDisSlip[j];
                                        var cateDissOnly = _dim_catalogSerivice.Gets(d => d.InCatalogName == item).FirstOrDefault();
                                        strCateDis += '"' + cateDissOnly.InCatalogId.ToString() +'"';
                                    }
                                    else
                                    {
                                        var item = cateDisSlip[j];
                                        var cateDissOnly = _dim_catalogSerivice.Gets(d => d.InCatalogName == item).FirstOrDefault();
                                        strCateDis += '"' + cateDissOnly.InCatalogId.ToString() + '"' + ",";
                                    }
                                }
                                var strEnd = "[" + strCateDis + "]"; // danh muc phan to
                                indiTree.InCatalogIds = strEnd;
                            }
                            else
                            {
                                var str = "00000000-0000-0000-0000-000000000000";
                                var strEnd = "["+ '"' + str +'"' + "]";
                                indiTree.InCatalogIds = strEnd;
                            }

                            if(indiLimitMin != null)
                            {
                                indiTree.Threshold_min = indiLimitMin.ToString();
                            }
                            else
                            {
                                indiTree.Threshold_min = null;
                            }
                            
                            if(indiLimitMax != null)
                            {
                                indiTree.Threshold_max = indiLimitMax.ToString();
                            }
                            else
                            {
                                indiTree.Threshold_max = null;
                            }

                            if (indiNumbePeriod != null)
                            {
                                indiTree.NumberPeriodReplace = indiNumbePeriod.ToString();
                            }
                            else
                            {
                                indiTree.NumberPeriodReplace = null;
                            }

                            if (indiFunMath != null)
                            {
                                indiTree.AggregationFormula = indiFunMath.ToString();
                            }
                            else
                            {
                                indiTree.AggregationFormula = null;
                            }

                            indiTree.Active = true;
                            indiTree.AllowAggregation = true;
                            indiTree.AllowAggregationByPeriod = true;
                            var endModel = indiTree.ToEntity();
                            _inCatalogValueService.Create(endModel);

                            i += 1;
                        }
                    }
                    while (indiCode != null);

                    TempData["Success"] = "SS";
                }
                else
                {
                    TempData["Error"] = "ER";
                }
            }

            return RedirectToAction("Index", "IndicatorTree");
        }


    }

    public class DetailIndica
    {
        public string inCode { get; set; }
        public string inName { get; set; }
        public string UnitName { get; set; }
        public string ParentName { get; set; }
        public string TypeDes { get; set; }
        public string TypePeriodName { get; set; }
        public string dataTypeName { get; set; }
        public string DescriptionName { get; set; }
    }
}