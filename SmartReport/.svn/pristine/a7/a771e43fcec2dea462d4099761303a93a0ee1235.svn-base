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
    /// Description : Mapping tương ứng với bảng DocFee trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocFeeMapMySql : EntityTypeConfiguration<DocFee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFeeMapMySql()
        {
            ToTable("doc_fee");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.FeeName).HasMaxLength(250).HasColumnType("varchar").IsRequired();
            Property(p => p.Price).IsRequired();
            Property(p => p.Type).IsRequired();
            Property(p => p.SupplementaryId);
            Ignore(p => p.TypeInEnum);
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
    /// Description : Mapping tương ứng với bảng DocFee trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocFeeMapSqlServer : EntityTypeConfiguration<DocFee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFeeMapSqlServer()
        {
            ToTable("doc_fee");
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.FeeName).HasMaxLength(250).IsRequired();
            Property(p => p.Price).IsRequired();
            Property(p => p.Type).IsRequired();
            //Property(p => p.SupplementaryId);

            Ignore(p => p.TypeInEnum);
        }
    }
}
