using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IAccountDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Modify Date : 140313
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng AttachmentDetail trong CSDL
    /// </summary>
    public interface IAttachmentDetailDal
    {
        /// <summary>
        /// Lấy ra danh sách file đính kèm
        /// </summary>
        /// <returns>Danh sách file đính kèm</returns>
        IEnumerable<AttachmentDetail> Gets(Expression<Func<AttachmentDetail, bool>> spec = null);

        /// <summary>
        /// Lấy ra file đính kèm
        /// </summary>
        /// <param name="id">Id của file đính kèm</param>
        /// <returns>Entity file đính kèm</returns>
        AttachmentDetail Get(int id);

        /// <summary>
        /// Tạo mới file đính kèm
        /// </summary>
        /// <param name="attachment">Entity file đính kèm</param>
        void Create(AttachmentDetail attachment);

        /// <summary>
        /// Cập nhật thông tin file đính kèm
        /// </summary>
        /// <param name="attachment">Entity file đính kèm</param>
        void Update(AttachmentDetail attachment);

        /// <summary>
        /// Xóa file đính kèm
        /// </summary>
        /// <param name="attachment">Entity file đính kèm</param>
        void Delete(AttachmentDetail attachment);

        /// <summary>
        /// Kiểm tra sự tồn tại của file đính kèm phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 file đính kèm phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<AttachmentDetail, bool>> spec);
    }
}
