using Bkav.eGovCloud.Business.Tasks;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Admin.TimerJobSchedules;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Bkav.eGovCloud.Business.Tasks
{
	/// <summary>
	/// 
	/// </summary>
	public class DocTypeScheduler
	{
		private readonly string egovApiUrl;

		/// <summary>
		/// Lịch trình chạy các job tự động.
		/// </summary>
		/// <remarks>
		/// Cần code sao có thể tách ra chạy service riêng, hiện tạm thời đang cho chạy chung cùng eGov
		/// </remarks>
		public DocTypeScheduler()
		{
			egovApiUrl = ConfigurationManager.AppSettings["egovApiUrl"];
			LogService("DocTypeScheduler");
		}

		private void LogService(IEnumerable<string> message)
		{
			var logFolder = CommonHelper.MapPath("~/Logs");
			var logFile = System.IO.Path.Combine(logFolder, "logservice_" + DateTime.Now.ToString("ddMMyyyy"));
			try
			{
				System.IO.File.AppendAllLines(logFile, message);
			}
			catch { }
		}

		private void LogService(string message)
		{
			LogService(new[] { message });
		}

		public void RunSchedule(IEnumerable<Guid> docTypeIds, DocTypeTimeJob timeJob)
		{
			foreach (var docTypeId in docTypeIds)
			{
				var job = new DocTypeJob(egovApiUrl, docTypeId, timeJob);

				switch (timeJob.ScheduleType)
				{
					case (int)DocTypeScheduleType.HangNgay:
						var dailySchedule = Json2.ParseAs<DailySchedule>(timeJob.ScheduleConfig);
						JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Days().At(dailySchedule.FromHour, dailySchedule.FromMinute));
						break;
					case (int)DocTypeScheduleType.HangTuan:
						var weeklySchedule = Json2.ParseAs<WeeklySchedule>(timeJob.ScheduleConfig);
						JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(0).Weeks().On(weeklySchedule.FromDayOfWeek).At(weeklySchedule.FromHour, weeklySchedule.FromMinute));
						break;
					case (int)DocTypeScheduleType.HangThang:
						var monthlySchedule = Json2.ParseAs<MonthlySchedule>(timeJob.ScheduleConfig);

                        //convert datetime to string
                        //DateTime dtime = new DateTime(monthlySchedule.)

						if (monthlySchedule.ByDayOfWeek == ScheduleMonthlyType.Day)
						{
							JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().On(monthlySchedule.FromDayOfMonth).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
						}
						else
						{
							switch (monthlySchedule.WeekOfMonth)
							{
								case 1:
									JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheFirst(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
									break;
								case 2:
									JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheSecond(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
									break;
								case 3:
									JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheThird(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
									break;
								case 4:
									JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheFourth(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
									break;
								default:
									break;
							}
						}
						break;
					case (int)DocTypeScheduleType.HangQuy:
						var quarterlySchedule = Json2.ParseAs<QuarterlySchedule>(timeJob.ScheduleConfig);
						var date = new DateTime(
							DateTime.Now.Year,
							quarterlySchedule.MonthOfQuarter,
							quarterlySchedule.FromDayOfMonth,
							quarterlySchedule.FromHour,
							quarterlySchedule.FromMinute,
							0);
						while (date < DateTime.Now) date = date.AddMonths(3);
						JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunOnceAt(date).AndEvery(3).Months().On(quarterlySchedule.FromDayOfMonth).At(quarterlySchedule.FromHour, quarterlySchedule.FromMinute));
						break;
					case (int)DocTypeScheduleType.HangNam:
						var yearSchedule = Json2.ParseAs<YearSchedule>(timeJob.ScheduleConfig);
						var dayOfYear = new DateTime(DateTime.Now.Year, yearSchedule.FromMonth, yearSchedule.FromDayOfMonth).DayOfYear;
						JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Years().On(dayOfYear).At(yearSchedule.FromHour, yearSchedule.FromMinute));
						break;
					default:
						break;
				}
			}
		}

        public void RunScheduleDueTime(IEnumerable<Guid> docTypeIds, DocTypeTimeJob timeJob)
        {
            //schedule tới hạn
            foreach(var docTypeId in docTypeIds)
            {
                var job = new DocTypeJobDueDate(egovApiUrl, docTypeId, timeJob);
                switch (timeJob.ScheduleTypeDueDate)
                {
                    case (int)DocTypeScheduleTypeDueDate.HangNgayDueDate:
                        var dailySchedule = Json2.ParseAs<DailyScheduleDueDate>(timeJob.ScheduleConfigDueDate);
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Days().At(dailySchedule.FromHour, dailySchedule.FromMinute));
                        break;
                    case (int)DocTypeScheduleTypeDueDate.HangTuanDueDate:
                        var weeklySchedule = Json2.ParseAs<WeeklyScheduleDueDate>(timeJob.ScheduleConfigDueDate);
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(0).Weeks().On(weeklySchedule.FromDayOfWeek).At(weeklySchedule.FromHour, weeklySchedule.FromMinute));
                        break;
                    case (int)DocTypeScheduleTypeDueDate.HangThangDueDate:
                        var monthlySchedule = Json2.ParseAs<MonthlyScheduleDueDate>(timeJob.ScheduleConfigDueDate);

                        //convert datetime to string
                        //DateTime dtime = new DateTime(monthlySchedule.)

                        if (monthlySchedule.ByDayOfWeek == ScheduleMonthlyType.Day)
                        {
                            JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().On(monthlySchedule.FromDayOfMonth).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                        }
                        else
                        {
                            switch (monthlySchedule.WeekOfMonth)
                            {
                                case 1:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheFirst(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                case 2:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheSecond(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                case 3:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheThird(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                case 4:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheFourth(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case (int)DocTypeScheduleTypeDueDate.HangQuyDueDate:
                        var quarterlySchedule = Json2.ParseAs<QuarterlyScheduleDueDate>(timeJob.ScheduleConfigDueDate);
                        var date = new DateTime(
                            DateTime.Now.Year,
                            quarterlySchedule.MonthOfQuarter,
                            quarterlySchedule.FromDayOfMonth,
                            quarterlySchedule.FromHour,
                            quarterlySchedule.FromMinute,
                            0);
                        while (date < DateTime.Now) date = date.AddMonths(3);
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunOnceAt(date).AndEvery(3).Months().On(quarterlySchedule.FromDayOfMonth).At(quarterlySchedule.FromHour, quarterlySchedule.FromMinute));
                        break;
                    case (int)DocTypeScheduleTypeDueDate.HangNamDueDate:
                        var yearSchedule = Json2.ParseAs<YearScheduleDueDate>(timeJob.ScheduleConfigDueDate);
                        var dayOfYear = new DateTime(DateTime.Now.Year, yearSchedule.FromMonth, yearSchedule.FromDayOfMonth).DayOfYear;
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Years().On(dayOfYear).At(yearSchedule.FromHour, yearSchedule.FromMinute));
                        break;
                    default:
                        break;

                }
            }
        }

        public void RunScheduleOutOfTime(IEnumerable<Guid> docTypeIds, DocTypeTimeJob timeJob)
        {
            //schedule quá hạn
            foreach (var docTypeId in docTypeIds)
            {
                var job = new DocTypeJobOutOfDate(egovApiUrl, docTypeId, timeJob);
                switch (timeJob.ScheduleTypeDueDate)
                {
                    case (int)DocTypeScheduleTypeOutOfDate.HangNgayOutOfDate:
                        var dailySchedule = Json2.ParseAs<DailyScheduleOutOfDate>(timeJob.ScheduleConfigOutOfDate);
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Days().At(dailySchedule.FromHour, dailySchedule.FromMinute));
                        break;
                    case (int)DocTypeScheduleTypeOutOfDate.HangTuanOutOfDate:
                        var weeklySchedule = Json2.ParseAs<WeeklyScheduleOutOfDate>(timeJob.ScheduleConfigOutOfDate);
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(0).Weeks().On(weeklySchedule.FromDayOfWeek).At(weeklySchedule.FromHour, weeklySchedule.FromMinute));
                        break;
                    case (int)DocTypeScheduleTypeDueDate.HangThangDueDate:
                        var monthlySchedule = Json2.ParseAs<MonthlyScheduleOutOfDate>(timeJob.ScheduleConfigOutOfDate);

                        //convert datetime to string
                        //DateTime dtime = new DateTime(monthlySchedule.)

                        if (monthlySchedule.ByDayOfWeek == ScheduleMonthlyType.Day)
                        {
                            JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().On(monthlySchedule.FromDayOfMonth).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                        }
                        else
                        {
                            switch (monthlySchedule.WeekOfMonth)
                            {
                                case 1:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheFirst(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                case 2:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheSecond(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                case 3:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheThird(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                case 4:
                                    JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Months().OnTheFourth(monthlySchedule.DayOfWeek).At(monthlySchedule.FromHour, monthlySchedule.FromMinute));
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case (int)DocTypeScheduleTypeOutOfDate.HangQuyOutOfDate:
                        var quarterlySchedule = Json2.ParseAs<QuarterlyScheduleOutOfDate>(timeJob.ScheduleConfigOutOfDate);
                        var date = new DateTime(
                            DateTime.Now.Year,
                            quarterlySchedule.MonthOfQuarter,
                            quarterlySchedule.FromDayOfMonth,
                            quarterlySchedule.FromHour,
                            quarterlySchedule.FromMinute,
                            0);
                        while (date < DateTime.Now) date = date.AddMonths(3);
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunOnceAt(date).AndEvery(3).Months().On(quarterlySchedule.FromDayOfMonth).At(quarterlySchedule.FromHour, quarterlySchedule.FromMinute));
                        break;
                    case (int)DocTypeScheduleTypeDueDate.HangNamDueDate:
                        var yearSchedule = Json2.ParseAs<YearScheduleOutOfDate>(timeJob.ScheduleConfigOutOfDate);
                        var dayOfYear = new DateTime(DateTime.Now.Year, yearSchedule.FromMonth, yearSchedule.FromDayOfMonth).DayOfYear;
                        JobManager.AddJob(job, s => WithName(s, docTypeId).ToRunEvery(1).Years().On(dayOfYear).At(yearSchedule.FromHour, yearSchedule.FromMinute));
                        break;
                    default:
                        break;

                }
            }
        }

        public void RunSchedule(IEnumerable<DocTypeTimeJob> timeJobs)
		{
			foreach (var timeJob in timeJobs)
			{
				RunSchedule(new[] { timeJob.DocTypeId }, timeJob);
			}
		}

        public void RunScheduleDueTime(IEnumerable<DocTypeTimeJob> timeJobs)
        {
            foreach(var timeJob in timeJobs)
            {
                RunScheduleDueTime(new[] { timeJob.DocTypeId }, timeJob);
            }
        }

        public void RunScheduleOutOfTime(IEnumerable<DocTypeTimeJob> timeJobs)
        {
            foreach(var timeJob in timeJobs)
            {
                RunScheduleOutOfTime(new[] { timeJob.DocTypeId }, timeJob);
            }
        }

		public void StopSchedule(Guid docTypeId)
		{
			JobManager.RemoveJob(docTypeId.ToString());
		}

		private Schedule WithName(Schedule s, Guid docTypeId)
		{
			return s.WithName(docTypeId.ToString()).NonReentrant();
		}
	}
}