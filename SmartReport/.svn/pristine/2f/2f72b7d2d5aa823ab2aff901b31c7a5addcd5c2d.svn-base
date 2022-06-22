using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RenewalsMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Renewals&gt;
    /// Create Date : 24/09/2015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Renewals trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class RenewalsMapMySql : EntityTypeConfiguration<Renewals>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RenewalsMapMySql()
        {
            ToTable("renewals");
            Property(p => p.UserRequestedId).IsRequired();
            Property(p => p.DocumentCopyId);
            Property(p => p.DocumentCopyIds).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.Reason).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.ApprovedComment).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.RenewalsDays).IsRequired();
            Property(p => p.IsApproved).HasColumnType("bit").IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RenewalsMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Renewals&gt;
    /// Create Date : 24/09/2015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Renewals trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class RenewalsMapSqlServer : EntityTypeConfiguration<Renewals>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RenewalsMapSqlServer()
        {
            ToTable("Renewals");
            Property(p => p.UserRequestedId).IsRequired();
            Property(p => p.DocumentCopyId);
            Property(p => p.DocumentCopyIds).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.Reason).HasMaxLength(1000);
            Property(p => p.ApprovedComment).HasMaxLength(1000);
            Property(p => p.RenewalsDays).IsRequired();
            Property(p => p.IsApproved).IsRequired();//.HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RenewalsMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Renewals&gt;
    /// Create Date : 24092015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Renewals trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class RenewalsMapOracle : EntityTypeConfiguration<Renewals>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RenewalsMapOracle()
        {

        }
    }
}
