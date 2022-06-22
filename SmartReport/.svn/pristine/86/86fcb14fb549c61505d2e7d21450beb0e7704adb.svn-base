using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreConfigMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreConfig&gt;
    /// Create Date : 170715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreConfig trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreConfigMapMySql : EntityTypeConfiguration<BackupRestoreConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreConfigMapMySql()
        {
            ToTable("backup_restore_config");
            HasKey(p => p.BackupRestoreConfigId);
            Property(p => p.Domain).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Server).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DatabaseName).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DatabaseType).IsRequired();
            Property(p => p.UserName).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Password).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Port).IsRequired();
            Property(p => p.Config).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.HasAutoRun).IsRequired();
            Property(p => p.ShareFolderId).IsRequired();
            Property(p => p.ZipPassword).HasColumnType("varchar").HasMaxLength(255);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreConfigMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreConfig&gt;
    /// Create Date : 170715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreConfig trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreConfigMapSqlServer : EntityTypeConfiguration<BackupRestoreConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreConfigMapSqlServer()
        {
            ToTable("backup_restore_config");
            HasKey(p => p.BackupRestoreConfigId);
            Property(p => p.Domain).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Server).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DatabaseName).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DatabaseType).HasColumnType("tinyint").IsRequired();
            Property(p => p.UserName).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Password).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Port).IsRequired();
            Property(p => p.Config).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.HasAutoRun).IsRequired();
            Property(p => p.ShareFolderId).IsRequired();
            Property(p => p.ZipPassword).HasColumnType("varchar").HasMaxLength(255);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreConfigMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreConfig&gt;
    /// Create Date : 170715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreConfig trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreConfigMapOracle : EntityTypeConfiguration<BackupRestoreConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreConfigMapOracle()
        {

        }
    }
}
