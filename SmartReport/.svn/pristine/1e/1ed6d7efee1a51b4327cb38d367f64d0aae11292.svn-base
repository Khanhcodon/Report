using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ClientValidator : AbstractValidator<ClientModel>
    {
        private readonly ResourceBll _resourceService;
        private const string VALIDATE_GUID = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";

        public ClientValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.MaxLength"));

            RuleFor(x => x.Identifier)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Identifier.Required"))
                .Matches(VALIDATE_GUID)
                .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.GuidFormat"));

            RuleFor(x => x.Secret)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Secret.Required"))
                .Matches(VALIDATE_GUID)
                .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.GuidFormat"));

            RuleFor(x => x.Domain)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Domain.Required"));

            //RuleFor(x => x.Ip)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Ip.Required"));
        }
    }
}