using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov
    /// Project: eGov Cloud v1.0
    /// Class : ProcessFunctionGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ProcessFunctionGroup&gt;
    /// Create Date : 02/01/2015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng ProcessFunctionGroup trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionGroupMapMySql : EntityTypeConfiguration<ProcessFunctionGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionGroupMapMySql()
        {
            ToTable("processfunctiongroup");
            HasKey(p => p.ProcessFunctionGroupId);
            Property(p => p.Name).IsRequired().HasMaxLength(300).HasColumnType("varchar");
            Property(p => p.DataQuery).IsRequired().HasColumnType("text");
            Property(p => p.ClientExpression).HasColumnType("text");
            Property(p => p.ColumnQuery).IsRequired().HasColumnType("text");
            Property(p => p.LimitQuery).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov
    /// Project: eGov Cloud v1.0
    /// Class : ProcessFunctionGroupMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ProcessFunctionGroup&gt;
    /// Create Date : 02/01/2015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng ProcessFunctionGroup trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionGroupMapSqlServer : EntityTypeConfiguration<ProcessFunctionGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ProcessFunctionGroupMapSqlServer()
        {
            ToTable("processfunctiongroup");
            HasKey(p => p.ProcessFunctionGroupId);
            Property(p => p.Name).IsRequired().HasMaxLength(300);
            Property(p => p.DataQuery).IsRequired();//.HasColumnType("ntext");
            //Property(p => p.ClientExpression).HasColumnType("ntext");
            Property(p => p.ColumnQuery).IsRequired();//.HasColumnType("ntext");
            Property(p => p.LimitQuery).IsRequired();
        }
    }

}
