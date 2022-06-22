using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Increase - public - Entity
    /// Access Modifiers: 
    /// Create Date : 140912
    /// Author      : DungHV
    /// Description : Entity tương ứng với bảng Increase trong CSDL
    /// </summary>
    public class Increase
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id loại nhảy số
        /// </summary>
        public int IncreaseId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại nhảy số
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị nhảy số đã được cấp mới nhất
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm
        /// </summary>
        public int BussinessDocFieldDocTypeGroupId { get; set; }
      
        /// <summary>
        /// 
        /// </summary>
        public BussinessDocFieldDocTypeGroup BussinessDocFieldDocTypeGroup { get; set; }
    }
}
