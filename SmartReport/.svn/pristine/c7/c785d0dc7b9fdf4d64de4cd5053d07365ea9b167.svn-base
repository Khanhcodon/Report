using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DocTypeTimeJobMapMySql : EntityTypeConfiguration<DocTypeTimeJob>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocTypeTimeJobMapMySql()
        {
            ToTable("doctype_timejob");
            HasKey(p => p.DocTypeTimeJobId);
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.ScheduleType).IsRequired();
            Property(p => p.ScheduleConfig).HasColumnType("text").IsRequired();
            Property(p => p.IsActive).HasColumnType("bit").IsRequired();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DocTypeTimeJobMapSqlServer : EntityTypeConfiguration<DocTypeTimeJob>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocTypeTimeJobMapSqlServer()
        {
            ToTable("doctype_timejob");
            HasKey(p => p.DocTypeTimeJobId);
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.ScheduleType).IsRequired();
            Property(p => p.ScheduleConfig).IsRequired();
            Property(p => p.IsActive).IsRequired();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DocTypeTimeJobMapOracle : EntityTypeConfiguration<DocTypeTimeJob>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocTypeTimeJobMapOracle()
        {

        }
    }
}
