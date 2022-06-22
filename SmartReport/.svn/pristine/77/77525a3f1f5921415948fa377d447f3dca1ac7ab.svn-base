using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class dataTypeValidator : AbstractValidator<dataTypeModel>
    {
        private readonly ResourceBll _resourceService;

        public dataTypeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.dataTypeName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Catalog.CreateOrEdit.Fields.CatalogName.Required"))
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("Catalog.CreateOrEdit.Fields.CatalogName.MaxLength"));
        }
    }
}