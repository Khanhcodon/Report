using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(LawValidator))]
    public class LawModel
    {
        public LawModel()
        {
            Files = new List<File>();
        }
        /// <summary>
        /// Lấy hoặc thiết lập key
        /// </summary>
        public int LawId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số kí hiệu văn bản quy phạm
        /// </summary>
        [LocalizationDisplayName("Law.CreateOrEdit.Fields.NumberSign.Label")]
        public string NumberSign { get; set; }

        /// <summary>
        ///Lấy hoặc thiết lập tóm tắt nội dung văn bản quy phạm
        /// </summary>
        [LocalizationDisplayName("Law.CreateOrEdit.Fields.SubContent.Label")]
        public string SubContent { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<File> Files { get; set; }
    }
}