using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class CodeValidator : AbstractValidator<CodeModel>
    {
        private readonly ResourceBll _resourceService;

        public CodeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.CodeName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.CreateOrEdit.Fields.CodeName.Required"));

            RuleFor(x => x.Template)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.CreateOrEdit.Fields.Template.Required"))
                .Matches(ValidationExpression.DocumentCodeRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.CreateOrEdit.Fields.Template.NotMatch"));
        }
    }
}