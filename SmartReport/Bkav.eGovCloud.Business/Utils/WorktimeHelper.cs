using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Worktime;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Business
{
	/// <author>
	/// Bkav Corp. - BSO - eGov - Department 2
	/// Project: eGov Cloud v1.0
	/// Class : WorktimeHelper - public - BLL
	/// Access Modifiers: 
	///     *Inherit: None
	/// Create Date : 221212
	/// Author      : CuongNT
	/// </author>
	/// <summary>
	/// <para>Xử lý thời gian làm việc trên hệ thống eGov cloud</para>
	/// <para>(CuongNT@bkav.com - 221212)</para>
	/// </summary>
	public class WorktimeHelper
	{
		// TODO: TienBV cần xử lý nốt trường hợp làm việc sáng thứ 7 và nghỉ chiều thứ 7 ở một số cơ quan.
		private readonly TimeBll _timeBll;
		private readonly WorktimeOfDay _workTimeOfDay;
		private readonly WorkTimeSettings _workTimeSettings;
		private readonly MemoryCacheManager _cacheManager;

		///<summary>
		/// Khởi tạo class <see cref="IncreaseBll"/>.
		///</summary>
		///<param name="timeBll">Bll xử lý cấu hình thời gian hệ thống</param>
		///<param name="workTimeSettings">Cấu hình giờ làm việc hành chính tương ứng</param>
		///<param name="cacheManager"></param>
		public WorktimeHelper(TimeBll timeBll,
			WorkTimeSettings workTimeSettings,
			MemoryCacheManager cacheManager)
		{
			_timeBll = timeBll;
			_workTimeSettings = workTimeSettings;
			_workTimeOfDay = GetWorktimeOfDay();
			_cacheManager = cacheManager;
		}

		/// <summary>
		/// CuongNT - 20.12.2012.
		/// Trả về ngày xử lý xong công việc sau khi tính trừ các ngày nghỉ.
		/// </summary>
		/// <param name="startTime">Ngày bắt đầu xử lý công việc</param>
		/// <param name="workDays">Số ngày làm việc</param>
		/// <returns></returns>
		public DateTime? GetDateAppoint(DateTime startTime, int workDays)
		{
			// workDays = 0 cau hinh tiep nhan nua ngay
			if (workDays <= 0)
			{
				return null;
			}

			var workOffsets = _timeBll.GetDayWorkOffsets().ToList();
			var holidays = GetHolidaysAndWeekends(startTime.Year);
            var dateWorkOffsets = GetWorkOffsets(workOffsets);
			DateTime date;

			date = WorktimeUtil.GetDateAppoint(startTime, workDays, _workTimeOfDay, holidays, dateWorkOffsets, (WorktimeType)_workTimeSettings.WorkTimeType);

			//Tính ra khoang từ ngày dự kiến sử lý xong với ngày truyền vào
			//Lấy ra danh sách ngày lam bù theo năm truyền vào
			//Duyệt qua danh sách các ngày làm bù 
			//Ngày truyền vào qua mỗi lần thì tăng thêm 1 ngày và kiểm tra
			//Nếu tồn tại thì thì giảm j đi 1
			//add lại j vào ngày được tính ở trên => ngày xử lý xong công việc 

			//int number = (date - startTime).Days;
			//int j = 0;
			//for (int i = 0; i < number; i++)
			//{
			//	var dayForOffset = startTime.AddDays(i);
			//	if (!IsWorkOffset(dayForOffset, workOffsets)) continue;

			//	// Nếu ngày làm bù là thứ 7 mà hạn vào thứ 2 phải giảm thêm 1 ngày để tránh chủ nhật
			//	if (dayForOffset.DayOfWeek == DayOfWeek.Saturday)
			//	{
			//		if (date.DayOfWeek == DayOfWeek.Monday)
			//		{
			//			date = date.AddDays(-1);
			//		}
			//	}
			//	j--;
			//}
			
            return date;
		}

		/// <summary>
		/// Trả về hạn xử lý của văn bản(cộng thẳng vào start time, không tính theo hồ sơ 1 cửa)
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="workDays"></param>
		/// <param name="holidays"></param>
		/// <returns></returns>
		private DateTime GetDocumentDateAppoint(DateTime startTime, int workDays, List<Holiday> holidays)
		{
			//Trả về 23:59:59
			var date = startTime.AddDays(workDays).Date.AddSeconds(86399);

			//Nếu trong thời gian xử lý có cuối tuần hoặc ngày nghỉ, tăng thêm 1 ngày
			for (int i = 0; i < workDays; i++)
			{
				if (IsWeekendOrHoliday(startTime.AddDays(i + 1)))
				{
					date = date.AddDays(1);
				}
			}
			return date;
		}

		/// <summary> 
		/// CuongNT - 20.12.2012.
		/// Trả về số ngày làm việc trong khoảng thời gian. Đã trừ đi các ngày nghỉ.
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		public int GetWorkdays(DateTime startTime, DateTime endTime)
		{
			var holidays = GetHolidaysAndWeekends(startTime.Year);
			int workDays = WorktimeUtil.GetWorkdays(startTime, endTime, holidays);

			int number = (endTime - startTime).Days;
			var workOffsets = _timeBll.GetDayWorkOffsets(startTime.Year).ToList();
			int j = 0;
			for (int i = 0; i < number; i++)
			{
				if (IsWorkOffset(startTime.AddDays(i), workOffsets))
				{
					++j;
				}
			}
			return workDays + j;
			//  return workDays;
		}

		/// <summary>
		/// Trả về số phút làm việc trong khoảng thời gian
		/// </summary>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		/// <returns></returns>
		public int GetWorkminutes(DateTime fromDate, DateTime toDate)
		{
			var holidays = GetHolidaysAndWeekends(fromDate.Year);
			int workTime = WorktimeUtil.GetWorkminutes(fromDate, toDate, _workTimeOfDay, holidays);

			int minuteSubtract1 = GetMinuteSubtract(_workTimeOfDay.AMStartWorking, _workTimeOfDay.AMEndWorking);
			int minuteSubtract2 = GetMinuteSubtract(_workTimeOfDay.PMStartWorking, _workTimeOfDay.PMEndWorking);
			int number = (toDate - fromDate).Days;
			var workOffsets = _timeBll.GetDayWorkOffsets(fromDate.Year).ToList();
			int j = 0;
			for (int i = 0; i < number; i++)
			{
				if (IsWorkOffset(fromDate.AddDays(i), workOffsets))
				{
					++j;
				}
			}
			var workOffsetsTime = j * (minuteSubtract1 + minuteSubtract2);
			return workTime + workOffsetsTime;
		}

		private int GetMinuteSubtract(int startHour, int startMinute, int endHour, int endMinute)
		{
			int num = (endHour - startHour) * 60 + (endMinute - startMinute);
			if (num <= 0)
				throw new ApplicationException("WorktimeUtil::GetMinuteSubtract::Lỗi:: starttime phải nhỏ hơn endtime");
			else
				return num;
		}

		private int GetMinuteSubtract(HourMinute startHour, HourMinute endHour)
		{
			int num = (endHour.Hour - startHour.Hour) * 60 + (endHour.Minute - startHour.Minute);
			if (num <= 0)
				throw new ApplicationException("WorktimeUtil::GetMinuteSubtract::Lỗi:: starttime phải nhỏ hơn endtime");
			else
				return num;
		}

		/// <summary>
		/// Trả về true nếu là ngày nghỉ
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public bool IsWeekendOrHoliday(DateTime date)
		{
			var holidays = GetHolidaysAndWeekends(date.Year);
			return WorktimeUtil.IsWeekendOrHoliday(date, holidays);
		}

		/// <summary>
		/// Trả về true nếu là ngày nghỉ
		/// </summary>
		/// <param name="holidays">Danh sách ngày nghỉ</param>
		/// <param name="date">Ngày cân check</param>
		/// <returns></returns>
		public bool IsWeekendOrHoliday(ref List<Holiday> holidays, DateTime date)
		{
			return WorktimeUtil.IsWeekendOrHoliday(date, holidays);
		}

		/// <summary>
		///  Trả về true nếu là ngày nghỉ cuối tuần
		///  <para>HopCv 030414</para>
		/// </summary>
		/// <param name="date">Thời gian</param>
		/// <returns></returns>
		public bool IsWeekend(DateTime date)
		{
			var weekends = GetAllWeekends();
			return WorktimeUtil.IsWeekendOrHoliday(date, weekends);
		}

		/// <summary>
		///  Trả về true nếu là ngày nghỉ lễ, ngỉ bù trong năm hiện tại và năm sau
		///  <para>HopCv 030414</para>
		/// </summary>
		/// <param name="date">Thời gian</param>
		/// <returns></returns>
		public bool IsHoliday(DateTime date)
		{
			var holidays = GetHolidays(date.Year);
			return WorktimeUtil.IsWeekendOrHoliday(date, holidays);
		}

		/// <summary>
		///  Trả về true nếu là ngày  làm bù
		/// </summary>
		/// <param name="date">Thời gian</param>
		///  <param name="workOffsets">Danh sách ngày làm bù</param>
		/// <returns></returns>
		public bool IsWorkOffset(DateTime date, List<Entities.Customer.Holiday> workOffsets)
		{
			return workOffsets.Any(d => d.HolidayDate.ToShortDateString().Equals(date.ToShortDateString()));
		}

        /// <summary>
        ///  Trả về true nếu là ngày  làm bù
        /// </summary>
        /// <param name="date">Thời gian</param>
        ///  <param name="workOffsets">Danh sách ngày làm bù</param>
        /// <returns></returns>
        public List<Holiday> GetWorkOffsets(List<Entities.Customer.Holiday> workOffsets)
        {
            return workOffsets.Select(c => new Holiday {
                DayOfWeek = null,
                HolidayDate = c.HolidayDateInSolar,
                IsRepeat = c.IsRepeated,
                IsWeekend = false
            }).ToList();
        }

        /// <summary>
        /// Trả về danh sách ngày nghỉ cuối tuần và nghỉ lễ, nghỉ bù trong năm hien tai va nam sau
        /// <para>HopCv 030414</para>
        /// </summary>
        ///  <param name="year">Năm</param>
        /// <returns>Danh sách ngày nghỉ cuối tuần và nghỉ lễ, nghỉ bù trong năm</returns>
        public List<Holiday> GetHolidaysAndWeekends(int year)
		{
			var results = GetHolidays(year);
			var weekendDays = GetAllWeekends();
			results.AddRange(weekendDays);
			return results;
		}

		/// <summary>
		/// Trả về danh sách ngày  nghỉ lễ, nghỉ bù trong năm hien tai va nam sau
		/// <para> HopCV</para>
		/// <para> Create date:030414</para>
		/// </summary>
		///  <param name="year">Năm</param>
		/// <returns>Danh sách ngày nghỉ lễ, nghỉ bù trong năm</returns>
		private List<Holiday> GetHolidays(int year)
		{
			var allholidays = _timeBll.GetHolidays().ToList();
			// Lay danh sach ngay nghi Duong lap
			var repeatesSolar = allholidays.GetRepeateSolars();
			var results = repeatesSolar.Select(c => new Holiday
			{
				DayOfWeek = null,
				HolidayDate = c.HolidayDateInSolar,
				IsRepeat = c.IsRepeated,
				IsWeekend = false
			}).ToList();

			// Lay danh sach ngay nghi Am lap; Duong, Am khong lap; --> chuyen sang duong cho year va year+1
			var holidays1 = allholidays.GetHolidaysInYear(year);
			results.AddRange(holidays1.Where(c => !c.IsRepeated || c.IsLunar).Select(c => new Holiday
			{
				DayOfWeek = null,
				HolidayDate = c.HolidayDateInSolar,
				IsRepeat = c.IsRepeated,
				IsWeekend = false
			}));

			var holidays2 = allholidays.GetHolidaysInYear(year + 1);
			results.AddRange(holidays2.Where(c => !c.IsRepeated || c.IsLunar).Select(c => new Holiday
			{
				DayOfWeek = null,
				HolidayDate = c.HolidayDateInSolar,
				IsRepeat = c.IsRepeated,
				IsWeekend = false
			}));

			return results;
		}

		/// <summary>
		/// Lấy tất cả các ngày nghỉ trong năm
		/// </summary>
		/// <returns></returns>
		private List<Holiday> GetAllWeekends()
		{
			var weekends = _timeBll.GetWeekends();
			var results = weekends.Select(c => new Holiday
			{
				DayOfWeek = c,
				HolidayDate = null,
				IsRepeat = false,
				IsWeekend = true
			}).ToList();
			return results;
		}

		/// <summary>
		/// Trả về các mốc thờ gian làm việc trong một ngày
		/// </summary>
		/// <returns></returns>
		private WorktimeOfDay GetWorktimeOfDay()
		{
			var worktimeOfDay = new WorktimeOfDay();
			if (!string.IsNullOrEmpty(_workTimeSettings.AmStartTime))
			{
				var times = _workTimeSettings.AmStartTime.Split(':');
				worktimeOfDay.AMStartWorking = new HourMinute
				{
					Hour = int.Parse(times[0]),
					Minute = times.Length > 1 ? int.Parse(times[1]) : 0
				};
			}
			if (!string.IsNullOrEmpty(_workTimeSettings.AmEndTime))
			{
				var times = _workTimeSettings.AmEndTime.Split(':');
				worktimeOfDay.AMEndWorking = new HourMinute
				{
					Hour = int.Parse(times[0]),
					Minute = times.Length > 1 ? int.Parse(times[1]) : 0
				};
			}
			if (!string.IsNullOrEmpty(_workTimeSettings.PmStartTime))
			{
				var times = _workTimeSettings.PmStartTime.Split(':');
				worktimeOfDay.PMStartWorking = new HourMinute
				{
					Hour = int.Parse(times[0]),
					Minute = times.Length > 1 ? int.Parse(times[1]) : 0
				};
			}
			if (!string.IsNullOrEmpty(_workTimeSettings.PmEndTime))
			{
				var times = _workTimeSettings.PmEndTime.Split(':');
				worktimeOfDay.PMEndWorking = new HourMinute
				{
					Hour = int.Parse(times[0]),
					Minute = times.Length > 1 ? int.Parse(times[1]) : 0
				};
			}
			return worktimeOfDay;
		}
	}
}
