
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities.Customer;
using FluentValidation;
using System.Web.Mvc;
namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class TemplateKeyValidator : AbstractValidator<TemplateKeyModel>
    {
        private readonly ResourceBll _resourceService;

        public TemplateKeyValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.TemplateKey.CreateOrEdit.Fields.Name.Required"));
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.TemplateKey.CreateOrEdit.Fields.Code.Required"))
                .Matches("{[\\w]+}")
                .WithMessage(_resourceService.GetResource("Admin.TemplateKey.CreateOrEdit.Fields.Code.Matches"));
            RuleFor(x => x.Sql)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.TemplateKey.CreateOrEdit.Fields.Sql.Required"));
            //RuleFor(x => x.HtmlTemplate)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Admin.TemplateKey.CreateOrEdit.Fields.HtmlTemplate.Required"));
        }
    }
}