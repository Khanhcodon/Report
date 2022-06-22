using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserPasswordHistoryMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserPasswordHistory&gt;
    /// Create Date : 050912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserPasswordHistory trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserPasswordHistoryMapMySql : EntityTypeConfiguration<UserPasswordHistory>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public UserPasswordHistoryMapMySql()
        {
            ToTable("userpasswordhistory");
            Property(p => p.Username).HasColumnType("varchar").IsRequired().HasMaxLength(255);
            Property(p => p.UserId).IsRequired();
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary");//.HasMaxLength(16);
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary");//.HasMaxLength(64);
            Property(p => p.CreatedOnDate).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserPasswordHistoryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserPasswordHistory&gt;
    /// Create Date : 050912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserPasswordHistory trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class UserPasswordHistoryMapSqlServer : EntityTypeConfiguration<UserPasswordHistory>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public UserPasswordHistoryMapSqlServer()
        {
            ToTable("userpasswordhistory");
            Property(p => p.Username).HasColumnType("varchar").IsRequired().HasMaxLength(255);
            Property(p => p.UserId).IsRequired();
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary");//.HasMaxLength(16);
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary");//.HasMaxLength(64);
            Property(p => p.CreatedOnDate).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserPasswordHistoryMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserPasswordHistory&gt;
    /// Create Date : 050912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserPasswordHistory trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class UserPasswordHistoryMapOracle : EntityTypeConfiguration<UserPasswordHistory>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public UserPasswordHistoryMapOracle()
        {
        }
    }
}
