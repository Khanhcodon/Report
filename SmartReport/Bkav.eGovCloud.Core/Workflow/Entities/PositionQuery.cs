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
    /// <para>Không gian cán bộ lấy theo vị trí tương đối với 1 node trên cây phòng ban</para>
    /// (CuongNT@bkav.com - 181212)
    /// </summary>
    public class PositionQuery : QueryBase
    {
        /// <summary>
        /// Khởi tạo <see cref="PositionQuery"/>
        /// </summary>
        public PositionQuery()
        {
            Type = QueryType.TheoViTri;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Vị trí tương đối với 1 node trên cây phòng ban
        /// </summary>
        public PositionType Position { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id phòng ban
        /// </summary>
        public int DepId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chức vụ
        /// </summary>
        public int PositionId { get; set; }


    }
}
