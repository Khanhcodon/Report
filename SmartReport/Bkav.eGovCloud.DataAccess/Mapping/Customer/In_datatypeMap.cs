using Bkav.eGovCloud.Entities.Customer.Ad_Report;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{

        /// <summary>
        /// Bkav Corp. - BSO - eGov - eOffice team
        /// Project: eGov Cloud v1.0
        /// Class : SiteMapMySql - public - DAL
        /// Access Modifiers: 
        ///     * Inherit : EntityTypeConfiguration&lt;Site&gt;
        /// Create Date : 260712
        /// Author      : TienBV
        /// Description : Mapping tương ứng với bảng Site trong CSDL MySql
        /// </summary>
        [ComVisible(false)]
        public class In_datatypeMapMySql : EntityTypeConfiguration<Dim_indicatordatatype>
        {
            ///<summary>
            /// Mapping property
            ///</summary>
            public In_datatypeMapMySql()
            {
                ToTable("dim_in_datatype");
                HasKey(c => c.Id);
                


            }
        }

        /// <summary>
        /// Bkav Corp. - BSO - eGov - eOffice team
        /// Project: eGov Cloud v1.0
        /// Class : SiteMapSqlServer - public - DAL
        /// Access Modifiers: 
        ///     * Inherit : EntityTypeConfiguration&lt;Site&gt;
        /// Create Date : 260712
        /// Author      : TienBV
        /// Description : Mapping tương ứng với bảng Site trong CSDL SqlServer
        /// </summary>
        [ComVisible(false)]
        public class In_datatypeMapSqlServer : EntityTypeConfiguration<Dim_indicatordatatype>
        {
            ///<summary>
            /// Mapping property
            ///</summary>
            public In_datatypeMapSqlServer()
            {
                ToTable("dim_in_datatype");
            HasKey(c => c.Id);
        }
        }
        /// <summary>
        /// Bkav Corp. - BSO - eGov - eOffice team
        /// Project: eGov Cloud v1.0
        /// Class : SiteMapOracle - public - DAL
        /// Access Modifiers: 
        ///     * Inherit : EntityTypeConfiguration&lt;Site&gt;
        /// Create Date : 260712
        /// Author      : TienBV
        /// Description : Mapping tương ứng với bảng Site trong CSDL Oracle
        /// </summary>
        [ComVisible(false)]
        public class In_datatypeMapOracle : EntityTypeConfiguration<Dim_indicatordatatype>
        {
            ///<summary>
            /// Mapping property
            ///</summary>
            public In_datatypeMapOracle()
            {

            }
        }
    }
