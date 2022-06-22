using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class JobTitlesValidator : PacketValidator<JobTitlesModel>
    {
        private readonly ResourceBll _resourceService;

        public JobTitlesValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.JobTitlesName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Customer.JobTitles.CreateOrEdit.Fields.JobTitlesName.Required"));

            //RuleFor(x => x.JobTitlesName).Length(1, 64).When(p => !p.HasCreatePacket || p.JobTitlesId > 0)
            //  .WithMessage(_resourceService.GetResource("Customer.JobTitles.CreateOrEdit.Fields.JobTitlesName.Length"))
            //  .Must(ValidCreatePacket)
            //  .WithMessage(_resourceService.GetResource("Customer.JobTitles.CreateOrEdit.Fields.JobTitlesName.ValidCreatePacket"));

            //RuleFor(x => x.PriorityLevel)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Customer.JobTitles.CreateOrEdit.Fields.PriorityLevel.Required"))
            //    .InclusiveBetween(1, 2000000)
            //    .WithMessage(_resourceService.GetResource("Customer.JobTitles.CreateOrEdit.Fields.PriorityLevel.LessThan"));
        }

        public override bool ValidCreatePacket(JobTitlesModel model, dynamic value)
        {
            if (model.JobTitlesId > 0 || !model.HasCreatePacket)
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