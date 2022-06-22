using System.Web.Mvc;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Validator
{
    public class BusinessLicenseValidator : AbstractValidator<BusinessLicenseModel>
    {
        private readonly ResourceBll _resourceService;

        public BusinessLicenseValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.LicenseCode)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.LicenseCode.Required"));

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.LicenseNumber.Required"));
        }
    }
}