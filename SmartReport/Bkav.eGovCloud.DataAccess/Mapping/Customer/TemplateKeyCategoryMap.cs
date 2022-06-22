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
    public class TemplateKeyCategoryMapMySql : EntityTypeConfiguration<TemplateKeyCategory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TemplateKeyCategoryMapMySql()
        {
            ToTable("templateKey_category");
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(p => p.IsActive).HasColumnType("bit");
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
    public class TemplateKeyCategoryMapSqlServer : EntityTypeConfiguration<TemplateKeyCategory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TemplateKeyCategoryMapSqlServer()
        {
            ToTable("templateKey_category");
            Property(p => p.Name).HasMaxLength(300).IsRequired();
            Property(p => p.IsActive).HasColumnType("bit");
        }
    }

}
