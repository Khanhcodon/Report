using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocCatalogMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocCatalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocCatalog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocCatalogMapMySql : EntityTypeConfiguration<DocCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocCatalogMapMySql()
        {
            ToTable("doc_catalog");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.CatalogValueId).IsRequired();
            Property(p => p.CatalogId).IsRequired();
            Property(p => p.FormId).IsRequired();
            Property(p => p.CatalogValue).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.CatalogName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            HasRequired(p => p.Form)
                .WithMany()
                .HasForeignKey(p => p.FormId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocCatalogMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocCatalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocCatalog trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocCatalogMapSqlServer : EntityTypeConfiguration<DocCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocCatalogMapSqlServer()
        {
            ToTable("doc_catalog");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.CatalogValueId).IsRequired();
            Property(p => p.CatalogId).IsRequired();
            Property(p => p.FormId).IsRequired();
            Property(p => p.CatalogValue).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.CatalogName).IsRequired().HasMaxLength(255);
            HasRequired(p => p.Form)
                .WithMany()
                .HasForeignKey(p => p.FormId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocCatalogMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocCatalog&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocCatalog trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocCatalogMapOracle : EntityTypeConfiguration<DocCatalog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocCatalogMapOracle()
        {

        }
    }
}
