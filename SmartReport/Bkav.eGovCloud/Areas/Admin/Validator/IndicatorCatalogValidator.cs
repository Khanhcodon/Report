using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class IndicatorCatalogValidator : AbstractValidator<IndicatorCatalogModel>
    {
        private readonly ResourceBll _resourceService;

        public IndicatorCatalogValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.IndicatorCatalogName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.MaxLength"));
            RuleFor(x => x.IndicatorCatalogCode)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogCode.Required"))
                .Length(0, 300)
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogCode.MaxLength"));
        }
    }
}