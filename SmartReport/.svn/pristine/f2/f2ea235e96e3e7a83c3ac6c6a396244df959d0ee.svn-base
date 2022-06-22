using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Admin.TimerJobSchedules;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimerJobBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : BLL tương ứng với bảng TimerJob trong CSDL
    /// </summary>
    public class TimeJobBll : ServiceBase
    {
        private readonly IRepository<TimeJob> _timerJobRepository;

        ///<summary>
        /// Khởi tạo class <see cref="TimeJobBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        public TimeJobBll(IDbCustomerContext context)
            : base(context)
        {
            _timerJobRepository = Context.GetRepository<TimeJob>();
        }

        /// <summary>
        /// Lấy ra TimerJob theo id
        /// </summary>
        /// <param name="timerJob">Id của TimerJob</param>
        /// <returns>Entity TimerJob</returns>
        public TimeJob Get(int timerJob)
        {
            TimeJob result = null;
            if (timerJob > 0)
            {
                result = _timerJobRepository.Get(timerJob);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới TimerJob
        /// </summary>
        /// <param name="timerJob">Entity TimerJob</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity TimerJob truyền vào bị null</exception>
        public void Create(TimeJob timerJob)
        {
            if (timerJob == null)
            {
                throw new ArgumentNullException("timeJob");
            }

            var nextTime = GetNextRunTime(timerJob.ScheduleType, timerJob.ScheduleConfig, timerJob.DateLastJobRun ?? DateTime.Now);
            timerJob.DateNextJobStartBefore = nextTime.NextStartTimeBefore;
            timerJob.DateNextJobStartAfter = nextTime.NextStartTimeAfter;
            _timerJobRepository.Create(timerJob);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin TimerJob
        /// </summary>
        /// <param name="timerJob">Entity TimerJob</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity TimerJob truyền vào bị null</exception>
        public void Update(TimeJob timerJob)
        {
            if (timerJob == null)
            {
                throw new ArgumentNullException("timeJob");
            }

            var nextTime = GetNextRunTime(timerJob.ScheduleType, timerJob.ScheduleConfig, timerJob.DateLastJobRun ?? DateTime.Now);
            timerJob.DateNextJobStartBefore = nextTime.NextStartTimeBefore;
            timerJob.DateNextJobStartAfter = nextTime.NextStartTimeAfter;
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về danh sách tất cả các timerJob đang tồn tại trên hệ thống. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<TimeJob> GetsReadOnly(Expression<Func<TimeJob, bool>> spec = null)
        {
            var sort = Context.Filters.CreateSort<TimeJob>(true, "IsActive");
            return _timerJobRepository.GetsReadOnly(spec, sort);
        }

        /// <summary>
        /// Trả về danh sách tất cả các timerJob đang tồn tại trên hệ thống
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<TimeJob> Gets(Expression<Func<TimeJob, bool>> spec = null)
        {
            return _timerJobRepository.Gets(false, spec);
        }

        /// <summary>
        /// Xóa bỏ một lịch trình
        /// </summary>
        /// <param name="timer">Lịch trình</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity TimerJob truyền vào bị null</exception>
        public void Delete(TimeJob timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException("timer");
            }
            _timerJobRepository.Delete(timer);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về danh sách các timer job trong thời điểm được active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TimeJob> GetAllActive()
        {
            var current = DateTime.Now;

            // TienBV
            // Lấy luôn cả trường hợp ngày hiện tại lớn hơn thời điểm cuối dc chạy (vì nguyên nhân nào đó),
            // Nếu không lấy job đó sẽ bị dừng mãi mãi
            var result = _timerJobRepository.GetsReadOnly(t => t.IsActive
                                                        && ((t.DateNextJobStartBefore <= current && t.DateNextJobStartAfter >= current)
                                                            || t.DateNextJobStartAfter <= current));

            return result;

        }

        /// <summary>
        /// Bỏ kích hoạt job
        /// </summary>
        /// <param name="id"></param>
        public void UnActive(int id)
        {
            if (id <= 0)
            {
                return;
            }

            var timer = Get(id);
            if (timer == null)
            {
                return;
            }

            timer.IsActive = false;

            Update(timer);
        }

        /// <summary>
        /// Cập nhật trạng thái đã chạy cho timer job
        /// </summary>
        /// <param name="id">Timer job id</param>
        public void SetJobSuccess(int id)
        {
            var timer = Get(id);
            SetJobSuccess(timer);
        }

        /// <summary>
        /// Cập nhật trạng thái đã chạy cho timer job
        /// </summary>
        /// <param name="timeJob">Timer job</param>
        public void SetJobSuccess(TimeJob timeJob)
        {
            if (timeJob == null)
            {
                throw new ArgumentNullException("timeJob");
            }

            timeJob.DateLastJobRun = DateTime.Now;
            timeJob.IsRunning = false;
            Update(timeJob);
        }

        /// <summary>
        /// Cập nhật trạng thái Job đang chạy
        /// </summary>
        /// <param name="id">Job id</param>
        public void SetJobRunning(int id)
        {
            var timer = Get(id);
            SetJobRunning(timer);
        }

        /// <summary>
        /// Cập nhật trạng thái Job đang chạy
        /// </summary>
        /// <param name="timeJob">Job</param>
        public void SetJobRunning(TimeJob timeJob)
        {
            if (timeJob == null)
            {
                throw new ArgumentNullException("timeJob");
            }

            timeJob.IsRunning = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật trạng thái khi time job bị lỗi
        /// </summary>
        /// <param name="id">Id của time job</param>
        public void SetJobRunFail(int id)
        {
            var timer = Get(id);
            SetJobRunFail(timer);
        }

        /// <summary>
        /// Cập nhật trạng thái khi time job bị lỗi
        /// </summary>
        /// <param name="timeJob">Đối tượng time job</param>
        public void SetJobRunFail(TimeJob timeJob)
        {
            if (timeJob == null)
            {
                throw new ArgumentNullException("timeJob");
            }

            timeJob.IsRunning = false;
            Context.SaveChanges();
        }

        #region Private Methods

        private NextStartTime GetNextRunTime(int scheduleType, string scheduleConfig, DateTime lastRunTime)
        {
            NextStartTime result = null;
            switch (scheduleType)
            {
                case (int)ScheduleType.HangPhut:
                    result = Json2.ParseAs<MinutesSchedule>(scheduleConfig).GetNextStartTime(lastRunTime);
                    break;
                case (int)ScheduleType.HangGio:
                    result = Json2.ParseAs<HourlySchedule>(scheduleConfig).GetNextStartTime(lastRunTime);
                    break;
                case (int)ScheduleType.HangNgay:
                    result = Json2.ParseAs<DailySchedule>(scheduleConfig).GetNextStartTime(lastRunTime);
                    break;
                case (int)ScheduleType.HangTuan:
                    result = Json2.ParseAs<WeeklySchedule>(scheduleConfig).GetNextStartTime(lastRunTime);
                    break;
                case (int)ScheduleType.HangThang:
                    result = Json2.ParseAs<MonthlySchedule>(scheduleConfig).GetNextStartTime(lastRunTime);
                    break;
                case (int)ScheduleType.HangNam:
                    result = Json2.ParseAs<YearSchedule>(scheduleConfig).GetNextStartTime(lastRunTime);
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }

        #endregion

    }
}
