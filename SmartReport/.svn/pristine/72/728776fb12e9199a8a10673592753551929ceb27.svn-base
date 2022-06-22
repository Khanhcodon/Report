using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Globalization;
using Bkav.eGovCloud.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ReportRuleController : CustomController
    {
        private readonly ReportRuleBll _reportRuleService;
        private readonly ReportKeyBll _reportKeyService;

        private readonly ReportGroupBll _reportGroupService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly ResourceBll _resourceService;
        private readonly DocColumnSettingBll _docColumnSetting;
        private readonly DocTypeBll _docTypeService;
        private readonly ReportModeBll _reportModeService;

        public ReportRuleController(
           ResourceBll resourceService,
           ReportRuleBll reportRuleService,
           ReportKeyBll reportKeyService,
           DepartmentBll departmentService,
           PositionBll positionService,
           UserBll userService,
           FileUploadSettings fileUploadSettings,
           ReportGroupBll reportGroupService, 
           DocColumnSettingBll columnSetting,
           DocTypeBll docTypeService,
           ReportModeBll reportModeService)
            : base()
        {
            _reportRuleService = reportRuleService;
            _resourceService = resourceService;
            _reportKeyService = reportKeyService;
            _departmentService = departmentService;
            _positionService = positionService;
            _userService = userService;
            _fileUploadSettings = fileUploadSettings;
            _reportGroupService = reportGroupService;
            _docColumnSetting = columnSetting;
            _docTypeService = docTypeService;
            _reportModeService = reportModeService;
        }
        public ActionResult Index()
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportRule.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportRule.NotPermission"));
                return RedirectToAction("Index");
            }
            var model = _reportRuleService.Gets(null).ToListModel();
            return View(model);
        }

        public ActionResult Create()
        {
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportRule.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportRule.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            var allReportModes = _reportModeService.GetsAs(p => new
           ReportModels  {
               ReportModelId =  p.ReportModeId,
               Name  =  p.Name
            }).ToList();
            ViewBag.AllReportModes = allReportModes;
            return View(new ReportRuleModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportCreate")]
        public ActionResult Create(ReportRuleModel model)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportRule.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportRule.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid) return View(model);
            try
            {
                var entity = model.ToEntity();
                _reportRuleService.Create(entity);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportRule.Created"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportRule.Created"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportRule.NotPermissionEdit"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportRule.NotPermissionEdit"));
                return RedirectToAction("Index");
            }
            ReportRuleModel model = null;
            try
            {
                var report = _reportRuleService.Get(id);
                ViewBag.ReportRuleId = id;
                if (report == null)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportRule.NotExist"));
                    return RedirectToAction("Index");
                }
                model = report.ToModel();

                var listSelectedReportMode = new  List<Bkav.eGovCloud.Entities.Customer.ReportModes>();
                var _listReportMode = report.ReportMode;

                if (_listReportMode != null && _listReportMode != "null") {
                    var listReportMode = JArray.Parse(_listReportMode);
                    if (listReportMode.Count > 0)
                    {
                        for (var i = 0; i < listReportMode.Count; i++)
                        {
                            var reportMode = Int32.Parse(listReportMode[i].ToString());
                            listSelectedReportMode.Add(_reportModeService.Get(reportMode));
                        }
                    }

                    StringBuilder str = new StringBuilder();
                    if (listSelectedReportMode.Count > 0)
                    {
                        foreach (var rep in listSelectedReportMode)
                        {
                            str.Append("<option selected value='");
                            str.Append(rep.ReportModeId);
                            str.Append("'");
                            str.Append(">");
                            str.Append(rep.Name);
                            str.Append("</option>");
                        }
                    }
                    ViewBag.AllSelectedReportMode = str;
                }

                // all list report mode
                var allReportModes = _reportModeService.GetsAs(p => new
                       ReportModels
                {
                    ReportModelId = p.ReportModeId,
                    Name = p.Name
                }).ToList();
                ViewBag.AllReportModes = allReportModes;
            }
            catch
            {
                //
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "ReportEdit")]
        public ActionResult Edit(ReportRuleModel model)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportRule.NotPermissionEdit"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportRule.NotPermissionEdit"));
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid) return View(model);
            try
            {
                var report = _reportRuleService.Get(model.ReportRuleId);
                if (report == null)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.NotExist"));
                    return RedirectToAction("Index");
                }

                report = model.ToEntity(report);
                _reportRuleService.Update(report);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.Updated"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.Updated"));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportDelete")]
        public ActionResult Delete(int id)
        {
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportRule.NotPermissionDelete"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportRule.NotPermissionDelete"));
                return RedirectToAction("Index");
            }
            try
            {
                var report = _reportRuleService.Get(id);
                _reportRuleService.Delete(report);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportRule.Deleted"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportRule.Deleted"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Index");

        }
    }
}