using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeFee - public - Entity
    /// Access Modifiers: 
    /// Create Date : 161015
    /// Author      : TrinhNVd
    /// Description : Entity tương ứng với bảng DoctypeTemplate trong CSDL
    /// </summary>
    public class DoctypeTemplate
    {
        /// <summary>
        /// Id
        /// </summary>
        public int DoctypeTemplateId { get; set; }

        /// <summary>
        /// Mã thủ tục hành chính
        /// </summary>
        public Guid DoctypeId { get; set; }

        /// <summary>
        /// Mã biểu mẫu hành chính
        /// </summary>
        public int OnlineTemplateId { get; set; }

        /// <summary>
        /// Not Map
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Tên biểu mẫu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập đối tượng Doctype tương ứng
        /// </summary>
        public DocType Doctype { get; set; }

        /// <summary>
        /// Biểu mẫu hành chính
        /// </summary>
        public virtual OnlineTemplate OnlineTemplate { get; set; }

    }
}
