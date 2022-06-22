using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFinish - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DocFinish trong CSDL
    /// </summary>
    public class DocFinish
    {
        /// <summary>
        /// 
        /// </summary>
        public int DocFinishId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người kết thúc văn bản, hồ sơ
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ được kết thúc
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vệt hồ sơ, văn bản
        /// </summary>
        public int DocumentCopyId { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã xem hồ sơ
        /// </summary>
        public bool IsViewed { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập kiểu tham gia xử lý hồ sơ: có xử lý, chỉ xem,...</para>
        /// <para>1. Quyền xem văn bản do tham gia xử lý;</para>
        /// <para>2. Quyền xem văn bản do là có quyền xem văn bản liên quan;</para>
        /// </summary>
        public int DocFinishType { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id thể hiện văn bản liên quan. Khác Null khi DocFinishType = 2.</para>
        /// </summary>
        public int? DocRelationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái văn bản quan trọng 
        /// </summary>
        public bool IsDocumentImportant { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu tham gia xử lý hồ sơ: có xử lý, chỉ xem,...
        /// </summary>
        public DocFinishType DocFinishTypeInEnum
        {
            get { return (DocFinishType)DocFinishType; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Văn bản, hồ sơ được kết thúc
        /// </summary>
        public virtual Document Document { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vệt hồ sơ, văn bản
        /// </summary>
        public virtual DocumentCopy DocumentCopy { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người kết thúc văn bản, hồ sơ
        /// </summary>
        public User User { get; set; }
    }
}
