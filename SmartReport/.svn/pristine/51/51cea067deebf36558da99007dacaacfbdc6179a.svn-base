using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DistrictMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;District&gt;
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng District trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DistrictMapMySql : EntityTypeConfiguration<District>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DistrictMapMySql()
        {
            ToTable("district");
            Property(p => p.DistrictName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(p => p.CityCode).IsRequired().HasMaxLength(10).HasColumnType("varchar");
            Property(p => p.DistrictCode).IsRequired().HasMaxLength(10).HasColumnType("varchar");
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
    public class DistrictMapSqlServer : EntityTypeConfiguration<District>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DistrictMapSqlServer()
        {
            ToTable("district");
            Property(p => p.DistrictName).IsRequired().HasMaxLength(50);
            Property(p => p.CityCode).HasColumnType("varchar").IsRequired().HasMaxLength(10);
            Property(p => p.DistrictCode).HasColumnType("varchar").IsRequired().HasMaxLength(10);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DistrictMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;District&gt;
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng District trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DistrictMapOracle : EntityTypeConfiguration<District>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DistrictMapOracle()
        {
            
        }
    }
}
