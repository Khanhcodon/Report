using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ShareFolderMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;share_folder&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng share_folder trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ShareFolderMapMySql : EntityTypeConfiguration<ShareFolder>
    {
        /// <summary>
        /// 
        /// </summary>
        public ShareFolderMapMySql()
        {
            ToTable("share_folder");
            HasKey(p => p.ShareFolderId);
            Property(p => p.Directory).IsRequired().HasColumnType("varchar").HasMaxLength(500);
            Property(p => p.UserName).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Password).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.IsNetwork).HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ShareFolderMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;share_folder&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng share_folder trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ShareFolderMapSqlServer : EntityTypeConfiguration<ShareFolder>
    {
        /// <summary>
        /// 
        /// </summary>
        public ShareFolderMapSqlServer()
        {
            ToTable("share_folder");
            HasKey(p => p.ShareFolderId);
            Property(p => p.Directory).IsRequired().HasMaxLength(500);
            Property(p => p.UserName).HasMaxLength(255);
            Property(p => p.Password).HasColumnType("varchar").HasMaxLength(255);
            //Property(p => p.IsNetwork).HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ShareFolderMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;share_folder&gt;
    /// Create Date : 140715
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng share_folder trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ShareFolderMapOracle : EntityTypeConfiguration<ShareFolder>
    {
        /// <summary>
        /// 
        /// </summary>
        public ShareFolderMapOracle()
        {
        }
    }
}
