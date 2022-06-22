
using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReplaceUser - public - Entity
    /// Access Modifiers: 
    /// Create Date : 141115
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng ReplaceUser trong CSDL
    /// </summary>
    public class ReplaceUser
    {
        /// <summary>
        /// 
        /// </summary>
        public int ReplaceUserId { get; set; }

        /// <summary>
        /// Id người dùng thay thế
        /// </summary>
        public int OldUserId { get; set; }

        /// <summary>
        /// Họ tên đầy đủ(account) người bị thay thế
        /// </summary>
        public string OldUserFulName { get; set; }

        /// <summary>
        /// Id người được thay thế
        /// </summary>
        public int NewUserId { get; set; }

        /// <summary>
        /// Họ tên đầy đủ(account) người thay thế
        /// </summary>
        public string NewUserFulName { get; set; }

        /// <summary>
        /// Danh sách id các quy trình mà muốn thay thế người dùng (chuỗi json)
        /// </summary>
        public string WorkflowStr { get; set; }

        /// <summary>
        /// Danh sách id các quy trình mà muốn thay thế người dùng
        /// </summary>
        public List<int> WorkflowIds
        {
            get
            {
                var ids = new List<int>();
                if (!string.IsNullOrEmpty(WorkflowStr))
                {
                    try
                    {
                        ids = Json2.ParseAs<List<int>>(WorkflowStr);
                    }
                    catch { }
                }
                return ids;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập có xóa hẳn người bị thay thế hay không?
        /// </summary>
        public bool IsDeletedOldUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian bắt đầu có hiệu lực thay thês
        /// </summary>
        public DateTime? BeginDated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian kết thúc hiệu lực thay thês
        /// </summary>
        public DateTime? EndDated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian kết thúc hiệu lực thay thês
        /// </summary>
        public bool HasAuthorize { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gia gửi
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người tạo
        /// </summary>
        public int UserCreatedId { get; set; }
    }
}
