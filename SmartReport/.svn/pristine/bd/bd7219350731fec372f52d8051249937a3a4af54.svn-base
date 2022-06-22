using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PermissionMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Permission&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Permission trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class PermissionMapMySql : EntityTypeConfiguration<Permission>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PermissionMapMySql()
        {
            ToTable("permission");
            Property(p => p.PermissionKey).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.PermissionName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.ModuleName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.Description).HasMaxLength(255).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PermissionMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Permission&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Permission trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class PermissionMapSqlServer : EntityTypeConfiguration<Permission>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PermissionMapSqlServer()
        {
            ToTable("permission");
            Property(p => p.PermissionKey).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.PermissionName).IsRequired().HasMaxLength(128);
            Property(p => p.ModuleName).IsRequired().HasMaxLength(128);
            Property(p => p.Description).HasMaxLength(255);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PermissionMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Permission&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Permission trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class PermissionMapOracle : EntityTypeConfiguration<Permission>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PermissionMapOracle()
        {
            
        }
    }
}
