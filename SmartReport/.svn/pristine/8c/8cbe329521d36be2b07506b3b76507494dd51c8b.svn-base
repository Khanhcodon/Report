using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class AdminGeneralSettingsValidator : AbstractValidator<AdminGeneralSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public AdminGeneralSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.DefaultPageSize)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.General.Fields.DefaultPageSize.Required"))
                .InclusiveBetween(1, 100000)
                .WithMessage(string.Format(_resourceService.GetResource("Setting.General.Fields.DefaultPageSize.InclusiveBetween"), 0, 100000));

            RuleFor(x => x.ListPageSizeParsed)
                .Matches(@"^(\d+[\,]?){1,20}?$")
                .WithMessage("Chuỗi không hợp lệ. Chỉ được nhập số và dấu phẩy, không quá 20 page size.");

            //RuleFor(x => x.DefaultPageSizeHome)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Setting.General.Fields.DefaultPageSize.Required"));
            //.InclusiveBetween(1, 100000)
            //.WithMessage(string.Format(_resourceService.GetResource("Setting.General.Fields.DefaultPageSize.InclusiveBetween"), 0, 100000));

            //RuleFor(x => x.ListPageSizeParsedHome)
            //    .Matches(@"^(\d+[\,]?){1,20}?$")
            //    .WithMessage("Chuỗi không hợp lệ. Chỉ được nhập số và dấu phẩy, không quá 20 page size.");

            RuleFor(x => x.Avatar)
             .NotEmpty()
             .WithMessage(_resourceService.GetResource("Setting.General.Fields.Avatar.Required"));
        }
    }
}