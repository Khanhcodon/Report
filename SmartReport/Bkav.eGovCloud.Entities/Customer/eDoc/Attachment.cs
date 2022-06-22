using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer.eDoc
{
    /// <summary>
    /// Thông tin file đính kem
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Tên file. Ví dụ: CongVan.pdf, FormDong.json
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Giá trị đã được base64
        /// </summary>
        public string Value { get; set; }
    }
}
