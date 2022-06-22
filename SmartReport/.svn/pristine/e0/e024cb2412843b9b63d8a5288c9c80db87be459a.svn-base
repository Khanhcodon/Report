using System;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Workflow - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Workflow trong CSDL
    /// </summary>
    public class Workflow
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id luồng văn bản, hồ sơ
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên luồng văn bản, hồ sơ
        /// </summary>
        public string WorkflowName { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập template giao diện mặc định cho quy trình
        ///// </summary>
        //public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Luồng văn bản hổ sơ (kiểu json)
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Luồng văn bản hổ sơ (kiểu doi tuong)</para>
        /// <para>CuongNT@bkav.com - 040713</para>
        /// </summary>
        public Path JsonInObject
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Json))
                {
                    return null;
                }
                var result = Json2.ParseAs<Path>(Json);
                if (result == null)
                {
                    throw new Exception("Chuỗi json quy trình không đúng format: " + Json);
                }
                foreach (var node in result.Nodes)
                {
                    node.Parent = result;
                }
                foreach (var action in result.GetActions())
                {
                    action.Parent = result;
                    action.WorkflowId = result.Id;
                }
                return result;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người cập nhật cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

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
