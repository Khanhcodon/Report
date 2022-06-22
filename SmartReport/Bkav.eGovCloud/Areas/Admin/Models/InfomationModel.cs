using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(InfomationValidator))]
    public class InfomationModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập key
        /// </summary>
        public int InfomationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cơ quan chủ quản
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Address.Label")]
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ thư điện tử cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Email.Label")]
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số điện thoại cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Phone.Label")]
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số mở rộng của Phone
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.PhoneExt.Label")]
        public string PhoneExt { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số Fax của cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Fax.Label")]
        public string Fax { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số website của cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Website.Label")]
        public string Website { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số tên hỉnh thức của cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.Alias.Label")]
        public string Alias { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập ten thức của cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.SystemName.Label")]
        public string SystemName { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập ten thức của cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.IsDisplaySystemName.Label")]
        public bool IsDisplaySystemName { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập logo của cơ quan 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.FileName.Label")]
        public string ImagePath { get; set; }
    }
}