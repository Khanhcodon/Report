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
    /// Create Date : 22102015
    /// Author      : TrinhNVd
    /// Description : Entity dùng để thống kê văn bản
    /// </summary>
    [DataContract]
    public class StatisticNew
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public StatisticNew()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public StatisticNew(IDictionary<string, object> resultQuery)
        {
            this.PreExtisting = Convert.ToInt32(resultQuery["PreExtisting"]);
            this.NewReception = Convert.ToInt32(resultQuery["NewReception"]);
            this.SolvedInTime = Convert.ToInt32(resultQuery["SolvedInTime"]);
            this.SolvedLate = Convert.ToInt32(resultQuery["SolvedLate"]);
            this.Pending = Convert.ToInt32(resultQuery["Pending"]);
            this.PendingLate = Convert.ToInt32(resultQuery["PendingLate"]);
        }

        /// <summary>
        /// Tồn kỳ trước 
        /// </summary>
        public int PreExtisting { get; set; }

        /// <summary>
        /// Nhận trong kỳ
        /// </summary>
        public int NewReception { get; set; }

        /// <summary>
        /// Đã giải quyết, đúng hẹn
        /// </summary>
        public int SolvedInTime { get; set; }

        /// <summary>
        /// Đã giải quyết, trễ hẹn
        /// </summary>
        public int SolvedLate { get; set; }

        /// <summary>
        /// Chưa giải quyết, chưa đến hạn
        /// </summary>
        public int Pending { get; set; }

        /// <summary>
        /// Chưa giải quyết, quá hạn
        /// </summary>
        public int PendingLate { get; set; }

        /// <summary>
        /// Tỉ lệ % đã giải quyết đúng hẹn
        /// </summary>
        public int SolvedInTimePercent
        {
            get
            {
                return SolvedInTime / (SolvedInTime + SolvedLate) * 100;
            }
        }

        /// <summary>
        /// Tỉ lệ % đã giải quyết trễ hẹn
        /// </summary>
        public int SolvedLatePercent
        {
            get
            {
                return SolvedLate / (SolvedInTime + SolvedLate) * 100;
            }
        }

        /// <summary>
        /// Tỉ lệ % chưa giải quyết, chưa đến hạn
        /// </summary>
        public int PeddingPercent
        {
            get
            {
                return Pending / (Pending + PendingLate) * 100;
            }
        }

        /// <summary>
        /// Tỉ lệ % chưa giải quyết, quá hạn
        /// </summary>
        public int PendingLatePercent
        {
            get
            {
                return PendingLate / (Pending + PendingLate) * 100;
            }
        }

    }
}