using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IAttachmentDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Modify Date: 140313
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Attachment trong CSDL
    /// </summary>
    public interface IAttachmentDal
    {
        /// <summary>
        /// Lấy ra danh sách file đính kèm
        /// </summary>
        /// <returns>Danh sách file đính kèm</returns>
        IEnumerable<Attachment> Gets(Expression<Func<Attachment, bool>> spec = null);

        /// <summary>
        /// Lấy ra file đính kèm
        /// </summary>
        /// <param name="id">Id của file đính kèm</param>
        /// <returns>Entity file đính kèm</returns>
        Attachment Get(int id);

        /// <summary>
        /// Tạo mới file đính kèm
        /// </summary>
        /// <param name="attachment">Entity file đính kèm</param>
        void Create(Attachment attachment);

        /// <summary>
        /// Cập nhật thông tin file đính kèm
        /// </summary>
        /// <param name="attachment">Entity file đính kèm</param>
        void Update(Attachment attachment);

        /// <summary>
        /// Xóa file đính kèm
        /// </summary>
        /// <param name="attachment">Entity file đính kèm</param>
        void Delete(Attachment attachment);
    }
}
