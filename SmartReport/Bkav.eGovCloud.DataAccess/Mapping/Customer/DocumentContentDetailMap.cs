using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentContentDetailMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentContentDetail&gt;
    /// Create Date : 240214
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocumentContentDetail trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocumentContentDetailMapMySql : EntityTypeConfiguration<DocumentContentDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentContentDetailMapMySql()
        {
            ToTable("documentcontentdetail");
            Property(c => c.DocumentContentId).IsRequired();
            Property(c => c.Version).IsRequired();
            Property(c => c.CreatedOnDate).IsRequired();
            Property(c => c.CreatedByUserId).IsRequired();
            Property(c => c.CreatedByUserName).HasColumnType("varchar").HasMaxLength(64).IsRequired();
            Property(c => c.Content).IsRequired().HasColumnType("longtext");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentContentDetailMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentContentDetail&gt;
    /// Create Date : 240214
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocumentContentDetail trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocumentContentDetailMapSqlServer : EntityTypeConfiguration<DocumentContentDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentContentDetailMapSqlServer()
        {
            ToTable("documentcontentdetail");
            Property(c => c.DocumentContentId).IsRequired();
            Property(c => c.Version).IsRequired();
            Property(c => c.CreatedOnDate).IsRequired();
            Property(c => c.CreatedByUserId).IsRequired();
            Property(c => c.CreatedByUserName).HasMaxLength(64).IsRequired();
            Property(c => c.Content).IsRequired();//.HasColumnType("ntext");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentContentDetailMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentContentDetail&gt;
    /// Create Date : 240214
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocumentContentDetail trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocumentContentDetailMapOracle : EntityTypeConfiguration<DocumentContentDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentContentDetailMapOracle()
        {
        }
    }
}
