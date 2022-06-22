using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PositionMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Position&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Position trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class PositionMapMySql : EntityTypeConfiguration<Position>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PositionMapMySql()
        {
            ToTable("position");
            Property(p => p.PositionName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            //Property(p => p.PriorityLevel).IsRequired().HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PositionMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Position&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Position trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class PositionMapSqlServer : EntityTypeConfiguration<Position>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PositionMapSqlServer()
        {
            ToTable("position");
            Property(p => p.PositionName).IsRequired().HasMaxLength(64);
           // Property(p => p.PriorityLevel).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PositionMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Position&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Position trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class PositionMapOracle : EntityTypeConfiguration<Position>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public PositionMapOracle()
        {
            
        }
    }
}
