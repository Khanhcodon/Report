using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateDocumentCopyMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateDocumentCopy&gt;
    /// Create Date : 071013
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateDocumentCopy trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateDocumentCopyMapMySql : EntityTypeConfiguration<StorePrivateDocumentCopy>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateDocumentCopyMapMySql()
        {
            ToTable("storeprivate_documentcopy");
            Property(p => p.StorePrivateId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DocumentId).IsRequired();
            HasRequired(p => p.DocumentCopy)
                .WithMany()
                .HasForeignKey(p => p.DocumentCopyId)
                .WillCascadeOnDelete(false);
            //HasRequired(p => p.Document)
            //    .WithMany()
            //    .HasForeignKey(p => p.DocumentId)
            //    .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateDocumentCopyMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateDocumentCopy&gt;
    /// Create Date : 071013
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateDocumentCopy trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateDocumentCopyMapSqlServer : EntityTypeConfiguration<StorePrivateDocumentCopy>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateDocumentCopyMapSqlServer()
        {
            ToTable("storeprivate_documentcopy");
            Property(p => p.StorePrivateId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            HasRequired(p => p.DocumentCopy)
                .WithMany()
                .HasForeignKey(p => p.DocumentCopyId)
                .WillCascadeOnDelete(false);
            //HasRequired(p => p.Document)
            //    .WithMany()
            //    .HasForeignKey(p => p.DocumentId)
            //    .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateDocumentCopyMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateDocumentCopy&gt;
    /// Create Date : 071013
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateDocumentCopy trong CSDL Oracal
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateDocumentCopyMapOracle : EntityTypeConfiguration<StorePrivateDocumentCopy>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateDocumentCopyMapOracle()
        {

        }
    }
}
