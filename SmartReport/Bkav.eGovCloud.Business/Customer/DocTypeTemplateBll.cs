using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : FormGroupBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 161015</para>
    /// <para>Author      : TrinhNVd</para>
    /// <para>Description : BLL tương ứng với bảng DoctypeTemplate trong CSDL</para>
    /// </summary>
    public class DoctypeTemplateBll : ServiceBase
    {
        private readonly IRepository<DoctypeTemplate> _doctypeTemplateRepository;

        ///<summary>
        /// Khởi tạo class <see cref="IncreaseBll"/>.
        ///</summary>
        ///<param name="context">context</param>
        public DoctypeTemplateBll(IDbCustomerContext context)
            : base(context)
        {
            _doctypeTemplateRepository = Context.GetRepository<DoctypeTemplate>();
        }

        /// <summary>
        /// Lấy ra danh sách các loại văn bản, biểu mẫu theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="isReadOnly"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<DoctypeTemplate> Gets(bool isReadOnly, Expression<Func<DoctypeTemplate, bool>> spec = null)
        {
            return _doctypeTemplateRepository.Gets(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy ra danh sách các loại văn bản, biểu mẫu theo điều kiện kỹ thuật truyền vào.
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec">The spec.</param>
        public IEnumerable<T> GetsAs<T>(Expression<Func<DoctypeTemplate, T>> projector, Expression<Func<DoctypeTemplate, bool>> spec)
        {
            return _doctypeTemplateRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<DoctypeTemplate, bool>> spec)
        {
            return _doctypeTemplateRepository.Exist(spec);
        }

        /// <summary>
        /// Tạo mới loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeTemplate">Entity nhóm biểu mẫu</param>
        public void Create(DoctypeTemplate doctypeTemplate)
        {
            if (doctypeTemplate == null)
            {
                throw new ArgumentNullException("doctypeTemplate");
            }
            _doctypeTemplateRepository.Create(doctypeTemplate);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeTemplates">Entity nhóm biểu mẫu</param>
        public void Create(IEnumerable<DoctypeTemplate> doctypeTemplates)
        {
            if (doctypeTemplates == null || !doctypeTemplates.Any())
            {
                throw new ArgumentNullException("doctypeTemplates");
            }

            foreach (var doctypeTemplate in doctypeTemplates)
            {
                _doctypeTemplateRepository.Create(doctypeTemplate);
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeTemplate">Entity nhóm biểu mẫu</param>
        public void Update(DoctypeTemplate doctypeTemplate)
        {
            if (doctypeTemplate == null)
            {
                throw new ArgumentNullException("doctypeTemplate");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeTemplate">Thực thể nhóm biểu mẫu</param>
        public void Delete(DoctypeTemplate doctypeTemplate)
        {
            if (doctypeTemplate == null)
            {
                throw new ArgumentNullException("doctypeTemplate");
            }
            _doctypeTemplateRepository.Delete(doctypeTemplate);
            Context.SaveChanges();
        }

        /// <summary>
        /// Hopcv:120814
        /// Xóa nhiều loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeForms"> Danh sách thực thể nhóm biểu mẫu</param>
        public void Delete(IEnumerable<DoctypeTemplate> doctypeForms)
        {
            if (doctypeForms == null || !doctypeForms.Any())
            {
                throw new ArgumentNullException("doctypeTemplate");
            }
            foreach (var doctypeForm in doctypeForms)
            {
                _doctypeTemplateRepository.Delete(doctypeForm);
            }
        }

        /// <summary>
        /// Trả về doctype Template theo id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public DoctypeTemplate Get(int id)
        {
            return _doctypeTemplateRepository.Get(id);
        }

        /// <summary>
        /// Trả về danh sách các mẫu Template của doctype tương ứng.
        /// </summary>
        /// <param name="docTypeId">Doctype Id</param>
        /// <returns></returns>
        public IEnumerable<DoctypeTemplate> GetsByDoctypeId(Guid docTypeId)
        {
            if (Guid.Empty.Equals(docTypeId))
            {
                throw new ArgumentException("DoctypeId không hợp lệ: empty");
            }
            return _doctypeTemplateRepository.Gets(true, dt => dt.DoctypeId == docTypeId);
        }
    }
}
