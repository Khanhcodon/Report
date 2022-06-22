using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using System.Text.RegularExpressions;

namespace kav.eGovCloud.Areas.Admin.Validator
{
    public class OfficeValidator : AbstractValidator<OfficeModel>
    {
        private ResourceBll _resourceService;
        public OfficeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(o => o.OfficeName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Common.Office.CreateOrEdit.Fields.OfficeName.Required"))
                .Length(1, 300)
                .WithMessage(_resourceService.GetResource("Common.Office.CreateOrEdit.Fields.OfficeName.MaxLength"));

            RuleFor(o => o.Phone)
                .Matches("^\\d{1,20}$")
                .WithMessage(_resourceService.GetResource("Common.Office.CreateOrEdit.Fields.Phone"));

            RuleFor(o => o.Email)
           .Must(CheckEmail).WithMessage(_resourceService.GetResource("Common.Office.CreateOrEdit.Fields.CheckEmail.Required"));


        }
        public bool CheckEmail(string Email)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (string.IsNullOrEmpty(Email))
            {
                return true;
            }
            bool result = false;
            if (Email != null)
            {
                result = regex.IsMatch(Email);
            }
            return result;
        }
    }
}