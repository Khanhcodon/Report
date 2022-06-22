using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using FluentValidation;

namespace Bkav.eGovCloud.Validator
{
    public class SignatureValidator : AbstractValidator<SignatureModel>
    {
        private readonly ResourceBll _resourceService;

        public SignatureValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.SignatureName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Signature.Fields.SignatureName.Required"));

            RuleFor(x => x.SearchWord)
               .NotEmpty()
                .WithMessage(_resourceService.GetResource("Signature.Fields.SearchWord.Required"));
        }
    }
}