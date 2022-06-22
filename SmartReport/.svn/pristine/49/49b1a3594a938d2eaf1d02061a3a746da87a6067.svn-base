using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class SurveyCatalogValidator : AbstractValidator<SurveyCatalogModel>
    {
        private readonly ResourceBll _resourceService;

        public SurveyCatalogValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.CatalogName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("SurveyCatalog.CreateOrEdit.Fields.SurveyCatalogName.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("SurveyCatalog.CreateOrEdit.Fields.SurveyCatalogName.MaxLength"));
            RuleFor(x => x.CatalogKey)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("SurveyCatalog.CreateOrEdit.Fields.SurveyCatalogCode.Required"))
                .Length(0, 300)
                .WithMessage(_resourceService.GetResource("SurveyCatalog.CreateOrEdit.Fields.SurveyCatalogCode.MaxLength"));
        }
    }
}