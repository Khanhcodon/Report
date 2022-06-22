using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class DepartmentController : CustomController
    {
        private readonly DepartmentBll _departmentService;
        private readonly UserBll _userService;
        private readonly JobTitlesBll _jobTitlesService;
        private readonly PositionBll _positionService;
        private readonly ResourceBll _resourceService;

        public DepartmentController(
             DepartmentBll departmentService,
             UserBll userService,
             JobTitlesBll jobTitlesService,
             PositionBll positionService,
             ResourceBll resourceService)
        {
            _departmentService = departmentService;
            _userService = userService;
            _jobTitlesService = jobTitlesService;
            _positionService = positionService;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            var isAdminDepartmentUser = false;
            if (!HasPermission())
            {
                if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Department.NotPermission"));
                    ErrorNotification(_resourceService.GetResource("Customer.Department.NotPermission"));
                    return RedirectToAction("Index", "Welcome");
                }
                isAdminDepartmentUser = true;
            }

            var modeCreateOrUpdate = new DepartmentModel();
            ViewBag.AllDepartments = GetAllDepartment(isAdminDepartmentUser);
            ViewBag.AllJobTitless = GetAllJobTitless();
            ViewBag.AllPositions = GetAllPositions();
            ViewBag.AllUsers = GetAllUsers(isAdminDepartmentUser);
            ViewBag.IsAdminDepartmentUser = isAdminDepartmentUser;
            return View(modeCreateOrUpdate);
        }
        public ActionResult GetDetailById(int? id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Department.NotPermissionDetail"));
                    ErrorNotification(_resourceService.GetResource("Customer.Department.NotPermissionDetail"));
                    return RedirectToAction("Index", "Welcome");
                }
            }

            Department department = null;
            if (id.HasValue)
            {
                department = _departmentService.Get(id.Value);
            }

            if (department == null)
            {
                return PartialView("_CreateOrEdit");
            }

            ViewBag.SelectedUsers = department.UserDepartmentJobTitlesPositions.Select(a => new
                                                                                              {
                                                                                                  value = a.UserId,
                                                                                                  label = "",
                                                                                                  fullname = "",
                                                                                                  username = "",
                                                                                                  firstjobtitlesid = a.JobTitlesId,
                                                                                                  firstpositionid = a.PositionId,
                                                                                                  isadmin = a.IsAdmin,
                                                                                                  isprimary = a.IsPrimary,
                                                                                                  hasReceiveDocument = a.HasReceiveDocument
                                                                                              }).StringifyJs();
            var modelUpdate = department.ToModel();
            modelUpdate.EdocId = _departmentService.GetEdocId(department.DepartmentId);
            return PartialView("_CreateOrEdit", modelUpdate);
        }

        [HttpPost]
        public string CreateTree(int? parentId, string departmentName,
            int departmentOrder, bool hasCreatePacket = true)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Department.NotPermissionCreate"));
                return (new { error = _resourceService.GetResource("Customer.Department.NotPermissionCreate") }).StringifyJs();
            }

            var model = new DepartmentModel
            {
                ParentId = parentId,
                DepartmentName = departmentName,
                Order = departmentOrder,
                IsActivated = true,
                HasCreatePacket = hasCreatePacket
            };

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var entity = model.ToEntity();
                        entity.CreatedByUserId = User.GetUserId();
                        entity.CreatedOnDate = DateTime.Now;

                        var results = new List<dynamic>();
                        if (hasCreatePacket)
                        {
                            var names = model.DepartmentName.Split(';').Distinct();
                            var list = new List<Department>();
                            foreach (var name in names)
                            {
                                var item = entity.Clone();
                                item.Order = departmentOrder;
                                item.DepartmentName = name;
                                list.Add(item);
                                departmentOrder++;
                            }
                            _departmentService.Create(list, parentId);
                            results.AddRange(list.Select(p => new
                            {
                                value = p.DepartmentId,
                                label = p.DepartmentName,
                                parentid = parentId.HasValue ? parentId.Value : 0,
                                data = p.DepartmentName,
                                metadata = new { id = p.DepartmentId },
                                state = "leaf",
                                extvalue = p.DepartmentIdExt,
                                path = p.DepartmentPath,
                                order = p.Order,
                                level = p.Level,
                                attr = new { id = p.DepartmentId, rel = p.IsActivated ? "dept" : "dept-deactivated" }
                            }));
                        }
                        else
                        {
                            _departmentService.Create(entity);
                            results.Add(new
                            {
                                value = entity.DepartmentId,
                                label = entity.DepartmentName,
                                parentid = parentId.HasValue ? parentId.Value : 0,
                                data = entity.DepartmentName,
                                metadata = new { id = entity.DepartmentId },
                                state = "leaf",
                                extvalue = entity.DepartmentIdExt,
                                path = entity.DepartmentPath,
                                order = entity.Order,
                                level = entity.Level,
                                attr = new { id = entity.DepartmentId, rel = entity.IsActivated ? "dept" : "dept-deactivated" }
                            });
                        }

                        return results.StringifyJs();
                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        var result = new { error = ex.Message };
                        return result.StringifyJs();
                    }
                }
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DepartmentTree.CreatedError"));
            return (new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DepartmentTree.CreatedError") }).StringifyJs();
        }

        [HttpPost]
        public ActionResult UpdateTree(DepartmentModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Department.NotPermissionUpdate"));
                return Json(new { message = _resourceService.GetResource("Customer.Department.NotPermissionUpdate") });
            }

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var department = _departmentService.Get(model.DepartmentId);
                    if (department == null)
                    {
                        return Json(new { error = "error" });
                    }
                    var oldParenId = department.ParentId;
                    var oldDepartmentName = department.DepartmentName;
                    var oldIsActivated = department.IsActivated;
                    department = model.ToEntity(department);
                    department.ParentId = oldParenId;
                    department.LastModifiedByUserId = User.GetUserId();
                    department.LastModifiedOnDate = DateTime.Now;

                    try
                    {
                        _departmentService.Update(department, oldDepartmentName, oldIsActivated, model.EdocId);
                        CreateActivityLog(ActivityLogType.Admin, "Có sự thay đổi nội dung người nhận văn bản đến");
                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        CreateActivityLog(ActivityLogType.Admin, ex.Message);
                        return Json(new { message = ex.Message });
                    }
                    return Json(new { model.IsActivated, model.DepartmentName, department.DepartmentIdExt });
                }
            }
            return Json(model);
        }

        [HttpPost]
        public ActionResult MoveData(int id, int target, string position, int parentid)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Department.NotPermissionMove"));
                return Json(new { error = _resourceService.GetResource("Customer.Department.NotPermissionMove") });
            }

            if (Request.IsAjaxRequest())
            {
                _departmentService.MoveDepartment(id, target, position, parentid);
                return Json(new { success = "error" });
            }
            return Json(new { error = "error" });
        }

        [HttpPost]
        public ActionResult DeleteTree(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Department.NotPermissionDelete"));
                return Json(new { error = _resourceService.GetResource("Customer.Department.NotPermissionDelete") });
            }

            var department = _departmentService.Get(id);
            if (department == null)
            {
                return Json(new { error = "error" });
            }
            try
            {
                _departmentService.Delete(department);
            }
            catch (EgovException ex)
            {
                LogException(ex);
                return Json(new { message = ex.Message });
            }
            CreateActivityLog(ActivityLogType.Admin, "Xóa cây thành công");
            return Json(new { message = "delete_success" });
        }

        [HttpPost]
        public JsonResult AddUserDeptPosJob(List<int> userIds, int deptId, int posId, int jobId)
        {
            if (userIds == null || !userIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, "Danh sách người dùng không được trống");
                return Json(new { result = false, message = "Danh sách người dùng không được trống" });
            }

            if (deptId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, "Chưa chọn phòng ban");
                return Json(new { result = false, message = "Chưa chọn phòng ban" });
            }

            if (posId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, "Bạn chưa chọn chức vụ");
                return Json(new { result = false, message = "Bạn chưa chọn chức vụ" });
            }

            if (jobId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, "Bạn chưa chọn chức danh");
                return Json(new { result = false, message = "Bạn chưa chọn chức danh" });
            }

            var allUser = _userService.GetAllCached();
            var inUsers = allUser.Where(p => userIds.Contains(p.UserId));
            if (inUsers.Count() != userIds.Count)
            {
                CreateActivityLog(ActivityLogType.Admin, "Có người dùng không tồn tại");
                return Json(new { result = false, message = "Có người dùng không tồn tại" });
            }

            // var allDepts = _departmentService.GetCacheAllDepartments();
            var inDept = _departmentService.Get(deptId); // allDepts.First(p => p.DepartmentId == deptId);
            if (inDept == null)
            {
                CreateActivityLog(ActivityLogType.Admin, "Phòng ban không tồn tại");
                return Json(new { result = false, message = "Phòng ban không tồn tại" });
            }

            var inJob = _jobTitlesService.Get(jobId);
            if (inJob == null)
            {
                CreateActivityLog(ActivityLogType.Admin, "Chức danh không tồn tại");
                return Json(new { result = false, message = "Chức danh không tồn tại" });
            }

            var inPos = _positionService.Get(posId);
            if (inPos == null)
            {
                CreateActivityLog(ActivityLogType.Admin, "Chức vụ không tồn tại");
                return Json(new { result = false, message = "Chức vụ không tồn tại" });
            }
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    _departmentService.Update(inDept, inUsers, inPos, inJob);
                    trans.Complete();
                }
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, "Có lỗi trong quá trình thêm");
                return Json(new { result = false, message = "Có lỗi trong quá trình thêm" });
            }
            CreateActivityLog(ActivityLogType.Admin, "Thêm mới thành công");
            return Json(new { result = true, message = "Thêm mới thành công" });
        }

        [HttpPost]
        public JsonResult AddUserDeptJob(List<int> userIds, int deptId, int jobId)
        {
            if (userIds == null || !userIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, "Danh sách người dùng không được trống");
                return Json(new { result = false, message = "Danh sách người dùng không được trống" });
            }

            if (deptId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, "Chưa chọn phòng ban");
                return Json(new { result = false, message = "Chưa chọn phòng ban" });
            }

            if (jobId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, "Bạn chưa chọn chức danh");
                return Json(new { result = false, message = "Bạn chưa chọn chức danh" });
            }

            var allUser = _userService.GetAllCached();
            var inUsers = allUser.Where(p => userIds.Contains(p.UserId));
            if (inUsers.Count() != userIds.Count)
            {
                CreateActivityLog(ActivityLogType.Admin, "Có người dùng không tồn tại");
                return Json(new { result = false, message = "Có người dùng không tồn tại" });
            }


            // var allDepts = _departmentService.GetCacheAllDepartments();
            var inDept = _departmentService.Get(deptId); // allDepts.First(p => p.DepartmentId == deptId);
            if (inDept == null)
            {
                CreateActivityLog(ActivityLogType.Admin, "Phòng ban không tồn tại");
                return Json(new { result = false, message = "Phòng ban không tồn tại" });
            }

            var inJob = _jobTitlesService.Get(jobId);
            if (inJob == null)
            {
                CreateActivityLog(ActivityLogType.Admin, "Chức danh không tồn tại");
                return Json(new { result = false, message = "Chức danh không tồn tại" });
            }

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    _departmentService.Update(inDept, inUsers, inJob);
                    trans.Complete();
                }
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, "Có lỗi trong quá trình thêm");
                return Json(new { result = false, message = "Có lỗi trong quá trình thêm" });
            }

            CreateActivityLog(ActivityLogType.Admin, "Thêm mới thành công");
            return Json(new { result = true, message = "Thêm mới thành công" });
        }

        [HttpPost]
        public JsonResult AddUserDeptPos(List<int> userIds, int deptId, int posId)
        {
            if (userIds == null || !userIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, "Danh sách người dùng không được trống");
                return Json(new { result = false, message = "Danh sách người dùng không được trống" });
            }

            if (deptId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, "Chưa chọn phòng ban");
                return Json(new { result = false, message = "Chưa chọn phòng ban" });
            }

            if (posId <= 0)
            {
                CreateActivityLog(ActivityLogType.Admin, "Bạn chưa chọn chức vụ");
                return Json(new { result = false, message = "Bạn chưa chọn chức vụ" });
            }

            var allUser = _userService.GetAllCached();
            var inUsers = allUser.Where(p => userIds.Contains(p.UserId));
            if (inUsers.Count() != userIds.Count)
            {
                CreateActivityLog(ActivityLogType.Admin, "Có người dùng không tồn tại");
                return Json(new { result = false, message = "Có người dùng không tồn tại" });
            }


            // var allDepts = _departmentService.GetCacheAllDepartments();
            var inDept = _departmentService.Get(deptId); // allDepts.First(p => p.DepartmentId == deptId);
            if (inDept == null)
            {
                CreateActivityLog(ActivityLogType.Admin, "Phòng ban không tồn tại");
                return Json(new { result = false, message = "Phòng ban không tồn tại" });
            }

            var inPos = _positionService.Get(posId);
            if (inPos == null)
            {
                CreateActivityLog(ActivityLogType.Admin, "Chức vụ không tồn tại");
                return Json(new { result = false, message = "Chức vụ không tồn tại" });
            }
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    _departmentService.Update(inDept, inUsers, inPos);
                    trans.Complete();
                }
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, "Có lỗi trong quá trình thêm");
                return Json(new { result = false, message = "Có lỗi trong quá trình thêm" });
            }

            CreateActivityLog(ActivityLogType.Admin, "Thêm mới thành công");
            return Json(new { result = true, message = "Thêm mới thành công" });
        }

        #region private method

        private string GetAllUsers(bool isAdminDepartmentUser)
        {
            return isAdminDepartmentUser
                    ? _userService.GetsUserAccess(u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                    fullname = u.FullName,
                                    username = u.Username,
                                    firstjobtitlesid = 0,
                                    firstpositionid = 0
                                }, User.GetUserId()).StringifyJs()
                    : _userService.GetsAs(u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                    fullname = u.FullName,
                                    username = u.Username,
                                    firstjobtitlesid = 0,
                                    firstpositionid = 0
                                }, true).OrderBy(u => u.username).StringifyJs();
        }

        private string GetAllDepartment(bool isAdminDepartmentUser)
        {
            var alldepartments = isAdminDepartmentUser ? _departmentService.GetAllDepartmentUserAccess(User.GetUserId()) : _departmentService.GetCacheAllDepartments();
            return alldepartments.OrderBy(t => t.Order).Select(u => new
            {
                value = u.DepartmentId,
                label = u.DepartmentName,
                parentid = u.ParentId.HasValue ? u.ParentId : 0,
                data = u.DepartmentName,
                metadata = new { id = u.DepartmentId },
                state = "leaf",
                extvalue = u.DepartmentIdExt,
                path = u.DepartmentPath,
                order = u.Order,
                level = u.Level,
                attr = new { id = u.DepartmentId, rel = u.IsActivated ? "dept" : "dept-deactivated" }
            }).StringifyJs();
        }

        private string GetAllJobTitless()
        {
            var allJobTitless = _jobTitlesService.GetCacheAllJobtitles();
            return allJobTitless.Select(u => new { value = u.JobTitlesId, label = u.JobTitlesName }).StringifyJs();
        }

        private string GetAllPositions()
        {
            var allPositions = _positionService.GetCacheAllPosition();
            return allPositions.Select(u => new { value = u.PositionId, label = u.PositionName }).StringifyJs();
        }

        #endregion

    }
}