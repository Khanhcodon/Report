using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class StatisticsController : CustomController
    {
        private readonly StatisticsBll _statisticsService;
        private readonly ReportGroupBll _reportGroupService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;

        public StatisticsController(
            ResourceBll resourceService,
            StatisticsBll statisticsService,
            DepartmentBll departmentService,
            PositionBll positionService,
            UserBll userService,
            ReportGroupBll reportGroupService)
            : base()
        {
            _resourceService = resourceService;
            _statisticsService = statisticsService;
            _departmentService = departmentService;
            _positionService = positionService;
            _userService = userService;
            _reportGroupService = reportGroupService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Admin.Statistics.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            ViewBag.AllStatistics = _statisticsService.GetsAs(r => new
            {
                id = r.StatisticsId,
                name = r.Name,
                parentid = r.ParentId,
                isActivated = r.IsActive
            }).StringifyJs();

            ViewBag.AllDepartments = GetAllDepartments();
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.AllPositions = _positionService.GetCacheAllPosition().StringifyJs();
            return View();
        }

        #region Report

        public ActionResult Create(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Admin.Statistics.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            BindData();
            return PartialView("_CreateOrEdit", new StatisticsModel() { ParentId = id, StatisticsId = 0, IsActive = true });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportCreate")]
        public JsonResult Create(StatisticsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Admin.Statistics.NotPermission"));
            //    //  return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                SetPermission(model);
                var entity = model.ToEntity();
                _statisticsService.Create(entity);

                return
                    Json(
                        new
                        {
                            functionType = "Create",
                            item =
                            new
                            {
                                id = entity.StatisticsId,
                                name = entity.Name,
                                parentid = entity.ParentId,
                                isActivated = entity.IsActive
                            }
                        });
            }

            BindData();
            return null;
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //   ErrorNotification(_resourceService.GetResource("Admin.Statistics.NotPermission"));
            //    //  return RedirectToAction("Index");
            //}

            var model = _statisticsService.Get(id);
            if (model == null)
            {
                return PartialView("_CreateOrEdit", new StatisticsModel() { ParentId = id, StatisticsId = 0 });
            }

            BindData();
            return PartialView("_CreateOrEdit", model.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportEdit")]
        public JsonResult Edit(StatisticsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    //  ErrorNotification("Bạn không có quyền thao tác với trang này!.");
            //     return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var statistics = _statisticsService.Get(model.StatisticsId);
                if (statistics == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Statistics.NotFound"));
                    return Json(new { error = true, message = _resourceService.GetResource("Admin.Statistics.NotFound") });
                }

                if (model.ParentId == 0)
                {
                    model.ParentId = null;
                }

                SetPermission(model);

                var oldName = statistics.Name;
                var oldParent = statistics.ParentId;

                statistics = model.ToEntity(statistics);
                statistics.ParentId = oldParent;
                _statisticsService.Update(statistics);

                return Json(new
                        {
                            functionType = "Update",
                            item =
                            new
                            {
                                id = statistics.StatisticsId,
                                name = statistics.Name,
                                oldname = oldName,
                                parentid = statistics.ParentId,
                                isActivated = statistics.IsActive
                            }
                        });
            }
            BindData();
            return null;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ReportDelete")]
        public JsonResult Delete(int id)
        {
            try
            {
                _statisticsService.Delete(id);
                return Json(new { success = true });
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Statistics.Action.Delete.NotAllow"));
                return Json(new { success = true, message = _resourceService.GetResource("Admin.Statistics.Action.Delete.NotAllow") });
            }
        }

        public JsonResult Copy(int targetId, int toParentId)
        {
            try
            {
                var newModel = _statisticsService.Copy(targetId, toParentId);
                return Json(new
                        {
                            success = true,
                            id = newModel.StatisticsId,
                            name = newModel.Name,
                            parentId = newModel.ParentId.HasValue ? newModel.ParentId.Value : 0,
                            isActivated = newModel.IsActive
                        });
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Statistics.Action.Copy.Error"));
                return Json(new { error = true, message = _resourceService.GetResource("Statistics.Action.Copy.Error") });
            }
        }

        #endregion

        #region Private Methods

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

        private void SetPermission(StatisticsModel model)
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

            if (model.ReportGroupIds != null && model.ReportGroupIds.Any())
            {
                model.ReportGroup = model.ReportGroupIds.StringifyJs();
            }
        }

        private void BindData()
        {
            ViewBag.ReportGroup = _reportGroupService.GetGroups(p => !p.IsReport).ToListModel();
        }

        #endregion
    }
}
