using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using FluentValidation;

namespace Bkav.eGovCloud.Validator
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            var resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            var passwordPolicySettings = DependencyResolver.Current.GetService<PasswordPolicySettings>();
            var messageMatches = resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.Matches");
            if(passwordPolicySettings.MinimumLength > 0)
            {
                messageMatches += string.Format(resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.MinLength"), passwordPolicySettings.MinimumLength);
            }
            if(passwordPolicySettings.MinimumLowerCase > 0)
            {
                messageMatches += ", " + string.Format(resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.MinLowerCase"), passwordPolicySettings.MinimumLowerCase);
            }
            if (passwordPolicySettings.MinimumUpperCase > 0)
            {
                messageMatches += ", " + string.Format(resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.MinUpperCase"), passwordPolicySettings.MinimumUpperCase);
            }
            if (passwordPolicySettings.MinimumNumbers > 0)
            {
                messageMatches += ", " + string.Format(resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.MinNumbers"), passwordPolicySettings.MinimumNumbers);
            }
            if (passwordPolicySettings.MinimumSymbols > 0)
            {
                messageMatches += ", " + passwordPolicySettings.MinimumSymbols + " " + resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.MinSymbols");
            }

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Account.ChangePassword.Fields.CurrentPassword.Required"));

            if(passwordPolicySettings.EnableHistory)
            {
                RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.Required"))
                .NotEqual(x => x.CurrentPassword)
                .WithMessage(resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.EqualCurrentPassword"))
                .Matches(passwordPolicySettings.PasswordExpression)
                .WithMessage(messageMatches);
            }
            else
            {
                RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Account.ChangePassword.Fields.NewPassword.Required"))
                .Matches(passwordPolicySettings.PasswordExpression)
                .WithMessage(messageMatches);
            }

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword)
                .WithMessage(resourceService.GetResource("Account.ChangePassword.Fields.ConfirmPassword.NotEqualNewPassword"));
        }
    }
}