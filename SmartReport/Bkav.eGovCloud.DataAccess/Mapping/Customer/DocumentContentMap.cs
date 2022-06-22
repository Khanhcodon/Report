using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : CatalogMapMySql - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para>     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// <para></para> Create Date : 211112
    /// <para></para> Author      : TienBV
    /// <para></para> Description : Mapping tương ứng với bảng DocumentContent trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocumentContentMapMySql : EntityTypeConfiguration<DocumentContent>
    {
        /// <summary>
        /// Map properties
        /// </summary>
        public DocumentContentMapMySql()
        {
            ToTable("documentcontent");
            Property(d => d.DocumentId).IsRequired();
            Property(d => d.ContentName).HasColumnType("varchar").HasMaxLength(250);
            Property(d => d.Content).HasColumnType("longtext").IsRequired();
            Property(d => d.FormTypeId).IsRequired();
            Property(d => d.IsMain).HasColumnType("bit").IsRequired();
            Property(d => d.Version).IsRequired();
            Property(d => d.Url);
            Property(d => d.ContentUrl);
            //Property(d => d.EditUrl);
            Ignore(p => p.FormTypeIdInEnum);
        }
    }

    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : CatalogMapSqlServer - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para>     * Inherit : EntityTypeConfiguration&lt;Catalog&gt;
    /// <para></para> Create Date : 211112
    /// <para></para> Author      : TienBV
    /// <para></para> Description : Mapping tương ứng với bảng DocumentContent trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocumentContentMapSqlServer : EntityTypeConfiguration<DocumentContent>
    {
        /// <summary>
        /// Map properties
        /// </summary>
        public DocumentContentMapSqlServer()
        {
            ToTable("documentcontent");
            Property(d => d.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(d => d.ContentName).HasMaxLength(250);
            Property(d => d.Content).IsRequired();//.HasColumnType("ntext") ;
            Property(d => d.FormTypeId).IsRequired();
            Property(d => d.IsMain).IsRequired();
            Property(d => d.Version).IsRequired();
            Property(d => d.Url).HasColumnType("varchar").HasMaxLength(2000);
            Property(d => d.ContentUrl).HasColumnType("varchar").HasMaxLength(2000);
            //Property(d => d.EditUrl);
            Ignore(p => p.FormTypeIdInEnum);
        }
    }
}
