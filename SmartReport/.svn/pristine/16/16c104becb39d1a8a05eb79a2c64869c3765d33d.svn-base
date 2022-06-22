using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CatalogMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 250520
    /// Author      : Tudv
    /// Description : Mapping tương ứng với bảng Disaggregation trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DisaggregationMySql : EntityTypeConfiguration<Disaggregation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DisaggregationMySql()
        {
            ToTable("dim_disaggregation");
            Property(c => c.IndicatorName).IsRequired().HasMaxLength(500).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CatalogMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 250520
    /// Author      : Tudv
    /// Description : Mapping tương ứng với bảng Indicattor trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DisaggregationSqlServer : EntityTypeConfiguration<Disaggregation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DisaggregationSqlServer()
        {
            ToTable("dim_disaggregation");
            Property(c => c.IndicatorId).HasColumnType("uniqueidentifier");
            Property(c => c.IndicatorName).IsRequired().HasMaxLength(500);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CatalogMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 250520
    /// Author      : Tudv
    /// Description : Mapping tương ứng với bảng Indicattor trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DisaggregationMapOracle : EntityTypeConfiguration<Disaggregation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DisaggregationMapOracle()
        {

        }
    }
}
