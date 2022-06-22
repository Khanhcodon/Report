using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Validation;
using Bkav.eGovCloud.Entities.Customer;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class PrinterValidator : AbstractValidator<PrinterModel>
    {
        private readonly ResourceBll _resourceService;

        public PrinterValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.PrinterName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.CreateOrEdit.Fields.PrinterName.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.CreateOrEdit.Fields.PrinterName.Length"));

            RuleFor(x => x.ShareName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.CreateOrEdit.Fields.ShareName.Required"))
                .Length(1, 255)
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.CreateOrEdit.Fields.ShareName.Length"));
        }
    }
}