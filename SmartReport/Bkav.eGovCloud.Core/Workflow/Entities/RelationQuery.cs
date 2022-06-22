namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : Action - public - Entity
    /// Access Modifiers: 
    /// Create Date : 181212
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// Không gian cán bộ lấy theo quan hệ tương đối với node khác trong quy trình bàn giao
    /// </summary>
    public class RelationQuery : QueryBase
    {
        /// <summary>
        /// Khởi tạo <see cref="RelationQuery"/>
        /// </summary>
        public RelationQuery()
        {
            Type = QueryType.TheoQuanHe;
        }

        /// <summary>
        /// Lấy hoặc thiết lập quan hệ tương đối với 1 node trong quy trình
        /// </summary>
        public RelationType Relation { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id phòng ban
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chức vụ
        /// </summary>
        public int PositionId { get; set; }
    }
}
