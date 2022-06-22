using System;
using System.Collections.Generic;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para></para> BSO - Phòng 2 - eGov
    /// <para></para> Project: eGov Cloud - v1.0
    /// <para></para> Access Level(Class/Struct/Interface) : ApproverBll - public - BLL
    /// <para></para> Access Modifiers:
    /// <para></para>     * Inherit   : [Class Name]
    /// <para></para>     * Implement : [Inteface Name], [Inteface Name], ...
    /// <para></para>
    /// <para></para> Create Date : 230113
    /// <para></para> Author      : TienBV
    /// <para></para> Description : Cung cấp các api quản lý ký duyệt.
    /// </summary>
    public class ApproverBll : ServiceBase
    {
        private readonly IRepository<Approver> _approverRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        public ApproverBll(IDbCustomerContext context)
            : base(context)
        {
            _approverRepository = Context.GetRepository<Approver>();
        }

        /// <summary>
        /// Trả về danh sách các ý kiến ký duyệt theo document copy. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="documentCopyId">The document copy id.</param>
        /// <returns></returns>
        public IEnumerable<Approver> Gets(int documentCopyId)
        {
            return _approverRepository.GetsReadOnly(a => a.DocumentCopyId == documentCopyId);
        }

        /// <summary>
        /// Thêm ý kiến ký duyệt
        /// </summary>
        /// <param name="entity">The entity</param>
        public void Create(Approver entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!_approverRepository.Exist(a => a.DocumentCopyId == entity.DocumentCopyId && a.DocumentId == entity.DocumentId && a.UserSendId == entity.UserSendId))
            {
                _approverRepository.Create(entity);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Xóa ý kiến ký duyệt
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Approver entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _approverRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa ý kiến ký duyệt
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _approverRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật ý kiến ký duyệt
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Approver entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ý kiến ký duyệt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Approver Get(int id)
        {
            return _approverRepository.Get(id);
        }

        /// <summary>
        /// Trả về nội dung ký duyệt của cán bộ đối với hồ sơ hiện tại
        /// </summary>
        /// <param name="docCopyId">Document Copy Id</param>
        /// <param name="userId">UserId hiện tại</param>
        /// <returns>Ký duyệt</returns>
        public Approver Get(int docCopyId, int userId)
        {
            return _approverRepository.Get(false, a => a.DocumentCopyId == docCopyId && a.UserSendId == userId);
        }

        /// <summary>
        /// Trả về tất cả các ký duyệt của văn bản
        /// </summary>
        /// <param name="docId">Document Id</param>
        /// <returns></returns>
        public IEnumerable<Approver> Gets(Guid docId)
        {
            return _approverRepository.Gets(false, a => a.DocumentId.Equals(docId) && !a.IsDraft);
        }
    }
}
