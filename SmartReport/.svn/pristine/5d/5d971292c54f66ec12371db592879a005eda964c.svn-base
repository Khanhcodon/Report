using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ApproverMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Approver&gt;
    /// Create Date : 230113
    /// Author      : Tienbv
    /// Description : Mapping tương ứng với bảng Approver trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ApproverMapMySql : EntityTypeConfiguration<Approver>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ApproverMapMySql()
        {
            ToTable("approver");
            HasKey(p => p.ApproverId);
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.Content).IsRequired().HasColumnType("text");
            Property(p => p.IsDraft).IsRequired().HasColumnType("bit");
            Property(p => p.IsSuccess).IsRequired().HasColumnType("bit");
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.UserName).HasColumnType("varchar").HasMaxLength(40).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ApproverMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Approver&gt;
    /// Create Date : 230113
    /// Author      : Tienbv
    /// Description : Mapping tương ứng với bảng Approver trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ApproverMapSqlServer : EntityTypeConfiguration<Approver>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ApproverMapSqlServer()
        {
            ToTable("approver");
            HasKey(p => p.ApproverId);
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.Content).IsRequired();
            Property(p => p.IsDraft).IsRequired();
            Property(p => p.IsSuccess).IsRequired();
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.UserName).IsRequired().HasMaxLength(40);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ApproverMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Approver&gt;
    /// Create Date : 230113
    /// Author      : Tienbv
    /// Description : Mapping tương ứng với bảng Approver trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ApproverMapOracle : EntityTypeConfiguration<Approver>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ApproverMapOracle()
        {

        }
    }
}
