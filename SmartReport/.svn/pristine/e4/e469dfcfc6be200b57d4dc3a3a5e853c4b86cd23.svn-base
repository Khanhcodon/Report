using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class DocumentRelatedValidator : AbstractValidator<DocumentRelatedModel>
    {
        private readonly ResourceBll _resourceService;

        public DocumentRelatedValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("DocumentRelated.CreateOrEdit.Fields.Name.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("DocumentRelated.CreateOrEdit.Fields.Name.MaxLength"));
            RuleFor(x => x.EmbryonicLocationName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("DocumentRelated.CreateOrEdit.Fields.File.Required"));

        }
    }
}