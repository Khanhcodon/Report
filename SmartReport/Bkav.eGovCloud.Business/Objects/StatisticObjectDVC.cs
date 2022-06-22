using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Statistic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đối tượng để xử lý báo cáo
    /// </summary>
    [Serializable]
    public class StatisticObjectDVC
    {
        /// <summary>
        /// DocumentCopyId
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// DocumentId
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Mã hồ sơ
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Trạng thái hồ sơ
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusText
        {
            get
            {
                return ((DocumentStatus)Status).ToString();
            }
        }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Hạn xử lý
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        /// <summary>
        /// Ngày ký duyệt
        /// </summary>
        public DateTime? DateSuccess { get; set; }

        /// <summary>
        /// Ngày trả kết quả
        /// </summary>
        public DateTime? DateReturned { get; set; }

        /// <summary>
        /// Ngày dừng xử lý
        /// </summary>
        public DateTime? DateRequireSupplementary { get; set; }

        /// <summary>
        /// Ngày kết thúc xử lý
        /// </summary>
        public DateTime? DateFinished { get; set; }

        /// <summary>
        /// Đã ký duyệt
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Đã trả kết quả
        /// </summary>
        public bool? IsReturned { get; set; }

        /// <summary>
        /// Loại gửi
        /// </summary>
        public int DocumentCopyType { get; set; }

        /// <summary>
        /// Nguồn gốc hồ sơ
        /// </summary>
        public int Original { get; set; }

        /// <summary>
        /// Loại hồ sơ
        /// </summary>
        public Guid? DocTypeId { get; set; }

        /// <summary>
        /// Mã lĩnh vực
        /// </summary>
        public string DocFieldIds { get; set; }

        /// <summary>
        /// Trạng thái quá hạn
        /// </summary>
        public OverdueStatusType OverdueStatusType { get; set; }

        /// <summary>
        /// Id người ký
        /// </summary>
        public int? UserSuccessId { get; set; }

        /// <summary>
        /// Id người đang giữ
        /// </summary>
        public int UserCurrentId { get; set; }

        /// <summary>
        /// Nghiệp vụ
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Id người tạo
        /// </summary>
        public int UserCreatedId { get; set; }

        /// <summary>
        /// Đơn vị thụ lý
        /// </summary>
        public string InOutPlace { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Thể loại văn bản
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Số đến đi
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// Cơ quan ban hành
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Tên loại hồ sơ
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Tên thể loại hồ sơ
        /// </summary>
        public string CategoryName { get; set; }


        private string _history;
        /// <summary>
        /// <para>Lấy hoặc thiết lập Vết đường đi. Nhận giá trị có khuôn dạng List&lt;HistoryPath&gt; Histories.StringifyJs(false);</para>
        /// <para>Set: Sử dụng SetHistories(HistoryProcess history) để gán giá trị mới cho History.</para>
        /// </summary>
        public string History
        {
            get
            {
                if (_history != null)
                {
                    return _history;
                }
                return "{}";
            }
            set
            {
                _history = value;
            }
        }

        private HistoryProcess _histories;
        /// <summary>
        /// 
        /// </summary>
        public HistoryProcess Histories
        {
            get
            {
                if (_histories == null)
                {
                    _histories = Json2.ParseAs<HistoryProcess>(History);
                    _histories.HistoryPath = _histories.HistoryPath.OrderBy(c => c.DateCreated).ToList();
                    _histories.HistoryThongbao = _histories.HistoryThongbao.OrderBy(c => c.DateCreated).ToList();
                    _histories.HistoryXinykien = _histories.HistoryXinykien.OrderBy(c => c.DateCreated).ToList();
                }

                return _histories;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateReceived { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ExpireProcess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? StoreId { get; set; }
    }
}
