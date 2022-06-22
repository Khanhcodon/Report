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
    /// Description : Mapping tương ứng với bảng CategoryDisaggreations trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CategoryDisaggreationsMapMySql : EntityTypeConfiguration<CategoryDisaggregations>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CategoryDisaggreationsMapMySql()
        {
            ToTable("dim_categorydisaggregations");
            Property(c => c.CategoryDisaggregationName).IsRequired().HasMaxLength(500).HasColumnType("varchar");
            Property(c => c.CategoryDisaggregationCode).IsRequired().HasMaxLength(500).HasColumnType("varchar");
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
    /// Description : Mapping tương ứng với bảng CategoryDisaggreations trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CategoryDisaggreationsMapSqlServer : EntityTypeConfiguration<CategoryDisaggregations>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CategoryDisaggreationsMapSqlServer()
        {
            ToTable("dim_categorydisaggregations");
            Property(c => c.CategoryDisaggregationId).HasColumnType("uniqueidentifier");
            Property(c => c.CategoryDisaggregationName).IsRequired().HasMaxLength(500);
            Property(c => c.CategoryDisaggregationCode).IsRequired().HasMaxLength(500);
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
    /// Description : Mapping tương ứng với bảng CategoryDisaggreations trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CategoryDisaggreationsMapOracle : EntityTypeConfiguration<InCatalogValue>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CategoryDisaggreationsMapOracle()
        {

        }
    }
}
