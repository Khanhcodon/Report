using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : DocTimelineBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 190213</para>
    /// <para>Author      : GiangPN</para>
    /// <para>Description : BLL tương ứng với bảng DocTimeline trong CSDL</para>
    /// </summary>
    public class DocTimelineBll : ServiceBase
    {
        private readonly IRepository<DocTimeline> _docTimelineRepository;
        private readonly WorktimeHelper _worktimeHelper;

        /// <summary>
        /// Khởi tạo class <see cref="DocTimelineBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="worktimeHelper"> </param>
        public DocTimelineBll(IDbCustomerContext context, WorktimeHelper worktimeHelper)
            : base(context)
        {
            _docTimelineRepository = Context.GetRepository<DocTimeline>();
            _worktimeHelper = worktimeHelper;
        }

        /// <summary>
        /// Lất ra tất cả các thời gian xử lý văn bản theo điều kiện. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<DocTimeline> Gets(Expression<Func<DocTimeline, bool>> spec = null)
        {
            return _docTimelineRepository.GetsReadOnly(spec);
        }
		
        /// <summary>
        /// Trả về khoảng thời gian xử lý của cán bộ khi giữ văn bản tại một node ở thời điểm nào đó
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <param name="nodeId"></param>
        /// <param name="userId">Id can bo giu ho so tai nut yeu cau va o thoi diem yeu cau</param>
        /// <param name="fromDate"> </param>
        /// <returns></returns>
        public DocTimeline Get(int documentCopyId, int userId, DateTime fromDate, int? nodeId = null)
        {
            return _docTimelineRepository.Gets(false, l => l.DocumentCopyId == documentCopyId && l.UserId == userId &&
                    (nodeId == null || l.NodeId == nodeId) && l.FromDate.Year == fromDate.Year &&
                    l.FromDate.Month == fromDate.Month &&
                    l.FromDate.Day == fromDate.Day &&
                    l.FromDate.Hour == fromDate.Hour &&
                    l.FromDate.Minute == fromDate.Minute).FirstOrDefault();
            //&&l.FromDate.Second == fromDate.Second);
        }
		
        /// <author>
        /// CuongNT@bkav.com - 280813: chuyen public --> internal
        /// </author>
        /// <summary>
        /// Thêm mới timeline khi bắt đầu xử lý văn bản tại một node trên quy trình
        /// </summary>
        /// <param name="docTimeline">Thực thể thời gian xử lý văn bản</param>
        /// <param name="isSaveChanges">Có gọi SaveChanges luôn trong hàm hay không?</param>
        /// <returns></returns>
        internal void Create(DocTimeline docTimeline, bool isSaveChanges = true)
        {
            if (docTimeline == null)
            {
                throw new ArgumentNullException("docTimeline");
            }

            if (!_docTimelineRepository.Exist(d =>
                d.DocumentId == docTimeline.DocumentId &&
                d.DocumentCopyId == docTimeline.DocumentCopyId &&
                d.DocumentCopyType == docTimeline.DocumentCopyType &&
                d.FromDate == docTimeline.FromDate &&
                d.IsWorkingTime == docTimeline.IsWorkingTime &&
                d.NodeId == docTimeline.NodeId &&
                d.NodeName == docTimeline.NodeName &&
                d.UserId == docTimeline.UserId))
            {
                _docTimelineRepository.Create(docTimeline);
                if (isSaveChanges)
                {
                    Context.SaveChanges();
                }
            };
        }

        /// <author>
        /// CuongNT@bkav.com - 280813: chuyen public --> internal
        /// </author>
        /// <summary>
        /// Cập nhật timeline khi kết thúc xử lý văn bản tại một node trên quy trình
        /// </summary>
        /// <param name="docTimeline"> </param>
        /// <param name="toDate"> </param>
        /// <param name="isSaveChanges"> Có gọi SaveChanges luôn trong hàm hay không?</param>
        internal void Update(DocTimeline docTimeline, DateTime toDate, bool isSaveChanges = true)
        {
            if (docTimeline == null)
            {
                throw new ArgumentNullException("docTimeline");
            }
            docTimeline.ToDate = toDate;
            docTimeline.ProcessedMinutes = _worktimeHelper.GetWorkminutes(docTimeline.FromDate, toDate);
            docTimeline.IsSuccess = true;
            if (isSaveChanges)
            {
                Context.SaveChanges();
            }
        }
		
        internal void DeleteByDocumentCopy(int documentCopyId)
        {
            var docTimelines = _docTimelineRepository.Gets(false, t => t.DocumentCopyId == documentCopyId);
            if (docTimelines.Any())
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var docTimeline in docTimelines)
                {
                    _docTimelineRepository.Delete(docTimeline);
                }
                Context.Configuration.AutoDetectChangesEnabled = true;
                Context.SaveChanges();
            }
        }		

		/// <summary>
		/// Trả về danh sách timeline tiến độ xử lý văn bản liên thông theo khoảng thời gian
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		public IEnumerable<dynamic> GetDocOnlineTimelines(DateTime from, DateTime to)
		{
			var cmd = @"
				select d.DocCode, d.`Status`, d.OrganizationCode, d.DocumentId, d.Address as EDocumentId, d.DateFinished, d.UserCreatedId, dl.UserId, dl.UserSendId, dl.FromDate, dl.ToDate
				from document d
				join doctimeline dl on d.DocumentId = dl.DocumentId
				WHERE d.Original = 2 and d.Status in (2, 4, 8) and dl.DocumentCopyType = 1 AND d.DateModified  >= @from and  d.DateModified  <= @to; ";

			var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("from", from));
			parameters.Add(new SqlParameter("to", to));
			
			var result = Context.RawQuery(cmd, parameters.ToArray());
			return result;
		}
    }
}
