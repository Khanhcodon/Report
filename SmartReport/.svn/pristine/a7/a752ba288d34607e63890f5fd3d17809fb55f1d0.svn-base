using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ProcessFunctionGroupModelValidator : AbstractValidator<ProcessFunctionGroupModel>
    {
        private readonly ResourceBll _resourceService;

        public ProcessFunctionGroupModelValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.Name.Required"))
                .Length(0, 300)
                .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.Name.Length"));

            RuleFor(x => x.DataQuery)
                .NotEmpty();

            RuleFor(x => x.ColumnQuery)
                .NotEmpty();
        }
    }
}