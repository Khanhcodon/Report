using System;
using System.Linq;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Vote - public - Entity
    /// Access Modifiers: 
    /// Create Date : 200717
    /// Author      : DungNVl
    /// Description : Entity tương ứng với bảng Vote trong CSDL
    /// </summary>
    public class Vote
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id cuộc trưng cầu
        /// </summary>
        public int VoteId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian bắt đầu trưng cầu
        /// </summary>
        public DateTime TimeBegin { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian kết thúc trưng cầu
        /// </summary>
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép chọn nhiều đáp án
        /// </summary>
        public bool IsMultiSelect { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách chi tiết đáp án
        /// </summary>
        public string VoteDetailId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép công khai
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép công khai đáp án
        /// </summary>
        public bool IsCommentDiff { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép
        /// </summary>
        public bool IsNotify { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép 
        /// </summary>
        public bool IsViewResultImmediately { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập phòng ban được xem
        /// </summary>
        public string DepartmentsView { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách người được xem
        /// </summary>
        public string UsersView { get; set; }

        /// <summary>
        /// ấy hoặc thiết lập danh sách phòng ban được Vote
        /// </summary>
        public string DepartmentsVote { get; set; }

        /// <summary>
        /// Danh sách những người được tham gia vote
        /// </summary>
        public string UsersVote { get; set; }

        /// <summary>
        /// Danh sách những người đã thực hiện vote
        /// </summary>        
        public string UsersVoted { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>        
        public int UserIdCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AddUser(string listUserId, int userId)
        {
            var users = ParseUserIds(listUserId);
            if (!users.Contains(userId))
            {
                users.Add(userId);
            }
            var strUsers = UserCompareString(users);
         
            return strUsers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int> ListUserView()
        {
            return ParseUserIds(UsersView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int> ListUserVote()
        {
            return ParseUserIds(UsersVote);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int> ListUserVoted()
        {
            return ParseUserIds(UsersVoted);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userStr"></param>
        /// <returns></returns>
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
