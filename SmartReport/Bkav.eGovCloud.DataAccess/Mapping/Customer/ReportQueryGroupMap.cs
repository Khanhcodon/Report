using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [ComVisible(false)]
    public class ReportQueryGroupMapMySql : EntityTypeConfiguration<ReportQueryGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportQueryGroupMapMySql()
        {
            ToTable("reportquerygroups");
            HasKey(p => p.ReportQueryGroupId);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportQueryGroupMapSqlServer : EntityTypeConfiguration<ReportQueryGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportQueryGroupMapSqlServer()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportQueryGroupMapOracle : EntityTypeConfiguration<ReportQuery>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportQueryGroupMapOracle()
        {

        }
    }
}
