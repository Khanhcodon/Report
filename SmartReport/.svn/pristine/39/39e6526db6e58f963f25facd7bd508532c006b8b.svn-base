using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// EgovJobMap
    /// </summary>
    public class EgovJobMap
    {
        /// <summary>
        /// EgovJobMapMySql
        /// </summary>
        [ComVisible(false)]
        public class EgovJobMapMySql : EntityTypeConfiguration<EgovJob>
        {
            /// <summary>
            /// constructer
            /// </summary>
            public EgovJobMapMySql()
            {
                ToTable("EgovJob");
                HasKey(c => c.Id);
                Property(c => c.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
                Property(c => c.Interval).IsRequired();
                Property(c => c.JobType).IsRequired();
                Property(c => c.LastRun).HasColumnType("datetime");
                Property(c => c.NextRun).HasColumnType("datetime");
                Property(c => c.LastModified).HasColumnType("datetime");
                Property(c => c.IsActivated).HasColumnType("bit").IsRequired();
            }
        }

        /// <summary>
        /// EgovJobMapSqlServer
        /// </summary>
        [ComVisible(false)]
        public class EgovJobMapSqlServer : EntityTypeConfiguration<EgovJob>
        {
            /// <summary>
            /// constructer
            /// </summary>
            public EgovJobMapSqlServer()
            {
                ToTable("EgovJob");
                HasKey(c => c.Id);
                Property(c => c.Name).HasMaxLength(300).IsRequired();
                Property(c => c.Interval).IsRequired();
                Property(c => c.JobType).IsRequired();
                Property(c => c.LastRun).HasColumnType("datetime");
                Property(c => c.NextRun).HasColumnType("datetime");
                Property(c => c.LastModified).HasColumnType("datetime");
                Property(c => c.IsActivated).IsRequired();//.HasColumnType("bit");
            }
        }
    }
}