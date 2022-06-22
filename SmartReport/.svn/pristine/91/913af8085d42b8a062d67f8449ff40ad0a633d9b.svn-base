using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDoctypeFormExfieldDal
    {
        /// <summary> Tienbv 081112
        /// Thêm quan hệ giữa doctype - form - exfield.
        /// </summary>
        /// <param name="exfieldIds">The exfield ids.</param>
        /// <param name="formid">The form id.</param>
        /// <param name="doctypeId">The doctype id.</param>
        void Add(IEnumerable<Guid> exfieldIds, Guid formid, Guid doctypeId);

        /// <summary> Tienbv 081112
        /// Lấy ra danh sách các record quan hệ giữa doctype - form - extendfield theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        IEnumerable<DoctypeFormExtendfield> Gets(Expression<Func<DoctypeFormExtendfield, bool>> spec);
 
        /// <summary> Tienbv 091112
        /// <para></para> Xóa các extend field trong form.
        /// <para></para> Sử dụng khi xóa một mẫu form chưa được sử dụng.
        /// </summary>
        /// <param name="formId">The form id.</param>
        void DeleteExfields(Guid formId);
    }
}
