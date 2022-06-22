using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WeekendMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Weekend&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Weekend trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class WeekendMapMySql : EntityTypeConfiguration<Weekend>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public WeekendMapMySql()
        {
            ToTable("weekend");
            HasKey(p => p.DayId);
            Property(p => p.DayName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WeekendMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Weekend&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Weekend trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class WeekendMapSqlServer : EntityTypeConfiguration<Weekend>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public WeekendMapSqlServer()
        {
            ToTable("weekend");
            HasKey(p => p.DayId);
            Property(p => p.DayName).IsRequired().HasMaxLength(255);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WeekendMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Weekend&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Weekend trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class WeekendMapOracle : EntityTypeConfiguration<Weekend>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public WeekendMapOracle()
        {
            
        }
    }
}
