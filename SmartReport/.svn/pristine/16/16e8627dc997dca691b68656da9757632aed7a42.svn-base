using Bkav.eGovCloud.Entities.Enum;
using System;
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Quản lý các mã hồ sơ, văn bản đã cấp trước hoặc đã cấp nhưng bị hủy
    /// </summary>
    public class CodeTemp
    {
        /// <summary>
        /// Key
        /// </summary>
        public int CodeTempId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập code id
        /// </summary>
        public int CodeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập document được gắn mã
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập code 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Kiểu:
        /// 1: mã đã cấp nhưng bị hủy
        /// 2: mã được cấp trước nhảy số (Mã tự chọn)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// User được cấp mã
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Thời điểm cấp mã
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CodeTempTypeEnum CodeTempTypeInEnum
        {
            get
            {
                return (CodeTempTypeEnum)Type;
            }
        }
    }
}
