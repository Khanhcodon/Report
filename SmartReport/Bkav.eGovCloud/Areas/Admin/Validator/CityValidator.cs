using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        private readonly ResourceBll _resourceService;

        public CityValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.CityName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.CreateOrEdit.Fields.CityName.Required"));
            RuleFor(x => x.CityCode)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.CreateOrEdit.Fields.CityCode.Required"));
        }
    }
}