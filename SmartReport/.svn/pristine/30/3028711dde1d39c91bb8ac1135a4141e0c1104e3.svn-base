using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ResourceValidator))]
    public class ResourceModel
    {
        public int ResourceId { get; set; }

        [LocalizationDisplayName("Common.Resource.CreateOrEdit.Fields.ResourceKey.Label")]
        public string ResourceKey { get; set; }

        [LocalizationDisplayName("Common.Resource.CreateOrEdit.Fields.ResourceValue.Label")]
        public string ResourceValue { get; set; }
    }
}