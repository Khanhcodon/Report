using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreFileConfigMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt; BackupRestoreFileConfig&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng  BackupRestoreFileConfig trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreFileConfigMapMySql : EntityTypeConfiguration<BackupRestoreFileConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreFileConfigMapMySql()
        {
            ToTable("backup_restore_file_config");
            HasKey(p => p.BackupRestoreFileConfigId);
            Property(p => p.Domain).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Directory).IsRequired().HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.FileName).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.UserName).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Password).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.ZipPassword).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.HasAutoRun).IsRequired();
            Property(p => p.IsNetwork).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreFileConfigMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt; BackupRestoreFileConfig&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng  BackupRestoreFileConfig trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreFileConfigMapSqlServer : EntityTypeConfiguration<BackupRestoreFileConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreFileConfigMapSqlServer()
        {
            ToTable("backup_restore_file_config");
            HasKey(p => p.BackupRestoreFileConfigId);
            Property(p => p.Domain).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Directory).IsRequired().HasMaxLength(1000);
            Property(p => p.FileName).IsRequired().HasMaxLength(255);
            Property(p => p.UserName).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Password).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.ZipPassword).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.HasAutoRun).IsRequired().HasColumnType("bit");
            Property(p => p.IsNetwork).IsRequired().HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreFileConfigMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt; BackupRestoreFileConfig&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng  BackupRestoreFileConfig trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreFileConfigMapOracle : EntityTypeConfiguration<BackupRestoreFileConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreFileConfigMapOracle()
        {
        }
    }
}
