using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CodeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CodeTemp&gt;
    /// Create Date : 16/09/2015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng CodeTemp trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CodeTempMapMySql : EntityTypeConfiguration<CodeTemp>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CodeTempMapMySql()
        {
            ToTable("codetemp");
            HasKey(p => p.CodeTempId);
            Property(p => p.CodeId).IsRequired();
            Property(p => p.Code).HasColumnType("varchar").IsRequired().HasMaxLength(100);
            Property(p => p.Type).IsRequired();
            Ignore(p => p.CodeTempTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CodeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CodeTemp&gt;
    /// Create Date : 16/09/2015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng CodeTemp trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CodeTempMapSqlServer : EntityTypeConfiguration<CodeTemp>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CodeTempMapSqlServer()
        {
            ToTable("CodeTemp");
            HasKey(p => p.CodeTempId);
            Property(p => p.CodeId).IsRequired();
            Property(p => p.Code).IsRequired().HasMaxLength(100);
            Property(p => p.Type).IsRequired();
            Ignore(p => p.CodeTempTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CodeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CodeTemp&gt;
    /// Create Date : 16/09/2015
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng CodeTemp trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CodeTempMapOracle : EntityTypeConfiguration<CodeTemp>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CodeTempMapOracle()
        {
            
        }
    }
}
