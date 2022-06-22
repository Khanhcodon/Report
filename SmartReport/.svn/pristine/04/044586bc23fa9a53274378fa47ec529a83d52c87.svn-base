namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDepartmentPosition - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng UserDepartmentPosition trong CSDL
    /// </summary>
    public class UserDepartmentPosition
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa người dùng, phòng ban và chức vụ
        /// </summary>
        public int UserDepartmentPositionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id phòng ban
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id chức vụ
        /// </summary>
        public int PositionId { get; set; }
    
        ///// <summary>
        ///// Lấy hoặc thiết lập Phòng ban
        ///// </summary>
        //public virtual Department Department { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chức vụ
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người dùng
        /// </summary>
        public virtual User User { get; set; }
    }
}
