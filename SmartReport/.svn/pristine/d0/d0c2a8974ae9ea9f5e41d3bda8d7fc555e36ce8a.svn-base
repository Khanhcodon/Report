using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class CategoryDisaggreationValidator : PacketValidator<CategoryDisaggreationModel>
    {
        private readonly ResourceBll _resourceService;
        public CategoryDisaggreationValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.CategoryDisaggregationName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.MaxLength"));
        }

        public override bool ValidCreatePacket(CategoryDisaggreationModel model, dynamic value)
        {
            if (model.CategoryDisaggregationId != Guid.Empty || !model.HasCreatePacket)
            {
                return true;
            }

            var results = ((string)value).Split(';');
            if (results.Length <= 0)
            {
                return false;
            }

            //foreach (var item in results)
            //{
            //    if (string.IsNullOrWhiteSpace(item) || item.Length > 1000)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }
    }
}