using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Business.Utils;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class TimeJobController : CustomController
    {
        private readonly TimeJobBll _timerJobService;
        private readonly ResourceBll _resourceService;
        private TimerJobHelper _timerJobHelper;

        public TimeJobController(TimeJobBll timerJobService, ResourceBll resourceService)
        {
            _timerJobService = timerJobService;
            _resourceService = resourceService;
            _timerJobHelper = DependencyResolver.Current.GetService<TimerJobHelper>();
        }

        #region Timer Job

        public ActionResult testMail()
        {
            _timerJobHelper.TestMail();
            return Redirect("index");
        }

        public ActionResult Index()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _timerJobService.GetsReadOnly().ToListModel();
            return View(model);
        }

        public ActionResult Create()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index");
            //}
            BindData();
            return View(new TimeJobModel()
            {
                IsActive = true,
                ScheduleTypeEnum = ScheduleType.HangPhut,
                ScheduleConfig = "{\"Type\": \"HangPhut\", \"Minutes\": 1}"
            });
        }

        [HttpPost]
        public ActionResult Create(TimeJobModel model)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var timerJob = model.ToEntity();
                    _timerJobService.Create(timerJob);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.Created"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.CreatedError"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.CreatedError"));
                    LogException(ex);
                }
            }
            BindData();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //    if (!HasPermission())
            //    {
            //        ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //        return RedirectToAction("Index", "Welcome");
            //    }

            var timerJob = _timerJobService.Get(id);
            if (timerJob == null)
            {
                return RedirectToAction("Index");
            }
            BindData();
            var model = timerJob.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TimeJobModel model)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _timerJobService.Get(model.TimeJobId);
                    if (entity != null)
                    {
                        entity = model.ToEntity(entity);
                        _timerJobService.Update(entity);
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.Update"));
                        SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.Update"));

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.UpdateError"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.UpdateError"));
                    LogException(ex);
                }
            }
            BindData();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var timer = _timerJobService.Get(id);
            if (timer != null)
            {
                try
                {
                    if (timer.IsRunning)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.TimeJobIsRunning"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.TimeJobIsRunning"));
                        return RedirectToAction("Index");
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.DeleteSuccess"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.DeleteSuccess"));
                    _timerJobService.Delete(timer);
                }
                catch (Exception ex)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.DeleteError"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Timejob.DeleteError"));
                    LogException(ex);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Rerun(int id)
        {
            var timer = _timerJobService.Get(id);
            ResetTimeJob(timer);

            return RedirectToAction("Index");
        }

        public ActionResult RerunAll()
        {
            var activeTimeJobs = _timerJobService.Gets(x => x.IsActive == true);
            foreach (var activeTimeJob in activeTimeJobs)
            {
                ResetTimeJob(activeTimeJob);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RunDVC()
        {
            _timerJobHelper.SendReportToDVCService();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Active(int id)
        {
            var timer = _timerJobService.Get(id);
            timer.IsActive ^= true;
            _timerJobService.Update(timer);
            return RedirectToAction("Index");
        }

        public void ResetTimeJob(TimeJob timeJob)
        {
            timeJob.DateLastJobRun = null;
            timeJob.IsRunning = false;
            _timerJobService.Update(timeJob);
        }

        private void BindData()
        {
            ViewBag.AllEgovJobEnum = GetListEgovJobEnum();
            ViewBag.AllScheduleType = GetListScheduleType();
        }

        private List<SelectListItem> GetListScheduleType()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(ScheduleType));
            foreach (var val in enumValArray)
            {
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<ScheduleType>((ScheduleType)val),
                    Value = val.ToString()
                });
            }
            return result;
        }

        private List<SelectListItem> GetListEgovJobEnum()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(EgovJobEnum));
            foreach (var val in enumValArray)
            {
                // var itemValue = Convert.ToInt32(((EgovJobEnum)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<EgovJobEnum>((EgovJobEnum)val),
                    Value = val.ToString()
                });
            }
            return result;
        }

        #endregion Timer Job
    }
}
