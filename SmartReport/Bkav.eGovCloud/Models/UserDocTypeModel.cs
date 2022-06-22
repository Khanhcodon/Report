using System;

namespace Bkav.eGovCloud.Models
{
    public class UserDocTypeModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại hồ sơ, công văn
        /// </summary>
        public string DocTypeName { get; set; }
    }
}