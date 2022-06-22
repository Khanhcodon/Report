using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DocTypeLawMapMySql : EntityTypeConfiguration<DocTypeLaw>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocTypeLawMapMySql()
        {
            ToTable("doctype_law");
            HasKey(dl => dl.DocTypeLawId);
            // HasRequired(dl => dl.DocType).WithMany(d => d.DocTypeLaws).HasForeignKey(dl => dl.DocTypeId).WillCascadeOnDelete(false);
            Property(dl => dl.LawId).IsRequired();
            //HasRequired(dl => dl.Law).WithMany(d => d.DocTypeLaws).HasForeignKey(dl => dl.LawId).WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DocTypeLawMapSqlServer: EntityTypeConfiguration<DocTypeLaw>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocTypeLawMapSqlServer()
        {
            ToTable("doctype_law");
            HasKey(dl => dl.DocTypeLawId);
            // HasRequired(dl => dl.DocType).WithMany(d => d.DocTypeLaws).HasForeignKey(dl => dl.DocTypeId).WillCascadeOnDelete(false);
            Property(dl => dl.LawId).IsRequired();
            //HasRequired(dl => dl.Law).WithMany(d => d.DocTypeLaws).HasForeignKey(dl => dl.LawId).WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DocTypeLawMapOracle : EntityTypeConfiguration<DocTypeLaw>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeLawMapOracle()
        {

        }
    }
}
