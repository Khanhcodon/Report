using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Customer
{
    public class DocTypeTimeJobBll : ServiceBase
    {
        private readonly IRepository<DocTypeTimeJob> _docTypeTimeJobRepository;

        public DocTypeTimeJobBll(IDbCustomerContext context)
            : base(context)
        {
            _docTypeTimeJobRepository = Context.GetRepository<DocTypeTimeJob>();
        }

        /// <summary>
        /// Lấy ra DocTypeTimeJob theo id
        /// </summary>
        /// <param name="timerJob">Id của DocTypeTimeJob</param>
        /// <returns>Entity DocTypeTimeJob</returns>
        public DocTypeTimeJob Get(Guid docTypeId)
        {
            DocTypeTimeJob result = null;
            if (docTypeId != Guid.Empty)
            {
                result = _docTypeTimeJobRepository.Gets(false, e => e.DocTypeId.Equals(docTypeId)).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Tạo mới DocTypeTimeJob
        /// </summary>
        /// <param name="timerJob">Entity DocTypeTimeJob</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity DocTypeTimeJob truyền vào bị null</exception>
        public void Create(DocTypeTimeJob docTypeTimeJob)
        {
            if (docTypeTimeJob == null)
            {
                throw new ArgumentNullException("docTypeTimeJob");
            }

            _docTypeTimeJobRepository.Create(docTypeTimeJob);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin DocTypeTimeJob
        /// </summary>
        /// <param name="timerJob">Entity DocTypeTimeJob</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity DocTypeTimeJob truyền vào bị null</exception>
        public void Update(DocTypeTimeJob docTypeTimeJob)
        {
            if (docTypeTimeJob == null)
            {
                throw new ArgumentNullException("timeJob");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa bỏ một lịch trình
        /// </summary>
        /// <param name="docTypeTimeJob">Lịch trình</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity DocTypeTimeJob truyền vào bị null</exception>
        public void Delete(DocTypeTimeJob docTypeTimeJob)
        {
            if (docTypeTimeJob == null)
            {
                throw new ArgumentNullException("docTypeTimeJob");
            }
            _docTypeTimeJobRepository.Delete(docTypeTimeJob);
            Context.SaveChanges();
        }
    }
}
