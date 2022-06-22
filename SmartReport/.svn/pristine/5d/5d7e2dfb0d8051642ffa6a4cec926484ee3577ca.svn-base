using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDepartmentJobTitlesMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserDepartmentJobTitles&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserDepartmentJobTitles trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserDepartmentJobTitlesPositionMapMySql : EntityTypeConfiguration<UserDepartmentJobTitlesPosition>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserDepartmentJobTitlesPositionMapMySql()
        {
            ToTable("user_department_jobtitles_position");
            Property(p => p.UserId).IsRequired();
            Property(p => p.DepartmentId).IsRequired();
            Property(p => p.DepartmentIdExt).HasColumnType("varchar").HasMaxLength(64).IsRequired();
            Property(p => p.JobTitlesId).IsRequired();
            Property(p => p.IsPrimary).IsRequired().HasColumnType("bit");
            Property(p => p.IsAdmin).IsRequired().HasColumnType("bit");
            Property(p => p.HasReceiveDocument).IsRequired().HasColumnType("bit");
            HasRequired(p => p.User)
                .WithMany(p => p.UserDepartmentJobTitless)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDepartmentJobTitlesMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserDepartmentJobTitles&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserDepartmentJobTitles trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class UserDepartmentJobTitlesPositionMapSqlServer : EntityTypeConfiguration<UserDepartmentJobTitlesPosition>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserDepartmentJobTitlesPositionMapSqlServer()
        {
            ToTable("user_department_jobTitles_position");
            Property(p => p.UserId).IsRequired();
            Property(p => p.DepartmentId).IsRequired();
            Property(p => p.DepartmentIdExt).HasMaxLength(64).IsRequired();
            Property(p => p.JobTitlesId).IsRequired();
            Property(p => p.IsPrimary).IsRequired();
            Property(p => p.IsAdmin).IsRequired();
            Property(p => p.HasReceiveDocument).IsRequired();
            HasRequired(p => p.User)
                .WithMany(p => p.UserDepartmentJobTitless)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDepartmentJobTitlesMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserDepartmentJobTitles&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserDepartmentJobTitles trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class UserDepartmentJobTitlesPositionMapOracle : EntityTypeConfiguration<UserDepartmentJobTitlesPosition>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserDepartmentJobTitlesPositionMapOracle()
        {
            
        }
    }
}
