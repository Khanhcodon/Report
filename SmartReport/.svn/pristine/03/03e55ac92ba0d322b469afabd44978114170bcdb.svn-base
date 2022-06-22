using System.Runtime.InteropServices;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    [ComVisible(false)]
    public class DomainValidator : AbstractValidator<DomainModel>
    {
        private readonly ResourceBll _resourceService;

        public DomainValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.DomainName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Fields.DomainName.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Fields.DomainName.MaxLength"));

            //RuleFor(x => x.CustomerName)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Fields.CustomerName.Required"))
            //    .Length(1, 255)
            //    .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Fields.CustomerName.MaxLength"));

            //RuleFor(x => x.Email)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Fields.Email.Required"))
            //    .Length(1, 255)
            //    .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Fields.Email.MaxLength"))
            //    .EmailAddress()
            //    .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Fields.Email.NotMatch"));

            RuleFor(x => x.AccountUsername)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Account.CreateOrEdit.Fields.Username.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Account.CreateOrEdit.Fields.Username.MaxLength"))
                .When(x => x.DomainId <= 0);

            RuleFor(x => x.AccountPassword)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Account.CreateOrEdit.Fields.Password.Required"))
               .Length(8, 64)
               .WithMessage(_resourceService.GetResource("Account.CreateOrEdit.Fields.Password.MaxLength"))
               .When(x => x.DomainId <= 0);
        }
    }
}