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
    public class TemplateMapMySql : EntityTypeConfiguration<Template>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TemplateMapMySql()
        {
            ToTable("template");
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(p => p.Content).HasColumnType("text").IsRequired();
            Property(p => p.Type).IsRequired();
            Property(p => p.Permission).IsRequired();
            Property(p => p.IsActive).HasColumnType("bit");
            Property(p => p.ContentFile).HasColumnType("varchar");
            Property(p => p.ContentFileLocalName).HasColumnType("varchar");
            Property(p => p.CommonTemplate);
            Property(p => p.Sql).HasColumnType("text");
            Property(p => p.TitleMail).HasColumnType("varchar").HasMaxLength(500);
            HasOptional(p => p.Doctype)
                .WithMany()
                .HasForeignKey(p => p.DoctypeId)
                .WillCascadeOnDelete(false);
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
    public class TemplateMapSqlServer : EntityTypeConfiguration<Template>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TemplateMapSqlServer()
        {
            ToTable("template");
            Property(p => p.Name).HasMaxLength(300).IsRequired();
            Property(p => p.Content).IsRequired();//.HasColumnType("ntext");
            Property(p => p.Type).IsRequired();
            Property(p => p.Permission).IsRequired();
            //Property(p => p.IsActive).HasColumnType("bit");
            //Property(p => p.ContentFile);
            //Property(p => p.ContentFileLocalName);
            //Property(p => p.Sql).HasColumnType("ntext");
            Property(p => p.TitleMail).HasMaxLength(500);
            HasOptional(p => p.Doctype)
                .WithMany()
                .HasForeignKey(p => p.DoctypeId)
                .WillCascadeOnDelete(false);
            Property(p => p.ContentFile);
            Property(p => p.CommonTemplate);

        }
    }
}