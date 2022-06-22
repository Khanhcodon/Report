using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;User&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng User trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserMapMySql : EntityTypeConfiguration<User>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserMapMySql()
        {
            ToTable("user");
            Property(p => p.Username).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.UsernameEmailDomain).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.DomainName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary"); //.HasMaxLength(16)
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary"); //.HasMaxLength(64)
            Property(p => p.OpenId).HasMaxLength(1024).HasColumnType("varchar");
            Property(p => p.FullName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.FirstName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.LastName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.Phone).HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.Fax).HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.Address).HasColumnType("text");
            Property(p => p.Gender).IsRequired().HasColumnType("bit");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Property(p => p.IsLockedOut).IsRequired().HasColumnType("bit");
            Property(p => p.CanReadEveryDocument).HasColumnType("bit");
            //
            Property(p => p.UserSetting).HasColumnType("text");
            Property(p => p.NotifyInfo).HasColumnType("text");
            //
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
            Ignore(p => p.RoleIds);
            Ignore(p => p.IgnorePermissionIds);
            Ignore(p => p.GrantPermissionIds);
            Ignore(p => p.DenyPermissionIds);
            Ignore(p => p.DepartmentJobTitlesId);
            Ignore(p => p.NotifyInfoModel);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;User&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng User trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class UserMapSqlServer : EntityTypeConfiguration<User>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserMapSqlServer()
        {
            ToTable("user");
            Property(p => p.Username).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.UsernameEmailDomain).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.DomainName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.PasswordSalt).IsRequired().HasColumnType("binary"); //.HasMaxLength(16)
            Property(p => p.PasswordHash).IsRequired().HasColumnType("binary"); //.HasMaxLength(64)
            Property(p => p.OpenId).HasMaxLength(1024).HasColumnType("varchar");
            Property(p => p.FullName).IsRequired().HasMaxLength(128);
            Property(p => p.FirstName).IsRequired().HasMaxLength(64);
            Property(p => p.LastName).IsRequired().HasMaxLength(64);
            Property(p => p.Phone).HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.Fax).HasMaxLength(32).HasColumnType("varchar");
            //Property(p => p.Address).HasColumnType("ntext");
            Property(p => p.Gender).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsActivated).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsLockedOut).IsRequired();//.HasColumnType("bit");
            //Property(p => p.CanReadEveryDocument).HasColumnType("bit");
            //
            //Property(p => p.UserSetting).HasColumnType("ntext");
            //
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");
            Ignore(p => p.RoleIds);
            Ignore(p => p.IgnorePermissionIds);
            Ignore(p => p.GrantPermissionIds);
            Ignore(p => p.DenyPermissionIds);
            Ignore(p => p.DepartmentJobTitlesId);
            Ignore(p => p.NotifyInfoModel);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;User&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng User trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class UserMapOracle : EntityTypeConfiguration<User>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserMapOracle()
        {

        }
    }
}
