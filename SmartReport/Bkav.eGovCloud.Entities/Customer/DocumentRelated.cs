using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentRelated
    {
        /// <summary>
        /// 
        /// </summary>
        public int DocumentRelatedId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UpdatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmbryonicLocationName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmbryonicPath { get; set; }
    }
}
