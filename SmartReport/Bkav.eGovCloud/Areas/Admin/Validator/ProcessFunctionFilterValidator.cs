using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ProcessFunctionFilterValidator : AbstractValidator<ProcessFunctionFilterModel>
    {
        private readonly ResourceBll _resourceService;

        public ProcessFunctionFilterValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 300)
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Name.Length"));

            RuleFor(x => x.DataField)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.TextField.Required"));
        }
    }
}