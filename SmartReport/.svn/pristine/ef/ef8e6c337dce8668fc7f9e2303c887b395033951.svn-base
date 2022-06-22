using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class BackupRestoreManagerValidator : AbstractValidator<BackupRestoreManagerModel>
    {
        private readonly ResourceBll _resourceService;

        public BackupRestoreManagerValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Domain)
            .NotEmpty()
            .WithMessage(_resourceService.GetResource("BackupRestoreManager.CreateOrEdit.Fields.Domain.Required"))
            .Length(1, 255)
            .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreManager.CreateOrEdit.Fields.Domain.Length"), 1, 255));

            RuleFor(x => x.Alias)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("BackupRestoreManager.CreateOrEdit.Fields.Alias.Required"))
               .Length(1, 100)
               .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreManager.CreateOrEdit.Fields.Alias.Length"), 1, 255));

            RuleFor(x => x.ZipPassword)
               .Length(0, 255)
               .WithMessage(string.Format(_resourceService.GetResource("BackupRestoreManager.CreateOrEdit.Fields.ZipPassword.Length"), 0, 255));
       
        }
    }
}