using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class InCatalogValueValidator : PacketValidator<InCatalogValueModel>
    {
        private readonly ResourceBll _resourceService;

        public InCatalogValueValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.InCatalogValueName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalogValue.CreateOrEdit.Fields.IndicatorCatalogName.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("IndicatorCatalogValue.CreateOrEdit.Fields.IndicatorCatalogName.MaxLength"));
            RuleFor(x => x.InCatalogValueCode)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalogValue.CreateOrEdit.Fields.IndicatorCatalogCode.Required"))
                .Length(0, 300)
                .WithMessage(_resourceService.GetResource("IndicatorCatalogValue.CreateOrEdit.Fields.IndicatorCatalogCode.MaxLength"));
            RuleFor(x => x.InCatalogId)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("IndicatorCatalogValue.CreateOrEdit.Fields.InCatalogId.Required"));

        }
        public override bool ValidCreatePacket(InCatalogValueModel model, dynamic value)
        {
            if (model.InCatalogValueId != Guid.Empty || !model.HasCreatePacket)
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