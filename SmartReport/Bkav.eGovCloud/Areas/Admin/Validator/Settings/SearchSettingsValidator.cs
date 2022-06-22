using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class SearchSettingsValidator : AbstractValidator<SearchSettingsModel>
    {
        private readonly ResourceBll _resourceService;

        public SearchSettingsValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.ServerUrl)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Setting.Search.Fields.ServerUrl.Required"));
                //.Matches("(?<!/)$")
                //.WithMessage(_resourceService.GetResource("Setting.Search.Fields.ServerUrl.NotEndsWith"));

            RuleFor(x => x.NumberSelected)
              .NotEmpty()
              .WithMessage(_resourceService.GetResource("Setting.Search.Fields.NumberSelected.Required"))
              .InclusiveBetween(20, 100)
              .WithMessage(string.Format(_resourceService.GetResource("Setting.Search.Fields.NumberSelected.InclusiveBetween"), 20, 100));
        }
    }
}