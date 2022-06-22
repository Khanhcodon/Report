using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Report Key trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ReportKeyMapMySql : EntityTypeConfiguration<ReportKey>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportKeyMapMySql()
        {
            ToTable("reportkey");
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(p => p.Description).HasColumnType("varchar").HasMaxLength(400);
            Property(p => p.ParentId);
            Property(p => p.Sql).HasColumnType("text");
            Property(p => p.IsActive).HasColumnType("bit").IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Report Key trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ReportKeyMapSqlServer : EntityTypeConfiguration<ReportKey>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportKeyMapSqlServer()
        {
            ToTable("reportkey");
            Property(p => p.Name).HasMaxLength(300).IsRequired();
            Property(p => p.Description).HasMaxLength(400);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Report trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ReportKeyMapOracle : EntityTypeConfiguration<ReportKey>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportKeyMapOracle()
        {

        }
    }
}
