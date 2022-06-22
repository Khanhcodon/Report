using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SurveyCatalogMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng SurveyCatalog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SurveyCatalogMapMySql : EntityTypeConfiguration<SurveyCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SurveyCatalogMapMySql()
        {
            ToTable("survey_catalog");
            Property(c => c.CatalogName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.IsActivated).IsRequired().HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SurveyCatalogMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng SurveyCatalog trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class SurveyCatalogMapSqlServer : EntityTypeConfiguration<SurveyCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SurveyCatalogMapSqlServer()
        {
            ToTable("survey_catalog");
            Property(c => c.CatalogId).HasColumnType("uniqueidentifier");
            Property(c => c.CatalogName).IsRequired().HasMaxLength(255);
            Property(c => c.IsActivated).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SurveyCatalogMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng SurveyCatalog trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class SurveyCatalogMapOracle : EntityTypeConfiguration<SurveyCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SurveyCatalogMapOracle()
        {

        }
    }
}
