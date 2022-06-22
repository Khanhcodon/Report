using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class DocFieldValidator : PacketValidator<DocFieldModel>
    {
        private readonly ResourceBll _resourceService;

        public DocFieldValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.DocFieldName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.DocFieldName.Required"));

            //RuleFor(x => x.DocFieldName).Length(1, 128).When(p => !p.HasCreatePacket || p.DocFieldId > 0)
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.DocFieldName.Length"))
            //    .Must(ValidCreatePacket)
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.DocFieldName.ValidCreatePacket"));

            RuleFor(x => x.CategoryBusiness)
                .NotEmpty()
                .WithMessage(
                    _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.CategoryBusiness.Required"));
        }

        public override bool ValidCreatePacket(DocFieldModel model, dynamic value)
        {
            if (model.DocFieldId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 128)
                {
                    return false;
                }
            }

            return true;
        }
    }
}