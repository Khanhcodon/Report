using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class InfomationValidator : AbstractValidator<InfomationModel>
    {
        private readonly ResourceBll _resourceService;

        public InfomationValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Name.Required"));
            
            RuleFor(x => x.Address)
                 .NotEmpty()
                 .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Address.Required"));
           
            RuleFor(x => x.Email)
                .Matches(ValidationExpression.EmailRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Email.Matches"));

            RuleFor(x => x.Phone)
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Phone.Matches"));

            //RuleFor(x => x.PhoneExt)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.PhoneExt.Matches"));

            RuleFor(x => x.Fax)                
                .Matches(ValidationExpression.PhoneRegex)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Fax.Matches"));
        }
    }
}