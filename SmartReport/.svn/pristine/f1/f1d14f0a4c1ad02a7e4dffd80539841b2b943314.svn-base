using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 011113
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Report trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BusinessLicenseAttachMapMySql : EntityTypeConfiguration<BusinessLicenseAttach>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessLicenseAttachMapMySql()
        {
            ToTable("businesslicenseattach");
            HasKey(p => p.BusinessLicenseAttackId);
            Property(p => p.BusinessLicenseId).IsRequired();
            Property(p => p.FilePath).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.FileLocationName).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.FileLocationKey).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.IdentityFolder).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.FileLocationId);
            
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicenseAttachMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng BusinessLicenseAttach trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class BusinessLicenseAttachMapSqlServer : EntityTypeConfiguration<BusinessLicenseAttach>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessLicenseAttachMapSqlServer()
        {
            ToTable("businesslicenseattach");
            HasKey(p => p.BusinessLicenseAttackId);
            Property(p => p.BusinessLicenseId).IsRequired();
            Property(p => p.FilePath).HasMaxLength(255);
            Property(p => p.FileLocationName).HasMaxLength(50);
            Property(p => p.FileLocationKey).HasMaxLength(50);
            Property(p => p.IdentityFolder).HasMaxLength(50);
            //Property(p => p.FileLocationId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicenseAttachMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng BusinessLicenseAttach trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class BusinessLicenseAttachMapOracle : EntityTypeConfiguration<BusinessLicenseAttach>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessLicenseAttachMapOracle()
        {

        }
    }
}
