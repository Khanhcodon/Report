using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CategoryMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Category&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Category trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CategoryMapMySql : EntityTypeConfiguration<Category>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CategoryMapMySql()
        {
            ToTable("category");
            Property(p => p.CategoryName).IsOptional().HasMaxLength(255).HasColumnType("varchar");
            Ignore(p => p.CodeIds);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CategoryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Category&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Category trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CategoryMapSqlServer : EntityTypeConfiguration<Category>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CategoryMapSqlServer()
        {
            ToTable("category");
            Property(p => p.CategoryName).IsOptional().HasMaxLength(255);
            Ignore(p => p.CodeIds);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CategoryMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Category&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Category trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CategoryMapOracle : EntityTypeConfiguration<Category>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CategoryMapOracle()
        {

        }
    }
}
