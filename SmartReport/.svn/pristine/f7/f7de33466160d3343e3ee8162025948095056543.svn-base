using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AnticipateMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Anticipate&gt;
    /// Create Date : 180214
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Anticipate trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class AnticipateMapMySql : EntityTypeConfiguration<Anticipate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AnticipateMapMySql()
        {
            ToTable("anticipate");
            HasKey(p => p.AnticipateId);
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.UserId).IsRequired();
            Property(p => p.DocumentCopyId);
            Property(p => p.AnticipateType).IsRequired();
            Property(p => p.Destination).IsRequired().HasColumnType("text");
            Ignore(p => p.AnticipateTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AnticipateMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Anticipate&gt;
    /// Create Date : 180214
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Anticipate trong CSDL Server
    /// </summary>
    [ComVisible(false)]
    public class AnticipateMapSqlServer : EntityTypeConfiguration<Anticipate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AnticipateMapSqlServer()
        {
            ToTable("anticipate");
            HasKey(p => p.AnticipateId);
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.UserId).IsRequired();
            Property(p => p.AnticipateType).HasColumnType("tinyint").IsRequired();
            Property(p => p.Destination).IsRequired();
            Ignore(p => p.AnticipateTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AnticipateMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Anticipate&gt;
    /// Create Date : 180214
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Anticipate trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class AnticipateMapOracle : EntityTypeConfiguration<Anticipate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AnticipateMapOracle()
        {

        }
    }
}
