﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer.Ad_Report
{
    /// <summary>
    /// 
    /// </summary>
   public class Ad_targets
    {/// <summary>
    /// 
    /// </summary>
    /// 
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid ValueId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public int DepartmentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Upper { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Lower { get; set; }
    }
}
