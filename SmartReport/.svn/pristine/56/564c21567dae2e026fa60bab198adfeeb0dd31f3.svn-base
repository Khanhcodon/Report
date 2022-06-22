using System;
using System.Data.Common;
using System.Data.Entity;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BaseDataProvider - public - DAL
    /// Access Modifiers:
    ///     Implement: IDataProvider
    /// Create Date : 250912
    /// Author      : TrungVH
    /// Description : Lớp base cho tất cả các data provider
    /// </summary>
    public abstract class BaseEfDataProvider : IEfDataProvider
    {
        #pragma warning disable 1591
        protected Connection Connection;
        #pragma warning restore 1591

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connection">Entity connection.</param>
        protected BaseEfDataProvider(Connection connection)
        {
            if(connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            Connection = connection;
        }

        #pragma warning disable 1591
        public virtual DbConnection InitDatabase(bool createDatabaseIfNotExist)
        {
            //InitConnectionFactory();
            return SetDatabaseInitializer(createDatabaseIfNotExist);
        }

        public abstract DbConnection SetDatabaseInitializer(bool createDatabaseIfNotExist);

        public void InitConnectionFactory()
        {
            Database.DefaultConnectionFactory = new CustomConnectionFactory(Connection);
        }
    }
}
