using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class TransferTypeValidator : PacketValidator<TransferTypeModel>
    {
        private readonly ResourceBll _resourceService;

        public TransferTypeValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.TransferTypeName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TransferType.CreateOrEdit.Fields.TransferTypeName.Required"));

            //RuleFor(x => x.TransferTypeName)
            //   .Length(1, 50).When(p => !p.HasCreatePacket || p.TransferTypeId > 0)
            //   .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TransferType.CreateOrEdit.Fields.TransferTypeName.Length"))
            //   .Must(ValidCreatePacket)
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TransferType.CreateOrEdit.Fields.TransferTypeName.ValidCreatePacket"));
        }

        public override bool ValidCreatePacket(TransferTypeModel model, dynamic value)
        {
            if (model.TransferTypeId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 50)
                {
                    return false;
                }
            }

            return true;
        }
    }
}