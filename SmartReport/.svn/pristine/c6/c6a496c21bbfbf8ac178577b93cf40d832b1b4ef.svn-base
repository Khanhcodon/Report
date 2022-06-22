using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountPasswordHistoryMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountPasswordHistory&gt;
    /// Create Date : 060912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AccountPasswordHistory trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class AccountPasswordHistoryMapMySql : EntityTypeConfiguration<AccountPasswordHistory>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public AccountPasswordHistoryMapMySql()
        {
            ToTable("accountpasswordhistory");
            Property(p => p.AccountId).IsRequired();
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary");//.HasMaxLength(16);
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary");//.HasMaxLength(64);
            Property(p => p.CreatedOnDate).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountPasswordHistoryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountPasswordHistory&gt;
    /// Create Date : 060912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AccountPasswordHistory trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class AccountPasswordHistoryMapSqlServer : EntityTypeConfiguration<AccountPasswordHistory>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public AccountPasswordHistoryMapSqlServer()
        {
            ToTable("accountpasswordhistory");
            Property(p => p.AccountId).IsRequired();
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary");//.HasMaxLength(16);
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary");//.HasMaxLength(64);
            Property(p => p.CreatedOnDate).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountPasswordHistoryMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountPasswordHistory&gt;
    /// Create Date : 060912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AccountPasswordHistory trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class AccountPasswordHistoryMapOracle : EntityTypeConfiguration<AccountPasswordHistory>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public AccountPasswordHistoryMapOracle()
        {
        }
    }
}
