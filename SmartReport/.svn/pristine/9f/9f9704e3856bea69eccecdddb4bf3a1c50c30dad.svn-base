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

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ReportKeyController : CustomController
    {
        private readonly ReportKeyBll _reportKeyService;

        private readonly ReportGroupBll _reportGroupService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly ResourceBll _resourceService;
        private readonly DocColumnSettingBll _docColumnSetting;

        public ReportKeyController(
            ResourceBll resourceService,
            ReportKeyBll reportKeyService,
            DepartmentBll departmentService,
            PositionBll positionService,
            UserBll userService,
            FileUploadSettings fileUploadSettings,
            ReportGroupBll reportGroupService, DocColumnSettingBll columnSetting)
            : base()
        {
            _resourceService = resourceService;
            _reportKeyService = reportKeyService;
            _departmentService = departmentService;
            _positionService = positionService;
            _userService = userService;
            _fileUploadSettings = fileUploadSettings;
            _reportGroupService = reportGroupService;
            _docColumnSetting = columnSetting;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                //   ErrorNotification("Bạn không có quyền thao tác với trang này!.");
                return RedirectToAction("Index");
            }

            ViewBag.AllReports = _reportKeyService.GetsAs(r => new
            {
                id = r.ReportKeyId,
                name = r.Name,
                parentid = r.ParentId
            }).OrderBy(r => r.name).StringifyJs();
            //ViewBag.AllDepartments = GetAllDepartments();
            //ViewBag.AllUsers = GetAllUsers();
            //ViewBag.AllPositions = _positionService.GetCacheAllPosition().StringifyJs();
            return View();
        }

        #region Report

        public ActionResult Create(int id)
        {
            //if (!HasPermission())
            //{
            //    return RedirectToAction("Index");
            //}

            return PartialView("_CreateOrEdit", new ReportKeyModel { ParentId = id, ReportKeyId = 0, IsActive = true });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportCreate")]
        public JsonResult Create(ReportKeyModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    // ErrorNotification("Bạn không có quyền thao tác với trang này!.");
            //    //  return RedirectToAction("Index");
            //}

            if (!ModelState.IsValid) return null;
            //SetPermission(model);
            var entity = model.ToEntity();
            _reportKeyService.Create(entity);
            return
                Json(
                    new
                    {
                        functionType = "Create",
                        item =
                            new
                            {
                                id = entity.ReportKeyId,
                                name = entity.Name,
                                parentid = entity.ParentId,
                                isActivated = entity.IsActive
                            }
                    });

        }

        public ActionResult Edit(int id)
        {
            //if (!HasPermission())
            //{
            //    return RedirectToAction("Index");
            //}
            ReportKey report = null;
            try
            {
                report = _reportKeyService.Get(id);
                if (report == null)
                {
                    return PartialView("_CreateOrEdit", new ReportKeyModel { ParentId = id, ReportKeyId = 0 });
                }
            }
            catch
            {
                //
            }
            //ViewBag.ReportGroup = _reportGroupService.GetGroups(p => p.IsReport).ToListModel();
            //ViewBag.ColumnSetting = GetDocColumnSetting(report.DocColumnId);
            return PartialView("_CreateOrEdit", report.ToModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "ReportEdit")]
        public JsonResult Edit(ReportKeyModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                //  ErrorNotification("Bạn không có quyền thao tác với trang này!.");
                // return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var report = _reportKeyService.Get(model.ReportKeyId);
                if (report == null)
                {
                    return Json(new { error = true, message = _resourceService.GetResource("Admin.Report.NotFound") });
                }
                if (model.ParentId == 0)
                {
                    model.ParentId = null;
                }
                //SetPermission(model);

                var allChildren = GetChild(report.ReportKeyId).Select(s => s.ReportKeyId).Distinct();
                foreach (var reportId in allChildren)
                {
                    var reportChild = _reportKeyService.Get(reportId);
                    if (reportChild != null)
                    {
                        _reportKeyService.Update(reportChild);
                    }
                }

                //var oldName = report.Name;
                //var oldParent = report.ParentId;
                //Dictionary<string, string> temFile = null;
                //Dictionary<string, string> temFileGroup = null;
                //if (!string.IsNullOrEmpty(model.ReportPath))
                //{
                //    temFile = Json2.ParseAs<Dictionary<string, string>>(model.ReportPath);
                //}
                //if (!string.IsNullOrEmpty(model.ReportGroupPath))
                //{
                //    temFileGroup = Json2.ParseAs<Dictionary<string, string>>(model.ReportGroupPath);
                //}
                //report = model.ToEntity(report);
                //report.ParentId = oldParent;
                _reportKeyService.Update(report);
                return
                    Json(
                        new
                        {
                            functionType = "Update",
                            item =
                            new
                            {
                                id = report.ReportKeyId,
                                name = report.Name,
                                parentid = report.ParentId,
                                isActivated = report.IsActive
                            }
                        });
            }
            return null;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportDelete")]
        public JsonResult Delete(int id)
        {
            if (!HasPermission())
            {
                return Json(new { error = _resourceService.GetResource("Common.Notify.NotPermissionForAction") });
            }

            try
            {
                _reportKeyService.Delete(id);
                return Json(new { success = _resourceService.GetResource("Admin.Report.Action.Delete.Success") });
            }
            catch
            {
                return Json(new { error = _resourceService.GetResource("Admin.Report.Action.Delete.NotAllow") });
            }
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

       

        #region Private Methods

        IEnumerable<ReportKey> GetChild(int id)
        {
            return _reportKeyService.Gets().Where(x => x.ParentId == id || x.ReportKeyId == id).Union(_reportKeyService.Gets().Where(x => x.ParentId == id).SelectMany(y => GetChild(y.ReportKeyId)));
        }

        private string GetAllDepartments()
        {
            var result = "[]";
            var allDepartments = _departmentService.GetCacheAllDepartments(true);
            if (allDepartments != null)
            {
                result = allDepartments
                            .Select(d =>
                                new
                                {
                                    label = d.DepartmentPath,
                                    value = d.DepartmentId,
                                    departmentName = d.DepartmentName,
                                    parentId = d.ParentId
                                }
                            )
                            .OrderBy(d => d.label).StringifyJs();
            }
            return result;
        }

        private string GetAllUsers()
        {
            var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
            return allUsers.Select(
                                u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                    fullname = u.FullName,
                                    username = u.Username,
                                    phone = u.Phone
                                }).StringifyJs();
        }

        private void SetPermission(ReportModel model)
        {
            if (model.PositionPermissionIds != null && model.PositionPermissionIds.Any())
            {
                model.PositionPermission = model.PositionPermissionIds.StringifyJs();
            }
            if (model.UserPermissionIds != null && model.UserPermissionIds.Any())
            {
                model.UserPermission = model.UserPermissionIds.StringifyJs();
            }
            if (model.DepartmentPositionIds != null && model.DepartmentPositionIds.Any())
            {
                var allDepartmentIds = _departmentService.GetsAs(d => d.DepartmentId, true);
                var allPositionIds = _positionService.GetsAs(p => p.PositionId);
                var departmentPositions = new List<IDictionary<string, int>>();
                foreach (var item in model.DepartmentPositionIds)
                {
                    var split = item.Split(',');
                    if (split.Length != 2)
                    {
                        continue;
                    }

                    int departmentParsed, positionParsed;
                    if (int.TryParse(split[0], out departmentParsed)
                        && int.TryParse(split[1], out positionParsed))
                    {
                        if (allDepartmentIds.Any(did => did == departmentParsed)
                            && allPositionIds.Any(pid => pid == positionParsed))
                        {
                            departmentPositions.Add(new Dictionary<string, int> { { "DepartmentId", departmentParsed }, { "PositionId", positionParsed } });
                        }
                    }
                }

                model.DeptPermission = departmentPositions.StringifyJs();
            }
            if (model.TreeGroupIds != null && model.TreeGroupIds.Any())
            {
                model.GroupForTree = model.TreeGroupIds.StringifyJs();
            }
        }

        private IEnumerable<SelectListItem> GetDocColumnSetting(int selected)
        {
            var columnSettings = _docColumnSetting.GetAllCaches().Where(c => c.TypeEnum == Entities.Enum.ColumnSettingType.Report);
            return columnSettings.Select(c => new SelectListItem()
            {
                Selected = c.DocColumnSettingId == selected,
                Text = c.DocColumnSettingName,
                Value = c.DocColumnSettingId.ToString()
            });
        }

        #endregion
    }
}
