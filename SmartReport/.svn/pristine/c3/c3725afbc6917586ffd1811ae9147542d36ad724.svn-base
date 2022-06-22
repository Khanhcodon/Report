using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AuthorizeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Authorize&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Authorize trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class AuthorizeMapMySql : EntityTypeConfiguration<Authorize>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AuthorizeMapMySql()
        {
            ToTable("authorize");
            Property(c => c.AuthorizeUserId).IsRequired();
            Property(c => c.AuthorizedUserId).IsRequired();
            Property(c => c.Active).IsRequired().HasColumnType("bit");
            Property(c => c.DateBegin).HasColumnType("datetime").IsRequired();
            Property(c => c.DateEnd).HasColumnType("datetime").IsRequired();
            Property(c => c.Note).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.Permission).IsRequired();
            Property(c => c.DocTypeId);
            Property(c => c.AuthorizedUserName).IsRequired().HasColumnType("varchar").HasMaxLength(128);
            Property(c => c.AuthorizeUserName).IsRequired().HasColumnType("varchar").HasMaxLength(128); 
            Ignore(c => c.DocTypes);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Authorize&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Authorize trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class AuthorizeMapSqlServer : EntityTypeConfiguration<Authorize>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AuthorizeMapSqlServer()
        {
            ToTable("authorize");
            Property(c => c.AuthorizeUserId).IsRequired();
            Property(c => c.AuthorizedUserId).IsRequired();
            Property(c => c.Active).IsRequired();
            Property(c => c.DateBegin).IsRequired();
            Property(c => c.DateEnd).IsRequired();
            Property(c => c.Note).HasMaxLength(255);
            Property(c => c.Permission).IsRequired();
            Property(c => c.DocTypeId).HasColumnType("char").HasMaxLength(36);
            Property(c => c.AuthorizedUserName).IsRequired().HasMaxLength(128);
            Property(c => c.AuthorizeUserName).IsRequired().HasMaxLength(128); 
            Ignore(c => c.DocTypes);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Authorize&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Attachment trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class AuthorizeMapOracle : EntityTypeConfiguration<Authorize>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AuthorizeMapOracle()
        {

        }
    }
}
