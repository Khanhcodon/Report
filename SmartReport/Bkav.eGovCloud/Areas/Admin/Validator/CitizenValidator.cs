using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class CitizenValidator : AbstractValidator<CitizenModel>
    {
        private ResourceBll _resourceService;

        public CitizenValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Account)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.Citizen.CreateOrEdit.Fields.Account.Required"))
                .Length(6, 20)
                .WithMessage(_resourceService.GetResource("Admin.Citizen.CreateOrEdit.Fields.Account.MinMaxLength"));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.Citizen.CreateOrEdit.Fields.Password.Required"))
                .Must(BeGoodPassword)
                .WithMessage(_resourceService.GetResource("Admin.Citizen.CreateOrEdit.Fields.Password.Validate"))
                .When(x => x.Id <= 0);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage(_resourceService.GetResource("Admin.Citizen.CreateOrEdit.Fields.ConfirmPassword.Validate"))
                .When(x => x.Id <= 0);

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Admin.Citizen.CreateOrEdit.Fields.Email.Required"))
               .EmailAddress()
               .WithMessage(_resourceService.GetResource("Admin.Citizen.CreateOrEdit.Fields.Email.Email"));
        }

        public bool BeGoodPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-zA-Z]).{6,20}$");
            bool result = false;
            if (password != null)
                result = regex.IsMatch(password);
            return result;
        }

        public bool IdCardValidator(string idCard)
        {
            Regex regex = new Regex(@"\b\d{9,12}$");
            bool result = false;
            if (idCard != null)
                result = regex.IsMatch(idCard);
            return result;
        }
    }
}