using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ProcessFunctionModelValidator : AbstractValidator<ProcessFunctionModel>
    {
        private readonly ResourceBll _resourceService;

        public ProcessFunctionModelValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.Name.Required"))
                .Length(0, 128)
                .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.Name.Length"));

            //RuleFor(x => x.ProcessFunctionGroupId)
            //  .NotEmpty()
            //  .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.ProcessFunctionGroupId.Required"));

            //RuleFor(x => x.QueryLatest)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.QueryLatest.Required"));

            //RuleFor(x => x.QueryOlder)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.QueryOlder.Required"))
            //    .When(x => x.IsEnablePaging);

            RuleFor(x => x.QueryExportDataToFile)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.QueryExportDataToFile.Required"))
                .When(x => x.HasExportFile);

            RuleFor(x => x.DocColumnSettingId)
               .NotNull()
               .WithMessage(_resourceService.GetResource("ProcessFunction.CreateOrEdit.Fields.DocColumnSettingId.Required"));
        }
    }
}