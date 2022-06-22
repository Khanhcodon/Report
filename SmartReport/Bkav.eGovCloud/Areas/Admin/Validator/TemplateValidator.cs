using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class TemplateValidator : AbstractValidator<TemplateModel>
    {
        private readonly ResourceBll _resourceService;

        public TemplateValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.Template.CreateOrEdit.Fields.Name.Required"));

            RuleFor(x => x.TitleMail)
                   .NotEmpty()
                   .WithMessage(_resourceService.GetResource("Admin.Template.CreateOrEdit.Fields.TitleMail.Required"))
                   .Length(1, 500)
                   .WithMessage(_resourceService.GetResource("Admin.Template.CreateOrEdit.Fields.TitleMail.Length"))
                   .When(p => p.Type == 2);
        }
    }
}