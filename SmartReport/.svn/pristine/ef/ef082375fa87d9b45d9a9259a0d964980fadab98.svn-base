using FluentValidation;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Models;
using System.Web.Mvc;
using Bkav.eGovCloud.Core.Validation;

namespace Bkav.eGovCloud.Validator
{
    public class DocumentValidator : AbstractValidator<DocumentModel>
    {
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Contructor.
        /// </summary>
        public DocumentValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Compendium).NotEmpty().WithMessage(_resourceService.GetResource("Document.CreateOrEdit.Compendium.Validate.Empty"));
            RuleFor(x => x.CitizenName).NotEmpty().WithMessage(_resourceService.GetResource("Document.CreateOrEdit.CitizenName.Validate.Empty"));
            RuleFor(x => x.Email).Matches(ValidationExpression.EmailRegex).WithMessage(_resourceService.GetResource("Document.CreateOrEdit.Email.Validate"));
            // RuleFor(x => x.IdentityCard).Matches(ValidationExpression.CmndRegex).WithMessage(_resourceService.GetResource("Document.CreateOrEdit.IdentityCard.Validate"));
            RuleFor(x => x.TotalPage).NotEmpty().WithMessage(_resourceService.GetResource("Document.CreateOrEdit.TotalPage.Validate"));
            RuleFor(x => x.DocFieldIds).NotEmpty().WithMessage(_resourceService.GetResource("Document.CreateOrEdit.DocFieldIds.Validate"));
            RuleFor(x => x.Keyword).NotEmpty().WithMessage(_resourceService.GetResource("Document.CreateOrEdit.Keyword.Validate"));
            RuleFor(x => x.SendTypeId).NotEmpty().WithMessage(_resourceService.GetResource("Document.CreateOrEdit.SendTypeId.Validate"));
            RuleFor(x => x.InOutPlace).NotEmpty().WithMessage(_resourceService.GetResource("Document.CreateOrEdit.InOutPlace.Validate"));
        }
    }
}