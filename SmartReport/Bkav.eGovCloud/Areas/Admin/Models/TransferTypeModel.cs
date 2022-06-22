using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(TransferTypeValidator))]
    public class TransferTypeModel : PacketModel
    {
        public TransferTypeModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id của hình thức chuyển
        /// </summary>
        public int TransferTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên hình thức chuyển
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.TransferType.CreateOrEdit.Fields.TransferTypeName.Label")]
        public string TransferTypeName { get; set; }
    }
}