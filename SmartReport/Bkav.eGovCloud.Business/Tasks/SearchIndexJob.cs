using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Lịch định kỳ đánh index dữ liệu lên server tìm kiếm (Solr)
    /// </summary>
    public class SearchIndexJob : IeGovJob
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public SearchIndexJob()
        {

        }

        /// <summary>
        /// Thực thi job
        /// </summary>
        public void Execute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dừng job
        /// </summary>
        /// <param name="immediate"></param>
        public void Stop(bool immediate)
        {
            throw new NotImplementedException();
        }
    }
}
