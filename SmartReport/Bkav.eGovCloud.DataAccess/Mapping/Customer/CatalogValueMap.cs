using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CatalogValueMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CatalogValue&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng CatalogValue trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CatalogValueMapMySql : EntityTypeConfiguration<CatalogValue>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CatalogValueMapMySql()
        {
            ToTable("catalogvalue");
            Property(c => c.CatalogId).IsRequired();
            Property(c => c.Value).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.Order);
            //Property(c => c.CatalogGuidId).IsRequired().HasColumnType("char");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CatalogValueMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CatalogValue&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng CatalogValue trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CatalogValueMapSqlServer : EntityTypeConfiguration<CatalogValue>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CatalogValueMapSqlServer()
        {
            ToTable("catalogvalue");
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
    /// Class : CatalogValueMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CatalogValue&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng CatalogValue trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CatalogValueMapOracle : EntityTypeConfiguration<CatalogValue>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CatalogValueMapOracle()
        {

        }
    }
}
