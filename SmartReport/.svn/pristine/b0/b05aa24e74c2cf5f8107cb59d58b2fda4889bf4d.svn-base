using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ScopeArea - public - Entity
    /// Access Modifiers:
    /// Create Date : 10092014
    /// Author      : QuangP
    /// Description : Entity tương ứng với bảng ScopeArea trong CSDL
    /// </summary>
    public class ScopeArea
    {
        /// <summary>
        /// Lấy hoặc thiết lập ScopeArea Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập từ khóa
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên scopearea
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả cho ScopeArea
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách scope được truy cập
        /// </summary>
        public string Scopes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách client
        /// </summary>
        public List<int> ClientIds { get; set; }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ScopeArea - public - Entity
    /// Access Modifiers:
    /// Create Date : 10092014
    /// Author      : QuangP
    /// Description : Entity tương ứng với bảng ClientScope trong CSDL
    /// </summary>
    public class ClientScope
    {
        /// <summary>
        /// Lấy hoặc thiết lập ClientScope Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ClientId
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ScopeAreaId
        /// </summary>
        public int ScopeAreaId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Client
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ScopeArea
        /// </summary>
        public virtual ScopeArea ScopeArea { get; set; }
    }
}