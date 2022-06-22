using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class Ad_LocalityValidator : AbstractValidator<Ad_LocalityModel>
    {
        private readonly ResourceBll _resourceService;

        public Ad_LocalityValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.LocalityName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.MaxLength"));   
        }
    }
}