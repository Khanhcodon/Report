using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CityMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;City&gt;
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng City trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CityMapMySql : EntityTypeConfiguration<City>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CityMapMySql()
        {
            ToTable("city");
            Property(p => p.CityName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.CityCode).IsRequired().HasMaxLength(10).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CityMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;City&gt;
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng City trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CityMapSqlServer : EntityTypeConfiguration<City>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CityMapSqlServer()
        {
            ToTable("city");
            Property(p => p.CityName).IsRequired().HasMaxLength(50);
            Property(p => p.CityCode).IsRequired().HasMaxLength(10);
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
    public class CityMapOracle : EntityTypeConfiguration<City>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CityMapOracle()
        {
            
        }
    }
}
