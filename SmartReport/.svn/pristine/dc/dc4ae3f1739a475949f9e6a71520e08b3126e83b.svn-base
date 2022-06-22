using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessTypeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;BusinessType&gt;
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng BusinessType trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BusinessTypeMapMySql : EntityTypeConfiguration<BusinessType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessTypeMapMySql()
        {
            ToTable("businesstype");
            Property(p => p.BusinessTypeName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessTypeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng BusinessType trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class BusinessTypeMapSqlServer : EntityTypeConfiguration<BusinessType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessTypeMapSqlServer()
        {
            ToTable("businesstype");
            Property(p => p.BusinessTypeName).IsRequired().HasMaxLength(255);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessTypeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng BusinessType trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class BusinessTypeMapOracle : EntityTypeConfiguration<BusinessType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BusinessTypeMapOracle()
        {
            
        }
    }
}
