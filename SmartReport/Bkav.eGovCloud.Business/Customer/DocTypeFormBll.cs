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
    /// <para>Create Date : 011013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng FormGroup trong CSDL</para>
    /// </summary>
    public class DocTypeFormBll : ServiceBase
    {
        private readonly IRepository<DocTypeForm> _doctypeformRepository;

        ///<summary>
        /// Khởi tạo class <see cref="IncreaseBll"/>.
        ///</summary>
        ///<param name="context">context</param>
        public DocTypeFormBll(IDbCustomerContext context)
            : base(context)
        {
            _doctypeformRepository = Context.GetRepository<DocTypeForm>();
        }

        /// <summary>
        /// Lấy ra danh sách các loại văn bản, biểu mẫu theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        public IEnumerable<DocTypeForm> Gets(Expression<Func<DocTypeForm, bool>> spec)
        {
            return _doctypeformRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra danh sách các loại văn bản, biểu mẫu theo điều kiện kỹ thuật truyền vào.
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec">The spec.</param>
        public IEnumerable<T> GetsAs<T>(Expression<Func<DocTypeForm, T>> projector, Expression<Func<DocTypeForm, bool>> spec)
        {
            return _doctypeformRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<DocTypeForm, bool>> spec)
        {
            return _doctypeformRepository.Exist(spec);
        }

        /// <summary>
        /// Tạo mới loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeform">Entity nhóm biểu mẫu</param>
        public void Create(DocTypeForm doctypeform)
        {
            if (doctypeform == null)
            {
                throw new ArgumentNullException("doctypeform");
            }
            _doctypeformRepository.Create(doctypeform);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeforms">Entity nhóm biểu mẫu</param>
        public void Create(IEnumerable<DocTypeForm> doctypeforms)
        {
            if (doctypeforms == null || !doctypeforms.Any())
            {
                throw new ArgumentNullException("doctypeform");
            }

            foreach (var doctypeform in doctypeforms)
            {
                _doctypeformRepository.Create(doctypeform);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeform">Entity nhóm biểu mẫu</param>
        public void Update(DocTypeForm doctypeform)
        {
            if (doctypeform == null)
            {
                throw new ArgumentNullException("doctypeform");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeform">Thực thể nhóm biểu mẫu</param>
        public void Delete(DocTypeForm doctypeform)
        {
            if (doctypeform == null)
            {
                throw new ArgumentNullException("doctypeform");
            }
            _doctypeformRepository.Delete(doctypeform);
            Context.SaveChanges();
        }

        /// <summary>
        /// Hopcv:120814
        /// Xóa nhiều loại văn bản, biểu mẫu
        /// </summary>
        /// <param name="doctypeForms"> Danh sách thực thể nhóm biểu mẫu</param>
        public void Delete(IEnumerable<DocTypeForm> doctypeForms)
        {
            if (doctypeForms == null || !doctypeForms.Any())
            {
                throw new ArgumentNullException("doctypeform");
            }
            foreach (var doctypeForm in doctypeForms)
            {
                _doctypeformRepository.Delete(doctypeForm);
            }
        }

        /// <summary>
        /// Trả về danh sách các mẫu form của doctype tương ứng.
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="docTypeId">Doctype Id</param>
        /// <returns></returns>
        public IEnumerable<T> GetForms<T>(Expression<Func<DocTypeForm, T>> projector, Guid docTypeId)
        {
            if (Guid.Empty.Equals(docTypeId))
            {
                throw new ArgumentException("DoctypeId không hợp lệ: empty");
            }

            return _doctypeformRepository.GetsAs(projector, df => df.DocTypeId == docTypeId && df.IsActive);
        }

        /// <summary>
        /// Trả về doctype form theo id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public DocTypeForm Get(int id)
        {
            return _doctypeformRepository.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <param name="isPrimary"></param>
        /// <returns></returns>
        public DocTypeForm Get(Guid doctypeId, bool isPrimary)
        {
            return _doctypeformRepository.Get(true, x => x.DocTypeId == doctypeId && (x.IsPrimary == isPrimary || x.IsPrimary == false) && x.IsActive);
        }

        /// <summary>
        /// Trả về doctype form theo id
        /// </summary>
        /// <param name="formid">Form Id</param>
        /// <param name="docTypeId">DocType Id</param>
        /// <returns></returns>
        public DocTypeForm Get(Guid formid, Guid docTypeId)
        {
            return _doctypeformRepository.Get(false, d => d.FormId == formid && d.DocTypeId == docTypeId);
        }

        /// <summary>
        /// Trả về danh sách các mẫu form của doctype tương ứng.
        /// </summary>
        /// <param name="docTypeId">Doctype Id</param>
        /// <returns></returns>
        public IEnumerable<DocTypeForm> GetsByDoctypeId(Guid docTypeId)
        {
            if (Guid.Empty.Equals(docTypeId))
            {
                throw new ArgumentException("DoctypeId không hợp lệ: empty");
            }
            return _doctypeformRepository.Gets(true, df => df.DocTypeId == docTypeId && df.IsActive);
        }
    }
}
