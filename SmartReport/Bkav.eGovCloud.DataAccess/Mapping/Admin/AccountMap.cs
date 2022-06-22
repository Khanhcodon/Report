using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Account&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Account trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class AccountMapMySql : EntityTypeConfiguration<Account>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AccountMapMySql()
        {
            ToTable("account");
            Property(p => p.Username).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.UsernameEmailDomain).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.DomainName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary");//.HasMaxLength(16);
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary");//.HasMaxLength(64);
            Property(p => p.OpenId).HasMaxLength(1024).HasColumnType("varchar");
            Property(p => p.FullName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.Phone).HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.Fax).HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.Address).HasColumnType("text");
            Property(p => p.Gender).IsRequired().HasColumnType("bit");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            //  Property(p => p.Avatar).HasMaxLength(255);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountDomain&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Account trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class AccountMapSqlServer : EntityTypeConfiguration<Account>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AccountMapSqlServer()
        {
            ToTable("account");
            Property(p => p.Username).IsRequired().HasMaxLength(255);
            Property(p => p.UsernameEmailDomain).IsRequired().HasMaxLength(255);
            Property(p => p.DomainName).IsRequired().HasMaxLength(64);
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary");//.HasMaxLength(16);
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary");//.HasMaxLength(64);
            Property(p => p.OpenId).HasMaxLength(1024);
            Property(p => p.FullName).IsRequired().HasMaxLength(128);
            Property(p => p.Phone).HasMaxLength(32);
            Property(p => p.Phone).HasMaxLength(32);
            Property(p => p.Address).HasColumnType("ntext");
            Property(p => p.Gender).IsRequired();
            Property(p => p.IsActivated).IsRequired();
            //
            //  Property(p => p.Avatar).HasMaxLength(255);
            //
            Ignore(p => p.VersionDateTime);
            Property(p => p.VersionByte).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountDomain&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Account trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class AccountMapOracle : EntityTypeConfiguration<Account>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AccountMapOracle()
        {

        }
    }
}
