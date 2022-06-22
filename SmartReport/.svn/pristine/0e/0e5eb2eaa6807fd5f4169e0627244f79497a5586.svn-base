using Bkav.eGovCloud.Entities;
using System;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đối tượng cache lại các mã số đã cấp theo năm
    /// </summary>
    public class CodeUseds
    {
        /// <summary>
        /// Document ID
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Số đến
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// CQBH
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Sổ
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Ngày tạo văn bản
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Nghiệp vụ
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// Cơ quan nhận văn bản khi nhận liên thông
        /// </summary>
        public string InOutPlace { get; set; }

        /// <summary>
        /// Nghiệp vụ
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Nguồn gốc văn bản
        /// </summary>
        public byte Original { get;  set; }

		/// <summary>
		/// 
		/// </summary>
		public int UserCreatedId { get;  set; }
	}
}
