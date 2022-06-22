using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class DocColumnSettingModelValidator : AbstractValidator<DocColumnSettingModel>
    {
        private readonly ResourceBll _resourceService;

        public DocColumnSettingModelValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.DocColumnSettingName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ColumnSetting.CreateOrEdit.Fields.DocColumnSettingName.Required"))
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("ColumnSetting.CreateOrEdit.Fields.DocColumnSettingName.Length"));

            //RuleFor(x => x.DisplayColumn)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("ColumnSetting.CreateOrEdit.Fields.DisplayColumn.Required"));

            //RuleFor(x => x.SortColumn)
            //   .NotEmpty()
            //   .WithMessage(_resourceService.GetResource("ColumnSetting.CreateOrEdit.Fields.SortColumn.Required"));
        }
    }
}