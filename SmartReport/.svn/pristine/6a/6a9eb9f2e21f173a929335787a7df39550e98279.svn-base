using System;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    public class NotifyConfigController : CustomController
    {
        private readonly NotifyConfigBll _notifyConfigService;
        private readonly TemplateBll _templateService;
        private readonly ResourceBll _resourceService;

        public NotifyConfigController(
            NotifyConfigBll notifyConfigService,
            ResourceBll resourceService,
            TemplateBll templateService)
            : base()
        {
            _templateService = templateService;
            _resourceService = resourceService;
            _notifyConfigService = notifyConfigService;
        }

        public ActionResult Index()
        {
            var cfgs = Enum.GetValues(typeof(NotifyConfigType)).Cast<NotifyConfigType>().ToList();
            var allNotifyConfigs = _notifyConfigService.Gets();
            var model = allNotifyConfigs.ToListModel().ToList();

            foreach (var cfg in cfgs)
            {
                var exist = model.Exists(p => p.Key.Equals(cfg.ToString()));
                if (!exist)
                {
                    var tmp = new NotifyConfigModel
                    {
                        Key = cfg.ToString(),
                        Description = _resourceService.GetEnumDescription<NotifyConfigType>(cfg)
                    };
                    model.Add(tmp);
                }
            }
            return View(model);
        }

        public ActionResult Edit(string key)
        {
            var model = new NotifyConfigModel();
            var notify = _notifyConfigService.Get(p => p.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (notify == null)
            {
                var description = string.Empty;

                var keyEnum = (NotifyConfigType)Enum.Parse(typeof(NotifyConfigType), key, true);
                var options = Enum.GetValues(typeof(NotifyConfigType)).Cast<NotifyConfigType>().ToList();
                foreach (var opt in options)
                {
                    if (opt == keyEnum)
                    {
                        description = _resourceService.GetEnumDescription<NotifyConfigType>(opt);
                        break;
                    }
                }

                model.Key = key;
                model.Description = description;
            }
            else
            {
                model = notify.ToModel();
            }

            BindTemplates();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NotifyConfigModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notify = _notifyConfigService.Get(p => p.Key.Equals(model.Key, StringComparison.OrdinalIgnoreCase));
                    if (notify == null)
                    {
                        _notifyConfigService.Create(model.ToEntity());
                    }
                    else
                    {
                        notify = model.ToEntity(notify);
                        _notifyConfigService.Update(notify);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.NotifyConfig.Edit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.NotifyConfig.Edit.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, "Common.NotifyConfig.Edit.Error");
                    ErrorNotification("Common.NotifyConfig.Edit.Error");
                }
            }
            BindTemplates();
            return View(model);
        }

        private void BindTemplates()
        {
            ViewBag.Templates = _templateService.GetsAs(p => new
           {
               value = p.TemplateId,
               label = p.Name,
               type = p.Type,
           }, p => p.IsActive
               && (p.Type == (int)TemplateType.Email
                    || p.Type == (int)TemplateType.Sms)).Stringify();
        }
    }
}