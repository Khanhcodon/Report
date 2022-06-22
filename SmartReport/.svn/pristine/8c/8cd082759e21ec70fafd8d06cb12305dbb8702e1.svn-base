using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(JobTitlesValidator))]
    public class JobTitlesModel:PacketModel
    {
        public JobTitlesModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id của chức danh
        /// </summary>
        public int JobTitlesId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên chức danh
        /// </summary>
        [LocalizationDisplayName("Customer.JobTitles.CreateOrEdit.Fields.JobTitlesName.Label")]
        public string JobTitlesName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quyền được ký duyệt
        /// </summary>
        [LocalizationDisplayName("Customer.JobTitles.CreateOrEdit.Fields.IsApproved.Label")]
        public bool IsApproved { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mức ưu tiên
        /// </summary>
        [LocalizationDisplayName("Customer.JobTitles.CreateOrEdit.Fields.PriorityLevel.Label")]
        public int PriorityLevel { get; set; }

        /// <summary>
        /// Xác định có phải là văn thư hay không
        /// </summary>
        [LocalizationDisplayName("Customer.JobTitles.CreateOrEdit.Fields.IsClerical.Label")]
        public bool IsClerical { get; set; }

        /// <summary>
        /// Thiết lập quyền có thể tiếp nhận văn bản đến hay không
        /// </summary>
        [LocalizationDisplayName("Customer.JobTitles.CreateOrEdit.Fields.CanGetDocumentCome.Label")]
        public bool CanGetDocumentCome { get; set; }
    }
}