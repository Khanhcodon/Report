using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class EgovJobController : CustomController
    {
        private readonly EgovJobBll _jobService;
        private readonly ResourceBll _resourceService;

        public EgovJobController(
            ResourceBll resourceService,
            EgovJobBll jobService)
            : base()
        {
            _resourceService = resourceService;
            _jobService = jobService;
        }

        public ActionResult Index()
        {
            _jobService.SyncJob();
            var model = _jobService.Gets().ToListModel();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!HasPermission("EditEgovJob"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionEgovJob"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionEgovJob"));
                return RedirectToAction("Index", "Welcome");
            }
            var timer = _jobService.Get(id);
            if (timer == null)
            {
                return RedirectToAction("Index");
            }
            var model = timer.ToModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "TimerEdit")]
        public ActionResult Edit(EgovJobModel model)
        {
            if (!HasPermission("EditEgovJob"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionEgovJob"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionEgovJob"));
                return RedirectToAction("Index", "Welcome");
            }
            if (ModelState.IsValid)
            {
                var job = _jobService.Get(model.Id);
                var oldInterval = job.Interval;
                if (job == null)
                {
                    return RedirectToAction("Index");
                }
                model.LastModified = DateTime.Now;
                if (model.IsActivated && model.Interval > 0)
                {
                    model.NextRun = DateTime.Now.AddSeconds(model.Interval);
                }
                else if (!model.IsActivated)
                {
                    model.NextRun = null;
                }
                try
                {
                    job = model.ToEntity(job);
                    _jobService.Update(job);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Resource.Updated"));
                SuccessNotification(_resourceService.GetResource("Common.Resource.Updated"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult SetActive(int id, bool isActivated)
        {
            if (!HasPermission("EditEgovJob"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionEgovJob"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionEgovJob"));
                return RedirectToAction("Index", "Welcome");
            }
            var timer = _jobService.Get(id);
            if (timer == null)
            {
                return RedirectToAction("Index");
            }
            timer.IsActivated = isActivated;
            try
            {
                _jobService.Update(timer);
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
                return RedirectToAction("Index");
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Resource.Updated"));
            SuccessNotification(_resourceService.GetResource("Common.Resource.Updated"));
            return RedirectToAction("Index");
        }

    }
}