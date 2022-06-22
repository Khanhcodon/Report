using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTool
    {
        /// <summary>
        /// 
        /// </summary>
        public string procedure { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tableName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isRun { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isRangeTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigDataTool
    {
        /// <summary>
        /// 
        /// </summary>
        public string connectionInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string connectionOutput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DataTool> data { get; set; }
    }
}
