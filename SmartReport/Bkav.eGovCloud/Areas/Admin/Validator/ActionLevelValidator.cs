using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ActionLevelValidator : AbstractValidator<ActionLevelModel>
    {
        private readonly ResourceBll _resourceService;

        public ActionLevelValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.ActionLevelName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelName.Required"));


            RuleFor(x => x.ActionLevelCode)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelCode.Required"));
        }
    }
}