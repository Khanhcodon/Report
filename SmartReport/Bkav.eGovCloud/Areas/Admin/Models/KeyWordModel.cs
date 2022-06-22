using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(KeyWordValidator))]
    public class KeyWordModel: PacketModel
    {
        public KeyWordModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id của từ khóa
        /// </summary>
        public int KeyWordId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên từ khóa
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.KeyWord.CreateOrEdit.Fields.KeyWordName.Label")]
        public string KeyWordName { get; set; }
    }
}