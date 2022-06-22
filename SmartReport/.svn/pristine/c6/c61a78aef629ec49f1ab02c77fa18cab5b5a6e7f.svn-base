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
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Catalog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CatalogMapMySql : EntityTypeConfiguration<Catalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CatalogMapMySql()
        {
            ToTable("catalog");
            Property(c => c.CatalogName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.IsActivated).IsRequired().HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CatalogMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Catalog trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CatalogMapSqlServer : EntityTypeConfiguration<Catalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CatalogMapSqlServer()
        {
            ToTable("catalog");
            Property(c => c.CatalogId).HasColumnType("uniqueidentifier");
            Property(c => c.CatalogName).IsRequired().HasMaxLength(255);
            Property(c => c.IsActivated).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CatalogMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Catalog trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CatalogMapOracle : EntityTypeConfiguration<Catalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CatalogMapOracle()
        {

        }
    }
}
