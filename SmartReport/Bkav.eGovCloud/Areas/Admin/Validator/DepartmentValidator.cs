using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class DepartmentValidator : PacketValidator<DepartmentModel>
    {
        private readonly ResourceBll _resourceService;

        public DepartmentValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Customer.Department.CreateOrEdit.Fields.DepartmentName.Required"));

            //RuleFor(x => x.DepartmentName).Length(1, 128).When(p => !p.HasCreatePacket || p.DepartmentId > 0)
            // .WithMessage(_resourceService.GetResource("Customer.Department.CreateOrEdit.Fields.DepartmentName.Length"))
            // .Must(ValidCreatePacket)
            // .WithMessage(_resourceService.GetResource("Customer.Department.CreateOrEdit.Fields.DepartmentName.ValidCreatePacket"));
        }

        public override bool ValidCreatePacket(DepartmentModel model, dynamic value)
        {
            if (model.DepartmentId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 128)
                {
                    return false;
                }
            }

            return true;
        }
    }
}