using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJob&gt;
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class TimerJobMapMySql : EntityTypeConfiguration<TimerJob>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimerJobMapMySql()
        {
            ToTable("timerjob");
            HasKey(p => p.TimerJobId);
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(500).IsRequired();
            Property(p => p.TimerJobId).IsRequired();
            Property(p => p.DomainId).IsRequired();
            Property(p => p.TimerJobType).IsRequired();
            Property(p => p.TimerJobConfig).HasColumnType("text");
            Property(p => p.DateLastJobRun).HasColumnType("datetime");
            Property(p => p.DateNextJobStartAfter).HasColumnType("datetime").IsRequired();
            Property(p => p.DateNextJobStartBefore).HasColumnType("datetime").IsRequired();
            Property(p => p.JobInterval).HasColumnType("datetime");
            Property(p => p.ScheduleConfig).HasColumnType("varchar").IsRequired();
            Property(p => p.ScheduleType).IsRequired();
            Property(p => p.IsActive).HasColumnType("bit").IsRequired();
            Property(p => p.IsRunning).HasColumnType("bit");
            HasRequired(p => p.Domain)
                .WithMany()
                .HasForeignKey(p => p.DomainId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJobDomain&gt;
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class TimerJobMapSqlServer : EntityTypeConfiguration<TimerJob>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimerJobMapSqlServer()
        {
            ToTable("timerjob");
            HasKey(p => p.TimerJobId);
            Property(p => p.Name).HasMaxLength(500).IsRequired();
            Property(p => p.TimerJobId).IsRequired();
            Property(p => p.TimerJobType).IsRequired();
            Property(p => p.TimerJobConfig).HasColumnType("text");
            Property(p => p.DomainId).IsRequired();
            Property(p => p.DateLastJobRun);
            Property(p => p.DateNextJobStartAfter).IsRequired();
            Property(p => p.DateNextJobStartBefore).IsRequired();
            Property(p => p.JobInterval);
            Property(p => p.ScheduleConfig).IsRequired();
            Property(p => p.ScheduleType).IsRequired();
            Property(p => p.IsActive).IsRequired();
            Property(p => p.IsRunning);
            HasRequired(p => p.Domain)
                .WithMany()
                .HasForeignKey(p => p.DomainId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJobDomain&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class TimerJobMapOracle : EntityTypeConfiguration<TimerJob>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimerJobMapOracle()
        {
            
        }
    }
}
