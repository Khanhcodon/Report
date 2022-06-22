using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : ITimerJobDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng TimerJob trong CSDL
    /// </summary>
    public interface ITimerJobDal
    {
        /// <summary>
        /// Lấy ra tất cả các TimerJob phù hợp với điều kiện truyền vào, nếu điều kiện bằng null sẽ lấy ra tất cả TimerJob
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách TimerJob</returns>
        IEnumerable<TimerJob> Gets(Expression<Func<TimerJob, bool>> spec = null);

        /// <summary>
        /// Lấy ra TimerJob theo id
        /// </summary>
        /// <param name="id">Id TimerJob</param>
        /// <returns>Entity TimerJob</returns>
        TimerJob Get(int id);

        /// <summary>
        /// Tạo mới TimerJob
        /// </summary>
        /// <param name="timerJob">Entity TimerJob</param>
        void Create(TimerJob timerJob);

        /// <summary>
        /// Cập nhật thông tin TimerJob
        /// </summary>
        /// <param name="timerJob">Entity TimerJob</param>
        void Update(TimerJob timerJob);

        /// <summary>
        /// Xóa TimerJob
        /// </summary>
        /// <param name="timerJob">Entity TimerJob</param>
        void Delete(TimerJob timerJob);

        /// <summary>
        /// Kiểm tra sự tồn tại của TimerJob phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 TimerJob phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<TimerJob, bool>> spec);

        /// <summary>
        /// Trả về dánh cách các job đến thời điểm kích hoạt
        /// </summary>
        /// <returns></returns>
        IEnumerable<TimerJob> GetAllActive();
    }
}
