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
    public class SupplementaryDetailMapMySql : EntityTypeConfiguration<SupplementaryDetail>
    {
        /// <summary>
        /// C'tỏr
        /// </summary>
        public SupplementaryDetailMapMySql()
        {
            ToTable("supplementary_detail");
            HasKey(p => p.SupplementaryDetailId);
            Property(p => p.SupplementaryId).IsRequired();
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.Comment).IsRequired().HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.DateSend).IsRequired().HasColumnType("datetime");
            Property(p => p.DateDeleted).HasColumnType("datetime");
            Property(p => p.IsDeleted).HasColumnType("bit");
            Property(p => p.UserDeletedId);
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
    public class SupplementaryDetailMapSqlServer : EntityTypeConfiguration<SupplementaryDetail>
    {
        /// <summary>
        /// C'tỏr
        /// </summary>
        public SupplementaryDetailMapSqlServer()
        {
            ToTable("supplementary_detail");

            HasKey(p => p.SupplementaryDetailId);
            Property(p => p.SupplementaryId).IsRequired();
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.Comment).IsRequired().HasMaxLength(1000);
            Property(p => p.DateSend).IsRequired();//.HasColumnType("datetime");
            //Property(p => p.DateDeleted).HasColumnType("datetime");
            //Property(p => p.IsDeleted).HasColumnType("bit");
            Property(p => p.UserDeletedId);
        }
    }
}
