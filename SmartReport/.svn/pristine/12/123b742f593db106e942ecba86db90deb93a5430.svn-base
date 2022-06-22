using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Mapping.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Resource&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Resource trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ResourceMapMySql : EntityTypeConfiguration<Resource>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ResourceMapMySql()
        {
            ToTable("resource");
            Property(c => c.ResourceKey).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.ResourceKey).IsRequired().HasColumnType("text");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Resource&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Resource trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ResourceMapSqlServer : EntityTypeConfiguration<Resource>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ResourceMapSqlServer()
        {
            ToTable("resource");
            Property(c => c.ResourceKey).IsRequired().HasMaxLength(255);
            Property(c => c.ResourceKey).IsRequired().HasColumnType("ntext");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Resource&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Resource trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ResourceMapOracle : EntityTypeConfiguration<Resource>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ResourceMapOracle()
        {
            
        }
    }


    ///// <summary>
    ///// TrinhNVd: Mapping tương ứng với bảng Resource trong CSDL Oracle
    ///// </summary>
    //[ComVisible(false)]
    //public class ResourceEngMapMySql : EntityTypeConfiguration<ResourceEng>
    //{
    //    ///<summary>
    //    /// Mapping property
    //    ///</summary>
    //    public ResourceEngMapMySql()
    //    {
    //        ToTable("resource_en");
    //        Property(c => c.ResourceKey).IsRequired().HasMaxLength(255).HasColumnType("varchar");
    //        Property(c => c.ResourceKey).IsRequired().HasColumnType("text");
    //    }
    //}

    ///// <summary>
    ///// TrinhNVd: Mapping tương ứng với bảng Resource trong CSDL Oracle
    ///// </summary>
    //[ComVisible(false)]
    //public class ResourceEngMapSqlServer : EntityTypeConfiguration<ResourceEng>
    //{
    //    ///<summary>
    //    /// Mapping property
    //    ///</summary>
    //    public ResourceEngMapSqlServer()
    //    {
    //        ToTable("resource_en");
    //        Property(c => c.ResourceKey).IsRequired().HasMaxLength(255);
    //        Property(c => c.ResourceKey).IsRequired().HasColumnType("ntext");
    //    }
    //}

    ///// <summary>
    ///// TrinhNVd: Mapping tương ứng với bảng Resource trong CSDL Oracle
    ///// </summary>
    //[ComVisible(false)]
    //public class ResourceEngMapOracle : EntityTypeConfiguration<ResourceEng>
    //{
    //    ///<summary>
    //    /// Mapping property
    //    ///</summary>
    //    public ResourceEngMapOracle()
    //    {

    //    }
    //}
}
