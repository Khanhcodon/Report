using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class DocTypeValidator : PacketValidator<DocTypeModel>
    {
        private readonly ResourceBll _resourceService;

        public DocTypeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.DocTypeName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Customer.DocType.CreateOrEdit.Fields.DocTypeName.Required"));

            //RuleFor(x => x.DocTypeName).Length(1, 1000).When(p => p.DocTypeId != Guid.Empty || !p.HasCreatePacket)
            //    .WithMessage(_resourceService.GetResource("Customer.DocType.CreateOrEdit.Fields.DocTypeName.Length"))
            //    .Must(ValidCreatePacket)
            //     .WithMessage(_resourceService.GetResource("Customer.DocType.CreateOrEdit.Fields.DocTypeName.ValidCreatePacket"));

            RuleFor(x => x.CategoryId)
               .NotEmpty()
               .WithMessage(_resourceService.GetResource("Customer.DocType.CreateOrEdit.Fields.CategoryId.Required"));

            //RuleFor(x => x.DoctypePermissions)
            //    .NotEmpty()
            //    .WithMessage(
            //        _resourceService.GetResource("Customer.DocType.CreateOrEdit.Fields.DoctypePermissions.Required"));
            //   .NotEmpty()
            //   .WithMessage(_resourceService.GetResource("Customer.DocType.CreateOrEdit.Fields.DocTypePermission.Required"));
        }

        public override bool ValidCreatePacket(DocTypeModel model, dynamic value)
        {
            if (model.DocTypeId != Guid.Empty || !model.HasCreatePacket)
            {
                return true;
            }

            var results = ((string)value).Split(';');
            if (results.Length <= 0)
            {
                return false;
            }

            foreach (var item in results)
            {
                if (string.IsNullOrWhiteSpace(item) || item.Length > 1000)
                {
                    return false;
                }
            }

            return true;
        }
    }
}