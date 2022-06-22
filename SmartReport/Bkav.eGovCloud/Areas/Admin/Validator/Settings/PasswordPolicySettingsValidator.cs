using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class PasswordPolicySettingsValidator : AbstractValidator<PasswordPolicySettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public PasswordPolicySettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.MinimumLength)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MinimumLength.Required"))
                .InclusiveBetween(0, 100)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MinimumLength.InclusiveBetween"), 0, 100));

            RuleFor(x => x.MinimumLowerCase)
                .InclusiveBetween(-1, 20)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MinimumLowerCase.InclusiveBetween"), 0, 20))
                .When(x => x.EnableSyntaxChecking);

            RuleFor(x => x.MinimumUpperCase)
                .InclusiveBetween(-1, 20)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MinimumUpperCase.InclusiveBetween"), 0, 20))
                .When(x => x.EnableSyntaxChecking);

            RuleFor(x => x.MinimumNumbers)
                .InclusiveBetween(-1, 20)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MinimumNumbers.InclusiveBetween"), 0, 20))
                .When(x => x.EnableSyntaxChecking);

            RuleFor(x => x.MinimumSymbols)
                .InclusiveBetween(-1, 20)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MinimumSymbols.InclusiveBetween"), 0, 20))
                .When(x => x.EnableSyntaxChecking);

            RuleFor(x => x.MaximumAge)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MaximumAge.Required"))
                .InclusiveBetween(0, 7300)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MaximumAge.InclusiveBetween"), 0, 7300))
                .When(x => x.EnableExpiration);

            RuleFor(x => x.WarningTime)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.PasswordPolicy.Fields.WarningTime.Required"))
                .InclusiveBetween(0, 30)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.WarningTime.InclusiveBetween"), 0, 30))
                .When(x => x.EnableExpiration);

            RuleFor(x => x.MaximumLogonFailure)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MaximumLogonFailure.Required"))
                .InclusiveBetween(0, 20)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.MaximumLogonFailure.InclusiveBetween"), 0, 20))
                .When(x => x.EnableLockout);

            RuleFor(x => x.LockoutDuration)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.PasswordPolicy.Fields.LockoutDuration.Required"))
                .InclusiveBetween(0, 43200)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.LockoutDuration.InclusiveBetween"), 0, 43200))
                .When(x => x.EnableLockout);

            RuleFor(x => x.HistoryCount)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.PasswordPolicy.Fields.HistoryCount.Required"))
                .InclusiveBetween(0, 100)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.PasswordPolicy.Fields.HistoryCount.InclusiveBetween"), 0, 100))
                .When(x => x.EnableHistory);
        }
    }
}