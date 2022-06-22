using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : KeyWord - public - Entity
    /// Access Modifiers:
    /// Create Date : 200814
    /// Author      : ManhNHc
    /// Description : Entity tương ứng với bảng KeyWord trong CSDL
    /// </summary>
    public class Law
    {
        /// <summary>
        /// Lấy hoặc thiết lập key
        /// </summary>
        public int LawId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số kí hiệu văn bản quy phạm
        /// </summary>
        public string NumberSign { get; set; }

        /// <summary>
        ///Lấy hoặc thiết lập tóm tắt nội dung văn bản quy phạm
        /// </summary>]
        public string SubContent { get; set; }

        /// <summary>
        /// List fieldId, ngăn cách bởi dấu , NotMap
        /// </summary>
        public string FileIds { get; set; }
        
        /// <summary>
        ///
        /// </summary>
        public virtual ICollection<File> Files { get; set; }
    }
}