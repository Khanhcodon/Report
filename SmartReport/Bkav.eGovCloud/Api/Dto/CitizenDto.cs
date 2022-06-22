
namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// Thông tin công dân
    /// </summary>
    public class CitizenDto
    {
        /// <summary>
        /// Tên công dân
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Địa chỉ công dân
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số CMT
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Thư điện tử
        /// </summary>
        public string Email { get; set; }

    }
}
