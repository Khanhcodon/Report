using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FileLocationMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FileLocation&gt;
    /// Create Date : 070313
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng FileLocation trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class FileLocationMapMySql : EntityTypeConfiguration<FileLocation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FileLocationMapMySql()
        {
            ToTable("filelocation");
            Property(p => p.FileLocationAddress).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.FileLocationType).IsRequired().HasColumnType("bit");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Ignore(p => p.FileLocationTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FileLocationMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FileLocation&gt;
    /// Create Date : 070313
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng FileLocation trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class FileLocationMapSqlServer : EntityTypeConfiguration<FileLocation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FileLocationMapSqlServer()
        {
            ToTable("filelocation");
            Property(p => p.FileLocationAddress).IsRequired().HasMaxLength(255);
            Property(p => p.FileLocationType).IsRequired();
            Property(p => p.IsActivated).IsRequired();
            Ignore(p => p.FileLocationTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FileLocationMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FileLocation&gt;
    /// Create Date : 070313
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng FileLocation trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class FileLocationMapOracle : EntityTypeConfiguration<FileLocation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FileLocationMapOracle()
        {
            
        }
    }
}
