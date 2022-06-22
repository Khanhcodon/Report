using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReportQueryGroupValidator : AbstractValidator<ReportQueryGroupModel>
    {
        private readonly ResourceBll _resourceService;

        public ReportQueryGroupValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.ReportQueryGroupName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ReportQuery.CreateOrEdit.Fields.ReportQueryName.Required"))
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("ReportQuery.CreateOrEdit.Fields.ReportQueryName.MaxLength"));
        }
    }
}