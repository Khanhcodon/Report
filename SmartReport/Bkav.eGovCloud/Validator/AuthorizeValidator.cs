using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Validator
{
    public class AuthorizeValidator : AbstractValidator<AuthorizeModel>
    {
        private readonly ResourceBll _resourceService;

        public AuthorizeValidator()
        {
            var dateNow = DateTime.Now;
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(p => p.AuthorizedUserId).NotNull()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.AuthorizedUserId.NotNull"));

            RuleFor(p => p.AuthorizedUserName).NotEmpty()
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.AuthorizedUserName.NotNull")).
               Length(0, 255).WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.AuthorizedUserName.Length"));

            RuleFor(p => p.DateBegin).NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.DateBegin.NotNull"))
                .LessThanOrEqualTo(p => p.DateEnd)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.DateBegin.LessThanOrEqualTo"));

            RuleFor(p => p.DateEnd).NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.DateEnd.NotNull"))
               .GreaterThanOrEqualTo(dateNow)
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.DateEnd.GreaterThanOrEqualToDateNow"));
        }
    }
}