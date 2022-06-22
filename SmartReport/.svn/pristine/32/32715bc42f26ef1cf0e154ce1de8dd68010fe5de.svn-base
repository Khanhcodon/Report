using System.Runtime.InteropServices;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    [ComVisible(false)]
    public class ConnectionValidator : AbstractValidator<ConnectionModel>
    {
        private readonly ResourceBll _resourceService;

        public ConnectionValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            //RuleFor(x => x.ConnectionName)
            //    .NotNull()
            //    .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Connection.Fields.ConnectionName.Required"));

            RuleFor(x => x.DbType)
                .NotNull()
                .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Connection.Fields.DbType.Required"));

            RuleFor(x => x.ServerName)
                .NotNull()
                .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Connection.Fields.Server.Required"));

            RuleFor(x => x.Database)
                .NotNull()
                .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Connection.Fields.Database.Required"));

            RuleFor(x => x.Username)
                .NotNull()
                .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Connection.Fields.Username.Required"));

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage(_resourceService.GetResource("Domain.CreateOrEdit.Connection.Fields.Password.Required"))
                .When(x => x.ConnectionId <= 0);
        }
    }
}