#region

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Statistic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;

#endregion

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    ///   <para> BSO - Phòng 2 - eGov </para>
    ///   <para> Project: eGov Cloud - v1.0 </para>
    ///   <para> [Access Level(Class/Struct/Interface)] : DocumentCopyBll - public - BLL </para>
    ///   <para> Access Modifiers: * Inherit : [Class Name] * Implement : [Inteface Name], [Inteface Name], ... </para>
    ///   <para> Create Date : 121225 </para>
    ///   <para> Author : TienBV </para>
    /// </author>
    /// <summary>
    ///   <para>Quản lý bảng lưu vết. </para>
    /// </summary>
    /// <remarks>
    /// Tác động tới DocumentCopy trong các tình huống sau:
    /// - Tạo mới văn bản/hồ sơ + Bàn giao văn bản hồ sơ (DocumentCopyTypes)
    /// - Xác nhận xử lý
    /// - Loại bỏ văn bản
    /// - Lấy lại văn bản
    /// - Cập nhật lại ngày hẹn trả khi dừng xử lý???
    /// - Dừng xử lý
    /// - Kết thúc xử lý
    /// - Khi phân loại
    /// </remarks>
    public class DocumentCopyBll : ServiceBase
    {
        #region Readonly & Static Fields

        private const DocumentCopyTypes SpecialDocumentCopyType = DocumentCopyTypes.XinYKien | DocumentCopyTypes.ThongBao |
                                                            DocumentCopyTypes.DuyetGiaHan;

        private const DocumentCopyTypes NormalDocumentCopyType = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy |
                                                            DocumentCopyTypes.ChoKetQuaDungXuLy;

        private readonly DocFinishBll _docFinishService;
        private readonly DocTimelineBll _docTimelineService;
        private readonly IRepository<DocumentCopy> _documentCopyRepository;
        private readonly UserBll _userService;
        private readonly CommentBll _commentService;
        private readonly IRepository<DocRelation> _docRelationRepository;
        private readonly WorkflowHelper _workflowHelper;
        private readonly CodeBll _codeService;
        private readonly WorktimeHelper _worktimeHelper;
        private readonly DocumentBll _documentService;
        private readonly WorkflowBll _workflowService;
        private readonly DocTypeBll _doctypeService;
        private readonly StoreBll _storeService;
        private readonly CategoryBll _categoryService;
        private readonly DailyProcessBll _dailyProcessService;
        private readonly DocumentPublishBll _docPublishService;
        private readonly IRepository<Supplementary> _supplementaryRepository;
        private readonly MemoryCacheManager _cacheManager;

        private readonly AdminGeneralSettings _adminSettings;

        #endregion

        #region C'tors

        /// <summary>
        ///   Khởi tạo class <see cref="DocumentCopyBll" />.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="docFinishService"> Bll tương ứng với bảng DocFinish trong CSDL </param>
        /// <param name="docTimelineService"> Bll tương ứng với bảng DocTimeline trong CSDL </param>
        /// <param name="userService"> </param>
        /// <param name="commentService"> Bll tương ứng với nghiệp vụ xử lý lịch sử ý kiến</param>
        /// <param name="workflowHelper"> </param>
        /// <param name="codeService"> </param>
        /// <param name="worktimeHelper"> </param>
        /// <param name="documentService"></param>
        /// <param name="workflowService"></param>
        /// <param name="doctypeService"></param>
        /// <param name="storeService"></param>
        /// <param name="categoryService"></param>
        /// <param name="dailyProcess"></param>
        /// <param name="docPublishService"></param>
        /// <param name="cacheManager"></param>
        /// <param name="adminSettings"></param>
        public DocumentCopyBll(
            IDbCustomerContext context,
            DocFinishBll docFinishService,
            DocTimelineBll docTimelineService,
            UserBll userService,
            CommentBll commentService,
            WorkflowHelper workflowHelper,
            CodeBll codeService,
            WorktimeHelper worktimeHelper,
            DocumentBll documentService,
            WorkflowBll workflowService,
            DocTypeBll doctypeService,
            StoreBll storeService,
            CategoryBll categoryService,
            DailyProcessBll dailyProcess,
            DocumentPublishBll docPublishService,
            MemoryCacheManager cacheManager,
            AdminGeneralSettings adminSettings)
            : base(context)
        {
            _documentCopyRepository = Context.GetRepository<DocumentCopy>();
            _docFinishService = docFinishService;
            _docTimelineService = docTimelineService;
            _userService = userService;
            _commentService = commentService;
            _docRelationRepository = Context.GetRepository<DocRelation>();
            _supplementaryRepository = Context.GetRepository<Supplementary>();
            _workflowHelper = workflowHelper;
            _codeService = codeService;
            _worktimeHelper = worktimeHelper;
            _documentService = documentService;
            _workflowService = workflowService;
            _doctypeService = doctypeService;
            _storeService = storeService;
            _dailyProcessService = dailyProcess;
            _cacheManager = cacheManager;
            _docPublishService = docPublishService;
            _adminSettings = adminSettings;
            _categoryService = categoryService;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Thay đổi người xử lý chính khi xác nhận xử lý
        /// </summary>
        /// <param name="documentCopyXlc"></param>
        /// <param name="userXlcChangeId"> </param>
        public void ChangeUserXlc(DocumentCopy documentCopyXlc, int userXlcChangeId)
        {
            //var deleteDocFinish = _docFinishService.Get(documentCopyXlc.DocumentCopyId, documentCopyXlc.UserCurrentId,
            //                                            DocFinishType.ThamGiaXuLy);

            var deleteUserXlcId = documentCopyXlc.UserCurrentId;

            documentCopyXlc.UserCurrentId = userXlcChangeId;
            // Cap nhat lai Historypath huong chinh
            var historyProcess = documentCopyXlc.Histories;
            var history1 = historyProcess.HistoryPath[historyProcess.HistoryPath.Count - 1];
            var history2 = historyProcess.HistoryPath[historyProcess.HistoryPath.Count - 2];
            history1.UserReceiveId = userXlcChangeId;
            history2.UserReceives.Single(c => c.DocumentCopyId == documentCopyXlc.DocumentCopyId).UserReceiveId = userXlcChangeId;
            documentCopyXlc.Histories = historyProcess;
            Update(documentCopyXlc);

            // DocFinish
            // Kiem tra trong lich su ban giao can bo giu van ban hien tai tung tham gia xu ly bao gio chua. Chua --> Xoa, Roi --> Giu lai
            var hasThamgiaxuly = false;
            var newHistoryProcess = documentCopyXlc.Histories;
            foreach (var history in newHistoryProcess.HistoryPath)
            {
                if (history.UserReceiveId != deleteUserXlcId && history.UserSendId != deleteUserXlcId)
                {
                    continue;
                }
                hasThamgiaxuly = true;
                break;
            }

            var removedUserXuly = new List<int>();
            if (!hasThamgiaxuly)
            {
                removedUserXuly.Add(documentCopyXlc.UserCurrentId);
            }

            UpdateUserThamGia(documentCopyXlc, new List<int>() { userXlcChangeId }, removedUserXuly);

            //_docFinishService.Create(new DocFinish
            //{
            //    DocFinishType = (int)DocFinishType.ThamGiaXuLy,
            //    DocumentCopyId = documentCopyXlc.DocumentCopyId,
            //    DocumentId = documentCopyXlc.DocumentId,
            //    UserId = userXlcChangeId,
            //    IsViewed = false
            //});
        }

        /// <summary>
        ///   <para>Tạo hướng chuyển bàn giao theo quy trình: Xử lý chính, Đồng xử lý, Chờ kết quả Dừng xử lý</para>
        ///   <para>CuongNT@bkav.com - 180813</para>
        /// </summary>
        /// <param name="documentId"> </param>
        /// <param name="docTypeId"> </param>
        /// <param name="nodeSend"> </param>
        /// <param name="userSendId"> </param>
        /// <param name="nodeReceive"> </param>
        /// <param name="userReceiveId"> </param>
        /// <param name="parentId"> </param>
        /// <param name="dateCreated"> </param>
        /// <param name="documentCopyType"> </param>
        /// <param name="documentStatus"> </param>
        /// <param name="docRelations">Danh sách các văn bản liên quan cán bộ nhận (userReceiveId) được phép xem</param>
        /// <param name="dateOverdue"></param>
        /// <returns> </returns>
        /// <remarks>
        /// Phan biet 2 truong hop:
        /// 1. Chi gui cho duy nhat chinh minh (Tiep nhan, tiep nhan va tiep tuc): Xu ly binh thuong
        /// VS
        /// 2. Gui cho nhieu nguoi (co the co ca chinh minh trong do): Xu ly nhu gui cho duy nhat chinh minh truoc, roi xu ly ban giao bt sau.
        /// </remarks>
        public DocumentCopy Create(Guid documentId, Guid docTypeId, Node nodeSend, int userSendId, Node nodeReceive,
                          int userReceiveId, int? parentId, DateTime dateCreated, DocumentCopyTypes documentCopyType,
                          DocumentStatus documentStatus, List<DocRelation> docRelations, DateTime? dateOverdue = null)
        {
            if (!NormalDocumentCopyType.HasFlag(documentCopyType))
            {
                throw new ArgumentException("documentCopyType chỉ được phép thuộc một trong các kiểu DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy | DocumentCopyTypes.ChoKetQuaDungXuLy");
            }
            if (nodeSend == null)
            {
                throw new ArgumentNullException("nodeSend");
            }
            if (nodeReceive == null)
            {
                throw new ArgumentNullException("nodeReceive");
            }
            if (documentCopyType != DocumentCopyTypes.XuLyChinh && !parentId.HasValue)
            {
                throw new ArgumentNullException("parentId");
            }

            DocumentCopy documentCopyParent = null;
            if (parentId.HasValue)
            {
                documentCopyParent = Get(parentId.Value);
            }

            #region Xử lý HistoryPath

            var historyProcess = new HistoryProcess();
            /*
             * Nếu là Khi tạo mới văn bản (hay tạo mới hướng văn bản xử lý chính) thì mặc định tạo một history là bàn giao cho chính mình,
             * phục vụ việc lấy lại và sẽ chưa thông tin List<UserReceives>() đang bàn giao hiện tại.
             */
            if (documentCopyType == DocumentCopyTypes.XuLyChinh)
            {
                historyProcess.HistoryPath.Add(new HistoryPath
                {
                    ParentId = parentId,
                    DateCreated = dateCreated,//.AddSeconds(-5) De khong trung voi thoi gian ban giao thuc te, gay history co 2 moc giong het nhau
                    UserReceiveId = userSendId,
                    UserSendId = userSendId,
                    NodeReceiveId = nodeSend.Id,
                    NodeSendId = nodeSend.Id,
                    UserReceives = new List<UserReceives>(),
                    WorkflowReceiveId = nodeSend.Parent.Id,
                    WorkflowSendId = nodeSend.Parent.Id
                });
            }
            // History tiếp theo lưu thông tin người vừa nhận văn bản như bình thường
            historyProcess.HistoryPath.Add(new HistoryPath
            {
                ParentId = parentId,
                DateCreated = dateCreated,
                UserReceiveId = userReceiveId,
                UserSendId = userSendId,
                NodeReceiveId = nodeReceive.Id,
                NodeSendId = nodeSend.Id,
                UserReceives = new List<UserReceives>(),
                WorkflowReceiveId = nodeReceive.Parent.Id,
                WorkflowSendId = nodeSend.Parent.Id
            });

            #endregion

            #region Tao moi DocumentCopy

            if (!dateOverdue.HasValue && nodeReceive.TimeInNode > 0)
            {
                var docType = _doctypeService.GetAllFromCache().SingleOrDefault(dt => dt.DocTypeId.Equals(docTypeId));
                if (docType != null && docType.HasOverdueInNode)
                {
                    dateOverdue = _worktimeHelper.GetDateAppoint(dateCreated, nodeReceive.TimeInNode / 24);
                }
            }

            var documentCopy = new DocumentCopy
            {
                ParentId = parentId,
                DocumentId = documentId,
                DocTypeId = docTypeId,
                WorkflowId = nodeReceive.Parent.Id,
                UserCurrentId = userReceiveId,
                Status = (int)documentStatus,
                History = historyProcess.StringifyJs(),
                DateCreated = dateCreated,
                DateReceived = dateCreated,
                DocumentCopyType = (int)documentCopyType,
                NodeCurrentId = nodeReceive.Id,
                NodeCurrentName = nodeReceive.NodeName,
                HasJustCreated = true,
                DateOverdue = dateOverdue,
                NodeCurrentPermission = (int)nodeReceive.GetNodePermission(),
                DocumentCopyParentPath = documentCopyParent != null
                        ? string.Format("{0}{1}\\", documentCopyParent.DocumentCopyParentPath, documentCopyParent.DocumentCopyId)
                        : ""
            };

            _documentCopyRepository.Create(documentCopy);
            Context.SaveChanges();

            #endregion

            #region Xử lý ghi danh sách các hướng nhận bàn giao

            var userReceive = new UserReceives
            {
                DocumentCopyId = documentCopy.DocumentCopyId,
                DocumentCopyType = (int)documentCopyType,
                WorkflowId = nodeReceive.Parent.Id,
                IsXlc = documentCopyType == DocumentCopyTypes.XuLyChinh,
                UserReceiveId = userReceiveId,
                DateCreated = dateCreated
            };

            var documentCopyUpdate = documentCopyType == DocumentCopyTypes.XuLyChinh ? documentCopy : documentCopyParent;
            var histories = documentCopyUpdate.Histories;
            if (histories.HistoryPath.Count > 1)
            {
                histories.HistoryPath[histories.HistoryPath.Count - 2].UserReceives.Add(userReceive);
            }
            documentCopyUpdate.Histories = histories;

            #endregion

            #region DocFinish

            Context.Configuration.AutoDetectChangesEnabled = false;

            var userThamGia = new List<int>();
            userThamGia.Add(userReceiveId);
            if (documentCopyType == DocumentCopyTypes.XuLyChinh)
            {
                userThamGia.Add(userSendId);
                //_docFinishService.Create(new DocFinish
                //{
                //    DocumentId = documentId,
                //    UserId = userSendId,
                //    DocumentCopyId = documentCopy.DocumentCopyId,
                //    // đặt false để xác định có thể lấy lại được văn bản hay không,
                //    // Nếu người nhận mở văn bản ra thì gán lại về true.
                //    IsViewed = false,
                //    DocFinishType = (int)DocFinishType.ThamGiaXuLy
                //});
            }

            //_docFinishService.Create(new DocFinish
            //{
            //    DocumentId = documentId,
            //    UserId = userReceiveId,
            //    DocumentCopyId = documentCopy.DocumentCopyId,
            //    IsViewed = false,
            //    DocFinishType = (int)DocFinishType.ThamGiaXuLy
            //});

            UpdateUserThamGia(documentCopy, userThamGia);

            #endregion

            #region DocTimeline

            if (documentCopyType == DocumentCopyTypes.XuLyChinh)
            {
                if (userSendId != userReceiveId)
                {
                    // Nếu người nhận khác người gửi thì tự tạo 1 record tự chuyển cho chính mình
                    _docTimelineService.Create(new DocTimeline
                    {
                        DocumentId = documentId,
                        DocumentCopyType = documentCopy.DocumentCopyType,
                        DocumentCopyId = documentCopy.DocumentCopyId,
                        FromDate = dateCreated,
                        IsWorkingTime = true,
                        NodeId = nodeSend.Id,
                        NodeName = nodeSend.NodeName,
                        UserId = userSendId,
                        ToDate = dateCreated,
                        ProcessedMinutes = 0,
                        IsSuccess = true,
                        UserSendId = userSendId,
                        NodeSendId = nodeSend.Id,
                        NodeSendName = nodeSend.NodeName,
                        TimeInNode = nodeSend.TimeInNode,
                        WorkFlowId = nodeSend.Parent.Id,
                        DateOverdue = dateOverdue
                    }, true);
                }
            }

            // Tạo doctimeline hướng người nhận
            _docTimelineService.Create(new DocTimeline
            {
                DocumentId = documentId,
                DocumentCopyType = documentCopy.DocumentCopyType,
                DocumentCopyId = documentCopy.DocumentCopyId,
                FromDate = dateCreated,
                IsWorkingTime = !nodeReceive.StopProcess,
                NodeId = nodeReceive.Id,
                NodeName = nodeReceive.NodeName,
                UserId = userReceiveId,
                UserSendId = userSendId,
                NodeSendId = nodeSend.Id,
                NodeSendName = nodeSend.NodeName,
                TimeInNode = nodeReceive.TimeInNode,
                WorkFlowId = nodeReceive.Parent.Id,
                DateOverdue = dateOverdue
            }, true);

            #endregion

            #region Xử lý quyen xem văn bản liên quan
            if (docRelations.Any())
            {
                var userXem = new List<int>() { userReceiveId };
                //var relationCopyIds = docRelations.Select(d => d.RelationCopyId).ToList();
                //var docCopyRelations = Gets(relationCopyIds);

                if (documentCopyType == DocumentCopyTypes.XuLyChinh)
                {
                    userXem.Add(userSendId);
                    //foreach (var relationCopy in docCopyRelations)
                    //{
                    //    UpdateUserThongBao(relationCopy, userXem);
                    //}

                    UpdateRelationUserJoineds(docRelations, userXem);

                    //// Xử lý văn bản liên quan riêng cho trường hợp khi tạo mới văn bản
                    //foreach (var docRelation in docRelations)
                    //{
                    //    _docFinishService.Create(new DocFinish
                    //    {
                    //        DocFinishType = (int)DocFinishType.KhongThamGiaXuLy,
                    //        DocumentCopyId = docRelation.RelationCopyId,
                    //        DocumentId = docRelation.RelationId,
                    //        IsViewed = false,
                    //        UserId = userSendId
                    //    });
                    //}
                }


                foreach (var docRelation in docRelations)
                {
                    //_docFinishService.Create(new DocFinish
                    //{
                    //    DocFinishType = (int)DocFinishType.KhongThamGiaXuLy,
                    //    DocumentCopyId = docRelation.RelationCopyId,
                    //    DocumentId = docRelation.RelationId,
                    //    IsViewed = false,
                    //    UserId = userReceiveId
                    //});

                    docRelation.DocumentCopyId = documentCopy.DocumentCopyId;
                    _docRelationRepository.Create(docRelation);
                }
            }

            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();

            #endregion

            return documentCopy;
        }

        /// <summary>
        ///   <para>Tạo hướng chuyển bàn giao không theo quy trình: Xin ý kiến, Thông báo, Chờ gia hạn</para>
        ///   <para>CuongNT@bkav.com - 180813</para>
        /// </summary>
        /// <param name="parentDocumentCopy"> </param>
        /// <param name="nodeSend">Co the null, neu van ban hien tai la van ban thong bao, xin y kien, duyet gia han</param>
        /// <param name="userSendId"> </param>
        /// <param name="userReceiveId"> </param>
        /// <param name="dateCreated"> </param>
        /// <param name="documentCopyType"> </param>
        /// <param name="status"> </param>
        /// <param name="docRelations">Danh sách các văn bản liên quan cán bộ nhận (userReceiveId) được phép xem</param>
        /// <param name="isTransfering"><c>True</c> neu la dang ban giao. <c>False</c> neu la van ban van thuoc nguoi dang giu.</param>
        /// <param name="lastcomment">Ý kiến xử lý cuối cùng</param>
        /// <param name="userSendFullName">Tên người gửi ý kiến cuối cùng</param>
        /// <returns> </returns>
        public DocumentCopy CreateSpecial(DocumentCopy parentDocumentCopy, Node nodeSend, int userSendId, int userReceiveId,
                                 DateTime dateCreated, DocumentCopyTypes documentCopyType,
                                 DocumentStatus status, List<DocRelation> docRelations, bool isTransfering, string lastcomment, string userSendFullName)
        {
            if (!SpecialDocumentCopyType.HasFlag(documentCopyType))
            {
                throw new ArgumentException("documentCopyType chỉ được phép thuộc một trong các kiểu DocumentCopyTypes.XinYKien | DocumentCopyTypes.ThongBao | DocumentCopyTypes.DuyetGiaHan");
            }

            #region Xu ly HistoryPath

            var historyProcess = new HistoryProcess();
            historyProcess.HistoryPath.Add(
                    new HistoryPath
                    {
                        ParentId = parentDocumentCopy.DocumentCopyId,
                        DateCreated = dateCreated,
                        UserReceiveId = userReceiveId,
                        UserSendId = userSendId,
                        NodeReceiveId = 0,
                        NodeSendId = nodeSend == null ? 0 : nodeSend.Id,
                        UserReceives = new List<UserReceives>(),
                        WorkflowReceiveId = 0,
                        WorkflowSendId = nodeSend == null ? 0 : nodeSend.Parent.Id
                    });

            #endregion

            #region Tao moi DocumentCopy

            var documentCopy = new DocumentCopy
            {
                ParentId = parentDocumentCopy.DocumentCopyId,
                DocumentId = parentDocumentCopy.DocumentId,
                DocTypeId = parentDocumentCopy.DocTypeId,
                WorkflowId = nodeSend == null ? 0 : nodeSend.Parent.Id,
                UserCurrentId = userReceiveId,
                Status = (int)status,
                History = historyProcess.StringifyJs(),
                DateCreated = dateCreated,
                DateReceived = dateCreated,
                DocumentCopyType = (int)documentCopyType,
                NodeCurrentId = null,
                NodeCurrentName = string.Empty,
                NodeCurrentPermission = null,
                LastDateComment = dateCreated,
                LastComment = lastcomment,
                LastUserIdComment = userSendId,
                LastUserComment = userSendFullName,
                DocumentCopyParentPath = string.Format("{0}{1}\\", parentDocumentCopy.DocumentCopyParentPath, parentDocumentCopy.DocumentCopyId)
            };
            _documentCopyRepository.Create(documentCopy);
            Context.SaveChanges();
            var newDocumentCopyId = documentCopy.DocumentCopyId;

            #endregion

            #region Xử lý ghi danh sách các hướng nhận bàn giao

            var histories = parentDocumentCopy.Histories;
            switch (documentCopyType)
            {
                case DocumentCopyTypes.XinYKien:
                    {
                        var newUserReceive = new UserReceiveXinykiens
                        {
                            DateCreated = dateCreated,
                            DocumentCopyType = (int)documentCopyType,
                            DocumentCopyId = newDocumentCopyId,
                            UserReceiveId = userReceiveId
                        };
                        var historyXinykien = histories.HistoryXinykien.GetOne(userSendId, dateCreated);
                        if (historyXinykien == null)
                        {
                            if (nodeSend == null)
                            {
                                throw new ArgumentNullException("nodeSend", "nodeSend không được phép null khi xin y kien.");
                            }
                            histories.HistoryXinykien.Add(new HistoryXinykien
                            {
                                DateCreated = dateCreated,
                                NodeSendId = nodeSend.Id,
                                ParentId = parentDocumentCopy.DocumentCopyId,
                                UserSendId = userSendId,
                                WorkflowSendId = nodeSend.Parent.Id,
                                UserReceives = new List<UserReceiveXinykiens> { newUserReceive }
                            });
                        }
                        else
                        {
                            historyXinykien.UserReceives.Add(newUserReceive);
                        }
                    }
                    break;

                case DocumentCopyTypes.ThongBao:
                    {
                        var newUserReceive = new UserReceiveThongbaos
                        {
                            DateCreated = dateCreated,
                            DocumentCopyType = (int)documentCopyType,
                            DocumentCopyId = documentCopy.DocumentCopyId,
                            UserReceiveId = userReceiveId
                        };

                        var historyThongbao = histories.HistoryThongbao.GetOne(userSendId, dateCreated);
                        if (historyThongbao == null)
                        {
                            histories.HistoryThongbao.Add(new HistoryThongbao
                            {
                                DateCreated = dateCreated,
                                ParentId = parentDocumentCopy.DocumentCopyId,
                                UserSendId = userSendId,
                                UserReceives = new List<UserReceiveThongbaos> { newUserReceive }
                            });
                        }
                        else
                        {
                            historyThongbao.UserReceives.Add(newUserReceive);
                        }
                    }
                    break;
            }
            parentDocumentCopy.Histories = histories;
            Update(parentDocumentCopy);

            #endregion

            #region DocFinish

            UpdateUserThamGia(documentCopy, new List<int>() { userReceiveId });
            //_docFinishService.Create(new DocFinish
            //{
            //    DocumentId = documentCopy.DocumentId,
            //    UserId = userReceiveId,
            //    DocumentCopyId = newDocumentCopyId,
            //    IsViewed = false,
            //    DocFinishType = (int)DocFinishType.ThamGiaXuLy
            //});

            #endregion

            #region DocTimeline

            _docTimelineService.Create(new DocTimeline
            {
                DocumentId = documentCopy.DocumentId,
                DocumentCopyType = documentCopy.DocumentCopyType,
                DocumentCopyId = documentCopy.DocumentCopyId,
                FromDate = dateCreated,
                IsWorkingTime = false,
                UserId = userReceiveId
            }, true);

            #endregion

            #region Xử lý văn bản liên quan

            UpdateRelationUserJoineds(docRelations, new List<int>() { userReceiveId });

            //foreach (var docRelation in docRelations)
            //{
            //    _docFinishService.Create(new DocFinish
            //    {
            //        DocFinishType = (int)DocFinishType.KhongThamGiaXuLy,
            //        DocumentCopyId = docRelation.RelationCopyId,
            //        DocumentId = docRelation.RelationId,
            //        IsViewed = false,
            //        UserId = userReceiveId
            //    });
            //}

            #endregion

            return documentCopy;
        }

        /// <summary>
        /// Cập nhật người tham gia
        /// </summary>
        /// <param name="documentCopy">Document Copy</param>
        /// <param name="addUserIds">Danh sách người dùng thêm vào</param>
        /// <param name="removedUserIds">Danh sách người dùng bỏ đi</param>
        public void UpdateUserThamGia(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy");
            }

            var result = new List<int>();
            var currents = documentCopy.UserThamGias();

            if (removedUserIds != null && removedUserIds.Any())
            {
                result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
            }
            else
            {
                result = currents;
            }

            if (addUserIds.Any())
            {
                result.AddRange(addUserIds);
            }

            documentCopy.UserNguoiThamGia = DocumentCopy.UserCompareString(result);

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật người nhận thông báo
        /// </summary>
        /// <param name="documentCopy">Document Copy</param>
        /// <param name="addUserIds"></param>
        /// <param name="removedUserIds"></param>
        public void UpdateUserThongBao(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy");
            }

            var result = new List<int>();
            var currents = documentCopy.UserThongBaos();

            if (removedUserIds != null && removedUserIds.Any())
            {
                result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
            }
            else
            {
                result = currents;
            }

            if (addUserIds != null && addUserIds.Any())
            {
                result.AddRange(addUserIds);
            }

            documentCopy.UserThongBao = DocumentCopy.UserCompareString(result);

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật người đã xem văn bản
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <param name="addUserIds"></param>
        /// <param name="removedUserIds"></param>
        public void UpdateUserDaXem(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy");
            }

            var result = new List<int>();
            var currents = documentCopy.UserDaXems();

            if (removedUserIds != null && removedUserIds.Any())
            {
                result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
            }
            else
            {
                result = currents;
            }

            if (addUserIds != null && addUserIds.Any())
            {
                result.AddRange(addUserIds);
            }

            documentCopy.UserNguoiDaXem = DocumentCopy.UserCompareString(result);

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật người giám sát
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <param name="addUserIds"></param>
        /// <param name="removedUserIds"></param>
        public void UpdateUserGiamSat(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy");
            }

            var result = new List<int>();
            var currents = documentCopy.UserGiamSats();

            if (removedUserIds != null && removedUserIds.Any())
            {
                result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
            }
            else
            {
                result = currents;
            }

            if (addUserIds.Any())
            {
                result.AddRange(addUserIds);
            }

            documentCopy.UserGiamSat = DocumentCopy.UserCompareString(result);

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật quyền xem cho các văn bản liên quan.
        /// </summary>
        /// <param name="relations">Danh sách văn bản liên quan</param>
        /// <param name="users">Danh sách người dùng có quyền xem</param>
        /// <param name="removedUsers">Danh sách người dùng bỏ quyền xem</param>
        public void UpdateRelationUserJoineds(List<DocRelation> relations, List<int> users, List<int> removedUsers = null)
        {
            if (relations.Any())
            {
                var relationCopyIds = relations.Select(d => d.RelationCopyId).ToList();
                var docCopyRelations = Gets(relationCopyIds);

                foreach (var relationCopy in docCopyRelations)
                {
                    UpdateUserThongBao(relationCopy, users);
                }
            }
        }

        /// <summary>
        ///   <para>Xóa các bản sao văn bản của 1 hướng chính(xóa các hướng đồng xử lý). </para>
        ///   <para>Thuc hien chuc nang nguoc voi ham Create: Tao gi thi xoa day.</para>
        /// </summary>
        /// <param name="documentCopy">1 bản sao văn bản vừa tạo ra và chưa chuyển đi tiếp</param>
        public void Delete(DocumentCopy documentCopy)
        {
            //if (documentCopy == null || !documentCopy.ParentId.HasValue)
            //{
            //    throw new ArgumentNullException("documentCopy");
            //}

            //if (documentCopy.Histories.HistoryPath.Count != 1)
            //{
            //    throw new ArgumentException("Chỉ được phép xóa bản sao văn bản (DocumentCopy) vừa tạo ra và chưa bàn giao đi tiếp. Tức documentCopy.Histories.Count == 1", "documentCopy");
            //}

            //using (var transaction = new TransactionScope(TransactionScopeOption.Required))
            //{
            //    // Xóa trong HistoryPath của các hướng chuyển liên quan
            //    var parentDocumentCopy = Get(documentCopy.ParentId.Value);

            //    // TODO: Viết thêm class helper: HistoryPathHelper để xử lý các cách ghi thông tin như bên dưới.
            //    var histories = parentDocumentCopy.Histories;
            //    if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ThongBao)
            //    {
            //        var newHistory = histories.HistoryThongbao.GetOne(documentCopy.DateCreated);
            //        var userReceiveRemove = newHistory.UserReceives.SingleOrDefault(c => c.DocumentCopyId == documentCopy.DocumentCopyId);
            //        if (userReceiveRemove == null)
            //        {
            //            throw new ApplicationException("Ghi HistoryPath khi chuyển văn bản chưa chính xác.");
            //        }
            //        newHistory.UserReceives.Remove(userReceiveRemove);
            //        if (!newHistory.UserReceives.Any())
            //        {
            //            histories.HistoryThongbao.Remove(newHistory);
            //        }
            //    }
            //    else if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
            //    {
            //        var newHistory = histories.HistoryXinykien.GetOne(documentCopy.DateCreated);
            //        var userReceiveRemove = newHistory.UserReceives.SingleOrDefault(c => c.DocumentCopyId == documentCopy.DocumentCopyId);
            //        if (userReceiveRemove == null)
            //        {
            //            throw new ApplicationException("Ghi HistoryPath khi chuyển văn bản chưa chính xác.");
            //        }
            //        newHistory.UserReceives.Remove(userReceiveRemove);
            //        if (!newHistory.UserReceives.Any())
            //        {
            //            histories.HistoryXinykien.Remove(newHistory);
            //        }
            //    }
            //    else if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh ||
            //                documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
            //    {
            //        // Bàn giao thông thường
            //        var newHistory = histories.HistoryPath[histories.HistoryPath.Count - 2];
            //        var userReceive = newHistory.UserReceives.SingleOrDefault(c => c.DocumentCopyId == documentCopy.DocumentCopyId);
            //        if (userReceive == null)
            //        {
            //            throw new ApplicationException("Ghi HistoryPath khi chuyển văn bản chưa chính xác.");
            //        }
            //        newHistory.UserReceives.Remove(userReceive);
            //        histories.HistoryPath[histories.HistoryPath.Count - 2] = newHistory;
            //    }
            //    else
            //    {
            //        throw new ApplicationException("Văn bản không cho phép lấy lại");
            //    }

            //    parentDocumentCopy.Histories = histories;
            //    Update(parentDocumentCopy);

            //    // Xóa thông tin văn bản DocFinish theo từng văn bản copy
            //    var docFinishs = _docFinishService.Gets(documentCopy.DocumentCopyId);
            //    _docFinishService.Delete(docFinishs);

            //    // Xóa thông tin văn bản DocTimeline theo từng văn bản copy
            //    _docTimelineService.DeleteByDocumentCopy(documentCopy.DocumentCopyId);

            //    // Xóa documentCopy
            //    _documentCopyDal.Delete(documentCopy);

            //    transaction.Complete();
            //}
        }

        /// <summary>
        ///   Kết thúc hướng xử lý. Đảm bảo chuyển ý kiến về hướng xử lý cha trước khi kết thúc.
        /// </summary>
        /// <param name="documentCopy">Bản sao văn bản cần kết thúc</param>
        /// <param name="dateFinished">Thời điểm kết kết</param>
        /// <param name="userFinishId"> </param>
        /// <param name="commentLog"></param>
        public void Finish(DocumentCopy documentCopy, DateTime dateFinished, int userFinishId, string commentLog = null)
        {
            // Tienbv: tạm bỏ quyền này do khi dừng xử lý (liên thông) cũng cập nhật kết thúc đc nếu hồ sơ đã trả kết quả ở cơ quan liên thông
            //if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh ||
            //    documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy ||
            //    documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien ||
            //    documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ThongBao)
            //{
            if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh || documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ChoKetQuaDungXuLy)
            {
                // Nếu hướng xử lý là hướng chính thì kết thúc luôn hồ sơ.
                documentCopy.Document.Status = (int)DocumentStatus.KetThuc;
                documentCopy.Document.DateFinished = dateFinished;
                documentCopy.DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh;
            }
            if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy
                || documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
            {
                // Kiểm tra điều kiện trả lời ý kiến đóng góp trước khi kết thúc
                if (!ExitCommentToParent(documentCopy))
                {
                    var userFinish = _userService.Get(userFinishId);
                    var content = "Văn bản này được kết thúc bởi " + userFinish.FullName + ". Thời gian kết thúc: " + dateFinished.ToString("dd/MM/yyyy HH:mm:ss");
                    SendAnswerToParent(documentCopy, documentCopy.UserCurrentId, content, dateFinished);
                }
            }

            // Kết thúc hướng xử lý
            documentCopy.DateFinished = dateFinished;
            documentCopy.Status = (int)DocumentStatus.KetThuc;
            documentCopy.DateReceived = dateFinished;
            // _documentCopyRepository.Update(documentCopy);

            // Cập nhật xử lý hồ sơ trong ngày
            var dailyProcess = new DailyProcess()
            {
                UserId = userFinishId,
                DocumentCopyId = documentCopy.DocumentCopyId,
                DocumentId = documentCopy.DocumentId,
                ProcessType = (int)DocumentProcessType.KetThuc,
                DateCreated = DateTime.Now,
                CitizenName = documentCopy.Document.CitizenName,
                Receiver = "",
                Note = "Hồ sơ đã kết thúc"
            };
            _dailyProcessService.Create(dailyProcess);

            Context.SaveChanges();

            // Ghi log ket thuc xu ly
            _commentService.SendCommentCommon(documentCopy, userFinishId, dateFinished, commentLog, CommentType.Finished);
            //}
        }

        /// <summary>
        /// Loại bỏ một văn bản Xử lý chính
        /// </summary>
        /// <param name="documentCopy">Văn bản cần loại bỏ</param>
        /// <param name="userRemovedId">Id cán bộ đang thực hiện loại bỏ văn bản</param>
        /// <param name="dateRemoved"> </param>
        public void Remove(DocumentCopy documentCopy, int userRemovedId, DateTime dateRemoved)
        {
            // Chỉ có văn bản chính mới có quyền hủy
            if (documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.XuLyChinh)
            {
                throw new ApplicationException("Chỉ có văn bản chính và văn bản thông báo mới có nghiệp vụ loại bỏ.");
            }

            //// Chỉ người khởi tạo văn bản mới được hủy văn bản
            //if (documentCopy.Document.UserCreatedId != userRemovedId)
            //{
            //    throw new ApplicationException("Chỉ người khởi tạo văn bản mới có quyền hủy văn bản.");
            //}

            // TODO: Config option cho hành động loại bỏ văn bản chính trong quản trị: hoặc loại bỏ luôn các văn bản sao, hoặc chỉ loại bỏ văn bản chính.
            /* Kết thúc tất cả các bản sao liên quan: dxl, tb, xyk, .... */
            //var childs = documentCopy.Document.DocumentCopys.Where(c => c.DocumentCopyTypeInEnum != DocumentCopyTypes.XuLyChinh).Select(o => o).ToList();

            //HOpCV:250914
            //Sửa lại chỗ lấy danh sách văn bản bản sao
            var childs = _documentCopyRepository.Gets(false, p => p.DocumentId == documentCopy.DocumentId
                                                                && p.DocumentCopyType != (int)DocumentCopyTypes.XuLyChinh);

            var userRenove = _userService.GetCacheAllUsers(true).First(p => p.UserId == userRemovedId);
            string contentFisnish = string.Format("{0} kết thúc văn bản vào lúc :{1}", userRenove.FullName, dateRemoved.ToString("dd/MM/yyyy HH:mm:ss"));
            foreach (var child in childs)
            {
                Finish(child, dateRemoved, userRemovedId, contentFisnish);
            }

            /* Loại bỏ văn bản/hồ sơ chính */
            documentCopy.Status = (int)DocumentStatus.LoaiBo;

            /* Loại bỏ văn bản/hồ sơ gốc */
            // Kiểm tra nếu là hồ sơ thì lấy lại mã hồ sơ đã được cấp(nếu có mã hồ sơ)
            if (documentCopy.Document.StoreId.HasValue)
            {
                var store = _storeService.Get(documentCopy.Document.StoreId.Value);
                if (store != null)
                {
                    var storeCodes = store.StoreCodes;
                    var codeId = storeCodes.First().CodeId;
                    _codeService.ReuseFromDocument(documentCopy.Document);
                }
            }

            documentCopy.Document.DocCode = string.Empty;
            documentCopy.Document.DateOfIssueCode = null;
            documentCopy.Document.Status = (int)DocumentStatus.LoaiBo;
            Update(documentCopy);

            // TODO: Cần tạo một commentType riêng cho phần kết thúc xử lý
            var userRemove = _userService.Get(userRemovedId);
            var commentRemoved = string.Format("{0} đã loại bỏ văn bản. Thời gian loại bỏ: {1}", userRemove.FullName, dateRemoved);
            _commentService.SendComment(documentCopy, userRemovedId, commentRemoved, dateRemoved);
        }

        /// <summary>
        /// <para>Kiểm tra hướng chuyển hiện tại đã trả lời ý kiến đóng góp về hướng cha chưa</para>
        /// <para>Các hướng xử lý: DocumentCopyTypes.XuLyChinh, DocumentCopyTypes.DuyetGiaHan, DocumentCopyTypes.ThongBao, DocumentCopyTypes.ChoKetQuaDungXuLy mặc định trả về true</para>
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <returns></returns>
        private bool ExitCommentToParent(DocumentCopy documentCopy)
        {
            if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh ||
                documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DuyetGiaHan ||
                documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ThongBao ||
                documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ChoKetQuaDungXuLy)
            {
                return true;
            }
            Comment comment = null;
            if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
            {
                comment = _commentService.GetAnswerForVanbanDxl(documentCopy.DocumentCopyId);
            }
            if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
            {
                comment = _commentService.GetAnswerForXinykien(documentCopy.DocumentCopyId);
            }
            if (comment == null)
            {
                return false;
            }
            if (comment.DocumentCopyTargetId != documentCopy.ParentId)
            {
                throw new ApplicationException("Lỗi nghiệp vụ trả lời ý kiến đóng góp. Nội dung trả lời không gửi về hướng yêu cầu.");
            }
            return true;
        }

        /// <summary>
        ///   Lấy ra một bản sao văn bản
        /// </summary>
        /// <param name="documentCopyId"> Id của bản sao văn bản </param>
        /// <returns> Entity bản sao văn bản </returns>
        public DocumentCopy Get(int documentCopyId)
        {
            return _documentCopyRepository.Get(documentCopyId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public int GetDocCopyIdByCurrentUser(Guid documentId)
        {
            var user = _userService.CurrentUser;
            var documentCopy = _documentCopyRepository.Get(true, x => x.DocumentId == documentId && x.UserCurrentId == user.UserId);
            if (documentCopy != null)
            {
                return documentCopy.DocumentCopyId;
            }

            return 0;
        }

        /// <summary>
        ///   Lấy ra một bản sao văn bản
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="documentCopyId"> Id của bản sao văn bản </param>
        /// <returns> Entity bản sao văn bản </returns>
        public T GetAs<T>(Expression<Func<DocumentCopy, T>> projector, int documentCopyId)
        {
            return _documentCopyRepository.GetAs(projector, d => d.DocumentCopyId == documentCopyId);
        }

        /// <summary>
        ///   Trả về các hướng Chờ kết quả dừng xử lý được được gửi từ hướng hiện tại. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="parentId">DocumenCopyId của hướng hiện tại</param>
        /// <param name="documentCopyTypes">Loại DocumentCopy cần trả về</param>
        /// <returns> </returns>
        public IEnumerable<DocumentCopy> GetChildren(int parentId, DocumentCopyTypes documentCopyTypes)
        {
            var spec = DocumentCopyQuery.IsChildWithType(parentId, documentCopyTypes);
            return _documentCopyRepository.GetsReadOnly(spec);
        }

        /// <summary>
        ///   Lấy văn bản copy chính của 1 văn bản/hồ sơ
        /// </summary>
        /// <param name="documentId"> Id văn bản/hồ sơ </param>
        public DocumentCopy GetMain(Guid documentId)
        {
            return _documentCopyRepository.Get(false, dc =>
                        (dc.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh || dc.DocumentCopyType == (int)DocumentCopyTypes.ChoKetQuaDungXuLy) && dc.DocumentId.Equals(documentId));
        }

        /// <summary>
        ///   Trả về danh sách các document copy theo id
        /// </summary>
        /// <param name="documentCopyIds"> </param>
        /// <param name="isIncludeDocument">Có lấy thêm dữ liệu về document</param>
        /// <returns> </returns>
        public IEnumerable<DocumentCopy> Gets(List<int> documentCopyIds, bool isIncludeDocument = false)
        {
            return isIncludeDocument
                ? _documentCopyRepository.Gets(false, d => documentCopyIds.Contains(d.DocumentCopyId),
                    Context.Filters.Include<DocumentCopy>("Document"))
                : _documentCopyRepository.Gets(false, d => documentCopyIds.Contains(d.DocumentCopyId));
        }

        /// <summary>
        /// Trả về tất cả documentid mà user có quyền xem
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<DocumentCopy> GetsByUser(int userId)
        {
            var userStr = DocumentCopy.UserCompareString(userId);
            return _documentCopyRepository.GetsReadOnly(d => d.UserNguoiThamGia.Contains(userStr) || d.UserThongBao.Contains(userStr));
        }

        /// <summary>
        ///   Trả về danh sách các document copy chưa xử lý
        /// </summary>
        /// <param name="userId"> userId</param>
        /// <returns> </returns>
        public IEnumerable<DocumentCopy> GetUnFinishs(int userId)
        {
            var contain = new List<int>{
                (int)DocumentCopyTypes.XuLyChinh,
                (int)DocumentCopyTypes.DongXuLy,
                (int)DocumentCopyTypes.XinYKien
            };

            return _documentCopyRepository.GetsReadOnly(
                d =>
                ((d.Status == (int)DocumentStatus.DangXuLy
                && d.UserCurrentId == userId)
                && contain.Contains(d.DocumentCopyType)));
        }

        /// <summary>
        ///  Trả về User xử lý chính
        /// </summary>
        /// <param name="documentId"> </param>
        /// <returns> </returns>
        public User GetCurrentUser(Guid? documentId)
        {
            var user = new User();
            if (documentId.HasValue)
            {
                var mainDoc = _documentCopyRepository.GetReadOnly(p => p.DocumentId == documentId
                && p.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh);
                if (mainDoc != null)
                {
                    user = _userService.Get(mainDoc.UserCurrentId);
                }
            }

            return user;
        }

        /// <summary>
        /// Trả về phòng ban đang giữ văn bản hiện tại
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <returns></returns>
        public string GetCurrentDepartment(DocumentCopy documentCopy)
        {
            var result = string.Empty;
            if (documentCopy != null
                    && documentCopy.UserCurrent != null
                    && documentCopy.UserCurrent.UserDepartmentJobTitless != null
                    && documentCopy.UserCurrent.UserDepartmentJobTitless.Any())
            {
                var userDeptIds = documentCopy.UserCurrent.UserDepartmentJobTitless.OrderBy(d => d.IsPrimary).First();
                result = userDeptIds.Department.DepartmentName;
            }

            return result;
        }

        /// <summary>
        /// Trả về danh sách các document copy theo document id
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="lastUpdated"></param>
        /// <returns></returns>
        public IEnumerable<DocumentCopy> Gets(Guid documentId, DateTime? lastUpdated)
        {
            Expression<Func<DocumentCopy, bool>> spec;
            if (lastUpdated.HasValue)
            {
                spec = x => x.DocumentId.Equals(documentId) &&
                    ((!x.LastDateComment.HasValue && x.DateCreated > lastUpdated.Value)
                    || (x.LastDateComment.HasValue && x.LastDateComment > lastUpdated.Value));
            }
            else
            {
                spec = x => x.DocumentId.Equals(documentId);
            }
            return _documentCopyRepository.GetsReadOnly(spec);
        }

        /// <summary>
        ///   Lấy các documentCopy theo điều  kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec"> </param>
        /// <returns> </returns>
        public IEnumerable<DocumentCopy> Gets(Expression<Func<DocumentCopy, bool>> spec = null)
        {
            return _documentCopyRepository.Gets(false, spec);
        }

        /// <summary>
        /// <para>Lấy danh sách các hồ sơ, văn bản liên quan của một hồ sơ, văn bản mà người sử dụng hiện tại được phép xem.</para>
        /// <para>TienBV@bkav.com - 061212</para>
        /// </summary>
        /// <param name="documentCopyId">The doc id.</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public IEnumerable<DocRelation> GetDocRelations(int documentCopyId, int userId)
        {
            var result = new List<DocRelation>();
            var relations = _docRelationRepository.GetsReadOnly(d => d.DocumentCopyId == documentCopyId);
            if (!relations.Any())
            {
                return result;
            }

            var relationCopyIds = relations.Select(r => r.RelationCopyId).ToList();

            var documentCopyRelations = _documentCopyRepository.GetsReadOnly(dc => relationCopyIds.Contains(dc.DocumentCopyId));

            foreach (var relation in relations)
            {
                var docCopy = documentCopyRelations.SingleOrDefault(dc => dc.DocumentCopyId == relation.RelationCopyId);
                if (docCopy != null && docCopy.HasQuyenXem(userId))
                {
                    result.Add(relation);
                }
            }

            return result;
        }

        /// <summary>
        /// Thêm văn bản liên quan
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <param name="docRelationId"></param>
        /// <param name="documentId"></param>
        /// <param name="relation"></param>
        /// <param name="type"></param>
        public void AddDocRelations(int documentCopyId, int docRelationId, Guid documentId, Document relation, RelationTypes type)
        {
            var categoryName = "";
            if (relation.CategoryId.HasValue && relation.Category == null)
            {
                var category = _categoryService.Get(relation.CategoryId.Value);
                categoryName = category.CategoryName;
            }

            _docRelationRepository.Create(new DocRelation()
            {
                DocumentCopyId = documentCopyId,
                RelationCopyId = docRelationId,
                RelationType = (int)type,
                DocumentId = documentId,
                RelationId = relation.DocumentId,
                Compendium = relation.Compendium,
                DocCode = relation.DocCode,
                CategoryName = categoryName,
                CitizenName = relation.CitizenName,
                InOutCode = relation.InOutCode,
                DateArrived = DateTime.Now
            });
        }

        /// <summary>
        ///   <para> Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp.. </para>
        ///   <para> (GiangPN@bkav.com - 07022013) </para>
        /// </summary>
        /// <param name="projector"> Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table) </param>
        /// <param name="spec"> Điều kiện </param>
        /// <typeparam name="TOutput"> Kiểu đầu ra. </typeparam>
        /// <returns> Danh sách các thực thể được ánh xạ </returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocumentCopy, TOutput>> projector,
                                                    Expression<Func<DocumentCopy, bool>> spec = null)
        {
            return _documentCopyRepository.GetsAs(projector, spec);
        }

        /// <summary>
        ///   Cập nhật thông tin bản sao văn bản
        /// </summary>
        /// <param name="documentCopy"> Entity bản sao văn bản </param>
        public void Update(DocumentCopy documentCopy)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thời hạn xử lý trên node
        /// </summary>
        /// <param name="documentCopyTransfering">Văn bản cần cập nhật</param>
        /// <param name="nodeReceive">Node hiện tại</param>
        public void UpdateDateOverdue(DocumentCopy documentCopyTransfering, Node nodeReceive)
        {
            if (documentCopyTransfering == null)
            {
                throw new ArgumentNullException("documentCopyTransfering");
            }

            // Nếu node hiện tại là node đc cấu hình dừng xử lý, bỏ qua thời gian node này giữ công văn ở hạn tổng
            // TienBV: tạm thời không dùng đoạn này do việc tự động cộng thêm ngày không rõ ràng, ảnh hưởng đến những
            // Phiếu in cán bộ đã in trước đó.
            // => Khi có thay đổi hạn để người dùng tự cập nhật.
            // var stopProcessDays = 0;
            if (documentCopyTransfering.NodeCurrentId.HasValue)
            {
                //var count = documentCopyTransfering.Histories.HistoryPath.Count;
                //var preHistory = documentCopyTransfering.Histories.HistoryPath[count - 2];
                //var nodeSendId = preHistory.NodeReceiveId;
                //var nodeSend = _workflowHelper.GetNode(documentCopyTransfering.WorkflowId, nodeSendId);
                //if (nodeSend != null && nodeSend.StopProcess)
                //{
                //    stopProcessDays = (DateTime.Now - preHistory.DateCreated).Days;
                //}
            }

            var doc = _documentService.Get(documentCopyTransfering.DocumentId);

            if (doc.DateAppointed.HasValue)
            {
                //doc.DateAppointed = doc.DateAppointed.Value.AddDays(stopProcessDays);
                //_documentService.Update(doc);
            }

            // Todo: cần xem lại cách lưu thời giạn xử lý cho node trên quy trình dạng giờ
            DateTime? dateOverdue = null;
            if (doc.DocType != null && doc.DocType.HasOverdueInNode && nodeReceive.TimeInNode > 0)
            {
                dateOverdue = _worktimeHelper.GetDateAppoint(DateTime.Now, nodeReceive.TimeInNode / 24);
            }

            // Nếu không đặt hạn giữ thì gán hạn tổng vào hạn giữ
            if (!dateOverdue.HasValue)
            {
                dateOverdue = doc.DateAppointed;
            }

            documentCopyTransfering.DateOverdue = dateOverdue;
        }

        /// <summary>
        /// Lấy tổng thời gian công văn ở node dừng xử lý
        /// </summary>
        /// <param name="doc">DocumentCopy</param>
        /// <param name="workflow">workflow</param>
        /// <returns></returns>
        public int GetStopProcessDays(DocumentCopy doc, Workflow workflow)
        {
            var histories = doc.Histories.HistoryPath;
            var stopProcessTime = 0;
            for (int i = 0; i < histories.Count; i++)
            {
                var pastNode = _workflowHelper.GetNode(workflow, (int)histories[i].NodeReceiveId);
                if (!pastNode.StopProcess)
                {
                    continue;
                }

                if (i == histories.Count - 1)
                {
                    stopProcessTime = stopProcessTime + (DateTime.Now - histories[i].DateCreated).Days;
                    for (int j = 0; j < stopProcessTime; j++)
                    {
                        var date = histories[i].DateCreated.AddDays(j + 1);
                        if (_worktimeHelper.IsWeekendOrHoliday(date))
                        {
                            stopProcessTime--;
                        }
                    }
                }
                else
                {
                    stopProcessTime = stopProcessTime + (histories[i + 1].DateCreated - histories[i].DateCreated).Days;
                    for (int j = 0; j < stopProcessTime; j++)
                    {
                        var date = histories[i].DateCreated.AddDays(j + 1);
                        if (_worktimeHelper.IsWeekendOrHoliday(date))
                        {
                            stopProcessTime--;
                        }
                    }
                }
            }
            return stopProcessTime;
        }

        /// <summary>
        /// </summary>
        /// <param name="documentCopyUpdate"> </param>
        /// <param name="userReceiveId"> </param>
        /// <param name="userSendId"> </param>
        /// <param name="nodeReceive"> </param>
        /// <param name="nodeSend"> </param>
        /// <param name="dateCreated"> </param>
        /// <param name="docRelations">Danh sách các văn bản liên quan cán bộ nhận (userReceiveId) được phép xem</param>
        /// <param name="dateOverdue"></param>
        public void UpdateForTransfering(DocumentCopy documentCopyUpdate, Node nodeSend, int userSendId,
                                            Node nodeReceive, int userReceiveId, DateTime dateCreated, List<DocRelation> docRelations, DateTime? dateOverdue)
        {
            if (nodeSend == null)
            {
                throw new ArgumentNullException("nodeSend");
            }

            if (nodeReceive == null)
            {
                throw new ArgumentNullException("nodeReceive");
            }

            #region DocTimeline

            // Cập nhật thời gian ra khỏi nút
            if (documentCopyUpdate.Histories.HistoryPath.Any())
            {
                var dateOfTimeline = documentCopyUpdate.Histories.HistoryPath.Last().DateCreated;
                var docTimeline = _docTimelineService.Get(documentCopyUpdate.DocumentCopyId, userSendId, dateOfTimeline, nodeSend.Id);
                if (docTimeline == null)
                {
                    //Todo: Xử lý null doctimeline ở đây
                }
                else
                {
                    _docTimelineService.Update(docTimeline, dateCreated, false);
                }
            }

            // Tạo mới timeline cho người nhập hướng chính
            _docTimelineService.Create(new DocTimeline
            {
                DocumentId = documentCopyUpdate.DocumentId,
                DocumentCopyType = documentCopyUpdate.DocumentCopyType,
                DocumentCopyId = documentCopyUpdate.DocumentCopyId,
                FromDate = dateCreated,
                IsWorkingTime = !nodeReceive.StopProcess,
                NodeId = nodeReceive.Id,
                NodeName = nodeReceive.NodeName,
                UserId = userReceiveId,
                UserSendId = userSendId,
                NodeSendId = nodeSend.Id,
                NodeSendName = nodeSend.NodeName,
                TimeInNode = nodeSend.TimeInNode,
                WorkFlowId = nodeSend.Parent.Id,
                DateOverdue = dateOverdue
            }, true);

            #endregion

            #region Xu ly HistoryPath

            var histories = documentCopyUpdate.Histories;

            histories.HistoryPath.Add(new HistoryPath
            {
                DateCreated = dateCreated,
                ParentId = documentCopyUpdate.ParentId,
                UserReceiveId = userReceiveId,
                UserSendId = userSendId,
                NodeReceiveId = nodeReceive.Id,
                NodeSendId = nodeSend.Id,
                UserReceives = new List<UserReceives>(),
                WorkflowReceiveId = nodeReceive.Parent.Id,
                WorkflowSendId = nodeSend.Parent.Id
            });

            #endregion

            #region Xử lý ghi danh sách các hướng nhận bàn giao

            if (histories.HistoryPath.Count > 1)
            {
                // Chỉ đúng khi Hướng XLC được thực hiện trước, sau đó các hướng ĐXL mới ghi theo dạng -2.
                // Ngược lại, Hướng XLC thực hiện sau, thì các hướng ĐXL sẽ ghi theo dạng -1.
                // TODO: --> Các có cách xử lý chính xác hơn cách làm hiện tại này.
                var userReceive = new UserReceives
                {
                    DocumentCopyId = documentCopyUpdate.DocumentCopyId,
                    DocumentCopyType = documentCopyUpdate.DocumentCopyType,
                    WorkflowId = nodeReceive.Parent.Id,
                    IsXlc = true,
                    UserReceiveId = userReceiveId,
                    DateCreated = dateCreated
                };
                histories.HistoryPath[histories.HistoryPath.Count - 2].UserReceives.Add(userReceive);
            }

            #endregion

            #region Cap nhat DocumentCopy

            documentCopyUpdate.Histories = histories;
            documentCopyUpdate.UserCurrentId = userReceiveId;
            documentCopyUpdate.NodeCurrentId = nodeReceive.Id;
            documentCopyUpdate.HasJustCreated = false;
            documentCopyUpdate.NodeCurrentPermission = (int)nodeReceive.GetNodePermission();
            documentCopyUpdate.DateReceived = dateCreated;

            #endregion

            #region Cập nhật số phút xử lý cho document
            //Todo: tạm bỏ cho phần demo BIDV, sau cần xem đưa vào lại
            //documentCopyUpdate.Document.ProcessedMinutes += _worktimeHelper.GetWorkminutes(docTimeline.FromDate, dateCreated);
            //Update(documentCopyUpdate);

            #endregion

            #region DocFinish

            UpdateUserThamGia(documentCopyUpdate, new List<int>() { userReceiveId });

            //_docFinishService.Create(new DocFinish
            //{
            //    DocumentId = documentCopyUpdate.DocumentId,
            //    UserId = userReceiveId,
            //    DocumentCopyId = documentCopyUpdate.DocumentCopyId,
            //    IsViewed = false,
            //    DocFinishType = (int)DocFinishType.ThamGiaXuLy
            //});

            #endregion

            #region Xử lý văn bản liên quan

            UpdateRelationUserJoineds(docRelations, new List<int>() { userReceiveId });

            //// Xử lý thêm người xem cho văn bản liên quan
            //foreach (var docRelation in docRelations)
            //{
            //    _docFinishService.Create(new DocFinish
            //    {
            //        DocFinishType = (int)DocFinishType.KhongThamGiaXuLy,
            //        DocumentCopyId = docRelation.RelationCopyId,
            //        DocumentId = docRelation.RelationId,
            //        IsViewed = false,
            //        UserId = userReceiveId
            //    });
            //}

            #endregion
        }

        /// <summary>
        /// Thực hiện chức năng ngược lại UpdateForTransfering().
        /// </summary>
        /// <param name="documentCopyUpdate"></param>
        public void UpdateForUndoTransfering(DocumentCopy documentCopyUpdate)
        {
            var historyProcess = documentCopyUpdate.Histories;

            #region DocTimeline
            // TODO: Lưu ý trường hợp xóa timeline khi khởi tạo văn bản thì tự bàn giao cho cả chính mình --> Không có timeline sau khi lấy lại --> Lỗi.
            // Xử lý không xóa timeline nếu đó là timeline của người khởi tạo văn bản tại thời điểm khởi tạo.

            #endregion

            #region Xu ly HistoryPath

            var removeHistory = historyProcess.HistoryPath.Last();
            historyProcess.HistoryPath.Remove(removeHistory);

            #endregion

            #region Xử lý xoa danh sách các hướng nhận bàn giao

            var removeUserReceive = historyProcess.HistoryPath.Last().UserReceives.SingleOrDefault(c => c.IsXlc && c.DocumentCopyId == documentCopyUpdate.DocumentCopyId);
            historyProcess.HistoryPath.Last().UserReceives.Remove(removeUserReceive);

            #endregion

            #region Cap nhat DocumentCopy

            documentCopyUpdate.Histories = historyProcess;
            documentCopyUpdate.UserCurrentId = removeHistory.UserSendId;
            documentCopyUpdate.NodeCurrentId = removeHistory.NodeSendId;
            documentCopyUpdate.NodeCurrentPermission = _workflowHelper.GetNodePermission(removeHistory.WorkflowSendId, removeHistory.NodeSendId);
            //documentCopyUpdate.DateReceived = historyProcess.HistoryPath.Last().DateCreated;

            // TienBV sửa: nếu gắn lại thời gian là cái cũ thì không lên đầu danh sách văn bản được
            // + khi lấy danh sách văn bản mới cũng không lấy được do chỉ lấy những văn bản có thời gian >= thời gian lấy gần nhất.
            documentCopyUpdate.DateReceived = DateTime.Now;

            #region Cập nhật lại ý kiến trước đó

            //Lay ra nguoi chuyen van ban toi truoc do
            var userSendId = documentCopyUpdate.Histories.HistoryPath.Last().UserSendId;
            //Lay comment gan nhat cua nguoi gui van ban de cap nhat ys kien xu ly cuoi cung
            var comment = _commentService.Gets(documentCopyUpdate.DocumentCopyId, userSendId).OrderByDescending(p => p.DateCreated).FirstOrDefault();
            string lastComent = string.Empty;
            string lastUserComment = string.Empty;
            int? lastUserIdComment = null;
            DateTime? lastDateComment = null;
            if (comment != null)
            {
                var objContentComment = Json2.ParseAs<ContentEntity>(comment.Content);
                lastComent = objContentComment.Content;
                lastUserIdComment = comment.UserSendId;
                lastDateComment = comment.DateCreated;
                lastUserComment = _userService.GetCacheAllUsers(true).FirstOrDefault(p => p.UserId == comment.UserSendId).FullName;
            }

            //Cập nhật lại ý kiến xử lý cuối cùng, người tham gia xủa lý cuối cùng
            documentCopyUpdate.LastComment = lastComent;
            documentCopyUpdate.LastUserComment = lastUserComment;
            documentCopyUpdate.LastUserIdComment = lastUserIdComment;
            documentCopyUpdate.LastDateComment = lastDateComment;

            #endregion

            Update(documentCopyUpdate);

            #endregion

            #region DocFinish

            var thamGiaxuly =
                historyProcess.HistoryPath.Any(
                    c => c.UserSendId == removeHistory.UserReceiveId || c.UserReceiveId == removeHistory.UserReceiveId);
            if (!thamGiaxuly)
            {
                UpdateUserThamGia(documentCopyUpdate, new List<int>(), new List<int>() { removeHistory.UserReceiveId });
                //var deleteDocFinish = _docFinishService.Get(documentCopyUpdate.DocumentCopyId,
                //                                             removeHistory.UserReceiveId, DocFinishType.ThamGiaXuLy);
                //_docFinishService.Delete(deleteDocFinish);
            }

            #endregion

            #region Xử lý văn bản liên quan

            if (!thamGiaxuly)
            {
                var deleteRelations = GetDocRelations(documentCopyUpdate.DocumentCopyId, removeHistory.UserReceiveId).ToList();
                UpdateRelationUserJoineds(deleteRelations, null, new List<int>() { removeHistory.UserReceiveId });
                //var docRelationIds = deleteRelations.Select(c => c.DocRelationId).ToList();
                //var deleteDocFinishs =
                //    _docFinishService.Gets(
                //        c => c.DocRelationId != null && docRelationIds.Contains(c.DocRelationId.Value));
                //_docFinishService.Delete(deleteDocFinishs);
            }

            #endregion
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <param name="userSendId"></param>
        /// <param name="comment"></param>
        /// <param name="dateCreated"></param>
        /// <param name="contentAuthorize"></param>
        private void SendAnswerToParent(DocumentCopy documentCopy, int userSendId, string comment, DateTime dateCreated, string contentAuthorize = "")
        {
            if (documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.DongXuLy &&
                documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.XinYKien)
            {
                throw new ApplicationException("Chỉ văn bản xin ý kiến và văn bản Đồng xử lý mới sử dụng nghiệp vụ này.");
            }

            // Xử lý chuyển ý kiến đóng góp văn bản Đồng xử lý
            var parentId = documentCopy.ParentId;
            if (!parentId.HasValue)
            {
                throw new ApplicationException(
                    "documentCopy.ParentId không được phép null với văn bản ĐXL trả lời ý kiến đóng góp.");
            }
            var parent = Get(parentId.Value);
            if (parent == null)
            {
                throw new ApplicationException("Không tim thấy hướng chuyển cha documentCopy.ParentId.");
            }

            var transfers = new List<CommentTransfer>
                {
                    new CommentTransfer
                        {
                            Label = _userService.Get(parent.UserCurrentId).FullName,
                            Type = "1",
                            Value = ""
                        }
                };

            switch (documentCopy.DocumentCopyTypeInEnum)
            {
                case DocumentCopyTypes.XinYKien:
                    _commentService.SendAnswerForXinykien(documentCopy, userSendId, parent.UserCurrentId, comment,
                                                          transfers, dateCreated, contentAuthorize);
                    break;

                case DocumentCopyTypes.DongXuLy:
                    _commentService.SendAnswerForVanbanDxl(documentCopy, userSendId, parent.UserCurrentId, comment,
                                                           transfers, dateCreated, contentAuthorize);
                    break;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <param name="userSendId"></param>
        /// <param name="comment"></param>
        /// <param name="dateCreated"></param>
        /// <param name="userParentId">HopCV: Lấy ra id người xử lý để tạo notification cho người đó biết</param>
        /// <param name="contentAuthorize">Thông tin ghi người ủy quyền xử lý</param>
        private void SendAnswerToParent(DocumentCopy documentCopy, int userSendId, string comment,
            DateTime dateCreated, out int userParentId, string contentAuthorize = "")
        {
            if (documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.DongXuLy &&
                documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.XinYKien)
            {
                throw new ApplicationException("Chỉ văn bản xin ý kiến và văn bản Đồng xử lý mới sử dụng nghiệp vụ này.");
            }

            // Xử lý chuyển ý kiến đóng góp văn bản Đồng xử lý
            var parentId = documentCopy.ParentId;
            if (!parentId.HasValue)
            {
                throw new ApplicationException(
                    "documentCopy.ParentId không được phép null với văn bản ĐXL trả lời ý kiến đóng góp.");
            }
            var parent = Get(parentId.Value);
            if (parent == null)
            {
                throw new ApplicationException("Không tim thấy hướng chuyển cha documentCopy.ParentId.");
            }

            //Lấy id của chuyển giao văn bản trước
            userParentId = parent.UserCurrentId;
            var transfers = new List<CommentTransfer>
                {
                    new CommentTransfer
                        {
                            Label = _userService.Get(parent.UserCurrentId).FullName,
                            Type = "xulychinh",
                            Value = "viewXlc"
                        }
                };

            switch (documentCopy.DocumentCopyTypeInEnum)
            {
                case DocumentCopyTypes.XinYKien:
                    _commentService.SendAnswerForXinykien(documentCopy, userSendId, parent.UserCurrentId, comment,
                                                          transfers, dateCreated, contentAuthorize);
                    break;

                case DocumentCopyTypes.DongXuLy:
                    _commentService.SendAnswerForVanbanDxl(documentCopy, userSendId, parent.UserCurrentId, comment,
                                                           transfers, dateCreated, contentAuthorize);
                    break;
            }
        }

        /// <summary>
        /// Chuyển ý kiến xử lý từ hướng ĐXL, Xin ý kiến về hướng xin ý kiến ban đầu
        /// </summary>
        /// <param name="documentCopy">Văn bản</param>
        /// <param name="userSendId">Id người gửi</param>
        /// <param name="comment">Nội dung</param>
        /// <param name="dateCreated">Ngày tạo</param>
        /// <param name="commentFinish">Nội dung ghi log khi kết thúc văn bản</param>
        /// <param name="contentAuthorize">Nội dung ghi ủy quyền</param>
        public void SendCommentToParent(DocumentCopy documentCopy, int userSendId, string comment, DateTime dateCreated, string commentFinish, string contentAuthorize = "")
        {
            // Gửi ý kiến trả lời
            SendAnswerToParent(documentCopy, userSendId, comment, dateCreated, contentAuthorize);
            // Kết thúc xử lý
            Finish(documentCopy, dateCreated, userSendId, commentFinish);
        }

        /// <summary>
        /// Chuyển ý kiến xử lý từ hướng ĐXL, Xin ý kiến về hướng xin ý kiến ban đầu
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <param name="userSendId"></param>
        /// <param name="comment"></param>
        /// <param name="dateCreated"> </param>
        /// <param name="userParentId">HopCV: Lấy ra id người xử lý để tạo notification cho người đó biết</param>
        /// <param name="commentFinish"></param>
        /// <param name="contentAuthorize"></param>
        public void SendCommentToParent(DocumentCopy documentCopy, int userSendId, string comment, DateTime dateCreated, out int userParentId, string commentFinish, string contentAuthorize = "")
        {
            // Gửi ý kiến trả lời
            SendAnswerToParent(documentCopy, userSendId, comment, dateCreated, out userParentId, contentAuthorize);

            // Kết thúc xử lý
            Finish(documentCopy, dateCreated, userSendId, comment);
        }

        /// <summary>
        /// Thiết lập trạng thái xem văn bản
        /// HopCV: 171214
        /// Đưa nghiệp vụ từ controler xuống và thêm kiểm tra người đang giữ văn bản có phải là người đăng nhập hiện tại không
        /// thì mới cập nhật trạng thái cho những người gửi trước
        /// </summary>
        /// <param name="documentCopy">Văn bản copy muốn thiết lập trạng thái xem văn bản</param>
        /// <param name="userSendId">Id người gửi văn bản</param>
        /// <param name="viewed"></param>
        /// <param name="outUserUpdateIds"></param>
        public void SetViewed(DocumentCopy documentCopy, int userSendId, bool viewed, out List<int> outUserUpdateIds)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy is null or empty!");
            }

            outUserUpdateIds = new List<int>();
            var lastUserComment = documentCopy.LastUserIdComment;

            // update đã xem: update cho các node có trạng thái văn bản là chờ xử lý
            // Todo: Các trạng thái khác sẽ do người nhận được khi mở văn bản lên sẽ tự update.
            // Mục đích là để người gửi biết được người nhận đã xem hay chưa.
            if (documentCopy.IsCurrentUser(userSendId))
            {
                if (documentCopy.IsViewed(userSendId) != viewed)
                {
                    outUserUpdateIds.Add(userSendId);
                }

                if (lastUserComment.HasValue && (userSendId != lastUserComment.Value
                        || lastUserComment.Value != _userService.CurrentUser.UserId))
                {
                    if (documentCopy.IsViewed(lastUserComment.Value) != viewed)
                    {
                        outUserUpdateIds.Add(documentCopy.LastUserIdComment.Value);
                    }
                }
            }
            else
            {
                if (documentCopy.IsViewed(userSendId) != viewed)
                {
                    // Trường hợp không xử lý văn bản nhưng vẫn được phân quyền xem.
                    outUserUpdateIds.Add(userSendId);
                }
            }

            if (!outUserUpdateIds.Any())
            {
                return;
            }

            if (viewed)
            {
                UpdateUserDaXem(documentCopy, outUserUpdateIds);
            }
            else
            {
                UpdateUserDaXem(documentCopy, null, outUserUpdateIds);
            }
        }

        /// <summary>
        /// Thiết lập trạng thái xem văn bản dạng thông báo
        /// TrinhNVd: 171215
        /// Đưa nghiệp vụ từ controler xuống và thêm kiểm tra người đang giữ văn bản có phải là người đăng nhập hiện tại không
        /// thì mới cập nhật trạng thái cho những người gửi trước
        /// </summary>
        /// <param name="documentCopy">Văn bản copy muốn thiết lập trạng thái xem văn bản</param>
        /// <param name="userSendId">Id người gửi văn bản</param>
        public void SetNotifyViewed(DocumentCopy documentCopy, int userSendId)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy is null or empty!");
            }

            //TienBV: chả hiểu hàm này viết với mục đích gì?

            // update đã xem: update cho các node có trạng thái văn bản là chờ xử lý
            // Todo: Các trạng thái khác sẽ do người nhận được khi mở văn bản lên sẽ tự update.
            // Mục đích là để người gửi biết được người nhận đã xem hay chưa.
            if (documentCopy.IsCurrentUser(userSendId))
            {
                documentCopy.DateReceived = DateTime.Now;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <param name="userId"></param>
        /// <param name="type"> </param>
        /// <returns></returns>
        public bool CheckIsViewed(int documentCopyId, int userId, DocFinishType type)
        {
            if (documentCopyId <= 0)
            {
                return false;
            }

            var documentCopy = Get(documentCopyId);

            return documentCopy != null && documentCopy.IsViewed(userId);
        }

        // TODO: Cần đưa vào trong nội bộ các hàm chuyên dụng, chứ không cần một hàm đổi trạng thái văn bản độc lập thế này.
        /// <summary>
        /// Thay đổi trạng thái của document.
        /// </summary>
        /// <param name="docCopyId">Document copy id.</param>
        /// <param name="status">Status in enum</param>
        public void ChangeStatus(int docCopyId, DocumentStatus status)
        {
            var documentCopy = Get(docCopyId);
            if (documentCopy == null)
            {
                throw new Exception("Hồ sơ không tồn tại.");
            }
            documentCopy.Status = (byte)status;
            // Nếu là bản chính thì thay đổi luôn trạng thái trên hồ sơ
            if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh)
            {
                documentCopy.Document.Status = (byte)status;
            }
            //_docCopyService.Update(documentCopy);
        }

        #region Thống kê, báo cáo

        /// <summary>
        /// HopCV:110815
        /// Lây danh sách văn bản quá hạn theo danh sách người dùng
        /// </summary>
        /// <param name="userIds">Danh sách id người dùng</param>
        /// <param name="beginDate">Ngày bắt đầu so sánh thời gian nhận văn bản</param>
        /// <param name="endDate">Ngày kết thúc so sanh thời gian nhận văn bản</param>
        /// <param name="docCopyList">Trả ra danh sách văn bản đang giữ(Dự thao, đang xử lý, kết thúc, loại bỏ, dừng xử lý)</param>
        /// <returns>Danh sách văn bản quá hạn</returns>
        public IEnumerable<DocumentCopy> GetDocumentCopyOverDueByListuser(
            IEnumerable<int> userIds,
            DateTime beginDate,
            DateTime endDate,
            out IEnumerable<DocumentCopy> docCopyList)
        {
            if (userIds == null || !userIds.Any())
            {
                throw new ArgumentNullException("Danh sách id can bộ null.");
            }

            //Lấy danh sách văn bản theo danh sách người dung đang giữ
            var docCopys = _documentCopyRepository.GetsReadOnly(p =>
                (p.DateReceived >= beginDate
                && p.DateReceived <= endDate)
                && userIds.Contains(p.UserCurrentId));
            docCopyList = docCopys;

            if (docCopys == null || !docCopys.Any())
            {
                return null;
            }

            //Danh sách văn bản đang ở mục xử lý
            var docCopyDangXuLys = docCopys.Where(p => p.Status == (int)DocumentStatus.DangXuLy);
            if (docCopyDangXuLys == null || !docCopyDangXuLys.Any())
            {
                return null;
            }

            var dateNow = DateTime.Now;
            var docCopyOverDues = new List<DocumentCopy>();
            var workflowIds = docCopyDangXuLys.Select(p => p.WorkflowId).Distinct();
            var workFlows = _workflowService.Gets(p => workflowIds.Contains(p.WorkflowId));

            if (workFlows == null || !workFlows.Any())
            {
                throw new ApplicationException("WorkFlow is not exist.");
            }

            var holidays = _worktimeHelper.GetHolidaysAndWeekends(dateNow.Year);
            foreach (var item in docCopyDangXuLys)
            {
                //Check ngày quá hạn trên hồ sơ, văn bản
                if (item.DateOverdue.HasValue && dateNow > item.DateOverdue.Value)
                {
                    docCopyOverDues.Add(item);
                    continue;
                }

                #region Xử lý thời gian giữ trên node của quy trình, ngày nghỉ lễ, ngày nghỉ

                // node hiện tại của văn bản
                if (!item.NodeCurrentId.HasValue)
                {
                    continue;
                }

                var workFlow = workFlows.FirstOrDefault(p => p.WorkflowId == item.WorkflowId);
                if (workFlow == null)
                {
                    continue;
                }

                var currentNode = _workflowHelper.GetNode(workFlow, item.NodeCurrentId.Value);
                if (currentNode == null)
                {
                    continue;
                }

                var currentNodeKeepTime = (dateNow - item.DateReceived).Days;
                var totalKeepTime = (dateNow - item.Document.DateCreated).Days;

                //Kiểm tra trong thời gian giữ văn bản nếu có ngày nghỉ lễ thì trừ ngày đó đi
                var holidaysInCurrentNodeKeepTime = 0;
                for (int i = 0; i < currentNodeKeepTime; i++)
                {
                    var date = item.DateReceived.AddDays(i + 1);
                    if (_worktimeHelper.IsWeekendOrHoliday(ref holidays, date))
                    {
                        holidaysInCurrentNodeKeepTime++;
                    }
                }

                var holidaysInTotalKeepTime = 0;
                for (int i = 0; i < totalKeepTime; i++)
                {
                    var date = item.DateReceived.AddDays(i + 1);
                    if (_worktimeHelper.IsWeekendOrHoliday(ref holidays, date))
                    {
                        holidaysInTotalKeepTime++;
                    }
                }

                //Thời gian ở node dừng xử lý
                var stopProcessTime = GetStopProcessDays(item, workFlow);
                totalKeepTime = totalKeepTime - holidaysInTotalKeepTime - stopProcessTime;
                //    currentNodeKeepTime = currentNodeKeepTime - holidaysInCurrentNodeKeepTime;
                if (totalKeepTime > workFlow.ExpireProcess)
                {
                    docCopyOverDues.Add(item);
                }

                #endregion
            }

            return docCopyOverDues;
        }

        /// <summary>
        /// Lây danh sách văn bản quá hạn theo danh sách người dùng
        /// </summary>
        /// <param name="userIds">Danh sách người dùng hiện tại</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentOverdues(IEnumerable<int> userIds, DateTime from, DateTime to)
        {
            if (userIds == null || !userIds.Any())
            {
                throw new ArgumentNullException("userIds is null.");
            }

            var docCopyDangXuLys = _documentCopyRepository.GetsReadOnly(d =>
                                (d.Status == (int)DocumentStatus.DangXuLy || d.Status == (int)DocumentStatus.DungXuLy)
                                && d.NodeCurrentId.HasValue
                                && d.Document.DateCreated >= from
                                && d.Document.DateCreated <= to
                                && userIds.Contains(d.UserCurrentId));

            return ParseDocumentOverdueList(docCopyDangXuLys);
        }

        /// <summary>
        /// Trả về danh sách văn bản quá hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentOverdues(bool hasOldDocument, DateTime from, DateTime to)
        {
            #region Chỉ lấy đang xử lý
            //var documentCopies = _documentCopyRepository.GetsReadOnly(d =>
            //                    (d.Document.Status == (int)DocumentStatus.DangXuLy || d.Document.Status == (int)DocumentStatus.DungXuLy)
            //                    && d.NodeCurrentId.HasValue
            //                    && !d.Document.IsConverted
            //                    && (d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh
            //                        || d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy || d.DocumentCopyType == (int)DocumentCopyTypes.ChoKetQuaDungXuLy)
            //                    && d.Document.DocTypeId.HasValue
            //                    && !d.Document.DateFinished.HasValue
            //                    && !d.Document.IsSuccess.HasValue
            //                    && !d.Document.IsReturned.HasValue
            //                    && ((d.Document.DateCreated >= from && d.Document.DateCreated <= to)
            //                        || (hasOldDocument && d.Document.DateCreated < from))
            //                    );
            #endregion

            var documentCopies = _documentCopyRepository.GetsReadOnly(d =>
                                d.Document.Status != (int)DocumentStatus.DuThao
                                && d.Document.Status != (int)DocumentStatus.LoaiBo
                                && d.NodeCurrentId.HasValue
                                && !d.Document.IsConverted
                                && (d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh
                                    || d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy || d.DocumentCopyType == (int)DocumentCopyTypes.ChoKetQuaDungXuLy)
                                && d.Document.DocTypeId.HasValue
                                && ((d.Document.DateCreated >= from && d.Document.DateCreated <= to)
                                    || (hasOldDocument && d.Document.DateCreated < from))
                                );

            return ParseDocumentOverdueList(documentCopies);
        }

        /// <summary>
        /// Trả về danh sách văn bản đúng hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentDungHans(bool hasOldDocument, DateTime from, DateTime to)
        {
            var documentCopies = GetsForStatisticFromCache(hasOldDocument, from, to);

            documentCopies = documentCopies.Where(d => StatisticUtil.OverdueStatus(d.Status, d.Document.DateAppointed.Value,
                d.Document.DateSuccess, d.Document.DateReturned,
                d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy ? d.DateFinished : d.Document.DateFinished,
                d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.ResolveInTime).ToList();

            return ParseDocumentOverdueListNotWithWorkflow(documentCopies);
        }

        /// <summary>
        /// Trả về danh sách văn bản trễ hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentTreHans(bool hasOldDocument, DateTime from, DateTime to)
        {
            var documentCopies = GetsForStatisticFromCache(hasOldDocument, from, to);

            documentCopies = documentCopies.Where(d => StatisticUtil.OverdueStatus(d.Status, d.Document.DateAppointed.Value, d.Document.DateSuccess,
                    d.Document.DateReturned,
                    d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy ? d.DateFinished : d.Document.DateFinished,
                    d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.ResolveLate).ToList();

            return ParseDocumentOverdueListNotWithWorkflow(documentCopies);
        }

        /// <summary>
        /// Trả về danh sách văn bản chưa đến hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentChuaDenHans(bool hasOldDocument, DateTime from, DateTime to)
        {
            var documentCopies = GetsForStatisticFromCache(hasOldDocument, from, to);

            documentCopies = documentCopies.Where(d => StatisticUtil.OverdueStatus(d.Document.Status, d.Document.DateAppointed.Value, d.Document.DateSuccess,
                    d.Document.DateReturned,
                    d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy ? d.DateFinished : d.Document.DateFinished,
                    d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.Pending).ToList();

            return ParseDocumentOverdueListNotWithWorkflow(documentCopies);
        }

        /// <summary>
        /// Trả về danh sách văn bản chưa đến hạn của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentQuaHans(bool hasOldDocument, DateTime from, DateTime to)
        {
            var now = DateTime.Now;

            var documentCopies = GetsForStatisticFromCache(hasOldDocument, from, to);

            documentCopies = documentCopies
                    .Where(d =>
                        StatisticUtil.OverdueStatus(d.Document.Status, d.Document.DateAppointed.Value, d.Document.DateSuccess, d.Document.DateReturned,
                            d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy ? d.DateFinished : d.Document.DateFinished,
                            d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.Overdue)
                    .ToList();

            return ParseDocumentOverdueListNotWithWorkflow(documentCopies);
        }

        /// <summary>
        /// Trả về danh sách văn bản, hồ sơ quá hạn theo từng node trên quy trình
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
            var result = GetDocumentQuaHans(withOldDocument, from, to).ToList();

            var documentCopyIds = result.Select(d => d.DocumentCopyId).Distinct().ToList();
            var ignoreUser = _adminSettings.UserIgnoreOverdueId;
            var timelines = _docTimelineService.Gets(tl =>
                                    tl.FromDate >= from && tl.FromDate <= to
                                    && (ignoreUser == 0 || tl.UserId != ignoreUser)
                                    && documentCopyIds.Contains(tl.DocumentCopyId)
                                    && (tl.ToDate.HasValue &&
                                        ((tl.DateOverdue.HasValue && tl.ToDate.Value > tl.DateOverdue.Value) ||
                                        (!tl.DateOverdue.HasValue && tl.ProcessedMinutes > (tl.TimeInNode * 60)))
                                    ));

            //var timeLines = GetTimeLinesFromCache(hasOldDocument, from, to);

            //var result = new List<DocumentOverdue>();
            //if (!timeLines.Any())
            //{
            //    return result;
            //}

            //var documentCopyIds = timeLines.Select(t => t.DocumentCopyId).Distinct().ToList();
            // var documentCopies = Gets(documentCopyIds, isIncludeDocument: true);
            var allUsers = _userService.GetCacheAllUsers(isActivated: true);

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

                //var currentDepartment = docCopy.CurrentDepartmentExt document.CategoryBusinessId == 4
                //            ? document.InOutPlace
                //            : docCopy.UserCurrent.UserDepartmentJobTitless.Any() ? docCopy.UserCurrent.UserDepartmentJobTitless.First().Department.DepartmentPath : "";

                var currentDepartment = "";
                var timeInNode = 1 + (timeLine.TimeInNode / 24);
                var dateOverdue = timeLine.DateOverdue ?? _worktimeHelper.GetDateAppoint(timeLine.FromDate, timeInNode);

                var docOverDue = new DocumentOverdue()
                {
                    DocumentCopyId = docCopy.DocumentCopyId,
                    Compendium = docCopy.CategoryBusinessId == 4 ? docCopy.DoctypeName : docCopy.Compendium,
                    DoctypeId = docCopy.DoctypeId,
                    DoctypeName = docCopy.DoctypeName,
                    DocCode = docCopy.DocCode,
                    DateAppointed = dateOverdue.ToString("HH:mm dd/MM/yyyy"),
                    DateCreated = timeLine.FromDate.ToString("HH:mm dd/MM/yyyy"),
                    DateFinished = timeLine.ToDate.HasValue ? timeLine.ToDate.Value.ToString("HH:mm dd/MM/yyyy") : "",
                    CurrentNodePermitTime = timeLine.TimeInNode * 60,
                    CurrentNodeKeepTime = timeLine.ProcessedMinutes,
                    CurrentUser = string.Format("{0} ({1})", user.FullName, user.Username),
                    CurrentUserId = timeLine.UserId,
                    UserIdCreated = timeLine.UserSendId,
                    CategoryBusinessId = docCopy.CategoryBusinessId,
                    CurrentDepartmentExt = currentDepartment,
                    IsProcessing = timeLine.IsWorkingTime,
                    Deadline = (int)((timeLine.ToDate.HasValue ? timeLine.ToDate.Value : DateTime.Now) - dateOverdue).TotalDays
                };

                result.Add(docOverDue);
            }

            return result;
        }

        /// <summary>
        /// Trả về danh sách hồ sơ đăng ký qua mạng của cả hệ thống
        /// </summary>
        /// <param name="hasOldDocument">Lấy cả hồ sơ tồn kỳ trước</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentOnlines(bool hasOldDocument, DateTime from, DateTime to)
        {
            var now = DateTime.Now;

            var documentCopies = GetsForStatisticFromCache(hasOldDocument, from, to);
            documentCopies = documentCopies.Where(d => d.Document.Original == 1);

            return ParseDocumentOverdueListNotWithWorkflow(documentCopies);
        }

        /// <summary>
        /// Trả về danh sách tất cả các DocumentCopy trong khoảng thời gian lấy báo cáo: tiếp nhận trong kỳ + tồn kỳ trước có lưu cache
        /// </summary>
        /// <param name="hasOldDocument">Giá trị xác định có lấy văn bản tồn kỳ trước không.</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<DocumentCopy> GetsForStatisticFromCache(bool hasOldDocument, DateTime? from, DateTime to)
        {
            var cacheKey = string.Format(CacheParam.StatisticKey,
                    string.Format("{0}{1}{2}", hasOldDocument ? 1 : 0, from.HasValue ? from.Value.ToString("yyyyMMdd") : "", to.ToString("yyyyMMdd")));
            return _cacheManager.Get<IEnumerable<DocumentCopy>>(cacheKey, CacheParam.StatisticCacheTimeOut, () =>
            {
                return GetsForStatistic(hasOldDocument, from, to);
            });
        }

        /// <summary>
        /// Trả về danh sách tất cả các DocumentCopy trong khoảng thời gian lấy báo cáo: tiếp nhận trong kỳ + tồn kỳ trước
        /// </summary>
        /// <param name="hasOldDocument">Giá trị xác định có lấy văn bản tồn kỳ trước không.</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<DocumentCopy> GetsForStatistic(bool hasOldDocument, DateTime? from, DateTime to)
        {

            var ignoreUser = _adminSettings.UserIgnoreOverdueId;

#if HoSoMotCuaEdition
            var documentCopies = _documentCopyRepository.GetsReadOnly(d =>
                                d.Document.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc
                                && (ignoreUser == 0 || d.UserCurrentId != ignoreUser)
                                && !d.Document.IsConverted
                                && (d.Document.Status != (int)DocumentStatus.DuThao && d.Document.Status != (int)DocumentStatus.LoaiBo)
                                && (d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh || d.DocumentCopyType == (int)DocumentCopyTypes.ChoKetQuaDungXuLy)
                                && d.Document.DocTypeId.HasValue
                                && d.Document.DateAppointed.HasValue
                                && (!from.HasValue || d.Document.DateCreated >= from)
                                && d.Document.DateCreated <= to
                                ).ToList();
#else
            var documentCopies = _documentCopyRepository.GetsReadOnly(d =>
                                (d.Document.CategoryBusinessId == (int)CategoryBusinessTypes.VbDen
                                    || d.Document.CategoryBusinessId == (int)CategoryBusinessTypes.VbDi)
                                && (ignoreUser == 0 || d.UserCurrentId != ignoreUser)
                                && !d.Document.IsConverted
                                && (d.Document.Status != (int)DocumentStatus.DuThao && d.Document.Status != (int)DocumentStatus.LoaiBo)
                                && (d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh || d.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy)
                                && d.Document.DocTypeId.HasValue
                                && d.Document.DateAppointed.HasValue
                                && (!from.HasValue || d.DateCreated >= from)
                                && d.DateCreated <= to
                                ).ToList();
#endif

            if (hasOldDocument && from.HasValue)
            {
                documentCopies.AddRange(GetPreExtistingDocuments(from.Value));
            }

            documentCopies = TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(documentCopies, to);

            return documentCopies;
        }

        /// <summary>
        /// Trả về danh sách hồ sơ tồn kỳ trước: chưa được xử lý ở kỳ trước
        /// </summary>
        /// <param name="to">Mốc thời gian</param>
        /// <returns></returns>
        private IEnumerable<DocumentCopy> GetPreExtistingDocuments(DateTime to)
        {
            var docs = _documentCopyRepository.GetsReadOnly(d =>
                                !d.Document.IsConverted &&
                                d.Document.DateAppointed.HasValue &&
                                (d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh || d.DocumentCopyType == (int)DocumentCopyTypes.ChoKetQuaDungXuLy) &&
                                d.Document.DocTypeId.HasValue &&
                                (d.Document.Status != (int)DocumentStatus.DuThao && d.Document.Status != (int)DocumentStatus.LoaiBo)
                                && d.Document.DateCreated < to).ToList();

            var documentCopyIds = new List<int>();

            docs = TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(docs, to);
            documentCopyIds.AddRange(docs.Where(d => StatisticUtil.OverdueStatus(d.Document.Status, d.Document.DateAppointed.Value, d.Document.DateSuccess,
                    d.Document.DateReturned, d.Document.DateFinished, d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.Overdue).Select(d => d.DocumentCopyId));
            documentCopyIds.AddRange(docs.Where(d => StatisticUtil.OverdueStatus(d.Document.Status, d.Document.DateAppointed.Value, d.Document.DateSuccess,
                    d.Document.DateReturned, d.Document.DateFinished, d.Document.DateRequireSupplementary, to, d.Document.DocCode) == OverdueStatusType.Pending).Select(d => d.DocumentCopyId));

            //Todo: cần Clone lại cái docs ở trên xong select dưới đây.
            var result = _documentCopyRepository.GetsReadOnly(d => documentCopyIds.Contains(d.DocumentCopyId));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docs"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private List<DocumentCopy> TinhLaiThoiGianXuLyTheoKhoangThoiGianLayThongKe(List<DocumentCopy> docs, DateTime to)
        {
            // Chỉ tính xử lý <= to
            foreach (var doc in docs)
            {
                var document = doc.Document;
                var dateFinish = doc.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy
                                 ? doc.DateFinished
                                 : document.DateFinished;

                if (document.DateSuccess.HasValue && document.DateSuccess.Value > to)
                {
                    document.DateSuccess = null;
                    document.IsSuccess = null;
                }

                if (document.DateReturned.HasValue && document.DateReturned.Value > to)
                {
                    document.DateReturned = null;
                    document.IsReturned = null;
                }

                if (dateFinish.HasValue && dateFinish.Value > to)
                {
                    if (doc.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
                    {
                        doc.DateFinished = null;
                        doc.Status = (int)DocumentStatus.DangXuLy;
                    }
                    else
                    {
                        document.DateFinished = null;
                        document.Status = (int)DocumentStatus.DangXuLy;
                    }
                }

                if (document.DateRequireSupplementary.HasValue && document.DateRequireSupplementary.Value > to)
                {
                    document.DateRequireSupplementary = null;
                    document.Status = (int)DocumentStatus.DangXuLy;
                }

                if (document.Status == (int)DocumentStatus.DungXuLy && document.DateSuccess == null && !document.DateRequireSupplementary.HasValue)
                {
                    // Truong hop Duyet xong gui tra ket qua: chuyen trang thai Dung xu ly
                    document.Status = (int)DocumentStatus.DangXuLy;
                }
            }

            return docs;
        }

        private string GetDateFinished(DateTime? dateSuccess, DateTime? dateReturned, DateTime? dateFinished)
        {
            var dateformat = "dd/MM/yyyy";
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

        /// <summary>
        /// Lây danh sách văn bản quá hạn theo người dùng
        /// </summary>
        /// <param name="userId">Người dùng hiện tại</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetDocumentOverdues(int userId, DateTime from, DateTime to)
        {
            var docCopyDangXuLys = _documentCopyRepository.GetsReadOnly(d =>
                                (d.Status == (int)DocumentStatus.DangXuLy || d.Status == (int)DocumentStatus.DungXuLy)
                                && d.NodeCurrentId.HasValue
                                && d.DateCreated >= from
                                && d.DateCreated <= to
                                && d.UserCurrentId == userId);

            return ParseDocumentOverdueList(docCopyDangXuLys);
        }

        /// <summary>
        /// Trả về danh sách tất cả các document đang xử lý mà người dùng hiện tại gửi đi.
        /// </summary>
        /// <param name="userId">Người dùng hiện tại</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<DocumentOverdue> GetFollowDocuments(int userId, DateTime from, DateTime to)
        {
            var result = new List<DocumentOverdue>();
            var docTimeLines = _docTimelineService.Gets(dt =>
                        dt.UserSendId == userId &&
                        dt.FromDate >= from &&
                        dt.FromDate <= to &&
                        (dt.DocumentCopyType != (int)DocumentCopyTypes.ThongBao || dt.DocumentCopyType != (int)DocumentCopyTypes.DuyetGiaHan));

            if (docTimeLines.Any())
            {
                var documentCopyIds = docTimeLines.Select(dt => dt.DocumentCopyId).Distinct();
                var documentCopys = _documentCopyRepository.GetsReadOnly(dc =>
                            documentCopyIds.Contains(dc.DocumentCopyId) &&
                            (dc.Status == (int)DocumentStatus.DangXuLy || dc.Status == (int)DocumentStatus.DungXuLy) &&
                            dc.UserCurrentId != userId
                    );

                foreach (var documentCopy in documentCopys)
                {
                    var document = documentCopy.Document;
                    var docTimeLine = docTimeLines.Where(dt => dt.DocumentCopyId == documentCopy.DocumentCopyId)
                                .OrderBy(dt => dt.DateOverdue).ThenBy(dt => dt.FromDate)
                                .LastOrDefault();
                    if (docTimeLine == null)
                    {
                        continue;
                    }

                    var currentUser = _userService.GetCacheAllUsers().SingleOrDefault(u => u.UserId == docTimeLine.UserId);
                    if (currentUser == null)
                    {
                        continue;
                    }

                    result.Add(new DocumentOverdue()
                    {
                        DocumentCopyId = documentCopy.DocumentCopyId,
                        Compendium = document.CategoryBusinessId == 4 ? document.DocType.DocTypeName : document.Compendium,
                        DoctypeId = document.DocType == null ? Guid.Empty : document.DocType.DocTypeId,
                        DoctypeName = document.DocType == null ? "" : document.DocType.DocTypeName,
                        DocCode = document.DocCode,
                        CitizenName = document.CitizenName,
                        DateAppointed = docTimeLine.DateOverdue.HasValue ? docTimeLine.DateOverdue.Value.ToString("dd/MM/yyyy") : "",
                        DateCreated = document.DateCreated.ToString("dd/MM/yyyy"),
                        CurrentUser = currentUser.Username,
                        CurrentUserId = docTimeLine.UserId,
                        UserIdCreated = document.UserCreatedId,
                        CategoryBusinessId = document.CategoryBusinessId,
                        CurrentNodeKeepTime = (DateTime.Now - docTimeLine.FromDate).Days,
                        CurrentNodePermitTime = docTimeLine.DateOverdue.HasValue ? (docTimeLine.DateOverdue.Value - docTimeLine.FromDate).Days : 0,// Do trong db luôn lưu dạng giờ nên / 24
                    });
                }
            }

            return result;
        }

        private IEnumerable<DocumentOverdue> ParseDocumentOverdueList(IEnumerable<DocumentCopy> docs)
        {
            //Danh sách văn bản đang ở mục xử lý
            if (docs == null || !docs.Any())
            {
                return new List<DocumentOverdue>();
            }

            var dateNow = DateTime.Now;
            var result = new List<DocumentOverdue>();
            var workflowIds = docs.Select(p => p.WorkflowId).Distinct();
            var workFlows = _workflowService.Gets(p => workflowIds.Contains(p.WorkflowId));

            if (workFlows == null || !workFlows.Any())
            {
                throw new ApplicationException("WorkFlow is not exist.");
            }

            foreach (var docCopy in docs)
            {
                var document = docCopy.Document;
                if (!document.DateAppointed.HasValue || !docCopy.DateOverdue.HasValue)
                {
                    continue;
                }

                var workFlow = workFlows.FirstOrDefault(p => p.WorkflowId == docCopy.WorkflowId);
                if (workFlow == null)
                {
                    continue;
                }

                Node currentNode = null;
                try
                {
                    currentNode = _workflowHelper.GetNode(workFlow, docCopy.NodeCurrentId.Value);
                    if (currentNode == null)
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }

                var dateAppoint = document.DateAppointed.Value;
                // var dateOverdue = docCopy.DateOverdue.Value;

                var isProcessing = document.Status == (int)DocumentStatus.DangXuLy || document.Status == (int)DocumentStatus.DungXuLy;

                var dateProcess = isProcessing ? DateTime.Now : document.DateFinished.Value;

                var currentNodeKeepTime = (dateProcess - docCopy.DateReceived).Days + 1;
                var totalKeepTime = (dateProcess - document.DateCreated).Days + 1;

                var stopProcessTime = 0; // GetStopProcessDays(docCopy, workFlow);
                totalKeepTime = totalKeepTime - stopProcessTime;

                var currentDepartment = document.CategoryBusinessId == 4
                            ? document.InOutPlace
                            : docCopy.UserCurrent.UserDepartmentJobTitless.Any() ? docCopy.UserCurrent.UserDepartmentJobTitless.First().Department.DepartmentPath : "";

                var docOverDue = new DocumentOverdue()
                {
                    DocumentCopyId = docCopy.DocumentCopyId,
                    Compendium = document.CategoryBusinessId == 4 ? document.DocType.DocTypeName : document.Compendium,
                    DoctypeId = document.DocType.DocTypeId,
                    DoctypeName = document.DocType.DocTypeName,
                    DocCode = document.DocCode,
                    CitizenName = document.CitizenName,
                    DateAppointed = document.DateAppointed.HasValue ? document.DateAppointed.Value.ToString("dd/MM/yyyy") : "",
                    DateCreated = document.DateCreated.ToString("dd/MM/yyyy"),
                    CurrentUser = docCopy.UserCurrent.Username, // + " - " + docCopy.UserCurrent.FullName
                    CurrentUserId = docCopy.UserCurrentId,
                    UserIdCreated = document.UserCreatedId,
                    CategoryBusinessId = document.CategoryBusinessId,
                    CurrentNodeKeepTime = currentNodeKeepTime,
                    CurrentNodePermitTime = currentNode.TimeInNode / 24,// Do trong db luôn lưu dạng giờ nên / 24
                    TotalKeepTime = totalKeepTime,
                    TotalPermitTime = document.ExpireProcess ?? 0,
                    Deadline = document.ExpireProcess.HasValue ? (totalKeepTime - document.ExpireProcess.Value) : 0,
                    DeadlineCurrent = currentNodeKeepTime - (currentNode.TimeInNode / 24),
                    CurrentDepartmentExt = currentDepartment,
                    IsProcessing = isProcessing
                };

                result.Add(docOverDue);
            }

            return result.ToList();
        }

        private IEnumerable<DocumentOverdue> ParseDocumentOverdueListNotWithWorkflow(IEnumerable<DocumentCopy> documentCopies)
        {
            var result = new List<DocumentOverdue>();
            documentCopies = documentCopies.OrderBy(d => d.Document.DocCode);
            var doctypeIds = documentCopies.Select(d => d.DocTypeId).Distinct();
            var doctypes = _doctypeService.Gets(dt => doctypeIds.Contains(dt.DocTypeId));
            foreach (var documentCopy in documentCopies)
            {
                var document = documentCopy.Document;
                var doctype = doctypes.Single(dt => dt.DocTypeId == documentCopy.DocTypeId);
                if (doctype == null)
                {
                    continue;
                }

                var processUserId = document.IsSuccess.HasValue ? document.UserSuccessId : documentCopy.UserCurrentId;
                var currentUser = _userService.GetCacheAllUsers().SingleOrDefault(u => u.UserId == processUserId);
                if (currentUser == null)
                {
                    continue;
                }

                var isProcessing = document.Status == (int)DocumentStatus.DangXuLy || document.Status == (int)DocumentStatus.DungXuLy;
                var dateProcess = isProcessing ? DateTime.Now
                                    : (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy
                                        ? documentCopy.DateFinished ?? DateTime.Now
                                        : document.DateFinished ?? DateTime.Now);

                result.Add(new DocumentOverdue()
                {
                    DocumentCopyId = documentCopy.DocumentCopyId,
                    Compendium = document.CategoryBusinessId == 4 ? doctype.DocTypeName : document.Compendium,
                    DoctypeId = doctype == null ? Guid.Empty : doctype.DocTypeId,
                    DoctypeName = doctype == null ? "" : doctype.DocTypeName,
                    DocCode = document.DocCode,
                    CitizenName = document.CitizenName,
                    DateAppointed = document.DateAppointed.HasValue ? document.DateAppointed.Value.ToString("dd/MM/yyyy") : "",
                    DateFinished = GetDateFinished(document.DateSuccess, document.DateReturned,
                                        documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy ? documentCopy.DateFinished : document.DateFinished),
                    DateCreated = document.DateCreated.ToString("dd/MM/yyyy"),
                    DateSuccess = document.DateSuccess.HasValue ? document.DateSuccess.Value.ToString("dd/MM/yyyy") : "",
                    CurrentUser = string.Format("{0} ({1})", currentUser.FullName, currentUser.Username),
                    CurrentUserId = documentCopy.UserCurrentId,
                    UserIdCreated = document.UserCreatedId,
                    CategoryBusinessId = document.CategoryBusinessId,
                    CurrentDepartmentExt = document.InOutPlace,
                    Deadline = !document.DateAppointed.HasValue
                                    ? 0
                                    : (int)(dateProcess - document.DateAppointed.Value).TotalDays,
                });
            }

            return result;
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<IDictionary<string, object>> GetActiveDocuments(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT dc.DocumentCopyId,dc.DocumentId,dc.Status,dc.DateOverdue,dc.DateModified,dc.UserCurrentId,d.DocCode,d.Compendium,u.UserName from documentcopy dc inner join document d on dc.DocumentId=d.DocumentId inner join user u on d.UserCreatedId=u.UserId where dc.DateModified >= @startDate and dc.DateModified <= @endDate and dc.DocumentCopyType in (1, 2, 4, 32, 64)";
            var parameters = new List<Object>
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            var result = Context.RawQuery(sql, parameters.ToArray()) as IEnumerable<IDictionary<string, object>>;
            return result;
        }

        /// <summary>
        /// Trả về danh sách các văn bản liên thông đến mới cập nhật
        /// </summary>
        /// <param name="lastUpdate">Lần cập nhật gần nhất</param>
        public IEnumerable<DocumentCopy> GetDocumentLienThongModified(DateTime? lastUpdate)
        {
            return _documentCopyRepository.Gets(true, d => (!lastUpdate.HasValue || (d.DateModified.HasValue
                                                            && d.DateModified.Value >= lastUpdate))
                                                            && d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh
                                                            && d.Document.Original == 2);
        }

        #endregion

        /// <summary>
        /// Cập nhật kết quả dừng xử lý
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <param name="comment"></param>
        /// <param name="dateAppointed"></param>
        public void ContinueProcess(int documentCopyId, string comment, DateTime dateAppointed)
        {
            var documentCopy = Get(documentCopyId);
            if (documentCopy == null)
            {
                throw new Exception("Hồ sơ yêu cầu không tồn lại, vui lòng thử lại");
            }

            var document = documentCopy.Document;
            var docPublishes = _docPublishService.GetSentPublishes(documentCopyId);

            // Cập nhật các hướng đang liên thông
            foreach (var docPublish in docPublishes)
            {
                if (docPublish.IsResponsed)
                {
                    continue;
                }

                docPublish.IsResponsed = true;
                docPublish.DateResponsed = DateTime.Now;
                docPublish.Note = comment;
            }

            // Cập nhật document
            documentCopy.DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh;
            documentCopy.Status = (int)DocumentStatus.DangXuLy;
            document.Status = (int)DocumentStatus.DangXuLy;
            document.DateRequireSupplementary = null;
            document.DateAppointed = dateAppointed;
            SaveChanges();
        }

        /// <summary>
        /// Xet trạng thái chưa đọc cho user hiện tại
        /// </summary>
        /// <param name="documentCopy">hồ sơ</param>
        public void SetCurrentUserUnread(DocumentCopy documentCopy)
        {
            if (documentCopy == null)
            {
                return;
            }

            UpdateUserDaXem(documentCopy, null, new List<int>() { documentCopy.UserCurrentId });

            //var currentUserId = documentCopy.UserCurrentId;

            //var docFinish = _docFinishService.Get(documentCopy.DocumentCopyId, currentUserId);
            //if (docFinish != null)
            //{
            //    docFinish.IsViewed = false;
            //}

            //Context.SaveChanges();
        }
    }
}