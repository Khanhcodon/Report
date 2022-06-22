using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ExtendFieldMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ExtendField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng ExtendField trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ExtendFieldMapMySql : EntityTypeConfiguration<ExtendField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ExtendFieldMapMySql()
        {
            ToTable("extendfield");
            Property(p => p.ExtendFieldId).IsRequired();
            Property(p => p.ExtendFieldName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.Mask).IsRequired().HasMaxLength(30).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ExtendFieldMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ExtendField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng ExtendField trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ExtendFieldMapSqlServer : EntityTypeConfiguration<ExtendField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ExtendFieldMapSqlServer()
        {
            ToTable("extendfield");
            Property(p => p.ExtendFieldId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.ExtendFieldName).IsRequired().HasMaxLength(128);
            Property(p => p.Mask).IsRequired().HasMaxLength(30);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ExtendFieldMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ExtendField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng ExtendField trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ExtendFieldMapOracle : EntityTypeConfiguration<ExtendField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ExtendFieldMapOracle()
        {
            
        }
    }
}
