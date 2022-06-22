using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class UnitValidator : AbstractValidator<Ad_UnitModel>
    {
        private readonly ResourceBll _resourceService;

        public UnitValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Unit)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.MaxLength"));
        }
    }
}