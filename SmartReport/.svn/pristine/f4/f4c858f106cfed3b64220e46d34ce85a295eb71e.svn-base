using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class CategoryValidator : PacketValidator<CategoryModel>
    {
        private readonly ResourceBll _resourceService;

        public CategoryValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.CategoryName.Required"));

            //RuleFor(x => x.CategoryName)
            //  .Length(1,255).When(p=>p.CategoryId > 0 || !p.HasCreatePacket)
            //  .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.CategoryName.Length"))
            //  .Must(ValidCreatePacket)
            //   .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.CategoryName.ValidCreatePacket"));

            RuleFor(x => x.CategoryBusiness)
                .NotEmpty()
                .WithMessage(
                    _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.CategoryBusiness.Required"));
        }

        public override bool ValidCreatePacket(CategoryModel model, dynamic value)
        {
            if (model.CategoryId > 0 || !model.HasCreatePacket)
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