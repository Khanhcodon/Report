using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TransferTypeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TransferType&gt;
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng TransferType trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class TransferTypeMapMySql : EntityTypeConfiguration<TransferType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TransferTypeMapMySql()
        {
            ToTable("transfertype");
            Property(p => p.TransferTypeName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TransferTypeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TransferType&gt;
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng TransferType trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class TransferTypeMapSqlServer : EntityTypeConfiguration<TransferType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TransferTypeMapSqlServer()
        {
            ToTable("transfertype");
            Property(p => p.TransferTypeName).IsRequired().HasMaxLength(50);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TransferTypeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TransferType&gt;
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng TransferType trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class TransferTypeMapOracle : EntityTypeConfiguration<TransferType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TransferTypeMapOracle()
        {
            
        }
    }
}
