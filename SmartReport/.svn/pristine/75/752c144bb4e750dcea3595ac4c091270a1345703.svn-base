using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class TreeGroupValidator : PacketValidator<TreeGroupModel>
    {
        private readonly ResourceBll _resourceService;

        public TreeGroupValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

            RuleFor(x => x.TreeGroupName)
                .NotEmpty()
                .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TreeGroup.CreateOrEdit.Fields.TreeGroupName.Required"));

            //RuleFor(x => x.TreeGroupName).Length(1, 255).When(p => !p.HasCreatePacket || p.TreeGroupId > 0)
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TreeGroup.CreateOrEdit.Fields.TreeGroupName.Length"))
            //    .Must(ValidCreatePacket)
            //    .WithMessage(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.TreeGroup.CreateOrEdit.Fields.TreeGroupName.ValidCreatePacket"));
        }

        public override bool ValidCreatePacket(TreeGroupModel model, dynamic value)
        {
            if (model.TreeGroupId > 0 || !model.HasCreatePacket)
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