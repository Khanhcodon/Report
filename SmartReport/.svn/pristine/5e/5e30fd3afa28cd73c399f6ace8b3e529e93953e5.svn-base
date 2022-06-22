using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Mapping.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SettingMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Setting&gt;
    /// Create Date : 140812
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Setting trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SettingMapMySql : EntityTypeConfiguration<Setting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SettingMapMySql()
        {
            ToTable("setting");
            Property(c => c.SettingKey).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.SettingValue).IsRequired().HasColumnType("text");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SettingMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Setting&gt;
    /// Create Date : 140812
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Setting trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class SettingMapSqlServer : EntityTypeConfiguration<Setting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SettingMapSqlServer()
        {
            ToTable("setting");
            Property(c => c.SettingKey).IsRequired().HasMaxLength(255);
            Property(c => c.SettingValue).IsRequired().HasColumnType("ntext");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SettingMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Setting&gt;
    /// Create Date : 140812
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Setting trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class SettingMapOracle : EntityTypeConfiguration<Setting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SettingMapOracle()
        {
        }
    }
}
