using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : OfficeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Office&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Office trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class OfficeMapMySql : EntityTypeConfiguration<Office>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public OfficeMapMySql()
        {
            ToTable("office");
            HasKey(p=>p.OfficeId);
            Property(p => p.OfficeName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.OfficeCode).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.Description).HasColumnType("text");
            Property(p => p.Phone).HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.Email).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.OnlineServiceUrl).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.ProcessServiceUrl).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.ReportServiceUrl).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.LevelId);
            Property(p => p.UserId);
            Property(p => p.FileService).HasColumnType("text");
            Property(p => p.DataService).HasColumnType("text");
            Property(p => p.Address).HasColumnType("text");
            Property(p => p.Password).HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.LastPassword).HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.IsMe).IsRequired().HasColumnType("bit");
            HasOptional(p => p.OfficeParent)
                .WithMany(p => p.OfficeChildren)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : OfficeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Office&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Office trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class OfficeMapSqlServer : EntityTypeConfiguration<Office>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public OfficeMapSqlServer()
        {
            ToTable("office");
            HasKey(p => p.OfficeId);
            Property(p => p.OfficeName).IsRequired().HasMaxLength(255);
            Property(p => p.OfficeCode).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            //Property(p => p.Description).HasColumnType("ntext");
            Property(p => p.Phone).HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.Email).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.OnlineServiceUrl).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.ProcessServiceUrl).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.ReportServiceUrl).HasMaxLength(255).HasColumnType("varchar");
            //Property(p => p.LevelId);
            //Property(p => p.UserId);
            //Property(p => p.FileService).HasColumnType("ntext");
            //Property(p => p.DataService).HasColumnType("ntext");
            //Property(p => p.Address).HasColumnType("ntext");
            Property(p => p.Password).HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.LastPassword).HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.IsMe).IsRequired();//.HasColumnType("bit");
            HasOptional(p => p.OfficeParent)
                .WithMany(p => p.OfficeChildren)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : OfficeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Office&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Office trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class OfficeMapOracle : EntityTypeConfiguration<Office>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public OfficeMapOracle()
        {
            
        }
    }
}
