using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LuceneMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Lucene&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Lucene trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class LuceneMapMySql : EntityTypeConfiguration<Lucene>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LuceneMapMySql()
        {
            ToTable("lucene");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.LastModified).IsRequired();
            Property(p => p.ContentId);
            Property(p => p.Title).IsRequired().HasColumnType("text");
            Property(p => p.IsFile).IsRequired().HasColumnType("bit");
            Property(p => p.IsIndexed).IsRequired().HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LuceneMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Lucene&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Lucene trong CSDL Server
    /// </summary>
    [ComVisible(false)]
    public class LuceneMapSqlServer : EntityTypeConfiguration<Lucene>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LuceneMapSqlServer()
        {
            ToTable("lucene");
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.LastModified).IsRequired();
            //Property(p => p.ContentId);
            Property(p => p.Title).IsRequired();//.HasColumnType("ntext");
            Property(p => p.IsFile).IsRequired();
            Property(p => p.IsIndexed).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LuceneMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Lucene&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Lucene trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class LuceneMapOracle : EntityTypeConfiguration<Lucene>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LuceneMapOracle()
        {
            
        }
    }
}
