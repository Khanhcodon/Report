using System;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.Business.Customer.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public static class DailyProcessQuery
    {
        /// <summary>
        /// Là tiếp nhận trong ngày
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.DailyProcess, bool>> IsCreated(int userId, DateTime from, DateTime to)
        {
            return d => (from < d.DateCreated && d.DateCreated < to)
                        && d.UserId == userId
                        && d.ProcessType == 1;
        }

        /// <summary>
        /// Là bàn giao trong ngày
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.DailyProcess, bool>> IsTransfered(int userId, DateTime from, DateTime to)
        {
            return d => (from < d.DateCreated && d.DateCreated < to)
                        && d.UserId == userId
                        && d.ProcessType == 2;
        }

        /// <summary>
        /// Là ký duyệt trong ngày
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.DailyProcess, bool>> IsApproved(int userId, DateTime from, DateTime to)
        {
            return d => (from < d.DateCreated && d.DateCreated < to)
                        && d.UserId == userId
                        && d.ProcessType == 3;
        }

        /// <summary>
        /// Là trả kết quả trong ngày
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.DailyProcess, bool>> IsReturned(int userId, DateTime from, DateTime to)
        {
            return d => (from < d.DateCreated && d.DateCreated < to)
                        && d.UserId == userId
                        && d.ProcessType == 4;
        }

        /// <summary>
        /// Là tiếp nhận bổ sung trong ngày
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.DailyProcess, bool>> IsSupplemented(int userId, DateTime from, DateTime to)
        {
            return d => (from < d.DateCreated && d.DateCreated < to)
                        && d.UserId == userId
                        && d.ProcessType == 5;
        }

        /// <summary>
        /// Kiểm tra tồn tại
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="documentCopyId"></param>
        /// <param name="processType"></param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.DailyProcess, bool>> Exists(int userId, int documentCopyId, int processType)
        {
            var currentDay = DateTime.Now.Day;
            return d => d.DateCreated.Day == currentDay     // Xử lý trong ngày
                        && d.UserId == userId               // 
                        && d.DocumentCopyId == documentCopyId
                        && d.ProcessType == processType;
        }

        /// <summary>
        /// Lấy ra danh sách tất cả các xử lý trong ngày của cán bộ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.DailyProcess, bool>> WithUserId(int userId, DateTime from, DateTime to)
        {
            return d => (from < d.DateCreated && d.DateCreated < to)
                        && d.UserId == userId;
        }
    }
}
