using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class TreeGroupController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly TreeGroupBll _treeGroupService;
        private readonly ProcessFunctionBll _processFunctionService;
        private readonly UserBll _userService;

        public TreeGroupController(
            ResourceBll resourceService,
            TreeGroupBll treeGroupService,
            UserBll userService,
            ProcessFunctionBll processFunctionService)
            : base()
        {
            _resourceService = resourceService;
            _treeGroupService = treeGroupService;
            _userService = userService;
            _processFunctionService = processFunctionService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _treeGroupService.Gets(sortBy: "Order").ToListModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            return View(new TreeGroupModel());
        }

        [HttpPost]
        public ActionResult Create(TreeGroupModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    model.UserNameCreated = _userService.CurrentUser.UsernameEmailDomain;
                    var order = _treeGroupService.Count() + 1;
                    var entity = model.ToEntity();
                    if (model.HasCreatePacket)
                    {
                        var results = model.TreeGroupName.Split(';').Distinct();
                        var list = new List<TreeGroup>();
                        foreach (var item in results)
                        {
                            var obj = entity.Clone();
                            obj.TreeGroupName = item;
                            obj.Order = order;
                            order++;
                            list.Add(obj);
                        }
                        _treeGroupService.Create(list);
                    }
                    else
                    {
                        entity.Order = order;
                        _treeGroupService.Create(model.ToEntity());
                    }
                    SuccessNotification(_resourceService.GetResource("Customer.TreeGroup.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.ErrorCreated"));
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            var entity = _treeGroupService.Get(id);
            if (entity == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotExist"));
                return RedirectToAction("Index");
            }

            var model = entity.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TreeGroupModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var entity = _treeGroupService.Get(model.TreeGroupId);
                if (entity == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    model.UserNameModified = _userService.CurrentUser.UsernameEmailDomain;
                    model.DateModified = DateTime.Now;
                    model.UserNameCreated = entity.UserNameCreated;
                    model.DateCreated = entity.DateCreated;

                    _treeGroupService.Update(model.ToEntity(entity));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.UpdateSuccessed"));
                    SuccessNotification(_resourceService.GetResource("Customer.TreeGroup.UpdateSuccessed"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.UpdateError"));
                    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.UpdateError"));
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            var entity = _treeGroupService.Get(id);
            if (entity == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                var functions = _processFunctionService.Gets(p => p.TreeGroupId == id);
                if (functions != null && functions.Any())
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.IsUsed"));
                    ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.IsUsed"));
                }
                else
                {
                    _treeGroupService.Delete(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.DeleteSuccessed"));
                    SuccessNotification(_resourceService.GetResource("Customer.TreeGroup.DeleteSuccessed"));
                }
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.DeleteError"));
                ErrorNotification(_resourceService.GetResource("Customer.TreeGroup.DeleteError"));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeActived(int id, bool isActived)
        {
            var model = _treeGroupService.Get(id);
            if (model == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.NotExist"));
                return Json(new
                {
                    error = _resourceService.GetResource("Customer.TreeGroup.NotExist")
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (model.IsActived != isActived)
                {
                    model.UserNameModified = _userService.CurrentUser.UsernameEmailDomain;
                    model.DateModified = DateTime.Now;
                    model.IsActived = isActived;
                    _treeGroupService.Update(model);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.UpdateSuccessed"));
                return Json(new
                {
                    success = _resourceService.GetResource("Customer.TreeGroup.UpdateSuccessed")
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.UpdateError"));
                return Json(new
                {
                    error = _resourceService.GetResource("Customer.TreeGroup.UpdateError")
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdatePriority(int[] treeGroupIds)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new
            //    {
            //        error = _resourceService.GetResource("Customer.TreeGroup.NotPermission")
            //    });
            //}

            try
            {
                if (treeGroupIds.Any())
                {
                    _treeGroupService.Update(treeGroupIds);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.UpdateSuccessed"));
                return Json(new
                {
                    success = _resourceService.GetResource("Customer.TreeGroup.UpdateSuccessed")
                });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.TreeGroup.UpdateError"));
                return Json(new
                {
                    error = _resourceService.GetResource("Customer.TreeGroup.UpdateError")
                });
            }
        }
    }
}
