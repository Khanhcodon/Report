using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(CodeValidator))]
    public class CodeModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của bảng mã
        /// </summary>
        public int CodeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của bảng mã
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Code.CreateOrEdit.Fields.CodeName.Label")]
        public string CodeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mấu của bảng mã
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Code.CreateOrEdit.Fields.Template.Label")]
        public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số lượng cuối cùng
        /// </summary>
        public int NumberLastest { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id của nhảy số
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Code.CreateOrEdit.Fields.IncreaseId.Label")]
        public int IncreaseId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id của phòng ban sử dụng
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Code.CreateOrEdit.Fields.BussinessDocFieldDocTypeGroupId.Label")]
        public int BussinessDocFieldDocTypeGroupId { get; set; }

        /// <summary>
        /// Cho phép cấp số trước
        /// </summary>
        public bool HasCapSoTruoc { get; set; }

        /// <summary>
        /// Lưu lại nhưng code đã được check đưa lên đầu
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Thiết lập một giá trị mặc định cho category
        /// </summary>
        public bool IsDefault { get; set; }
    }
}