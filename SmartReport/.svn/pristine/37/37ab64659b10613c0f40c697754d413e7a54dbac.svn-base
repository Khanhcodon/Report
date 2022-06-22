using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFeeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocField&gt;
    /// Create Date : 041212
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DocPaper trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocPaperMapMySql : EntityTypeConfiguration<DocPaper>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocPaperMapMySql()
        {
            ToTable("doc_paper");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.PaperName).HasMaxLength(1000).HasColumnType("varchar").IsRequired();
            Property(p => p.Amount).IsRequired();
            Property(p => p.Type).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFeeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocField&gt;
    /// Create Date : 041212
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng DocPaper trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocPaperMapSqlServer : EntityTypeConfiguration<DocPaper>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocPaperMapSqlServer()
        {
            ToTable("doc_paper");
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.PaperName).HasMaxLength(250).IsRequired();
            Property(p => p.Amount).IsRequired();
            Property(p => p.Type).IsRequired();
        }
    }
}
