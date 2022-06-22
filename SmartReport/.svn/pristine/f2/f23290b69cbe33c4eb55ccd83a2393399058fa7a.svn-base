using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DepartmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Department&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Department trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DepartmentMapMySql : EntityTypeConfiguration<Department>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DepartmentMapMySql()
        {
            ToTable("department");
            Property(p => p.DepartmentName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Property(p => p.DepartmentIdExt).HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.DepartmentPath).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.HasCalendar).IsRequired().HasColumnType("bit");
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
            HasOptional(p => p.DepartmentParent)
                .WithMany(p => p.DepartmentChildren)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);

            Ignore(p => p.VersionByte);
            Ignore(p => p.UserJobTitlesPositionIds);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DepartmentMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Department&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Department trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DepartmentMapSqlServer : EntityTypeConfiguration<Department>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DepartmentMapSqlServer()
        {
            ToTable("department");
            Property(p => p.DepartmentName).IsRequired().HasMaxLength(128);
            Property(p => p.IsActivated).IsRequired();
            Property(p => p.DepartmentIdExt).HasMaxLength(64);
            Property(p => p.DepartmentPath).HasMaxLength(255);
            Ignore(p => p.VersionByte);
            Ignore(p => p.UserJobTitlesPositionIds);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");
            HasOptional(p => p.DepartmentParent)
                .WithMany(p => p.DepartmentChildren)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DepartmentMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Department&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Department trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DepartmentMapOracle : EntityTypeConfiguration<Department>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DepartmentMapOracle()
        {
            
        }
    }
}
