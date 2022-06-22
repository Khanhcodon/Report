using System;
using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class LawValidator : AbstractValidator<LawModel>
    {
        private readonly ResourceBll _resourceService;

        public LawValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.NumberSign)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.Authorize.CreateOrEdit.Fields.NumberSign.Required"))
                .Length(1, 300)
                .WithMessage(_resourceService.GetResource("Admin.Authorize.CreateOrEdit.Fields.NumberSign.Length"));
        }
    }
}