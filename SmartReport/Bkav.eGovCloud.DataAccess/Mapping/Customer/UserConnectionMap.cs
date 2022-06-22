using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserConnectionMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 230414
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng UserConnection trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserConnectionMapMySql : EntityTypeConfiguration<UserConnection>
    {
        /// <summary>
        /// Mapping
        /// </summary>
        public UserConnectionMapMySql()
        {
            ToTable("user_connection");
            Property(p => p.UserConnectionId).HasMaxLength(64).HasColumnType("varchar").IsRequired();
            Property(p => p.DateCreated).HasColumnType("datetime");
            Property(p => p.UserId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserConnectionSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 230414
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng UserConnection trong CSDL sql server
    /// </summary>
    [ComVisible(false)]
    public class UserConnectionMapSqlServer : EntityTypeConfiguration<UserConnection>
    {
        /// <summary>
        /// Mapping
        /// </summary>
        public UserConnectionMapSqlServer()
        {
            ToTable("user_connection");
            HasKey(p => p.UserConnectionId);
            Property(p => p.UserConnectionId).HasMaxLength(64).HasColumnType("varchar").IsRequired();
            Property(p => p.DateCreated).HasColumnType("datetime");
            Property(p => p.UserId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserConnectionOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 230414
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng UserConnection trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class UserConnectionMapOracle : EntityTypeConfiguration<UserConnection>
    {
        /// <summary>
        /// Mapping
        /// </summary>
        public UserConnectionMapOracle()
        {
        }
    }
}
