using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReportGroupValidator : AbstractValidator<ReportGroupModel>
    {
        private readonly ResourceBll _resourceService;
        public ReportGroupValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ReportGroup.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 300)
                .WithMessage(_resourceService.GetResource("ReportGroup.CreateOrEdit.Fields.Name.MaxLength"));

            RuleFor(x => x.FieldName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("ReportGroup.CreateOrEdit.Fields.FieldName.Required"));
            //.Matches("groupname.*groupvalue|groupvalue.*groupname")
            //.WithMessage(_resourceService.GetResource("ReportGroup.CreateOrEdit.Fields.Query.RequiredParameter"));

            RuleFor(x => x.FieldDisplay)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("ReportGroup.CreateOrEdit.Fields.FieldDisplay.Required"));

            RuleFor(x => x.Query)
              .NotEmpty()
              .WithMessage(_resourceService.GetResource("ReportGroup.CreateOrEdit.Fields.Query.Required"));
        }
    }
}