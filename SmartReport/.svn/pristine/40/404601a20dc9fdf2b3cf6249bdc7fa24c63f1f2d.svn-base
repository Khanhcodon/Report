using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    [FluentValidation.Attributes.Validator(typeof(ImageSettingsValidator))]
    public class ImageSettingsModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập cho phép  file đính kem
        /// </summary>
        [LocalizationDisplayName("Setting.Image.Fields.FileAttachments.Label")]
        public bool IsFileAttachment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  số trang bắt đầu
        /// </summary>
        [LocalizationDisplayName("Setting.Image.Fields.NumberStartPage.Label")]
        public int NumberStartPage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  số trang kết thúc
        /// </summary>
        [LocalizationDisplayName("Setting.Image.Fields.NumberEndPage.Label")]
        public int NumberEndPage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số bít màu
        /// </summary>
        [LocalizationDisplayName("Setting.Image.Fields.ColorBits.Label")]
        public int ColorBits { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ảnh xám
        /// </summary>
        [LocalizationDisplayName("Setting.Image.Fields.GrayImage.Label")]
        public bool IsGrayImage { get; set; }

        /// <summary>
        /// Nén ảnh
        /// </summary>
        [LocalizationDisplayName("Setting.Image.Fields.ZipImage.Label")]
        public int ZipImage { get; set; }
    }
}
