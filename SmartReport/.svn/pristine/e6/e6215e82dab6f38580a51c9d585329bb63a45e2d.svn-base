using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Search;

namespace Bkav.eGovCloud.Api.Service
{
    public class DocumentApiService
    {
        private readonly AttachmentBll _attachmentService;
        private readonly AuthorizeBll _authorizeService;
        private readonly CategoryBll _categoryService;
        private readonly CodeBll _codeService;
        private readonly CommentBll _commentService;
        private readonly DepartmentBll _deptService;
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocRelationBll _docRelationService;
        private readonly ISearchInDatabase _searchInDatabaseService;
        private readonly LogBll _logService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;
        private readonly WorkflowBll _workflowService;
        private readonly WorkflowHelper _workflowHelper;
        private readonly WorktimeHelper _workTimeHelper;
        private readonly CBCLSetting _cbclSettings;

        /// <summary>
        /// C'tor
        /// </summary>
        public DocumentApiService()
        {
            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
            _docTypeService = DependencyResolver.Current.GetService<DocTypeBll>();
            _docCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _deptService = DependencyResolver.Current.GetService<DepartmentBll>();
            _categoryService = DependencyResolver.Current.GetService<CategoryBll>();
            _logService = DependencyResolver.Current.GetService<LogBll>();
            _workflowHelper = DependencyResolver.Current.GetService<WorkflowHelper>();
            _workflowService = DependencyResolver.Current.GetService<WorkflowBll>();
            _authorizeService = DependencyResolver.Current.GetService<AuthorizeBll>();
            _codeService = DependencyResolver.Current.GetService<CodeBll>();
            _docRelationService = DependencyResolver.Current.GetService<DocRelationBll>();
            _commentService = DependencyResolver.Current.GetService<CommentBll>();
            _positionService = DependencyResolver.Current.GetService<PositionBll>();
            _workTimeHelper = DependencyResolver.Current.GetService<WorktimeHelper>();
            _searchInDatabaseService = DependencyResolver.Current.GetService<ISearchInDatabase>();
            _cbclSettings = DependencyResolver.Current.GetService<CBCLSetting>();
        }

        #region Service

        /// <summary>
        /// Tạo documentCopy
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="tempFiles"></param>
        /// <param name="copyAttachments"></param>
        /// <param name="document"></param>
        /// <param name="sendNode"></param>
        /// <param name="userSendId"></param>
        /// <param name="receiveNode"></param>
        /// <param name="toUsers"></param>
        /// <param name="status"></param>
        /// <param name="newDocument"></param>
        /// <param name="newDocumentCopy"></param>
        /// <param name="deleteTempfiles"></param>
        public void CreateDocumentDefault(string comment, IDictionary<string, IDictionary<string, string>> tempFiles,
            IEnumerable<Entities.Customer.Attachment> copyAttachments, Document document,
            Node sendNode, int userSendId, Node receiveNode, IEnumerable<User> toUsers, DocumentStatus status,
            out Document newDocument, out DocumentCopy newDocumentCopy,
            bool deleteTempfiles = true)
        {
            const DocumentStatus statusCheck = DocumentStatus.DuThao | DocumentStatus.DangXuLy;
            if (!EnumHelper<DocumentStatus>.ContainFlags(statusCheck, status))
            {
                throw new ArgumentException("status chỉ được phép là DocumentStatus.DuThao | DocumentStatus.DangXuLy.");
            }
            newDocument = null;
            newDocument = CreateDocument(tempFiles, copyAttachments, document, status, document.DateCreated, deleteTempfiles);
            var documentCopyTypes = receiveNode.GetNodePermission().HasFlag(NodePermissions.QuyenDungXuLy)
                ? DocumentCopyTypes.ChoKetQuaDungXuLy : DocumentCopyTypes.XuLyChinh;

            var newDocumentIdRelation = newDocument.DocumentId;

            var relations = document.DocRelations.Select(c => new DocRelation
            {
                DocumentId = newDocumentIdRelation,
                RelationId = c.RelationId,
                RelationCopyId = c.RelationCopyId,
                RelationType = c.RelationType
            }).ToList();

            var user = toUsers.First();
            newDocumentCopy = null;
            newDocumentCopy = _docCopyService.Create(document.DocumentId, document.DocTypeId.Value, sendNode, userSendId, receiveNode,
                                           user.UserId, null, document.DateCreated, documentCopyTypes, status, relations);

            //newDocumentCopy.Document = document;
            _docCopyService.UpdateDateOverdue(newDocumentCopy, receiveNode);

            foreach (var rel in relations)
            {
                _docRelationService.Create(new DocRelation
                {
                    RelationId = newDocumentCopy.DocumentId,
                    RelationCopyId = newDocumentCopy.DocumentCopyId,
                    DocumentId = rel.RelationId,
                    DocumentCopyId = rel.RelationCopyId,
                    RelationType = rel.RelationType,
                    Compendium = document.Compendium,
                    DocCode = document.DocCode,
                    InOutCode = document.InOutCode,
                    DateArrived = document.DateArrived
                });
            }

            var from = _userService.GetFromCache(userSendId);
            if (from != null)
            {
                newDocumentCopy.LastComment = comment;
                newDocumentCopy.LastDateComment = document.DateCreated;
                newDocumentCopy.LastUserComment = from.FullName;
                newDocumentCopy.LastUserIdComment = userSendId;
                _docCopyService.Update(newDocumentCopy);
                var commentTransfer = new List<CommentTransfer>
                        {
                            new CommentTransfer{
                                Label=user.FullName,
                                Type="1",
                            }
                        };
                _commentService.SendTransfer(newDocumentCopy, userSendId, user.UserId, comment, commentTransfer, document.DateCreated, "", newDocumentCopy.DateOverdue);
            }
        }

        /// <summary>
        /// Nhận văn bản từ hệ thống khác
        /// </summary>
        /// <param name="doc">ReceivedDocument</param>
        /// <returns>documentcopy id</returns>
        public int ReceiveDocument(ReceivedDocument doc)
        {
            try
            {
                var tempDoc = new ReceivedDocument(doc);
                CreateLog(tempDoc);
                var model = MappingDocument(doc);
                var fromUser = _userService.GetByUserName(doc.From);
                var toUsers = _userService.Gets(doc.To);

                var userSendId = fromUser != null ? fromUser.UserId : 0;

                var workflow = _docTypeService.GetWorkflowActive(model.DocTypeId.Value);
                var startNodes = _workflowHelper.GetStartNodes(workflow, userSendId);
                var receivedNode = _workflowHelper.GetNode(workflow, doc.NodeReceived);

                model.DateAppointed = model.DateCreated.AddDays(workflow.ExpireProcess);
                var nodeSend = startNodes.First();
                var tempPath = ResourceLocation.Default.FileUploadTemp;
                var newAttachments = new Dictionary<string, IDictionary<string, string>>();
                if (doc.Attachments != null && doc.Attachments.Any())
                {
                    foreach (var attachment in doc.Attachments)
                    {
                        var data = Convert.FromBase64String(attachment.Data);
                        var stream = new MemoryStream(data);
                        var fileInfo = FileManager.Default.Create(stream, tempPath);
                        var tempDic = new Dictionary<string, string>();
                        tempDic.Add("name", attachment.Name);
                        newAttachments.Add(fileInfo.Name, tempDic);
                    }
                }
                DocumentCopy documentCopyIsTransfering;
                Document documentIsTransfering;
                CreateDocumentDefault(doc.Comment, newAttachments, null, model, nodeSend, userSendId, receivedNode, toUsers,
                        DocumentStatus.DangXuLy, out documentIsTransfering, out documentCopyIsTransfering, false);
                return documentCopyIsTransfering.DocumentCopyId;
            }
            catch (DbEntityValidationException dbEx)
            {
                _logService.Error(dbEx.Message, dbEx);
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                _logService.Error(e.Message, e);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// lấy danh sách văn bản quá hạn
        /// </summary>
        /// <returns>IEnumerable<OverdueDocument></returns>
        public List<OverdueDocument> GetOverDueDocuments()
        {
            var dxlStatus = (int)DocumentStatus.DangXuLy;
            var docCopys = _docCopyService.Gets(d => d.Status == dxlStatus).Where(d => d.Document != null && d.NodeCurrentId != null);
            var overDueDocuments = new List<OverdueDocument>();

            foreach (var docCopy in docCopys)
            {
                var workFlow = _workflowService.Get(docCopy.WorkflowId);
                if (workFlow != null)
                {
                    //HopCV
                    //todo:Chỗ này mục đích là lấy ra danh sách văn bản quá hạn
                    // Đầu tiên là kiểm tra overdue trong bảng documentCopy xem có quá hạn hay không
                    // sao không add luôn vào danh sách văn bản quá hạn luôn mà lại kiểm tra đủ các kiểu rồi lại có điều kiện or với thằng overdue?
                    //QuangP: lý do là vì có kiểm tra trước và add vào luôn thì cũng vẫn phải tính giời gian giữ trên node và tổng thời gian xử lý.
                    //tách ra thành 2 câu if: so sánh với dateoverdue trước=>add vào + tổng thời gian xl => add vào, xong dưới lại foreach cái list đó ra để tính thời gian xl còn loằng ngoằng hơn mà không thấy có tí tối ưu (nhanh) nào hơn
                    Node currentNode;
                    try
                    {
                        currentNode = _workflowHelper.GetNode(workFlow, (int)docCopy.NodeCurrentId);
                    }
                    catch
                    {
                        currentNode = null;
                    }
                    if (currentNode != null)
                    {
                        var currentNodeKeepTime = (DateTime.Now - docCopy.DateReceived).Days;
                        var totalKeepTime = (DateTime.Now - docCopy.DateCreated).Days;

                        //Kiểm tra trong thời gian giữ văn bản nếu có ngày nghỉ lễ thì trừ ngày đó đi
                        var holidaysInCurrentNodeKeepTime = 0;
                        for (int i = 0; i < currentNodeKeepTime; i++)
                        {
                            var date = docCopy.DateReceived.AddDays(i + 1);
                            if (_workTimeHelper.IsWeekendOrHoliday(date))
                            {
                                holidaysInCurrentNodeKeepTime++;
                            }
                        }

                        var holidaysInTotalKeepTime = 0;
                        for (int i = 0; i < totalKeepTime; i++)
                        {
                            var date = docCopy.DateCreated.AddDays(i + 1);
                            if (_workTimeHelper.IsWeekendOrHoliday(date))
                            {
                                holidaysInTotalKeepTime++;
                            }
                        }

                        //Thời gian ở node dừng xử lý
                        var stopProcessTime = _docCopyService.GetStopProcessDays(docCopy, workFlow);
                        //+1 do giữ quá 24 giờ
                        totalKeepTime = totalKeepTime - holidaysInTotalKeepTime - stopProcessTime + 1;
                        currentNodeKeepTime = currentNodeKeepTime - holidaysInCurrentNodeKeepTime + 1;

                        if (!currentNode.StopProcess && (docCopy.DateOverdue != null && DateTime.Now > docCopy.DateOverdue) || totalKeepTime > workFlow.ExpireProcess)
                        {
                            var userCreate = _userService.GetFromCache(docCopy.Document.UserCreatedId);
                            overDueDocuments.Add(new OverdueDocument
                            {
                                DocumentCopyId = docCopy.DocumentCopyId,
                                Compendium = docCopy.Document.Compendium,
                                UserCreated = userCreate != null ? userCreate.UsernameEmailDomain : docCopy.Document.Organization,//trường hợp nhân viên mới ko có trong db, trả về tên bwss truyền qua lúc khởi tạo
                                CurrentUser = docCopy.UserCurrentName,
                                CurrentDepartment = _deptService.GetsPath(docCopy.UserCurrentId).ToList(),
                                CurrentNodeKeepTime = currentNodeKeepTime,
                                CurrentNodePermitTime = currentNode.TimeInNode / 24,// Do trong db luôn lưu dạng giờ nên / 24
                                TotalKeepTime = totalKeepTime,
                                TotalPermitTime = workFlow.ExpireProcess
                            });
                        }
                    }
                }
            }
            return overDueDocuments;
        }

        /// <summary>
        /// lấy danh sách văn bản mới xử lý từ startDate => Now
        /// </summary>
        /// <param name="startDate">Thời gian bắt đầu lấy</param>
        /// <returns>danh sách văn bản</returns>
        public List<ActiveDocumentDto> GetActiveDocuments(DateTime startDate, DateTime endDate)
        {
            //var docCopys = _docCopyService.Gets(d => d.DateModified != null && d.DateModified >= startDate && d.DateModified <= endDate)
            //    .Where(d => d.Document != null && d.NodeCurrentId != null);
            var docCopys = _docCopyService.GetActiveDocuments(startDate, endDate);
            var activeDocuments = new List<ActiveDocumentDto>();
            var docs = new List<DocumentCopy>();
            foreach (var doc in docCopys)
            {
                var userCurrent = _userService.GetFromCache(Convert.ToInt32(doc["UserCurrentId"].ToString()));
                DateTime? expiredDate = null;
                if (doc["DateOverdue"] != null)
                {
                    expiredDate = DateTime.Parse(doc["DateOverdue"].ToString());
                }
                activeDocuments.Add(new ActiveDocumentDto
                {
                    DocumentCopyId = Convert.ToInt32(doc["DocumentCopyId"].ToString()),
                    Compendium = doc["Compendium"].ToString(),
                    Status = Convert.ToInt32(doc["Status"].ToString()),
                    ExpiredDate = expiredDate,
                    CurrentUser = userCurrent.Username,
                    CreatedUser = doc["UserName"].ToString(),
                    DocCode = doc["DocCode"] != null ? doc["DocCode"].ToString() : null,
                    DateModified = DateTime.Parse(doc["DateModified"].ToString())
                });
            }
            return activeDocuments;
        }

        /// <summary>
        /// Lấy ra danh sách các hướng chuyển
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<ActionDto> GetActionList(Guid docTypeId, string username)
        {
            var user = _userService.GetByUserName(username);
            var workflow = _docTypeService.GetWorkflowActive(docTypeId);
            var actionsInWorkflow = _workflowHelper.GetActionsCreate(workflow, user.UserId).ToList();
            RemoveActionsSpecial(ref actionsInWorkflow);
            RemoveActionsForSupplementary(workflow, ref actionsInWorkflow);
            var result = new List<ActionDto>();
            if (actionsInWorkflow.Any())
            {
                foreach (var action in actionsInWorkflow)
                {
                    result.Add(new ActionDto
                    {
                        Id = action.Id,
                        Name = action.Name,
                        NextNodeId = action.Next,
                        CurrentNodeId = action.Current,
                        Priority = GetActionPriority(action.Id, action.IsSpecial)
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy ra danh sách người dùng theo hướng chuyển
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="actionId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<UserDto> GetUsersByAction(Guid docTypeId, string actionId, string username)
        {
            var user = _userService.GetByUserName(username);
            var workflow = _docTypeService.GetWorkflowActive(docTypeId);
            var listUserIdByAction = _workflowHelper.GetUsersByActionId(workflow.WorkflowId, actionId, user.UserId);
            var departmentsUser = _deptService.GetCacheAllUserDepartmentJobTitlesPosition();
            var departments = _deptService.GetCacheAllDepartments();
            var positions = _positionService.GetCacheAllPosition();

            var userByAction = _userService.GetAllCached(true)
                                    .Where(u => listUserIdByAction.Contains(u.UserId))
                                    .Select(u =>
                                    {
                                        var dept = departmentsUser.FirstOrDefault(d => d.UserId == u.UserId);
                                        var deptName = string.Empty;
                                        var position = string.Empty;
                                        if (dept != null)
                                        {
                                            var dept1 = departments.SingleOrDefault(d => d.DepartmentId == dept.DepartmentId);
                                            deptName = dept1 == null ? string.Empty : dept1.DepartmentName;

                                            var pos = positions.SingleOrDefault(p => p.PositionId == dept.PositionId);
                                            position = pos == null ? string.Empty : pos.PositionName;
                                        }

                                        return new UserDto
                                        {
                                            Username = u.UsernameEmailDomain,
                                            FullName = u.FullName,
                                            Position = position
                                        };
                                    })
                                    .OrderBy(u => u.Username).ToList();
            return userByAction;
        }

        /// <summary>
        /// Tìm văn bản liên quan
        /// </summary>
        /// <param name="username"></param>
        /// <param name="compendium"></param>
        /// <param name="docCode"></param>
        /// <returns></returns>
        public List<RelationDocumentDto> GetRelationDocuments(string username, string compendium, string docCode)
        {
            var user = _userService.GetByUserName(username);
            var docs = _searchInDatabaseService.SearchAdvanceInDatabase(user.UserId, compendium, null, docCode: docCode);
            var result = docs.Items.Select(d =>
            {
                return new RelationDocumentDto
                {
                    DocCode = d.ExtendInfo.GetType().GetProperty("DocCode").GetValue(d.ExtendInfo, null),
                    Compendium = d.DocumentCompendium,
                    DocumentId = d.DocumentId,
                    DocumentCopyId = d.DocumentCopyId
                };
            }).ToList();

            return result;
        }

        /// <summary>
        /// Uỷ quyền
        /// </summary>
        /// <param name="auth">entity Authorize</param>
        /// <returns></returns>
        public bool Authorize(AuthorizeDto auth)
        {
            try
            {
                var authorizeUser = _userService.Get(auth.AuthorizeUser);
                var authorizedUser = _userService.Get(auth.AuthorizedUser);
                if (authorizeUser != null && authorizedUser != null)
                {
                    _authorizeService.Create(new Authorize
                    {
                        AuthorizeUserId = authorizeUser.UserId,
                        AuthorizedUserId = authorizedUser.UserId,
                        DateBegin = auth.StartDate,
                        DateEnd = auth.EndDate,
                        Active = auth.EndDate > DateTime.Now
                    });
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy ra vết xử lý theo DocumentCopyId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DocumentTransferHistoryDto GetDocumentTransferHistory(int id)
        {
            var comments = _commentService.Gets(true, c => c.DocumentCopyId == id);

            var result = new DocumentTransferHistoryDto();
            try
            {
                foreach (var comment in comments)
                {
                    if (comment.UserSendId.HasValue && comment.UserReceiveId.HasValue)
                    {
                        var userSend = _userService.GetFromCache(comment.UserSendId.Value);
                        var userReiceived = _userService.GetFromCache(comment.UserReceiveId.Value);
                        result.UserTransferHistorys.Add(
                            new UserTransferHistory
                            {
                                UserSend = userSend.Username,
                                TransferTime = comment.DateCreated,
                                UserReiceived = userReiceived.Username
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result.Error = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Lấy ra doctypeId được cấu hình
        /// </summary>
        /// <returns></returns>
        public Guid GetDoctypeId()
        {
            if (_cbclSettings.DoctypeConfig != null)
            {
                var doctypeid = _cbclSettings.DoctypeConfig;
                var name = Guid.Parse(doctypeid);
                return name;
            }
            else
            {
                return new Guid();
            }
        }

        #endregion

        #region Private Methods

        private void CreateLog(ReceivedDocument doc)
        {
            if (doc.Attachments != null)
            {
                doc.Attachments = doc.Attachments.Select(att => new ReceivedAttachment
                {
                    Name = att.Name,
                    Data = new MemoryStream(Convert.FromBase64String(att.Data)).Length.ToString()
                });
            }
            doc.Content = "Length:" + doc.Content.Length;
            var data = doc.Stringify();
            var tempPath = ResourceLocation.Default.FileUploadTemp;
            var log = string.Empty;
            var logFile = string.Empty;
            logFile = tempPath + "\\ApiLog" + "\\InputLog" + DateTime.Now.Date.ToString("-dd-MM-yy") + ".txt";
            log = "\n\n " + doc.From + " Time: " + DateTime.Now + "\n Data: " + data;
            var logStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(log));
            using (var streamWriter = System.IO.File.AppendText(logFile))
            {
                streamWriter.WriteLine(log);
                streamWriter.Close();
            }
        }

        /// <summary>
        ///   Loại bỏ các hướng chuyển đặc biệt khi tạo mới
        /// </summary>
        /// <param name="actions"> </param>
        private void RemoveActionsSpecial(ref List<Bkav.eGovCloud.Core.Workflow.Action> actions)
        {
            var removeActions = actions.Where(action => action.IsSpecial).ToList();
            foreach (var removeAction in removeActions)
            {
                actions.Remove(removeAction);
            }
        }

        /// <summary>
        ///   Loại bỏ các hướng chuyển tới các nút "Dừng xử lý" hoặc "Tiếp nhận bổ sung"
        /// </summary>
        /// <param name="workflow"> </param>
        /// <param name="actions"> </param>
        private void RemoveActionsForSupplementary(Workflow workflow, ref List<Bkav.eGovCloud.Core.Workflow.Action> actions)
        {
            var path = workflow.JsonInObject;
            var removeActions = new List<Bkav.eGovCloud.Core.Workflow.Action>();
            foreach (var action in actions)
            {
                if (action.IsSpecial)
                {
                    continue;
                }
                var nextNode = path.GetNode(action.Next);
                if (nextNode == null)
                {
                    throw new WorkflowFormatException("action.Next bị null.");
                }
                var nextNodePermission = nextNode.GetNodePermission();
                var isRemove =
                    EnumHelper<NodePermissions>.ContainFlags(nextNodePermission, NodePermissions.QuyenDungXuLy) ||
                    EnumHelper<NodePermissions>.ContainFlags(nextNodePermission, NodePermissions.QuyenTiepNhanBoSung);
                if (isRemove)
                {
                    removeActions.Add(action);
                }
            }
            foreach (var removeAction in removeActions)
            {
                actions.Remove(removeAction);
            }
        }

        /// <summary>
        ///   Lấy mức ưu tiên của action (dùng khi sắp xếp danh sách hướng chuyển)
        /// </summary>
        /// <param name="actionId"> </param>
        /// <param name="isSpecial"> </param>
        private int GetActionPriority(string actionId, bool isSpecial)
        {
            var result = 0;
            if (!isSpecial)
            {
                result = 2;
            }
            else
            {
                if (actionId == ActionSpecial.ChuyenYKienDongGopVbDxl.ToString()
                    || actionId == ActionSpecial.ChuyenYKienDongGopVbXinYKien.ToString()
                    || actionId == ActionSpecial.ChuyenNguoiCoQuyenDongGopYKien.ToString()
                    || actionId == ActionSpecial.TiepNhanHoSo.ToString()
                    || actionId == ActionSpecial.TiepNhanHoSoVaTiepTuc.ToString()
                    || actionId == ActionSpecial.TiepTucXuLy.ToString()
                    || actionId == ActionSpecial.CapNhatKetQuaDungXuLy.ToString())
                {
                    result = 1;
                }

                if (actionId == ActionSpecial.ChuyenNguoiGui.ToString()
                    || actionId == ActionSpecial.ChuyenNguoiKhoiTao.ToString())
                {
                    result = 3;
                }

                if (actionId == ActionSpecial.LuuSoNoiBo.ToString()
                    || actionId == ActionSpecial.LuuSoVaPhatHanhNoiBo.ToString()
                    || actionId == ActionSpecial.LuuSoVaPhatHanhRaNgoai.ToString())
                {
                    result = 4;
                }
            }
            return result;
        }

        /// <summary>
        /// Map ReceivedDocument nhận từ BWSS sang document của egov
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Document MappingDocument(ReceivedDocument doc)
        {
            var attachments = new List<Attachment>();
            var fromUsers = _userService.Gets();
            var fromUser = _userService.GetByUserName(doc.From);
            var newId = Guid.NewGuid();

            if (doc.Attachments != null && doc.Attachments.Any())
            {
                var tempFiles = new Dictionary<string, IDictionary<string, string>>();
                foreach (var attachment in doc.Attachments)
                {
                    var data = Convert.FromBase64String(attachment.Data);
                    var stream = new MemoryStream(data);
                    var tempPath = ResourceLocation.Default.FileUploadTemp;
                    var fileInfo = FileManager.Default.Create(stream, tempPath);
                    var tempDic = new Dictionary<string, string>();
                    //Cấu trúc của dictionary
                    tempDic.Add("name", attachment.Name);
                    tempFiles.Add(fileInfo.Name, tempDic);
                }
                attachments = _attachmentService.AddAttachmentInDoc(tempFiles, fromUser.UserId, true);
            }
            var contents = new List<DocumentContent>();
            var docType = _docTypeService.Get(GetDoctypeId());
            if (!string.IsNullOrEmpty(doc.Content))
            {
                var documentContent = new DocumentContent
                {
                    Url = doc.Url,
                    ContentUrl = doc.ContentUrl,
                    DocumentId = newId,
                    ContentName = docType.DocTypeName,
                    Content = doc.Content,
                    FormTypeId = (int)Bkav.eGovCloud.Entities.FormType.HtmlForm,
                    FormTypeIdInEnum = Bkav.eGovCloud.Entities.FormType.HtmlForm,
                    Version = 1
                };
                documentContent.DocumentContentDetails.Add(new DocumentContentDetail
                {
                    Content = doc.Content,
                    CreatedByUserId = fromUser != null ? fromUser.UserId : 0,
                    CreatedByUserName = fromUser != null ? fromUser.Username : string.Empty,
                    CreatedOnDate = DateTime.Now,
                    Version = 1
                });
                contents.Add(documentContent);
            }
            var relations = new List<DocRelation>();

            if (doc.RelationDocumentCopysId != null)
            {
                if (doc.RelationDocumentCopysId.Any())
                {
                    foreach (var relDocCopyId in doc.RelationDocumentCopysId)
                    {
                        var relDocCopy = _docCopyService.Get(relDocCopyId);
                        if (relDocCopy != null)
                        {
                            relations.Add(new DocRelation
                            {
                                RelationType = 1,
                                RelationId = relDocCopy.DocumentId,
                                RelationCopyId = relDocCopy.DocumentCopyId
                            });
                        }
                    }
                }
            }

            var dateCreated = DateTime.Now;
            var category = _categoryService.GetsFromCache().Where(c => c.CategoryBusinessId == (int)CategoryBusinessTypes.VbDen).FirstOrDefault();

            return new Document
            {
                DocumentContents = contents,
                CategoryBusinessId = (int)CategoryBusinessTypes.VbDen,
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                DateCreated = dateCreated,
                DateModified = dateCreated,
                Compendium = doc.Compendium,
                UserCreatedId = fromUser != null ? fromUser.UserId : 0,
                Original = 2,
                DocTypeId = docType.DocTypeId,
                DocTypeName = docType.DocTypeName,
                Organization = doc.From,
                DateArrived = dateCreated,
                Attachments = attachments,
                DocumentId = newId,
                DocRelations = relations,
                UrgentId = (int)Urgent.Thuong,
                UrgentIdInEnum = Urgent.Thuong
            };
        }

        /// <summary>
        /// Tạo document
        /// </summary>
        /// <param name="tempFiles"></param>
        /// <param name="copyAttachments"></param>
        /// <param name="document"></param>
        /// <param name="status"></param>
        /// <param name="dateCreated"></param>
        /// <param name="deleteTempfiles"></param>
        /// <returns></returns>
        private Document CreateDocument(
            IDictionary<string, IDictionary<string, string>> tempFiles,
            IEnumerable<Entities.Customer.Attachment> copyAttachments,
            Document document,
            DocumentStatus status,
            DateTime dateCreated,
            bool deleteTempfiles = true)
        {
            document.Status = (byte)status;
            var docType = _docTypeService.Get(document.DocTypeId.Value);
            document.CategoryBusinessId = docType.CategoryBusinessId;
            document.CategoryId = docType.CategoryId;
            document.DocTypePermission = docType.DocTypePermission;
            // Add DocCode
            // Kiểm tra loại hồ sơ 1 cửa hoặc phát hành hoặc đánh số thì thêm DocCode, tăng số sổ hồ sơ. Hiện tại ở đây chỉ xét trường hợp HSMC. 2 Trường hợp kia xử lý ở 2 chức năng tương ứng.
            if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen)
            {
                var docCode = docType.CodeId == null
                                  ? string.Empty
                                  : _codeService.ConfirmCode(docType.CodeId.Value, dateCreated, null);

                document.DocCode = docCode;
                document.DateOfIssueCode = dateCreated;
            }

            if (copyAttachments != null)
            {
                var newAttachments = _attachmentService.CopyAttachment(copyAttachments.ToList(), document.UserCreatedId);
                foreach (var newAttachment in newAttachments)
                {
                    document.Attachments.Add(newAttachment);
                }
            }
            _documentService.Create(document);
            return document;
        }

        #endregion
    }
}