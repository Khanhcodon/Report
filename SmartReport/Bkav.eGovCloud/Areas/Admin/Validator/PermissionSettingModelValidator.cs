using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class PermissionSettingModelValidator : AbstractValidator<PermissionSettingModel>
    {
        private readonly ResourceBll _resourceService;

        public PermissionSettingModelValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.PermissionSettingName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("PermissionSetting.CreateOrEdit.Fields.PermissionSettingName.Required"))
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("PermissionSetting.CreateOrEdit.Fields.PermissionSettingName.Length"));
        }
    }
}