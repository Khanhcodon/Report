using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountDomainMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountDomain&gt;
    /// Create Date : 120912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AccountDomain trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class AccountDomainMapMySql : EntityTypeConfiguration<AccountDomain>
    {
        /// <summary>
        /// Mapping
        /// </summary>
        public AccountDomainMapMySql()
        {
            ToTable("account_domain");
            Property(p => p.AccountId).IsRequired();
            Property(p => p.DomainId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountDomainMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountDomain&gt;
    /// Create Date : 120912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AccountDomain trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class AccountDomainMapSqlServer : EntityTypeConfiguration<AccountDomain>
    {
        /// <summary>
        /// Mapping
        /// </summary>
        public AccountDomainMapSqlServer()
        {
            ToTable("account_domain");
            Property(p => p.AccountId).IsRequired();
            Property(p => p.DomainId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountDomainMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AccountDomain&gt;
    /// Create Date : 120912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AccountDomain trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class AccountDomainMapOracle : EntityTypeConfiguration<AccountDomain>
    {
        /// <summary>
        /// Mapping
        /// </summary>
        public AccountDomainMapOracle()
        {
            
        }
    }
}
