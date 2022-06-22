using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SupplementaryMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Supplementary&gt;
    /// Create Date : 230113
    /// Author      : Tienbv
    /// Description : Mapping tương ứng với bảng Supplementary trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SupplementaryMapMySql : EntityTypeConfiguration<Supplementary>
    {
        /// <summary>
        /// C'tỏr
        /// </summary>
        public SupplementaryMapMySql() {
            ToTable("supplementary");
            Property(p => p.SupplementType).IsRequired();
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.DateSend).IsRequired();
            Property(p => p.CommentSend).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.CommentReceived).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.DocumentCopyIds).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(p => p.IsSuccess).IsRequired();
            Property(p => p.UserSendName).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.UserReceiveName).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.NewDateAppointed).HasColumnType("datetime");
            Property(p => p.OldDateAppointed).HasColumnType("datetime");
            Property(p => p.IsReceived).HasColumnType("bit");

            Ignore(p => p.SupplementaryDetail);
            Ignore(p => p.LocalId);
            Ignore(p => p.Details);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SupplementaryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Supplementary&gt;
    /// Create Date : 230113
    /// Author      : Tienbv
    /// Description : Mapping tương ứng với bảng Supplementary trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SupplementaryMapSqlServer : EntityTypeConfiguration<Supplementary>
    {
        /// <summary>
        /// C'tỏr
        /// </summary>
        public SupplementaryMapSqlServer()
        {
            ToTable("supplementary");
            Property(p => p.SupplementType).IsRequired();
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.DateSend).IsRequired();
            Property(p => p.CommentSend).HasMaxLength(1000);
            Property(p => p.CommentReceived).HasMaxLength(1000);
            Property(p => p.DocumentCopyIds).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(p => p.IsSuccess).IsRequired();
            Property(p => p.UserSendName).HasMaxLength(50);
            Property(p => p.UserReceiveName).HasMaxLength(50);
            Property(p => p.IsReceived);

            Ignore(p => p.SupplementaryDetail);
            Ignore(p => p.LocalId);
            Ignore(p => p.Details);
        }
    }
}
