using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PrinterMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Printer&gt;
    /// Create Date : 261013
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Printer trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class PrinterMapMySql : EntityTypeConfiguration<Printer>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public PrinterMapMySql()
        {
            ToTable("printer");
            Property(p => p.PrinterId).IsRequired();
            Property(p => p.PrinterName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.ShareName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.IsActivated).HasColumnType("bit");
            Property(p => p.IsShared).HasColumnType("bit");
            Property(p => p.UserIds).HasColumnType("varchar");
            Property(p => p.DepartmentPositions).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PrinterMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Printer&gt;
    /// Create Date : 261013
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Printer trong CSDL sqlserver
    /// </summary>
    [ComVisible(false)]
    public class PrinterMapSqlServer : EntityTypeConfiguration<Printer>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public PrinterMapSqlServer()
        {
            ToTable("printer");
            Property(p => p.PrinterId).IsRequired();
            Property(p => p.PrinterName).IsRequired().HasMaxLength(255);
            Property(p => p.ShareName).IsRequired().HasMaxLength(255);
            //Property(p => p.IsActivated).HasColumnType("bit");
            //Property(p => p.IsShared).HasColumnType("bit");
            Property(p => p.UserIds).HasColumnType("varchar");
            //Property(p => p.DepartmentPositions);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PrinterMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Printer&gt;
    /// Create Date : 261013
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Printer trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class PrinterMapOracle : EntityTypeConfiguration<Printer>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public PrinterMapOracle()
        {
        }
    }
}
