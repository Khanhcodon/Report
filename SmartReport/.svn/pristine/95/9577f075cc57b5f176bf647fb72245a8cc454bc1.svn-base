using Bkav.eGovCloud.Entities.Admin;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimerJobDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ITimerJobDal
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : Tương tác với bảng TimerJob trong CSDL.
    /// Đây là bảng lưu các lịch trình chạy tự động trên toàn bộ hệ thống và domain con.
    /// </summary>
    public class TimerJobDal : DataAccessBase, ITimerJobDal
    {
        private readonly IRepository<TimerJob> _TimerJobRepository;
        /// <summary>
        /// Khởi tạo class <see cref="AccountDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public TimerJobDal(IDbAdminContext context)
            : base(context)
        {
            _TimerJobRepository = Context.GetRepository<TimerJob>();
        }

#pragma warning disable 1591

        public System.Collections.Generic.IEnumerable<TimerJob> Gets(System.Linq.Expressions.Expression<System.Func<TimerJob, bool>> spec = null)
        {
            return _TimerJobRepository.Find(spec);
        }

        public TimerJob Get(int id)
        {
            return _TimerJobRepository.One(id);
        }

        public void Create(TimerJob TimerJob)
        {
            _TimerJobRepository.Create(TimerJob);
        }

        public void Update(TimerJob TimerJob)
        {
            _TimerJobRepository.Update(TimerJob);
        }

        public void Delete(TimerJob TimerJob)
        {
            _TimerJobRepository.Delete(TimerJob);
        }

        public bool Exist(System.Linq.Expressions.Expression<System.Func<TimerJob, bool>> spec)
        {
            return _TimerJobRepository.Any(spec);
        }

        public IEnumerable<TimerJob> GetAllActive()
        {
            var date = DateTime.Now;
            return _TimerJobRepository
                    .Find(
                        t => t.IsActive &&
                        !t.IsRunning &&
                        ((t.ScheduleType > 1 && t.DateNextJobStartAfter <= date && t.DateNextJobStartBefore >= date) ||
                        (t.ScheduleType == 1 && t.DateNextJobStartAfter <= date)));
        }
    }
}
