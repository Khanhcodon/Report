using System;
using System.Collections.Generic;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimerTemplateBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : BLL tương ứng với bảng TimerTemplate trong CSDL
    /// </summary>
    public class TimerTemplateBll : ServiceBase
    {
        private readonly IRepository<TimerTemplate> _timerTemplateRepository;

        ///<summary>
        /// Khởi tạo class <see cref="TimerTemplateBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        public TimerTemplateBll(IDbAdminContext context)
            : base(context)
        {
            _timerTemplateRepository = Context.GetRepository<TimerTemplate>();
        }

        /// <summary>
        /// Lấy ra TimerTemplate theo id
        /// </summary>
        /// <param name="id">Id của TimerTemplate</param>
        /// <returns>Entity TimerTemplate</returns>
        public TimerTemplate Get(int id)
        {
            TimerTemplate result = null;
            if (id > 0)
            {
                result = _timerTemplateRepository.Get(id);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới TimerTemplate
        /// </summary>
        /// <param name="timerTemplate">Entity TimerTemplate</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity TimerTemplate truyền vào bị null</exception>
        public void Create(TimerTemplate timerTemplate)
        {
            if (timerTemplate == null)
            {
                throw new ArgumentNullException("timerTemplate");
            }
            _timerTemplateRepository.Create(timerTemplate);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin TimerTemplate
        /// </summary>
        /// <param name="timerTemplate">Entity TimerTemplate</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity TimerTemplate truyền vào bị null</exception>
        public void Update(TimerTemplate timerTemplate)
        {
            if (timerTemplate == null)
            {
                throw new ArgumentNullException("timerTemplate");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa bỏ một template
        /// </summary>
        /// <param name="timerTemplate">Template</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity TimerJob truyền vào bị null</exception>
        public void Delete(TimerTemplate timerTemplate)
        {
            if (timerTemplate == null)
            {
                throw new ArgumentNullException("timerTemplate");
            }
            _timerTemplateRepository.Delete(timerTemplate);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về danh sách tất cả các mẫu hiện có. Kết quả chỉ đọc
        /// </summary>
        /// <returns>Danh sách mẫu</returns>
        public IEnumerable<TimerTemplate> Gets()
        {
            return _timerTemplateRepository.GetsReadOnly();
        }

        /// <summary>
        /// Trả về danh sách các mẫu đang được active. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TimerTemplate> GetActives()
        {
            return _timerTemplateRepository.GetsReadOnly(t => t.IsActive);
        }
    }
}
