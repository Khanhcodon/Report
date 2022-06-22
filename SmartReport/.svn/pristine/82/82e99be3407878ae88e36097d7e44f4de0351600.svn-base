using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    ///
    /// </summary>
    [ComVisible(false)]
    public class LawMapMySql : EntityTypeConfiguration<Law>
    {
        /// <summary>
        ///
        /// </summary>
        public LawMapMySql()
        {
            ToTable("law");
            HasKey(l => l.LawId);
            Property(l => l.NumberSign).HasMaxLength(300).IsRequired();
            Property(l => l.SubContent).HasColumnType("text").IsRequired();
            Ignore(l => l.FileIds);
        }
    }

    /// <summary>
    ///
    /// </summary>
    [ComVisible(false)]
    public class LawMapSqlServer : EntityTypeConfiguration<Law>
    {
        /// <summary>
        ///
        /// </summary>
        public LawMapSqlServer()
        {
            ToTable("law");
            HasKey(p => p.LawId);
            //Property(l => l.FileId);
            //HasRequired(l => l.File).WithMany(a => a.Law).HasForeignKey(l => l.FileId).WillCascadeOnDelete(false);
            Property(p => p.NumberSign).HasMaxLength(300).IsRequired();
            Property(p => p.SubContent).IsRequired();
            Ignore(l => l.FileIds);
        }
    }

    /// <summary>
    ///
    /// </summary>
    [ComVisible(false)]
    public class LawMapOracle : EntityTypeConfiguration<Law>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LawMapOracle()
        {
        }
    }
}