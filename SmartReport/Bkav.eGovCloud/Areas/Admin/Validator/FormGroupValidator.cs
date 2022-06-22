using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class FormGroupValidator : PacketValidator<FormGroupModel>
    {
        public FormGroupValidator()
        {
            var _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.FormGroupName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Create.Fields.FormGroupName.Required"));

            //RuleFor(x => x.FormGroupName).Length(1, 255).When(p => !p.HasCreatePacket || p.FormGroupId > 0)
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Create.Fields.FormGroupName.Length"))
            //    .Must(ValidCreatePacket)
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.FormGroupName.ValidCreatePacket"));
        }

        public override bool ValidCreatePacket(FormGroupModel model, dynamic value)
        {
            if (model.FormGroupId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 255)
                {
                    return false;
                }
            }

            return true;
        }
    }
}