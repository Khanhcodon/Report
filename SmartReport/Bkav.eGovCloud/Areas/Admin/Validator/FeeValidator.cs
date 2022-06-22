using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class FeeValidator : PacketValidator<FeeModel>
    {
        private readonly ResourceBll _resourceService;

        public FeeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.FeeName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.FeeName.Required"));

            //RuleFor(x => x.FeeName)
            //   .Length(1, 1000).When(p => !p.HasCreatePacket || p.FeeId > 0)
            //   .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.FeeName.Length"))
            //   .Must(ValidCreatePacket)
            //   .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.FeeName.ValidCreatePacket"));

            RuleFor(x => x.FeeTypeId)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.FeeTypeId.Required"));

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.Price.Required"));

            RuleFor(x => x.DocTypeId)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.DocTypeId.Required"));
        }

        public override bool ValidCreatePacket(FeeModel model, dynamic value)
        {
            if (model.FeeId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 1000)
                {
                    return false;
                }
            }

            return true;
        }
    }
}