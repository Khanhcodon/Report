using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(SignatureValidator))]
    public class SignatureModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập mã cấu hình chữ ký người dùng
        /// </summary>
        public int SignatureId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên chữ ký người dùng
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.SignatureName.Label")]
        public string SignatureName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí đặt chữ ký
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.SignaturePosition.Label")]
        public int SignaturePosition { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập từ cần tìm
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.SearchWord.Label")]
        public string SearchWord { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chữ ký là hình anh hay dạng text
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.IsTypeImage.Label")]
        public bool IsTypeImage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ảnh
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.Image.Label")]
        public string Image { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép hiển thị thông tin của chứng thư số
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.IsDispplayCertificate.Label")]
        public bool IsDispplayCertificate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho :true - tìm từ đầu ; false - tìm từ cuối
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.IsFindType.Label")]
        public bool IsFindType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trường định dạng của anh
        /// </summary>
        [LocalizationDisplayName("Signature.Fields.ImageExtension.Label")]
        public string ImageExtension { get; set; }

        /// <summary>
        /// Thiết lập người dùng
        /// </summary>
        public int UserId { get; set; }
    }
}
