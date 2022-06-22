using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReportValidator : AbstractValidator<ReportModel>
    {
        private readonly ResourceBll _resourceService;
        public ReportValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Report.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 300)
                .WithMessage(_resourceService.GetResource("Report.CreateOrEdit.Fields.Name.MaxLength"));
            //RuleFor(x => x.QueryReport)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Report.CreateOrEdit.Fields.Query.Required"));
        }
    }
}