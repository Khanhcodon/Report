using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentPublishMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocPublish&gt;
    /// Create Date : 150114
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng DocPublish trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocPublishPlusMapMySql : EntityTypeConfiguration<DocPublishPlus>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocPublishPlusMapMySql()
        {
            ToTable("doc_publishplus");
            HasKey(p => p.DocumentPublishPlusId);
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DoctypeId).IsRequired();
            Property(p => p.DocCode).HasColumnType("varchar").IsRequired().HasMaxLength(100);
            Property(p => p.DatePublished).HasColumnType("datetime").IsRequired();
            Property(p => p.AddressName).HasColumnType("varchar").HasMaxLength(400).IsRequired();
            Property(p => p.IsHsmc).HasColumnType("bit").IsRequired();
            Property(p => p.UserPublishId).IsRequired();
            Property(p => p.UserPublishName).HasColumnType("varchar").HasMaxLength(100);
            Property(p => p.HasLienThong).HasColumnType("bit").IsRequired();
            Property(p => p.AddressId);
            Property(p => p.DateSent).HasColumnType("datetime");
            Property(p => p.IsPending).HasColumnType("bit").IsRequired();
            Property(p => p.HasRequireResponse).HasColumnType("bit").IsRequired();
            Property(p => p.DateAppointed).HasColumnType("datetime");
            Property(p => p.IsResponsed).HasColumnType("bit").IsRequired();
            Property(p => p.DateResponsed).HasColumnType("datetime");
            Property(p => p.DocCodeResponsed).HasColumnType("varchar").HasMaxLength(100);
            Property(p => p.DocumentCopyResponsed);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentPublishMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentPublish&gt;
    /// Create Date : 150114
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng DocumentPublish trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocPublishPlusMapSqlServer : EntityTypeConfiguration<DocPublishPlus>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocPublishPlusMapSqlServer()
        {
            ToTable("doc_publishplus");
            HasKey(p => p.DocumentPublishPlusId);
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DoctypeId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.DocCode).HasColumnType("varchar").IsRequired().HasMaxLength(100);
            Property(p => p.DatePublished).IsRequired();
            Property(p => p.AddressName).HasMaxLength(400).IsRequired();
            Property(p => p.IsHsmc).IsRequired();
            Property(p => p.UserPublishId).IsRequired();
            Property(p => p.UserPublishName).HasMaxLength(100);
            Property(p => p.HasLienThong).IsRequired();
            //Property(p => p.AddressId);
            //Property(p => p.DateSent);
            Property(p => p.IsPending).IsRequired();
            Property(p => p.HasRequireResponse).IsRequired();
            //Property(p => p.DateAppointed);
            Property(p => p.IsResponsed).IsRequired();
            //Property(p => p.DateResponsed);
            Property(p => p.DocCodeResponsed).HasColumnType("varchar").HasMaxLength(100);
            //Property(p => p.DocumentCopyResponsed);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentPublishMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentPublish&gt;
    /// Create Date : 150114
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng DocumentPublish trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocPublishPlusMapOracle : EntityTypeConfiguration<DocPublishPlus>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocPublishPlusMapOracle()
        {
        }
    }
}
