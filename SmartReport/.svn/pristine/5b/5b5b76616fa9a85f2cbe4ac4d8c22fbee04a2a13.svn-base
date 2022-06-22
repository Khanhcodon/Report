using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FormGroup&gt;
    /// Create Date : 011013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng FormGroup trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class FormGroupMapMySql : EntityTypeConfiguration<FormGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormGroupMapMySql()
        {
            ToTable("formgroup");
            Property(p => p.FormGroupName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : IncreaseMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Increase&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Increase trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class FormGroupMapSqlServer : EntityTypeConfiguration<FormGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormGroupMapSqlServer()
        {
            ToTable("formgroup");
            Property(p => p.FormGroupName).IsRequired().HasMaxLength(255);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormGroupMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FormGroup&gt;
    /// Create Date : 011013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng FormGroup trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class FormGroupMapOracle : EntityTypeConfiguration<FormGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormGroupMapOracle()
        {
            
        }
    }
}
