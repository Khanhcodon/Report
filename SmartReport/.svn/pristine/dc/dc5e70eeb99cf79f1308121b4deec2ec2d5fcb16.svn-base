using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class PaperValidator : PacketValidator<PaperModel>
    {
        private readonly ResourceBll _resourceService;

        public PaperValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.PaperName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.PaperName.Required"));

            //RuleFor(x => x.PaperName)
            // .Length(1, 3000).When(p => !p.HasCreatePacket || p.PaperId > 0)
            // .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.PaperName.Length"))
            // .Must(ValidCreatePacket)
            //  .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.PaperName.ValidCreatePacket"));

            RuleFor(x => x.PaperTypeId)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.PaperTypeId.Required"));

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.Amount.Required"));

            RuleFor(x => x.DocTypeId)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.DocTypeId.Required"));
        }

        public override bool ValidCreatePacket(PaperModel model, dynamic value)
        {
            if (model.PaperId > 0 || !model.HasCreatePacket)
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