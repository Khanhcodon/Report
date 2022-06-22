using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class BackupRestoreHistoryValidator : AbstractValidator<BackupRestoreHistoryModel>
    {
        private readonly ResourceBll _resourceService;

        public BackupRestoreHistoryValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Domain)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.BusinessType.CreateOrEdit.Fields.Domain.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Admin.BusinessType.CreateOrEdit.Fields.Domain.Length"));

            RuleFor(x => x.Description)
             .NotEmpty()
             .WithMessage(_resourceService.GetResource("Admin.BusinessType.CreateOrEdit.Fields.Description.Required"));
        }
    }
}