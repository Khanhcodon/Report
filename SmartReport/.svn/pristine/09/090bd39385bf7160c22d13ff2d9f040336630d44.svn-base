using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentExtensionMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentExtension&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocumentExtension trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocumentExtensionMapMySql : EntityTypeConfiguration<DocumentExtension>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentExtensionMapMySql()
        {
            ToTable("documentextension");
            HasKey(p => p.DocumentId);
            Property(p => p.SenderId).IsRequired();
            Property(p => p.DocNumber).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Progress).HasColumnType("text");
            Property(p => p.DocumentVersion).IsRequired();
            HasRequired(p => p.Document);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentExtensionMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentExtension&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocumentExtension trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocumentExtensionMapSqlServer : EntityTypeConfiguration<DocumentExtension>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentExtensionMapSqlServer()
        {
            ToTable("documentextension");
            HasKey(p => p.DocumentId);
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier");
            Property(p => p.SenderId).IsRequired();
            Property(p => p.DocNumber).HasColumnType("varchar").IsRequired().HasMaxLength(255);
            //Property(p => p.Progress).HasColumnType("ntext");
            Property(p => p.DocumentVersion).IsRequired();
            HasRequired(p => p.Document);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentExtensionMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentExtension&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocumentExtension trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocumentExtensionMapOracle : EntityTypeConfiguration<DocumentExtension>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentExtensionMapOracle()
        {
            
        }
    }
}
