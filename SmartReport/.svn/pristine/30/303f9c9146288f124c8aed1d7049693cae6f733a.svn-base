using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 161013
    /// Author      : DungHV
    /// Description : Entity tương ứng với bảng BusinessType trong CSDL
    /// </summary>
    public class BusinessType
    {
        private ICollection<Business> _business; 
    
        /// <summary>
        /// Lấy hoặc thiết lập Id loại doanh nghiệp
        /// </summary>
        public int BusinessTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại doanh nghiệp
        /// </summary>
        public string BusinessTypeName { get; set; }
    
        /// <summary>
        /// Lấy hoặc thiết lập Các form liên quan
        /// </summary>
        public virtual ICollection<Business> Businesss
        {
            get { return _business ?? (_business = new List<Business>()); }
            set { _business = value; }
        }
    }
}
