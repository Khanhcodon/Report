using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TreeGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TreeGroup&gt;
    /// Create Date : 011115
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng TreeGroup trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class TreeGroupMapMySql : EntityTypeConfiguration<TreeGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TreeGroupMapMySql()
        {
            ToTable("tree_group");
            HasKey(p => p.TreeGroupId);
            Property(p => p.TreeGroupName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.DateCreated).HasColumnType("datetime");
            Property(p => p.IsActived).IsRequired().HasColumnType("bit");
            Property(p => p.IsShowUserFullName).IsRequired().HasColumnType("bit");
            Property(p => p.IsShowTreeName).IsRequired().HasColumnType("bit");
            Property(p => p.IsDocumentTree).IsRequired().HasColumnType("bit");
            Property(p => p.IsOtherSystems).IsRequired().HasColumnType("bit");
            Property(p => p.HasChildrenContextMenuAdmin).IsRequired().HasColumnType("bit");
            Property(p => p.Order);
            Property(p => p.UserNameCreated).HasMaxLength(125).HasColumnType("varchar");
            Property(p => p.UserNameModified).HasMaxLength(125).HasColumnType("varchar");
            Property(p => p.DateModified).HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TreeGroupMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TreeGroup&gt;
    /// Create Date : 021115
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng TreeGroup trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class TreeGroupMapSqlServer : EntityTypeConfiguration<TreeGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TreeGroupMapSqlServer()
        {
            ToTable("tree_group");
            HasKey(p => p.TreeGroupId);
            Property(p => p.TreeGroupName).IsRequired().HasMaxLength(255);
            //Property(p => p.DateCreated).HasColumnType("datetime");
            Property(p => p.IsActived).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsShowUserFullName).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsShowTreeName).IsRequired();//.HasColumnType("bit");
            Property(p => p.HasChildrenContextMenuAdmin).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsDocumentTree).IsRequired();//.HasColumnType("bit");
            Property(p => p.IsOtherSystems).IsRequired();//.HasColumnType("bit");
            //Property(p => p.Order);
            Property(p => p.UserNameCreated).HasMaxLength(125);
            Property(p => p.UserNameModified).HasMaxLength(125);
            //Property(p => p.DateModified).HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TreeGroupMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TreeGroup&gt;
    /// Create Date : 021115
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng TreeGroup trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class TreeGroupMapOracle : EntityTypeConfiguration<TreeGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TreeGroupMapOracle()
        {

        }
    }
}
