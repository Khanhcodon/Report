using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    class DataTypeMap
    {

        /// <summary>
        /// Bkav Corp. - BSO - eGov - eOffice team
        /// Project: eGov Cloud v1.0
        /// Class : ActivityLogMapMySql - public - DAL
        /// Access Modifiers: 
        ///     * Inherit : EntityTypeConfiguration&lt;ActivityLog&gt;
        /// Create Date : 150414
        /// Author      : TrungVH
        /// Description : Mapping tương ứng với bảng ActivityLog trong CSDL MySql
        /// </summary>
        [ComVisible(false)]
        public class ActivityLogMapMySql : EntityTypeConfiguration<dataType>
        {
            ///<summary>
            /// Mapping property
            ///</summary>
            public ActivityLogMapMySql()
            {
                ToTable("dim_datatype");
                Property(p => p.dataTypeId).IsRequired();
                Property(p => p.dataTypeName).HasColumnType("varchar").HasMaxLength(1000).IsRequired();
             
            }
        }

        /// <summary>
        /// Bkav Corp. - BSO - eGov - eOffice team
        /// Project: eGov Cloud v1.0
        /// Class : ActivityLogMapSqlServer - public - DAL
        /// Access Modifiers: 
        ///     * Inherit : EntityTypeConfiguration&lt;ActivityLog&gt;
        /// Create Date : 150414
        /// Author      : TrungVH
        /// Description : Mapping tương ứng với bảng ActivityLog trong CSDL SqlServer
        /// </summary>
        [ComVisible(false)]
        public class ActivityLogMapSqlServer : EntityTypeConfiguration<dataType>
        {
            ///<summary>
            /// Mapping property
            ///</summary>
            public ActivityLogMapSqlServer()
            {
                ToTable("dim_datatype");
                Property(p => p.dataTypeId).IsRequired();
                Property(p => p.dataTypeName).HasColumnType("varchar").HasMaxLength(1000).IsRequired();
         
            }

            /// <summary>
            /// Bkav Corp. - BSO - eGov - eOffice team
            /// Project: eGov Cloud v1.0
            /// Class : ActivityLogMapOracle - public - DAL
            /// Access Modifiers: 
            ///     * Inherit : EntityTypeConfiguration&lt;ActivityLog&gt;
            /// Create Date : 150414
            /// Author      : TrungVH
            /// Description : Mapping tương ứng với bảng ActivityLog trong CSDL Oracle
            /// </summary>
            [ComVisible(false)]
            public class ActivityLogMapOracle : EntityTypeConfiguration<dataType>
            {
                ///<summary>
                /// Mapping property
                ///</summary>
                public ActivityLogMapOracle()
                {

                }
            }


        }
    }
}

