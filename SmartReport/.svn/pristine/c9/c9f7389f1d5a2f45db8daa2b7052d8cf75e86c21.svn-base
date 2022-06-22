using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// QuangP: đồng bộ loại văn bản liên thông
    /// </summary>
    public class SyncDocType
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id loại văn bản của cơ quan hiện tại
        /// </summary>
        public Guid InsideDocTypeId { get; set; }

        /// <summary>
        /// loại văn bản của cơ quan hiện tại
        /// </summary>
        public virtual DocType InsideDocType { get; set; }

        /// <summary>
        /// Id loại văn bản của cơ quan liên thông cùng
        /// </summary>
        public Guid OutsideDocTypeId { get; set; }
    }
}