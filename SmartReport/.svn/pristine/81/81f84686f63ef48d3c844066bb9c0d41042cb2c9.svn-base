using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : KeyWordMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;KeyWord&gt;
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng KeyWord trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class KeyWordMapMySql : EntityTypeConfiguration<KeyWord>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public KeyWordMapMySql()
        {
            ToTable("keyword");
            Property(p => p.KeyWordId);
            Property(p => p.KeyWordName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : KeyWordMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;KeyWord&gt;
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng KeyWord trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class KeyWordMapSqlServer : EntityTypeConfiguration<KeyWord>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public KeyWordMapSqlServer()
        {
            ToTable("keyword");
            //Property(p => p.KeyWordId);
            Property(p => p.KeyWordName).IsRequired().HasMaxLength(50);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : KeyWordMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;KeyWord&gt;
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng KeyWord trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class KeyWordMapOracle : EntityTypeConfiguration<KeyWord>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public KeyWordMapOracle()
        {
            
        }
    }
}
