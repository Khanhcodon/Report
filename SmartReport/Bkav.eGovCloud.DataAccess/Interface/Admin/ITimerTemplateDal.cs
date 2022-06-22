using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : ITimerTemplateDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng TimerTemplate trong CSDL
    /// </summary>
    public interface ITimerTemplateDal
    {
        /// <summary>
        /// Lấy ra tất cả các TimerTemplate phù hợp với điều kiện truyền vào, nếu điều kiện bằng null sẽ lấy ra tất cả TimerTemplate
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách TimerTemplate</returns>
        IEnumerable<TimerTemplate> Gets(Expression<Func<TimerTemplate, bool>> spec = null);

        /// <summary>
        /// Lấy ra TimerTemplate theo id
        /// </summary>
        /// <param name="id">Id TimerTemplate</param>
        /// <returns>Entity TimerTemplate</returns>
        TimerTemplate Get(int id);

        /// <summary>
        /// Tạo mới TimerTemplate
        /// </summary>
        /// <param name="timerTemplate">Entity TimerTemplate</param>
        void Create(TimerTemplate timerTemplate);

        /// <summary>
        /// Cập nhật thông tin TimerTemplate
        /// </summary>
        /// <param name="timerTemplate">Entity TimerTemplate</param>
        void Update(TimerTemplate timerTemplate);

        /// <summary>
        /// Xóa TimerTemplate
        /// </summary>
        /// <param name="timerTemplate">Entity TimerTemplate</param>
        void Delete(TimerTemplate timerTemplate);

        /// <summary>
        /// Kiểm tra sự tồn tại của TimerTemplate phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 TimerTemplate phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<TimerTemplate, bool>> spec);
    }
}
