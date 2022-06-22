using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para> Bkav Corp. - BSO - eGov - eGate team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : IFileLocationDal - public - Dal </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 060313</para>
    /// <para> Author : trungvh@bkav.com </para>
    /// <para> DAL tương ứng với bảng FileLocation trong CSDL. </para>
    /// </summary>
    public interface IFileLocationDal
    {
        /// <summary>
        /// <para> Lấy các vị trí lưu file theo điều kiện truyền vào.</para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách vị trí lưu file</returns>
        IEnumerable<FileLocation> Gets(Expression<Func<FileLocation, bool>> spec);

        /// <summary>
        /// Lấy ra vị trí lưu file theo id
        /// </summary>
        /// <param name="id">Id của vị trí lưu file</param>
        /// <returns>Entity lệ phí</returns>
        FileLocation Get(int id);

        /// <summary>
        /// <para> Thêm vị trí lư file</para>
        /// </summary>
        /// <param name="entity">entity</param>
        void Create(FileLocation entity);

        /// <summary>
        /// <para> Cập nhật vị trí lưu file.</para>
        /// </summary>
        /// <param name="entity">entity</param>
        void Update(FileLocation entity);

        /// <summary>
        /// <para> Xóa vị trí lưu file.</para>
        /// </summary>
        /// <param name="entity">entity</param>
        void Delete(FileLocation entity);
    }
}
