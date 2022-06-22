using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class AddressValidator : AbstractValidator<AddressModel>
    {
        private readonly ResourceBll _resourceService;

        public AddressValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Name.Required"));

            RuleFor(x => x.Email)
                .Matches(ValidationExpression.EmailRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Email.Matches"));

            RuleFor(x => x.Phone)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Phone.Matches"));

            RuleFor(x => x.PhoneExt)
                .Matches(@"[0-9]{0,5}$")
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.PhoneExt.Matches"));

            RuleFor(x => x.Fax)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Fax.Matches"));

            RuleFor(x => x.EdocId)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.EdocId.Required"));
        }
    }
}