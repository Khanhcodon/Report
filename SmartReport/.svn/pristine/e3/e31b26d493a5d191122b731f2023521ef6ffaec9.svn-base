using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class ReportQueryController : CustomController
    {
        public ReportQueryBll _reportQueryService;
        public ReportQueryGroupBll _reportQueryGroupService;
        public ReportQueryFilterBll _reportQueryFilterService;
        public TemplateKeyCategoryBll _templateKeyCategoryService;
        public E_DataTableBll _eDataTableService;
        public DataFieldBll _dataFieldService;
        private readonly ResourceBll _resourceService;

        public ReportQueryController(
            ReportQueryBll reportQueryService,
            ReportQueryGroupBll reportQueryGroupService,
            ReportQueryFilterBll reportQueryFilterService,
            TemplateKeyCategoryBll templateKeyCategoryService,
            E_DataTableBll eDataTableService,
            DataFieldBll dataFieldService,
            ResourceBll resourceService)
        {
            _reportQueryService = reportQueryService;
            _reportQueryGroupService = reportQueryGroupService;
            _reportQueryFilterService = reportQueryFilterService;
            _templateKeyCategoryService = templateKeyCategoryService;
            _eDataTableService = eDataTableService;
            _dataFieldService = dataFieldService;
            _resourceService = resourceService;
        }

        #region Action
        public ActionResult Index()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQuery.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQuery.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = _reportQueryService.Gets().Select(p => new ReportQueryModel
            {
                ReportQueryId = p.ReportQueryId,
                ReportQueryName = p.ReportQueryName,
                FormulaDataColumnName = _dataFieldService.GetDataFieldName(p.FormulaDataColumnId),
                DataTableName = _eDataTableService.GetDataTableName(p.DataTableId),
                DataTableDescription = _eDataTableService.GetDataTableName(p.DataTableId),
                Description = p.Description,
                ActionLevelId = p.ActionLevelId,
                ActionLevelName = p.ActionLevelId != null ? _reportQueryService.GetActionLevels(null).Where(q => q.Value == p.ActionLevelId.ToString()).First().Text : "",
                CreatedAt = p.CreatedAt
            });

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var reportQuery = _reportQueryService.Get(id);
                _reportQueryService.Delete(reportQuery);

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQuery.Deleted"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQuery.Deleted"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQuery.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQuery.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQuery.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = new ReportQueryModel();

            LoadDropDownData(new ReportQuery());

            model.Query = string.Empty;

            if (model.Query != string.Empty)
            {
                var errMsg = string.Empty;
                var tableId = model.DataTableId ?? 0;
                ViewBag.ReportQueryData = Json2.Stringify(_reportQueryService.GetReportQueryData(model.Query, tableId, out errMsg));
                ViewBag.ErrMsg = errMsg;
            }
            else
            {
                ViewBag.ReportQueryData = "[]";
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ReportQueryModel model, List<ReportQueryFilter> filters)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQuery.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQuery.NotPermission"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var tempReportQuerys = _reportQueryService.Gets(p => p.ReportQueryName == model.ReportQueryName);
                if (tempReportQuerys.Count() > 0)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryName.Exist"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryName.Exist"));
                    ModelState.AddModelError("ReportQueryName", _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryName.Exist"));

                    LoadDropDownData(new ReportQuery());

                    return View(model);
                }

                var reportQuery = model.ToEntity();
                reportQuery.CreatedBy = User.GetUserId();
                reportQuery.CreatedAt = DateTime.Now;

                reportQuery.Filters = filters;
                try
                {
                    _reportQueryService.Create(reportQuery);

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQuery.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQuery.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            LoadDropDownData(new ReportQuery());

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQuery.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQuery.NotPermissionEdit"));
                return RedirectToAction("Index");
            }

            var reportQuery = _reportQueryService.Get(id);
            if (reportQuery == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQuery.NotExist"));
                return RedirectToAction("Index");
            }

            LoadDropDownData(reportQuery);

            var model = reportQuery.ToModel();
            model.Query = _reportQueryService.GenerateQueryByGroup(new List<ReportQuery> { reportQuery });

            if (model.Query != string.Empty)
            {
                //ViewBag.ReportQueryData = Json2.Stringify(_reportQueryService.GetReportQueryData(model.Query));
                var errMsg = string.Empty;

                var tableId = reportQuery.DataTableId ?? 0;
                var data = _reportQueryService.GetReportQueryData(model.Query, tableId, out errMsg);
                ViewBag.ReportQueryData = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
                ViewBag.ErrMsg = errMsg;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ReportQueryModel model, List<ReportQueryFilter> filters)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQuery.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQuery.NotPermissionEdit"));
                return RedirectToAction("Index");
            }
            var reportQuery = new ReportQuery();
            if (ModelState.IsValid)
            {
                reportQuery = _reportQueryService.Get(model.ReportQueryId);
                if (reportQuery == null)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQuery.NotExist"));
                    return RedirectToAction("Index");
                }

                reportQuery = model.ToEntity(reportQuery);
                reportQuery.UpdatedAt = DateTime.Now;
                reportQuery.UpdatedBy = User.GetUserId();

                _reportQueryService.Update(reportQuery, filters);
                if (model.Filters == null || model.Filters.Count <= 0) return RedirectToAction("Index");
                foreach (var reportQueryFilter in from filter in model.Filters
                                                  let reportQueryFilter = _reportQueryFilterService.Get(filter.ReportQueryFilterId)
                                                  select filter.ToEntity(reportQueryFilter))
                {
                    _reportQueryFilterService.Update(reportQueryFilter);
                }
                return RedirectToAction("Index");
            }

            LoadDropDownData(reportQuery);
            return View(model);
        }

        public JsonResult GetDataReport()
        {
            try
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
                return Json(new { success = true, ReportQuery = reportQuery, ReportQueryGroup = reportQueryGroup }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        public JsonResult GetFormCode(int id, string type)
        {
            var list = new List<string>();
            var query = "";
            try
            {
                switch (type)
                {
                    case "Report":
                        var report = _reportQueryService.Get(id);
                        query =  _reportQueryService.GenerateQueryByGroup(new List<ReportQuery> { report });
                        if (!string.IsNullOrWhiteSpace(report?.FormCode))
                            list.Add(report.FormCode);
                        break;
                    case "ReportGroup":
                        var reportQueryGroup = _reportQueryGroupService.Get(id);
                        if (reportQueryGroup == null)
                            break;
                        var reportQueryIds = reportQueryGroup.Querys.Select(p => p.ReportQueryId).ToList();
                        if (reportQueryIds.Count == 0)
                            break;
                        var reports = new List<ReportQuery>();

                        foreach (var item in reportQueryIds)
                        {
                            report = _reportQueryService.Get(item);
                            reports.Add(report);
                            if (!string.IsNullOrWhiteSpace(report?.FormCode))
                                list.Add(report.FormCode);
                        }
                        query = _reportQueryService.GenerateQueryByGroup(reports);
                        break;
                }
            }
            catch
            {
                return Json(new { success = false });
            }
            return Json(new { success = true, data = list , query = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFieldNames(int tableId)
        {
            var eDataTable = _eDataTableService.Get(tableId);
            if (eDataTable != null)
            {
                var dataFields = _dataFieldService.Gets(p => p.DataTableId == eDataTable.DataTableId)
                    .Select(f => new SelectListItem
                    {
                        Selected = false,
                        Text = eDataTable.Name + "." + f.FieldName,
                        Value = f.DataFieldId.ToString()
                    }
                ).ToList();

                // START Add field relation nếu có
                var relationDataTables = _eDataTableService.GetRelationTables((int)eDataTable.DataTableId);
                foreach (var relationDataTable in relationDataTables)
                {
                    var relationFields = _dataFieldService.Gets(p => p.DataTableId == relationDataTable.DataTableId)
                    .Select(f => new SelectListItem
                    {
                        Selected = false,
                        Text = relationDataTable.Name + "." + f.FieldName,
                        Value = f.DataFieldId.ToString()
                    }).ToList();

                    foreach (var temp in relationFields)
                    {
                        dataFields.Add(temp);
                    }
                }


                return Json(new { Data = dataFields.OrderBy(p => p.Text.Split('.')[1]), success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = new List<SelectListItem>(), success = false, message = "Có lỗi xảy ra trong quá trình tải dữ liệu" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetValuesByDataField(int dataFieldId)
        {
            string type = string.Empty;
            var result = _reportQueryService.GetValuesByDataField(dataFieldId, out type);
            return Json(new { result, dataType = type }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// VuHQ TBD
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GenerateQuery(ReportQueryModel model)
        {
            var query = string.Empty;
            var reportQueryData = "[]";
            var errMsg = string.Empty;
            var reportQuery = model.ToEntity();

            reportQuery.Filters = model.Filters != null && model.Filters.Any() ? model.Filters.ToListEntity().ToList() : new List<ReportQueryFilter>();
            query = _reportQueryService.GenerateQueryByGroup(new List<ReportQuery> { reportQuery });

            if (query != string.Empty)
            {
                var tableId = reportQuery.DataTableId ?? 0;
                var data = _reportQueryService.GetReportQueryData(query, (int)model.DataTableId, out errMsg);
                reportQueryData = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
            }

            return Json(new { Query = query, Data = reportQueryData, ErrMsg = errMsg, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerateQueryByGroup(List<int> reportQueryIds)
        {
            var reportQuerys = new List<ReportQuery>();
            foreach (var reportQueryId in reportQueryIds)
            {
                reportQuerys.Add(_reportQueryService.Get(reportQueryId));
            }
            var query = _reportQueryService.GenerateQueryByGroup(reportQuerys);

            return Json(new { Data = query, success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region private method

        private void LoadDropDownData(ReportQuery reportQuery)
        {
            ViewBag.ActionLevelId = _reportQueryService.GetActionLevels(reportQuery.ActionLevelId);

            if (!reportQuery.FormulaDataColumnId.HasValue)
            {
                ViewBag.FormulaDataColumnId = new List<SelectListItem>()
                {
                    new SelectListItem
                    {
                        Selected = true,
                        Text = "Cột công thức",
                        Value = "0"
                    }
                };

                ViewBag.FieldNames = new List<SelectListItem>();
            }
            else
            {
                var eDataTable = _eDataTableService.Get((int)reportQuery.DataTableId);
                if (eDataTable != null)
                {
                    ViewBag.TableName = eDataTable.Name;
                    var tempDataFields = _dataFieldService.Gets(p => p.DataTableId == eDataTable.DataTableId)
                        .Select(f => new SelectListItem
                        {
                            Selected = reportQuery.FormulaDataColumnId.HasValue && reportQuery.FormulaDataColumnId == f.DataFieldId,
                            Text = eDataTable.Name + "." + f.FieldName,
                            Value = f.DataFieldId.ToString()
                        }
                    ).ToList();

                    var dataFields = new List<SelectListItem>();
                    dataFields.Add(new SelectListItem
                    {
                        Selected = reportQuery.FormulaDataColumnId.HasValue && reportQuery.FormulaDataColumnId == 0,
                        Text = "Cột công thức",
                        Value = "0"
                    });

                    foreach (var tempDataField in tempDataFields)
                    {
                        dataFields.Add(tempDataField);
                    }

                    ViewBag.FormulaDataColumnId = dataFields;

                    // START Add field relation nếu có
                    var relationDataTables = _eDataTableService.GetRelationTables((int)reportQuery.DataTableId);
                    foreach (var relationDataTable in relationDataTables)
                    {
                        var temps = _dataFieldService.Gets(p => p.DataTableId == relationDataTable.DataTableId)
                        .Select(f => new SelectListItem
                        {
                            Selected = reportQuery.FormulaDataColumnId.HasValue && reportQuery.FormulaDataColumnId == f.DataFieldId,
                            Text = relationDataTable.Name + "." + f.FieldName,
                            Value = f.DataFieldId.ToString()
                        }).ToList();

                        foreach (var temp in temps)
                        {
                            tempDataFields.Add(temp);
                        }
                    }
                    // END Add field relation nếu có

                    ViewBag.FieldNames = tempDataFields.OrderBy(p => p.Text.Split('.')[1]);
                }
                else
                {
                    ViewBag.FormulaDataColumnId = new List<SelectListItem>()
                    {
                        new SelectListItem
                        {
                            Selected = true,
                            Text = "Cột công thức",
                            Value = "0"
                        }
                    };
                    ViewBag.FieldNames = new List<SelectListItem>();
                }
            }

            var tableNames = new List<SelectListItem>();
            tableNames.Add(new SelectListItem
            {
                Selected = !reportQuery.DataTableId.HasValue,
                Text = "Danh sách bảng",
                Value = "0"
            });

            var eTables = _eDataTableService.Gets().ToList();

            foreach (var item in eTables)
            {
                tableNames.Add(new SelectListItem
                {
                    Selected = reportQuery.DataTableId.HasValue && reportQuery.DataTableId == item.DataTableId,
                    Text = item.Name + " &&& "+ item.Description,
                    Value = item.DataTableId.ToString()
                });
            }

            ViewBag.DataTableId = tableNames;

            var filters = new List<ReportQueryFilterModel>();
            if (reportQuery.ReportQueryId == 0)
            {
                var tempReportQueryFilterModel = new ReportQueryFilterModel
                {
                    DataFieldName = string.Empty,
                    IsFilter = true,
                    IsSummary = true
                };

                filters.Add(tempReportQueryFilterModel);
                filters.Add(tempReportQueryFilterModel);
            }
            else
            {
                var reportQueryFilters = _reportQueryService.GetFilters(p => p.ReportQueryId == reportQuery.ReportQueryId);

                filters = _reportQueryService
                    .GetFilters(p => p.ReportQueryId == reportQuery.ReportQueryId)
                    .Select(p => new ReportQueryFilterModel
                    {
                        ReportQueryFilterId = p.ReportQueryFilterId,
                        ReportQueryId = p.ReportQueryId,
                        DataFieldId = p.DataFieldId,
                        DataFieldName = _dataFieldService.GetDataFieldName(p.DataFieldId),
                        Condition = p.Condition,
                        FilterToValue = p.FilterToValue,
                        FilterValue = p.FilterValue,
                        FilterValues = p.FilterValues,
                        IsFilter = p.IsFilter,
                        IsSummary = p.IsSummary
                    }).ToList();
            }

            ViewBag.Filters = filters;
        }
        #endregion private method
    }
}