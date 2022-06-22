using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Models
{
    public class VoteModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id Cuộc trưng cầu ý kiến
        /// </summary>
        public int VoteId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian bắt đầu
        /// </summary>
        public DateTime TimeBegin { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian kết thúc
        /// </summary>
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép chọn nhiều ý kiến
        /// </summary>
        public bool IsMultiSelect { get; set; }

        /// <summary>
        /// Lấy hoặc thiết các ý kiến dạng Json
        /// </summary>
        public string VoteDetailId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên cuộc trưng cầu
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép công khai ý kiến
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép thêm các ý kiến khác
        /// </summary>
        public bool IsCommentDiff { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép thông báo
        /// </summary>
        public bool IsNotify { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép xem kết quả ngay
        /// </summary>
        public bool IsViewResultImmediately { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập phòng ban xem trưng cầu
        /// </summary>
        public string DepartmentsView { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người xem trưng cầu
        /// </summary>
        public string UsersView { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập phòng ban tham gia trưng cầu
        /// </summary>
        public string DepartmentsVote { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người tham gia trưng cầu
        /// </summary>
        public string UsersVote { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người tham gia trưng cầu
        /// </summary>
        public string UsersVoted { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người tạo cuộc trưng cầu
        /// </summary>
        public int UserIdCreate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người tạo cuộc trưng cầu
        /// </summary>
        public string UsernameCreate { get; set; }

        /// <summary>
        /// Được quyền vote
        /// </summary>
        public bool IsVote { get; set; }

        /// <summary>
        /// Đang ở trạng thái trong thời gian trưng cầu
        /// </summary>
        public bool IsNow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsView { get; set; }

        /// <summary>
        /// Đang ở trang thái đã trưng cầu
        /// </summary>
        public bool IsVoted { get; set; }

        /// <summary>
        /// Là người tạo
        /// </summary>
        public bool IsCreate { get; set; }

        /// <summary>
        /// Lấy hoặc thiế
        /// </summary>
        public string ListOpinion { get; set; }

        /// <summary>
        /// Người dùng hiện tại
        /// </summary>
        public int CurrentUserId { get; set; }
    }
}