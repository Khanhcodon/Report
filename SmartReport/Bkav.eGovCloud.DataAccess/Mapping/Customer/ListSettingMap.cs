using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ListSettingMapMySql - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ListSetting&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ListSetting trong CSDL MySql</para>
    /// </summary>
    [ComVisible(false)]
    public class ListSettingMapMySql : EntityTypeConfiguration<ListSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ListSettingMapMySql()
        {
            ToTable("listsetting");
            Property(c => c.SettingContent).HasColumnType("text");
            Property(c => c.ProcessFunctionId).IsRequired();
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ListSettingMapSqlServer - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ListSetting&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ListSetting trong CSDL SqlServer</para>
    /// </summary>
    [ComVisible(false)]
    public class ListSettingMapSqlServer : EntityTypeConfiguration<ListSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ListSettingMapSqlServer()
        {
            ToTable("listsetting");
            //Property(c => c.SettingContent).HasColumnType("ntext");
            Property(c => c.ProcessFunctionId).IsRequired();
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ListSettingMapOracle - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ListSetting&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ListSetting trong CSDL Oracle</para>
    /// </summary>
    [ComVisible(false)]
    public class ListSettingMapOracle : EntityTypeConfiguration<ListSetting>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ListSettingMapOracle()
        {

        }
    }
}
