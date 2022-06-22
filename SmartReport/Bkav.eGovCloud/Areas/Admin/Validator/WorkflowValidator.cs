using System.Web.Mvc; 
using System.Collections.Generic;
using FluentValidation;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common; 
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;    

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class WorkflowValidator : PacketValidator<WorkflowModel>
    {
        private readonly ResourceBll _resourceService;

        public WorkflowValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.WorkflowName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Admin.Workflow.CreateOrEdit.Fields.WorkflowName.Required"));

            //RuleFor(x => x.WorkflowName).Length(1, 255).When(p => p.WorkflowId > 0 || !p.HasCreatePacket)
            //    .WithMessage(_resourceService.GetResource("Admin.Workflow.CreateOrEdit.Fields.WorkflowName.Length"))
            //    .Must(ValidCreatePacket)
            //     .WithMessage(_resourceService.GetResource("Admin.Workflow.CreateOrEdit.Fields.WorkflowName.ValidCreatePacket"));

            //RuleFor(x => x.ExpireProcess)
            //    .NotEmpty()
            //    .WithMessage(_resourceService.GetResource("Admin.Workflow.CreateOrEdit.Fields.ExpireProcess.Required"));
        }

        public override bool ValidCreatePacket(WorkflowModel model, dynamic value)
        {
            if (model.WorkflowId > 0 || !model.HasCreatePacket)
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
                if (string.IsNullOrWhiteSpace(item) || item.Length > 255)
                {
                    return false;
                }
            }

            return true;
        }
    }
}