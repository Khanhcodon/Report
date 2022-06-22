using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Admin.TimerJobSchedules;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Business.Admin
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
    public class TimerJobBll : ServiceBase
    {
        private readonly IRepository<TimerJob> _timerJobRepository;

        ///<summary>
        /// Khởi tạo class <see cref="TimerJobBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        public TimerJobBll(IDbAdminContext context)
            : base(context)
        {
            _timerJobRepository = Context.GetRepository<TimerJob>();
        }

        /// <summary>
        /// Lấy ra TimerJob theo id
        /// </summary>
        /// <param name="timerJob">Id của TimerJob</param>
        /// <returns>Entity TimerJob</returns>
        public TimerJob Get(int timerJob)
        {
            TimerJob result = null;
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
        public void Create(TimerJob timerJob)
        {
            if (timerJob == null)
            {
                throw new ArgumentNullException("timerJob");
            }
            var type = timerJob.ScheduleTypeEnum;
            var nextTime = GetNextRunTime(type, timerJob.ScheduleConfig, timerJob.DateLastJobRun ?? DateTime.Now);
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
        public void Update(TimerJob timerJob)
        {
            if (timerJob == null)
            {
                throw new ArgumentNullException("timerJob");
            }
            var type = timerJob.ScheduleTypeEnum;
            var nextTime = GetNextRunTime(type, timerJob.ScheduleConfig, timerJob.DateLastJobRun ?? DateTime.Now);
            timerJob.DateNextJobStartBefore = nextTime.NextStartTimeBefore;
            timerJob.DateNextJobStartAfter = nextTime.NextStartTimeAfter;
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về danh sách tất cả các timerJob đang tồn tại trên hệ thống theo domain
        /// </summary>
        /// <param name="domainId">Domain</param>
        /// <param name="type">Timer Job Type</param>
        /// <returns>Danh sách timer job</returns>
        public IEnumerable<TimerJob> Gets(int domainId, TimerJobType type)
        {
            return _timerJobRepository.GetsReadOnly(t => (t.DomainId == domainId) && (t.TimerJobType == (int)type));
        }

        /// <summary>
        /// Xóa bỏ một lịch trình
        /// </summary>
        /// <param name="timer">Lịch trình</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity TimerJob truyền vào bị null</exception>
        public void Delete(TimerJob timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException("timer");
            }
            _timerJobRepository.Delete(timer);
        }

        /// <summary>
        /// Trả về danh sách các timer job trong thời điểm được active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TimerJob> GetAllActive()
        {
            var date = DateTime.Now;
            return _timerJobRepository
                .Gets(false,
                    t => t.IsActive &&
                         !t.IsRunning &&
                         ((t.ScheduleType > 1 && t.DateNextJobStartBefore <= date && t.DateNextJobStartAfter >= date) ||
                          (t.ScheduleType == 1 && t.DateNextJobStartAfter >= date)));
        }

        /// <summary>
        /// Cập nhật trạng thái đã chạy cho timer job
        /// </summary>
        /// <param name="id">Timer job id</param>
        public void SetJobSuccess(int id)
        {
            var timer = Get(id);
            timer.DateLastJobRun = DateTime.Now;
            timer.IsRunning = false;
            Update(timer);
        }

        /// <summary>
        /// Cập nhật trạng thái Job đang chạy
        /// </summary>
        /// <param name="id">Job id</param>
        public void SetJobRunning(int id)
        {
            var timer = Get(id);
            timer.IsRunning = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void SetJobRunFail(int id)
        {
            var timer = Get(id);
            timer.IsRunning = false;
            Context.SaveChanges();
        }

        #region Private Methods

        private NextStartTime GetNextRunTime(ScheduleType type, string json, DateTime lastRunTime)
        {
            NextStartTime result = null;
            switch (type)
            {
                case ScheduleType.HangPhut:
                    result = Json2.ParseAs<MinutesSchedule>(json).GetNextStartTime(lastRunTime);
                    break;
                case ScheduleType.HangGio:
                    result = Json2.ParseAs<HourlySchedule>(json).GetNextStartTime(lastRunTime);
                    break;
                case ScheduleType.HangNgay:
                    result = Json2.ParseAs<DailySchedule>(json).GetNextStartTime(lastRunTime);
                    break;
                case ScheduleType.HangTuan:
                    result = Json2.ParseAs<WeeklySchedule>(json).GetNextStartTime(lastRunTime);
                    break;
                case ScheduleType.HangThang:
                    result = Json2.ParseAs<MonthlySchedule>(json).GetNextStartTime(lastRunTime);
                    break;
            }
            return result;
        }

        #endregion

    }
}
