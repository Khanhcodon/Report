using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class BackupRestoreFileConfigValidator : AbstractValidator<BackupRestoreFileConfigModel>
    {
        private readonly ResourceBll _resourceService;

        public BackupRestoreFileConfigValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Domain)
            .NotEmpty()
            .WithMessage(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.Domain.Required"))
            .Length(1, 255)
            .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.Domain.Length"), 1, 255));

            RuleFor(x => x.Directory)
              .NotEmpty()
              .WithMessage(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.Directory.Required"))
              .Length(1, 255)
              .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.Directory.Length"), 1, 255));

            RuleFor(x => x.FileName)
                 .NotEmpty()
                 .WithMessage(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.FileName.Required"))
                 .Length(1, 255)
                 .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.FileName.Length"), 1, 255));

            RuleFor(x => x.UserName)
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.UserName.MaxLength"));

            RuleFor(x => x.Password)
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("BackupRestoreFileConfig.CreateOrEdit.Fields.Password.MaxLength"));
        }
    }
}