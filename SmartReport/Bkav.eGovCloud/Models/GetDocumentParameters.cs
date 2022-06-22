using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Models
{
    public class GetDocumentParameters
    {
        public GetDocumentParameters()
        {
            Params = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Params { get; set; }

        public DateTime? LastDate { get; set; }

        public string QuickSearch { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }

    /// <summary>
    /// Lớp chứa parameter tham số đầu vào của câu truy vấn lấy danh sách văn bản
    /// </summary>
    public class ObjectParams
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}