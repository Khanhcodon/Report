using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class AuthorizeValidator : AbstractValidator<AuthorizeModel>
    {
        private readonly ResourceBll _resourceService;

        public AuthorizeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.AuthorizeUserName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizeUser.Required"));

            RuleFor(x => x.AuthorizeUserId)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizeUser.Required"));

            RuleFor(x => x.AuthorizeUserId).NotEqual(p => p.AuthorizedUserId)
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizeUser.NotEqual"));

            RuleFor(x => x.AuthorizedUserName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizedUser.Required"));

            RuleFor(x => x.AuthorizedUserId)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizedUser.Required"));

            RuleFor(x => x.AuthorizedUserId).NotEqual(p => p.AuthorizeUserId)
            .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizedUser.NotEqual"));

            RuleFor(x => x.DateBegin)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.DateBegin.Required"));

            RuleFor(x => x.DateBegin).LessThanOrEqualTo(p => p.DateEnd)
            .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.DateBegin.LessThanOrEqualToDateEnd"));

            RuleFor(x => x.DateEnd)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.DateEnd.Required"));

            RuleFor(x => x.DateEnd).GreaterThanOrEqualTo(p => p.DateBegin)
            .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.DateBegin.LessThanOrEqualToDateEnd"));
        }
    }
}