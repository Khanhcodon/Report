using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DatabaseType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DatabaseType trong CSDL
    /// </summary>
    public class DatabaseType
    {
        private ICollection<Connection> _connections;

        /// <summary>
        /// Lấy hoặc thiết lập Id loại cơ sở dữ liệu
        /// </summary>
        public byte DatabaseTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại cơ sở dữ liệu
        /// </summary>
        public string DatabaseTypeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các connection liên quan
        /// </summary>
        public virtual ICollection<Connection> Connections
        {
            get { return _connections ?? (_connections = new List<Connection>()); }
            set { _connections = value; }
        }
    }
}
