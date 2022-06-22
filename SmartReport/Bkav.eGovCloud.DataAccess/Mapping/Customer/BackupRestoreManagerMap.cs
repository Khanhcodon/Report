using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreHistoryMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreManager&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreManager trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreManagerMapMySql : EntityTypeConfiguration<BackupRestoreManager>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreManagerMapMySql()
        {
            ToTable("backup_restore_manager");
            HasKey(p => p.BackupRestoreManagerId);
            Property(p => p.Domain).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Account).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DateCreated).IsRequired().HasColumnType("datetime");
            Property(p => p.Description).HasColumnType("text");
            Property(p => p.IsDatabaseFile).IsRequired().HasColumnType("bit");
            Property(p => p.FileNameAlias).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Size);
            Property(c => c.FileLocationId).IsRequired();
            Property(c => c.IdentityFolder).IsRequired().HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.FileLocationKey).HasColumnType("varchar").HasMaxLength(36);
            Property(c => c.FileName).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            Property(p => p.ZipPassword).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Alias).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.LastModifiedByUser).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.LastModifiedOnDate).HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreHistoryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreManager&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreManager trong CSDL Server
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreManagerMapSqlServer : EntityTypeConfiguration<BackupRestoreManager>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreManagerMapSqlServer()
        {
            ToTable("backup_restore_manager");
            HasKey(p => p.BackupRestoreManagerId);
            Property(p => p.Domain).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Account).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DateCreated).IsRequired();//.HasColumnType("datetime");
            //Property(p => p.Description).HasColumnType("ntext");
            Property(p => p.IsDatabaseFile).IsRequired();//.HasColumnType("bit");
            Property(p => p.FileNameAlias).IsRequired().HasMaxLength(255);
            //Property(p => p.Size);
            Property(c => c.FileLocationId).IsRequired();
            Property(c => c.IdentityFolder).IsRequired().HasMaxLength(10);
            Property(c => c.FileLocationKey).HasColumnType("varchar").HasMaxLength(36);
            Property(c => c.FileName).IsRequired().HasMaxLength(36);
            Property(p => p.ZipPassword).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Alias).IsRequired().HasMaxLength(255);
            Property(p => p.LastModifiedByUser).HasMaxLength(255);
            Property(p => p.LastModifiedOnDate);//.HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreHistoryMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreManager&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreManager trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreManagerMapOracle : EntityTypeConfiguration<BackupRestoreManager>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreManagerMapOracle()
        {

        }
    }
}
