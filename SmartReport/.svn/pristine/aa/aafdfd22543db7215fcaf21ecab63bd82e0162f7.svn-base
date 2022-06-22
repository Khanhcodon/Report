using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class PositionController : CustomController
    {
        private readonly PositionBll _positionService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "PriorityLevel";

        public PositionController(PositionBll positionService,
            ResourceBll resourceService)
            : base()
        {
            _positionService = positionService;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = _positionService.Gets(DEFAULT_SORT_BY).ToListModel();
            var sortAndPage = new SortAndPagingModel
            {
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.AllPositions = model.Select(p => new
            {
                value = p.PositionId,
                label = p.PositionName
            }).Stringify();
            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            return View(new PositionModel { PriorityLevel = 1 });
        }

        [HttpPost]
        public ActionResult Create(PositionModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var maxPriorityLevel = _positionService.GetMaxPriorityLevel() + 1;
                    var entity = model.ToEntity();

                    if (model.HasCreatePacket)
                    {
                        var names = model.PositionName.Split(';').Distinct();
                        var list = new List<Position>();

                        foreach (var name in names)
                        {
                            var item = entity.Clone();
                            item.PositionName = name;
                            item.PriorityLevel = maxPriorityLevel;
                            list.Add(item);
                            maxPriorityLevel++;
                        }
                        _positionService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        entity.PriorityLevel = maxPriorityLevel;
                        _positionService.Create(entity);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.Created"));
                    SuccessNotification(_resourceService.GetResource("Customer.Position.Created"));
                    return RedirectToAction("Create");

                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var position = _positionService.Get(id);
            if (position == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotExist"));
                return RedirectToAction("Index");
            }

            var model = position.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PositionModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var position = _positionService.Get(model.PositionId);
                    if (position == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Customer.Position.NotExist"));
                        return RedirectToAction("Index");
                    }

                    var oldPositionName = position.PositionName;
                    position.PositionName = model.PositionName;
                    position.IsApproved = model.IsApproved;
                    _positionService.Update(position, oldPositionName);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.Updated"));
                    SuccessNotification(_resourceService.GetResource("Customer.Position.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var position = _positionService.Get(id);
            if (position == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.Position.NotExist"));
                return RedirectToAction("Index");
            }

            _positionService.Delete(position);
            SuccessNotification(_resourceService.GetResource("Customer.Position.Deleted"));
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(string sortBy, bool isSortDesc)
        {
            IEnumerable<PositionModel> model = null;
            if (Request.IsAjaxRequest())
            {
                model = _positionService.Gets(sortBy, isSortDesc).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy
                };

                ViewBag.SortAndPage = sortAndPage;
            }
            return PartialView("_PartialList", model);
        }

        public JsonResult UpdatePriority(string positionIds)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.NotPermissionUpdatePriority"));
                return Json(new { error = _resourceService.GetResource("Customer.Position.NotPermissionUpdatePriority") }, JsonRequestBehavior.AllowGet);
            }

            if (Request.IsAjaxRequest())
            {
                try
                {
                    var positionLevel = Json2.ParseAs<Dictionary<int, int>>(positionIds);
                    if (positionLevel.Any())
                    {
                        _positionService.Update(positionLevel);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated"));
                    return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated") }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Update.Error"));
                    return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Update.Error") }, JsonRequestBehavior.AllowGet);
                }
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated"));
            return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated") }, JsonRequestBehavior.AllowGet);
        }
    }
}
