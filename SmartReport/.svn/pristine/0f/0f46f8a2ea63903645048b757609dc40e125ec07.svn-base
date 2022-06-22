using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BussinessDocFieldDocTypeGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : BussinessDocFieldDocTypeGroup&lt;City&gt;
    /// Create Date : 041215
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng City trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BussinessDocFieldDocTypeGroupMapMySql : EntityTypeConfiguration<BussinessDocFieldDocTypeGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BussinessDocFieldDocTypeGroupMapMySql()
        {
            ToTable("bussiness_docfield_doctype_group");
            HasKey(p => p.BussinessDocFieldDocTypeGroupId);
            Property(p => p.Name).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.CategoryBusinessId).IsRequired();
            Property(p => p.DocFieldId);
            Property(p => p.DocTypeId).HasColumnType("char");
            Property(p => p.IsActived).IsRequired().HasColumnType("bit");
            Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BussinessDocFieldDocTypeGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : BussinessDocFieldDocTypeGroup&lt;City&gt;
    /// Create Date : 041215
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng City trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BussinessDocFieldDocTypeGroupMapSqlServer : EntityTypeConfiguration<BussinessDocFieldDocTypeGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BussinessDocFieldDocTypeGroupMapSqlServer()
        {
            ToTable("bussiness_docfield_doctype_group");
            HasKey(p => p.BussinessDocFieldDocTypeGroupId);
            Property(p => p.Name).IsRequired().HasMaxLength(255);
            Property(p => p.CategoryBusinessId).IsRequired();
            //Property(p => p.DocFieldId);
            Property(p => p.DocTypeId).HasColumnType("char");
            Property(p => p.IsActived).IsRequired();//.HasColumnType("bit");
            Property(p => p.CreatedDate).IsRequired();//.HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BussinessDocFieldDocTypeGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : BussinessDocFieldDocTypeGroup&lt;City&gt;
    /// Create Date : 041215
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng City trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class BussinessDocFieldDocTypeGroupMapOracle : EntityTypeConfiguration<BussinessDocFieldDocTypeGroup>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public BussinessDocFieldDocTypeGroupMapOracle()
        {

        }
    }
}
