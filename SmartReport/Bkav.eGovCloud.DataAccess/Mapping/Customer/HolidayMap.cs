using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : HolidayMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Holiday&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Holiday trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class HolidayMapMySql : EntityTypeConfiguration<Holiday>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public HolidayMapMySql()
        {
            ToTable("holiday");
            HasKey(p => p.HolidayId);
            Property(p => p.IsExtendHoliday).IsRequired().HasColumnType("bit");
            Property(p => p.HolidayName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.HolidayDate).IsRequired();
            Property(p => p.IsRepeated).IsRequired().HasColumnType("bit");
            Property(p => p.IsLunar).IsRequired().HasColumnType("bit");
            Property(p => p.IsExtendHoliday).IsRequired().HasColumnType("bit");
            Property(p => p.HolidayRange).IsRequired();
            Ignore(p => p.HolidayDateInLunar);
            Ignore(p => p.HolidayDateInSolar);

            //Hopcv ; createDate: 030414
            Property(p => p.IsHoliday).HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : HolidayMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Holiday&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Holiday trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class HolidayMapSqlServer : EntityTypeConfiguration<Holiday>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public HolidayMapSqlServer()
        {
            ToTable("holiday");
            HasKey(p => p.HolidayId);
            Property(p => p.IsExtendHoliday).IsRequired();//.HasColumnType("bit");
            Property(p => p.HolidayName).IsRequired().HasMaxLength(64);
            Property(p => p.HolidayDate).IsRequired();
            Property(p => p.IsRepeated).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsLunar).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsExtendHoliday).IsRequired();//.HasColumnType("bit");
            Property(p => p.HolidayRange).IsRequired();
            Ignore(p => p.HolidayDateInLunar);
            Ignore(p => p.HolidayDateInSolar);

            //Hopcv ; createDate: 030414
            //Property(p => p.IsHoliday).HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : HolidayMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Holiday&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Holiday trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class HolidayMapOracle : EntityTypeConfiguration<Holiday>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public HolidayMapOracle()
        {

        }
    }
}
