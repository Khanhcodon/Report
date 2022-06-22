using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class FileUploadSettingsValidator : AbstractValidator<FileUploadSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public FileUploadSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.FileUploadMaximumSizeMegaBytes)
                .NotEmpty()
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.FileUploadMaximumSizeBytes.Required")))
                .InclusiveBetween(1, 2147)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.FileUploadMaximumSizeBytes.InclusiveBetween"), 0, 2147));

            RuleFor(x => x.FileUploadAllowedExtensionsParsed)
                .Matches(@"^([\.]+\w+[\,]?){1,20}?$")
                .WithMessage("Chuỗi không hợp lệ. Đuôi file có dấu chấm và cách nhau bởi dấu phẩy, không quá 20 đuôi file.");

            RuleFor(x => x.ProfilePictureMaximumSizeKiloBytes)
                .NotEmpty()
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.ProfilePictureMaximumSizeBytes.Required")))
                .InclusiveBetween(1, 5120)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.ProfilePictureMaximumSizeBytes.InclusiveBetween"), 0, 5120));

            RuleFor(x => x.ProfilePictureAllowedExtensionsParsed)
                .Matches(@"^([\.]+\w+[\,]?){1,20}?$")
                .WithMessage("Chuỗi không hợp lệ. Đuôi file có dấu chấm và cách nhau bởi dấu phẩy, không quá 20 đuôi file.");

            RuleFor(x => x.ProfilePictureMaximumHeight)
                .NotEmpty()
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.ProfilePictureMaximumHeight.Required")))
                .InclusiveBetween(1, 2000)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.ProfilePictureMaximumHeight.InclusiveBetween"), 0, 2000));
            
            RuleFor(x => x.ProfilePictureMaximumWidth)
                .NotEmpty()
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.ProfilePictureMaximumWidth.Required")))
                .InclusiveBetween(1, 2000)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.FileUpload.Fields.ProfilePictureMaximumWidth.InclusiveBetween"), 0, 2000));
        }
    }
}