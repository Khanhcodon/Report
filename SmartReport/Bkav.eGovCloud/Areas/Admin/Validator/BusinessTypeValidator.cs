using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class BusinessTypeValidator : AbstractValidator<BusinessTypeModel>
    {
        private readonly ResourceBll _resourceService;

        public BusinessTypeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.BusinessTypeName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessType.CreateOrEdit.Fields.BusinessTypeName.Required"));
        }
    }
}