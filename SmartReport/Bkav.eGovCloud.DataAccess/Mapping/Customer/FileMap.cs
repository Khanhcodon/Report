using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EmbryonicFormMySql - public - DAL
    /// Create Date : 210814
    /// Author      : ManhNHc
    /// Description : Entity tương ứng với bảng File(File đính kèm văn bản quy phạm) trong CSDL
    /// </summary>

    [ComVisible(false)]
    public class FileMapMySql : EntityTypeConfiguration<File>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FileMapMySql()
        {
            ToTable("file");
            HasKey(f => f.FileId);
            Property(f => f.FileLocalName).HasColumnType("varchar").IsRequired();
            Property(f => f.Size).IsRequired();
            Property(f => f.FileExtension).HasColumnType("varchar").IsRequired();
            Property(f => f.CreatedOnDate).HasColumnType("datetime");
            Property(f => f.IsDeleted).HasColumnType("bit");
            Property(f => f.FileName).HasColumnType("varchar").IsRequired();
            Property(c => c.IdentityFolder).HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.FileLocationKey).HasColumnType("varchar").HasMaxLength(1000);
            Property(f => f.FileLocationId);
            Property(f => f.Version);
            Property(f => f.LawId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EmbryonicFormMySql - public - DAL
    /// Create Date : 210814
    /// Author      : ManhNHc
    /// Description : Entity tương ứng với bảng File(File đính kèm văn bản quy phạm) trong CSDL
    /// </summary>

    [ComVisible(false)]
    public class FileMapSqlServer : EntityTypeConfiguration<File>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FileMapSqlServer()
        {
            ToTable("file");
            HasKey(f => f.FileId);
            Property(f => f.FileLocalName).IsRequired();
            Property(f => f.Size).IsRequired();
            Property(f => f.FileExtension).HasColumnType("varchar").IsRequired();
            //Property(f => f.CreatedOnDate).HasColumnType("datetime");
            Property(f => f.IsDeleted);//.HasColumnType("bit");
            Property(f => f.FileName).IsRequired();
            Property(c => c.IdentityFolder).HasMaxLength(10);
            Property(c => c.FileLocationKey).HasMaxLength(1000);
            //Property(f => f.FileLocationId);
            //Property(f => f.Version);
            //Property(f => f.DocOnlineId);
            //Property(f => f.LawId);
        }
    }

}
