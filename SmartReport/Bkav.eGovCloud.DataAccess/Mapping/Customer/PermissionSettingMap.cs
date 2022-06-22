using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionMapMySql - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;PermissionSetting&gt;</para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng PermissionSetting trong CSDL MySql</para>
    /// </summary>
    [ComVisible(false)]
    public class PermissionSettingMapMySql : EntityTypeConfiguration<PermissionSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PermissionSettingMapMySql()
        {
            ToTable("permission_setting");
            HasKey(p => p.PermissionSettingId);
            Property(c => c.PermissionSettingName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.DepartmentPositionHasPermission).HasColumnType("text");
            Property(c => c.PositionHasPermission).HasColumnType("text");
            Property(c => c.UserHasPermission).HasColumnType("text");

            Ignore(c => c.ListDepartmentPositionHasPermission);
            Ignore(c => c.ListPositionHasPermission);
            Ignore(c => c.ListUserHasPermission);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : PermissionSettingMapSqlServer - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;PermissionSetting&gt;</para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng PermissionSetting trong CSDL SqlServer</para>
    /// </summary>
    [ComVisible(false)]
    public class PermissionSettingMapSqlServer : EntityTypeConfiguration<PermissionSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PermissionSettingMapSqlServer()
        {
            ToTable("permission_setting");
            HasKey(p => p.PermissionSettingId);
            Property(c => c.PermissionSettingName).IsRequired().HasMaxLength(255);
            //Property(c => c.DepartmentPositionHasPermission).HasColumnType("ntext");
            //Property(c => c.PositionHasPermission).HasColumnType("ntext");
            //Property(c => c.UserHasPermission).HasColumnType("ntext");

            Ignore(c => c.ListDepartmentPositionHasPermission);
            Ignore(c => c.ListPositionHasPermission);
            Ignore(c => c.ListUserHasPermission);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : PermissionSettingMapOracle - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;PermissionSetting&gt;</para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng PermissionSetting trong CSDL Oracle</para>
    /// </summary>
    [ComVisible(false)]
    public class PermissionSettingMapOracle : EntityTypeConfiguration<PermissionSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PermissionSettingMapOracle()
        {

        }
    }
}
