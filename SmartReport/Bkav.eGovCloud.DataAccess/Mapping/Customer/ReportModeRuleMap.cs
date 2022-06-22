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
    public class ReportRulesModeMapMySql : EntityTypeConfiguration<ReportRulesMode>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportRulesModeMapMySql()
        {
            ToTable("reportrulesmode");
            HasKey(p => p.RulesModeId);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportRulesModeMapSqlServer : EntityTypeConfiguration<ReportRulesMode>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportRulesModeMapSqlServer()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class ReportRulesModeMapOracle : EntityTypeConfiguration<ReportRulesMode>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportRulesModeMapOracle()
        {

        }
    }
}
