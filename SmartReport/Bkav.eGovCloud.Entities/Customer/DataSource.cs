using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// 
        /// </summary>
        public int DataSourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DomainId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DatabaseType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserCreatedId { get; set; }
    }
}
