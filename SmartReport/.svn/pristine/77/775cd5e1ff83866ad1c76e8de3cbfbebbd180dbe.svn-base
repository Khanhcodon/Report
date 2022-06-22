using System;
using System.Linq;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Statistic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đối tượng để xử lý báo cáo
    /// </summary>
    //[Serializable]
    public class StatisticObject
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
        /// Trạng thái quá hạn Tại VPUB tỉnh
        /// </summary>
        public OverdueStatusTypeVPUB OverdueStatusTypeVPUB { get; set; }

        /// <summary>
        /// Id người ký
        /// </summary>
        public int? UserSuccessId { get; set; }

        /// <summary>
        /// Id người đang giữ
        /// </summary>
        public int UserCurrentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserCurrentName { get; set; }

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
                if (!string.IsNullOrEmpty(_history))
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
                try
                {
                    _histories = Json2.ParseAs<HistoryProcess>(History);
                    _histories.HistoryPath = _histories.HistoryPath.OrderBy(c => c.DateCreated).ToList();
                    _histories.HistoryThongbao = _histories.HistoryThongbao.OrderBy(c => c.DateCreated).ToList();
                    _histories.HistoryXinykien = _histories.HistoryXinykien.OrderBy(c => c.DateCreated).ToList();
                }
                catch (Exception) { }

                return _histories;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateReceived { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsLTVPUB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateAppointVPUB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateResponsedVPUB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ExpireProcess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentDepartmentName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string CurrentDepartmentExt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentDepartmentPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocTimelines { get; set; }

        /// <summary>
        /// ky bao cao
        /// </summary>
        public string DatePublish { get; set; }
        /// <summary>
        /// nguoi tao
        /// </summary>
        public string UserCreatedName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StatusDXLName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DocTimeline> ListTimeLine
        {
            get
            {
                if (string.IsNullOrEmpty(DocTimelines))
                {
                    var dtl = Json2.ParseAs<List<DocTimeline>>(DocTimelines);

                    return dtl;
                }
                return new List<DocTimeline>();
            }
        }
    }
}
