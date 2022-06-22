using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class AuthenticationSettingsValidator : AbstractValidator<AuthenticationSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public AuthenticationSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.SingleSignOnDomain)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.SingleSignOnDomain.Required"));

            RuleFor(x => x.LdapServer)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapServer.Required"))
                .When(x => x.EnableLdap);

            RuleFor(x => x.LdapHost)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapHost.Required"))
                .When(x => x.EnableLdap);

            RuleFor(x => x.LdapPort)
                .Must(x =>
                        {
                            Int16 value;
                            return Int16.TryParse(x, out value);
                        })
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapPort.IsNumber"))
                .When(x => x.EnableLdap && !string.IsNullOrWhiteSpace(x.LdapPort));

            RuleFor(x => x.LdapUsername)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapUsername.Required"))
                .When(x => x.EnableLdap);

            RuleFor(x => x.LdapPassword)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapPassword.Required"))
                .When(x => x.EnableLdap);

            RuleFor(x => x.LdapAuthenticationFilter)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapAuthenticationFilter.Required"))
                .When(x => x.EnableLdap);

            RuleFor(x => x.LdapMappingFirstName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapMappingFirstName.Required"))
                .When(x => x.LdapEnableImport);

            RuleFor(x => x.LdapMappingLastName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapMappingLastName.Required"))
                .When(x => x.LdapEnableImport);

            RuleFor(x => x.LdapMappingUsername)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapMappingUsername.Required"))
                .When(x => x.LdapEnableImport);

            RuleFor(x => x.LdapMappingEmail)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Authentication.Fields.LdapMappingEmail.Required"))
                .When(x => x.LdapEnableImport);
        }
    }
}