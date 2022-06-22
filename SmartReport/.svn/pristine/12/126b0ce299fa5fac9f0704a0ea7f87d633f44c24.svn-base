using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ReplaceUserValidator))]
    public class ReplaceUserModel
    {
        public ReplaceUserModel()
        {
            IsDeletedUserWorkflow = true;
            BeginDated = DateTime.Now;
            EndDated = DateTime.Now.AddDays(1);
            HasAuthorize = true;
            HasUnActivateUser = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ReplaceUserId { get; set; }

        /// <summary>
        /// Id người dùng bị thay thế
        /// </summary>
        public int OldUserId { get; set; }

        /// <summary>
        /// Họ tên đầy đủ(account) người bị thay thế
        /// </summary>
        [LocalizationDisplayName("ReplaceUser.CreateOrEdit.Fields.OldUserFulName.Label")]
        public string OldUserFulName { get; set; }

        /// <summary>
        /// Id người thay thế
        /// </summary>
        public int NewUserId { get; set; }

        /// <summary>
        /// Họ tên đầy đủ(account) người thay thế
        /// </summary>
        [LocalizationDisplayName("ReplaceUser.CreateOrEdit.Fields.NewUserFulName.Label")]
        public string NewUserFulName { get; set; }

        /// <summary>
        /// Danh sách id các quy trình mà muốn thay thế người dùng
        /// </summary>
        public List<int> WorkflowIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có xóa hẳn người bị thay thế hay không?
        /// </summary>
        [LocalizationDisplayName("ReplaceUser.CreateOrEdit.Fields.IsDeletedUserWorkflow.Label")]
        public bool IsDeletedUserWorkflow { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian bắt đầu có hiệu lực thay thês
        /// </summary>
        [LocalizationDisplayName("ReplaceUser.CreateOrEdit.Fields.BeginDated.Label")]
        public DateTime? BeginDated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian kết thúc hiệu lực thay thês
        /// </summary>
        [LocalizationDisplayName("ReplaceUser.CreateOrEdit.Fields.EndDated.Label")]
        public DateTime? EndDated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian kết thúc hiệu lực thay thês
        /// </summary>
        [LocalizationDisplayName("ReplaceUser.CreateOrEdit.Fields.HasAuthorize.Label")]
        public bool HasAuthorize { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có không cho người thay thế hoạt động nữa không
        /// </summary>
        [LocalizationDisplayName("ReplaceUser.CreateOrEdit.Fields.HasUnActivateUser.Label")]
        public bool HasUnActivateUser { get; set; }
    }
}