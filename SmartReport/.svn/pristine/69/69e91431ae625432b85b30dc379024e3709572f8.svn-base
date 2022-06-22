using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects.StatisticXlvb
{
    /// <summary>
    /// Giám sát sổ văn bản đến
    /// </summary>
    public class StoreDocumentIn : StatisticsGroup
    {
        /// <summary>
        /// ngày đến
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// ngày đến
        /// </summary>
        public string DateArrived { get; set; }

        /// <summary>
        /// Số đến
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public int UserCreatedId { get; set; }

        /// <summary>
        /// CQBH
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Ngày ban hành
        /// </summary>
        public string DatePublished { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Nơi nhận
        /// </summary>
        public string ProcessInfo { get; set; }

        /// <summary>
        /// Hạn
        /// </summary>
        public string DateAppointed { get; set; }

        /// <summary>
        /// Cv trả lời
        /// </summary>
        public string AnswerDocCode { get; set; }

        /// <summary>
        /// Thể loại văn bản
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Người ký
        /// </summary>
        public string UserSuccess { get; set; }

        /// <summary>
        /// Nơi lưu
        /// </summary>
        public string InOutPlace { get; set; }

        /// <summary>
        /// Người soạn
        /// </summary>
        public string UserCreatedName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocCodeResponsed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateResponsed { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string StatusResponse { get; set; }
    }

    public class DocsResponse : StatisticsGroup
    {
        public DocsResponse()
        {
            ListResponse = new List<DocsResponse>();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Cv trả lời
        /// </summary>
        public string AnswerDocCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DocumentCopyId { get; set; }
        /// <summary>
        /// Thể loại văn bản
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Người ký
        /// </summary>
        public string UserSuccess { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DatePublished { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsResponsed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasRequireResponse { get; set; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public DateTime? DateResponsed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocCodeResponsed { get; set; }

        /// <summary>
        /// Ngày ban hành
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        public string ToDateAppointed
        {
            get
            {
                var str = DateAppointed.HasValue ? DateAppointed.Value.ToString("dd/MM/yyyy") : "";
                return str;
            }
            set { }
        }

        public string ToDateResponsed
        {
            get
            {
                var str = DateResponsed.HasValue ? DateResponsed.Value.ToString("dd/MM/yyyy") : "";
                return str;
            }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UserCurrentId { get; set; }

        public int RequireResponseStatus { get; set; }
        public string RequireResponseStatusName
        {
            get
            {
                switch (RequireResponseStatus)
                {
                    case 1:
                        return "Đúng hạn";
                    case 2:
                        return "Chưa tới hạn";
                    case 4:
                        return "Quá hạn";
                    case 3:
                        return "Trễ hạn";
                    default:
                        break;
                }
                return "";
            }
            set { }
        }

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

        public List<DocsResponse> ListResponse { get; set; }
    }
}
