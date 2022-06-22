using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ResourceValidator : AbstractValidator<ResourceModel>
    {
        private readonly ResourceBll _resourceService;

        public ResourceValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.ResourceKey)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Common.Resource.CreateOrEdit.Fields.ResourceKey.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Common.Resource.CreateOrEdit.Fields.ResourceKey.MaxLength"));

            RuleFor(x => x.ResourceValue)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Common.Resource.CreateOrEdit.Fields.ResourceValue.Required"));
        }
    }
}