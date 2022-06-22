using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Microsoft.AspNet.SignalR;
using Bkav.eGovCloud.SmsService;
using Bkav.eGovCloud.Core.Statistic;
using Bkav.eGovCloud.Entities.Common;
using Bkav.EReport.RazorEngine;
using Bkav.eGovCloud.Core.Logging;
using System.Threading;
using System.Threading.Tasks;
using Bkav.eGovCloud.Business.Objects;
using System.Data.SqlClient;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Statistic;

namespace Bkav.eGovCloud.Business.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class TimerJobHelper : ServiceBase
    {
        private SendEmailHelper _mailHelper;
        private SendSmsHelper _smsHelper;
        private TimeJobBll _timejobService;
        private LogBll _logService;
        private BackupRestoreConfigBll _backupRestoreConfigService;
        private BackupRestoreFileConfigBll _backupRestoreFileConfigService;
        private ResourceBll _resourceService;
        private IncreaseBll _increaService;
        private SupplementaryBll _supplementaryService;
        private MailBll _mailService;
        private ActivityLogBll _activityLogService;
        private DocumentBll _documentService;
        private DocumentCopyBll _documentCopyService;
        private CommentBll _commentService;
        private AttachmentBll _attachmentService;
        private UserBll _userService;
        private readonly MemoryCacheManager _cacheManager;
        private readonly StatisticBll _statisticService;
        private DocFieldBll _docfieldService;

        /// <summary>
        /// 
        /// </summary>
        public TimerJobHelper(IDbCustomerContext context)
            : base(context)
        {
            _statisticService = DependencyResolver.Current.GetService<StatisticBll>();
            _timejobService = DependencyResolver.Current.GetService<TimeJobBll>();
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            _logService = DependencyResolver.Current.GetService<LogBll>();
            _activityLogService = DependencyResolver.Current.GetService<ActivityLogBll>();
            _cacheManager = DependencyResolver.Current.GetService<MemoryCacheManager>();
            _docfieldService = DependencyResolver.Current.GetService<DocFieldBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RunAll()
        {
            IEnumerable<TimeJob> timeJobIsActives;
            try
            {
                timeJobIsActives = _timejobService.GetAllActive();
            }
            catch
            {
                timeJobIsActives = new List<TimeJob>();
            }

            //Lấy ra danh sách các timeJob được phép thực thi
            if (timeJobIsActives == null || !timeJobIsActives.Any())
            {
                return;
            }

            // Vòng lặp song song
            // Cho phép chạy các timejob cùng lúc.
            foreach (var timeJob in timeJobIsActives)
            {
                if (!IsTurn(timeJob))
                {
                    continue;
                }

                try
                {
                    // Sử dụng set running theo Id tránh việc thời gian thực thi job lâu thì connection sẽ timeout
                    _timejobService.SetJobRunning(timeJob.TimeJobId);

                    RunThread(timeJob);

                    // Sử dụng set success theo Id tránh việc thời gian thực thi job lâu thì connection sẽ timeout
                    _timejobService.SetJobSuccess(timeJob.TimeJobId);
                }
                catch (Exception ex)
                {
                    _timejobService.SetJobRunFail(timeJob.TimeJobId);
                    LogException(ex);
                    continue;
                }
            }
        }

        private void RunThread(TimeJob timeJob)
        {
            switch (timeJob.TimerJobType)
            {
                case (int)EgovJobEnum.IndexTimerElapsed:
                    break;

                case (int)EgovJobEnum.GetDocumentsFromEdocTool:
                    // result = new Thread(new ThreadStart(() => SyncDocuments()));
                    SyncDocuments();
                    break;

                case (int)EgovJobEnum.CheckServices:
                    // result = new Thread(new ThreadStart(() => CheckServices()));
                    break;

                case (int)EgovJobEnum.NotifyDocInProcesses:
                    // result = new Thread(new ThreadStart(() => NotifyDocInProcesses()));
                    break;

                case (int)EgovJobEnum.NotifyDocUnread:
                    // result = new Thread(new ThreadStart(() => NotifyDocUnread()));
                    break;

                case (int)EgovJobEnum.AddIndex:
                    // AddIndex(timeJob);
                    break;

                case (int)EgovJobEnum.BackupDatabase:
                    // result = new Thread(new ThreadStart(() => BackupDatabase()));
                    break;

                case (int)EgovJobEnum.BackupFile:
                    // result = new Thread(new ThreadStart(() => BackupFile()));
                    break;

                case (int)EgovJobEnum.SendSms:
                    // result = new Thread(new ThreadStart(() => SendSms()));
                    SendSms();
                    break;

                case (int)EgovJobEnum.SendMail:
                    // result = new Thread(new ThreadStart(() => SendMail()));
                    SendMail();
                    break;

                case (int)EgovJobEnum.ResetInCrease:
                    // result = new Thread(new ThreadStart(() => ResetIncreate()));
                    // ResetIncreate();
                    break;

                case (int)EgovJobEnum.CheckNewMail:
                    // result = new Thread(new ThreadStart(() => CheckNewMail()));
                    CheckNewMail();
                    break;
                case (int)EgovJobEnum.SendWarning:
                    // result = new Thread(new ThreadStart(() => SendWarning()));
                    SendWarning();
                    break;
                case (int)EgovJobEnum.SendReportToDVCService:
                    SendReportToDVCService();
                    break;
            }
        }

        /// <summary>
        /// Test sendwanrning
        /// </summary>
        public void TestSendWarning()
        {
            SendWarning();
        }

        /// <summary>
        /// Test update Document
        /// </summary>
        public void TestSendDocument()
        {
            UpdateSentDocument();
        }

        #region Private Method

        /// <summary>
        /// Kiểm tra xem service đã chạy chưa trong khoảng thời gian cho phép
        /// </summary>
        /// <returns></returns>
        private bool IsTurn(TimeJob timeJob)
        {
            if (!timeJob.DateLastJobRun.HasValue)
            {
                // Chưa chạy lần nào
                return true;
            }

            var dateNow = DateTime.Now;
            var lastRun = timeJob.DateLastJobRun.Value;

            if (timeJob.IsRunning)
            {
                // Nếu thời gian chạy gần nhất > 5h (nửa ngày) thì sẽ cho chạy lại: do có khả năng việc cập nhật trạng thái bị lỗi.
                return dateNow.Subtract(lastRun).TotalHours > 5;
            }

            var result = true;
            switch (timeJob.ScheduleType)
            {
                case (int)ScheduleType.HangPhut:
                    //Kiểm tra phút, giờ, ngày, tháng, năm nếu trùng với hiện tại => đã chạy rồi không chạy nữa
                    if (lastRun.Minute == dateNow.Minute && lastRun.Hour == dateNow.Hour && lastRun.Day == dateNow.Day
                            && lastRun.Month == dateNow.Month && lastRun.Year == dateNow.Year)
                    {
                        result = false;
                    }
                    break;
                case (int)ScheduleType.HangGio:
                    if (lastRun.Hour == dateNow.Hour && lastRun.Day == dateNow.Day
                            && lastRun.Month == dateNow.Month && lastRun.Year == dateNow.Year)
                    {
                        result = false;
                    }
                    break;
                case (int)ScheduleType.HangNgay:
                    if (lastRun.Day == dateNow.Day && lastRun.Month == dateNow.Month && lastRun.Year == dateNow.Year)
                    {
                        result = false;
                    }
                    break;
                case (int)ScheduleType.HangTuan:
                    var weekend = dateNow.GetIso8601WeekOfYear();
                    var weekend2 = lastRun.GetIso8601WeekOfYear();
                    if (weekend <= weekend2 && lastRun.Year == dateNow.Year)
                    {
                        result = false;
                    }
                    break;
                case (int)ScheduleType.HangThang:
                    if (lastRun.Month == dateNow.Month
                    && lastRun.Year == dateNow.Year)
                    {
                        result = false;
                    }
                    break;
                case (int)ScheduleType.HangNam:
                    if (lastRun.Year == dateNow.Year)
                    {
                        result = false;
                    }
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Sao lưu file
        /// </summary>
        private void BackupFile()
        {
            try
            {
                _backupRestoreFileConfigService = DependencyResolver.Current.GetService<BackupRestoreFileConfigBll>();
                var configHasAutoRuns = _backupRestoreFileConfigService.Gets(p => p.HasAutoRun);
                if (configHasAutoRuns == null || !configHasAutoRuns.Any())
                {
                    return;
                }

                foreach (var config in configHasAutoRuns)
                {
                    try
                    {
                        _backupRestoreFileConfigService.Backup(config);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                        _backupRestoreFileConfigService.CreateHistoryBackupError(config);
                    }
                }

                //_timejobService = DependencyResolver.Current.GetService<TimeJobBll>();
                //_timejobService.SetJobSuccess(timeJob);
            }
            finally
            {
                // _timejobService.SetJobSuccess(timeJob);
            }
        }

        /// <summary>
        /// Sao lưu cơ sở dữ liệu
        /// </summary>
        private void BackupDatabase()
        {
            try
            {
                _backupRestoreConfigService = DependencyResolver.Current.GetService<BackupRestoreConfigBll>();
                var configHasAutoRuns = _backupRestoreConfigService.GetReaOnlys(p => p.HasAutoRun);
                if (configHasAutoRuns == null || !configHasAutoRuns.Any())
                {
                    return;
                }

                foreach (var config in configHasAutoRuns)
                {
                    try
                    {
                        _backupRestoreConfigService.Backup(config);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                        _backupRestoreConfigService.CrateHitoryBackupError(config, DateTime.Now);
                    }
                }
            }
            finally
            {
                // _timejobService.SetJobSuccess(timeJob);
            }
        }

        /// <summary>
        /// Ghi log lỗi
        /// </summary>
        /// <param name="exc"></param>
        private void LogException(Exception exc)
        {
            if (exc == null)
            {
                return;
            }
            _logService.Error(exc.Message, exc);
        }

        /// <summary>
        /// Đồng bộ văn bản:
        /// - KIểm tra Văn bản liên thông đang chờ gửi và gửi đi.
        /// - Update tiến độ các văn bản liên thông.
        /// - Update tiến độ hồ sơ đăng ký trực tuyến.
        /// </summary>
        private void SyncDocuments()
        {
            try
            {
                SendPendingDocuments();
            }
            catch (Exception ex)
            {
                _logService.Error("Liên Thông", ex);
            }

            try
            {
                UpdateSentDocument();
            }
            catch (Exception ex)
            {
                _logService.Error("Liên Thông", ex);
            }

            try
            {
                SyncOnlineDocument();
            }
            catch (Exception ex)
            {
                _logService.Error("Cập nhật Online", ex);
            }
        }

        private void UpdateSentDocument()
        {
            var _docPublishService = DependencyResolver.Current.GetService<DocumentPublishBll>();

            // Cập nhật tiến độ hồ sơ đang gửi liên thông
            var sendingDocs = _docPublishService.Gets(false, d => !d.IsResponsed && d.AddressId.HasValue && !d.IsPending && d.HasLienThong);

            if (!sendingDocs.Any())
            {
                return;
            }

            var _docCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            var _edocService = DependencyResolver.Current.GetService<EdocBll>();
            var _addressService = DependencyResolver.Current.GetService<AddressBll>();

            var currentAddress = _addressService.GetCurrent();
            if (currentAddress == null || string.IsNullOrEmpty(currentAddress.EdocId))
            {
                return;
            }

            var currentEdocId = currentAddress.EdocId;
            var addressIds = sendingDocs.Select(d => d.AddressId.Value).Distinct().ToList();
            var addresses = _addressService.Gets(addressIds);

            var allSentPublished = _docPublishService.GetSentPublishes();

            foreach (var docPublish in sendingDocs)
            {
                var docCode = docPublish.DocCode;

                try
                {
                    var address = addresses.SingleOrDefault(a => a.AddressId == docPublish.AddressId.Value);
                    if (address == null)
                    {
                        continue;
                    }

                    var traces = _edocService.GetTraces(docCode, address.Website, currentEdocId);
                    if (traces == null || !traces.Any())
                    {
                        continue;
                    }

                    var status = traces.OrderByDescending(t => t.DateCreated).First().Status;
                    var hasResult = status == (int)DocumentStatus.KetThuc || status == (int)DocumentStatus.LoaiBo;
                    if (hasResult)
                    {
                        docPublish.IsResponsed = true;
                        docPublish.DateResponsed = DateTime.Now;
                        docPublish.Note = "Hồ sơ, văn bản liên thông đã xử lý.";

                        // Cập nhật trở lại hồ sơ gốc đã gửi liên thông
                        // Nếu không có hồ sơ đang liên thông sang đơn vị khác thì kết thúc xử lý luôn.
                        var documentCopyId = docPublish.DocumentCopyId;
                        var otherLienthong = allSentPublished.Where(d => d.DocumentCopyId == documentCopyId);
                        if (!otherLienthong.Any(d => !d.IsResponsed && d.DocumentPublishId != docPublish.DocumentPublishId))
                        {
                            var documentCopy = _docCopyService.Get(documentCopyId);
                            if (documentCopy != null)
                            {
                                _docCopyService.Finish(documentCopy, DateTime.Now, documentCopy.UserCurrentId, "Kết thúc xử lý: hồ sơ liên thông đã kết thúc.");
                            }
                        }
                    }

                    docPublish.Traces = Json2.Stringify(traces);

                    _docPublishService.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logService.Error("Lỗi Cập nhật tiến độ hồ sơ liên thông" + docPublish.DocumentPublishId, ex);
                    continue;
                }
            }

            // Đối tượng khởi tạo trong timejob cần được hủy sau khi thực thi
            //_docCopyService.Dispose();
            //_docPublishService.Dispose();
            //_edocService.Dispose();
        }

        /// <summary>
        /// Kiểm tra văn bản liên thông đang chờ gửi và gửi đi
        /// </summary>
        private void SendPendingDocuments()
        {
            var _docPublishService = DependencyResolver.Current.GetService<DocumentPublishBll>();

            // Những hồ sơ một cửa chờ gửi liên thông
            // Văn bản sử dụng tool liên thông qua bdoc
            var pendings = _docPublishService.GetPending().Where(d => d.IsHsmc).OrderByDescending(d => d.DatePublished);

            if (!pendings.Any())
            {
                // _docPublishService.Dispose();
                return;
            }

            var _docCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            var _edocService = DependencyResolver.Current.GetService<EdocBll>();

            var documentCopyIds = pendings.Select(p => p.DocumentCopyId).Distinct().ToList();
            var documentCopies = _docCopyService.Gets(documentCopyIds, true);

            foreach (var docPublish in pendings)
            {
                try
                {
                    var documentCopy = documentCopies.SingleOrDefault(dc => dc.DocumentCopyId == docPublish.DocumentCopyId);
                    if (documentCopy == null)
                    {
                        //Todo: Xem lại nên xóa hay set lại trạng thái
                        docPublish.HasLienThong = false;
                        docPublish.IsPending = false;

                        continue;
                    }

                    var isSentDocument = _edocService.SendDocument(documentCopy, docPublish.AddressId.Value, docPublish.DateResponsed);

                    if (isSentDocument)
                    {
                        // Cập nhật lại sau khi gửi thành công
                        docPublish.IsPending = false;
                    }

                    _docPublishService.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logService.Error("Gửi hồ sơ liên thông " + docPublish.DocumentPublishId, ex);
                    continue;
                }
            }
        }

        /// <summary>
        /// Đồng bộ hồ sơ với hồ sơ online.
        /// - Đồng bộ các yêu cẩu bổ sung.
        /// - Đồng bộ file và giấy tờ.
        /// - Đồng bộ thanh toán.
        /// </summary>
        private void SyncOnlineDocument()
        {
            _documentService = DependencyResolver.Current.GetService<DocumentBll>();

            var processingOnlineDocuments =
                    _documentService.Gets(false, d => d.Original == 1
                        && d.CategoryBusinessId == 4
                        && (d.Status == (int)DocumentStatus.DungXuLy));

            if (!processingOnlineDocuments.Any())
            {
                //  _documentService.Dispose();
                return;
            }

            _supplementaryService = DependencyResolver.Current.GetService<SupplementaryBll>();
            _commentService = DependencyResolver.Current.GetService<CommentBll>();
            _attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
            _documentCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _mailHelper = DependencyResolver.Current.GetService<SendEmailHelper>();
            _smsHelper = DependencyResolver.Current.GetService<SendSmsHelper>();
            _userService = DependencyResolver.Current.GetService<UserBll>();

            foreach (var document in processingOnlineDocuments)
            {
                try
                {
                    var supplementary = _supplementaryService.GetNotReceived(document.DocumentId);

                    if (supplementary == null)
                    {
                        continue;
                    }

                    var lastUpdateDate = supplementary.DateOnlineUpdate ?? supplementary.DateSend;
                    var documentOnline = GetDocumentDetail(document.DocumentId);
                    if (documentOnline == null)
                    {
                        continue;
                    }

                    var onlineSupplementary = documentOnline.Supplementaries.SingleOrDefault(s => s.LocalId == supplementary.SupplementaryId);
                    var onlinePayment = documentOnline.DocumentPayments.SingleOrDefault(s => s.LocalId == supplementary.SupplementaryId);

                    var isSupplementaried = onlineSupplementary != null && onlineSupplementary.IsSuccess && onlineSupplementary.DateReceived > lastUpdateDate;
                    var isPaid = onlinePayment != null && onlinePayment.Status == 0 && onlinePayment.TransactionDate > lastUpdateDate;

                    if ((!supplementary.Papers.Equals("[]") && !isSupplementaried) ||
                        (!supplementary.Fees.Equals("[]") && !isPaid))
                    {
                        continue;
                    }

                    if (!isSupplementaried && !isPaid)
                    {
                        continue;
                    }

                    var commentBuilder = new StringBuilder();
                    commentBuilder.AppendLine("Công dân đã cập trực trực tuyến hồ sơ, Nội dung bổ sung:");

                    if (isSupplementaried)
                    {
                        var onlinePapers = Json2.ParseAs<IEnumerable<Paper>>(onlineSupplementary.Details);
                        commentBuilder.AppendLine("- Giấy tờ:            " + string.Join(",", onlinePapers.Select(p => p.PaperName)));
                    }

                    if (isPaid)
                    {
                        var paymentDetail = Json2.ParseAs<DomesticPaymentResponse>(onlinePayment.TransactionInfo);
                        if (paymentDetail != null)
                        {
                            var price = onlinePayment.PaymentFee.ToString("N0", System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN"));
                            commentBuilder.AppendLine(
                                string.Format("- Đã thanh toán:      {0} đ, Mã giao dịch: {1}, Ngày giao dịch: {2}",
                                    price, paymentDetail.TransactionNo, onlinePayment.TransactionDate.Value.ToString("hh:mm dd/MM/yyyy")));
                        }
                    }

                    var supplementFiles = documentOnline.Files.Where(f => f.CreatedOnDate > lastUpdateDate);
                    if (supplementFiles.Any())
                    {
                        var index = 0;
                        // Lưu file đính kèm
                        var newAttachments = new Dictionary<string, IDictionary<string, string>>();

                        foreach (var file in supplementFiles)
                        {
                            long leng = 0;
                            var stream = GetFileOnlines(file.FileId, out leng);
                            var fileName = file.FileName;
                            if (stream != null)
                            {
                                var temp = new Dictionary<string, string>();
                                var tempPath = ResourceLocation.Default.FileUploadTemp;
                                var tempFile = FileManager.Default.Create(stream, tempPath, fileName);
                                temp.Add("name" + index, fileName);
                                if (newAttachments.ContainsKey(fileName))
                                {
                                    file.FileName = fileName + "(" + index + ")";
                                }
                                newAttachments.Add(fileName, temp);
                            }
                            index++;
                        }
                        if (newAttachments.Any())
                        {
                            var attachments = _attachmentService.AddAttachmentInDoc(newAttachments, document.UserCreatedId, deleteTempfiles: true);
                            foreach (var newAttachment in attachments)
                            {
                                document.Attachments.Add(newAttachment);
                            }

                            _documentService.Update(document);

                            commentBuilder.AppendLine("- Tệp đính kèm:       " + string.Join(",", supplementFiles.Select(f => f.FileName)));
                        }
                    }

                    var comment = commentBuilder.ToString();
                    if (!string.IsNullOrEmpty(comment))
                    {
                        _commentService.SendComment(supplementary.DocumentCopyId.Value, 0, comment, DateTime.Now, "Tiếp nhận bổ sung trực tuyến");

                        supplementary.DateOnlineUpdate = DateTime.Now;
                        supplementary.CommentReceived = comment;
                        _supplementaryService.Update(supplementary);

                        var documentCopy = _documentCopyService.GetMain(document.DocumentId);
                        _documentCopyService.SetCurrentUserUnread(documentCopy);

                        // Send email and sms
                        try
                        {
                            var userSend = _userService.GetFromCache(documentCopy.UserCurrentId);
                            if (userSend != null)
                            {
                                _smsHelper.CreateQueueSms(userSend.Phone, string.Format("Ho so {0} da duoc bo sung truc tuyen, vui long kiem tra lai.", document.DocCode), userSend, document.DocumentId, documentCopyId: null, type: null);
                                _mailHelper.CreateQueueMail("Thông báo Công dân bổ sung hồ sơ trực tuyến",
                                    new List<string>() { userSend.Email }, comment, userSend, document.DocumentId);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogException(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    continue;
                }
            }

            //_documentService.Dispose();
            //_supplementaryService.Dispose();
            //_commentService.Dispose();
            //_attachmentService.Dispose();
            //_documentCopyService.Dispose();
            //_userService.Dispose();
        }

        private string GetNotifyContent(int NotificationType, string Compendium)
        {
            string result = string.Empty;
            string format = string.Empty;
            switch (NotificationType)
            {
                case (int)eGovNotificationTypes.eGovXuLyChinh:
                    format = _resourceService.GetResource("Common.Notify.eGovXuLyChinh");
                    break;
                case (int)eGovNotificationTypes.eGovDongXuLy:
                    format = _resourceService.GetResource("Common.Notify.eGovDongXuLy");
                    break;
                case (int)eGovNotificationTypes.eGovThongBao:
                    format = _resourceService.GetResource("Common.Notify.eGovThongBao");
                    break;
                case (int)eGovNotificationTypes.eGovXinYKien:
                    format = _resourceService.GetResource("Common.Notify.eGovXinYKien");
                    break;
                case (int)eGovNotificationTypes.eGovKetThuc:
                    format = _resourceService.GetResource("Common.Notify.eGovKetThuc");
                    break;
                case (int)eGovNotificationTypes.eGovXinGiaHan:
                    format = _resourceService.GetResource("Common.Notify.eGovXinGiaHan");
                    break;
                default:
                    format = _resourceService.GetResource("Common.Notify.Default");
                    break;
            }

            result = string.Format(format, Compendium);
            return result;
        }

        /// <summary>
        /// Time job tự động reset lại toàn bộ nhảy số của hệ thống
        /// </summary>
        private void ResetIncreate()
        {
            try
            {
                _increaService = DependencyResolver.Current.GetService<IncreaseBll>();
                var allIncreases = _increaService.Gets(isReadOnly: false);
                if (allIncreases != null && allIncreases.Any())
                {
                    using (var trans = new TransactionScope(TransactionScopeOption.Required))
                    {
                        _increaService.ResetIncrease(allIncreases);
                        trans.Complete();
                    }
                }
            }
            finally
            {
                // _increaService.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void TestMail()
        {
            SendMail();
        }

        /// <summary>
        /// Gui mail tu hang doi
        /// </summary>
        private void SendMail()
        {
            try
            {
                _mailService = DependencyResolver.Current.GetService<MailBll>();
                _attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
                _mailHelper = DependencyResolver.Current.GetService<SendEmailHelper>();

                var mailers = _mailService.Gets(p => !p.IsSent && !string.IsNullOrEmpty(p.SendTo));

                if (mailers == null || !mailers.Any())
                {
                    return;
                }
                foreach (var mail in mailers)
                {
                    try
                    {
                        var isAttach = string.IsNullOrEmpty(mail.AttachmentIdStr);
                        List<System.Net.Mail.Attachment> listAttach = new List<System.Net.Mail.Attachment>();
                        //if (!isAttach)
                        //{
                        //    var attachmentidStr = mail.AttachmentIdStr.Split(';');
                        //    var attachmentids = attachmentidStr.Select(s => Convert.ToInt32(s)).ToList();
                        //    if (attachmentids.Any())
                        //    {
                        //        var attachs = _attachmentService.GetAttachments(attachmentids);
                        //        foreach (var attach in attachs)
                        //        {
                        //            var reportAttachment = new System.Net.Mail.Attachment(attach.Value, attach.Key);
                        //            listAttach.Add(reportAttachment);
                        //        }
                        //    }
                        //}
                        if (!isAttach)
                        {
                            listAttach = GetAttachments(mail.AttachmentIdStr);
                        }

                        var receives = mail.SendTo.Split(',');
                        if (!receives.Any())
                        {
                            continue;
                        }

                        foreach (var receive in receives)
                        {
                            _mailHelper.Send(mail.Subject, mail.Body,
                                receive, attachments: listAttach);
                        }

                        var dateSend = DateTime.Now;
                        mail.DateSend = dateSend;
                        mail.IsSent = true;
                        _mailService.Update(mail);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                        continue;
                    }
                }
            }
            finally
            {
                //_mailService.Dispose();
                //_mailHelper.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachmentIds"></param>
        /// <returns></returns>
        private List<System.Net.Mail.Attachment> GetAttachments(string attachmentIds)
        {
            var result = new List<System.Net.Mail.Attachment>();
            if (string.IsNullOrEmpty(attachmentIds))
            {
                return result;
            }

            var attachmentPaths = Json2.ParseAs<Dictionary<string, string>>(attachmentIds);
            if (attachmentPaths.Count == 0)
            {
                return result;
            }

            foreach (var att in attachmentPaths)
            {
                if (!System.IO.File.Exists(att.Value))
                {
                    continue;
                }

                var stream = new System.IO.FileStream(att.Value, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                result.Add(new System.Net.Mail.Attachment(stream, att.Key));
            }

            return result;
        }


        /// <summary>
        /// Kiểm tra các hồ sơ quá hạn để gửi tin nhắn
        /// </summary>
        private void SendDocumentOverdueSms()
        {
            // Lấy ra những hồ sơ đến 17h ngày hôm trước khi hết hạn mà chưa có kết quả xử lý.
            var now = DateTime.Now;
            if (now.Hour < 11)
            {
                return;
            }

            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
            var dateAppointToCompare = now.AddDays(1);

            var documents = _documentService.Gets(true, d =>
                                    (d.Status == (int)DocumentStatus.DangXuLy || d.Status == (int)DocumentStatus.DungXuLy)
                                    && d.DateAppointed.HasValue
                                    && d.ExpireProcess > 2
                                    && !d.IsSuccess.HasValue
                                    && !d.IsReturned.HasValue
                                    && d.Phone != ""
                                    && d.Phone != null
                                    && d.DateAppointed.Value <= dateAppointToCompare);

            if (!documents.Any())
            {
                return;
            }

            _smsHelper = DependencyResolver.Current.GetService<SendSmsHelper>();
            foreach (var document in documents)
            {
                try
                {
                    if (_smsHelper.ValidPhoneNumber(document.Phone))
                    {
                        continue;
                    }

                    _smsHelper.SendDocumentOverdue(document, null, 0);
                }
                catch
                {
                    continue;
                }
            }

            //_smsHelper.Dispose();
            //_documentService.Dispose();
        }

        /// <summary>
        /// Gủi tin nhắn từ hàng đợi
        /// </summary>
        private void SendSms()
        {
            var _smsSettings = DependencyResolver.Current.GetService<SmsSettings>();
            if (string.IsNullOrEmpty(_smsSettings.ServiceUrl))
            {
                return;
            }

            var _smsService = DependencyResolver.Current.GetService<SmsBll>();
            try
            {
                var fromDate = DateTime.Now.Subtract(TimeSpan.FromDays(2));
                var listSms = _smsService.GetPendings();
                if (listSms == null || !listSms.Any())
                {
                    return;
                }

                var smsClient = GetServiceSMS();
                if (smsClient == null)
                {
                    return;
                }

                var serviceLogs = new List<string>();
                foreach (var sms in listSms)
                {
                    try
                    {
                        var phone = sms.PhoneNumber;
#if DEBUG
                        phone = "0912342342"; //"0914252584"; // "0973909395"; // "0919287970"; // "0914252584";
#endif
                        if (!phone.IsMobilePhoneNumber())
                        {
                            sms.HasSendFail = true;
                            _smsService.Update(sms);
                            continue;
                        }

                        var isSent = smsClient.SendSms(phone, sms.Message);

                        if (isSent)
                        {
                            sms.DateSend = DateTime.Now;
                            sms.IsSent = true;
                        }
                        else
                        {
                            sms.CountSendFail++;

                            if (sms.CountSendFail == 5)
                            {
                                sms.HasSendFail = true;
                            }
                        }

                        _smsService.Update(sms);

                        // Log gọi service ra bên ngoài
                        serviceLogs.Add(string.Format("[Sms] - {0}: {1} - {2}", DateTime.Now.ToString("hh:mm:ss ddMMYYYY"), sms.PhoneNumber, sms.Message));
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }
                }

                if (serviceLogs.Any())
                {
                    LogService(serviceLogs);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            finally
            {
                // _smsService.Dispose();
            }

            try
            {
                SendDocumentOverdueSms();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TestSendSms(string phone, string message)
        {
            var smsClient = GetServiceSMS();
            try
            {
                var isSent = smsClient.SendSms(phone, message);
                return true;
            }
            catch
            {
                return false;
                // LogException(ex);
            }
        }

        private ISmsServiceHelper GetServiceSMS()
        {
            var _smsSettings = DependencyResolver.Current.GetService<SmsSettings>();
            ISmsServiceHelper smsClient = null;
            var smsVendor = _smsSettings.SmsVendor;
            var servicePass = EncryptionHelper.Decrypt(_smsSettings.ServicePass);
            switch (smsVendor)
            {
                case "VTDD":
                    smsClient = new VTDDSmsService(
                        _smsSettings.ServiceUser,
                        servicePass,
                        _smsSettings.ServiceCode,
                        _smsSettings.Alias);
                    break;
                case "Bkav":
                    break;
                case "Vnpt":
                    smsClient = new VnptSmsService(
                      _smsSettings.ServiceUser,
                      servicePass,
                      _smsSettings.ServiceCode,
                      _smsSettings.Alias);
                    break;
                case "Modem":
                    break;
                case "Viettel":
                    smsClient = new ViettelSmsService(
                        _smsSettings.ServiceUrl,
                        _smsSettings.ServiceUser,
                        servicePass,
                        _smsSettings.ServiceCode,
                        _smsSettings.Alias
                        );
                    break;
                default:
                    break;
            }

            if (smsClient == null)
            {
                smsClient = new ViettelSmsService(_smsSettings.ServiceUrl,
                        _smsSettings.ServiceUser,
                        servicePass,
                        _smsSettings.ServiceCode,
                        _smsSettings.Alias
                        );
            }

            return smsClient;
        }

        /// <summary>
        /// Lấy chi tiết hồ sơ đăng ký qua mạng
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        private DocumentOnline GetDocumentDetail(Guid docId)
        {
            var results = string.Empty;

            var _onlineRegistrationSettings = DependencyResolver.Current.GetService<OnlineRegistrationSettings>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/DocumentDetailForUpdate?eGovDocumentId=" + docId;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    results = response.Content.ReadAsStringAsync().Result as string;
                }
            }

            return Json2.ParseAs<DocumentOnline>(results);
        }

        /// <summary>
        /// Loại bỏ ký tự "/" cuối trên url
        /// </summary>
        /// <param name="apiUrl">url truyền vào</param>
        /// <returns></returns>
        private string GetApiUrl(string apiUrl)
        {
            if (string.IsNullOrWhiteSpace(apiUrl))
                return null;

            if (apiUrl.EndsWith("/", StringComparison.InvariantCultureIgnoreCase))
            {
                apiUrl = apiUrl.Substring(0, apiUrl.LastIndexOf("/"));
            }
            return apiUrl;
        }

        /// <summary>
        /// Trả về nội dung file từ eGov Online
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="fileLength"></param>
        /// <returns></returns>
        private Stream GetFileOnlines(int fileId, out long fileLength)
        {
            var _onlineRegistrationSettings = DependencyResolver.Current.GetService<OnlineRegistrationSettings>();

            var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/Download/" + fileId;
            var fileReq = (HttpWebRequest)HttpWebRequest.Create(url);
            var fileResp = (HttpWebResponse)fileReq.GetResponse();
            fileLength = fileResp.ContentLength;
            return fileResp.GetResponseStream();
        }

        /// <summary>
        /// Kiểm tra có mail mới
        /// </summary>
        private void CheckNewMail()
        {
            try
            {
                var _notificationSettings = DependencyResolver.Current.GetService<NotificationSettings>();
                if (!_notificationSettings.MailActived)
                {
                    return;
                }

                var _connectionSettings = DependencyResolver.Current.GetService<ConnectionSettings>();
                if (string.IsNullOrWhiteSpace(_connectionSettings.BmailLink) || _connectionSettings.MailType == 1)
                {
                    return;
                }

                var _mobileDeviceSerice = DependencyResolver.Current.GetService<MobileDeviceBll>();
                var devices = _mobileDeviceSerice.Gets(x => x.IsActive == true);
                if (!devices.Any())
                {
                    return;
                }

                _userService = DependencyResolver.Current.GetService<UserBll>();
                var _notificationService = DependencyResolver.Current.GetService<NotificationBll>();

                foreach (var device in devices)
                {
                    var user = _userService.CurrentUser;
                    // var notifyType = _notificationSettings.MailNotifyType;
                    var notifyInfo = _userService.GetUserNotifyInfo(user.NotifyInfo);

                    if (notifyInfo == null || string.IsNullOrWhiteSpace(notifyInfo.MailLastestToken))
                    {
                        continue;
                    }

                    // notifyType = notifyInfo.BmailNotifyType;
                    var bmailNotificationHelper = new BmailNotification(user, _connectionSettings.BmailLink);
                    var results = bmailNotificationHelper.GetLastestMail(notifyInfo.MailLastestToken, notifyInfo.MailFolderNotify);

                    if (results.Any())
                    {
                        _notificationService.Create(results);
                    }
                }

                //_mobileDeviceSerice.Dispose();
                //_notificationService.Dispose();
            }
            catch (Exception ex)
            {
                // _userService.Dispose();
                LogException(ex);
            }
        }

        /// <summary>
        /// Gửi cảnh báo xử lý hồ sơ, văn bản
        /// </summary>
        private void SendWarning()
        {
            var _warningSettings = DependencyResolver.Current.GetService<WarningSettings>();

            if (!_warningSettings.IsActive)
            {
                return;
            }

            if (_warningSettings.NumberToWarning < 1)
            {
                return;
            }

            var template = GetOverdueTemplateMail();
            if (string.IsNullOrEmpty(template))
            {
                _logService.Error("Không lấy được mẫu template cho gửi cảnh báo.");
                return;
            }

            var statisticService = DependencyResolver.Current.GetService<StatisticBll>();
            var from = DateTime.Now.AddYears(-2);
            var docOverdues = statisticService.GetDocumentOverdues(false, from, DateTime.Now, hasHsmc: true, hasXlvb: true);

            // Loại bỏ các hồ sơ quá hạn của người được cấu hình bỏ qua cảnh báo
            if (_warningSettings.UserToIgnores.Any())
            {
                docOverdues = docOverdues.Where(d => !_warningSettings.UserToIgnores.Contains(d.UserCurrentId));
            }

            var numbOfOverdues = docOverdues.Count();

            // Gửi cảnh báo khi số hồ sơ xử lý quá hạn vượt quá định mức cho phép
            if (numbOfOverdues > _warningSettings.NumberToWarning)
            {
                try
                {
                    var groups = docOverdues.GroupBy(d => d.CurrentDepartmentName).OrderBy(g => g.Key).ToList();
                    SendMailOverdue(groups, _warningSettings.SentMailOverdueToes, template, _warningSettings.Subject);
                }
                catch
                {

                }
            }

            // Gửi cảnh báo cho các phòng ban có hồ sơ quá hạn
            if (_warningSettings.HasSentToDepartment)
            {
                try
                {
                    SendMailOverdueToDepartment(docOverdues, template);
                }
                catch
                {

                }
            }

            // Gửi cảnh báo tới người dùng có văn bản xử lý quá hạn
            if (_warningSettings.HasSentToInfringer)
            {
                try
                {
                    SendMailOverdueToInfringer(docOverdues, template, _warningSettings.Subject);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SendReportToDVCService()
        {
            string organizationId = "562";
            string authenticationId = "3C31DB90-10BB-186E-8708-06B2F1672690";
            //Lấy tất cả các id lĩnh vực
            string[] allIdLinhVuc = {"176","219","177","175","1","2","3","4","5","6","7","GD-ĐT","VSATTP-DD",
                                  "VT-INTER","TS","10","12","13","14","15","9","17","H0034","TV001","GD001","Q003",
                                  "DT004","CN004","0004","00010","0011","00041","0045","0122","1234","01245","2345",
                                  "21345","24561","4523","1456","4521","4561","2489","004","123456"};
            string[] allNameLinhVuc = {"Hộ tịch","Chứng thực","Bảo trợ xã hội","Đất đai","Nhà ở và Công sở","Lưu thông hàng hóa trong nước và XNK",
                                      "Hạ tầng KT đô thị, KCN, KKT, KCNC","Môi trường","Tôn giáo","Xây dựng","Bồi thường nhà nước","Giáo dục và Đào tạo",
                                      "An toàn thực phẩm và dinh dưỡng","Viễn thông và Internet","Thủy sản","Giải quyết khiếu nại",
                                      "Tiếp công dân","Xử lý đơn","Tài nguyên nước","Thi đua khen thưởng","Đầu tư","Văn hóa cơ sở",
                                      "Phòng chống tham nhũng","Thư viện","Gia đình","QLNN về quỹ xã hội, quỹ từ thiện","Đường thủy nội địa",
                                      "Công nghiệp tiêu dùng","Lao động – Tiền lương – Quan hệ lao động","Xuất Bản","Thành Lập và Hoạt Động HKD",
                                      "Thành lập và hoạt động Hợp Tác Xã","Đấu Thầu","Người có công","Hoạt động KH&CN","Hội","TCHC, đơn vị sự nghiệp công lập",
                                      "Đăng ký giao dịch bảo đảm","Phổ biến giáo dục pháp luật","Hòa giải cơ sở","Phòng chống tệ nạn xã hội","ĐT trong nước sử dụng vốn nhà nước",
                                      "Phát triển nông thôn","Quy hoạch xây dựng","VPĐK Đất Đai","Quy Hoạch - Kiến Trúc"};
            var now = DateTime.Now;
            //Hiện tại chưa sử dụng thằng này do chưa có dữ liệu;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //var startOfMonth = new DateTime(2016, 1, 1);
            //var endOfMonth = new DateTime(2016, 12, 31);
            var services = new DVCService.DVCServiceHoSoSoapClient();
            var listbc = GetProgressStatisticDetail(true, false, true, firstDayOfMonth, lastDayOfMonth);
            //foreach (var baocao in listbc)
            //{
            //    if (baocao.DocFieldIds != null)
            //    {
            //        var str = "";
            //        int docfieldId = Convert.ToInt32(baocao.DocFieldIds.Replace(";", ""));
            //        string docFieldName = _docfieldService.GetDocFieldName(docfieldId);
            //        for (int i = 0; i < allNameLinhVuc.Length; i++)
            //        {
            //            if (docFieldName.Equals(allNameLinhVuc[i]))
            //            {
            //                string sohstiepnhan = baocao.NewReception.ToString();
            //                string sohsdaxuly = baocao.TotalSolved.ToString();
            //                string sohsdunghan = baocao.SolvedInTime.ToString();
            //                string idLinhVuc = allIdLinhVuc[i];
            //                var status = services.CapNhatTinhHinhGiaiQuyetHoSoCapHuyen(organizationId, authenticationId,
            //                    idLinhVuc, firstDayOfMonth.Year.ToString(), firstDayOfMonth.Month.ToString(), sohstiepnhan,
            //                    sohsdaxuly, sohsdunghan);
            //                str = str + docFieldName + "--" +  status;
            //            }
            //        }
            //        Resource a = new Resource();
            //        a.ResourceKey = string.Format("test.dvc.status{0}", DateTime.Now.ToString());
            //        a.ResourceValue = str;
            //        _resourceService.Create(a);
            //    }
            //}
            var str = "";
            var abc = "";
            for (int i = 0; i < allNameLinhVuc.Length; i++)
            {
                string sohstiepnhan = "0";
                string sohsdaxuly = "0";
                string sohsdunghan = "0";
                string idLinhVuc = allIdLinhVuc[i];
                foreach (var baocao in listbc)
                {
                    if (baocao.DocFieldIds != null)
                    {

                        int docfieldId = Convert.ToInt32(baocao.DocFieldIds.Replace(";", ""));
                        string docFieldName = _docfieldService.GetDocFieldName(docfieldId);

                        if (docFieldName.Equals(allNameLinhVuc[i]))
                        {
                            abc = abc + "," + Newtonsoft.Json.JsonConvert.SerializeObject(baocao);
                            sohstiepnhan = baocao.NewReception.ToString();
                            sohsdaxuly = baocao.TotalSolved.ToString();
                            sohsdunghan = baocao.SolvedInTime.ToString();
                        }

                        string result = services.CapNhatTinhHinhGiaiQuyetHoSoCapHuyen(organizationId, authenticationId,
                               idLinhVuc, firstDayOfMonth.Year.ToString(), firstDayOfMonth.Month.ToString(), sohstiepnhan,
                               sohsdaxuly, sohsdunghan);
                        str = str + allIdLinhVuc[i] + "--" + result + ";";

                    }

                }
            }
            Resource a = new Resource();
            a.ResourceKey = string.Format("test.dvc.status{0}", DateTime.Now.ToString());
            a.ResourceValue = str;

            Resource b = new Resource();
            b.ResourceKey = string.Format("test.dvc.status.dl{0}", DateTime.Now.ToString());
            b.ResourceValue = abc;
            _resourceService.Create(b);
        }

        /// <summary>
        /// Trả về kết quả thống kê tình trạng xử lý hồ sơ chi tiết của cơ quan
        /// </summary>
        /// <param name="hasOldDocument">Lấy danh sách hồ sơ từ kỳ trước</param>
        /// <param name="hasHsmc">Lấy hồ sơ một cửa</param>
        /// <param name="hasXlvb">Lấy văn bản</param>
        /// <param name="from">Thời gian bắt đầu lấy thống kê</param>
        /// <param name="to">Thời gian kết thúc lấy thống kê</param>
        /// <returns></returns>
        public IEnumerable<ProgressStatisticDVC> GetProgressStatisticDetail(bool hasOldDocument, bool hasXlvb, bool hasHsmc, DateTime from, DateTime to)
        {
            var result = new List<ProgressStatisticDVC>();
            var docs = GetsStatisticObjectDVC(hasOldDocument, hasXlvb, hasHsmc, from, to);
            if (!docs.Any())
            {
                return result;
            }

            var groups = docs.GroupBy(d => d.DocFieldIds);

            result.AddRange(groups.Select(g =>
            {
                var groupDocs = g;
                var newResultItem = ParseStatistic(groupDocs, from, to);
                newResultItem.DocFieldIds = g.Key;
                return newResultItem;
            }).ToList());

            return result;
        }

        private ProgressStatisticDVC ParseStatistic(IEnumerable<StatisticObjectDVC> docs, DateTime from, DateTime to)
        {
            var result = new ProgressStatisticDVC();
            if (!docs.Any())
            {
                return result;
            }

            // Tổng xử lý
            //result.Total = docs.Count();

            // Nhận trong kỳ
            result.NewReception = docs.Count(d => d.DateCreated >= from && d.DateCreated <= to);

            // Tồn kỳ trước
            //result.PreExtisting = result.Total - result.NewReception;

            // Đã giải quyết đúng hạn
            result.SolvedInTime = docs.Where(d => d.OverdueStatusType == OverdueStatusType.ResolveInTime).Count();

            // Đã giải quyết trễ hẹn
            result.SolvedLate = docs.Count(d => d.OverdueStatusType == OverdueStatusType.ResolveLate);

            // Tổng đã xử lý
            result.TotalSolved = result.SolvedInTime + result.SolvedLate;

            return result;
        }

        private IEnumerable<StatisticObjectDVC> GetsStatisticObjectDVC(bool hasOldDocument, bool hasXlvb, bool hasHsmc, DateTime from, DateTime to)
        {
            var fromYear = from.Year;
            var toYear = to.Year;
            var year = toYear;

            var documentInYears = GetDocuments(year);

            if (hasOldDocument && from.Month == 1 && from.Day == 1)
            {
                // Lấy dữ liệu 2 năm gần nhất.
                documentInYears = documentInYears.Concat(GetDocuments(year - 1));
            }

            if (!hasHsmc)
            {
                documentInYears = documentInYears.Where(d => d.CategoryBusinessId == (int)CategoryBusinessTypes.VbDen || d.CategoryBusinessId == (int)CategoryBusinessTypes.VbDi);
            }

            if (!hasXlvb)
            {
                documentInYears = documentInYears.Where(d => d.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc && d.DateAppointed.HasValue
                                                                && d.DocumentCopyType != (int)DocumentCopyTypes.DongXuLy);
            }

            var result = new List<StatisticObjectDVC>();
            result.AddRange(documentInYears.Where(d => d.DateCreated >= from && d.DateCreated <= to));

            if (hasOldDocument)
            {
                result.AddRange(GetPreExistingDocuments(documentInYears, from));
            }

            foreach (var doc in result)
            {
                var docObject = TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(doc, to);

                doc.OverdueStatusType = StatisticUtil.OverdueStatus(docObject.Status, docObject.DateAppointed ?? DateTime.Now, docObject.DateSuccess, docObject.DateReturned,
                                                docObject.DateFinished, docObject.DateRequireSupplementary, to, doc.DocCode);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private IEnumerable<StatisticObjectDVC> GetDocuments(int year)
        {
            var ignoreUser = 0;

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@year", year));
            parameters.Add(new SqlParameter("@userId", ignoreUser == 0 ? 0 : ignoreUser));

            var docs = Context.RawProcedure("statisticDVC", parameters.ToArray());
            var a = Json2.Stringify(docs);
            var result = Json2.ParseAs<IEnumerable<StatisticObjectDVC>>(a);
            return result;
        }

        /// <summary>
        /// Tính toán lại các khoảng thời gian xử lý và trạng thái văn bản , hồ sơ ở thời điểm TO
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private StatisticObjectDVC TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(StatisticObjectDVC doc, DateTime to)
        {
            var result = new StatisticObjectDVC();

            result.Status = doc.Status;
            result.DateAppointed = doc.DateAppointed ?? DateTime.Now;
            result.DateSuccess = doc.DateSuccess;
            result.DateReturned = doc.DateReturned;
            result.DateFinished = doc.DateFinished;
            result.DateRequireSupplementary = doc.DateRequireSupplementary;

            if (doc.DateSuccess.HasValue && doc.DateSuccess.Value > to)
            {
                result.DateSuccess = null;
                result.IsSuccess = null;
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

        /// <summary>
        /// Trả về danh sách hồ sơ tồn kỳ trước: chưa được xử lý ở kỳ trước
        /// </summary>
        /// <param name="cachedDocs"></param>
        /// <param name="to">Mốc thời gian</param>
        /// <returns></returns>
        private IEnumerable<StatisticObjectDVC> GetPreExistingDocuments(IEnumerable<StatisticObjectDVC> cachedDocs, DateTime to)
        {
            var docs = cachedDocs.Where(d => d.DateCreated < to);
            var result = new List<StatisticObjectDVC>();

            foreach (var doc in docs)
            {
                var docObject = TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(doc, to);

                var overdueStatusType = StatisticUtil.OverdueStatus(docObject.Status, docObject.DateAppointed.Value, docObject.DateSuccess, docObject.DateReturned,
                                                docObject.DateFinished, docObject.DateRequireSupplementary, to, doc.DocCode);
                if (overdueStatusType == OverdueStatusType.ResolveInTime || overdueStatusType == OverdueStatusType.ResolveLate)
                {
                    result.Add(doc);
                }
            }

            return result;
        }

        private void SendMailOverdueToInfringer(IEnumerable<DocumentOverdue> docOverdues, string template, string subject)
        {
            var groups = docOverdues.GroupBy(d => d.UserCurrentId);
            _userService = DependencyResolver.Current.GetService<UserBll>();
            var users = _userService.GetAllCached();
            foreach (var group in groups)
            {
                var userId = group.Key;
                var user = users.SingleOrDefault(u => u.UserId == userId);
                if (user == null || string.IsNullOrEmpty(user.Email))
                {
                    continue;
                }

                var userEmail = new List<string>() { user.Email };

                var model = new Dictionary<string, Dictionary<string, IEnumerable<DocumentOverdue>>>();
                var data = new Dictionary<string, IEnumerable<DocumentOverdue>>();
                var docGroups = group.GroupBy(d => d.UserCurrentName);
                foreach (var doc in docGroups)
                {
                    data.Add(doc.Key, doc.ToList());
                }
                model.Add(user.Username, data);
                var body = ParseTemplate(template, model);
                _mailHelper = DependencyResolver.Current.GetService<SendEmailHelper>();
                _mailHelper.CreateQueueMail(subject, userEmail, body, null);
            }

            _userService.Dispose();
        }

        private void SendMailOverdueToDepartment(IEnumerable<DocumentOverdue> docOverdues, string template)
        {
            throw new NotImplementedException();
        }

        private void SendMailOverdue(IEnumerable<IGrouping<string, DocumentOverdue>> groups, List<string> mailToList, string template, string subject)
        {
            var model = new Dictionary<string, Dictionary<string, IEnumerable<DocumentOverdue>>>();

            foreach (var group in groups)
            {
                var data = new Dictionary<string, IEnumerable<DocumentOverdue>>();
                var docGroups = group.GroupBy(d => d.UserCurrentName);
                foreach (var doc in docGroups)
                {
                    data.Add(doc.Key, doc.ToList());
                }

                model.Add(group.Key, data);
            }

            template = ParseTemplate(template, model);

            _mailHelper = DependencyResolver.Current.GetService<SendEmailHelper>();
            _mailHelper.CreateQueueMail(subject, mailToList, template, null);
        }

        private string GetOverdueTemplateMail()
        {
            var tempPath = CommonHelper.MapPath("~/MailTemplate/OverdueTemplate.cshtml");
            var result = "";
            if (System.IO.File.Exists(tempPath))
            {
                try
                {
                    result = System.IO.File.ReadAllText(tempPath, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    _logService.Error("Không lấy được mẫu template cho gửi cảnh báo.", ex);
                }
            }
            return result;
        }

        private string ParseTemplate(string template, Dictionary<string, Dictionary<string, IEnumerable<DocumentOverdue>>> model)
        {
            return RazorEngineUtil.Bind(template, model);
        }

        private void LogService(List<string> message)
        {
            var logFolder = CommonHelper.MapPath("~/Logs");
            var logFile = Path.Combine(logFolder, "logservice_" + DateTime.Now.ToString("ddMMyyyy"));
            try
            {
                System.IO.File.AppendAllLines(logFile, message);
            }
            catch { }
        }

        #endregion
    }
}
