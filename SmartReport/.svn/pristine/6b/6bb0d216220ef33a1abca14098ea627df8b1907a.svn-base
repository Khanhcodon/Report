using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PaperMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Paper&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Paper trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class PaperMapMySql : EntityTypeConfiguration<Paper>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PaperMapMySql()
        {
            ToTable("paper");
            Property(p => p.PaperTypeId).IsRequired();
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.PaperName).IsRequired().HasColumnType("text").HasMaxLength(3000);
            Property(p => p.IsRequired).IsRequired().HasColumnType("bit");
            Property(p => p.Amount).IsRequired();
            Property(p => p.Order).IsRequired();
            Ignore(p => p.PaperTypeIdInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PaperMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Paper&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Paper trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class PaperMapSqlServer : EntityTypeConfiguration<Paper>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PaperMapSqlServer()
        {
            ToTable("paper");
            Property(p => p.PaperTypeId).IsRequired();
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.PaperName).IsRequired().HasMaxLength(1000);
            Property(p => p.IsRequired).IsRequired();//.HasColumnType("bit");
            Property(p => p.Amount).IsRequired();
            Property(p => p.Order).IsRequired();
            Ignore(p => p.PaperTypeIdInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PaperMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Paper&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Paper trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class PaperMapOracle : EntityTypeConfiguration<Paper>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PaperMapOracle()
        {

        }
    }
}
