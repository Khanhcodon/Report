using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class StatisticsValidator : AbstractValidator<StatisticsModel>
    {
        private readonly ResourceBll _resourceService;

        public StatisticsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Statistics.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Statistics.CreateOrEdit.Fields.Name.MaxLength"));

            RuleFor(x => x.Query)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Statistics.CreateOrEdit.Fields.Query.Required"));
        }
    }
}