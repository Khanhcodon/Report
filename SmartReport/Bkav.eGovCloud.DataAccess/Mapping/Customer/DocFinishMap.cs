using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFinishMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocFinish&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocFinish trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocFinishMapMySql : EntityTypeConfiguration<DocFinish>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFinishMapMySql()
        {
            ToTable("docfinish");
            Property(p => p.UserId).IsRequired();
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.IsViewed).IsRequired().HasColumnType("bit");
            Property(p => p.IsDocumentImportant).IsRequired().HasColumnType("bit");
            Property(p => p.DocFinishType).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFinishMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocFinish&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocFinish trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocFinishMapSqlServer : EntityTypeConfiguration<DocFinish>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFinishMapSqlServer()
        {
            ToTable("docfinish");
            Property(p => p.UserId).IsRequired();
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.IsViewed).IsRequired();
            Property(p => p.IsDocumentImportant).IsRequired();
            Property(p => p.DocFinishType).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFinishMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocFinish&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocFinish trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocFinishMapOracle : EntityTypeConfiguration<DocFinish>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFinishMapOracle()
        {
            
        }
    }
}
