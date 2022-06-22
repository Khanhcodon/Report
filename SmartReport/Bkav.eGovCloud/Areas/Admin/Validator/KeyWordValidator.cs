using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class KeyWordValidator : PacketValidator<KeyWordModel>
    {
        private readonly ResourceBll _resourceService;

        public KeyWordValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.KeyWordName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.CreateOrEdit.Fields.KeyWordName.Required"));

            //RuleFor(x => x.KeyWordName)
            //      .Length(1, 50).When(p => p.KeyWordId > 0 || !p.HasCreatePacket)
            //      .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.CreateOrEdit.Fields.KeyWordName.Length"))
            //      .Must(ValidCreatePacket)
            //      .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.CreateOrEdit.Fields.KeyWordName.ValidCreatePacket"));

        }

        public override bool ValidCreatePacket(KeyWordModel model, dynamic value)
        {
            if (model.KeyWordId > 0 || !model.HasCreatePacket)
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