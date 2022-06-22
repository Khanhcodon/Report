using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DatabaseTypeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DatabaseType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DatabaseType trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DatabaseTypeMapMySql : EntityTypeConfiguration<DatabaseType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DatabaseTypeMapMySql()
        {
            ToTable("databasetype");
            Property(p => p.DatabaseTypeName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DatabaseTypeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DatabaseType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DatabaseType trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DatabaseTypeMapSqlServer : EntityTypeConfiguration<DatabaseType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DatabaseTypeMapSqlServer()
        {
            ToTable("databasetype");
            Property(p => p.DatabaseTypeName).IsRequired().HasMaxLength(128);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DatabaseTypeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DatabaseType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DatabaseType trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DatabaseTypeMapOracle : EntityTypeConfiguration<DatabaseType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DatabaseTypeMapOracle()
        {

        }
    }
}
