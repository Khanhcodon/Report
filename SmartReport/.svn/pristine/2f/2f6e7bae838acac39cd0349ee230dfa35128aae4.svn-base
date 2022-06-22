using System.Data.Common;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IEfDataProvider - public - DAL
    /// Access Modifiers: 
    ///     Implement: IDataProvider
    /// Create Date : 260912
    /// Author      : TrungVH
    /// Description : Interface cơ sở của 1 data provider
    /// </summary>
    public interface IEfDataProvider : IDataProvider
    {
        /// <summary>
        /// Khởi tạo connection factory
        /// </summary>
        void InitConnectionFactory();

        /// <summary>
        /// Thiết lập các thông số khởi tạo cơ sở dữ liệu
        /// </summary>
        /// <param name="createDatabaseIfNotExist">True: Tạo mới 1 database nếu database đó không tồn tại và ngược lại: false</param>
        /// <returns>DbConnection</returns>
        DbConnection SetDatabaseInitializer(bool createDatabaseIfNotExist);
    }
}
