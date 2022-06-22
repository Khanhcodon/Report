using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Core.Validation;
using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Validator
{
    public class MailValidator : AbstractValidator<MailModel>
    {
        private readonly ResourceBll _resourceService;

        public MailValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Mail.Fields.Subject.Required"))
                .Length(1, 500)
                .WithMessage(_resourceService.GetResource("Mail.Fields.Subject.Length"));

            RuleFor(x => x.Body)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Mail.Fields.Body.Required"));

            RuleFor(x => x.SendTo)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Mail.Fields.SendTo.Required"))
                .Length(1, 500)
                .WithMessage(_resourceService.GetResource("Mail.Fields.SendTo.Length"))
                .Matches(ValidationExpression.MultiEmailRegex)
                .WithMessage(_resourceService.GetResource("Mail.Fields.SendTo.Matches"));
        }
    }
}