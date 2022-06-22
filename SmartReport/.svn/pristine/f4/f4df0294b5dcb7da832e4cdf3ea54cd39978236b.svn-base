using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class FileLocationSettingsValidator : AbstractValidator<FileLocationSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public FileLocationSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Threshold)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.FileLocation.Fields.Threshold.Required"))
                .InclusiveBetween(1, int.MaxValue)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileLocation.Fields.Threshold.InclusiveBetween"), 0, int.MaxValue));
        }
    }
}