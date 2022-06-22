using Bkav.eGovCloud.Api.Service;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Core.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Document)]
    public class PublishAPIController : EgovApiBaseController
    {
        private readonly AddressBll _addressService;
        private readonly AttachmentBll _attachmentService;
        private readonly FileBll _fileService;
        private readonly ApproverBll _approverService;
        private readonly CommentBll _commentService;
        private readonly DailyProcessBll _dailyProcessService;
        private readonly DocumentBll _documentService;
        private readonly DocumentApiService _documentApiService;
        private readonly DocumentPublishBll _docPublishService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocTimelineBll _docTimelineService;
        private readonly DocTypeBll _doctypeService;
        private readonly DepartmentBll _departmentService;
        private readonly InfomationBll _imfomationService;
        private readonly LogBll _logService;
        private readonly TransferSettings _transferSetting;
        private readonly SettingBll _settingService;
        private readonly SupplementaryBll _supplementaryService;
        private readonly WorkflowHelper _workflowHelper;
        private readonly CategoryBll _categoryService;
        private readonly FeeBll _feeService;
        private readonly PaperBll _paperService;
        private readonly FormHelper _formHelper;
        private readonly FormBll _formService;
        private readonly CodeBll _codeService;
        private readonly UserBll _userService;

        private const string DEFAULT_DATETIME_FORMAT = "dd/MM/yyyy";
        // Để tạm: Xử lý nhanh hơn việc load ở webconfig
        private const string TOKEN = "90f683b8-1f47-468a-9af6-9a30c0ec2174";

        /// <summary>
        /// C'tor
        /// </summary>
        public PublishAPIController()
        {
            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
            _docPublishService = DependencyResolver.Current.GetService<DocumentPublishBll>();
            _docCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
            _attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
            _addressService = DependencyResolver.Current.GetService<AddressBll>();
            _supplementaryService = DependencyResolver.Current.GetService<SupplementaryBll>();
            _logService = DependencyResolver.Current.GetService<LogBll>();
            _transferSetting = DependencyResolver.Current.GetService<Bkav.eGovCloud.Entities.Customer.TransferSettings>();
            _dailyProcessService = DependencyResolver.Current.GetService<DailyProcessBll>();
            _imfomationService = DependencyResolver.Current.GetService<InfomationBll>();
            _settingService = DependencyResolver.Current.GetService<SettingBll>();
            _workflowHelper = DependencyResolver.Current.GetService<WorkflowHelper>();
            _documentApiService = DependencyResolver.Current.GetService<DocumentApiService>();
            _categoryService = DependencyResolver.Current.GetService<CategoryBll>();
            _feeService = DependencyResolver.Current.GetService<FeeBll>();
            _paperService = DependencyResolver.Current.GetService<PaperBll>();
            _formHelper = DependencyResolver.Current.GetService<FormHelper>();
            _formService = DependencyResolver.Current.GetService<FormBll>();
            _docTimelineService = DependencyResolver.Current.GetService<DocTimelineBll>();
            _commentService = DependencyResolver.Current.GetService<CommentBll>();
            _approverService = DependencyResolver.Current.GetService<ApproverBll>();
            _departmentService = DependencyResolver.Current.GetService<DepartmentBll>();
            _fileService = DependencyResolver.Current.GetService<FileBll>();
            _codeService = DependencyResolver.Current.GetService<CodeBll>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
        }

        [System.Web.Http.HttpGet]
        public string Test()
        {
            return "";
        }

        /// <summary>
        /// Lấy ra các văn bản đi đã phát hành
        /// </summary>
        /// <param name="fromDate">Thời gian bắt đầu</param>
        /// <param name="limitNum">Số bản ghi</param>
        /// <param name="token">Mã xác thực</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<CongVan> GetMultiOutGoingDocData(DateTime fromDate, int limitNum, string token)
        {
            if (TOKEN != token.Trim())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sai Token"));
            }

            // lấy ra danh sách id các văn bản phát hành
            // var docCopyIds = GetDocumentCopyIds(fromDate, limitNum);

            var result = Json2.ParseAs<IEnumerable<CongVan>>(_docPublishService.GetForCDDH(fromDate, limitNum));

            var allUsers = _userService.GetAllCached();

            foreach (var congvan in result)
            {
                var userApprover = congvan.nguoi_ky_chinh_id != 0 ? allUsers.FirstOrDefault(u => u.UserId == congvan.nguoi_ky_chinh_id) : null;
                var userCreated = congvan.nguoi_soan_id != 0 ? allUsers.FirstOrDefault(u => u.UserId == congvan.nguoi_soan_id) : null;

                congvan.nguoi_ky_chinh = userApprover != null ? userApprover.UsernameEmailDomain : "";
                congvan.nguoi_soan = userCreated != null ? userCreated.UsernameEmailDomain : "";

                var attachments = _attachmentService.Gets(congvan.documentId);

                congvan.file_dinh_kem.AddRange(attachments.Select(a => new AttachmentPublish()
                {
                    attachment_content_id = a.AttachmentId,
                    kich_thuoc = StringExtension.ReadFileSize(a.Size),
                    ten_attachment = a.AttachmentName,
                    file_path = a.Extension
                }));
            }

            return result;
        }

        /// <summary>
        /// Hàm Tải file đính kèm khí có mã file
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public string GetFileInBase64(int fileId, string token)
        {
            if (TOKEN != token.Trim())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sai Token"));
            }
            string fileName;

            var fileStream = _attachmentService.TestDownloadAttachment(out fileName, fileId);
            return fileStream.ToBase64String();
        }

        /// <summary>
        /// Cập nhật chỉ đạo điều hành
        /// </summary>
        /// <param name="cong_van_id"></param>
        /// <param name="bStatus"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public bool UpdateCongVanStatus(int cong_van_id, Boolean bStatus, string token)
        {
            if (TOKEN != token.Trim())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sai Token"));
            }
            try
            {
                _docPublishService.UpdateCDDH(cong_van_id, bStatus);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //[System.Web.Http.HttpGet]
        //public DocumentOnlineLookup Lookup(string docCode)
        //{
        //    DocumentOnlineLookup result;
        //    docCode = HttpUtility.UrlDecode(docCode);
        //    var doc = _documentService.GetHsmc(docCode);
        //    if (doc == null)
        //    {
        //        return null;
        //    }

        //    var documentcopy = _docCopyService.GetMain(doc.DocumentId);
        //    //var userSuccess = doc.UserSuccessId.HasValue ? _userService.GetFromCache(doc.UserSuccessId.Value).Username : "";
        //    var userReturn = doc.UserReturnedId.HasValue ? _userService.GetFromCache(doc.UserReturnedId.Value).Username : "";

        //    var currentUser = documentcopy.UserCurrentName;
        //    var currentDept = documentcopy.CurrentDepartmentName;

        //    var progress = GetProgress(documentcopy, doc);

        //    result = new DocumentOnlineLookup()
        //    {
        //        Compendium = doc.Compendium,
        //        DateReceived = doc.DateCreated,
        //        DateAppointed = doc.DateAppointed,
        //        DocCode = doc.DocCode,
        //        ResultStatus = doc.ResultStatus,
        //        ReturnNote = doc.ReturnNote,
        //        Status = doc.Status,
        //        CitizenName = doc.CitizenName,
        //        Address = doc.Address,
        //        Phone = doc.Phone,
        //        IdCard = doc.IdentityCard,
        //        Email = doc.Email,
        //        Progress = progress
        //    };

        //    result.DocTypeName = doc.DocTypeName;

        //    return result;
        //}

        /// <summary>
        /// Trả về danh sách các documentid đã phát hành
        /// </summary>
        /// <returns>Danh sách DocumentId</returns>
        private List<int> GetDocumentCopyIds(DateTime from, int limitNum)
        {
            var pending = _docPublishService
                                    .Gets(true, dp =>
                                            dp.DatePublished >= from
                                            && (!dp.IsSentCDDH.HasValue || dp.IsSentCDDH.Value == false))
                                    .OrderBy(d => d.DatePublished);

            return pending.Select(p => p.DocumentCopyId).Distinct().Take(limitNum).ToList();
        }

        //private List<DocumentProcessDto> GetProgress(DocumentCopy documentCopy, Document doc)
        //{
        //    var progress = new List<DocumentProcessDto>();
        //    if (documentCopy == null || documentCopy.Histories == null || !documentCopy.Histories.HistoryPath.Any())
        //    {
        //        return progress;
        //    }

        //    var historyPaths = documentCopy.Histories.HistoryPath;
        //    var lastDate = DateTime.Now;
        //    var currentOffice = _imfomationService.GetCurrentOfficeName();

        //    var userSentIds = historyPaths.Select(h => h.UserSendId).Distinct();
        //    var userSents = _userService.GetCacheAllUsers(isActivated: true).Where(u => userSentIds.Contains(u.UserId)).ToList();

        //    foreach (var history in historyPaths)
        //    {
        //        var userSend = userSents.SingleOrDefault(u => u.UserId == history.UserSendId);
        //        var dateCreate = history.DateCreated;
        //        var node = _workflowHelper.GetNode(history.WorkflowSendId, history.NodeSendId);
        //        var isSuccess = doc.IsSuccess == true
        //                        || (doc.IsReturned == true)
        //                        || (doc.Status == (int)DocumentStatus.KetThuc)
        //                        || history.UserReceives.Any();

        //        // Bỏ qua trường hợp tự chuyển đến cho mình ở node hiện tại.
        //        if ((history.UserSendId != history.UserReceiveId) && (history.NodeSendId != history.NodeReceiveId))
        //        {
        //            progress.Add(new DocumentProcessDto()
        //            {
        //                NodeName = node == null ? "" : node.NodeName,
        //                UserName = userSend == null ? "" : userSend.FullName,
        //                DateCreated = lastDate,
        //                IsSuccess = true,
        //                OfficeName = currentOffice
        //            });
        //        }

        //        if (!isSuccess)
        //        {
        //            var userReceive = _userService.GetFromCache(history.UserReceiveId);
        //            var nodeReceive = _workflowHelper.GetNode(history.WorkflowReceiveId, history.NodeReceiveId);
        //            progress.Add(new DocumentProcessDto()
        //            {
        //                NodeName = nodeReceive == null ? "" : nodeReceive.NodeName,
        //                UserName = userReceive == null ? "" : userReceive.FullName,
        //                DateCreated = dateCreate,
        //                IsSuccess = (doc.IsSuccess | doc.IsReturned) ?? false,
        //                OfficeName = currentOffice
        //            });
        //        }

        //        lastDate = dateCreate;
        //    }

        //    #region Kiếm tra tiến độ hồ sơ nếu là đang liên thông

        //    var docPublish = _docPublishService.GetSentPublishes(documentCopy.DocumentCopyId);
        //    if (docPublish.Any(p => !p.IsResponsed))
        //    {
        //        doc.Status = 16;
        //    }

        //    foreach (var publish in docPublish)
        //    {
        //        currentOffice = publish.AddressName;
        //        if (string.IsNullOrEmpty(publish.Traces))
        //        {
        //            progress.Add(new DocumentProcessDto()
        //            {
        //                NodeName = "Tiếp nhận",
        //                UserName = currentOffice,
        //                DateCreated = DateTime.Now,
        //                IsSuccess = publish.IsResponsed,
        //                OfficeName = currentOffice
        //            });
        //        }
        //        else
        //        {
        //            var traces = Json2.ParseAs<List<DocumentTrace>>(publish.Traces);
        //            progress.AddRange(traces.Select(t => new DocumentProcessDto()
        //            {
        //                DateCreated = t.DateCreated,
        //                NodeName = t.Comment,
        //                OfficeName = currentOffice,
        //                UserName = t.UserName,
        //                IsSuccess = true,
        //            }));
        //        }
        //    }

        //    #endregion

        //    return progress;
        //}
    }

    /// <summary>
    /// Class lưu thông tin của công văn
    /// </summary>
    public class CongVan
    {
        public int cong_van_id { get; set; }
        public Guid documentId { get; set; }

        public string so_ky_hieu { get; set; }
        public string trich_yeu { get; set; }
        public DateTime ngay_cong_van { get; set; }

        public int nguoi_soan_id { get; set; }
        public string nguoi_soan { get; set; }

        public string nguoi_ky_sao { get; set; }

        public int nguoi_ky_chinh_id { get; set; }
        public string nguoi_ky_chinh { get; set; }
        public DateTime created_date { get; set; }
        public List<AttachmentPublish> file_dinh_kem { get; set; }

        public CongVan()
        {
            file_dinh_kem = new List<AttachmentPublish>();
        }
    }

    /// <summary>
    /// Class lưu thông tin của file đính kèm
    /// </summary>
    public class AttachmentPublish
    {
        public int attachment_content_id { get; set; }
        public string ten_attachment { get; set; }
        public string kich_thuoc { get; set; }
        public string file_path { get; set; }
    }

}