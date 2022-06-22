using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SurveyCatalogValueMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CatalogValue&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng SurveyCatalogValue trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SurveyCatalogValueMapMySql : EntityTypeConfiguration<SurveyCatalogValue>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SurveyCatalogValueMapMySql()
        {
            ToTable("survey_catalogvalue");
            Property(c => c.CatalogId).IsRequired();
            Property(c => c.Value).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.Order);
            //Property(c => c.CatalogGuidId).IsRequired().HasColumnType("char");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SurveyCatalogValueMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CatalogValue&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng SurveyCatalogValue trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class SurveyCatalogValueMapSqlServer : EntityTypeConfiguration<SurveyCatalogValue>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SurveyCatalogValueMapSqlServer()
        {
            ToTable("survey_catalogvalue");
            Property(c => c.CatalogValueId).HasColumnType("uniqueidentifier");
            Property(c => c.CatalogId).HasColumnType("uniqueidentifier").IsRequired();
            Property(c => c.Value).IsRequired().HasMaxLength(255);
            //Property(c => c.Order);
            //Property(c => c.CatalogGuidId).IsRequired().HasColumnType("char");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SurveyCatalogValueMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CatalogValue&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng SurveyCatalogValue trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class SurveyCatalogValueMapOracle : EntityTypeConfiguration<SurveyCatalogValue>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SurveyCatalogValueMapOracle()
        {

        }
    }
}
