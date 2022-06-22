using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class DistrictValidator : AbstractValidator<DistrictModel>
    {
        private readonly ResourceBll _resourceService;

        public DistrictValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.DistrictName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.CreateOrEdit.Fields.DistrictName.Required"));

            RuleFor(x => x.DistrictCode)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.CreateOrEdit.Fields.DistrictCode.Required"));
        }
    }
}