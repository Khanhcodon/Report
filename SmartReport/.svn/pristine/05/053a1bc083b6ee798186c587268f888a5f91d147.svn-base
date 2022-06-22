using System;
using System.Collections.Generic;
using System.Linq;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : VoteDetail - public - Entity
    /// Access Modifiers: 
    /// Create Date : 200717
    /// Author      : DungNVl
    /// Description : Entity tương ứng với bảng VoteDetail trong CSDL
    /// </summary>
    public class VoteDetail
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id cuộc trưng cầu
        /// </summary>
        public int VoteId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Id chi tiết từng ý kiến của cuộc trưng cầu
        /// </summary>
        public int VoteDetailId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ý kiến đáp án
        /// </summary>
        public string TitleDetail { get; set; }

        
        /// <summary>
        /// Lấy hoặc thiết lập id người tạo (dùng cho trường hợp được phép thêm ý kiên khác)
        /// </summary>
        public int UserIdCreate { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public string UserIdsVote { get; set; }

        /// <summary>
        /// Lấy hoặc danh sách người vote
        /// </summary>
        public List<int> ListUserVote()
        {
            return ParseUserIds(UserIdsVote);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static string UserCompareString(List<int> userIds)
        {
            return string.Format("{0}{1}{0}", ";", string.Join(";", userIds));
        }

        private List<int> ParseUserIds(string userStr)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(userStr))
            {
                return result;
            }

            var userIds = userStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            result = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => Int32.Parse(u)).ToList();

            return result;
        }
    }
}
