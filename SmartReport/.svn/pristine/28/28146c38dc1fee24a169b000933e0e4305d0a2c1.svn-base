using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class PositionValidator : PacketValidator<PositionModel>
    {
        private readonly ResourceBll _resourceService;

        public PositionValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.PositionName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Customer.Position.CreateOrEdit.Fields.PositionName.Required"));

            //RuleFor(x => x.PositionName).Length(1, 64).When(p => !p.HasCreatePacket || p.PositionId > 0)
            // .WithMessage(_resourceService.GetResource("Customer.Position.CreateOrEdit.Fields.PositionName.Length"))
            // .Must(ValidCreatePacket)
            // .WithMessage(_resourceService.GetResource("Customer.Position.CreateOrEdit.Fields.PositionName.ValidCreatePacket"));
            //RuleFor(x => x.PriorityLevel)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Customer.Position.CreateOrEdit.Fields.PriorityLevel.Required"))
            //    .InclusiveBetween(1,2000000)
            //    .WithMessage(_resourceService.GetResource("Customer.Position.CreateOrEdit.Fields.PriorityLevel.LessThan"));
        }

        public override bool ValidCreatePacket(PositionModel model, dynamic value)
        {
            if (model.PositionId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 64)
                {
                    return false;
                }
            }

            return true;
        }
    }
}