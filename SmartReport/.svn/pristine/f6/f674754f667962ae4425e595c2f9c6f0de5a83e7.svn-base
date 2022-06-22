using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class HolidayValidator : AbstractValidator<HolidayModel>
    {
        private readonly ResourceBll _resourceService;
        public HolidayValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.HolidayName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Holiday.CreateOrEdit.HolidayName.Empty"));

            RuleFor(x => x.HolidayName)
                .Length(0, 63)
                .WithMessage(_resourceService.GetResource("Holiday.CreateOrEdit.HolidayName.MaxLenght"));
        }
    }
}