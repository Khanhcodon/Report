using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Form trong CSDL
    /// </summary>
    public class LegalPresentation
    {
        private ICollection<Form> _forms; 
    
        /// <summary>
        /// Lấy hoặc thiết lập Id loại form
        /// </summary>
        public int FormTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại form
        /// </summary>
        public string FormTypeName { get; set; }
    
        /// <summary>
        /// Lấy hoặc thiết lập Các form liên quan
        /// </summary>
        public virtual ICollection<Form> Forms
        {
            get { return _forms ?? (_forms = new List<Form>()); }
            set { _forms = value; }
        }
    }
}
