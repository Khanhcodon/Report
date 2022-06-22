using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;

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
    public class TemplateKeyMapMySql : EntityTypeConfiguration<TemplateKey>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TemplateKeyMapMySql()
        {
            ToTable("templatekey");
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(p => p.Code).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(p => p.Sql).HasColumnType("text").IsRequired();
            Property(p => p.HtmlTemplate).HasColumnType("text").IsRequired();
            Property(p => p.Type).IsRequired();
            Property(p => p.IsActive).HasColumnType("bit");
            Property(p => p.IsCustomKey).HasColumnType("bit");
            Ignore(p => p.TypeInEnum);
            HasOptional(p => p.Doctype)
                .WithMany()
                .HasForeignKey(p => p.DoctypeId).
                WillCascadeOnDelete(false);
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
    public class TemplateKeyMapSqlServer : EntityTypeConfiguration<TemplateKey>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TemplateKeyMapSqlServer()
        {
            ToTable("templatekey");
            Property(p => p.Name).HasMaxLength(200).IsRequired();
            Property(p => p.Code).HasMaxLength(200).IsRequired();
            Property(p => p.Sql).IsRequired();//.HasColumnType("ntext");
            Property(p => p.HtmlTemplate).IsRequired();//.HasColumnType("ntext");
            Property(p => p.Type).IsRequired();
            //Property(p => p.IsActive).HasColumnType("bit");
            //Property(p => p.IsCustomKey).HasColumnType("bit");
            Ignore(p => p.TypeInEnum);
            HasOptional(p => p.Doctype)
                .WithMany()
                .HasForeignKey(p => p.DoctypeId).
                WillCascadeOnDelete(false);
        }
    }

}
