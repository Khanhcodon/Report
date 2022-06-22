using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ShareFolderValidator : AbstractValidator<ShareFolderModel>
    {
        private readonly ResourceBll _resourceService;

        public ShareFolderValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.Directory)
           .NotEmpty()
           .WithMessage(_resourceService.GetResource("ShareFolder.CreateOrEdit.Fields.ShareFolderUrl.Required"));

            RuleFor(x => x.UserName)
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("ShareFolder.CreateOrEdit.Fields.UserName.MaxLength"));

            RuleFor(x => x.Password)
                .Length(0, 255)
                .WithMessage(_resourceService.GetResource("ShareFolder.CreateOrEdit.Fields.Password.MaxLength"));
        }
    }
}