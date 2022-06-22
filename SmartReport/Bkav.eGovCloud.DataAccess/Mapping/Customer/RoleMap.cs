using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RoleMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Role trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class RoleMapMySql : EntityTypeConfiguration<Role>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RoleMapMySql()
        {
            ToTable("role");
            Property(p => p.RoleKey).IsRequired().HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.RoleName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.Description).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.IsAutoAssignment).IsRequired().HasColumnType("bit");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Ignore(p => p.VersionByte);
            Ignore(p => p.UserIds);
            Ignore(p => p.IgnorePermissionIds);
            Ignore(p => p.GrantPermissionIds);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RoleMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Role trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class RoleMapSqlServer : EntityTypeConfiguration<Role>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RoleMapSqlServer()
        {
            ToTable("role");
            Property(p => p.RoleKey).IsRequired().HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.RoleName).IsRequired().HasMaxLength(128);
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.IsAutoAssignment).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsActivated).IsRequired();//.HasColumnType("bit");
            Ignore(p => p.VersionByte);
            Ignore(p => p.UserIds);
            Ignore(p => p.IgnorePermissionIds);
            Ignore(p => p.GrantPermissionIds);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RoleMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Role trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class RoleMapOracle : EntityTypeConfiguration<Role>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RoleMapOracle()
        {
            
        }
    }
}
