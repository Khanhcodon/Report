using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng ReportGroup trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ReportGroupMapMySql : EntityTypeConfiguration<ReportGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportGroupMapMySql()
        {
            ToTable("reportgroup");
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(p => p.Query).HasColumnType("text");
            Property(p => p.FieldName).HasColumnType("varchar").HasMaxLength(150);
            Property(p => p.FieldDisplay).HasColumnType("varchar").HasMaxLength(150);
            Property(p => p.IsReport).HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportGroupMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng ReportGroup trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ReportGroupMapSqlServer : EntityTypeConfiguration<ReportGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportGroupMapSqlServer()
        {
            ToTable("reportgroup");
            Property(p => p.Name).HasMaxLength(300).IsRequired();
            Property(p => p.Query).HasColumnType("ntext");
            Property(p => p.FieldName).HasMaxLength(150);
            Property(p => p.FieldDisplay).HasMaxLength(150);
            Property(p => p.IsReport);//.HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportGroupMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng ReportGroup trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ReportGroupMapOracle : EntityTypeConfiguration<ReportGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportGroupMapOracle()
        {

        }
    }
}
