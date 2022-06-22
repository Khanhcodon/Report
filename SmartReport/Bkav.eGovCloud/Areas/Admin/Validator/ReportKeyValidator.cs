using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReportKeyValidator : AbstractValidator<ReportKeyModel>
    {
        public ReportKeyValidator()
        {

            var resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Report.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 300)
                .WithMessage(resourceService.GetResource("Report.CreateOrEdit.Fields.Name.MaxLength"));
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Admin.TemplateKey.CreateOrEdit.Fields.Code.Required"))
                .Matches("{[\\w]+}")
                .WithMessage(resourceService.GetResource("Admin.TemplateKey.CreateOrEdit.Fields.Code.Matches"));
            //RuleFor(x => x.QueryReport)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Report.CreateOrEdit.Fields.Query.Required"));
        }
    }
}