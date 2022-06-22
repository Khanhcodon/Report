using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocRelation - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DocRelation trong CSDL
    /// </summary>
    public class DocRelation
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa các hồ sơ liên quan
        /// </summary>
        public int DocRelationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ liên quan
        /// </summary>
        public Guid RelationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản/hồ sơ copy liên quan
        /// </summary>
        public int RelationCopyId { get; set; }

        /// <summary>
        /// <para> Lấy hoặc thiết lập loại văn bản liên quan</para>
        /// <para> Notes:</para>
        /// <para>   - 1: Liên quan thông thường</para>
        /// <para>   - 2: Liên quan khi trả lời văn bản/hồ sơ</para>
        /// <para>   - 3: Liên quan khi hồi báo</para>
        /// </summary>
        public int RelationType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trích yếu văn bản liên quan.
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thể loại văn bản liên quan.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày khởi tạo của văn bản liên quan (ngày trả lời)
        /// </summary>
        public DateTime? DateArrived { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mã hồ sơ (SKH) của văn bản liên quan.
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số đến đi của văn bản liên quan.
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Hồ sơ
        /// </summary>
        public virtual Document Document { get; set; }
    }
}
