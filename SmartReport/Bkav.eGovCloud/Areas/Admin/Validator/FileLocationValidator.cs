using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class FileLocationValidator : AbstractValidator<FileLocationModel>
    {
        private readonly ResourceBll _resourceService;

        public FileLocationValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.FileLocationAddress)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("FileLocation.CreateOrEdit.Fields.FileLocationAddress.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("FileLocation.CreateOrEdit.Fields.FileLocationAddress.MaxLength"));
        }
    }
}