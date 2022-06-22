using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov </para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionFilterMapMySql - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionFilter&gt;</para>
    /// <para>Create Date : 05/01/2015</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunctionFilter trong CSDL MySql</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionFilterMapMySql : EntityTypeConfiguration<ProcessFunctionFilter>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionFilterMapMySql()
        {
            ToTable("processfunctionfilter");
            HasKey(p => p.ProcessFunctionFilterId);
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(p => p.Value).HasColumnType("text");
            Property(p => p.IsSqlValue).HasColumnType("bit").IsRequired();
            Property(p => p.DataField).HasColumnType("varchar").IsRequired().HasMaxLength(100);
            Property(p => p.FilterExpression).IsRequired();
            Property(p => p.IsAutoGenNodeName).HasColumnType("bit");
            Property(p => p.NodeNameTemp).HasColumnType("varchar").HasMaxLength(400);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov </para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionFilterMapSqlServer - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionFilter&gt;</para>
    /// <para>Create Date : 05/01/2015</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunctionFilter trong CSDL SqlServer</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionFilterMapSqlServer : EntityTypeConfiguration<ProcessFunctionFilter>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionFilterMapSqlServer()
        {
            ToTable("processfunctionfilter");
            HasKey(p => p.ProcessFunctionFilterId);
            Property(p => p.Name).HasMaxLength(300).IsRequired();
            //Property(p => p.Value).HasColumnType("ntext");
            Property(p => p.IsSqlValue).IsRequired();//.HasColumnType("bit");
            Property(p => p.DataField).IsRequired().HasMaxLength(100);
            Property(p => p.FilterExpression).IsRequired();
            //Property(p => p.IsAutoGenNodeName).HasColumnType("bit");
            Property(p => p.NodeNameTemp).HasMaxLength(400);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov </para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionFilterMapOracle - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionFilter&gt;</para>
    /// <para>Create Date : 05/01/2015</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunctionFilter trong CSDL Oracle</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionFilterMapOracle : EntityTypeConfiguration<ProcessFunctionFilter>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionFilterMapOracle()
        {

        }
    }
}
