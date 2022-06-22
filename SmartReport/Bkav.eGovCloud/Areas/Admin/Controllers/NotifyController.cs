using System;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovOnline.Business.Customer;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    public class NotifyController : CustomController
    {
        private readonly SettingBll _settingService;
        private readonly NotifyBll _notifyService;
        private readonly TemplateBll _templateService;
        private readonly ResourceBll _resourceService;
        private SmsSettings _smsSettings;
        private const string DEFAULT_SORT_BY = "Name";

        public NotifyController(
            SettingBll settingService,
            NotifyBll notifyService,
            ResourceBll resourceService,
            SmsSettings smsSettings,
            TemplateBll templateService
            )
            : base()
        {
            _settingService = settingService;
            _smsSettings = smsSettings;
            _templateService = templateService;
            _resourceService = resourceService;
            _notifyService = notifyService;
        }

        public ActionResult Index()
        {
            var options = Enum.GetValues(typeof(Option)).Cast<Option>().ToList();
            var notifies = _notifyService.Gets();
            var model = notifies.ToListModel().ToList();
            foreach (var option in options)
            {
                var flag = false;
                foreach (var notify in notifies)
                {
                    if (notify.Option == option.ToString())
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    model.Add(new NotifyModel(option.ToString(), null, _resourceService.GetEnumDescription<Option>(option)));
                }
            }
            return View(model);
        }

        public ActionResult Edit(string option)
        {
            var options = Enum.GetValues(typeof(Option)).Cast<Option>().ToList();
            int totalRecords;
            ViewBag.Templates = _templateService.GetParents(out totalRecords, t => new TemplateModel
            {
                TemplateId = t.TemplateId,
                Name = t.Name,
                Type = t.Type,
                IsActive = t.IsActive
            }, pageSize: 100,
                sortBy: "Name", isDescending: false,
                keySearch: "", currentPage: 1);
            var model = new NotifyModel();
            var notify = _notifyService.Get(option);
            var description = string.Empty;
            if (notify == null)
            {
                foreach (var opt in options)
                {
                    if (opt.ToString() == option)
                    {
                        description = _resourceService.GetEnumDescription<Option>(opt);
                    }
                }
                model.Option = option;
                model.Description = description;
            }
            else
            {
                model = notify.ToModel();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NotifyModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notify = _notifyService.Get(model.Option);
                    if (notify == null)
                    {
                        if (model.TemplateId != 0)
                        {
                            _notifyService.Create(model.ToEntity());
                        }
                    }
                    else
                    {
                        if (model.TemplateId != 0)
                        {
                            notify.TemplateId = Convert.ToInt32(model.TemplateId);
                            _notifyService.Update(notify);
                        }
                        else
                        {
                            _notifyService.Delete(notify);
                        }
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Notify.Edit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.Notify.Edit.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, "Common.Notify.Edit.Error" + ex.Message);
                    ErrorNotification("Common.Notify.Edit.Error" + ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Sms()
        {
            ViewBag.Templates = _templateService.GetAllForSms().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.TemplateId.ToString()
            });
            var model = _smsSettings.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingSms")]
        public ActionResult Sms(SmsSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _smsSettings = model.ToEntity(_smsSettings);
                _settingService.SaveSetting(_smsSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Sms");
            }
            ViewBag.Templates = _templateService.GetAllForSms().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.TemplateId.ToString()
            });
            return View(model);
        }
    }
}