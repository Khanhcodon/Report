using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CommentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Comment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Comment trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CommentMapMySql : EntityTypeConfiguration<Comment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CommentMapMySql()
        {
            ToTable("comment");
            Property(p => p.Content).IsRequired().HasColumnType("text");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.CommentType).IsRequired();
            Property(p => p.DateCreated);
            Property(p => p.CommentText).HasColumnType("text");
            Ignore(p => p.CommentTypeInEnum);
            Property(p => p.Content2).HasColumnType("varchar").HasMaxLength(255);
            Ignore(p => p.Children);
            //HasOptional(p => p.UserSend)
            //    .WithMany()
            //    .HasForeignKey(p => p.UserSendId)
            //    .WillCascadeOnDelete(false);
            //HasOptional(p => p.UserReceive)
            //    .WithMany()
            //    .HasForeignKey(p => p.UserReceiveId)
            //    .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CommentMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Comment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Comment trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CommentMapSqlServer : EntityTypeConfiguration<Comment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CommentMapSqlServer()
        {
            ToTable("comment");
            Property(p => p.Content).IsRequired();//.HasColumnType("ntext");
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.CommentType).HasColumnType("tinyint").IsRequired();
            //Property(p => p.CommentText).HasColumnType("nvarchar(MAX)");
            Ignore(p => p.CommentTypeInEnum);
            Ignore(p => p.Children);
            Property(p => p.Content2).HasMaxLength(255);
            //HasOptional(p => p.UserSend)
            //    .WithMany()
            //    .HasForeignKey(p => p.UserSendId)
            //    .WillCascadeOnDelete(false);
            //HasOptional(p => p.UserReceive)
            //    .WithMany()
            //    .HasForeignKey(p => p.UserReceiveId)
            //    .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CommentMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Comment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Comment trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CommentMapOracle : EntityTypeConfiguration<Comment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CommentMapOracle()
        {

        }
    }
}
