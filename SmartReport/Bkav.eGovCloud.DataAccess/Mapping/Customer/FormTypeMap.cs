using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormTypeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FormType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng FormType trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class FormTypeMapMySql : EntityTypeConfiguration<FormType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormTypeMapMySql()
        {
            ToTable("formtype");
            Property(p => p.FormTypeName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormTypeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FormType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng FormType trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class FormTypeMapSqlServer : EntityTypeConfiguration<FormType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormTypeMapSqlServer()
        {
            ToTable("formtype");
            Property(p => p.FormTypeName).IsRequired().HasMaxLength(255);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormTypeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FormType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng FormType trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class FormTypeMapOracle : EntityTypeConfiguration<FormType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormTypeMapOracle()
        {
            
        }
    }
}
