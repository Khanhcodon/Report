using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Truy vấn cho tiếp dân
    /// </summary>
    public static class TalkingPeopleQuery
    {
        /// <summary>
        /// Lấy ra danh sách hẹn trong ngày
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<TalkingPeople, bool>> GetsOnNowDate()
        {
            return x => x.BookedDate.Year == DateTime.Now.Year && x.BookedDate.Month == DateTime.Now.Month && x.BookedDate.Day == DateTime.Now.Day;
        }
    }
}
