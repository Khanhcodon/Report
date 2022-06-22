using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using Bkav.eGovCloud.Models;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class OnlineRegistrationSettingsValidator : AbstractValidator<OnlineRegistrationSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public OnlineRegistrationSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Name)
              .NotEmpty()
              .WithMessage(_resourceService.GetResource("Setting.OnlineRegistration.Fields.Name.Required"))
              .When(x => x.Active);

            RuleFor(x => x.ApiUrl)
           .NotEmpty()
           .WithMessage(_resourceService.GetResource("Setting.OnlineRegistration.Fields.ApiUrl.Required"))
           .When(x => x.Active);
        }
    }
}