using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DocumentCustomerInfo
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int Stt { get; set; }
        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Tên loại văn bản
        /// </summary>
        public string DoctypeName { get; set; }

        /// <summary>
        /// Ngày tiếp nhận
        /// </summary>
        public DateTime SDateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateCreated
        {
            get
            {
                return SDateCreated.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Ngày hẹn trả
        /// </summary>
        public DateTime? SDateAppointed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateAppointed
        {
            get
            {
                return SDateAppointed.HasValue? SDateAppointed.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Địa chỉ người giữ văn bản
        /// </summary>
        public string Address { get; set; }
    }
}
