using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(DepartmentValidator))]
    public class DepartmentModel : PacketModel
    {
        public DepartmentModel() : base() { }

        private ICollection<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositions;
        /// <summary>
        /// Lấy hoặc thiết lập Id của phòng ban
        /// </summary>
        public int DepartmentId { get; set; }

        public string id { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập tên phòng ban
        /// </summary>
        [LocalizationDisplayName("Customer.Department.CreateOrEdit.Fields.DepartmentName.Label")]
        public string DepartmentName { set; get; }

        /// <summary>
        /// Lấy hoặc thiết lập tên phòng ban cấp cha
        /// </summary>
        [LocalizationDisplayName("Customer.Department.CreateOrEdit.Fields.ParrentDeparmentName.Label")]
        public string ParrentDeparmentName { set; get; }

        /// <summary>
        /// Lấy hoặc thiết lập tên Id cấp cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra phòng ban này đã được kích hoạt
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu phòng ban này đã được kích hoạt; ngược lại, <c>false</c>.
        /// </value>
        [LocalizationDisplayName("Customer.Department.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        public string BreadCrumb { get; set; }


        /// <summary>
        /// Lấy hoặc thiết lập giá trị mở rộng của Id phòng ban
        /// </summary>
        /// <value>
        /// 	<c>Dạng:2.3.5</c>.
        /// </value>
        public string DepartmentIdExt { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị tên phòng ban dưới dạng đường dẫn đầy đủ(bao gồm cả tên các phòng ban cấp cha).
        /// </summary>
        /// <value>
        /// 	<c>Giá trị dạng: \Bkav\BSO\Phòng2</c>.
        /// </value>
        public string DepartmentPath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị thể hiện vị trí phòng ban được sắp xếp.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị thể hiện cấp độ phòng ban trên cây.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ma dinh danh cho co quan
        /// </summary>
        public string EdocId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id các user và chức vụ
        /// </summary>
        public List<string> UserJobTitlesPositionIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách user(có chức vụ) thuộc phòng ban
        /// </summary>
        public ICollection<UserDepartmentJobTitlesPosition> UserDepartmentJobTitlesPositions
        {
            get { return _userDepartmentJobTitlesPositions ?? (_userDepartmentJobTitlesPositions = new List<UserDepartmentJobTitlesPosition>()); }
        }

        /// <summary>
        /// Cho phép nhận mail cảnh báo
        /// </summary>
        public bool HasReceiveWarning { get; set; }

        /// <summary>
        /// Danh sách mail nhận cảnh báo
        /// </summary>
        public string Emails { get; set; }

        /// <summary>
        /// Sử dụng lịch
        /// </summary>
        public bool HasCalendar { get; set; }
    }
}