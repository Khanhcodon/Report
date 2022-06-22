using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRolePermissionMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserRolePermission&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserRolePermission trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserRolePermissionMapMySql : EntityTypeConfiguration<UserRolePermission>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserRolePermissionMapMySql()
        {
            ToTable("user_role_permission");
            Property(p => p.PermissionId).IsRequired();
            Property(p => p.PermissionKey).HasColumnType("varchar").HasMaxLength(64).IsRequired();
            Property(p => p.RoleKey).HasColumnType("varchar").HasMaxLength(32);
            Property(p => p.UsernameEmailDomain).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.AllowAccess).HasColumnType("bit").IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRolePermissionMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserRolePermission&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserRolePermission trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class UserRolePermissionMapSqlServer : EntityTypeConfiguration<UserRolePermission>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserRolePermissionMapSqlServer()
        {
            ToTable("user_role_permission");
            Property(p => p.PermissionId).IsRequired();
            Property(p => p.PermissionKey).HasColumnType("varchar").HasMaxLength(64).IsRequired();
            Property(p => p.RoleKey).HasColumnType("varchar").HasMaxLength(32);
            Property(p => p.UsernameEmailDomain).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.AllowAccess).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRolePermissionMapOralce - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserRolePermission&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserRolePermission trong CSDL Oralce
    /// </summary>
    [ComVisible(false)]
    public class UserRolePermissionMapOralce : EntityTypeConfiguration<UserRolePermission>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserRolePermissionMapOralce()
        {
            
        }
    }
}
