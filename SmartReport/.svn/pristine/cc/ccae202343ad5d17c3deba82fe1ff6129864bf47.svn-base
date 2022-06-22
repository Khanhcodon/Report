using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RequiredSupplementaryMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;RequiredSupplementary&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng RequiredSupplementary trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class RequiredSupplementaryMapMySql : EntityTypeConfiguration<RequiredSupplementary>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RequiredSupplementaryMapMySql()
        {
            ToTable("requiredsupplementary");
            Property(p => p.RequiredSupplementaryId).IsRequired();
            Property(p => p.DocTypeId);
            Property(p => p.DocFieldId);
            Property(p => p.UserId);
            Property(p => p.Name).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RequiredSupplementaryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;RequiredSupplementary&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng RequiredSupplementary trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class RequiredSupplementaryMapSqlServer : EntityTypeConfiguration<RequiredSupplementary>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RequiredSupplementaryMapSqlServer()
        {
            ToTable("requiredsupplementary");
            Property(p => p.RequiredSupplementaryId).IsRequired();
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier");
            //Property(p => p.DocFieldId);
            //Property(p => p.UserId);
            Property(p => p.Name).IsRequired().HasMaxLength(1000);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RequiredSupplementaryMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;RequiredSupplementary&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng RequiredSupplementary trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class RequiredSupplementaryMapOracle : EntityTypeConfiguration<RequiredSupplementary>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RequiredSupplementaryMapOracle()
        {

        }
    }
}
