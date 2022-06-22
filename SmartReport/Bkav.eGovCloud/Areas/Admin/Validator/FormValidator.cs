using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class FormValidator : PacketValidator<FormModel>
    {
        private readonly ResourceBll _resourceService;
        public FormValidator()
        {
            //_resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            // 20200220 VuHQ hiện tại lấy DocTypeName và Title để set vào FormName và Description nên không cần xử lý validate này nữa
            //RuleFor(x => x.FormName)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Form.CreateOrEdit.Fields.FormName.Required"));

            //RuleFor(x => x.FormName)
            //  .Length(1, 128).When(p => !p.HasCreatePacket || p.FormId != Guid.Empty)
            //  .WithMessage(_resourceService.GetResource("Form.CreateOrEdit.Fields.FormName.Length"))
            //  .Must(ValidCreatePacket)
            //  .WithMessage(_resourceService.GetResource("Form.CreateOrEdit.Fields.FormName.ValidCreatePacket"));
        }

        public override bool ValidCreatePacket(FormModel model, dynamic value)
        {
            if (model.FormId != Guid.Empty || !model.HasCreatePacket)
            {
                return true;
            }

            var results = ((string)value).Split(';');
            if (results.Length <= 0)
            {
                return false;
            }

            foreach (var item in results)
            {
                if (string.IsNullOrWhiteSpace(item) || item.Length > 128)
                {
                    return false;
                }
            }

            return true;
        }
    }
}