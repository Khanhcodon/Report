using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Business trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BusinessMapMySql : EntityTypeConfiguration<Business>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessMapMySql()
        {
            ToTable("business");
            Property(p => p.BusinessTypeId).IsRequired();
            Property(p => p.ForeignName).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.BusinessName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.AbbreviationName).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.BusinessCode).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.IssueCodeby).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.Phone).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.Fax).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.Email).HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.Website).HasMaxLength(100).HasColumnType("varchar");
            Property(p => p.Address).IsRequired().HasMaxLength(200).HasColumnType("varchar");
            Property(p => p.CityCode).HasMaxLength(10).HasColumnType("varchar");
            Property(p => p.DistrictCode).HasMaxLength(10).HasColumnType("varchar");
            Property(p => p.IssueDate).IsRequired();
            Property(p => p.UserName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.PermanentAddress).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.TemporaryAddress).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.UserPhone).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.UserEmail).HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.IdCard).HasMaxLength(10).HasColumnType("varchar");
            Property(p => p.IdCardPlace).HasMaxLength(50).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FeeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Business trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class BusinessMapSqlServer : EntityTypeConfiguration<Business>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessMapSqlServer()
        {
            ToTable("business");
            Property(p => p.BusinessTypeId).IsRequired();
            Property(p => p.ForeignName).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.BusinessName).IsRequired().HasMaxLength(255);
            Property(p => p.AbbreviationName).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.BusinessCode).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.IssueCodeby).IsRequired().HasMaxLength(50);
            Property(p => p.Phone).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.Fax).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.Email).HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.Website).HasMaxLength(100).HasColumnType("varchar");
            Property(p => p.Address).IsRequired().HasMaxLength(200);
            Property(p => p.CityCode).HasMaxLength(10).HasColumnType("varchar");
            Property(p => p.DistrictCode).HasMaxLength(10).HasColumnType("varchar");
            Property(p => p.IssueDate).IsRequired();
            Property(p => p.UserName).IsRequired().HasMaxLength(50);
            Property(p => p.PermanentAddress).HasMaxLength(255);
            Property(p => p.TemporaryAddress).HasMaxLength(255);
            Property(p => p.UserPhone).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.UserEmail).HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.IdCard).HasMaxLength(10).HasColumnType("varchar");
            Property(p => p.IdCardPlace).HasMaxLength(50);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FeeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Business trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class BusinessMapOracle : EntityTypeConfiguration<Business>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessMapOracle()
        {
            
        }
    }
}
