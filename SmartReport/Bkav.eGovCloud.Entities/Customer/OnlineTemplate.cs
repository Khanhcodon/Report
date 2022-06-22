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
    public class OnlineTemplate
    {
        /// <summary>
        /// Id
        /// </summary>
        public int OnlineTemplateId { get; set; }

        /// <summary>
        /// Tên thủ tục hành chính
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File Id
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public File File { get; set; }

    }
}
