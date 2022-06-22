using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : City - public - Entity
    /// Access Modifiers: 
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Entity tương ứng với bảng City trong CSDL
    /// </summary>
    public class City
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id tỉnh/thành phố
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên tỉnh/thành phố
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã của tỉnh/thành phố
        /// </summary>
        public string CityCode { get; set; }
    }
}
