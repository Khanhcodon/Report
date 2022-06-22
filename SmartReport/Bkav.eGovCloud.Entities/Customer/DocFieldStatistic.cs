using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class DocFieldStatistic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DocFieldStatistic()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocFieldStatistic(IDictionary<string, object> resultQuery)
        {
            this.Name = resultQuery["Name"].ToString();
            this.DocFielId = Convert.ToInt32(resultQuery["DocFieldId"]);
            this.VBTonKyTruoc = Convert.ToInt32(resultQuery["VBTonKyTruoc"]);
            this.VBTrongKy = Convert.ToInt32(resultQuery["VBTrongKy"]);
            this.VBChuaHoanThanhConHan = Convert.ToInt32(resultQuery["VBChuaHoanThanhConHan"]);
            this.VBChuaHoanThanhQuaHan = Convert.ToInt32(resultQuery["VBChuaHoanThanhQuaHan"]);
            this.VBHoanThanhDungHan = Convert.ToInt32(resultQuery["VBHoanThanhDungHan"]);
            this.VBHoanThanhQuaHan = Convert.ToInt32(resultQuery["VBHoanThanhQuaHan"]);
        }

        /// <summary>
        /// Tên
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int DocFielId{get;set;}

        /// <summary>
        /// Văn bản tồn kỳ trước
        /// </summary>
        [DataMember]
        public int VBTonKyTruoc { get; set; }

        /// <summary>
        /// Văn bản nhận trong kỳ
        /// </summary>
        [DataMember]
        public int VBTrongKy { get; set; }

        /// <summary>
        /// Văn bản đã hoàn thành đúng hạn
        /// </summary>
        [DataMember]
        public int VBHoanThanhDungHan { get; set; }

        /// <summary>
        /// Văn bản đã hoàn thành nhưng quá hạn
        /// </summary>
        [DataMember]
        public int VBHoanThanhQuaHan { get; set; }

        /// <summary>
        /// Văn bản chưa hoàn hành và đã hết hạn
        /// </summary>
        [DataMember]
        public int VBChuaHoanThanhQuaHan { get; set; }

        /// <summary>
        /// Văn bản chưa hoàn thành nhưng còn hạn
        /// </summary>
        [DataMember]
        public int VBChuaHoanThanhConHan { get; set; }
    }
    
}
