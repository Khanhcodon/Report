using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class InterfaceConfigValidator : PacketValidator<InterfaceConfigModel>
    {
        private readonly ResourceBll _resourceService;

        public InterfaceConfigValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.InterfaceConfigName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.CreateOrEdit.Fields.InterfaceConfigName.Required"));

            //RuleFor(x => x.InterfaceConfigName)
            //  .Length(1,255).When(p=>p.InterfaceConfigId > 0 || !p.HasCreatePacket)
            //  .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.CreateOrEdit.Fields.InterfaceConfigName.Length"))
            //  .Must(ValidCreatePacket)
            //   .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.CreateOrEdit.Fields.InterfaceConfigName.ValidCreatePacket"));
        }

        public override bool ValidCreatePacket(InterfaceConfigModel model, dynamic value)
        {
            if (model.InterfaceConfigId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 255)
                {
                    return false;
                }
            }

            return true;
        }
    }
}