using System;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Đổi tượng thể hiện 1 khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Key
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên domain
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại khách hàng
        /// </summary>
        public bool CustomerType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại khách hàng
        /// </summary>
        public CustomerType CustomerTypeInEnum
        {
            get { return (CustomerType)Convert.ToInt32(CustomerType); }
            set { CustomerType = Convert.ToBoolean((int)value); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Tỉnh thành phố
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Quận, huyện
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phường, xã
        /// </summary>
        public string Commune { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra Domain này đang hoạt động
        /// </summary>
        public bool IsActivated { get; set; }
    }
}