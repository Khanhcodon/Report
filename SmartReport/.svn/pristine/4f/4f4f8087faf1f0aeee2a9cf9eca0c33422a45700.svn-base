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
using System.Text;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class ReportQueryGroupController : CustomController
    {
        public ReportQueryGroupBll _reportQueryGroupService;
        public ReportQueryBll _reportQueryService;
        public ReportQueryFilterBll _reportQueryFilterService;
        public TemplateKeyCategoryBll _templateKeyCategoryService;
        public E_DataTableBll _eDataTableService;
        public DataFieldBll _dataFieldService;
        private readonly ResourceBll _resourceService;

        public ReportQueryGroupController(
            ReportQueryGroupBll reportQueryGroupService,
            ReportQueryBll reportQueryService,
            ReportQueryFilterBll reportQueryFilterService,
            TemplateKeyCategoryBll templateKeyCategoryService,
            DataFieldBll dataFieldService,
            E_DataTableBll eDataTableService,
            ResourceBll resourceService)
        {
            _reportQueryGroupService = reportQueryGroupService;
            _reportQueryService = reportQueryService;
            _reportQueryFilterService = reportQueryFilterService;
            _templateKeyCategoryService = templateKeyCategoryService;
            _dataFieldService = dataFieldService;
            _eDataTableService = eDataTableService;
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

            var reportQueryGroups = _reportQueryGroupService.Gets();
            var model = reportQueryGroups.Select(p => new ReportQueryGroupModel
            {
                ReportQueryGroupId = p.ReportQueryGroupId,
                ReportQueryGroupName = p.ReportQueryGroupName,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                ReportQuerys = _reportQueryGroupService.GetReportQuerys(p.Querys)
            });

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var reportQueryGroup = _reportQueryGroupService.Get(id);
                _reportQueryGroupService.Delete(reportQueryGroup);
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

            var model = new ReportQueryGroupModel();

            LoadDropDownData(new ReportQueryGroup());
           
            ViewBag.ReportQueryData = "[]";

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ReportQueryGroupModel model)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQueryGroup.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQueryGroup.NotPermission"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var tempReportQueryGroups = _reportQueryGroupService.Gets(p => p.ReportQueryGroupName == model.ReportQueryGroupName);
                if (tempReportQueryGroups.Count() > 0)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryGroupName.Exist"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryGroupName.Exist"));
                    ModelState.AddModelError("ReportQueryName", _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryGroupName.Exist"));

                    LoadDropDownData(new ReportQueryGroup());

                    return View(model);
                }

                var reportQueryGroup = model.ToEntity();
                reportQueryGroup.CreatedBy = User.GetUserId();
                reportQueryGroup.CreatedAt = DateTime.Now;

                try
                {
                    _reportQueryGroupService.Create(reportQueryGroup, model.ReportQueryIds);

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryGroup.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryGroup.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            LoadDropDownData(new ReportQueryGroup());

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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQueryGroup.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQueryGroup.NotPermissionEdit"));
                return RedirectToAction("Index");
            }

            var reportQueryGroup = _reportQueryGroupService.Get(id);
            if (reportQueryGroup == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryGroup.NotExist"));
                return RedirectToAction("Index");
            }

            LoadDropDownData(reportQueryGroup);
            var model = reportQueryGroup.ToModel();
            var reportQueryIds = reportQueryGroup.Querys.Select(p => p.ReportQueryId).ToList<int>();
            if(reportQueryIds.Count == 0)
                return View(model);
            var reportTmp = _reportQueryService.Get(reportQueryIds[0]);
            var tableId = reportTmp?.DataTableId ?? 0;

            var reportQuerys = _reportQueryService.Gets().Where(p => reportQueryIds.Contains(p.ReportQueryId)).ToList();
            model.Query = _reportQueryService.GenerateQueryByGroup(reportQuerys);

            if (model.Query != string.Empty)
            {
                var errMsg = string.Empty;
                ViewBag.ReportQueryData = Json2.Stringify(_reportQueryService.GetReportQueryData(model.Query, tableId, out errMsg));
                ViewBag.ErrMsg = errMsg;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ReportQueryGroupModel model)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportQueryGroup.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.ReportQueryGroup.NotPermissionEdit"));
                return RedirectToAction("Index");
            }
            var reportQueryGroup = new ReportQueryGroup();
            if (ModelState.IsValid)
            {
                reportQueryGroup = _reportQueryGroupService.Get(model.ReportQueryGroupId);
                if (reportQueryGroup == null)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportQueryGroup.NotExist"));
                    return RedirectToAction("Index");
                }

                reportQueryGroup = model.ToEntity(reportQueryGroup);
                reportQueryGroup.UpdatedAt = DateTime.Now;
                reportQueryGroup.UpdatedBy = User.GetUserId();
                _reportQueryGroupService.Update(reportQueryGroup, model.ReportQueryIds);

                return RedirectToAction("Index");
            }

            LoadDropDownData(reportQueryGroup);
            return View(model);
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
                        Text = f.FieldName,
                        Value = f.DataFieldId.ToString()
                    }
                );

                return Json(new { Data = dataFields, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = new List<SelectListItem>(), success = false, message = "Có lỗi xảy ra trong quá trình tải dữ liệu" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetValuesByDataField(int dataFieldId)
        {
            var type = string.Empty;
            var result = _reportQueryService.GetValuesByDataField(dataFieldId, out type);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCategoryTemplateKey()
        {
            return Json(new { success = true, result = GetTreeTemplateKey() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// VuHQ TBD
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GenerateQuery(ReportQueryModel model)
        {
            if (true)
            {
                return Json(new { Data = string.Empty, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = new List<SelectListItem>(), success = false, message = "Có lỗi xảy ra trong quá trình tải dữ liệu" }, JsonRequestBehavior.AllowGet);
            }
        }



        private void LoadDropDownData(ReportQueryGroup reportQueryGroup)
        {
            if (reportQueryGroup.ReportQueryGroupId == 0)
            {
                ViewBag.Querys = new List<ReportQueryModel>();
            }
            else
            {
                ViewBag.Querys = reportQueryGroup.Querys.Select(p => new ReportQueryModel
                {
                    ReportQueryId = p.ReportQueryId,
                    ReportQueryName = _reportQueryService.GetName(p.ReportQueryId),
                    ActionLevelName = _reportQueryService.GetCategoryName(p.ReportQueryId)
                }).ToList();
            }
        }
        private IEnumerable<TemplateKeyTree> GetTreeTemplateKey()
        {
            //var enumValArray = _templateKeyCategoryService.Gets();
            var actionLevels = _reportQueryService.GetActionLevels(null);
            foreach (var val in actionLevels)
            {
                var actionLevelId = int.Parse(val.Value);
                var obj = new TemplateKeyTree()
                {
                    CategoryId = actionLevelId,
                    CategoryName = val.Text,
                    ChidrenList = _reportQueryService.Gets(x => x.ActionLevelId == actionLevelId).Select(c => new ChidrenTemplate
                    { TemplateKeyId = c.ReportQueryId, Name = c.ReportQueryName })
                };
                yield return obj;
            }
        }

        #endregion private method
    }
}