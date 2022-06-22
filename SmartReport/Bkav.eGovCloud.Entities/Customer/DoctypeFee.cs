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
    /// Create Date : 170914
    /// Author      : QuangP
    /// Description : Entity tương ứng với bảng DocTypeFee trong CSDL
    /// </summary>
    public class DoctypeFee
    {
        /// <summary>
        /// 
        /// </summary>
        public DoctypeFee()
        {
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="doctypeId"></param>
       /// <param name="feeId"></param>
       /// <param name="isRequired"></param>
        public DoctypeFee(Guid doctypeId, int feeId, bool isRequired)
        {
            this.DocTypeId = doctypeId;
            this.FeeId = feeId;
            this.IsRequired = isRequired;
        }
        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public int FeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập đối tượng Paper tương ứng
        /// </summary>
        public Fee Fee { get; set; }
    }
}
