using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportQueryGroupReportQueryMapMySql : EntityTypeConfiguration<ReportQueryGroupReportQuery>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportQueryGroupReportQueryMapMySql()
        {
            ToTable("reportquerygroup_reportquerys");
            Property(c => c.ReportQueryGroupId).IsRequired();
            Property(c => c.ReportQueryId).IsRequired();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportQueryGroupReportQueryMapSqlServer : EntityTypeConfiguration<ReportQueryGroupReportQuery>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportQueryGroupReportQueryMapSqlServer()
        {
            
        }
    }
}
