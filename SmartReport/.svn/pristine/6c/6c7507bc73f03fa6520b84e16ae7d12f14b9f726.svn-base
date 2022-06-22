using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Mobile
{
    /// <summary>
    /// Nghiệp vụ xử lý riêng cho phiên bản mobile
    /// </summary>
    public class DocumentMobileBll : ServiceBase
    {
        private readonly IRepository<DocumentContent> _documentContentRepository;
        private readonly IRepository<DocumentCopy> _documentCopyRepository;
        private readonly IRepository<Document> _documentRepository;
        private readonly AuthorizeBll _authorizeService;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="authorizeService"></param>
        public DocumentMobileBll(IDbCustomerContext context, AuthorizeBll authorizeService)
            : base(context)
        {
            _documentRepository = Context.GetRepository<Document>();
            _documentContentRepository = Context.GetRepository<DocumentContent>();
            _documentCopyRepository = Context.GetRepository<DocumentCopy>();

            _authorizeService = authorizeService;
        }

        /// <summary>
        /// Trả về văn bản đang xử lý
        /// </summary>
        /// <param name="type">Nghiệp vụ xử lý</param>
        /// <param name="userId">Người xử lý</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetDocumentProcessing(CategoryBusinessTypes type, int userId, int? isViewed)
        {
            var proceduceName = "mobile_documentprocessings";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", userId));
            parameters.Add(new SqlParameter("@categoryBusinessId", (int)type));
			parameters.Add(new SqlParameter("@viewed", isViewed));

            return Context.RawProcedure(proceduceName, parameters.ToArray());
        }

        /// <summary>
        /// Trả về danh sách văn bản đang dự thảo
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetDrafts(int userId)
        {
            return _documentRepository.GetsAs(d => new
            {
                d.DocumentId,
                d.Compendium,
                d.CategoryName,
                d.CategoryBusinessId,
                d.Organization,
                d.DocCode,
                UserName = d.UserCreatedName,
                UserId = d.UserCreatedId
            }, d => d.Status == (int)DocumentStatus.DuThao);
        }

        /// <summary>
        /// Trả về danh sách văn bản đang theo dõi
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetSents(int userId)
        {
            var proceduceName = "mobile_documentsents";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", userId));

            return Context.RawProcedure(proceduceName, parameters.ToArray());
        }

        /// <summary>
        /// Trả về danh sách các văn bản thông báo
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetAnnoucements(int userId)
        {
            var proceduceName = "mobile_documentannoucements";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", userId));

            return Context.RawProcedure(proceduceName, parameters.ToArray());
        }

        /// <summary>
        /// Trả về danh sách văn bản đã kết thúc
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetFinished(int userId)
        {
            var proceduceName = "mobile_documentfinisheds";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", userId));

            return Context.RawProcedure(proceduceName, parameters.ToArray());
        }

        /// <summary>
        /// Trả về danh sách văn bản đc ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetAuthorized(int userId)
        {
            var proceduceName = "mobile_documentauthorizeds";
            var parameters = new List<SqlParameter>();

            var authorized = _authorizeService.GetAuthorizeUsers(userId, Guid.Empty);
            parameters.Add(new SqlParameter("@userId", userId));
            parameters.Add(new SqlParameter("@authorizeIds", string.Join(",", authorized)));

            return Context.RawProcedure(proceduceName, parameters.ToArray());
        }

        /// <summary>
        /// Trả về danh sách số các văn bản trong các node.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Danh sách chỉ số theo thứ tự:  Đến chờ xử lý, đi chờ xử lý, theo dõi, ủy quyền</returns>
        public IEnumerable<int> GetCountDocuments(int userId)
        {
            var proceduceName = "mobile_count_document";
            var parameters = new List<SqlParameter>();

            var authorized = _authorizeService.GetAuthorizeUsers(userId, Guid.Empty);
            parameters.Add(new SqlParameter("@userId", userId));
            parameters.Add(new SqlParameter("@authorizeIds", string.Join(",", authorized)));

            var data = Context.RawProcedure(proceduceName, parameters.ToArray());
            var dataResult = (IDictionary<string, object>)data.ElementAt(0);
            var result = dataResult["@result"].ToString();

            return result.Split(new char[] { ',' }).Select(c => int.Parse(c));
        }
    }
}
