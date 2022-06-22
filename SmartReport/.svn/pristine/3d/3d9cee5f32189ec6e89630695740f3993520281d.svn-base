using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IStoreCodeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 011012
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng StoreCode trong CSDL
    /// </summary>
    public interface IStoreCodeDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping giữa mẫu đánh số hồ sơ và sổ hồ sơ phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<StoreCode> Gets(Expression<Func<StoreCode, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các mapping giữa mẫu đánh số hồ sơ và sổ hồ sơ phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<StoreCode, TOutput>> projector,
                                           Expression<Func<StoreCode, bool>> spec = null);

        /// <summary>
        /// Tạo mới mapping giữa loại hồ sơ và sổ hồ sơ
        /// </summary>
        /// <param name="storeCode">Entity StoreCode</param>
        void Create(StoreCode storeCode);

        /// <summary>
        /// Tạo mới nhiều mapping giữa mẫu đánh số và sổ hồ sơ
        /// </summary>
        /// <param name="storeCodes">Danh sách entity StoreCode</param>
        void Create(IEnumerable<StoreCode> storeCodes);

        /// <summary>
        /// Xóa mapping giữa mẫu đánh số và sổ hồ sơ
        /// </summary>
        /// <param name="storeCode">Entity StoreCode</param>
        void Delete(StoreCode storeCode);

        /// <summary>
        /// Xóa nhiều mapping giữa mẫu đánh số và sổ hồ sơ
        /// </summary>
        /// <param name="storeCodes">Danh sách entity StoreCodes</param>
        void Delete(IEnumerable<StoreCode> storeCodes);
    }
}
