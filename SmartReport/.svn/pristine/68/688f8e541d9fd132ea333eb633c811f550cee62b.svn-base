using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class RequiredSupplementaryValidator : AbstractValidator<RequiredSupplementaryModel>
    {
        private readonly ResourceBll _resourceService;

        public RequiredSupplementaryValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.CreateOrEdit.Fields.CodeName.Required"));
        }
    }
}