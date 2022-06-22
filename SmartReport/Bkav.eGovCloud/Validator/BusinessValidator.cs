using System.Web.Mvc;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using FluentValidation;

namespace Bkav.eGovCloud.Validator
{
    public class BusinessValidator : AbstractValidator<BusinessModel>
    {
        private readonly ResourceBll _resourceService;

        public BusinessValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.BusinessName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.BusinessName.Required"));

            RuleFor(x => x.BusinessCode)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.BusinessCode.Required"));

            RuleFor(x => x.IssueCodeby)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.IssueCodeby.Required"));

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Address.Required"));

            RuleFor(x => x.Email)
               .Matches(ValidationExpression.EmailRegex)
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Email.Matches"));

            RuleFor(x => x.Phone)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Phone.Matches"));

            RuleFor(x => x.Fax)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Fax.Matches"));

            RuleFor(x => x.Website)
                .Matches(ValidationExpression.DomainRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Website.Matches"));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.UserName.Required"));

            RuleFor(x => x.UserEmail)
               .Matches(ValidationExpression.EmailRegex)
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Email.Matches"));

            RuleFor(x => x.UserPhone)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Phone.Matches"));

            
        }
    }
}