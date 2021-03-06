using System.Collections.Generic;
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

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ReportModeController : CustomController
    {
        private readonly ReportModeBll _reportModeService;
        private readonly ReportKeyBll _reportKeyService;

        private readonly ReportGroupBll _reportGroupService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly ResourceBll _resourceService;
        private readonly DocColumnSettingBll _docColumnSetting;
        private readonly DocTypeBll _docTypeService;
        public ReportModeController(
            ResourceBll resourceService,
            ReportModeBll reportModeService,
            ReportKeyBll reportKeyService,
            DepartmentBll departmentService,
            PositionBll positionService,
            UserBll userService,
            FileUploadSettings fileUploadSettings,
            ReportGroupBll reportGroupService, DocColumnSettingBll columnSetting,
            DocTypeBll docTypeService)
            : base()
        {
            _reportModeService = reportModeService;
            _resourceService = resourceService;
            _reportKeyService = reportKeyService;
            _departmentService = departmentService;
            _positionService = positionService;
            _userService = userService;
            _fileUploadSettings = fileUploadSettings;
            _reportGroupService = reportGroupService;
            _docColumnSetting = columnSetting;
            _docTypeService = docTypeService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportMode.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportMode.NotPermission"));
                return RedirectToAction("Index");
            }
            var model = _reportModeService.Gets(null).ToListModel();
            return View(model);
        }

        #region Report

        public ActionResult Create()
        {
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportMode.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportMode.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            var allDocType = _docTypeService.GetsAs(p => new
            {
                p.DocTypeId,
                p.DocTypeName,
                p.ReportModeId
            });
            ViewBag.AllDocTypes = allDocType.Stringify();
            return View(new ReportModeModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportCreate")]
        public ActionResult Create(ReportModeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportMode.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportMode.NotPermissionCreate"));
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrWhiteSpace(model.TmpIssueDate))
            {
                DateTime dt;
                var resultTime = DateTime.TryParseExact(model.TmpIssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dt);
                if (resultTime)
                {
                    model.IssueDate = dt;
                }
                else
                {
                    ModelState.AddModelError("TmpIssueDate", _resourceService.GetResource("ReportMode.CreateOrEdit.Fields.IssueDate.NotValid"));
                }
            }
            else
            {
                model.IssueDate = null;
            }
            if (!ModelState.IsValid) return View(model);
            try
            {
                model.RefNotation = JsonConvert.SerializeObject(model.RefNotations);
                model.Attachments = JsonConvert.SerializeObject(model.AFiles);
                var entity = model.ToEntity();
                _reportModeService.Create(entity, model.DocTypeIds);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.Created"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.Created"));
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
                ErrorNotification(_resourceService.GetResource("Customer.ReportMode.NotPermissionEdit"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportMode.NotPermissionEdit"));
                return RedirectToAction("Index");
            }
            ReportModeModel model = null;
            try
            {
               var report = _reportModeService.Get(id);
                ViewBag.ReportModeId = id;
                if (report == null)
               {
                   ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.NotExist"));
                   return RedirectToAction("Index");
               }
               model = report.ToModel();

               var orderNumber = 1;

                var allDocType = _docTypeService.GetsAs(p => new
                {
                    p.DocTypeId,
                    p.DocTypeName,
                    p.ReportModeId
                });

                ViewBag.SelectedDocType = allDocType.Where(p => p.ReportModeId == id)
                    .Select(re => new
                     {
                         _orderNumber = orderNumber++,
                        DocTypeId = re.DocTypeId,
                        DocTypeName = re.DocTypeName,
                        ReportModeId = re.ReportModeId
                     }).Stringify();
                ViewBag.AllDocTypes = allDocType.Stringify();
                model.TmpIssueDate = model.IssueDate.HasValue ? model.IssueDate.Value.ToString("dd/MM/yyyy") : string.Empty;
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
        public ActionResult Edit(ReportModeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ReportMode.NotPermissionEdit"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportMode.NotPermissionEdit"));
                return RedirectToAction("Index");
            }

            if (!string.IsNullOrWhiteSpace(model.TmpIssueDate))
            {
                DateTime dt;
                var resultTime = DateTime.TryParseExact(model.TmpIssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dt);
                if (resultTime)
                {
                    model.IssueDate = dt;
                }
                else
                {
                    ModelState.AddModelError("TmpIssueDate", _resourceService.GetResource("ReportMode.CreateOrEdit.Fields.IssueDate.NotValid"));
                }
            }
            else
            {
                model.IssueDate = null;
            }
            if (!ModelState.IsValid) return View(model);
            try
            {
                var report = _reportModeService.Get(model.ReportModeId);
                if (report == null)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.NotExist"));
                    return RedirectToAction("Index");
                }
                model.RefNotation = JsonConvert.SerializeObject(model.RefNotations);
                model.Attachments = JsonConvert.SerializeObject(model.AFiles);
                report = model.ToEntity(report);
                _reportModeService.Update(report, model.DocTypeIds);
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
                ErrorNotification(_resourceService.GetResource("Customer.ReportMode.NotPermissionDelete"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ReportMode.NotPermissionDelete"));
                return RedirectToAction("Index");
            }
            try
            {
                var report = _reportModeService.Get(id);
                _reportModeService.Delete(report);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.Deleted"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ReportMode.Deleted"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Index");
           
        }

        public JsonResult Copy(int targetId, int toParentId)
        {
            if (!HasPermission())
            {
                return Json(new { error = true, message = _resourceService.GetResource("Common.Notify.NotPermissionForAction") }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var newReport = _reportKeyService.Copy(targetId, toParentId);
                return Json(data: new
                {
                    success = true,
                    id = newReport.ReportKeyId,
                    name = newReport.Name,
                    parentId = newReport.ParentId ?? 0,
                    isActivated = newReport.IsActive
                });
            }
            catch
            {
                return Json(new { error = true, message = _resourceService.GetResource("Report.Action.Copy.Error") });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportUploadTempAsync")]
        public void UploadTempAsync(HttpPostedFileBase files)
        {
            AsyncManager.OutstandingOperations.Increment();

            var task =
                    Task.Factory.StartNew(() =>
                    {
                        var length = files.InputStream.Length;
                        if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
                        {
                            return
                                new
                                {
                                    key = "",
                                    size = length,
                                    name = files.FileName,
                                    extension = "",
                                    error = _resourceService.GetResource("Admin.Report.Upload.FileTooLarge")
                                };
                        }
                        var ext = Path.GetExtension(files.FileName);
                        if (ext != ".rpt")
                        {
                            return
                                new
                                {
                                    key = "",
                                    size = length,
                                    name = files.FileName,
                                    extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                                    error = _resourceService.GetResource("Admin.Report.Upload.Extention.RPTOnly")
                                };
                        }
                        var fileName = files.FileName;
                        var temp = FileManager.Default.Create(files.InputStream, ResourceLocation.Default.CrystalReport);
                        return
                            new
                            {
                                key = temp.Name,
                                size = temp.Length,
                                name = fileName,
                                extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                                error = ""
                            };
                    });
            Task.Factory.ContinueWhenAll(new Task[] { task }, tasks =>
            {
                var listFile = tasks.Select(item => item);
                AsyncManager.Parameters["data"] = listFile;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public JsonResult UploadTempCompleted(dynamic data)
        {
            return Json(data);
        }

        #endregion
    }
}
