using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionTypeMapMySql - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionType&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunctionType trong CSDL MySql</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionTypeMapMySql : EntityTypeConfiguration<ProcessFunctionType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionTypeMapMySql()
        {
            ToTable("processfunctiontype");
            Property(c => c.Name).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(c => c.Query).IsRequired().HasColumnType("text");
            Property(c => c.TextField).IsRequired().HasMaxLength(32).HasColumnType("varchar");
            Property(c => c.Param).HasMaxLength(256).HasColumnType("varchar");
            Ignore(c => c.ListParam);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionTypeMapSqlServer - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionType&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunctionType trong CSDL SqlServer</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionTypeMapSqlServer : EntityTypeConfiguration<ProcessFunctionType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionTypeMapSqlServer()
        {
            ToTable("processfunctiontype");
            Property(c => c.Name).IsRequired().HasMaxLength(128);
            Property(c => c.Query).IsRequired();//.HasColumnType("ntext");
            Property(c => c.TextField).IsRequired().HasMaxLength(32);
            Property(c => c.Param).HasMaxLength(256);
            Ignore(c => c.ListParam);
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionTypeMapOracle - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionType&gt;</para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Mapping tương ứng với bảng ProcessFunctionType trong CSDL Oracle</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionTypeMapOracle : EntityTypeConfiguration<ProcessFunctionType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionTypeMapOracle()
        {

        }
    }
}
