using Bkav.eGovCloud.Entities.Admin;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimerTemplateDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ITimerTemplateDal
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : Tương tác với bảng TimerTemplate trong CSDL.
    /// Đây là bảng lưu các lịch trình chạy tự động trên toàn bộ hệ thống và domain con.
    /// </summary>
    public class TimerTemplateDal : DataAccessBase, ITimerTemplateDal
    {
        private readonly IRepository<TimerTemplate> _TimerJobRepository;
        /// <summary>
        /// Khởi tạo class <see cref="TimerTemplateDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public TimerTemplateDal(IDbAdminContext context)
            : base(context)
        {
            _TimerJobRepository = Context.GetRepository<TimerTemplate>();
        }

#pragma warning disable 1591

        public System.Collections.Generic.IEnumerable<TimerTemplate> Gets(System.Linq.Expressions.Expression<System.Func<TimerTemplate, bool>> spec = null)
        {
            return _TimerJobRepository.Find(spec);
        }

        public TimerTemplate Get(int id)
        {
            return _TimerJobRepository.One(id);
        }

        public void Create(TimerTemplate timerTemplate)
        {
            _TimerJobRepository.Create(timerTemplate);
        }

        public void Update(TimerTemplate timerTemplate)
        {
            _TimerJobRepository.Update(timerTemplate);
        }

        public void Delete(TimerTemplate timerTemplate)
        {
            _TimerJobRepository.Delete(timerTemplate);
        }

        public bool Exist(System.Linq.Expressions.Expression<System.Func<TimerTemplate, bool>> spec)
        {
            return _TimerJobRepository.Any(spec);
        }
    }
}
