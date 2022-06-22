using Bkav.eGovCloud.Core.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.Document
{
    /// <summary>
    /// 
    /// </summary>
    public static class StatisticUtil
    {
        /// <summary>
        /// Trả về trạng thái Đúng hạn, chưa đến hạn, tới hạn, quá hạn, trễ hẹn
        /// -- 1 - Đúng hạn: đã xử lý đúng hạn
        /// -- 2 - Chưa đến hạn: chưa xử lý + chưa đến hạn.
        /// -- 3 - Quá hạn: Chưa xử lý + quá hạn.
        /// -- 4 - Trễ hẹn: Đã xử lý + quá hạn
        /// </summary>
        /// <param name="status">Trạng thái hồ sơ</param>
        /// <param name="dateAppointed">Ngày hẹn trả</param>
        /// <param name="dateResult">Ngày duyệt</param>
        /// <param name="dateReturned">Ngày trả kết quả</param>
        /// <param name="DateFinished">Ngày kết thúc</param>
        /// <param name="DateRequireSupplementary">Ngày yêu cầu bổ sung, dừng xử lý</param>
        /// <param name="EndDateStatistic">Ngày kết thúc khoảng lấy báo cáo</param>
        /// <param name="DocCode">Mã hồ sơ: thêm vào để debug cho dễ</param>
        /// <returns></returns>
        public static OverdueStatusType OverdueStatus(int status, DateTime dateAppointed, DateTime? dateResult, DateTime? dateReturned, DateTime? DateFinished, DateTime? DateRequireSupplementary, DateTime? EndDateStatistic, string DocCode)
        {
            // DocCode: thêm vào để debug cho dễ

            OverdueStatusType result;

            if (!EndDateStatistic.HasValue || EndDateStatistic.Value.Date > DateTime.Now.Date)
            {
                EndDateStatistic = DateTime.Now;
            }

//#if HoSoMotCuaEdition
            if (dateResult.HasValue)
            {
                result = (dateResult.Value.Date <= dateAppointed.Date) ? OverdueStatusType.ResolveInTime : OverdueStatusType.ResolveLate;
            }
            else if (dateReturned.HasValue)
            {
                result = (dateReturned.Value.Date <= dateAppointed.Date) ? OverdueStatusType.ResolveInTime : OverdueStatusType.ResolveLate;
            }
            else if (DateFinished.HasValue)
            {
                result = (DateFinished.Value.Date <= dateAppointed.Date) ? OverdueStatusType.ResolveInTime : OverdueStatusType.ResolveLate;
            }
            else if (status == 16 && DateRequireSupplementary.HasValue) // Dừng xử lý
            {
                result = DateRequireSupplementary.Value.Date <= dateAppointed.Date ? OverdueStatusType.Pending : OverdueStatusType.Overdue;
            }
            else
            {
                result = EndDateStatistic.Value.Date <= dateAppointed.Date ? OverdueStatusType.Pending : OverdueStatusType.Overdue;
            }
//#else
            
//            if (DateFinished.HasValue)
//            {
//                result = (DateFinished.Value.Date <= dateAppointed.Date) ? OverdueStatusType.ResolveInTime : OverdueStatusType.ResolveLate;
//            }
//            else
//            {
//                result = EndDateStatistic.Value.Date <= dateAppointed.Date ? OverdueStatusType.Pending : OverdueStatusType.Overdue;
//            }
//#endif
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="dateAppointed"></param>
        /// <param name="dateResponse"></param>
        /// <returns></returns>
        public static OverdueStatusTypeVPUB OverdueStatusVPUB(int status, DateTime? dateAppointed, DateTime? dateResponse)
        {
            // DocCode: thêm vào để debug cho dễ
            if (status > 0)
            {
                OverdueStatusTypeVPUB result;
                if (!dateAppointed.HasValue )
                {
                    result = OverdueStatusTypeVPUB.Pending;
                }
                else if (!dateResponse.HasValue)
                {
                    result = OverdueStatusTypeVPUB.Overdue;
                }
                else if (dateAppointed.Value.Date < dateResponse.Value.Date)
                {
                    result = OverdueStatusTypeVPUB.ResolveLate;
                }
                else
                {
                    result = OverdueStatusTypeVPUB.ResolveInTime;
                }

                return result;
            }

            return OverdueStatusTypeVPUB.None;
        }
    }
}
