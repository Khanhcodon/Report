using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Statistic - public - Entity
    /// Access Modifiers:
    /// Create Date : 22102014
    /// Author      : QuangP
    /// Description : Entity dùng để thống kê văn bản
    /// </summary>
     [DataContract]
    public class Statistic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Statistic()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Statistic(IDictionary<string, object> resultQuery)
        {
            this.Name = resultQuery["Name"].ToString();
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