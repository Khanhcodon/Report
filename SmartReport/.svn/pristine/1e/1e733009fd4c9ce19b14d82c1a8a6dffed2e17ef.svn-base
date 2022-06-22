using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ColumnSettingMapMySql - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;DocColumnSetting&gt;</para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng DocColumnSetting trong CSDL MySql</para>
    /// </summary>
    [ComVisible(false)]
    public class DocColumnSettingMapMySql : EntityTypeConfiguration<DocColumnSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocColumnSettingMapMySql()
        {
            ToTable("doc_column_setting");
            HasKey(p => p.DocColumnSettingId);
            Property(c => c.DocColumnSettingName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.DisplayColumn).IsRequired().HasColumnType("text");
            Property(c => c.SortColumn).IsRequired().HasColumnType("text");
            Property(c => c.GroupColumn).HasColumnType("text");
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ColumnSettingMapSqlServer - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;DocColumnSetting&gt;</para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng DocColumnSetting trong CSDL SqlServer</para>
    /// </summary>
    [ComVisible(false)]
    public class DocColumnSettingMapSqlServer : EntityTypeConfiguration<DocColumnSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocColumnSettingMapSqlServer()
        {
            ToTable("doc_column_setting");
            HasKey(p => p.DocColumnSettingId);
            Property(c => c.DocColumnSettingName).IsRequired().HasMaxLength(255);
            Property(c => c.DisplayColumn).IsRequired();//.HasColumnType("ntext");
            Property(c => c.SortColumn).IsRequired();//.HasColumnType("text");
            Property(c => c.GroupColumn).HasColumnType("text");
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ColumnSettingMapOracle - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;DocColumnSetting&gt;</para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng DocColumnSetting trong CSDL Oracle</para>
    /// </summary>
    [ComVisible(false)]
    public class DocColumnSettingMapOracle : EntityTypeConfiguration<DocColumnSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocColumnSettingMapOracle()
        {

        }
    }
}
