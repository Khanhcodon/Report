using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    [System.Runtime.InteropServices.ComVisible(false)]
    public class TimeJobValidator : AbstractValidator<TimeJobModel>
    {
        private readonly ResourceBll _resourceService;

        public TimeJobValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            //RuleFor(x => x.Name)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("TimeJob.CreateOrEdit.Fields.Name.Required"));
        }
    }
}