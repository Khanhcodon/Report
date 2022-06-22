using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using FluentValidation;

namespace Bkav.eGovCloud.Validator
{
    public class UserSettingValidator : AbstractValidator<UserSettingModel>
    {
        private readonly ResourceBll _resourceService;

        public UserSettingValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            //RuleFor(x => x.DefaultPageSizeHome)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Setting.General.Fields.DefaultPageSize.Required"))
            //    .InclusiveBetween(10, 100000)
            //    .WithMessage(string.Format(_resourceService.GetResource("Setting.General.Fields.DefaultPageSize.InclusiveBetween"), 10, 100000));

            //RuleFor(x => x.ListPageSizeHome).NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Setting.General.Fields.ListPageSizeHome.Required"))              
            //    .Matches(@"^(\d+[\,]?){1,20}?$")
            //    .WithMessage("Chuỗi không hợp lệ. Chỉ được nhập số và dấu phẩy, không quá 20 page size.");
        }
    }
}