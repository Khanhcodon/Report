using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(AddressValidator))]
    public class AddressModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập key
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên cơ quan ngoài
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ cơ quan ngoài
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Address.Label")]
        public string AddressString { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ thư điện tử cơ quan ngoài
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Email.Label")]
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số điện thoại cơ quan ngoài
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Phone.Label")]
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số mở rộng của Phone
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.PhoneExt.Label")]
        public string PhoneExt { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số Fax của cơ quan ngoài
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Fax.Label")]
        public string Fax { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã định danh của 1 cơ quan khi liên thông văn bản qua eDoc
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.EdocId.Label")]
        public string EdocId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã định danh của 1 cơ quan khi liên thông văn bản qua eDoc
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.Website.Label")]
        public string Website { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xác định đâu là cơ quan mình
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.IsMe.Label")]
        public bool IsMe { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có bỏ qua xét là cơ quan hiện tại khi đã tờn tại hay không
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.IgnoreExistMe.Label")]
        public bool IgnoreExistMe { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên nhóm của cơ quan
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.GroupName.Label")]
        public string GroupName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên nhóm của cơ quan
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập việc phát hành qua Email
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.IsPublishEmail.Label")]
        public bool IsPublishEmail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập việc phát hành qua Email
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Address.CreateOrEdit.Fields.LevelEdocId.Label")]
        public int LevelEdocId { get; set; }
    }
}