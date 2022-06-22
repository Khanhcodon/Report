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
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunction&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunction trong CSDL MySql</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionMapMySql : EntityTypeConfiguration<ProcessFunction>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionMapMySql()
        {
            ToTable("processfunction");
            Property(c => c.Name).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            //Property(c => c.ProcessFunctionGroupId).IsRequired();
            Property(c => c.IsActivated).HasColumnType("bit");
            Property(c => c.IsEnablePaging).HasColumnType("bit");
            Property(c => c.QueryLatest).HasColumnType("text");
            Property(c => c.QueryOlder).HasColumnType("text");
            Property(c => c.QueryItemRemove).HasColumnType("text");
            Property(c => c.QueryCountItemUnread).HasColumnType("text");
            Property(c => c.QueryPaging).HasColumnType("text");
            Property(c => c.QueryCountAllItems).HasColumnType("text");
            Property(c => c.Color).HasMaxLength(16).HasColumnType("varchar");
            Property(c => c.Icon).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.Order).IsRequired();
            Property(c => c.DateFilter).HasMaxLength(20).HasColumnType("varchar");
            Property(c => c.DateFilterView).HasMaxLength(20).HasColumnType("varchar");
            Property(c => c.IsDateFilter).HasColumnType("bit");
            Property(c => c.IsOverdueFilter).HasColumnType("bit");
            Property(c => c.Type);
            Property(c => c.DateModified).HasColumnType("datetime");
            Property(c => c.ShowTotalInTreeType);
            Property(c => c.HasUyQuyen).HasColumnType("bit");
            Property(c => c.HasTransferTheoLo).HasColumnType("bit");
            Property(c => c.TreeGroupId);
            Property(c => c.ExportFileConfig).HasMaxLength(1000).HasColumnType("varchar");
            Property(c => c.HasExportFile).HasColumnType("bit");
            Property(c => c.QueryExportDataToFile).HasColumnType("text");

            Property(c => c.DocColumnSettingId);
            Property(c => c.PermissionSettingId);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionMapSqlServer - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunction&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunction trong CSDL SqlServer</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionMapSqlServer : EntityTypeConfiguration<ProcessFunction>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionMapSqlServer()
        {
            ToTable("processfunction");
            Property(c => c.Name).IsRequired().HasMaxLength(128);
            //Property(c => c.ProcessFunctionGroupId).IsRequired();
            //Property(c => c.IsActivated).HasColumnType("bit");
            //Property(c => c.IsEnablePaging).HasColumnType("bit");
            //Property(c => c.QueryLatest).HasColumnType("ntext");
            //Property(c => c.QueryOlder).HasColumnType("ntext");
            //Property(c => c.QueryItemRemove).HasColumnType("ntext");
            //Property(c => c.QueryCountItemUnread).HasColumnType("ntext");
            //Property(c => c.QueryPaging).HasColumnType("ntext");
            //Property(c => c.QueryCountAllItems).HasColumnType("ntext");
            Property(c => c.Color).HasMaxLength(16);
            Property(c => c.Icon).HasMaxLength(255);
            Property(c => c.Order).IsRequired();
            Property(c => c.DateFilter).HasMaxLength(20);
            Property(c => c.DateFilterView).HasMaxLength(20);
            //Property(c => c.IsDateFilter).HasColumnType("bit");
            //Property(c => c.IsOverdueFilter).HasColumnType("bit");
            //Property(c => c.Type);
            //Property(c => c.DateModified).HasColumnType("datetime");
            //Property(c => c.ShowTotalInTreeType);
            //Property(c => c.HasUyQuyen).HasColumnType("bit");
            //Property(c => c.HasTransferTheoLo).HasColumnType("bit");
            //Property(c => c.TreeGroupId);
            Property(c => c.ExportFileConfig).HasMaxLength(1000);
            //Property(c => c.HasExportFile).HasColumnType("bit");
            //Property(c => c.QueryExportDataToFile).HasColumnType("ntext");

            //Property(c => c.DocColumnSettingId);
            //Property(c => c.PermissionSettingId);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionMapOracle - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunction&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunction trong CSDL Oracle</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionMapOracle : EntityTypeConfiguration<ProcessFunction>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionMapOracle()
        {

        }
    }
}
