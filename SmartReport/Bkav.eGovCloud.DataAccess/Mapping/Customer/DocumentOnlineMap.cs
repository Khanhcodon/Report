using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// DocumentOnlineMapMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DocumentOnlineMapMySql : EntityTypeConfiguration<DocumentOnline>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public DocumentOnlineMapMySql()
        {
            ToTable("documentonline");
            HasKey(c => c.Id);
            Property(c => c.DocCode);
            Property(c => c.DocTypeId);
            Property(c => c.DocumentCopyId);
            Property(c => c.DateReceived);
            Property(c => c.DateAppoint);
            Property(c => c.Status);
            Property(c => c.PersonInfo);
            Property(c => c.IdCard);
            Property(c => c.Email);
            Property(c => c.Phone);
            Property(c => c.Address);
            Property(c => c.Json);
            Property(c => c.Comment);
            Property(c => c.IsViewed);
        }
    }

    /// <summary>
    /// DocumentOnlineMapSqlServer
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DocumentOnlineMapSqlServer : EntityTypeConfiguration<DocumentOnline>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public DocumentOnlineMapSqlServer()
        {
            ToTable("documentonline");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnType("uniqueidentifier");
            Property(c => c.DocCode).HasColumnType("varchar");
            Property(c => c.DocTypeId).HasColumnType("uniqueidentifier");
            //Property(c => c.DocumentCopyId);
            Property(c => c.DateReceived);
            //Property(c => c.DateAppoint);
            //Property(c => c.Status);
            //Property(c => c.PersonInfo);
            Property(c => c.IdCard).HasColumnType("varchar");
            Property(c => c.Email).HasColumnType("varchar");
            Property(c => c.Phone).HasColumnType("varchar");
            //Property(c => c.Address);
            //Property(c => c.Json).HasColumnType("ntext");
            //Property(c => c.IsViewed);
        }
    }
}
