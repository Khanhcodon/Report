
namespace Bkav.eGovCloud.Entities.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DataTableRow - public - Entity
    /// Access Modifiers: 
    /// Create Date : 10102014
    /// Author      : QuangP
    /// Description : Entity sử dụng để chuyển chuỗi json trong của form người dùng nhập sang datatable để truyền vào crystal report
    /// </summary>
    public class DataTableRow
    {
        /// <summary>
        /// FormId
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Tên cột của datatable
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// controlID của control trong form
        /// </summary>
        public string ControlId { get; set; }
    }
}
