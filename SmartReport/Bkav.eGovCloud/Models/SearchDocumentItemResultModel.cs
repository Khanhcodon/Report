using System;
using System.Web;

namespace Bkav.eGovCloud.Models
{
    public class SearchDocumentItemResultModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string HighLight { get; set; }

        public Guid DocumentId { get; set; }

        public string DocumentCompendium { get; set; }

        public int DocumentCopyId { get; set; }

        public bool IsFile { get; set; }

        public string Extension { get; set; }

        public int ContentId { get; set; }

        public int Color { get; set; }

        public dynamic ExtendInfo { get; set; }

        public DateTime? DatePublished { get; set; }

        public string DatePublishedStr
        {
            get
            {
                return DatePublished.HasValue ? DatePublished.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        // 20191218 VuHQ START SUPPORT TaiDA
        public string TitleStr
        {
            get
            {
                return HttpUtility.UrlDecode(Title);
            }
        }

        public bool IsViewed { get; set; }

        public string DocCode { get; set; }

        public string InOutCode { get; set; }

        public DateTime? DateArrived { get; set; }

        public string DateArrivedStr
        {
            get
            {
                return DateArrived.HasValue ? DateArrived.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        public string CategoryName { get; set; }
        
        public string UserSuccessName { get; set; }

        public string ReportModeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitDelivery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UnitDeliveryStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UnitReceiveStr { get; set; }
    }
}