using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(IncreaseValidator))]
    public class IncreaseModel:PacketModel
    {
        public IncreaseModel() : base() { }

        public int IncreaseId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Increase.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Increase.CreateOrEdit.Fields.Value.Label")]
        public int Value { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập nhóm
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Increase.CreateOrEdit.Fields.BussinessDocFieldDocTypeGroupId.Label")]
        public int BussinessDocFieldDocTypeGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual BussinessDocFieldDocTypeGroup BussinessDocFieldDocTypeGroup { get; set; }
    }
}