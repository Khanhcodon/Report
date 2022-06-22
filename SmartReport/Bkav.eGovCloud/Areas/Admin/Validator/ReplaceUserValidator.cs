using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReplaceUserValidator : AbstractValidator<ReplaceUserModel>
    {
        private readonly ResourceBll _resourceService;

        public ReplaceUserValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(p => p.OldUserId)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.OldUserId.Required"));

            RuleFor(p => p.OldUserFulName)
                 .NotEmpty()
                 .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.OldUserId.Required"));
              //   .When(p => p.OldUserId <= 0);

            RuleFor(p => p.NewUserId)
              .NotEmpty()
              .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.NewUserId.Required"));

            RuleFor(p => p.NewUserId)
                .NotEqual(p => p.OldUserId)
                .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.NewUserId.EqualOldUser"));

            RuleFor(p => p.NewUserFulName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.NewUserId.Required"));
                //.When(p => p.NewUserId <= 0);

            RuleFor(p => p.WorkflowIds)
             .NotNull()
             .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.WorkflowIds.Required"));

            RuleFor(p => p.EndDated).NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.EndDated.Required"))
                .When(p => !p.IsDeletedUserWorkflow);

            RuleFor(p => p.BeginDated).NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.BeginDated.Required"));

            RuleFor(p => p.BeginDated)
                .LessThan(p => p.EndDated.Value)
                .WithMessage(_resourceService.GetResource("Admin.ReplaceUser.CreateOrEdit.Fields.BeginDated.LessThanBeginDated"))
                .When(p => !p.IsDeletedUserWorkflow);
        }
    }
}