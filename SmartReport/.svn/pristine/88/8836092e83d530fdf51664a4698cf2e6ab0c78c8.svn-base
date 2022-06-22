using System.Data.Common;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDataProvider - public - DAL
    /// Access Modifiers: 
    /// Create Date : 250912
    /// Author      : TrungVH
    /// Description : Interface cơ sở của 1 data provider
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Khởi tạo cơ sở dữ liệu
        /// </summary>
        /// <param name="createDatabaseIfNotExist">True: Tạo mới 1 database nếu database đó không tồn tại và ngược lại: false</param>
        /// <returns>DbConnection</returns>
        DbConnection InitDatabase(bool createDatabaseIfNotExist);
    }
}
