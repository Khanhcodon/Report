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
    public class ReportRuleMapMySql : EntityTypeConfiguration<ReportRule>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportRuleMapMySql()
        {
            ToTable("reportrules");
            HasKey(p => p.ReportRuleId);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportRuleMapSqlServer : EntityTypeConfiguration<ReportRule>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportRuleMapSqlServer()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportRuleMapOracle : EntityTypeConfiguration<ReportRule>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportRuleMapOracle()
        {

        }
    }
}
