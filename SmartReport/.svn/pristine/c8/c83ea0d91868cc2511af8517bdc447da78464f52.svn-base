using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Core.Validation;

namespace Bkav.eGovCloud.Validator
{
    public class SmsValidator : AbstractValidator<SmsModel>
    {
        private readonly ResourceBll _resourceService;

        public SmsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage(_resourceService.GetResource("Sms.Fields.Message.Required"))
                .Length(1, 2000).WithMessage(_resourceService.GetResource("Sms.Fields.Message.Length"));

            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage(_resourceService.GetResource("Sms.Fields.PhoneNumber.Required"))
           .Matches(ValidationExpression.PhoneRegex).WithMessage(_resourceService.GetResource("Sms.Fields.PhoneNumber.Matches"));
        }
    }
}