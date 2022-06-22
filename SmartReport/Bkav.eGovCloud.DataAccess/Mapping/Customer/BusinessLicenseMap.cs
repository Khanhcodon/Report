using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicenseMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BusinessLicense&gt;
    /// Create Date : 261013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng BusinessLicense trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BusinessLicenseMapMySql : EntityTypeConfiguration<BusinessLicense>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessLicenseMapMySql()
        {
            ToTable("businesslicense");
            Property(p => p.BusinessId).IsRequired();
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.LicenseStatusId).IsRequired();
            Property(p => p.LicenseCode).IsRequired().HasMaxLength(20).HasColumnType("varchar");
            Property(p => p.LicenseNumber).IsRequired().HasMaxLength(20).HasColumnType("varchar");
            Property(p => p.RegisDate).IsRequired();
            Property(p => p.IssueDate).IsRequired();
            Property(p => p.ExpireDate).IsRequired();
            Property(p => p.CreateByUserId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicenseMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BusinessLicense&gt;
    /// Create Date : 261013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng BusinessLicense trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class BusinessLicenseMapSqlServer : EntityTypeConfiguration<BusinessLicense>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessLicenseMapSqlServer()
        {
            ToTable("businesslicense");
            Property(p => p.BusinessId).IsRequired();
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.LicenseStatusId).IsRequired();
            Property(p => p.LicenseCode).IsRequired().HasMaxLength(20).HasColumnType("varchar");
            Property(p => p.LicenseNumber).IsRequired().HasMaxLength(20).HasColumnType("varchar");
            Property(p => p.RegisDate).IsRequired();
            Property(p => p.IssueDate).IsRequired();
            Property(p => p.ExpireDate).IsRequired(); 
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicenseMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BusinessLicense&gt;
    /// Create Date : 261013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng BusinessLicense trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class BusinessLicenseMapOracle : EntityTypeConfiguration<BusinessLicense>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessLicenseMapOracle()
        {
            
        }
    }
}
