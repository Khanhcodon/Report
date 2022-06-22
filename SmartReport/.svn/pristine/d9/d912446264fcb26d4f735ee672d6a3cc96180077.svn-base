using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    [FluentValidation.Attributes.Validator(typeof(FileUploadSettingsValidator))]
    public class FileUploadSettingsModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Kích thước tối đa mỗi tệp được tải lên
        /// </summary>
        [LocalizationDisplayName("Setting.FileUpload.Fields.FileUploadMaximumSizeBytes.Label")]
        public int FileUploadMaximumSizeMegaBytes {
            get 
            {
                return FileUploadMaximumSizeBytes / 1048576;
            }
            set 
            {
                FileUploadMaximumSizeBytes = value * 1048576;
            }
        }

        public int FileUploadMaximumSizeBytes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách các loại tệp được phép tải lên
        /// </summary>
        [LocalizationDisplayName("Setting.FileUpload.Fields.FileUploadAllowedExtensionsParsed.Label")]
        public string FileUploadAllowedExtensionsParsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước tối đa mỗi tệp ảnh đại diện của cán bộ được tải lên
        /// </summary>
        [LocalizationDisplayName("Setting.FileUpload.Fields.ProfilePictureMaximumSizeBytes.Label")]
        public int ProfilePictureMaximumSizeKiloBytes
        {
            get
            {
                return ProfilePictureMaximumSizeBytes / 1024;
            }
            set
            {
                ProfilePictureMaximumSizeBytes = value * 1024;
            }
        }

        public int ProfilePictureMaximumSizeBytes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách các loại tệp ảnh đại diện của cán bộ được phép tải lên
        /// </summary>
        [LocalizationDisplayName("Setting.FileUpload.Fields.ProfilePictureAllowedExtensionsParsed.Label")]
        public string ProfilePictureAllowedExtensionsParsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chiều cao tối đa mỗi tệp ảnh đại diện của cán bộ
        /// </summary>
        [LocalizationDisplayName("Setting.FileUpload.Fields.ProfilePictureMaximumHeight.Label")]
        public int ProfilePictureMaximumHeight { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chiều rộng tối đa mỗi tệp ảnh đại diện của cán bộ
        /// </summary>
        [LocalizationDisplayName("Setting.FileUpload.Fields.ProfilePictureMaximumWidth.Label")]
        public int ProfilePictureMaximumWidth { get; set; }
    }
}