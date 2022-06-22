using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Statistic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Business.Objects.StatisticXlvb;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Lớp xử lý dữ liệu thống kê
    /// </summary>
    public class StatisticBll : ServiceBase
    {
        private readonly WorkflowHelper _workflowHelper;
        private readonly DocumentPublishBll _documentPublishService;
        private readonly WorkflowBll _workflowService;
        private readonly MemoryCacheManager _cacheManager;
        private readonly AdminGeneralSettings _adminSettings;
        private readonly UserBll _userService;
        private readonly DepartmentBll _departmentService;
        private readonly DocumentBll _documentService;
        private readonly DocTypeBll _doctypeService;
        private readonly PositionBll _positionService;
        private readonly DocTimelineBll _docTimelineService;
        private readonly WorktimeHelper _worktimeHelper;
        private readonly MemoryCacheManager _cache;
        private readonly AddressBll _addressService;

        private const string DEFAULT_DATETIME_FORMAT = "dd/MM/yyyy";

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        public StatisticBll(IDbCustomerContext context)
            : base(context)
        {
            _workflowHelper = DependencyResolver.Current.GetService<WorkflowHelper>();
            _documentPublishService = DependencyResolver.Current.GetService<DocumentPublishBll>();
            _workflowService = DependencyResolver.Current.GetService<WorkflowBll>();
            _cacheManager = DependencyResolver.Current.GetService<MemoryCacheManager>();
            _adminSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _departmentService = DependencyResolver.Current.GetService<DepartmentBll>();
            _positionService = DependencyResolver.Current.GetService<PositionBll>();
            _docTimelineService = DependencyResolver.Current.GetService<DocTimelineBll>();
            _worktimeHelper = DependencyResolver.Current.GetService<WorktimeHelper>();
            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
            _cache = DependencyResolver.Current.GetService<MemoryCacheManager>();
            _addressService = DependencyResolver.Current.GetService<AddressBll>();
        }

        /// <summary>
        /// Trả về kết quả thống kê tình trạng xử lý hồ sơ chung của cơ quan hiện tại
        /// </summary>
        /// <param name="hasOldDocument">Lấy danh sách hồ sơ từ kỳ trước</param>
        /// <param name="from">Thời gian bắt đầu lấy thống kê</param>
        /// <param name="to">Thời gian kết thúc lấy thống kê</param>
        /// <param name="hasHsmc"></param>
        /// <param name="hasXlvb"></param>
        /// <returns></returns>
        public ProgressStatistic GetProgressStatistic(bool hasOldDocument, bool hasXlvb, bool hasHsmc, DateTime from, DateTime to)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);
            return ParseStatistic(docs, from, to);
        }

        /// <summary>
        /// Trả về kết quả thống kê tình trạng xử lý hồ sơ chi tiết của cơ quan
        /// </summary>
        /// <param name="hasOldDocument">Lấy danh sách hồ sơ từ kỳ trước</param>
        /// <param name="hasHsmc">Lấy hồ sơ một cửa</param>
        /// <param name="hasXlvb">Lấy văn bản</param>
        /// <param name="from">Thời gian bắt đầu lấy thống kê</param>
        /// <param name="to">Thời gian kết thúc lấy thống kê</param>
        /// <param name="groupBy">Nhóm mặc định</param>
        /// <returns></returns>
        public IEnumerable<ProgressStatistic> GetProgressStatisticDetail(bool hasOldDocument, bool hasXlvb, bool hasHsmc,
                        DateTime from, DateTime to, string groupBy = "DocTypeName")
        {
            var result = new List<ProgressStatistic>();

            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);

            if (!docs.Any())
            {
                return result;
            }

            result = ParseForGroup(docs, from, to, groupBy);

            return result;
        }

        private List<int> GetNhanVienCapDuoi()
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var userDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
            var position = _positionService.GetCacheAllPosition();

            var result = _userService.GetNhanVienCapDuoi(currentUserId, userDepts, position).ToList();

            if (!result.Contains(currentUserId))
            {
                result.Add(currentUserId);
            }

            return result;
        }

        private List<ProgressStatistic> ParseForGroup(IEnumerable<StatisticObject> docs, DateTime from, DateTime to, string groupBy)
        {
            var result = new List<ProgressStatistic>();

            IEnumerable<IGrouping<string, StatisticObject>> groups;

            switch (groupBy)
            {
                case "DocTypeName":
                    groups = docs.GroupBy(d => d.DocTypeName);
                    break;
                case "CurrentDepartmentName":
                    groups = docs.GroupBy(d => d.CurrentDepartmentName);
                    break;
                case "UserCurrentName":
                    groups = docs.GroupBy(d => d.UserCurrentName);
                    break;
                case "CategoryName":
                    groups = docs.GroupBy(d => d.CategoryName);
                    break;
                default:
                    groups = docs.GroupBy(d => d.DocTypeName);
                    break;
            }

            result.AddRange(groups.Select(g =>
            {
                var groupDocs = g;
                var newResultItem = ParseStatistic(groupDocs, from, to);
                newResultItem.Name = g.Key;
                return newResultItem;
            }).ToList());

            //QuiBQ:Loai tru mot cac phong ban = null
            var resultNew = new List<ProgressStatistic>();
            foreach (var doc in result)
            {
                if (doc.Name != null)
                {
                    resultNew.Add(doc);
                }
            }

            return resultNew;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hasOldDocument"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cateBussinessTypeId"></param>
        /// <returns></returns>
        public int GetTotalDocument(bool hasOldDocument, DateTime from, DateTime to, int cateBussinessTypeId = 1)
        {
            var cached = GetCache();

            var docs = cached.Count(dc =>
                        dc.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh &&
                        dc.DocTypeId.HasValue &&
                        dc.CategoryBusinessId == cateBussinessTypeId &&
                        (dc.Status != (int)DocumentStatus.DuThao && dc.Status != (int)DocumentStatus.LoaiBo) &&
                        ((dc.DateCreated >= from && dc.DateCreated <= to)
                            || (hasOldDocument
                                && dc.DateCreated < from
                                && (!dc.DateFinished.HasValue || dc.DateFinished.Value >= from || !dc.IsSuccess.HasValue)))
                );

            return docs;
        }

        /// <summary>
        /// Trả về danh sách văn bản quá hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasHsmc"></param>
        /// <param name="hasXlvb"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentOverdues(bool hasOldDocument, DateTime from, DateTime to, bool hasHsmc, bool hasXlvb)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);
            return ParseDocumentOverdueList(docs, from, to);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public object PublicStart(DateTime from, DateTime to)
		{
			var paras = new List<SqlParameter>();
			paras.Add(new SqlParameter("from", from));
			paras.Add(new SqlParameter("to", to));
			return Context.RawProcedure("PublicStart", paras.ToArray());
		}

		/// <summary>
		/// Trả về danh sách văn bản đúng hạn của cả hệ thống
		/// </summary>
		/// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="hasHsmc"></param>
		/// <param name="hasXlvb"></param>
		/// <returns></returns>
		public IEnumerable<DocumentOverdue> GetDocumentDungHans(bool hasOldDocument, DateTime from, DateTime to, bool hasHsmc, bool hasXlvb)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);
            return GetDocumentStatisticByStatus(docs, OverdueStatusType.ResolveInTime);
        }

        /// <summary>
        /// Trả về danh sách văn bản trễ hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasHsmc"></param>
        /// <param name="hasXlvb"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentTreHans(bool hasOldDocument, DateTime from, DateTime to, bool hasHsmc, bool hasXlvb)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);
            return GetDocumentStatisticByStatus(docs, OverdueStatusType.ResolveLate);
        }

        public IEnumerable<DocumentOverdue> GetDocumentToiHans(bool hasOldDocument, DateTime from, DateTime to, bool hasHsmc, bool hasXlvb)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);
            return GetDocumentStatisticByStatus(docs, OverdueStatusType.PendingNear);
        }

        /// <summary>
        /// Trả về danh sách văn bản chưa đến hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasHsmc"></param>
        /// <param name="hasXlvb"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentChuaDenHans(bool hasOldDocument, DateTime from, DateTime to, bool hasHsmc, bool hasXlvb)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);
            return GetDocumentStatisticByStatus(docs, OverdueStatusType.Pending);
        }

        /// <summary>
        /// Trả về danh sách văn bản quá hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasHsmc"></param>
        /// <param name="hasXlvb"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentQuaHans(bool hasOldDocument, DateTime from, DateTime to, bool hasHsmc, bool hasXlvb)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, hasXlvb, hasHsmc, from, to);
            return GetDocumentStatisticByStatus(docs, OverdueStatusType.Overdue);
        }

        public IEnumerable<DocumentOverdue> GetDocumentUser(bool hasOldDocument, DateTime from, DateTime to, int userId)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, true, false, from, to);
            return GetDocumentStatisticByUser(docs, userId);
        }

        /// <summary>
        /// Trả về thống kê xử lý theo quy trình
        /// </summary>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetOverdueByWorkflow(DateTime from, DateTime to)
        {
            // Khi tính theo hạn giữ, chỉ nhận người đó nhận dc văn bản trong khoảng thời gian from - to, 
            // Không cần quan tâm văn bản đó được khởi tạo khi nào.
            // => cần lấy cả văn bản tồn kỳ trước (có thể vẫn còn sai -_-)

            var withOldDocument = true;
            var result = GetDocumentQuaHans(withOldDocument, from, to, hasHsmc: true, hasXlvb: true).ToList();
            // var documentCopyIds = result.Select(d => d.DocumentCopyId).Distinct().ToList();

            // Người dùng không tính hạn xử lý
            var ignoreUser = _adminSettings.UserIgnoreOverdueId;

            var timelines = _docTimelineService.Gets(tl =>
                                    tl.FromDate >= from && tl.FromDate <= to
                                    && (ignoreUser == 0 || tl.UserId != ignoreUser));

            var allUsers = _userService.GetAllCached(isActivated: true);

            foreach (var timeLine in timelines)
            {
                var docCopies = result.Where(d => d.DocumentCopyId == timeLine.DocumentCopyId);
                if (!docCopies.Any())
                {
                    continue;
                }

                var docCopy = docCopies.First();
                var user = allUsers.SingleOrDefault(u => u.UserId == timeLine.UserId);
                if (user == null)
                {
                    continue;
                }

                var currentDepartment = "";
                var timeInNode = 1 + (timeLine.TimeInNode / 24);
                var dateOverdue = timeLine.DateOverdue ?? _worktimeHelper.GetDateAppoint(timeLine.FromDate, timeInNode);

                var docOverDue = new DocumentOverdue()
                {
                    DocumentCopyId = docCopy.DocumentCopyId,
                    Compendium = docCopy.CategoryBusinessId == 4 ? docCopy.DocTypeName : docCopy.Compendium,
                    DoctypeId = docCopy.DoctypeId,
                    DocTypeName = docCopy.DocTypeName,
                    DocCode = docCopy.DocCode,
                    DateAppointed = dateOverdue.HasValue ? dateOverdue.Value.ToString("HH:mm dd/MM/yyyy") : "",
                    DateCreated = timeLine.FromDate.ToString("HH:mm dd/MM/yyyy"),
                    DateFinished = timeLine.ToDate.HasValue ? timeLine.ToDate.Value.ToString("d") : "",
                    UserCurrentName = string.Format("{0} ({1})", user.FullName, user.Username),
                    UserCurrentId = timeLine.UserId,
                    UserCreatedId = timeLine.UserSendId,
                    CategoryBusinessId = docCopy.CategoryBusinessId,
                    CurrentDepartmentName = currentDepartment,
                };

                result.Add(docOverDue);
            }

            return result;
        }

        /// <summary>
        /// Trả về thống kê xử lý theo quy trình
        /// </summary>
        /// <param name="docCopy"></param>
        ///<returns></returns>
        public List<DocumentOverdue> GetDocumentProcessDetail(DocumentCopy docCopy)
        {

            var result = new List<DocumentOverdue>();
            if (docCopy == null)
            {
                return result;
            }
            var ignoreUser = _adminSettings.UserIgnoreOverdueId;

            var timelines = _docTimelineService.Gets(tl =>
                                    tl.DocumentCopyId == docCopy.DocumentCopyId);

            var allUsers = _userService.GetAllCached();
            var document = docCopy.Document;
            foreach (var timeLine in timelines)
            {

                var user = allUsers.SingleOrDefault(u => u.UserId == timeLine.UserId);
                if (user == null)
                {
                    continue;
                }

                var currentDepartment = "";
                //var timeInNode = 1 + (timeLine.TimeInNode / 24);
                //var dateOverdue = timeLine.DateOverdue ?? _worktimeHelper.GetDateAppoint(timeLine.FromDate, timeInNode);

                var timeInNode = timeLine.TimeInNode;
                var dateOverdue = timeLine.DateOverdue ?? timeLine.FromDate.AddHours(timeInNode);
                //var isOverdue = false;
                //if (timeLine.ToDate.HasValue)
                //{
                //    if (dateOverdue.Date >  timeLine.ToDate.Value.Date)
                //    {
                //        isOverdue = true;
                //    }
                //    if (dateOverdue.Date == timeLine.ToDate.Value.Date)
                //    {
                //        if (timeLine.ToDate.Value.Date.ToString("tt") == "PM" && dateOverdue.Date.ToString("tt") == "AM")
                //        {
                //            isOverdue = true;
                //        }
                //    }
                //}
                var docOverDue = new DocumentOverdue()
                {
                    DocumentCopyId = docCopy.DocumentCopyId,
                    Compendium = document.CategoryBusinessId == 4 ? document.DocTypeName : document.Compendium,
                    DoctypeId = document.DocTypeId ?? new Guid(),
                    DocTypeName = document.DocTypeName,
                    DocCode = document.DocCode,
                    DateAppointed = dateOverdue.ToString("HH:mm dd/MM/yyyy"),
                    DateCreated = timeLine.FromDate.ToString("HH:mm dd/MM/yyyy"),
                    DateFinished = timeLine.ToDate.HasValue ? timeLine.ToDate.Value.ToString("HH:mm dd/MM/yyyy") : "",
                    UserCurrentName = string.Format("{0} ({1})", user.FullName, user.Username),
                    UserCurrentId = timeLine.UserId,
                    UserCreatedId = timeLine.UserSendId,
                    CategoryBusinessId = document.CategoryBusinessId,
                    CurrentDepartmentName = currentDepartment,
                    CitizenName = document.CitizenName,
                    IsOverdue = IsOverdue(timeLine.ToDate, dateOverdue)
                };

                result.Add(docOverDue);
            }

            return result;
        }

        #region Thông tin khách hàng

        /// <summary>
        /// Lấy tất cả thông tin các công dân
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentCustomerInfo> GetDocumentCustomerInfo(DateTime from, DateTime to)
        {
            var docs = GetsStatisticObjectFromCache(false, false, true, from, to);
            var result = docs.Select(d => new DocumentCustomerInfo()
            {
                Address = d.Address,
                CitizenName = d.CitizenName,
                DocCode = d.DocCode,
                DoctypeName = d.DocTypeName,
                Phone = d.Phone,
                SDateAppointed = d.DateAppointed,
                SDateCreated = d.DateCreated
            });

            result = result.OrderBy(d => d.DoctypeName).ThenBy(d => d.DocCode);

            return result;
        }

        /// <summary>
        /// Trả về số công dân, doanh nghiệp đã đk hồ sơ
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountCustomer(DateTime from, DateTime to)
        {
            var docs = GetsStatisticObjectFromCache(false, false, true, from, to);
            return docs.Count();
        }

        #endregion

        #region Liên thông

        /// <summary>
        /// Trả về tất cả hồ sơ liên thông theo khoảng thời gian
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<LienThongDto> GetLienThongs(DateTime from, DateTime to)
        {
            var result = new List<LienThongDto>();
            var docPublishs = _documentPublishService.GetLienThongs(from, to);
            if (!docPublishs.Any())
            {
                return result;
            }

            var docs = GetCache();

            var docPublishGroups = docPublishs.GroupBy(dp => dp.DocumentCopyId);
            foreach (var group in docPublishGroups)
            {
                var doc = docs.SingleOrDefault(d => d.DocumentCopyId == group.Key);
                if (doc == null)
                {
                    continue;
                }

                var docPublish = group.First();

                var newLienThongItem = new LienThongDto()
                {
                    DocCode = docPublish.DocCode,
                    Compendium = doc.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc
                            ? doc.CitizenName : doc.Compendium,
                    DatePublished = docPublish.DatePublished.ToString(DEFAULT_DATETIME_FORMAT),
                    CitizenName = doc.CitizenName,
                    DocumentCopyId = docPublish.DocumentCopyId,
                    Address = docPublish.AddressName,
                    IsResponsed = docPublish.IsResponsed,
                    DateResponsed = docPublish.DateResponsed.HasValue ? docPublish.DateResponsed.Value.ToString(DEFAULT_DATETIME_FORMAT) : "",
                    DateAppointed = docPublish.DateAppointed.HasValue ? docPublish.DateAppointed.Value.ToString(DEFAULT_DATETIME_FORMAT) : "",
                    Children = new List<LienThongDto>()
                };

                if (group.Count() > 1)
                {
                    // Bỏ qua đối tượng đầu tiên
                    for (var i = 1; i < group.Count(); i++)
                    {
                        var docPublishChild = group.ElementAt(i);
                        newLienThongItem.Children.Add(new LienThongDto()
                        {
                            DatePublished = docPublishChild.DatePublished.ToString(DEFAULT_DATETIME_FORMAT),
                            Address = docPublishChild.AddressName,
                            IsResponsed = docPublishChild.IsResponsed,
                            DateResponsed = docPublishChild.DateResponsed.HasValue ? docPublish.DateResponsed.Value.ToString(DEFAULT_DATETIME_FORMAT) : "",
                            DateAppointed = docPublishChild.DateAppointed.HasValue ? docPublish.DateAppointed.Value.ToString(DEFAULT_DATETIME_FORMAT) : ""
                        });
                    }
                }

                result.Add(newLienThongItem);
            }
            return result;
        }

        /// <summary>
        /// Trả về số lượng hồ sơ liên thông
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountLienThong(DateTime from, DateTime to)
        {
            return _documentPublishService.CountLienThong(from, to);
        }

        #endregion

        #region Đăng ký qua mạng

        /// <summary>
        /// Trả về danh sách hồ sơ đăng ký qua mạng của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentOnlines(bool hasOldDocument, DateTime from, DateTime to)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, false, true, from, to);
            docs = docs.Where(d => d.Original == 1);

            return ParseDocumentOverdueListNotWithWorkflow(docs);
        }

        /// <summary>
        /// Trả về số lượng hồ sơ đăng ký qua mạng.
        /// </summary>
        /// <param name="hasOldDocument"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountDocumentOnline(bool hasOldDocument, DateTime from, DateTime to)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, false, true, from, to);
            return docs.Count(d => d.Original == 1);
        }

        /// <summary>
        /// Trả về kết quả thống kê tình trạng xử lý hồ sơ chi tiết của cơ quan
        /// </summary>
        /// <param name="hasOldDocument">Lấy danh sách hồ sơ từ kỳ trước</param>
        /// <param name="from">Thời gian bắt đầu lấy thống kê</param>
        /// <param name="to">Thời gian kết thúc lấy thống kê</param>
        /// <returns></returns>
        public IEnumerable<ProgressStatistic> GetOnlineStatisticDetail(bool hasOldDocument, DateTime from, DateTime to)
        {
            var result = new List<ProgressStatistic>();

            var docs = GetsStatisticObjectFromCache(hasOldDocument, false, true, from, to);
            if (!docs.Any())
            {
                return result;
            }

            docs = docs.Where(d => d.Original == 1);

            var groups = docs.GroupBy(d => d.DocTypeName);
            result.AddRange(groups.Select(g =>
            {
                var groupDocs = g;
                var newResultItem = ParseStatistic(groupDocs, from, to);
                newResultItem.Name = g.Key;
                return newResultItem;
            }).ToList());

            return result;
        }

        /// <summary>
        /// Trả về kết quả thống kê tình trạng xử lý hồ sơ chung của cơ quan hiện tại
        /// </summary>
        /// <param name="hasOldDocument">Lấy danh sách hồ sơ từ kỳ trước</param>
        /// <param name="from">Thời gian bắt đầu lấy thống kê</param>
        /// <param name="to">Thời gian kết thúc lấy thống kê</param>
        /// <returns></returns>
        public ProgressStatistic GetOnlineStatistic(bool hasOldDocument, DateTime from, DateTime to)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, false, true, from, to);
            docs = docs.Where(d => d.Original == 1);
            return ParseStatistic(docs, from, to);
        }

        #endregion

        /// <summary>
        /// Get cache document 3 năm gần nhất.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<StatisticObject> GetCache()
        {
            var year = DateTime.Now.Year;
            var cacheKey = string.Format(CacheParam.StatisticKey, year);
            var documentInYears = _cacheManager.Get<IEnumerable<StatisticObject>>(cacheKey, CacheParam.StatisticCacheTimeOut, () =>
            {
                var result = GetsCacheDocuments(year - 2);
                result = result.Concat(GetsCacheDocuments(year - 1));
                result = result.Concat(GetsCacheDocuments(year));

                //var result = GetsCacheDocuments(year);
                return result;
            });
            return documentInYears;
        }

        /// <summary>
        /// Xóa cache thống kê.
        /// </summary>
        public void ClearCache()
        {
            var currentYear = DateTime.Now.Year;
            var cacheKey = string.Format(CacheParam.StatisticKey, currentYear);
            _cacheManager.Remove(cacheKey);
        }

        #region Private Methods

        private ProgressStatistic GetStatistic(IEnumerable<DocumentCopy> docs, DateTime from, DateTime to)
        {
            var result = new ProgressStatistic();
            if (!docs.Any())
            {
                return result;
            }

            // Tổng xử lý
            result.Total = docs.Count();

            // Nhận trong kỳ
            result.NewReception = docs.Count(d => d.Document.DateCreated >= from && d.Document.DateCreated <= to);

            // Tồn kỳ trước
            result.PreExtisting = result.Total - result.NewReception;

            // Tổng đã xử lý
            result.TotalSolved = docs.Count(d => d.Document.DateResult.HasValue || d.Document.DateReturned.HasValue || d.Document.DateFinished.HasValue);

            // Đã giải quyết đúng hạn
            result.SolvedInTime = docs.Where(d => StatisticUtil.OverdueStatus(d.Document.Status, d.Document.DateAppointed.Value, d.Document.DateResult,
                    d.Document.DateReturned,
                    d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy ? d.DateFinished : d.Document.DateFinished,
                    d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.ResolveInTime).Count();

            // Đã giải quyết trễ hẹn
            result.SolvedLate = result.TotalSolved - result.SolvedInTime;

            // Tổng chưa giải quyết
            result.TotalPending = result.Total - result.TotalSolved;

            var pendingLateDocs = docs.Where(d => StatisticUtil.OverdueStatus(d.Document.Status, d.Document.DateAppointed.Value, d.Document.DateResult,
                    d.Document.DateReturned,
                    d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy ? d.DateFinished : d.Document.DateFinished,
                    d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.Overdue);

            // Chưa giải quyết quá hạn
            result.PendingLate = pendingLateDocs.Count();

            // Chưa giải quyết trong hạn
            result.Pending = result.TotalPending - result.PendingLate;

            return result;
        }

        private ProgressStatistic ParseStatistic(IEnumerable<StatisticObject> docs, DateTime from, DateTime to)
        {
            var result = new ProgressStatistic();
            if (!docs.Any())
            {
                return result;
            }

            // Tổng xử lý
            result.Total = docs.Count();

            // Nhận trong kỳ
            result.NewReception = docs.Count(d => d.DateCreated >= from && d.DateCreated <= to);

            // Tồn kỳ trước
            result.PreExtisting = result.Total - result.NewReception;

            // Đã giải quyết đúng hạn
            result.SolvedInTime = docs.Where(d => d.OverdueStatusType == OverdueStatusType.ResolveInTime).Count();

            // Đã giải quyết trễ hẹn
            result.SolvedLate = docs.Count(d => d.OverdueStatusType == OverdueStatusType.ResolveLate);

            // Tổng đã xử lý
            result.TotalSolved = result.SolvedInTime + result.SolvedLate;

            // Tổng chưa giải quyết
            result.TotalPending = result.Total - result.TotalSolved;

            var pendingLateDocs = docs.Where(d => d.OverdueStatusType == OverdueStatusType.Overdue);

            // Chưa giải quyết quá hạn
            result.PendingLate = pendingLateDocs.Count();

            // Chưa giải quyết trong hạn
            result.Pending = result.TotalPending - result.PendingLate;

            var vbQuaHanVPUB = docs.Where(d => d.OverdueStatusTypeVPUB == OverdueStatusTypeVPUB.Overdue);
            var vbTreHenVPUB = docs.Where(d => d.OverdueStatusTypeVPUB == OverdueStatusTypeVPUB.ResolveInTime);
            if (vbQuaHanVPUB != null)
            {
                result.QuaHanVPUB = vbQuaHanVPUB.Count();
            }
            else
            {
                result.QuaHanVPUB = 0;
            }

            if (vbTreHenVPUB != null)
            {
                result.TreHenVPUB = vbTreHenVPUB.Count();
            }
            else
            {
                result.TreHenVPUB = 0;
            }

            return result;
        }
        private IEnumerable<DocumentOverdue> ParseDocumentOverdueList(IEnumerable<StatisticObject> docs, DateTime from, DateTime to)
        {
            var result = new List<DocumentOverdue>();
            if (docs == null || !docs.Any())
            {
                return result;
            }

            var dateNow = DateTime.Now;

            var workFlows = _workflowService.GetsFromCache().ToList();
            if (workFlows == null || !workFlows.Any())
            {
                throw new ApplicationException("WorkFlow is not exist.");
            }

            foreach (var document in docs)
            {
                var currentHistory = document.Histories.HistoryPath.LastOrDefault(h => h.DateCreated >= from && h.DateCreated <= to);
                if (currentHistory == null)
                {
                    continue;
                }

                var workFlow = workFlows.SingleOrDefault(w => w.WorkflowId == currentHistory.WorkflowReceiveId);   //.FirstOrDefault(p => p.WorkflowId == currentHistory.WorkflowReceiveId);
                if (workFlow == null)
                {
                    continue;
                }

                Node currentNode = null;
                try
                {
                    currentNode = _workflowHelper.GetNode(workFlow, currentHistory.NodeReceiveId);
                    if (currentNode == null)
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }

                var isProcessing = document.Status == (int)DocumentStatus.DangXuLy
                                || document.Status == (int)DocumentStatus.DungXuLy;

                var dateProcess = document.DateFinished;
                // var dateProcess = (isProcessing || !document.DateFinished.HasValue) ? DateTime.Now : document.DateFinished.Value;

                var currentNodeKeepTime = dateProcess.HasValue ? (dateProcess.Value - currentHistory.DateCreated).Days + 1 : 0;
                var totalKeepTime = dateProcess.HasValue ? (dateProcess.Value - document.DateCreated).Days + 1 : 0;

                var currentDepartment = document.CategoryBusinessId == 4
                                                ? document.InOutPlace
                                                : document.CurrentDepartmentName;
                var docOverDue = new DocumentOverdue()
                {
                    DocumentCopyId = document.DocumentCopyId,
                    Compendium = document.CategoryBusinessId == 4 ? document.DocTypeName : document.Compendium,
                    DoctypeId = document.DocTypeId ?? Guid.NewGuid(),
                    DocTypeName = document.DocTypeName,
                    DocCode = document.DocCode,
                    CitizenName = document.CitizenName,
                    DateAppointed = document.DateAppointed.HasValue
                                            ? document.DateAppointed.Value.ToString(DEFAULT_DATETIME_FORMAT) : "",
                    DateFinished = dateProcess.HasValue ? dateProcess.Value.ToString(DEFAULT_DATETIME_FORMAT) : "",
                    DateCreated = document.DateCreated.ToString(DEFAULT_DATETIME_FORMAT),
                    KyBaoCao = document.DatePublish,
                    UserCreatedName = document.UserCreatedName,
                    UserCurrentName = document.UserCurrentName,
                    UserCurrentId = document.UserCurrentId,
                    UserCreatedId = document.UserCreatedId,
                    CategoryBusinessId = document.CategoryBusinessId,
                    Deadline = document.ExpireProcess.HasValue ? (totalKeepTime - document.ExpireProcess.Value) : 0,
                    CurrentDepartmentName = currentDepartment
                };
                docOverDue.CategoryName = document.CategoryName;

                result.Add(docOverDue);
            }

            return result.ToList();
        }

        private IEnumerable<DocumentOverdue> GetDocumentStatisticByStatus(IEnumerable<StatisticObject> docs, OverdueStatusType status)
        {
            var result = docs.Where(d => d.OverdueStatusType == status).ToList();
            if (status == OverdueStatusType.PendingNear)
            {
                result = docs.Where(d => d.OverdueStatusType == OverdueStatusType.Pending && d.DateAppointed.HasValue && (d.DateAppointed.Value - DateTime.Now).TotalDays <= 1).ToList();
            }
            return ParseDocumentOverdueListNotWithWorkflow(result);
        }

        private IEnumerable<DocumentOverdue> GetDocumentStatisticByUser(IEnumerable<StatisticObject> docs, int userId)
        {
            var result = docs.Where(d => d.UserCurrentId == userId).ToList();
           
            return ParseDocumentOverdueListNotWithWorkflow(result);
        }

        private IEnumerable<StatisticObject> GetsStatisticObjectFromCache(bool hasOldDocument, bool hasXlvb, bool hasHsmc, DateTime from, DateTime to)
        {
            var fromYear = from.Year;
            var toYear = to.Year;
            var allUses = _userService.GetAllCached();
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();

            var allDepts = _departmentService.GetCacheAllDepartments();
            var documentInYears = GetCache();
            var result = new List<StatisticObject>();

            if (!hasHsmc)
            {
                documentInYears = documentInYears.Where(d => d.CategoryBusinessId == (int)CategoryBusinessTypes.VbDen
                                            || d.CategoryBusinessId == (int)CategoryBusinessTypes.VbDi).ToList();
            }

            if (!hasXlvb)
            {
                documentInYears = documentInYears.Where(d => d.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc && d.DateAppointed.HasValue
                                                                && d.DocumentCopyType != (int)DocumentCopyTypes.DongXuLy).ToList();
            }

            if (hasOldDocument)
            {
                result.AddRange(GetPreExistingDocuments(documentInYears, from));
            }

            result.AddRange(documentInYears.Where(d => d.DateCreated >= from && d.DateCreated <= to));

            foreach (var doc in result)
            {
                if (doc.Histories != null && doc.Histories.HistoryPath.Any())
                {
                    var history = doc.Histories.HistoryPath.OrderByDescending(d => d.DateCreated);
                    var userHistory = history.FirstOrDefault(d => d.DateCreated <= to.Date);
                    if (userHistory != null)
                    {
                        doc.UserCurrentId = userHistory.UserReceiveId;
                        var userCurrent = allUses.FirstOrDefault(u => u.UserId == doc.UserCurrentId);
                        if (userCurrent != null)
                        {
                            doc.UserCurrentName = userCurrent.FullName;
                        }
                    }
                    //if (doc.DocCode == "2.19.2018/DD-TNMT")
                    //{
                    //    var a = userHistory.UserReceiveId;
                    //}
                }
                var docObject = TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(doc, to);
                doc.OverdueStatusType = StatisticUtil.OverdueStatus(docObject.Status, docObject.DateAppointed ?? DateTime.Now, docObject.DateResult, docObject.DateReturned,
                                 docObject.DateFinished, docObject.DateRequireSupplementary, to, doc.DocCode);

                doc.OverdueStatusTypeVPUB = StatisticUtil.OverdueStatusVPUB(doc.IsLTVPUB, docObject.DateAppointVPUB, docObject.DateResponsedVPUB);
                if (doc.OverdueStatusType == OverdueStatusType.Pending)
                {
                    doc.OverdueStatusTypeVPUB = OverdueStatusTypeVPUB.Pending;
                }

                doc.CurrentDepartmentExt = "";
                doc.CurrentDepartmentPath = "";
                var userDept = allUserDepts.Where(u => u.UserId == doc.UserCurrentId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
                if (userDept == null)
                {
                    continue;
                }
                //var doc
                var dept = allDepts.SingleOrDefault(d => d.DepartmentId == userDept.DepartmentId);
                if (dept == null)
                {
                    continue;
                }

                doc.CurrentDepartmentExt = dept.DepartmentIdExt;
                doc.CurrentDepartmentPath = dept.DepartmentPath;
             }

            var currentUser = _userService.CurrentUser;
            if (currentUser != null && currentUser.HasViewReport == false)
            {
                var userToStatictis = GetNhanVienCapDuoi();
                result = result.Where(d => userToStatictis.Contains(d.UserCurrentId)).ToList();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private IEnumerable<StatisticObject> GetsCacheDocuments(int year)
        {
            var ignoreUser = 0;//  _adminSettings.UserIgnoreOverdueId;

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@year", year));
            parameters.Add(new SqlParameter("@userId", ignoreUser == 0 ? 0 : ignoreUser));

            var docs = Context.RawProcedure("statistic", parameters.ToArray());
            var a = Json2.Stringify(docs);
            var result = Json2.ParseAs<IEnumerable<StatisticObject>>(a);

            return result;
        }

        /// <summary>
        /// Trả về danh sách hồ sơ tồn kỳ trước: chưa được xử lý ở kỳ trước
        /// </summary>
        /// <param name="cachedDocs"></param>
        /// <param name="to">Mốc thời gian</param>
        /// <returns></returns>
        private IEnumerable<StatisticObject> GetPreExistingDocuments(IEnumerable<StatisticObject> cachedDocs, DateTime to)
        {
            var docs = cachedDocs.Where(d => d.DateCreated < to);
            var result = new List<StatisticObject>();

            foreach (var doc in docs)
            {
                var docObject = TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(doc, to);

                var overdueStatusType = StatisticUtil.OverdueStatus(docObject.Status, docObject.DateAppointed.Value, docObject.DateResult, docObject.DateReturned,
                                                docObject.DateFinished, docObject.DateRequireSupplementary, to, doc.DocCode);

                if (overdueStatusType == OverdueStatusType.Overdue || overdueStatusType == OverdueStatusType.Pending)
                {
                    result.Add(doc);
                }
            }

            return result;
        }

        /// <summary>
        /// Tính toán lại các khoảng thời gian xử lý và trạng thái văn bản , hồ sơ ở thời điểm TO
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private StatisticObject TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(StatisticObject doc, DateTime to)
        {
            var result = new StatisticObject();

            result.Status = doc.Status;
            result.DateAppointed = doc.DateAppointed ?? DateTime.Now;
            result.DateSuccess = doc.DateSuccess;
            result.DateReturned = doc.DateReturned;
            result.DateResult = doc.DateResult;
            result.DateFinished = doc.DateFinished;
            result.DateRequireSupplementary = doc.DateRequireSupplementary;
            result.DateAppointVPUB = doc.DateAppointVPUB;
            result.DateResponsedVPUB = doc.DateResponsedVPUB;

            if (doc.DateAppointVPUB.HasValue && doc.DateAppointVPUB.Value > to)
            {
                result.DateAppointVPUB = null;
            }

            if (doc.DateResponsedVPUB.HasValue && doc.DateResponsedVPUB.Value > to)
            {
                result.DateResponsedVPUB = null;
            }

            if (doc.DateSuccess.HasValue && doc.DateSuccess.Value > to)
            {
                result.DateSuccess = null;
                result.IsSuccess = null;
            }

            if (doc.DateResult.HasValue && doc.DateResult.Value > to)
            {
                result.DateResult = null;
            }

            if (doc.DateReturned.HasValue && doc.DateReturned.Value > to)
            {
                result.DateReturned = null;
                result.IsReturned = null;
            }

            if (doc.DateFinished.HasValue && doc.DateFinished.Value > to)
            {
                if (doc.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy)
                {
                    result.DateFinished = null;
                    result.Status = (int)DocumentStatus.DangXuLy;
                }
                else
                {
                    result.DateFinished = null;
                    result.Status = (int)DocumentStatus.DangXuLy;
                }
            }

            if (doc.DateRequireSupplementary.HasValue && doc.DateRequireSupplementary.Value > to)
            {
                result.DateRequireSupplementary = null;
                result.Status = (int)DocumentStatus.DangXuLy;
            }

            if (doc.Status == (int)DocumentStatus.DungXuLy && doc.DateSuccess == null && !doc.DateRequireSupplementary.HasValue)
            {
                // Truong hop Duyet xong gui tra ket qua: chuyen trang thai Dung xu ly
                result.Status = (int)DocumentStatus.DangXuLy;
            }

            return result;
        }


        //private IEnumerator<StatisticObject> TinhLaiNguoiXuly(IEnumerable<StatisticObject> cachedDocs)
        //{
        //    var doctime = "";
        //    return 
        //}

        private IEnumerable<DocumentOverdue> ParseDocumentOverdueListNotWithWorkflow(IEnumerable<StatisticObject> docs)
        {
            var result = new List<DocumentOverdue>();
            if (docs == null || !docs.Any())
            {
                return result;
            }

            docs = docs.OrderBy(d => d.DocCode);
            var count = docs.Count();
            foreach (var doc in docs)
            {
                var isProcessing = doc.Status == (int)DocumentStatus.DangXuLy || doc.Status == (int)DocumentStatus.DungXuLy;
                var dateProcess = isProcessing ? DateTime.Now : doc.DateFinished ?? DateTime.Now;

                result.Add(new DocumentOverdue()
                {
                    DocumentCopyId = doc.DocumentCopyId,
                    Compendium = doc.CategoryBusinessId == 4 ? doc.CitizenName : doc.Compendium,
                    DoctypeId = doc.DocTypeId ?? Guid.Empty,
                    DocTypeName = doc.DocTypeName,
                    DocCode = doc.DocCode,
                    CitizenName = doc.CitizenName,
                    DateAppointed = doc.DateAppointed.HasValue ? doc.DateAppointed.Value.ToString(DEFAULT_DATETIME_FORMAT) : "",
                    DateFinished = GetDateFinished(doc.DateResult, doc.DateReturned, doc.DateFinished),
                    DateCreated = doc.DateCreated.ToString(DEFAULT_DATETIME_FORMAT),
                    DateSuccess = doc.DateSuccess.HasValue ? doc.DateSuccess.Value.ToString(DEFAULT_DATETIME_FORMAT) : "",
                    UserCurrentName = doc.UserCurrentName,
                    UserCurrentId = doc.UserCurrentId,
                    UserCreatedId = doc.UserCreatedId,
                    CategoryBusinessId = doc.CategoryBusinessId,
                    CurrentDepartmentName = doc.CategoryBusinessId == 4 ? doc.InOutPlace : doc.CurrentDepartmentName,
                    Deadline = doc.DateAppointed.HasValue ? (int)(dateProcess - doc.DateAppointed.Value).TotalDays : 0,
                    StatusName = OverStatus((int)doc.OverdueStatusType),
                    StatusDXLName = doc.StatusDXLName
                });
            }

            return result;
        }

        private string OverStatus(int status)
        {
            switch (status)
            {
                case (int)OverdueStatusType.Overdue:
                    return "Quá hạn";
                case (int)OverdueStatusType.Pending:
                    return "Chưa đến hạn";
                case (int)OverdueStatusType.ResolveInTime:
                    return "Đúng hạn";
                case (int)OverdueStatusType.ResolveLate:
                    return "Trễ hạn";
                default:
                    break;
            }
            return "";
           
        }
        private string GetDateFinished(DateTime? dateSuccess, DateTime? dateReturned, DateTime? dateFinished)
        {
            var dateformat = DEFAULT_DATETIME_FORMAT;
            if (dateSuccess.HasValue)
            {
                return dateSuccess.Value.ToString(dateformat);
            }

            if (dateReturned.HasValue)
            {
                return dateReturned.Value.ToString(dateformat);
            }

            if (dateFinished.HasValue)
            {
                return dateFinished.Value.ToString(dateformat);
            }

            return string.Empty;
        }

        #endregion

        #region Xử lý văn bản

        #region Văn bản đến

        /// <summary>
        /// Giám sát xử lý văn bản tổng quan
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasOldDocument"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public List<ProgressStatisticXlvb> GiamSatTong_Xlvb(DateTime from, DateTime to, bool hasOldDocument, ReportViewType viewType)
        {
            var timelines = GetDocumentTimelines(from, to, hasOldDocument);

            var result = new List<ProgressStatisticXlvb>();
            switch (viewType)
            {
                case ReportViewType.User:
                    result = VBden_GroupingByUser(timelines, from, to);
                    break;
                default:
                    result = VBden_GroupingByDepartment(timelines, from, to);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Giám sát xử lý văn bản tổng quan
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasOldDocument"></param>
        /// <param name="viewType"></param>
        /// <param name="categoryBussinessId"></param>
        /// <returns></returns>
        public List<ProgressStatisticXlvb> GiamSatTong_XlvbNormal(DateTime from, DateTime to, bool hasOldDocument, ReportViewType viewType, int categoryBussinessId = 1)
        {
            var docs = GetsStatisticObjectFromCache(hasOldDocument, true, false, from, to).Where(d => d.CategoryBusinessId == categoryBussinessId);
            if (docs == null)
            {
                docs = new List<StatisticObject>();
            }
            var result = new List<ProgressStatisticXlvb>();
            switch (viewType)
            {
                case ReportViewType.Department:
                    result = VBden_GroupingByUserNormal(docs, from, to, false);
                    break;
                default:
                    result = VBden_GroupingByUserNormal(docs, from, to);
                    break;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasOldDocument"></param>
        /// <param name="isProcess"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public dynamic VanBanDenOverdue(DateTime from, DateTime to, bool hasOldDocument, bool isProcess, string groupBy)
        {
            var timelines = GetDocumentTimelines(from, to, hasOldDocument);
            if (isProcess)
            {
                timelines = timelines.Where(t => t.IsOverdue && t.IsProcess);
            }
            else
            {
                timelines = timelines.Where(t => t.IsOverdue && !t.IsProcess);
            }

            var result = new List<VanBanDenOverdue>();
            bool isParent;

            if (IsGrouping(groupBy))
            {
                IEnumerable<IGrouping<string, StatisticTimeline>> groups;
                switch (groupBy)
                {
                    case "Department":
                        groups = timelines.GroupBy(d => d.DepartmentName).OrderBy(g => g.Key);
                        break;
                    default:
                        groups = timelines.GroupBy(d => d.UserFullName).OrderBy(g => g.Key);
                        break;
                }

                foreach (var group in groups)
                {
                    result.Add(new VanBanDenOverdue()
                    {
                        GroupName = ParseDepartmentName(group.Key, out isParent),
                        GroupCount = group.Count(),
                        IsGroup = true
                    });

                    foreach (var item in group)
                    {
                        item.GroupName = ParseDepartmentName(group.Key, out isParent);
                    }

                    result.AddRange(group.OrderBy(t => t.FromDate).Select(t => new VanBanDenOverdue
                                    {
                                        DocCode = t.DocCode,
                                        Compendium = t.Compendium,
                                        UserFullName = t.UserFullName,
                                        FromDate = t.FromDate.ToString("g"),
                                        ToDate = t.ToDate.HasValue ? t.ToDate.Value.ToString("g") : "",
                                        DateOverdue = t.DateOverdue.Value.ToString("g"),
                                        //DepartmentName =  ParseDepartmentName(t.DepartmentName, out isParent),
                                        GroupName = t.GroupName
                                    }));
                }

                return result;
            }

            return timelines.OrderBy(t => t.FromDate).Select(t => new VanBanDenOverdue
                                    {
                                        DocCode = t.DocCode,
                                        Compendium = t.Compendium,
                                        UserFullName = t.UserFullName,
                                        FromDate = t.FromDate.ToString("g"),
                                        ToDate = t.ToDate.HasValue ? t.ToDate.Value.ToString("g") : "",
                                        DateOverdue = t.DateOverdue.Value.ToString("g"),
                                        //DepartmentName = ParseDepartmentName(t.DepartmentName, out isParent),
                                        //GroupName = t.GroupName
                                    });
        }

        /// <summary>
        /// Thống kê văn bản đến quá hạn theo quy trình
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasOldDocument"></param>
        /// <param name="isProcess"></param>
        /// <param name="groupBy"></param>
        /// <param name="categoryBusinessId"></param>
        /// <returns></returns>
        public dynamic VanBanDenOverdueNormal(DateTime from, DateTime to, bool hasOldDocument, bool isProcess, string groupBy, int categoryBusinessId = 1, bool overdues = true, int storeId =0)
        {
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();

            var allDepts = _departmentService.GetCacheAllDepartments();
            var docs = GetDocumentQuaHans(hasOldDocument, from, to, true, false).Where(d => d.CategoryBusinessId == categoryBusinessId);
            if (overdues)
            {
                if (isProcess)
                {
                    docs = GetDocumentTreHans(hasOldDocument, from, to, true, false).Where(d => d.CategoryBusinessId == categoryBusinessId); ;
                }
            }
            else
            {
                if (isProcess)
                {
                    docs = GetDocumentDungHans(hasOldDocument, from, to, true, false).Where(d => d.CategoryBusinessId == categoryBusinessId); ;
                }
                else
                {
                    if (storeId == 5)
                    {
                        docs = GetDocumentToiHans(hasOldDocument, from, to, true, false).Where(d => d.CategoryBusinessId == categoryBusinessId); ;
                    }
                    else
                    {
                        docs = GetDocumentChuaDenHans(hasOldDocument, from, to, true, false).Where(d => d.CategoryBusinessId == categoryBusinessId); ;
                    }
                }
            }

            if (docs == null)
            {
                docs = new List<DocumentOverdue>();
            }
            var vbdens = new List<VanBanDenOverdue>();
            var index = 0;
            foreach (var doc in docs)
            {
                var vb = new VanBanDenOverdue();
                var userDept = allUserDepts.Where(u => u.UserId == doc.UserCurrentId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
                if (userDept == null)
                {
                    continue;
                }

                var dept = allDepts.SingleOrDefault(d => d.DepartmentId == userDept.DepartmentId);
                if (dept == null)
                {
                    continue;
                }

                vb.Index = index;
                vb.DocumentCopyId = doc.DocumentCopyId;
                vb.Compendium = doc.Compendium;
                vb.DateOverdue = doc.DateAppointed;
                vb.DocCode = doc.DocCode;
                vb.FromDate = doc.DateCreated;
                vb.ToDate = doc.DateFinished;
                vb.DepartmentName = dept.DepartmentPath;
                vb.UserFullName = doc.UserCurrentName;
                vb.StatusDXLName = doc.StatusDXLName;
                vb.StatusName = doc.StatusName;
                vbdens.Add(vb);
                index++;
            }
            var result = new List<VanBanDenOverdue>();
            bool isParent;

            if (IsGrouping(groupBy))
            {
                IEnumerable<IGrouping<string, VanBanDenOverdue>> groups;
                switch (groupBy)
                {
                    case "Department":
                        groups = vbdens.GroupBy(d => d.DepartmentName).OrderBy(g => g.Key);
                        break;
                    default:
                        groups = vbdens.GroupBy(d => d.UserFullName).OrderBy(g => g.Key);
                        break;
                }

                foreach (var group in groups)
                {
                    result.Add(new VanBanDenOverdue()
                    {
                        GroupName = ParseDepartmentName(group.Key, out isParent),
                        GroupCount = group.Count(),
                        IsGroup = true
                    });

                    foreach (var item in group)
                    {
                        item.GroupName = ParseDepartmentName(group.Key, out isParent);
                    }

                    result.AddRange(group.OrderBy(t => t.FromDate).Select((t, idx) => new VanBanDenOverdue
                    {
                        Index = idx,
                        DocumentCopyId = t.DocumentCopyId,
                        DocCode = t.DocCode,
                        Compendium = t.Compendium,
                        UserFullName = t.UserFullName,
                        FromDate = t.FromDate,
                        ToDate = t.ToDate,
                        DateOverdue = t.DateOverdue,
                        StatusName = t.StatusName,
                        StatusDXLName = t.StatusDXLName,
                        //DepartmentName =  ParseDepartmentName(t.DepartmentName, out isParent),
                        GroupName = t.GroupName
                    }));
                }

                return result;
            }

            return docs.OrderBy(t => t.FromDate).Select(t => new VanBanDenOverdue
            {
                DocCode = t.DocCode,
                Compendium = t.Compendium,
                UserFullName = t.UserCurrentName,
                FromDate = t.FromDate.ToString("g"),
                ToDate = t.ToDate.HasValue ? t.ToDate.Value.ToString("g") : "",
                DateOverdue = t.DateOverdue.Value.ToString("g"),
                        StatusDXLName = t.StatusDXLName,
                StatusName = t.StatusName
                //DepartmentName = ParseDepartmentName(t.DepartmentName, out isParent),
                //GroupName = t.GroupName
            });
        }

        /// <summary>
        /// Thống kê văn bản đến quá hạn theo quy trình
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="hasOldDocument"></param>
        /// <param name="isProcess"></param>
        /// <param name="groupBy"></param>
        /// <param name="categoryBusinessId"></param>
        /// <returns></returns>
        public dynamic VanBanDenByUser(DateTime from, DateTime to, int userId, string groupBy = "FullName")
        {
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();

            var allDepts = _departmentService.GetCacheAllDepartments();
            var docs = GetDocumentUser(false, from, to, userId);
           
            if (docs == null)
            {
                docs = new List<DocumentOverdue>();
            }
            var vbdens = new List<VanBanDenOverdue>();
            var index = 0;
            foreach (var doc in docs)
            {
                var vb = new VanBanDenOverdue();
                var userDept = allUserDepts.Where(u => u.UserId == doc.UserCurrentId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
                if (userDept == null)
                {
                    continue;
                }

                var dept = allDepts.SingleOrDefault(d => d.DepartmentId == userDept.DepartmentId);
                if (dept == null)
                {
                    continue;
                }
                vb.DocumentCopyId = doc.DocumentCopyId;

                vb.Index = index;
                vb.Compendium = doc.Compendium;
                vb.DateOverdue = doc.DateAppointed;
                vb.DocCode = doc.DocCode;
                vb.FromDate = doc.DateCreated;
                vb.ToDate = doc.DateFinished;
                vb.DepartmentName = dept.DepartmentPath;
                vb.UserFullName = doc.UserCurrentName;
                vb.StatusName = doc.StatusName;
                vb.StatusDXLName = doc.StatusDXLName;
                index++;
                vbdens.Add(vb);
            }
            var result = new List<VanBanDenOverdue>();
            bool isParent;

            if (IsGrouping(groupBy))
            {
                IEnumerable<IGrouping<string, VanBanDenOverdue>> groups;
                switch (groupBy)
                {
                    case "Department":
                        groups = vbdens.GroupBy(d => d.DepartmentName).OrderBy(g => g.Key);
                        break;
                    default:
                        groups = vbdens.GroupBy(d => d.UserFullName).OrderBy(g => g.Key);
                        break;
                }

                foreach (var group in groups)
                {
                    result.Add(new VanBanDenOverdue()
                    {
                        GroupName = ParseDepartmentName(group.Key, out isParent),
                        GroupCount = group.Count(),
                        IsGroup = true
                    });

                    foreach (var item in group)
                    {
                        item.GroupName = ParseDepartmentName(group.Key, out isParent);
                    }

                    result.AddRange(group.OrderBy(t => t.FromDate).Select((t,idx) => new VanBanDenOverdue
                    {
                        Index = idx,
                        DocumentCopyId = t.DocumentCopyId,
                        DocCode = t.DocCode,
                        Compendium = t.Compendium,
                        UserFullName = t.UserFullName,
                        FromDate = t.FromDate,
                        ToDate = t.ToDate,
                        DateOverdue = t.DateOverdue,
                        StatusName = t.StatusName,
                        StatusDXLName = t.StatusDXLName,
                        //DepartmentName =  ParseDepartmentName(t.DepartmentName, out isParent),
                        GroupName = t.GroupName
                    }));
                }

                return result;
            }

            return docs.OrderBy(t => t.FromDate).Select(t => new VanBanDenOverdue
            {
                DocCode = t.DocCode,
                Compendium = t.Compendium,
                UserFullName = t.UserCurrentName,
                FromDate = t.FromDate.ToString("g"),
                ToDate = t.ToDate.HasValue ? t.ToDate.Value.ToString("g") : "",
                DateOverdue = t.DateOverdue.Value.ToString("g"),
                        StatusName = t.StatusName,
                        StatusDXLName = t.StatusDXLName,
                //DepartmentName = ParseDepartmentName(t.DepartmentName, out isParent),
                //GroupName = t.GroupName
            });
        }

        public dynamic VanBanDiByUser(DateTime from, DateTime to, int userId, string groupBy = "FullName")
        {
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();

            var allDepts = _departmentService.GetCacheAllDepartments();
            var docs = GetDocumentUser(false, from, to, userId);

            if (docs == null)
            {
                docs = new List<DocumentOverdue>();
            }
            var vbdens = new List<VanBanDenOverdue>();
            foreach (var doc in docs)
            {
                var vb = new VanBanDenOverdue();
                var userDept = allUserDepts.Where(u => u.UserId == doc.UserCurrentId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
                if (userDept == null)
                {
                    continue;
                }

                var dept = allDepts.SingleOrDefault(d => d.DepartmentId == userDept.DepartmentId);
                if (dept == null)
                {
                    continue;
                }

                vb.DocumentCopyId = doc.DocumentCopyId;
                vb.Compendium = doc.Compendium;
                vb.DateOverdue = doc.DateAppointed;
                vb.DocCode = doc.DocCode;
                vb.FromDate = doc.DateCreated;
                vb.ToDate = doc.DateFinished;
                vb.DepartmentName = dept.DepartmentPath;
                vb.UserFullName = doc.UserCurrentName;
                vb.StatusName = doc.StatusName;
                vbdens.Add(vb);
            }
            var result = new List<VanBanDenOverdue>();
            bool isParent;

            if (IsGrouping(groupBy))
            {
                IEnumerable<IGrouping<string, VanBanDenOverdue>> groups;
                switch (groupBy)
                {
                    case "Department":
                        groups = vbdens.GroupBy(d => d.DepartmentName).OrderBy(g => g.Key);
                        break;
                    default:
                        groups = vbdens.GroupBy(d => d.UserFullName).OrderBy(g => g.Key);
                        break;
                }

                foreach (var group in groups)
                {
                    result.Add(new VanBanDenOverdue()
                    {
                        GroupName = ParseDepartmentName(group.Key, out isParent),
                        GroupCount = group.Count(),
                        IsGroup = true
                    });

                    foreach (var item in group)
                    {
                        item.GroupName = ParseDepartmentName(group.Key, out isParent);
                    }

                    result.AddRange(group.OrderBy(t => t.FromDate).Select(t => new VanBanDenOverdue
                    {
                        DocumentCopyId = t.DocumentCopyId,
                        DocCode = t.DocCode,
                        Compendium = t.Compendium,
                        UserFullName = t.UserFullName,
                        FromDate = t.FromDate,
                        ToDate = t.ToDate,
                        DateOverdue = t.DateOverdue,
                        StatusName = t.StatusName,
                        //DepartmentName =  ParseDepartmentName(t.DepartmentName, out isParent),
                        GroupName = t.GroupName
                    }));
                }

                return result;
            }

            return docs.OrderBy(t => t.FromDate).Select(t => new VanBanDenOverdue
            {
                DocCode = t.DocCode,
                Compendium = t.Compendium,
                UserFullName = t.UserCurrentName,
                FromDate = t.FromDate.ToString("g"),
                ToDate = t.ToDate.HasValue ? t.ToDate.Value.ToString("g") : "",
                DateOverdue = t.DateOverdue.Value.ToString("g"),
                //DepartmentName = ParseDepartmentName(t.DepartmentName, out isParent),
                //GroupName = t.GroupName
            });
        }


        private IEnumerable<StatisticTimeline> GetDocumentTimelines(DateTime from, DateTime to, bool hasOldDocument)
        {
            var timelines =
                ExecuteDatabase<StatisticTimeline>("Giamsat_timeline", from, to, hasOldDocument);

            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();

            var allDepts = _departmentService.GetCacheAllDepartments();
            var allUser = _userService.GetAllCached();

            // Chỉ tính user current của người cuối cùng đang giữ
            var docCopyGroup = timelines.GroupBy(d => d.DocumentCopyId);
            foreach (var group in docCopyGroup)
            {
                if (group.Count() == 1)
                {
                    continue;
                }

                for (var i = 0; i < group.Count() - 2; i++)
                {
                    group.ElementAt(i).UserCurrentId = 0;
                }
            }

            foreach (var timeline in timelines)
            {
                var user = allUser.SingleOrDefault(u => u.UserId == timeline.UserId);
                if (user != null)
                {
                    timeline.UserName = user.Username;
                    timeline.UserFullName = user.FullName;
                }

                var userDept = allUserDepts.Where(u => u.UserId == timeline.UserId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
                if (userDept == null)
                {
                    continue;
                }

                var dept = allDepts.SingleOrDefault(d => d.DepartmentId == userDept.DepartmentId);
                if (dept == null)
                {
                    continue;
                }

                timeline.DepartmentExt = dept.DepartmentIdExt;
                timeline.DepartmentName = dept.DepartmentPath;

                timeline.CurrentDepartmentExt = "";

                if (timeline.UserCurrentId > 0)
                {
                    var currentDeparment = allUserDepts.Where(u => u.UserId == timeline.UserCurrentId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
                    if (currentDeparment != null)
                    {
                        timeline.CurrentDepartmentExt = currentDeparment.DepartmentIdExt;
                    }
                }

                if (!timeline.DateOverdue.HasValue)
                {
                    timeline.DateOverdue = timeline.FromDate.AddHours(timeline.TimeInNode);
                }

                if (!timeline.ToDate.HasValue && timeline.Status == (int)DocumentStatus.KetThuc)
                {
                    timeline.ToDate = timeline.DateFinished;
                }

                timeline.IsOverdue = IsOverdue(timeline.ToDate, timeline.DateOverdue.Value);
            }

            return timelines;
        }

        private bool IsOverdue(DateTime? dateProcess, DateTime dateOverdue)
        {
            // Tính xử lsy theo buổi

            // Chưa xử lý: quá hạn khi ngày hiện tại lớn hơn hạn xử lý
            // Todo: cũng cần tính theo buổi chỗ này
            if (!dateProcess.HasValue)
            {
                return dateOverdue.EndOf(DateTimeUnit.Day) < DateTime.Now.EndOf(DateTimeUnit.Day);
            }

            var date = dateProcess.Value;

            // Ngày xử lý nhỏ hơn hạn xử lý
            if (date < dateOverdue)
            {
                // Còn hạn
                return false;
            }

            // Ngày xử lý lớn hơn hạn xử lý
            if (date.EndOf(DateTimeUnit.Day) > dateOverdue.EndOf(DateTimeUnit.Day))
            {
                // quá hạn
                return true;
            }

            // Cùng ngày, tính theo buổi
            // Hạn là buổi chiều thì là đúng hạn do tính cả buổi
            if (dateOverdue.Hour > 12)
            {
                return false;
            }

            // Hạn là buổi sáng: quá hạn khi thời gian xử lý thuộc buổi chiều
            return date.Hour > 12;
        }

        private List<ProgressStatisticXlvb> VBden_GroupingByDepartment(IEnumerable<StatisticTimeline> timelines, DateTime from, DateTime to)
        {
            var result = new List<ProgressStatisticXlvb>();
            var allDepartment = _departmentService.GetCacheAllDepartments().OrderBy(d => d.DepartmentIdExt);

            foreach (var department in allDepartment)
            {
                var groupTimelines = timelines.Where(t => t.DepartmentExt.StartsWith(department.DepartmentIdExt));

                var progress = new ProgressStatisticXlvb();
                progress = ParseStatisticXLVB(groupTimelines, from, to, "CurrentDepartmentExt", department.DepartmentIdExt, isStartWithCompare: true);

                bool isParent;
                progress.Name = ParseDepartmentName(department.DepartmentPath, out isParent);
                progress.IsParent = isParent;

                result.Add(progress);
            }

            return result;
        }

        private List<ProgressStatisticXlvb> VBden_GroupingByDepartmentNormal(IEnumerable<StatisticObject> statistics, DateTime from, DateTime to)
        {
            var result = new List<ProgressStatisticXlvb>();
            var allDepartment = _departmentService.GetCacheAllDepartments().OrderBy(d => d.DepartmentIdExt);

            foreach (var department in allDepartment)
            {
                var groupTimelines = statistics.Where(t => t.CurrentDepartmentExt.StartsWith(department.DepartmentIdExt));

                var progress = new ProgressStatisticXlvb();
                progress = ParseStatisticXLVBNormal(groupTimelines, from, to, "CurrentDepartmentExt", department.DepartmentIdExt, true);

                bool isParent;
                progress.Name = ParseDepartmentName(department.DepartmentPath, out isParent);
                progress.IsParent = isParent;
                progress.TypeStatistics = 1;
                progress.ParrentName = department.DepartmentIdExt;
                result.Add(progress);
            }

            return result;
        }

        private List<ProgressStatisticXlvb> VBden_GroupingByUser(IEnumerable<StatisticTimeline> timelines, DateTime from, DateTime to)
        {
            var result = new List<ProgressStatisticXlvb>();

            var departmentLevel2 = _departmentService.GetCacheAllDepartments().OrderBy(d => d.DepartmentIdExt); //.Where(d => d.DepartmentIdExt.Split('.').Length == 2)
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
            var allUser = _userService.GetAllCached();

            foreach (var department in departmentLevel2)
            {
                var groupTimelines = timelines.Where(t => t.DepartmentExt.StartsWith(department.DepartmentIdExt));

                var progress = ParseStatisticXLVB(groupTimelines, from, to, "CurrentDepartmentExt", department.DepartmentIdExt, true);

                bool isParent;
                progress.Name = ParseDepartmentName(department.DepartmentPath, out isParent);
                progress.IsParent = true;

                result.Add(progress);

                var deparmentUsers = allUserDepts.Where(d => d.DepartmentIdExt.Equals(department.DepartmentIdExt)).Select(u => u.UserId);
                var users = allUser.Where(u => deparmentUsers.Contains(u.UserId));
                foreach (var user in users)
                {
                    // Nếu người dùng thuộc nhiều phòng ban thì chỉ lấy phòng ban chính
                    var userDept = allUserDepts.Where(ud => ud.UserId == user.UserId).OrderByDescending(ud => ud.IsPrimary).FirstOrDefault();
                    if (userDept.DepartmentId != department.DepartmentId)
                    {
                        //result.Add(new ProgressStatisticXlvb()
                        //{
                        //    Name = string.Format(@"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", user.FullName)
                        //});
                        continue;
                    }

                    var userTimelines = groupTimelines.Where(t => t.UserId == user.UserId);

                    var userProgress = ParseStatisticXLVB(userTimelines, from, to, "UserCurrentId", user.UserId.ToString());
                    userProgress.Name = string.Format(@"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", user.FullName);

                    result.Add(userProgress);
                }
            }

            return result;
        }

        private List<ProgressStatisticXlvb> VBden_GroupingByUserNormal(IEnumerable<StatisticObject> statistics, DateTime from, DateTime to, bool isShowAll = true)
        {
            var result = new List<ProgressStatisticXlvb>();

            var departmentLevel2 = _departmentService.GetCacheAllDepartments().Where(d=>d.IsActivated).OrderBy(d => d.DepartmentIdExt).ThenBy(d=>d.DepartmentName); //.Where(d => d.DepartmentIdExt.Split('.').Length == 2)
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
            var allUser = _userService.GetAllCached();
            foreach (var department in departmentLevel2)
            {
                var groupStatistics = statistics.Where(t => t.CurrentDepartmentExt.StartsWith(department.DepartmentIdExt));

                var progress = ParseStatisticXLVBNormal(groupStatistics, from, to, "CurrentDepartmentExt", department.DepartmentIdExt, true);

                bool isParent;
                progress.Name = ParseDepartmentName(department.DepartmentPath, out isParent);
                progress.IsParent = true;
                progress.IsShowAll = true;
                progress.TypeStatistics = 1;
                progress.ParrentName = department.DepartmentIdExt;
                result.Add(progress);

                var deparmentUsers = allUserDepts.Where(d => d.DepartmentIdExt.Equals(department.DepartmentIdExt)).Select(u => u.UserId);
                var users = allUser.Where(u => deparmentUsers.Contains(u.UserId));
                foreach (var user in users)
                {
                    // Nếu người dùng thuộc nhiều phòng ban thì chỉ lấy phòng ban chính
                    var userDept = allUserDepts.Where(ud => ud.UserId == user.UserId).OrderByDescending(ud => ud.IsPrimary).FirstOrDefault();
                    if (userDept.DepartmentId != department.DepartmentId)
                    {
                        continue;
                    }

                    var userTimelines = groupStatistics.Where(t => t.UserCurrentId == user.UserId);

                    var userProgress = ParseStatisticXLVBNormal(userTimelines, from, to, "UserCurrentId", user.UserId.ToString());
                    userProgress.Name = string.Format(@"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", user.FullName);
                    userProgress.TypeStatistics = 2;
                    userProgress.IsShowAll = isShowAll;
                    userProgress.UserId = user.UserId;
                    userProgress.ParrentName = department.DepartmentIdExt;
                    result.Add(userProgress);
                }
            }

            return result;
        }

        private string ParseDepartmentName(string departmentPath, out bool isParent)
        {
            var result = "";
            isParent = false;

            if (string.IsNullOrEmpty(departmentPath))
            {
                return result;
            }

            if (departmentPath.StartsWith(@"\"))
            {
                departmentPath = departmentPath.Remove(0, 1);
            }

            var departmentList = departmentPath.Split('\\');

            if (departmentList.Length == 1 || departmentList.Length == 2)
            {
                isParent = true;
                return departmentList.Last();
            }

            result = string.Format(@"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", departmentList.Last());

            return result;
        }

        /// <summary>
        /// Xử lý danh sách group theo phòng ban
        /// </summary>
        /// <param name="timelines"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="processedName"></param>
        /// <param name="processedValue"></param>
        /// <param name="isStartWithCompare"></param>
        /// <returns></returns>
        private ProgressStatisticXlvb ParseStatisticXLVB(IEnumerable<StatisticTimeline> timelines, DateTime from, DateTime to,
                    string processedName, string processedValue, bool isStartWithCompare = false)
        {
            var result = new ProgressStatisticXlvb();
            if (!timelines.Any())
            {
                return result;
            }

            // Tổng xử lý
            result.Total = timelines.Count();

            // Tồn kỳ trước
            result.PreExtisting = timelines.Count(d => d.FromDate < from);
            result.PreExtisting_Overdue = timelines.Count(d => d.FromDate < from && d.IsOverdue);

            // Nhận trong kỳ
            var docNewReceptions = timelines.Where(d => d.FromDate >= from && d.FromDate <= to);
            docNewReceptions = docNewReceptions != null ? docNewReceptions : new List<StatisticTimeline>();
            result.NewReception = docNewReceptions.Count();

            // Tổng đã xử lý
            var solveds = docNewReceptions.Where(d => d.IsProcess);
            result.TotalSolved = solveds.Count();

            // Đã giải quyết đúng hạn
            var solved_dh = solveds.Where(d => !d.IsOverdue);
            result.SolvedInTime = solved_dh.Count();

            if (isStartWithCompare)
            {
                result.SolvedInTime_XuLy = solved_dh.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString().StartsWith(processedValue)));
            }
            else
            {
                result.SolvedInTime_XuLy = solved_dh.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString() == processedValue));
            }

            result.SolvedInTime_DeBiet = result.SolvedInTime - result.SolvedInTime_XuLy;

            // Đã giải quyết trễ hẹn
            var solvedLate = solveds.Where(d => d.IsOverdue);
            result.SolvedLate = solvedLate.Count();

            if (isStartWithCompare)
            {
                result.SolvedLate_XuLy = solvedLate.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString().StartsWith(processedValue)));
            }
            else
            {
                result.SolvedLate_XuLy = solvedLate.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString() == processedValue));
            }

            result.SolvedLate_DeBiet = result.SolvedLate - result.SolvedLate_XuLy;

            // Tồn cuối kỳ
            result.TotalPending = timelines.Count(d => !d.IsProcess);

            // Tồn cuối kỳ - Quá hạn
            result.PendingLate = timelines.Count(d => !d.IsProcess && d.IsOverdue);

            return result;
        }

        /// <summary>
        /// Xử lý danh sách group theo phòng ban
        /// </summary>
        /// <param name="docs"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="processedName"></param>
        /// <param name="processedValue"></param>
        /// <param name="isStartWithCompare"></param>
        /// <returns></returns>
        private ProgressStatisticXlvb ParseStatisticXLVBNormal(IEnumerable<StatisticObject> docs, DateTime from, DateTime to,
                    string processedName, string processedValue, bool isStartWithCompare = false)
        {
            var result = new ProgressStatisticXlvb();
            if (!docs.Any())
            {
                return result;
            }

            // Tổng xử lý
            result.Total = docs.Count();

            // Nhận trong kỳ
            result.NewReception = docs.Count(d => d.DateCreated >= from && d.DateCreated <= to);

            // Tồn kỳ trước
            result.PreExtisting = result.Total - result.NewReception;
            var solved_dh = docs.Where(d => d.OverdueStatusType == OverdueStatusType.ResolveInTime);
            solved_dh = solved_dh != null ? solved_dh : new List<StatisticObject>();

            // Đã giải quyết đúng hạn
            result.SolvedInTime = solved_dh.Count();
            if (isStartWithCompare)
            {
                result.SolvedInTime_XuLy = solved_dh.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString().StartsWith(processedValue)));
            }
            else
            {
                result.SolvedInTime_XuLy = solved_dh.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString() == processedValue));
            }
            result.SolvedInTime_DeBiet = result.SolvedInTime - result.SolvedInTime_XuLy;

            // Đã giải quyết trễ hẹn
            var solvedLate = docs.Where(d => d.OverdueStatusType == OverdueStatusType.ResolveLate);
            solvedLate = solvedLate != null ? solvedLate : new List<StatisticObject>();
            result.SolvedLate = solvedLate.Count();

            if (isStartWithCompare)
            {
                result.SolvedLate_XuLy = solvedLate.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString().StartsWith(processedValue)));
            }
            else
            {
                result.SolvedLate_XuLy = solvedLate.Count(d => d.Status == (int)DocumentStatus.KetThuc
                                                    && (d.GetType().GetProperty(processedName).GetValue(d, null).ToString() == processedValue));
            }

            // Tổng đã xử lý
            result.TotalSolved = result.SolvedInTime + result.SolvedLate;

            // Tổng chưa Xử lý
            result.TotalPending = result.Total - result.TotalSolved;

            var pendingLateDocs = docs.Where(d => d.OverdueStatusType == OverdueStatusType.Overdue);
            pendingLateDocs = pendingLateDocs != null ? pendingLateDocs : new List<StatisticObject>();
            // Chưa giải quyết quá hạn
            result.PendingLate = pendingLateDocs.Count();

            // Chưa giải quyết trong hạn
            result.Pending = result.TotalPending - result.PendingLate;

            var pendingDocs = docs.Where(d => d.OverdueStatusType == OverdueStatusType.Pending);
            pendingDocs = pendingDocs != null ? pendingDocs : new List<StatisticObject>();
            // Chưa giải quyết quá hạn
            var pending = pendingDocs.Count();

            return result;
        }

        #endregion

        #region Văn bản phát hành

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<StoreDocumentIn> GiamSatTong_PhatHanh(StatisticsCriteriaObject model, out int total)
        {
            var docs = ExecuteDatabase<StoreDocumentIn>("Giamsat_phathanh", model.From, model.To, false, model.StoreId);
            if (IsGrouping(model.GroupBy) && model.GroupBy.Equals("ProcessInfo"))
            {
                docs = ExecuteDatabase<StoreDocumentIn>("Giamsat_phathanh_nhom", model.From, model.To, false, model.StoreId);
            }

            total = 0;
            if (model.IsFirstLoad && IsGrouping(model.GroupBy))
            {
                total = docs.Count();
                docs = ParseGroup(docs, model.GroupBy, model.IsGetAll);
            }
            else
            {
                docs = ParseSoVanBanGroupAPI(docs, model, out total);
            }

            return docs;
        }

        public IEnumerable<StoreDocumentIn> GiamSatTong_PhatHanh_User(StatisticsCriteriaObject model, out int total)
        {
            total = 0;
            var docs = ExecuteDatabase<StoreDocumentIn>("Giamsat_phathanh", model.From, model.To, false, model.StoreId);
            if (docs != null && docs.Any())
            {
                docs = docs.Where(d => d.UserCreatedId == model.UserId);
            }
            docs = docs.Select((d, index) =>
            {
                d.Index = index;
                return d;
            });
            var user = _userService.GetCacheAllUsers().FirstOrDefault(u => u.UserId == model.UserId);
            if (user == null)
            {
                return new List<StoreDocumentIn>();
            }
            var result = new List<StoreDocumentIn>();
            total = docs.Count();
            result.Add(new StoreDocumentIn()
            {
                GroupName = user.FullName,
                GroupCount = total,
                IsGroup = true
            });
            result.AddRange(docs);

            return result;
        }

        public IEnumerable<ProgressStatisticXlvb> GiamSatTong_Vbdi(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_tong_hoibao", model.From, model.To, model.StoreId);
            total = 0;
            var docs = _cache.Get<IEnumerable<DocsResponse>>(cacheKey, () =>
            {
                return ExecuteDatabase<DocsResponse>("Giamsat_tong_hoibao", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);

            if (docs == null || !docs.Any())
            {
                return new List<ProgressStatisticXlvb>();
            }
            docs = ParseDepartment(docs.ToList());
            docs = ParseGroupsHoiBao(docs);

            var gs = ParseXLVBHoiBaoDP(docs);
            return gs;
        }

        public IEnumerable<ProgressStatisticXlvb> VBdi_HoiBao(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_tong_di_hoibao", model.From, model.To, model.StoreId);
            total = 0;
            var docs = _cache.Get<IEnumerable<DocsResponse>>(cacheKey, () =>
            {
                return ExecuteDatabase<DocsResponse>("Giamsat_tong_di_hoibao", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);

            if (docs == null || !docs.Any())
            {
                return new List<ProgressStatisticXlvb>();
            }
            docs = ParseDepartment(docs.ToList());

            var gs = ParseXLVBHoiBaoDP(docs);
            return gs;
        }

        public IEnumerable<DocsResponse> Vbdi_HB_Department(StatisticsCriteriaObject model, out int total, string type = "department")
        {
            var cacheKey = BuildCacheKey("Giamsat_tong_hoibao", model.From, model.To, model.StoreId);
            total = 0;
            var docs = _cache.Get<IEnumerable<DocsResponse>>(cacheKey, () =>
            {
                return ExecuteDatabase<DocsResponse>("Giamsat_tong_hoibao", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);

            if (docs == null || !docs.Any())
            {
                docs =new  List<DocsResponse>();
            }

            docs = docs.Where(d=>d.HasRequireResponse == true);
            docs = ParseDepartment(docs.ToList());

            var gs = ParseXLVBHBByDepartment(docs, model.GroupName, type);
            total = gs.Count();
            return gs;
        }

        public IEnumerable<DocsResponse> Vbdi_HB_Status(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_tong_hoibao", model.From, model.To, model.StoreId);
            total = 0;
            var docs = _cache.Get<IEnumerable<DocsResponse>>(cacheKey, () =>
            {
                return ExecuteDatabase<DocsResponse>("Giamsat_tong_hoibao", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);

            if (docs == null || !docs.Any())
            {
                docs = new List<DocsResponse>();
            }

            docs = docs.Where(d => d.HasRequireResponse == true);
            docs = ParseDepartment(docs.ToList());

            var gs = ParseXLVBHBByStatus(docs, model.StoreId ,model.GroupName);
            return gs;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<ProgressStatisticXlvb> GiamSatTong_HoiBao(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_tong_hoibao", model.From, model.To, model.StoreId);
            total = 0;
            var docs = _cache.Get<IEnumerable<DocsResponse>>(cacheKey, () =>
            {
                return ExecuteDatabase<DocsResponse>("Giamsat_tong_hoibao", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);

            var addresses = _addressService.Gets();
            var addressHB = addresses.Where(ad => !string.IsNullOrEmpty(ad.EdocId));
            if (addressHB == null)
            {
                return new List<ProgressStatisticXlvb>();
            }
            var gs = ParseXLVBHoiBao(docs, addresses, model.From, model.To);
            total = gs.Count();
            return gs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<DocsResponse> GiamSatTong_PhatHanh_HoiBao(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_tong_hoibao", model.From, model.To, model.StoreId);
            total = 0;
            var docs = _cache.Get<IEnumerable<DocsResponse>>(cacheKey, () =>
            {
                return ExecuteDatabase<DocsResponse>("Giamsat_tong_hoibao", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);

            docs = docs.Where(d => d.HasRequireResponse == true);
            if (docs == null  || !docs.Any())
            {
                return new List<DocsResponse>();
            }
            docs = docs.Where(d => d.RequireResponseStatus == model.StoreId);

             var result = ParseXLVBHBByAddressName(docs, model.GroupName);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<StoreDocumentIn> GiamSatTong_Den_TW(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_tw_den", model.From, model.To, model.StoreId);
            var docs = _cache.Get<IEnumerable<StoreDocumentIn>>(cacheKey, () =>
            {
                return ExecuteDatabase<StoreDocumentIn>("Giamsat_tw_den", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);
            total = 0;
            if (model.IsFirstLoad && IsGrouping(model.GroupBy))
            {
                total = docs.Count();
                docs = ParseGroup(docs, model.GroupBy, model.IsGetAll);
            }
            else
            {
                docs = ParseSoVanBanGroupAPI(docs, model, out total);
            }

            return docs;
        }

        private bool IsGrouping(string groupBy)
        {
            if (string.IsNullOrEmpty(groupBy))
            {
                return false;
            }

            return !groupBy.Equals("NonGroup", StringComparison.OrdinalIgnoreCase);
        }

        private List<StatisticPublish> Vbdi_Grouping(IEnumerable<StatisticPublish> docPublishes, string groupBy)
        {
            var result = new List<StatisticPublish>();
            IEnumerable<IGrouping<string, StatisticPublish>> groups;

            switch (groupBy)
            {
                case "UserSuccess":
                    groups = docPublishes.GroupBy(d => d.UserSuccess).OrderBy(g => g.Key);
                    break;
                case "UserCreated":
                    groups = docPublishes.GroupBy(d => d.UserCreatedName).OrderBy(g => g.Key);
                    break;
                case "Category":
                    groups = docPublishes.GroupBy(d => d.CategoryName).OrderBy(g => g.Key);
                    break;
                case "ProcessInfo":
                    groups = docPublishes.GroupBy(d => d.ProcessInfo).OrderBy(g => g.Key);
                    break;
                default:
                    groups = docPublishes.GroupBy(d => d.CategoryName).OrderBy(g => g.Key);
                    break;
            }

            foreach (var group in groups)
            {
                result.Add(new StatisticPublish()
                {
                    GroupName = group.Key,
                    GroupCount = group.Count(),
                    IsGroup = true
                });

                foreach (var item in group)
                {
                    item.GroupName = group.Key;
                }

                result.AddRange(group.OrderBy(d => d.DatePublished));
            }

            return result;
        }

        private List<StatisticPublish> Vbdi_GroupingByCategory(IEnumerable<StatisticPublish> docPublishes)
        {
            throw new NotImplementedException();
        }

        private List<ProgressStatisticXlvb> ParseXLVBHoiBao(IEnumerable<DocsResponse> docs, IEnumerable<Bkav.eGovCloud.Entities.Customer.Address> addresses,DateTime from, DateTime to, bool isResponse = true)
        {
            var result = new List<ProgressStatisticXlvb>();
            if (!docs.Any())
            {
                return result;
            }

            docs = docs.Where(d => d.HasRequireResponse == true);
            if (!docs.Any())
            {
                return result;
            }

            foreach (var address in addresses)
            {
                var th = new ProgressStatisticXlvb();
                th.Name = address.Name;
                // Tổng số văn bản hồi báo
                var totalDocs = docs.Where(d => d.AddressName == address.Name);
              
                th.NewReception = totalDocs == null? 0: totalDocs.Count();
                //if (th.NewReception == 0)
                //{
                //    continue;
                //}

                // Tổng số văn bản đã hồi báo
                var totalResponsed = docs.Where(d => d.AddressName == address.Name && d.IsResponsed);

                th.TotalSolved = totalResponsed == null ? 0 : totalResponsed.Count();

                // Tổng số văn bản hồi báo trễ hạn
                var responedOverTime = docs.Where(d => d.AddressName == address.Name &&
                    d.IsResponsed && 
                    d.DateAppointed.HasValue && d.DateResponsed.HasValue &&
                    d.DateResponsed > d.DateAppointed );

                th.SolvedLate = responedOverTime == null ? 0 : responedOverTime.Count();
                th.SolvedInTime = th.TotalSolved - th.SolvedLate;

                // Tổng số văn bản chưa hồi báo
                var totalUnRespone = docs.Where(d => d.AddressName == address.Name && !d.IsResponsed);

                th.TotalPending = totalUnRespone == null ? 0 : totalUnRespone.Count();

                // Tổng số văn bản chưa hồi báo quá hạn
                var UnResponeOverTime = docs.Where(d => d.AddressName == address.Name && !d.IsResponsed &&
                    d.DateAppointed.HasValue && d.DateAppointed > to
                );

                th.PendingLate = UnResponeOverTime == null ? 0 : UnResponeOverTime.Count();
                th.Pending = th.TotalPending - th.PendingLate;

                result.Add(th);
            }

            return result;
        }

        private List<ProgressStatisticXlvb> ParseXLVBHoiBaoDP(IEnumerable<DocsResponse> docs, bool isShowAll = false)
        {
            var result = new List<ProgressStatisticXlvb>();
            if (!docs.Any())
            {
                return result;
            }

            var allDepartment = _departmentService.GetCacheAllDepartments().Where(d=>d.IsActivated).OrderBy(d => d.DepartmentIdExt).ThenBy(d=>d.DepartmentName);
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
            var allUser = _userService.GetAllCached();

            foreach (var department in allDepartment)
            {
                var groupTimelines = docs.Where(t => t.CurrentDepartmentExt.StartsWith(department.DepartmentIdExt));

                var progress = new ProgressStatisticXlvb();
                progress = ParseXLVBHoiBaoDepartment(groupTimelines, department.DepartmentIdExt, department.DepartmentName);

                bool isParent;
                progress.Name = ParseDepartmentName(department.DepartmentPath, out isParent);
                progress.IsParent = true;
                progress.TypeStatistics = 1;
                progress.ParrentName = department.DepartmentIdExt;
                progress.IsShowAll = true;

                result.Add(progress);

                var deparmentUsers = allUserDepts.Where(d => d.DepartmentIdExt.Equals(department.DepartmentIdExt)).Select(u => u.UserId);
                var users = allUser.Where(u => deparmentUsers.Contains(u.UserId));
                foreach (var user in users)
                {
                    // Nếu người dùng thuộc nhiều phòng ban thì chỉ lấy phòng ban chính
                    var userDept = allUserDepts.Where(ud => ud.UserId == user.UserId).OrderByDescending(ud => ud.IsPrimary).FirstOrDefault();
                    if (userDept.DepartmentId != department.DepartmentId)
                    {
                        continue;
                    }

                    var userTimelines = groupTimelines.Where(t => t.UserCurrentId == user.UserId);

                    var userProgress = ParseXLVBHoiBaoUser(userTimelines, user.UserId);
                    userProgress.Name = string.Format(@"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", user.FullName);
                    userProgress.TypeStatistics = 2;
                    userProgress.IsShowAll = isShowAll;
                    userProgress.TypeStatistics = 2;
                    userProgress.UserId = user.UserId;
                    userProgress.ParrentName = department.DepartmentIdExt;
                    result.Add(userProgress);
                }

            }

            return result;
            
        }

        private List<DocsResponse> ParseXLVBHBByDepartment(IEnumerable<DocsResponse> docs, string departmentExt, string type)
        {
            var groupName = departmentExt;
            var count = 0;
            var docDeparts = docs.Where(dg => dg.AddressName == departmentExt);
            count = docDeparts.Count();

            if (type == "department")
            {
                docs = ParseGroupsHoiBao(docs);
                docDeparts = docs.Where(dg => dg.CurrentDepartmentExt == departmentExt);
                if (docDeparts != null && docDeparts.Any())
                {
                    groupName = docDeparts.FirstOrDefault().CurrentDepartmentName;
                    count = docDeparts.Count();
                }
            }

            docDeparts = docDeparts.Select((d, index) =>
            {
                d.GroupName = groupName;
                d.Index = index;
                d.ListResponse.Select((lr, idx) =>
                {
                    lr.Index = index;
                    lr.GroupName = groupName;
                    return lr;
                });
                return d;
            });
            var result = new List<DocsResponse>();
            result.Add(new DocsResponse()
            {
                GroupName = groupName,
                GroupCount = count,
                IsGroup = true
            });
            result.AddRange(docDeparts);

            return result;
        }

        private List<DocsResponse> ParseGroupsHoiBao(IEnumerable<DocsResponse> docs)
        {
            if (!docs.Any())
            {
                return new List<DocsResponse>();
            }

            var docGroups = docs.GroupBy(d => d.DocumentCopyId);
            var docGroupDeparts = docGroups.Select((dg, index) =>
            {
                var first = dg.FirstOrDefault();
                if (first == null)
                {
                    return new DocsResponse();
                }
                var objectStatus = new DocsResponse
                {
                    AddressName = string.Join("\n", dg.Select(kvp => kvp.AddressName)),
                    DocCodeResponsed = "",
                    RequireResponseStatus = dg.Max(d => d.RequireResponseStatus),
                    DocumentCopyId = first.DocumentCopyId,
                    HasRequireResponse = first.HasRequireResponse,
                    IsResponsed = first.IsResponsed,
                    DocCode = first.DocCode,
                    DateResponsed = first.DateResponsed,
                    DateAppointed = first.DateAppointed,
                    DatePublished = first.DatePublished,
                    CurrentDepartmentExt = first.CurrentDepartmentExt,
                    CurrentDepartmentPath = first.CurrentDepartmentPath,
                    CurrentDepartmentName = first.CurrentDepartmentName,
                    Compendium = first.Compendium,
                    CategoryName = first.CategoryName,
                    UserCurrentId = first.UserCurrentId,
                    UserSuccess = first.UserSuccess,
                };
                var child = dg.ToList();
                objectStatus.ListResponse = child;
                return objectStatus;
            });

            return docGroupDeparts.ToList();
        }

        private List<DocsResponse> ParseXLVBHBByAddressName(IEnumerable<DocsResponse> docs, string groupBy)
        {
            groupBy = "AddressName";
            var docDeparts = docs.Select((d, index) =>
            {
                d.Index = index;
                d.ListResponse.Select((lr, idx) =>
                {
                    lr.Index = index;
                    return lr;
                });
                return d;
            });

            var result = new List<DocsResponse>();
            if (IsGrouping(groupBy))
            {
                IEnumerable<IGrouping<string, DocsResponse>> groups;
                switch (groupBy)
                {
                    case "AddressName":
                        groups = docDeparts.GroupBy(d => d.AddressName).OrderBy(g => g.Key);
                        break;
                    default:
                        groups = docDeparts.GroupBy(d => d.AddressName).OrderBy(g => g.Key);
                        break;
                }
                var isParent = false;
                foreach (var group in groups)
                {
                    result.Add(new DocsResponse()
                    {
                        GroupName = ParseDepartmentName(group.Key, out isParent),
                        GroupCount = group.Count(),
                        IsGroup = true
                    });
                    var idxs = 0;
                    foreach (var item in group)
                    {
                        item.GroupName = ParseDepartmentName(group.Key, out isParent);
                        item.Index = idxs;
                        item.ListResponse.Select((lr, idx) =>
                        {
                            lr.GroupName = ParseDepartmentName(group.Key, out isParent);
                            lr.Index = idxs;
                            return lr;
                        });
                        idxs++;
                    }

                    result.AddRange(group.Select(t =>
                    {
                        return t;
                    }));
                }

                return result;
            }

            return docDeparts == null && !docDeparts.Any() ? new List<DocsResponse>() : docDeparts.ToList();
        }

        private List<DocsResponse> ParseXLVBHBByStatus(IEnumerable<DocsResponse> docs, int status, string groupBy)
        {
            var docGroupDeparts = ParseGroupsHoiBao(docs);
            groupBy = "department";
            var docDeparts = docGroupDeparts.Where(dg => dg.RequireResponseStatus == status);
            docDeparts = docDeparts.Select((d, index) =>
            {
                d.Index = index;
                d.ListResponse.Select((lr, idx) =>
                {
                    lr.Index = index;
                    return lr;
                });
                return d;
            });

            var result = new List<DocsResponse>();
            if (IsGrouping(groupBy))
            {
                IEnumerable<IGrouping<string, DocsResponse>> groups;
                switch (groupBy)
                {
                    case "Department":
                        groups = docDeparts.GroupBy(d => d.CurrentDepartmentName).OrderBy(g => g.Key);
                        break;
                    default:
                        groups = docDeparts.GroupBy(d => d.CurrentDepartmentName).OrderBy(g => g.Key);
                        break;
                }
                var isParent = false;
                foreach (var group in groups)
                {
                    result.Add(new DocsResponse()
                    {
                        GroupName = ParseDepartmentName(group.Key, out isParent),
                        GroupCount = group.Count(),
                        IsGroup = true
                    });
                    var idxs = 0;
                    foreach (var item in group)
                    {
                        item.GroupName = ParseDepartmentName(group.Key, out isParent);
                        item.Index = idxs;
                        item.ListResponse.Select((lr, idx) =>
                        {
                            lr.GroupName = ParseDepartmentName(group.Key, out isParent);
                            lr.Index = idxs;
                            return lr;
                        });
                        idxs++;
                    }

                    result.AddRange(group.Select(t => 
                    {
                        return t;
                    }));
                }

                return result;
            }

            return docDeparts == null && !docDeparts.Any() ? new List<DocsResponse>() : docDeparts.ToList();
        }

        private List<DocsResponse> ParseDepartment(List<DocsResponse> docs)
        {
            var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();

            var allDepts = _departmentService.GetCacheAllDepartments();
            var allUser = _userService.GetAllCached();

            foreach (var doc in docs)
            {
                doc.CurrentDepartmentExt = "";
                doc.CurrentDepartmentPath = "";
                var userDept = allUserDepts.Where(u => u.UserId == doc.UserCurrentId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
                if (userDept == null)
                {
                    continue;
                }
                //var doc
                var dept = allDepts.SingleOrDefault(d => d.DepartmentId == userDept.DepartmentId);
                if (dept == null)
                {
                    continue;
                }

                doc.CurrentDepartmentExt = dept.DepartmentIdExt;
                doc.CurrentDepartmentPath = dept.DepartmentPath;
                doc.CurrentDepartmentName = dept.DepartmentName;
            }

            return docs;
        }

        private ProgressStatisticXlvb ParseXLVBHoiBaoDepartment(IEnumerable<DocsResponse> docs, string compare, string name)
        {
            var th = new ProgressStatisticXlvb();
            th.Name = name;
            th.Total = docs.Count();
            // Tổng số văn bản hồi báo
            docs = docs.Where(d => d.HasRequireResponse == true);

            var totalDocs = docs.Where(d => d.CurrentDepartmentExt.StartsWith(compare));

            th.NewReception = totalDocs == null ? 0 : totalDocs.Count();
            //if (th.NewReception == 0)
            //{
            //    continue;
            //}

            // Tổng số văn bản đã hồi báo
            var totalResponsed = docs.Where(d => d.CurrentDepartmentExt.StartsWith(compare) && d.IsResponsed);

            th.TotalSolved = totalResponsed == null ? 0 : totalResponsed.Count();

            // Tổng số văn bản hồi báo trễ hạn
            var responedOverTime = docs.Where(d => d.CurrentDepartmentExt.StartsWith(compare) &&
                d.IsResponsed &&
              d.RequireResponseStatus == 4);

            th.SolvedLate = responedOverTime == null ? 0 : responedOverTime.Count();
            th.SolvedInTime = th.TotalSolved - th.SolvedLate;

            // Tổng số văn bản chưa hồi báo
            var totalUnRespone = docs.Where(d => d.CurrentDepartmentExt.StartsWith(compare) && !d.IsResponsed);

            th.TotalPending = totalUnRespone == null ? 0 : totalUnRespone.Count();

            // Tổng số văn bản chưa hồi báo quá hạn
            var UnResponeOverTime = docs.Where(d => d.CurrentDepartmentExt.StartsWith(compare) && !d.IsResponsed &&
                d.RequireResponseStatus == 3
            );

            th.PendingLate = UnResponeOverTime == null ? 0 : UnResponeOverTime.Count();
            th.Pending = th.TotalPending - th.PendingLate;

            return th;
        }

        private ProgressStatisticXlvb ParseXLVBHoiBaoUser(IEnumerable<DocsResponse> docs, int compare)
        {
            var th = new ProgressStatisticXlvb();
            th.Total = docs.Count();
            // Tổng số văn bản hồi báo
            docs = docs.Where(d => d.HasRequireResponse == true);

            var totalDocs = docs.Where(d => d.UserCurrentId == compare);

            th.NewReception = totalDocs == null ? 0 : totalDocs.Count();
            //if (th.NewReception == 0)
            //{
            //    continue;
            //}

            // Tổng số văn bản đã hồi báo
            var totalResponsed = docs.Where(d => d.UserCurrentId == compare && d.IsResponsed);

            th.TotalSolved = totalResponsed == null ? 0 : totalResponsed.Count();

            // Tổng số văn bản hồi báo trễ hạn
            var responedOverTime = docs.Where(d => d.UserCurrentId == compare &&
                d.IsResponsed &&
                d.RequireResponseStatus == 4);

            th.SolvedLate = responedOverTime == null ? 0 : responedOverTime.Count();
            th.SolvedInTime = th.TotalSolved - th.SolvedLate;

            // Tổng số văn bản chưa hồi báo
            var totalUnRespone = docs.Where(d => d.UserCurrentId == compare && !d.IsResponsed);

            th.TotalPending = totalUnRespone == null ? 0 : totalUnRespone.Count();

            // Tổng số văn bản chưa hồi báo quá hạn
            var UnResponeOverTime = docs.Where(d => d.UserCurrentId == compare && !d.IsResponsed &&
                 d.RequireResponseStatus == 3
            );

            th.PendingLate = UnResponeOverTime == null ? 0 : UnResponeOverTime.Count();
            th.Pending = th.TotalPending - th.PendingLate;

            return th;
        }
        #endregion

        #region Liên thông

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<StoreDocumentIn> GiamSatTong_LienThong_Den(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_lienthong_den", model.From, model.To, model.StoreId);
            var docs = _cache.Get<IEnumerable<StoreDocumentIn>>(cacheKey, () =>
            {
                return ExecuteDatabase<StoreDocumentIn>("Giamsat_lienthong_den", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);
            total = 0;
            if (model.IsFirstLoad && IsGrouping(model.GroupBy))
            {
                total = docs.Count();
                docs = ParseGroup(docs, model.GroupBy, model.IsGetAll);
            }
            else
            {
                docs = ParseSoVanBanGroupAPI(docs, model, out total);
            }

            return docs;
        }

        #endregion

        #region Sổ văn bản API XLVB

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<StoreDocumentIn> GS_SoVanBanDen(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_sovanbanden", model.From, model.To, model.StoreId);
            var docs = _cache.Get<IEnumerable<StoreDocumentIn>>(cacheKey, () =>
            {
                return ExecuteDatabase<StoreDocumentIn>("Giamsat_sovanbanden", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);

            total = 0;
            if (model.IsFirstLoad && IsGrouping(model.GroupBy))
            {
                total = docs.Count();
                docs = ParseGroup(docs, model.GroupBy, model.IsGetAll);
            }
            else
            {
                docs = ParseSoVanBanGroupAPI(docs, model, out total);
            }

            return docs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<StoreDocumentIn> GS_SoVanBanDi(StatisticsCriteriaObject model, out int total)
        {
            var cacheKey = BuildCacheKey("Giamsat_sovanbandi", model.From, model.To, model.StoreId);
            var docs = _cache.Get<IEnumerable<StoreDocumentIn>>(cacheKey, () =>
            {
                return ExecuteDatabase<StoreDocumentIn>("Giamsat_sovanbandi", model.From, model.To, false, model.StoreId); ;
            }, CacheParam.ReportCacheTimeOut);
            total = 0;
            if (model.IsFirstLoad && IsGrouping(model.GroupBy))
            {
                total = docs.Count();
                docs = ParseGroup(docs, model.GroupBy, model.IsGetAll);
            }
            else
            {
                docs = ParseSoVanBanGroupAPI(docs, model, out total);
            }

            return docs;
        }

        private List<StoreDocumentIn> ParseGroup(IEnumerable<StoreDocumentIn> docs, string groupBy, bool isGetAll)
        {
            IEnumerable<IGrouping<string, StoreDocumentIn>> groups;

            var result = new List<StoreDocumentIn>();

            switch (groupBy)
            {
                case "UserSuccess":
                    groups = docs.GroupBy(d => d.UserSuccess).OrderBy(g => g.Key);
                    break;
                case "UserCreated":
                    groups = docs.GroupBy(d => d.UserCreatedName).OrderBy(g => g.Key);
                    break;
                case "UserCreatedId":
                    groups = docs.GroupBy(d => d.UserSuccess).OrderBy(g => g.Key);
                    break;
                case "Category":
                    groups = docs.GroupBy(d => d.CategoryName).OrderBy(g => g.Key);
                    break;
                case "Organization":
                    groups = docs.GroupBy(d => d.Organization).OrderBy(g => g.Key);
                    break;
                case "ProcessInfo":
                    groups = docs.GroupBy(d => d.ProcessInfo).OrderBy(g => g.Key);
                    break;
                default:
                    groups = docs.GroupBy(d => d.ProcessInfo).OrderBy(g => g.Key);
                    break;
            }
            foreach (var group in groups)
            {
                result.Add(new StoreDocumentIn()
                {
                    GroupName = group.Key,
                    GroupCount = group.Count(),
                    IsGroup = true
                });

                if (isGetAll)
                {
                    result.AddRange(group.OrderBy(d => d.DatePublished));
                }
            }

            return result;
        }

        private IEnumerable<StoreDocumentIn> ParseSoVanBanGroupAPI(IEnumerable<StoreDocumentIn> docs, StatisticsCriteriaObject model, out int total)
        {
            total = 0;
            if (model.IsGetAll)
            {
                return docs;
            }
            switch (model.GroupBy)
            {
                case "UserSuccess":
                    docs = docs.Where(d => d.UserSuccess == model.GroupName).OrderBy(g => g.UserSuccess);
                    break;
                case "UserCreated":
                    docs = docs.Where(d => d.UserCreatedName == model.GroupName).OrderBy(g => g.UserCreatedName);
                    break;
                case "Category":
                    docs = docs.Where(d => d.CategoryName == model.GroupName).OrderBy(g => g.CategoryName);
                    break;
                case "Organization":
                    docs = docs.Where(d => d.Organization == model.GroupName).OrderBy(g => g.Organization);
                    break;
                case "ProcessInfo":
                    docs = docs.Where(d => d.ProcessInfo == model.GroupName).OrderBy(g => g.ProcessInfo);
                    break;
            }
            if (docs == null)
            {
                return new List<StoreDocumentIn>();
            }
            if (!string.IsNullOrEmpty(model.GroupName))
            {
                foreach (var doc in docs)
                {
                    doc.GroupName = model.GroupName;
                }
            }
            total = docs.Count();
            return docs.Skip(model.RecordPerPage * (model.Page - 1)).Take(model.RecordPerPage);
        }

        #region Sổ văn bản đến

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public IEnumerable<StoreDocumentIn> GiamSatTong_SoVanBanDen(int storeId, DateTime from, DateTime to, string groupBy)
        {
            var docs = ExecuteDatabase<StoreDocumentIn>("Giamsat_sovanbanden", from, to, false, storeId);

            if (IsGrouping(groupBy))
            {
                return ParseSoVanBanGroup(docs, groupBy);
            }

            return docs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public IEnumerable<StatisticPublish> GiamSatTong_SoVanBanDi(int storeId, DateTime from, DateTime to, string groupBy)
        {
            var docs = ExecuteDatabase<StatisticPublish>("Giamsat_sovanbandi", from, to, false, storeId);

            IEnumerable<IGrouping<string, StatisticPublish>> groups;
            var result = new List<StatisticPublish>();
            if (IsGrouping(groupBy))
            {

                switch (groupBy)
                {
                    case "Category":
                        groups = docs.GroupBy(d => d.CategoryName).OrderBy(g => g.Key);
                        break;
                    default:
                        groups = docs.GroupBy(d => d.CategoryName).OrderBy(g => g.Key);
                        break;
                }

                foreach (var group in groups)
                {
                    result.Add(new StatisticPublish()
                    {
                        GroupName = group.Key,
                        GroupCount = group.Count(),
                        IsGroup = true
                    });

                    foreach (var item in group)
                    {
                        item.GroupName = group.Key;
                    }

                    result.AddRange(group.OrderBy(d => d.DatePublished));
                }

                return result;
            }

            return docs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="statisticName"></param>
        /// <returns></returns>
        public List<StoreDocumentIn> GetDocExport(StatisticsCriteriaObject model, string statisticName)
        {
            var docs = new List<StoreDocumentIn>();

            var total = 0;
            switch (statisticName)
            {
                case "SoVBDi":
                    docs = GS_SoVanBanDi(model, out total).ToList();
                    break;
                case "SoVBDen":
                    docs = GS_SoVanBanDen(model, out total).ToList();
                    break;
                case "VBDi":
                    docs = GiamSatTong_PhatHanh(model, out total).ToList();
                    break;
                case "VBDenLT":
                    docs = GiamSatTong_LienThong_Den(model, out total).ToList();
                    break;
                case "VBDenTW":
                    docs = GiamSatTong_Den_TW(model, out total).ToList();
                    break;
            }

            return docs;
        }

        private List<StoreDocumentIn> ParseSoVanBanGroup(IEnumerable<StoreDocumentIn> docs, string groupBy)
        {
            IEnumerable<IGrouping<string, StoreDocumentIn>> groups;

            var result = new List<StoreDocumentIn>();

            switch (groupBy)
            {
                case "Category":
                    groups = docs.GroupBy(d => d.CategoryName).OrderBy(g => g.Key);
                    break;
                case "Organization":
                    groups = docs.GroupBy(d => d.Organization).OrderBy(g => g.Key);
                    break;
                case "ProcessInfo":
                    groups = docs.GroupBy(d => d.ProcessInfo).OrderBy(g => g.Key);
                    break;
                default:
                    groups = docs.GroupBy(d => d.CategoryName).OrderBy(g => g.Key);
                    break;
            }

            foreach (var group in groups)
            {
                result.Add(new StoreDocumentIn()
                {
                    GroupName = group.Key,
                    GroupCount = group.Count(),
                    IsGroup = true
                });

                foreach (var item in group)
                {
                    item.GroupName = group.Key;
                }

                result.AddRange(group.OrderBy(d => d.DatePublished));
            }

            return result;
        }

        #endregion

        private IEnumerable<T> ExecuteDatabase<T>(string storeName, DateTime from, DateTime to, bool hasOldDocument, int storeId = 0, bool isResponse = false)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@from", from));
            parameters.Add(new SqlParameter("@to", to));
            parameters.Add(new SqlParameter("@storeId", storeId));
            parameters.Add(new SqlParameter("@hasOldDocument", hasOldDocument));
            parameters.Add(new SqlParameter("@isResponse", isResponse));

            var docs = Context.RawProcedure(storeName, parameters.ToArray());
            var a = Json2.Stringify(docs);
            var result = Json2.ParseAs<IEnumerable<T>>(a);

            return result;
        }

        private string BuildCacheKey(string statisticName, DateTime from, DateTime to, int storeId)
        {
            var key = string.Join("_", new string[] { statisticName,
                            from.ToString("yyyyMMdd"), to.ToString("yyyyMMdd"), storeId.ToString() });
            return string.Format(CacheParam.StatisticKey, key);
        }

        #endregion
    }
}
