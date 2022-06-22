using System.Text.RegularExpressions;
using System.Web.Mvc;
using FluentValidation;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Models;

namespace Bkav.eGovCloud.Validator
{
    public class DocumentOnlineValidator : AbstractValidator<DocumentOnlineModel>
    {
        public DocumentOnlineValidator()
        {
            var resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.PersonInfo)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.Required"));
            RuleFor(x => x.IdCard)
                .Must(IdCardValidator)
                .WithMessage(resourceService.GetResource("Common.Client.CreateOrEdit.Fields.IdCard.Validate"))
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.Required"));
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.Required"))
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.Required"));
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.Required"));
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(resourceService.GetResource("Common.Client.CreateOrEdit.Fields.Name.Required"));
        }

        public bool IdCardValidator(string idCard)
        {
            Regex regex = new Regex(@"\b\d{9,12}$");
            bool result = false;
            if (idCard != null)
                result = regex.IsMatch(idCard);
            return result;
        }
    }
}