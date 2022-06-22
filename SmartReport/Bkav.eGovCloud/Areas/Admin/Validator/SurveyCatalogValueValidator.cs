using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class SurveyCatalogValueValidator : PacketValidator<SurveyCatalogValueModel>
    {
        private readonly ResourceBll _resourceService;

        public SurveyCatalogValueValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("SurveyCatalogValue.CreateOrEdit.Fields.SurveyCatalogName.Required"))
                .Length(0, 500)
                .WithMessage(_resourceService.GetResource("SurveyCatalogValue.CreateOrEdit.Fields.SurveyCatalogName.MaxLength"));
            RuleFor(x => x.CatalogKey)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("SurveyCatalogValue.CreateOrEdit.Fields.SurveyCatalogCode.Required"))
                .Length(0, 300)
                .WithMessage(_resourceService.GetResource("SurveyCatalogValue.CreateOrEdit.Fields.SurveyCatalogCode.MaxLength"));
            RuleFor(x => x.CatalogId)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("SurveyCatalogValue.CreateOrEdit.Fields.SurveyCatalogId.Required"));

        }
        public override bool ValidCreatePacket(SurveyCatalogValueModel model, dynamic value)
        {
            if (model.CatalogValueId != Guid.Empty || !model.HasCreatePacket)
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