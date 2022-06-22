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
    /// Description : Mapping tương ứng với bảng IndicatorCatalog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class IndicatorCatalogMapMySql : EntityTypeConfiguration<IndicatorCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public IndicatorCatalogMapMySql()
        {
            ToTable("indicatorcatalog");
            Property(c => c.IndicatorCatalogName).IsRequired().HasMaxLength(500).HasColumnType("varchar");
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
    /// Description : Mapping tương ứng với bảng IndicatorCatalog trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class IndicatorCatalogMapSqlServer : EntityTypeConfiguration<IndicatorCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public IndicatorCatalogMapSqlServer()
        {
            ToTable("indicatorcatalog");
            Property(c => c.IndicatorCatalogId).HasColumnType("uniqueidentifier");
            Property(c => c.IndicatorCatalogName).IsRequired().HasMaxLength(500);
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
    /// Description : Mapping tương ứng với bảng IndicatorCatalog trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class IndicatorCatalogMapOracle : EntityTypeConfiguration<IndicatorCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public IndicatorCatalogMapOracle()
        {

        }
    }
}
