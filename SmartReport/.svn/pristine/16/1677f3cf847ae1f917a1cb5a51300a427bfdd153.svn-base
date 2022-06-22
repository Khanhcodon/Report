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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Bkav.eGovCloud.Entities.Enum;
using System.Security.Principal;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ReportController : CustomAsyncController
    {
        private readonly ReportBll _reportService;
        private readonly ReportGroupBll _reportGroupService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly ResourceBll _resourceService;
        private readonly DocColumnSettingBll _docColumnSetting;
        private readonly TemplateKeyBll _templateKeyService;
        private readonly FormBll _formService;

        public ReportController(
            ResourceBll resourceService,
            ReportBll reportService,
            DepartmentBll departmentService,
            PositionBll positionService,
            UserBll userService,
            FileUploadSettings fileUploadSettings,
            ReportGroupBll reportGroupService,
            DocColumnSettingBll columnSetting,
            TemplateKeyBll templateKeyService,
            FormBll formService)
            : base()
        {
            _resourceService = resourceService;
            _reportService = reportService;
            _departmentService = departmentService;
            _positionService = positionService;
            _userService = userService;
            _fileUploadSettings = fileUploadSettings;
            _reportGroupService = reportGroupService;
            _docColumnSetting = columnSetting;
            _templateKeyService = templateKeyService;
            _formService = formService;
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

            ViewBag.AllReports = _reportService.GetsAs(r => new
            {
                id = r.ReportId,
                name = r.Name,
                parentid = r.ParentId,
                isActivated = r.IsActive,
                isHSMC = r.IsHSMC
            }).OrderBy(r => r.name).StringifyJs();
            ViewBag.AllDepartments = GetAllDepartments();
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.AllPositions = _positionService.GetCacheAllPosition().StringifyJs();
            return View();
        }

        #region Report

        public ActionResult Create(int id)
        {
            if (!HasPermission())
            {
                return RedirectToAction("Index");
            }

            ViewBag.ReportGroup = _reportGroupService.GetGroups(p => p.IsReport).ToListModel();
            ViewBag.ColumnSetting = GetDocColumnSetting(1);
            #region
            var descriptions = new List<SelectListItem>()
                {
                new SelectListItem
                {
                    Selected = true,
                    Text = "Tùy Chọn",
                    Value = "TuyChon"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Ngày",
                    Value = "TrongNgay"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Tuần",
                    Value = "TrongTuan"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Tháng",
                    Value = "TrongThang"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Quý",
                    Value = "TrongQuy"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Nữa Năm",
                    Value = "NuaNam"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Năm",
                    Value = "TrongNam"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Chín Tháng",
                    Value = "ChinThang"
                }
            };
            #endregion     
            var listViewReports = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Selected = true,
                    Text = "Tùy Chọn",
                    Value = "0"
                },
                new SelectListItem()
                {
                    Selected = false,
                    Text = "Hiển Thị Dạng File",
                    Value = "1"
                },
                new SelectListItem()
                {
                    Selected = false,
                    Text = "Hiển Thị Dạng Báo Cáo",
                    Value = "2"
                },
                new SelectListItem()
                {
                    Selected = false,
                    Text = "Hiển Thị Dạng BC Thuyết Minh",
                    Value = "4"
                }

            };
            ViewBag.Description = descriptions;
            ViewBag.ListViewReport = listViewReports;
            return PartialView("_CreateOrEditReport", new ReportModel() { ParentId = id, ReportId = 0, IsActive = true });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportCreate")]
        public JsonResult Create(ReportModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    // ErrorNotification("Bạn không có quyền thao tác với trang này!.");
            //    //  return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                SetPermission(model);
                var entity = model.ToEntity();
                _reportService.Create(entity);
                return
                    Json(
                        new
                        {
                            functionType = "Create",
                            item =
                            new
                            {
                                id = entity.ReportId,
                                name = entity.Name,
                                parentid = entity.ParentId,
                                isActivated = entity.IsActive
                            }
                        });
            }

            return null; 
        }

        public ActionResult Edit(int id)
        {
            if (!HasPermission())
            {
                return RedirectToAction("Index");
            }
            Report report = null;
            try
            {
                report = _reportService.Get(id);
                if (report == null)
                {   
                    return PartialView("_CreateOrEditReport", new ReportModel() { ParentId = id, ReportId = 0 });
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.ReportGroup = _reportGroupService.GetGroups(p => p.IsReport).ToListModel();
            ViewBag.ColumnSetting = GetDocColumnSetting(report.DocColumnId);
            #region
            var descriptions = new List<SelectListItem>()
                {
                new SelectListItem
                {
                    Selected = false,
                    Text = "Tùy Chọn",
                    Value = "TuyChon"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Ngày",
                    Value = "TrongNgay"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Tuần",
                    Value = "TrongTuan"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Tháng",
                    Value = "TrongThang"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Quý",
                    Value = "TrongQuy"
                },           
                new SelectListItem
                {
                    Selected = false,
                    Text = "Nữa Năm",
                    Value = "NuaNam"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Trong Năm",
                    Value = "TrongNam"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Chín Tháng",
                    Value = "ChinThang"
                }
            };
            #endregion
            #region
            if (report.Description == "TrongNgay")
            {
                descriptions[1].Selected = true;
            }
            else if (report.Description == "TrongTuan")
            {
                descriptions[2].Selected = true;
            }
            else if (report.Description == "TrongThang")
            {
                descriptions[3].Selected = true;
            }
            else if (report.Description == "TrongQuy")
            {
                descriptions[4].Selected = true;
            }         
            else if (report.Description == "NuaNam")
            {
                descriptions[5].Selected = true;
            }
            else if (report.Description == "TrongNam")
            {
                descriptions[6].Selected = true;
            }
            else if (report.Description == "ChinThang")
            {
                descriptions[7].Selected = true;
            }
            else
            {
                descriptions[0].Selected = true;
            }
            #endregion

            var listViewReports = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Selected = true,
                    Text = "Tùy Chọn",
                    Value = "0"
                },
                new SelectListItem()
                {
                    Selected = false,
                    Text = "Hiển Thị Dạng File",
                    Value = "1"
                },
                new SelectListItem()
                {
                    Selected = false,
                    Text = "Hiển Thị Dạng Báo Cáo",
                    Value = "2"
                },
                new SelectListItem()
                {
                    Selected = false,
                    Text = "Hiển Thị Dạng BC Thuyết Minh",
                    Value = "4"
                }

            };
            if(report.IsFile == 1)
            {
                listViewReports[1].Selected = true;
            }else if(report.IsFile == 2)
            {
                listViewReports[2].Selected = true;
            }else if(report.IsFile == 4)
            {
                listViewReports[3].Selected = true;
            }else
            {
                listViewReports[0].Selected = true;
            }

            ViewBag.Description = descriptions;
            ViewBag.ListViewReport = listViewReports;
            return PartialView("_CreateOrEditReport", report.ToModel());
        }


        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "ReportEdit")]
        public JsonResult Edit(ReportModel model)
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
                var report = _reportService.Get(model.ReportId);
                if (report == null)
                {
                    return Json(new { error = true, message = _resourceService.GetResource("Admin.Report.NotFound") });
                }
                if (model.ParentId == 0)
                {
                    model.ParentId = null;
                }
                SetPermission(model);

                var allChildren = GetChild(report.ReportId).Select(s => s.ReportId).Distinct();
                foreach (var reportId in allChildren)
                {
                    var reportChild = _reportService.Get(reportId);
                    if (reportChild != null)
                    {
                        reportChild.IsHSMC = model.IsHSMC;
                        _reportService.Update(reportChild);
                    }
                }

                var oldName = report.Name;
                var oldParent = report.ParentId;
                Dictionary<string, string> temFile = null;
                Dictionary<string, string> temFileGroup = null;
                if (!string.IsNullOrEmpty(model.ReportPath))
                {
                    temFile = Json2.ParseAs<Dictionary<string, string>>(model.ReportPath);
                }
                if (!string.IsNullOrEmpty(model.ReportGroupPath))
                {
                    temFileGroup = Json2.ParseAs<Dictionary<string, string>>(model.ReportGroupPath);
                }
                report = model.ToEntity(report);
                report.ParentId = oldParent;
                _reportService.Update(report, temFile, temFileGroup);
                return
                    Json(
                        new
                        {
                            functionType = "Update",
                            item =
                            new
                            {
                                id = report.ReportId,
                                name = report.Name,
                                oldname = oldName,
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
                _reportService.Delete(id);
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
                var newReport = _reportService.Copy(targetId, toParentId);
                return Json(new
                {
                    success = true,
                    id = newReport.ReportId,
                    name = newReport.Name,
                    parentId = newReport.ParentId.HasValue ? newReport.ParentId.Value : 0,
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

        #region Report Group

        public ActionResult ListGroup()
        {
            if (!HasPermission())
            {
                return RedirectToAction("Index");
            }

            var model = _reportGroupService.GetGroups();
            return View(model.ToListModel());
        }

        public ActionResult AddGroup()
        {
            //if (!HasPermission())
            //{
            //    return RedirectToAction("Index");
            //}

            return View(new ReportGroupModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportAddGroup")]
        public ActionResult AddGroup(ReportGroupModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            ////    ErrorNotification("Bạn không có quyền thao tác với trang này!.");
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                _reportGroupService.CreateGroup(entity);
                return RedirectToAction("AddGroup");
            }
            return View(model);
        }

        public ActionResult EditGroup(int id)
        {
            if (!HasPermission())
            {
                return RedirectToAction("Index");
            }
            var group = _reportGroupService.GetGroup(id);
            return View(group.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportEditGroup")]
        public ActionResult EditGroup(ReportGroupModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                //  ErrorNotification("Bạn không có quyền thao tác với trang này!.");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var group = _reportGroupService.GetGroup(model.ReportGroupId);
                var entity = model.ToEntity(group);
                _reportGroupService.UpdateGroup(entity);
                return RedirectToAction("ListGroup");
            }
            return View(model);
        }

        public ActionResult DeleteGroup(int id)
        {
            if (!HasPermission())
            {
                return RedirectToAction("Index");
            }
            try
            {
                _reportGroupService.DeleteGroup(id);
            }
            catch
            {

            }
            return RedirectToAction("ListGroup");
        }

        #endregion

        #region Private Methods

        IEnumerable<Report> GetChild(int id)
        {
            return _reportService.Gets().Where(x => x.ParentId == id || x.ReportId == id).Union(_reportService.Gets().Where(x => x.ParentId == id).SelectMany(y => GetChild(y.ReportId)));
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
