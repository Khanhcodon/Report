using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDocumentContentDetailDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 240214
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocumentContentDetail trong CSDL
    /// </summary>
    public interface IDocumentContentDetailDal
    {
        /// <summary>
        /// Lấy ra danh sách các phiên bản nội dung vb,hs
        /// </summary>
        /// <returns>Danh sách các phiên bản nội dung vb,hs</returns>
        IEnumerable<DocumentContentDetail> Gets(Expression<Func<DocumentContentDetail, bool>> spec = null);

        /// <summary>
        /// Lấy ra phiên bản nội dung vb,hs
        /// </summary>
        /// <param name="id">Id của phiên bản nội dung vb,hs</param>
        /// <returns>Entity phiên bản nội dung vb,hs</returns>
        DocumentContentDetail Get(int id);

        /// <summary>
        /// Tạo mới phiên bản nội dung vb,hs
        /// </summary>
        /// <param name="documentContentDetail">Entity phiên bản nội dung vb,hs</param>
        void Create(DocumentContentDetail documentContentDetail);

        /// <summary>
        /// Cập nhật thông tin phiên bản nội dung vb,hs
        /// </summary>
        /// <param name="documentContentDetail">Entity phiên bản nội dung vb,hs</param>
        void Update(DocumentContentDetail documentContentDetail);

        /// <summary>
        /// Xóa phiên bản nội dung vb,hs
        /// </summary>
        /// <param name="documentContentDetail">Entity phiên bản nội dung vb,hs</param>
        void Delete(DocumentContentDetail documentContentDetail);

        /// <summary>
        /// Kiểm tra sự tồn tại của phiên bản nội dung vb,hs phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 phiên bản nội dung vb,hs phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<DocumentContentDetail, bool>> spec);
    }
}
