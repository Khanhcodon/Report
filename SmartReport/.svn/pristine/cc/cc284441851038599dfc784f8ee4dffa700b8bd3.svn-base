using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Models
{
    public class SearchDocumentResultModel
    {
        private readonly AdminGeneralSettings _generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();

        /// <summary>
        /// Loại tìm kiếm : Trong văn bản/ trong nội dung file
        /// </summary>
        public int SearchType { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPage
        {
            get
            {
                return TotalResult % _generalSettings.DefaultPageSize == 0 ? TotalResult / _generalSettings.DefaultPageSize : TotalResult / _generalSettings.DefaultPageSize + 1;
            }
        }

        public IEnumerable<SearchDocumentItemResultModel> Items { get; set; }

        public ICollection<KeyValuePair<string, KeyValuePair<string, int>>> FacetDocField { get; set; }

        public ICollection<KeyValuePair<string, KeyValuePair<string, int>>> FacetDocType { get; set; }

        public IEnumerable<KeyValuePair<string, int>> FacetCreatedDate { get; set; }

        public string DidYouMean { get; set; }

        public int TotalResult { get; set; }

        public SearchDocumentParameters Parameters { get; set; }
    }
}