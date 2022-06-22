using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ImageSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 270214
    /// Author      : HopCV
    /// Description : Entity cho phần cấu hình chèn ảnh
    /// </summary>
    public class ImageSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết lập cho phép  file đính kem
        /// </summary>
        public bool IsFileAttachment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  số trang bắt đầu
        /// </summary>
        public int NumberStartPage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  số trang kết thúc
        /// </summary>
        public int NumberEndPage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số bít màu
        /// </summary>
        public int ColorBits { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ảnh xám
        /// </summary>
        public bool IsGrayImage { get; set; }

        /// <summary>
        /// Nén ảnh
        /// </summary>
        public int ZipImage { get; set; }
    }
}