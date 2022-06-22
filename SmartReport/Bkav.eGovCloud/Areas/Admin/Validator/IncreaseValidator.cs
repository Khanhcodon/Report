using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class IncreaseValidator : PacketValidator<IncreaseModel>
    {
        public IncreaseValidator()
        {
            var resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Create.Fields.Name.Required"));

            //RuleFor(x => x.Name).Length(1, 255).When(p => p.IncreaseId > 0 || !p.HasCreatePacket)
            //   .WithMessage(resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Create.Fields.Name.Length"))
            //   .Must(ValidCreatePacket)
            //   .WithMessage(resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Create.Fields.Name.ValidCreatePacket"));

            RuleFor(x => x.BussinessDocFieldDocTypeGroupId)
               .NotNull()
               .WithMessage(resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Create.Fields.BussinessDocFieldDocTypeGroupId.Required"));

            //RuleFor(x => x.Value)
            //    .GreaterThanOrEqualTo(0)
            //    .WithMessage(resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Increase.Create.Fields.Value.GreaterThan"));
        }

        public override bool ValidCreatePacket(IncreaseModel model, dynamic value)
        {
            if (model.IncreaseId > 0 || !model.HasCreatePacket)
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