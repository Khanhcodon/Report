using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Anticipate - public - Entity
    /// Access Modifiers: 
    /// Create Date : 180214
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Anticipate trong CSDL
    /// </summary>
    public class Anticipate
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id dự kiến
        /// </summary>
        public int AnticipateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người nhận được dự kiến
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id bản sao văn bản hồ sơ
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại dự kiến
        /// </summary>
        public byte AnticipateType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại dự kiến
        /// </summary>
        public AnticipateType AnticipateTypeInEnum
        {
            get { return (AnticipateType)AnticipateType; }
            set { AnticipateType = (byte)value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách những người dự kiến chuyển đến
        /// </summary>
        public string Destination { get; set; }
    }
}
