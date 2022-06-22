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
    public class VoteDetailModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int VoteId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int VoteDetailId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public string UserIdsVote { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu đã được băm với muối
        /// </summary>
        public string TitleDetail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>
        public int UserIdCreate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>
        public string UsersInfo { get; set; }
    }
}