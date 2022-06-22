using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class BussinessDocFieldDocTypeGroupValidator : AbstractValidator<BussinessDocFieldDocTypeGroupModel>
    {
        private readonly ResourceBll _resourceService;

        public BussinessDocFieldDocTypeGroupValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroupValidator.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroupValidator.CreateOrEdit.Fields.Name.Length"));
        }
    }
}