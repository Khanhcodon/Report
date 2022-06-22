using Bkav.eGovCloud.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer.Settings
{
    /// <summary>
    /// Cấu hình thông tin kết nối đến phần chat tin dieu hanh
    /// </summary>
    public class ChatSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết tên hiển thị
        /// </summary>
        public string ValaToken { get; set; }
        /// <summary>
        /// Lấy hoặc thiết ValaService
        /// </summary>
        public string ValaService { get; set; }

        /// <summary>
        /// Lấy hoặc thiết MessageService
        /// </summary>
        public string MessageService { get; set; }

        /// <summary>
        /// Lấy hoặc thiết ValaId
        /// </summary>
        public string ValaId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết ValaFactoryId
        /// </summary>
        public string ValaFactoryId { get; set; }

    }
}
