using System;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// TienQD
    /// 
    /// </summary>
    public class ReportRule
    {
        private DateTime _date = DateTime.Now;
        private bool _isActived = true;
        /// <summary>
        /// Id van ban quy dinh
        /// </summary>
        public int ReportRuleId { get; set; }
        /// <summary>
        /// Ma van ban quy dinh
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Ten van ban quy dinh
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Che do bao cao
        /// </summary>
        public string ReportMode { get; set; }
        /// <summary>
        /// Trang thai van ban quy dinh
        /// </summary>
        public bool IsActive {
            get { return _isActived;  }
            set { _isActived = value; }
        }
        /// <summary>
        /// Ngay tao van ban quy dinh
        /// </summary>
        public DateTime DateCreate {
            get { return _date; }
            set { _date = value; }
        }

    }
}
