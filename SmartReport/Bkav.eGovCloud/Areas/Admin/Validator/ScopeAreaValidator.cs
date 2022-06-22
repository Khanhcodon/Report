
using FluentValidation;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ScopeAreaValidator: AbstractValidator<ScopeAreaModel>
    {
    private readonly ResourceBll _resourceService;

    public ScopeAreaValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Key)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Customer.ScopeArea.CreateOrEdit.Fields.Key.Required"))
                .Length(1,300)
                .WithMessage(_resourceService.GetResource("Customer.ScopeArea.CreateOrEdit.Fields.Key.Length"));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Customer.ScopeArea.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 300)
                .WithMessage(_resourceService.GetResource("Customer.ScopeArea.CreateOrEdit.Fields.Name.Length"));
        }
    }
}