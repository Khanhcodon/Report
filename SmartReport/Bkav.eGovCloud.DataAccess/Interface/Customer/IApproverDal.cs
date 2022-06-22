using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : IApproverDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 230113
    /// <para></para> Author      : TienBV
    /// <para></para> Description : DAL tương ứng với bảng Approver trong CSDL
    /// </summary>
    public interface IApproverDal
    {
        /// <summary>
        /// Trả về danh sách các ý kiến ký duyệt theo điều kiện kỹ thuật truyền vào.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        IEnumerable<Approver> Gets(Expression<Func<Approver, bool>> spec = null);

        /// <summary>
        /// Thêm ý kiến ký duyệt
        /// </summary>
        /// <param name="entity">The entity</param>
        void Create(Approver entity);

        /// <summary>
        /// Cập nhật ý kiến ký duyệt
        /// </summary>
        /// <param name="entity"></param>
        void Update(Approver entity);

        /// <summary>
        /// Lấy ý kiến ký duyệt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Approver Get(int id);

        /// <summary>
        /// Kiểm tra đã có ý kiến ký duyệt  chưa.
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <param name="docId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Exist(int docCopyId, Guid docId, int userId);

        /// <summary>
        /// Trả về nội dung ký duyệt của cán bộ đối với hồ sơ hiện tại
        /// </summary>
        /// <param name="docCopyId">Document Copy Id</param>
        /// <param name="userId">UserId hiện tại</param>
        /// <returns>Ký duyệt</returns>
        Approver Get(int docCopyId, int userId);
    }
}
