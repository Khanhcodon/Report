using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    [FluentValidation.Attributes.Validator(typeof(FileLocationSettingsValidator))]
    public class FileLocationSettingsModel
    {
        [LocalizationDisplayName("Setting.FileLocation.Fields.Threshold.Label")]
        public int Threshold { get; set; }
    }
}