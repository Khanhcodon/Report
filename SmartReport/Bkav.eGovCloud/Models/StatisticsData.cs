using System.Data;

namespace Bkav.eGovCloud.Models
{
    public class StatisticsData
    {
        public int Index { get; set; }

        public string ParentIdExt { get; set; }

        public int ParentId { get; set; }

        public string IndexExt { get; set; }

        public string Name { get; set; }

        public DataTable Data { get; set; }
    }
}