using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class QuestionMapMySql : EntityTypeConfiguration<Question>
    {
        /// <summary>
        /// 
        /// </summary>
        public QuestionMapMySql()
        {
            ToTable("question");
            HasKey(x => x.QuestionId);
            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(200);
            Property(x => x.Tag).HasColumnType("varchar").HasMaxLength(200);
            Property(x => x.Detail).HasColumnType("text").IsRequired();
            Property(x => x.Answer).HasColumnType("text");
            Property(x => x.AskPeople).HasColumnType("varchar").HasMaxLength(100);
            Property(x => x.AnswerPeople).HasColumnType("varchar").HasMaxLength(100);
            Property(x => x.Active).HasColumnType("bit");
            Property(x => x.Email).HasColumnType("varchar").HasMaxLength(200);

            Ignore(x => x.QuestionType);
            Ignore(x => x.DocumentId);
            Ignore(x => x.DocumentLocalId);
            Ignore(x => x.eGovUserId);
            Ignore(x => x.Comment);
            Ignore(x => x.HasAnswered);
            Ignore(x => x.Doc_Code);
            Ignore(x => x.Doc_Compendium);
        }
    }


    /// <summary>
    /// 
    /// </summary>
   [ComVisible(false)]
    public class QuestionMapSqlServer : EntityTypeConfiguration<Question>
    {
       /// <summary>
       /// 
       /// </summary>
        public QuestionMapSqlServer()
        {
            ToTable("question");
            HasKey(x => x.QuestionId);
            Property(x => x.Name).HasMaxLength(200);
            Property(x => x.Tag).HasMaxLength(200);
            Property(x => x.Detail).IsRequired();//.HasColumnType("ntext");
            //Property(x => x.Answer).HasColumnType("ntext");
            Property(x => x.AskPeople).HasMaxLength(100);
            Property(x => x.AnswerPeople).HasMaxLength(100);
            //Property(x => x.Active);
            Property(x => x.Email).HasMaxLength(200);

            Ignore(x => x.QuestionType);
            Ignore(x => x.DocumentId);
            Ignore(x => x.DocumentLocalId);
            Ignore(x => x.HasAnswered); 
            Ignore(x => x.eGovUserId);
            Ignore(x => x.Comment);
            Ignore(x => x.Doc_Code);
            Ignore(x => x.Doc_Compendium);
        }
    }

    /// <summary>
    /// 
    /// </summary>
   [ComVisible(false)]
   public class QuestionMapOracle : EntityTypeConfiguration<Question>
   {
       ///<summary>
       /// Mapping property
       ///</summary>
       public QuestionMapOracle()
       {

       }
   }
}
