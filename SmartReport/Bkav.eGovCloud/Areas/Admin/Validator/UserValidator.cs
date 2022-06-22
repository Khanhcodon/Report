using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using Bkav.eGovCloud.Entities.Customer;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            var resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            var passwordPolicySettings = DependencyResolver.Current.GetService<PasswordPolicySettings>();
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.Username.Required"))
                .Length(1, 64)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.Username.Length"))
                .When(x => x.UserId <= 0);


            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.Password.Required"))
                .Matches(passwordPolicySettings.PasswordExpression)
                .WithMessage(GetMessageMatches(resourceService, passwordPolicySettings))
                .When(x => x.UserId <= 0);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.ConfirmPassword.NotEqualPassword"))
                .When(x => x.UserId <= 0);

            RuleFor(x => x.OpenId)
                .Length(0, 1024)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.OpenId.Length"));

            //RuleFor(x => x.FullName)
            //    .NotEmpty()
            //    .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.FullName.Required"))
            //    .Length(1, 128)
            //    .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.FullName.Length"));

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.FirstName.Required"))
                .Length(1, 64)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.FirstName.Length"));

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.LastName.Required"))
                .Length(1, 64)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.LastName.Length"));

            RuleFor(x => x.Address)
                .Length(0, 1000)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.Address.Length"));

            RuleFor(x => x.Phone)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.Phone.Matches"));

            RuleFor(x => x.Fax)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(resourceService.GetResource("User.CreateOrEdit.Fields.Fax.Matches"));
        }

        public static string GetMessageMatches(ResourceBll resourceService, PasswordPolicySettings passwordPolicySettings)
        {
            var messageMatches = resourceService.GetResource("User.CreateOrEdit.Fields.Password.Matches") + ": ";
            if (passwordPolicySettings.MinimumLength > 0)
            {
                messageMatches += 
                    string.Format(resourceService.GetResource("User.CreateOrEdit.Fields.Password.MinLength"), 
                    passwordPolicySettings.MinimumLength) + ", ";
            }
            if (passwordPolicySettings.MinimumLowerCase > 0)
            {
                messageMatches += 
                    string.Format(resourceService.GetResource("User.CreateOrEdit.Fields.Password.MinLowerCase"), 
                    passwordPolicySettings.MinimumLowerCase) + ", ";
            }
            if (passwordPolicySettings.MinimumUpperCase > 0)
            {
                messageMatches += 
                    string.Format(resourceService.GetResource("User.CreateOrEdit.Fields.Password.MinUpperCase"),
                    passwordPolicySettings.MinimumUpperCase) + ", ";
            }
            if (passwordPolicySettings.MinimumNumbers > 0)
            {
                messageMatches += 
                    string.Format(resourceService.GetResource("User.CreateOrEdit.Fields.Password.MinNumbers"), 
                    passwordPolicySettings.MinimumNumbers) + ", ";
            }
            if (passwordPolicySettings.MinimumSymbols > 0)
            {
                messageMatches += 
                    passwordPolicySettings.MinimumSymbols + 
                    resourceService.GetResource("User.CreateOrEdit.Fields.Password.MinSymbols") + ", ";
            }
            return messageMatches.Substring(0, messageMatches.Length - 2) + ".";
        }
    }
}