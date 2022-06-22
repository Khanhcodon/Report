using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eGov Core
    /// Project: eGov Cloud v1.0
    /// Class : DocumentViewedMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentViewed&gt;
    /// Create Date : 100513
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DocumentViewed trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocumentViewedMapMySql : EntityTypeConfiguration<DocumentViewed>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentViewedMapMySql()
        {
            ToTable("documentviewed");
            HasRequired(p => p.DocumentCopy)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
            Property(p => p.DocumentId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentViewedMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentViewed&gt;
    /// Create Date : 100513
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DocumentViewed trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocumentViewedMapSqlServer : EntityTypeConfiguration<DocumentViewed>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentViewedMapSqlServer()
        {
            ToTable("documentviewed");
            HasRequired(p => p.DocumentCopy)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentViewedMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentViewed&gt;
    /// Create Date : 100513
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DocumentViewed trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocumentViewedMapOracle : EntityTypeConfiguration<DocumentViewed>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentViewedMapOracle()
        {
            
        }
    }
}
