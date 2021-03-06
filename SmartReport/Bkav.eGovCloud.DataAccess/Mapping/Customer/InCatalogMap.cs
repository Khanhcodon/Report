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
    public class InCatalogMapMySql : EntityTypeConfiguration<InCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InCatalogMapMySql()
        {
            ToTable("dim_incatalog");
            Property(c => c.InCatalogName).IsRequired().HasMaxLength(500).HasColumnType("varchar");
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
    public class InCatalogMapSqlServer : EntityTypeConfiguration<InCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InCatalogMapSqlServer()
        {
            ToTable("dim_incatalog");
            Property(c => c.InCatalogId).HasColumnType("uniqueidentifier");
            Property(c => c.InCatalogName).IsRequired().HasMaxLength(500);
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
    public class InCatalogMapOracle : EntityTypeConfiguration<InCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InCatalogMapOracle()
        {

        }
    }
}
