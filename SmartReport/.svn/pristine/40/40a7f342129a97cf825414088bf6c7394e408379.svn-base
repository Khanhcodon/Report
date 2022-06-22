using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ProcessFunctionTypeValidator : AbstractValidator<ProcessFunctionTypeModel>
    {
        private readonly ResourceBll _resourceService;

        public ProcessFunctionTypeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 128)
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Name.Length"));

            RuleFor(x => x.TextField)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.TextField.Required"))
                .Length(1, 32)
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.TextField.Length"));

            RuleFor(x => x.Query)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Query.Required"));
        }
    }
}