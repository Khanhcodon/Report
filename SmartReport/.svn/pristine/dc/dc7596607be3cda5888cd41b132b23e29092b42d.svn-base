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
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreHistory&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreHistory trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreHistoryMapMySql : EntityTypeConfiguration<BackupRestoreHistory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreHistoryMapMySql()
        {
            ToTable("backup_restore_history");
            HasKey(p => p.BackupRestoreHistoryId);
            Property(p => p.Domain).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Ip).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.DateCreated).IsRequired().HasColumnType("datetime");
            Property(p => p.Description).HasColumnType("text");
            Property(p => p.IsBackup).IsRequired().HasColumnType("bit");
            Property(p => p.Account).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.IsSuccessed).IsRequired().HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreHistoryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreHistory&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreHistory trong CSDL Server
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreHistoryMapSqlServer : EntityTypeConfiguration<BackupRestoreHistory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreHistoryMapSqlServer()
        {
            ToTable("backup_restore_history");
            HasKey(p => p.BackupRestoreHistoryId);
            Property(p => p.Domain).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Ip).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.DateCreated).IsRequired();//.HasColumnType("datetime");
            //Property(p => p.Description).HasColumnType("ntext");
            Property(p => p.IsBackup).IsRequired();
            Property(p => p.Account).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.IsSuccessed).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreHistoryMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BackupRestoreHistory&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng BackupRestoreHistory trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class BackupRestoreHistoryMapOracle : EntityTypeConfiguration<BackupRestoreHistory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BackupRestoreHistoryMapOracle()
        {

        }
    }
}
