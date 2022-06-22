using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class BackupRestoreConfigValidator : AbstractValidator<BackupRestoreConfigModel>
    {
        private readonly ResourceBll _resourceService;

        public BackupRestoreConfigValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Server)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Server.Required"));

            RuleFor(x => x.Domain)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Domain.Required"))
              .Length(1, 255)
              .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Domain.Length"), 1, 255));

            RuleFor(x => x.DatabaseName)
              .NotEmpty()
              .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.DatabaseName.Required"))
              .Length(1, 255)
              .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.DatabaseName.Length"), 1, 255));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.UserName.Required"))
               .Length(0, 255)
               .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.UserName.MaxLength"));

            RuleFor(x => x.Password)
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Password.MaxLength"));

            RuleFor(x => x.Port)
                .NotNull()
                .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Port.Required"))
                .LessThan(65535)
                .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Port.MaxLength"));

            RuleFor(x => x.DatabaseType)
               .NotNull()
               .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.DatabaseType.Required"));

            RuleFor(x => x.Config)
             .NotNull()
             .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Config.Required"));

            RuleFor(x => x.Config).Must((model, value) =>
            {
                if (model.DatabaseType == (byte)DatabaseType.MySql)
                {
                    try
                    {
                        var results = Json2.ParseAs<ConfigInMySqlModel>(value);
                        if (string.IsNullOrWhiteSpace(results.WorkingDirectory))
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }

                return true;
            })
              .WithMessage(_resourceService.GetResource("BackupRestoreConfig.CreateOrEdit.Fields.Config.Validate"));
        }
    }
}