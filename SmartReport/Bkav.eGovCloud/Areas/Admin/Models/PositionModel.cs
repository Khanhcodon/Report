using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(PositionValidator))]
    public class PositionModel : PacketModel
    {
        public PositionModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id của chức vụ
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên chức vụ
        /// </summary>
        [LocalizationDisplayName("Customer.Position.CreateOrEdit.Fields.PositionName.Label")]
        public string PositionName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mức ưu tiên
        /// </summary>
        [LocalizationDisplayName("Customer.Position.CreateOrEdit.Fields.PriorityLevel.Label")]
        public int PriorityLevel { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quyền được ký duyệt
        /// </summary>
        [LocalizationDisplayName("Customer.Position.CreateOrEdit.Fields.IsApproved.Label")]
        public bool IsApproved { get; set; }
    }
}