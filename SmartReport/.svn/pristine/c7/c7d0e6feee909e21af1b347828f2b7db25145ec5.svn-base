using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DailyProcessMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DailyProcess&gt;
    /// Create Date : 251113
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DailyProcess trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DailyProcessMapMySql : EntityTypeConfiguration<DailyProcess>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DailyProcessMapMySql()
        {
            ToTable("dailyprocess");
            Property(p => p.UserId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.ProcessType).IsRequired();
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.CitizenName).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.Receiver).HasColumnType("varchar");
            Property(p => p.Note).HasColumnType("varchar").HasMaxLength(1000);
            HasRequired(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.DocumentCopy)
                .WithMany()
                .HasForeignKey(p => p.DocumentCopyId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.Document)
                .WithMany()
                .HasForeignKey(p => p.DocumentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DailyProcessMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DailyProcess&gt;
    /// Create Date : 251113
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DailyProcess trong CSDL Sql
    /// </summary>
    [ComVisible(false)]
    public class DailyProcessMapSqlServer : EntityTypeConfiguration<DailyProcess>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DailyProcessMapSqlServer()
        {
            ToTable("dailyprocess");
            Property(p => p.UserId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.ProcessType).IsRequired();
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.CitizenName).HasMaxLength(1000);
            Property(p => p.Receiver);
            Property(p => p.Note).HasMaxLength(1000);
            HasRequired(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.DocumentCopy)
                .WithMany()
                .HasForeignKey(p => p.DocumentCopyId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.Document)
                .WithMany()
                .HasForeignKey(p => p.DocumentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DailyProcessMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DailyProcess&gt;
    /// Create Date : 251113
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DailyProcess trong CSDL Sql
    /// </summary>
    [ComVisible(false)]
    public class DailyProcessMapOracle : EntityTypeConfiguration<DailyProcess>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DailyProcessMapOracle()
        {

        }
    }
}
