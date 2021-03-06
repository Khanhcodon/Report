using System;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Models
{
    public class SearchAdvangeModel
    {
        /// <summary>
        /// Loại tìm kiếm : Trong văn bản/ trong nội dung file
        /// </summary>
        public SearchType SearchType { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Hình thức hồ sơ, văn bản
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Số ký hiệu, mã hồ sơ
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Số đến đi, mã đến đi.
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// Độ khẩn
        /// </summary>
        public int? UrgentId { get; set; }

        /// <summary>
        /// Trạng thái văn bản đến đi: True (văn bản đến), False (văn bản đi).
        /// </summary>
        public int? CategoryBusinessId { get; set; }

        /// <summary>
        /// chế độ báo cáo
        /// </summary>
        public int? ReportModeId { get; set; }

        /// <summary>
        /// mã báo cáo
        /// </summary>
        public string DocTypeCode { get; set; }

        /// <summary>
        /// Hồ sơ cá nhân
        /// </summary>
        public int? StorePrivateId { get; set; }

        /// <summary>
        /// Sổ văn bản
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// Người đang giữ
        /// </summary>
        public int? CurrentUserId { get; set; }

        /// <summary>
        /// Nơi đến đi.
        /// </summary>
        public int? InOutPlaceId { get; set; }

        /// <summary>
        /// Loại hồ sơ, văn bản.
        /// </summary>
        public Guid? DocTypeId { get; set; }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public string FromDateStr { get; set; }

        /// <summary>
        /// Đến ngày
        /// </summary>
        public string ToDateStr { get; set; }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public DateTime? FromDate
        {
            get
            {
                var currentYear = DateTime.Now.Year;

                return string.IsNullOrEmpty(FromDateStr)
                    ? new DateTime(currentYear - 1, 1, 1)
                    : DateTime.ParseExact(FromDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Đến ngày
        /// </summary>
        public DateTime? ToDate
        {
            get
            {
                var result = string.IsNullOrEmpty(ToDateStr)
                    ? DateTime.Now
                    : DateTime.ParseExact(ToDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                return new DateTime(result.Year, result.Month, result.Day, 23, 59, 59);
            }
        }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public string FromPubDateStr { get; set; }

        /// <summary>
        /// Đến ngày
        /// </summary>
        public string ToPubDateStr { get; set; }

        /// <summary>
        /// Từ ngày ban hành
        /// </summary>
        public DateTime? FromPubDate
        {
            get
            {
                return string.IsNullOrEmpty(FromPubDateStr)
                    ? (DateTime?)null
                    : DateTime.ParseExact(FromPubDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Đến ngày ban hành
        /// </summary>
        public DateTime? ToPubDate
        {
            get
            {
                return string.IsNullOrEmpty(ToPubDateStr)
                 ? (DateTime?)null
                 : DateTime.ParseExact(ToPubDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public string BeforeDate { get; set; }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public string AfterDate { get; set; }

        public string OrganizationCreate { get; set; }

        public int? DocFieldId { get; set; }

        public int? UserSuccessId { get; set; }

        public int? UserCreateId { get; set; }

        public bool IsMainProcess { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        public string FormId { get; set; }

        public bool IsUseCached { get; set; }

        public bool IsRelationDoc { get; set; }

        public string SortBy { get; set; }

        public string IsDesc { get; set; }


        /// <summary>
        /// đơn vị nhận
        /// </summary>
        public string InOutPlace { get; set; }

        
        /// <summary>
        /// đơn vị giao
        /// </summary>
        public string UserCreatedName { get; set; }

        /// <summary>
        /// tình trạng báo cáo
        /// </summary>
        public int? Status { get; set; }


        /// <summary>
        /// tình trạng báo cáo
        /// </summary>
        public int? ReportRuleIdOnly { get; set; }

        /// <summary>
        /// kiểu kỳ báo cáo
        /// </summary>
        public int? ActionLevel { get; set; }

        /// <summary>
        /// timkey
        /// </summary>
        public string TimeKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitDelivery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitReceive { get; set; }
    }
}