using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FileUploadSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 100812
    /// Author      : TrungVH
    /// Description : Entity cho phần cấu hình tệp tải lên
    /// </summary>
    public class FileUploadSettings : ISettings
    {
        ///<summary>
        /// Khởi tạo class <see cref="FileUploadSettings"/>.
        ///</summary>
        public FileUploadSettings()
        {
            FileUploadAllowedExtensions = new List<string>();
            ProfilePictureAllowedExtensions = new List<string>();
        }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước tối đa mỗi tệp được tải lên
        /// </summary>
        public int FileUploadMaximumSizeBytes { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập Kích thước tối thiểu mỗi tệp được tải lên
        /// </summary>
        public int FileUploadMinmumSizeBytes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách các loại tệp được phép tải lên
        /// </summary>
        public List<string> FileUploadAllowedExtensions { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước tối đa mỗi tệp ảnh đại diện của cán bộ được tải lên
        /// </summary>
        public int ProfilePictureMaximumSizeBytes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước tối thiểu mỗi tệp ảnh đại diện của cán bộ được tải lên
        /// </summary>
        public int ProfilePictureMinmumSizeBytes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách các loại tệp ảnh đại diện của cán bộ được phép tải lên
        /// </summary>
        public List<string> ProfilePictureAllowedExtensions { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chiều cao tối đa mỗi tệp ảnh đại diện của cán bộ
        /// </summary>
        public int ProfilePictureMaximumHeight { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chiều cao tối thiểu mỗi tệp ảnh đại diện của cán bộ
        /// </summary>
        public int ProfilePictureMinmumHeight { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chiều rộng tối đa mỗi tệp ảnh đại diện của cán bộ
        /// </summary>
        public int ProfilePictureMaximumWidth { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chiều rộng tối đa mỗi tệp ảnh đại diện của cán bộ
        /// </summary>
        public int ProfilePictureMinmumWidth { get; set; }

    }
}