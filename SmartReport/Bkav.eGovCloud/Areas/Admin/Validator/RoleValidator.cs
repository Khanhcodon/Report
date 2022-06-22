using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class RoleValidator : AbstractValidator<RoleModel>
    {
        private readonly ResourceBll _resourceService;

        public RoleValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.RoleKey)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Required"))
                .Length(1, 32)
                .WithMessage(_resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Length"))
                .Matches(ValidationExpression.RoleKeyRegex)
                .WithMessage(_resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Matches"));

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Role.CreateOrEdit.Fields.RoleName.Required"))
                .Length(1, 128)
                .WithMessage(_resourceService.GetResource("Role.CreateOrEdit.Fields.RoleName.Length"));

            RuleFor(x => x.Description)
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("Role.CreateOrEdit.Fields.Description.Length"));
        }
    }
}