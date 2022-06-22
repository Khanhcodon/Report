using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReportModeValidator : AbstractValidator<ReportModeModel>
    {
        private readonly ResourceBll _resourceService;

        public ReportModeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ReportMode.CreateOrEdit.Fields.Code.Required"));
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ReportMode.CreateOrEdit.Fields.Name.Required"));
            RuleFor(x => x.IssueOrg)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ReportMode.CreateOrEdit.Fields.IssueOrg.Required"));
        }
    }
}