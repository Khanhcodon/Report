using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class StoreValidator : AbstractValidator<StoreModel>
    {
        private readonly ResourceBll _resourceService;

        public StoreValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.StoreName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Customer.Store.CreateOrEdit.Fields.StoreName.Required"));
        }
    }
}