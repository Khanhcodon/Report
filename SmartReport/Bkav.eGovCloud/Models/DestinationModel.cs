using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : DestinationModel - public - Entity
    /// Access Modifiers: 
    /// Create Date : 271112
    /// Author      : GiangPN
    /// Description : Định nghĩa 1 entity thể hiện danh sách user, các tham biến khi chuyển công văn.
    /// </summary>
    public class DestinationModel
    {
		/// <summary>
		/// Khởi tạo
		/// </summary>
		public DestinationModel()
		{
			UserIdsDxl = new List<int>();
			UserIdsDg = new List<int>();
			UserTb = new List<int>();
		}

        /// <summary>
        /// Id của user xử lý chính (Xlc).
        /// </summary>
        public int? UserIdXlc { get; set; }

        /// <summary>
        /// Danh sách Id của user tham gia đồng xử lý(Dxl).
        /// </summary>
        public List<int> UserIdsDxl { get; set; }

        /// <summary>
        /// Danh sách Id của user nhận đồng gửi(Dg).
        /// </summary>
        public List<int> UserIdsDg { get; set; }

        /// <summary>
        /// Danh sách các user giám sát
        /// </summary>
        public List<int> UserTb { get; set; }

        /// <summary>
        /// Tham biến gửi khi chọn đồng gửi: là gửi dưới dạng thông báo.
        /// </summary>
        public bool IsThongbao { get; set; }

        /// <summary>
        /// Tham biến gửi khi chọn đồng gửi: là gửi dưới dạng đồng xử lý.
        /// </summary>
        public bool IsDxl { get; set; }

        /// <summary>
        /// Tham biến gửi khi chọn gửi kèm ý kiến.
        /// </summary>
        public bool IsAttachYk { get; set; }

        /// <summary>
        /// Hiển thị danh sách người nhận ý kiến.
        /// </summary>
        public string TargetComment { get; set; }

        /// <summary>
        /// Danh sách người nhận ý kiến.
        /// </summary>
        public List<CommentTransfer> CommentTransfer
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TargetComment))
                {
                    return new List<CommentTransfer>();
                }
                return Json2.ParseAs<List<CommentTransfer>>(TargetComment);
            }
        }

        /// <summary>
        /// Id của node nhận văn bản/hồ sơ theo hướng chuyển
        /// </summary>
        public int NextNodeId { get; set; }

        /// <summary>
        /// Id của node hiện tại
        /// </summary>
        public int CurrentNodeId { get; set; }

        /// <summary>
        /// <para>Lay hoac thiet lap Id quy trinh dang thuc hien ban giao</para>
        /// <para>CuongNT@bkav.com - 040713</para>
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Id của hướng chuyển
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// Lay hoặc thiet lap cac thong tin de hieen thi lai tren giao dien
        /// </summary>
        public string ExtInfo { get; set; }

        /// <summary>
        /// Loại văn bản phân loại
        /// </summary>
        public Guid NewDocTypeId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PublishPlanId { get; set; }
    }
}