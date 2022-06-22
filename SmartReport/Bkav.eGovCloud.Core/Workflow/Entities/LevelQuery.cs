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
    /// Không gian cán bộ theo kiểu Cấp (Cơ quan - cấp 1, Đơn vị thuộc cơ quan - cấp 2...)
    /// </summary>
    public class LevelQuery : QueryBase
    {
        /// <summary>
        /// Khởi tạo <see cref="LevelQuery"/>
        /// </summary>
        public LevelQuery()
        {
            Type = QueryType.TheoCap;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Chức vụ
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cấp trong cây phòng ban
        /// </summary>
        public int Level { get; set; }
    }
}
