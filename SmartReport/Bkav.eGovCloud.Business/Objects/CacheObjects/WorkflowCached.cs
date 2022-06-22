using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WorkflowCached
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id luồng văn bản, hồ sơ
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên luồng văn bản, hồ sơ
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Luồng văn bản hổ sơ (kiểu json)
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Id người cập nhật cuối cùng
        ///// </summary>
        //public int? LastModifiedByUserId { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Ngày cập nhật cuối cùng
        ///// </summary>
        //public DateTime? LastModifiedOnDate { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public byte[] VersionByte { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra luồng văn bản này đang được sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hạn xử lý luồng văn bản 
        /// </summary>
        public int ExpireProcess { get; set; }

        /// <summary>
        /// Chuỗi json chứa loại workflow
        /// </summary>
        public string WorkflowTypeJson { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id template giao diện mặc định cho quy trình
        /// </summary>
        public int? InterfaceConfigId { get; set; }
    }
}
