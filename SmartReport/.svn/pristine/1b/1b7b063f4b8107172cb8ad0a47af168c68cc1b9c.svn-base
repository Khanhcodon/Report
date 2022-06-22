using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;


namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ImageSettingsValidator : AbstractValidator<ImageSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public ImageSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.ZipImage)
                 .NotEmpty()
                 .WithMessage(string.Format(_resourceService.GetResource("Setting.Image.Fields.ZipImage.Required")))
                 .InclusiveBetween(1, 100)
                 .WithMessage(string.Format(_resourceService.GetResource("Setting.Image.Fields.ZipImage.InclusiveBetween"), 0, 100));
        }
    }
}