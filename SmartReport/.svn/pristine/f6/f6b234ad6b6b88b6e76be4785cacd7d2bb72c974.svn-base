using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJob&gt;
    /// Create Date : 270515
    /// Author      : TienBV- HopCV
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class TimeJobMapMySql : EntityTypeConfiguration<TimeJob>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimeJobMapMySql()
        {
            ToTable("time_job");
            HasKey(p => p.TimeJobId);
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(500).IsRequired();
            Property(p => p.TimerJobType).IsRequired();
            Property(p => p.DateLastJobRun).HasColumnType("datetime");
            Property(p => p.DateNextJobStartAfter).HasColumnType("datetime").IsRequired();
            Property(p => p.DateNextJobStartBefore).HasColumnType("datetime").IsRequired();
            Property(p => p.JobInterval).HasColumnType("datetime");
            Property(p => p.ScheduleConfig).HasColumnType("text").IsRequired();
            Property(p => p.ScheduleType).IsRequired();
            Property(p => p.IsActive).HasColumnType("bit").IsRequired();
            Property(p => p.IsRunning).HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJobDomain&gt;
    /// Create Date : 270515
    /// Author      : TienBV- HopCV
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class TimeJobMapSqlServer : EntityTypeConfiguration<TimeJob>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimeJobMapSqlServer()
        {
            ToTable("time_job");
            HasKey(p => p.TimeJobId);
            Property(p => p.Name).HasMaxLength(500).IsRequired();
            Property(p => p.TimerJobType).IsRequired();
            //Property(p => p.DateLastJobRun).HasColumnType("datetime");
            Property(p => p.DateNextJobStartAfter).IsRequired();//.HasColumnType("datetime");
            Property(p => p.DateNextJobStartBefore).IsRequired();//.HasColumnType("datetime");
            //Property(p => p.JobInterval).HasColumnType("datetime");
            Property(p => p.ScheduleConfig).IsRequired();//.HasColumnType("ntext");
            Property(p => p.ScheduleType).IsRequired();
            Property(p => p.IsActive).IsRequired();//.HasColumnType("bit");
            //Property(p => p.IsRunning).HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJobDomain&gt;
    /// Create Date : 270515
    /// Author      : TienBV- HopCV
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class TimeJobMapOracle : EntityTypeConfiguration<TimeJob>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimeJobMapOracle()
        {

        }
    }
}
