using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class EmailSettingsValidator : AbstractValidator<EmailSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public EmailSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.SmtpServer)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Email.Fields.SmtpServer.Required"));

            RuleFor(x => x.SmtpPort)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Email.Fields.SmtpPort.Required"))
                .InclusiveBetween(1, 65535)
                .WithMessage(_resourceService.GetResource("Setting.Email.Fields.SmtpPort.InclusiveBetween"));

            RuleFor(x => x.SmtpUsername)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Email.Fields.SmtpUsername.Required"))
                .EmailAddress()
                .WithMessage(_resourceService.GetResource("Setting.Email.Fields.SmtpUsername.EmailAddress"));

            RuleFor(x => x.SmtpPassword)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Email.Fields.SmtpPassword.Required"));
        }
    }
}