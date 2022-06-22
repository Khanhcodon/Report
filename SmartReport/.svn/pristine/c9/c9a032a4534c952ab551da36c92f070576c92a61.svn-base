using System;
using Bkav.eGovCloud.Entities.Admin;
using DatabaseType = Bkav.eGovCloud.Entities.DatabaseType;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DataProviderManager - public - DAL
    /// Access Modifiers:
    /// Create Date : 260912
    /// Author      : TrungVH
    /// Description : Class quản lý việc lấy ra các dataprovider
    /// </summary>
    public static class DataProviderManager
    {
        /// <summary>
        /// Lấy ra data provider tương ứng với connection
        /// </summary>
        /// <param name="connection">Entity connection</param>
        /// <returns>Data provider tương ứng</returns>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ nếu connection là null</exception>
        public static IDataProvider LoadDataProvider(Connection connection)
        {
            IDataProvider result = null;
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            switch (connection.DatabaseTypeIdInEnum)
            {
                case DatabaseType.MySql:
                    result = new MySqlDataProvider(connection);
                    break;
                case DatabaseType.SqlServer:
                    result = new SqlServerDataProvider(connection);
                    break;
                case DatabaseType.Oracle:
                    //TODO: Them provider oracle
                    break;
            }
            return result;
        }
    }
}
