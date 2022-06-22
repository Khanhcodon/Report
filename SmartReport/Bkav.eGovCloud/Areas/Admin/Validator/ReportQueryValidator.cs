using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReportQueryValidator : AbstractValidator<ReportQueryModel>
    {
        private readonly ResourceBll _resourceService;

        public ReportQueryValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.ReportQueryName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ReportQuery.CreateOrEdit.Fields.ReportQueryName.Required"))
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("ReportQuery.CreateOrEdit.Fields.ReportQueryName.MaxLength"));
        }
    }
}