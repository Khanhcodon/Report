using System;
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
    public class JobTitlesController : CustomController
    {
        private readonly JobTitlesBll _jobTitlesService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "PriorityLevel";

        public JobTitlesController(
            JobTitlesBll jobTitlesService,
            ResourceBll resourceService)
            : base()
        {
            _jobTitlesService = jobTitlesService;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = _jobTitlesService.Gets(DEFAULT_SORT_BY).ToListModel();
            var sortAndPage = new SortAndPagingModel
            {
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.AllJobs = model.Select(p => new
                                                {
                                                    value = p.JobTitlesId,
                                                    label = p.JobTitlesName
                                                }).Stringify();
            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            return View(new JobTitlesModel { PriorityLevel = 1 });
        }

        [HttpPost]
        public ActionResult Create(JobTitlesModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    var maxPriorityLevel = _jobTitlesService.GetMaxPriorityLevel() + 1;

                    if (model.HasCreatePacket)
                    {
                        var names = model.JobTitlesName.Split(';').Distinct();
                        var list = new List<JobTitles>();
                        foreach (var name in names)
                        {
                            var item = entity.Clone();
                            item.JobTitlesName = name;
                            item.PriorityLevel = maxPriorityLevel;
                            list.Add(item);
                            maxPriorityLevel++;
                        }
                        _jobTitlesService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        entity.PriorityLevel = maxPriorityLevel;
                        _jobTitlesService.Create(entity);
                    }

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.Created"));
                    SuccessNotification(_resourceService.GetResource("Customer.JobTitles.Created"));
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var jobTitles = _jobTitlesService.Get(id);
            if (jobTitles == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotExist"));
                return RedirectToAction("Index");
            }

            var model = jobTitles.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(JobTitlesModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var jobTitles = _jobTitlesService.Get(model.JobTitlesId);
                    if (jobTitles == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotExist"));
                        return RedirectToAction("Index");
                    }

                    var oldJobTitlesName = jobTitles.JobTitlesName;
                    jobTitles = model.ToEntity(jobTitles);

                    _jobTitlesService.Update(jobTitles, oldJobTitlesName);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.Updated"));
                    SuccessNotification(_resourceService.GetResource("Customer.JobTitles.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult UpdatePriority(int[] ids)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotPermissionUpdatePriority"));
                return Json(new { error = _resourceService.GetResource("Customer.JobTitles.NotPermissionUpdatePriority") });
            }

            if (Request.IsAjaxRequest())
            {
                try
                {
                    _jobTitlesService.UpdatePriority(ids);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated"));
                    return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated") });
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Update.Error"));
                    return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Update.Error") });
                }
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated"));
            return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.JobTitle.Updated") });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var jobTitles = _jobTitlesService.Get(id);
            if (jobTitles == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.JobTitles.NotExist"));
                return RedirectToAction("Index");
            }

            _jobTitlesService.Delete(jobTitles);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.JobTitles.Deleted"));
            SuccessNotification(_resourceService.GetResource("Customer.JobTitles.Deleted"));
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(string sortBy, bool isSortDesc)
        {
            IEnumerable<JobTitlesModel> model = null;
            if (Request.IsAjaxRequest())
            {
                model = _jobTitlesService.Gets(sortBy, isSortDesc).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy
                };
                ViewBag.SortAndPage = sortAndPage;
            }

            return PartialView("_PartialList", model);
        }
    }
}