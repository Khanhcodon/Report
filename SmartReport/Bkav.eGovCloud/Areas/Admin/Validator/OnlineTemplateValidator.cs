using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class OnlineTemplateValidator : AbstractValidator<OnlineTemplateModel>
    {
        private readonly ResourceBll _resourceService;

        public OnlineTemplateValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.OnlineTemplate.CreateOrEdit.Fields.Name.Required"))
                .Length(1,255)
                .WithMessage(
                    _resourceService.GetResource("Admin.OnlineTemplate.CreateOrEdit.Fields.Name.Length"));
        }
    }
}