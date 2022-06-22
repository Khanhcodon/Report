using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Transactions;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Newtonsoft.Json.Linq;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class DocumentOnlineControllerOld : CustomerBaseController
    {
#if HoSoMotCuaEdition

        private readonly ResourceBll _resourceService;
        private readonly AttachmentBll _attachmentService;
        private readonly CodeBll _codeService;
        private readonly DocumentOnlineBll _docService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly FormBll _formService;
        private readonly FormHelper _formHelper;
        private readonly SendEmailHelper _mailHelper;
        private readonly WorkflowBll _workflowService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocumentBll _documentService;
        private readonly WorkflowHelper _workflowHelper;
        private readonly CitizenBll _peopleService;
        private readonly FileBll _fileService;
        private readonly SendSmsHelper _smsHelper;
        private readonly TemplateBll _templateService;
        private readonly UserBll _userService;
        private readonly NotifyConfigBll _notifyConfigService;
        private readonly WorktimeHelper _workTimeHelper;
        private readonly DocFieldBll _docFieldService;
        private readonly AddressBll _addresService;
        private readonly OnlineRegistrationSettings _onlineRegistrationSettings;
        private readonly StoreBll _storeService;

        private const string DATE_FORMAT = "dd/MM/yyyy hh:mm:ss";

        public DocumentOnlineControllerOld(
            ResourceBll resourceService,
            FileBll fileService,
            CitizenBll peopleService,
            FileUploadSettings fileUploadSettings,
            WorkflowHelper workflowHelper,
            DocumentBll documentService,
            AttachmentBll attachmentService,
            CodeBll codeService,
            DocumentCopyBll documentCopyService,
            WorkflowBll workflowService,
            SendEmailHelper mailHelper,
            DocTypeFormBll doctypeFormService,
            FormBll formService,
            DocumentOnlineBll docService,
            DocTypeBll doctypeService,
            FormHelper formHelper,
            DoctypePaperBll doctypePaperService,
            OnlineRegistrationSettings onlineRegistrationSettings,
            SendSmsHelper smsHelper,
            TemplateBll templateService,
            UserBll userService,
            WorktimeHelper workTimeHelper,
            NotifyConfigBll notifyConfigService,
            DocFieldBll docFieldService,
            AddressBll addresService,
            StoreBll storeService)
            : base()
        {
            _resourceService = resourceService;
            _fileService = fileService;
            _peopleService = peopleService;
            _workflowHelper = workflowHelper;
            _documentService = documentService;
            _attachmentService = attachmentService;
            _codeService = codeService;
            _docCopyService = documentCopyService;
            _workflowService = workflowService;
            _mailHelper = mailHelper;
            _doctypeFormService = doctypeFormService;
            _formService = formService;
            _docService = docService;
            _docTypeService = doctypeService;
            _formHelper = formHelper;
            _onlineRegistrationSettings = onlineRegistrationSettings;
            _smsHelper = smsHelper;
            _templateService = templateService;
            _userService = userService;
            _workTimeHelper = workTimeHelper;
            _notifyConfigService = notifyConfigService;
            _docFieldService = docFieldService;
            _addresService = addresService;
            _storeService = storeService;
        }

        public JsonResult Test()
        {
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegisterSigned(string model, string files)
        {
            var docModel = Json2.ParseAs<DocumentOnlineModel>(model);
            var signedFile = Json2.ParseAs<List<dynamic>>(files);
            docModel.DocType = _docTypeService.Get(docModel.DocTypeId);
            if (ModelState.IsValid)
            {
                try
                {
                    docModel.Id = Guid.NewGuid();
                    var tempPath = ResourceLocation.Default.FileUploadTemp;
                    //HopCV: Test quan ly quy trinh theo code moiw

                    var workflow = _workflowService.GetWorkflows(docModel.DocTypeId).FirstOrDefault();
                    //var workflow = _workflowService.Gets(wf => wf.DocTypeId == docModel.DocTypeId).FirstOrDefault();

                    var expireProcess = 7;
                    if (workflow != null)
                    {
                        expireProcess = workflow.ExpireProcess;
                    }
                    docModel.DateAppoint = docModel.DateReceived.AddDays(expireProcess);
                    foreach (var file in signedFile)
                    {
                        docModel.Files.Add(new Bkav.eGovCloud.Entities.Customer.File
                        {
                            FileName = docModel.DocType.DocTypeName,
                            FileLocalName = file.key.ToString(),
                            Size = file.size,
                            FileExtension = file.extension.ToString(),
                            IsDeleted = false,
                            CreatedOnDate = DateTime.Now,
                        });
                    }
                    var entity = docModel.ToEntity();

                    _docService.Create(entity);

                    docModel.Json = ParseToOriginJson(docModel.Json);
                    return Json(new { result = true, document = entity }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = false, message = "Thông tin không hợp lệ" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConvertToPdfForSign(string docTypeId, string form)
        {
            var docType = _docTypeService.Get(Guid.Parse(docTypeId));
            var files = new List<dynamic>();
            if (docType != null && !string.IsNullOrEmpty(form))
            {

            }
            return Json(files, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadSignedFile(string base64)
        {
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AcceptOnline(string doc, string files, int docId)
        {

            if (string.IsNullOrWhiteSpace(doc))
            {
                return Json(new { error = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Accept.Error") });
            }

            DocumentOnline model;
            var fileModels = new List<File>();
            try
            {
                model = Json2.ParseAs<DocumentOnline>(doc);
                try
                {
                    fileModels = Json2.ParseAs<List<File>>(files);
                }
                catch { }
                model.Files = fileModels;
            }
            catch
            {
                return Json(new { error = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Accept.Error") });
            }

            try
            {
                Document outDocument = null;
                var isSuccess = false;
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    AcceptOnline(docId, model, out outDocument);

                    isSuccess = hasComfirmAcceptedDocument(docId, outDocument.DocCode, outDocument.DocumentId, model.Comment, outDocument.DateAppointed);

                    trans.Complete();
                }

                if (isSuccess)
                {
                    #region gửi tin nhắn và mail

                    try
                    {
                        var attachmentIds = outDocument.Attachments.Select(p => p.AttachmentId);
                        SendMailAccept(outDocument, attachmentIds);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    try
                    {
                        //gửi tin nhắn
                        SendSmsAccept(outDocument);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    #endregion

                    return Json(new { success = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Accept.Success") });
                }

                return Json(new { error = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Accept.Error") });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult RejectOnline(string doc, string comment, int docId)
        {
            try
            {
                if (RejectDocument(docId, comment))
                {
                    try
                    {
                        var model = Json2.ParseAs<DocumentOnline>(doc);

                        SendSmsAndMailReject(model, comment);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    return Json(new
                    {
                        success = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Reject.Success")
                    });
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return Json(new { error = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Reject.Error") });
        }

        public JsonResult OnlineSupplementary(string doc, string comment, int docId)
        {
            var expireDate = 7; // mặc định 7 ngày xử lý
            try
            {
                if (AdditionalRequirements(docId, comment, comment, expireDate))
                {
                    try
                    {
                        var model = Json2.ParseAs<DocumentOnline>(doc);
                        SendSmsAndMailAdditionalRequirements(model, comment);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    return Json(new
                    {
                        success = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Reject.Success")
                    });
                }

                return Json(new { error = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Reject.Error") });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = _resourceService.GetResource("DocumentOnline.OnlineRegistration.Reject.Error") });
            }
        }

        public JsonResult CheckDocument(string citizenName = "", string idCard = "", string phone = "", string email = "")
        {
            var listDocument = _documentService.GetsAs(x => new
            {
                CitizenName = x.CitizenName,
                IdCard = x.IdentityCard,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                DocumentId = x.DocumentId,
                DateCreated = x.DateCreated,
                Compendium = x.Compendium,
                DocCode = x.DocCode,
                DocTypeName = x.DocType.DocTypeName,
                CategoryName = x.Category.CategoryName,
                Status = x.Status,
                RatePoint = (x.IdentityCard == idCard ? 3 : 0)
                + ((x.Phone == phone || x.Email == email) ? 2 : 0)
                + (x.CitizenName.ToLower().Contains(citizenName.ToLower()) ? 1 : 0)
            }, x => x.IdentityCard == idCard
               || x.Phone == phone
               || x.Email == email
                || x.CitizenName.ToLower().Contains(citizenName.ToLower()))
                .OrderByDescending(x => x.RatePoint);

            var documents = new List<dynamic>();
            foreach (var document in listDocument)
            {
                var docCopyId = _docCopyService.GetDocCopyIdByCurrentUser(document.DocumentId);
                //if (docCopyId != 0)
                //{
                documents.Add(new
                {
                    citizenName = document.CitizenName,
                    idCard = document.IdCard,
                    phone = document.Phone,
                    email = document.Email,
                    address = document.Address,
                    documentId = document.DocumentId,
                    dateCreated = document.DateCreated,
                    compendium = document.Compendium,
                    docCode = document.DocCode,
                    docTypeName = document.DocTypeName,
                    categoryName = document.CategoryName,
                    status = document.Status,
                    canView = docCopyId != 0,
                    docCopyId = docCopyId,
                });
                //}
            }

            return Json(documents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalOnlineRegistration()
        {
            var total = 0;
            try
            {
                total = GetTotal();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return Json(total, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentOnlineRegistration()
        {
            object results = null;
            try
            {
                var documents = GetDocumentRegister();
                if (!string.IsNullOrWhiteSpace(documents))
                {
                    results = Json2.ParseAs<object>(documents);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentDetailOnlineRegistration(int id)
        {
            string formJson;
            JObject results = null;
            try
            {
                var docDetail = GetDocumentDetail(id);
                if (!string.IsNullOrWhiteSpace(docDetail))
                {
                    results = Json2.ParseAs<JObject>(docDetail);
                    var formContent = results["Json"].ToString();
                    var token = JToken.Parse(formContent);
                    if (token is JArray)
                    {
                        var dynamicResult = new List<string>();
                        foreach (var json in token.Children<JObject>())
                        {
                            JsDocument jsDocument;
                            if (DynamicFormHelper.TryParse(json.ToString(), out jsDocument))
                            {
                                var dynamicForm = "{}";
                                var formjs = _formHelper.ParseFormModel(jsDocument);
                                dynamicForm = formjs.StringifyJs();
                                dynamicResult.Add(dynamicForm);
                            }
                        }
                        formJson = "[" + string.Join(",", dynamicResult) + "]";
                    }
                    else
                    {
                        formJson = "{}";
                        JsDocument jsDocument;
                        if (DynamicFormHelper.TryParse(formContent, out jsDocument))
                        {
                            var formjs = _formHelper.ParseFormModel(jsDocument);
                            formJson = formjs.StringifyJs();
                        }
                    }

                    results["FormJson"] = formJson;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalOnlineCancel()
        {
            var total = 0;
            try
            {
                total = GetTotalCancel();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return Json(total, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentOnlineCancel()
        {
            object results = null;
            try
            {
                var documents = GetDocumentCancel();
                if (!string.IsNullOrWhiteSpace(documents))
                {
                    results = Json2.ParseAs<object>(documents);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownLoadFileOnlineRegistration(int fileId, string fileName)
        {
            long leng = 0;
            using (var stream = GetFileOnlines(fileId, out leng))
            {
                byte[] result = stream.ToByte((int)leng);
                if (result.Length > 0)
                    return File(result, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }

            throw new Exception("File is not exist.");
        }

        public string OpenFileOnlineRegistration(int fileId, string fileName)
        {
            long leng = 0;
            using (var stream = GetFileOnlines(fileId, out leng))
            {
                try
                {
                    return GetOpenStreamUrl(stream, fileName, leng);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            throw new Exception("File is not exist.");
        }

        /// <summary>
        ///   <para> Tạo một bản ghi văn bản trong database </para>
        /// </summary>
        /// <param name="tempFiles"> Danh sách file đính kèm tạm </param>
        /// <param name="copyAttachments">Các file đính kèm sẽ được copy</param>
        /// <param name="model"> </param>
        /// <param name="status"> </param>
        /// <param name="dateCreated"> </param>
        /// <param name="deleteTempfiles"> <c>True</c> xóa tempfiles sau khi thực thi. <c>False</c> ngược lại. </param>
        private Document CreateDocument(IDictionary<string, IDictionary<string, string>> tempFiles,
            IEnumerable<Entities.Customer.Attachment> copyAttachments,
            DocumentModel model, DocumentStatus status, DateTime dateCreated,
            bool deleteTempfiles = true)
        {
            // CuongNT@bkav.com - 210613: Ủy quyền khởi tạo
            var userSendId = GetUserCreatedId();

            var newId = Guid.NewGuid();
            var dateCreate = DateTime.Now;
            // Add contents
            if (model.Contents != null && model.Contents.Any())
            {
                // tránh bị nhân content khi chỉ gửi đồng xử lý
                model.DocumentContents = new List<DocumentContentModel>();
                foreach (var content in model.Contents)
                {
                    if (string.IsNullOrEmpty(content))
                    {
                        continue;
                    }
                    var documentContent = Json2.ParseAsJs<DocumentContentModel>(content);
                    documentContent.DocumentId = newId;
                    documentContent.DocumentContentDetails.Add(new DocumentContentDetailModel
                    {
                        Content = documentContent.Content,
                        CreatedByUserId = User.GetUserId(),
                        CreatedByUserName = User.GetUserName(),
                        CreatedOnDate = dateCreate,
                        Version = 1
                    });
                    documentContent.Version = 1;
                    model.DocumentContents.Add(documentContent);
                }
            }

            // Add doc fees
            var docFees = new List<DocFeeModel>();
            if (model.Fees != null && model.Fees.Any())
            {
                docFees = model.Fees.Select(Json2.ParseAsJs<DocFeeModel>).ToList();//JsonConvert.DeserializeObject<DocFeeModel>(c, new JsonSerializerSettings { Culture = GetCurrentCulture() })
            }
            model.DocFees = docFees;

            model.DateCreated = dateCreated;
            model.DateModified = dateCreated;
            model.DateReceived = dateCreated;
            model.DocumentId = newId;
            model.UserCreatedId = userSendId;
            model.IsReturned = null;
            model.IsSuccess = null;
            model.IsSupplemented = null;
            model.Status = (byte)status;

            var docType = _docTypeService.Get(model.DocTypeId);
            model.CategoryBusinessId = docType.CategoryBusinessId;
            model.CategoryId = docType.CategoryId;
#if !XuLyVanBanEdition
            if (docType.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
            {
                model.DocFieldIds = string.Format(";{0};", docType.DocFieldId);
            }
#endif
            // Add relations
            // Default permission
            model.DocTypePermission = docType.DocTypePermission;
            var document = model.ToEntity();

            // Add DocCode
            // Kiểm tra loại hồ sơ 1 cửa hoặc phát hành hoặc đánh số thì thêm DocCode, tăng số sổ hồ sơ
            // Hiện tại ở đây chỉ xét trường hợp HSMC. 2 Trường hợp kia xử lý ở 2 chức năng tương ứng.
            if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc
                || document.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen)
            {
                var stores = _storeService.GetsByDoctype(docType.DocTypeId);
                var docCode = "";
                if (stores.Any())
                {
                    var storeCodes = stores.First().StoreCodes;
                    if (storeCodes.Any())
                    {
                        docCode = _codeService.ConfirmCode(storeCodes.First().Code, dateCreated, document.DocumentId, "");
                    }
                }
                document.DocCode = docCode;
                document.DateOfIssueCode = dateCreated;
            }

            document.Attachments = _attachmentService.AddAttachmentInDoc(tempFiles, document.UserCreatedId, deleteTempfiles);
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

        /// <summary>
        ///   <para> Tạo văn bản dự thảo. </para>
        ///   <para> Khi tạo mới hồ sơ luôn luôn phải thực hiện tạo văn bản dự thảo trước khi thực hiện các thao tác khác. </para>
        /// </summary>
        /// <param name="tempFiles"> Danh sách tệp mới đính kèm </param>
        /// <param name="copyAttachments">Các file đính kèm sẽ được copy</param>
        /// <param name="model"> Văn bản và các nội dung </param>
        /// <param name="userSendId"> Cán bộ chuyển </param>
        /// <param name="userReceiveId"> Cán bộ nhận </param>
        /// <param name="dateCreated"> </param>
        /// <param name="status"> Trạng thái văn bản </param>
        /// <param name="newDocument"> </param>
        /// <param name="newDocumentCopy"> </param>
        /// <param name="sendNode"> </param>
        /// <param name="receiveNode"> Node văn bản được chuyển tới. Nếu là lưu văn bản dự thảo thì giá trị bằng Id của Node gửi (nodeReceiveId = 0 khi truyền vào) </param>
        /// <param name="deleteTempfiles"> <c>True</c> xóa tempfiles sau khi thực thi. <c>False</c> ngược lại. </param>
        private void CreateDocumentDefault(string comment,
            IDictionary<string, IDictionary<string, string>> tempFiles,
            IEnumerable<Entities.Customer.Attachment> copyAttachments,
            DocumentModel model,
            Node sendNode, int userSendId, Node receiveNode,
            int userReceiveId, DateTime dateCreated, DocumentStatus status,
            out Document newDocument, out DocumentCopy newDocumentCopy,
            bool deleteTempfiles = true)
        {
            const DocumentStatus statusCheck = DocumentStatus.DuThao | DocumentStatus.DangXuLy;
            if (!EnumHelper<DocumentStatus>.ContainFlags(statusCheck, status))
            {
                throw new ArgumentException("status chỉ được phép là DocumentStatus.DuThao | DocumentStatus.DangXuLy.");
            }
            newDocument = CreateDocument(tempFiles, copyAttachments, model, status, dateCreated, deleteTempfiles);

            var documentCopyTypes = receiveNode.GetNodePermission()
                .HasFlag(NodePermissions.QuyenDungXuLy)
                ? DocumentCopyTypes.ChoKetQuaDungXuLy : DocumentCopyTypes.XuLyChinh;

            var newDocumentIdRelation = newDocument.DocumentId;
            var relations = model.RelationModels.Where(c => c.IsAddNext).Select(c => new DocRelation
            {
                DocumentId = newDocumentIdRelation,
                RelationId = c.RelationId,
                RelationCopyId = c.RelationCopyId,
                RelationType = c.RelationType
            }).ToList();
            newDocumentCopy = _docCopyService.Create(model.DocumentId, model.DocTypeId, sendNode, userSendId, receiveNode,
                                           userReceiveId, null, dateCreated, documentCopyTypes, status, relations);
            newDocumentCopy.LastComment = comment;
            newDocumentCopy.LastDateComment = DateTime.Now;
            newDocumentCopy.LastUserComment = User.GetFullName();
        }

        private void AcceptOnline(int docId, DocumentOnline doc, out Document document)
        {
            var model = new DocumentModel(doc);
            model.Contents = new List<string>();

            // Lưu file đính kèm
            var newAttachments = new Dictionary<string, IDictionary<string, string>>();

            if (!string.IsNullOrEmpty(doc.Json))
            {
                var token = JToken.Parse(doc.Json);
                if (token is JArray)
                {
                    var dynamicResult = new List<string>();
                    foreach (var json in token.Children<JObject>())
                    {
                        JsDocument jsDocument;
                        if (!DynamicFormHelper.TryParse(json.ToString(), out jsDocument))
                        {
                            continue;
                        }

                        Guid formId;
                        if (!Guid.TryParse(jsDocument.FormId, out formId))
                        {
                            continue;
                        }

                        var form = _formService.Get(formId);
                        if (form == null)
                        {
                            continue;
                        }

                        var content = new DocumentContent();
                        content.Content = jsDocument.Stringify();
                        content.ContentName = doc.DoctypeName; // doc.Doctype.DocTypeName;
                        content.FormTypeId = (int)Bkav.eGovCloud.Entities.FormType.DynamicForm;
                        model.Contents.Add(content.Stringify());

                        // Mẫu phôi
                        if (!string.IsNullOrEmpty(form.EmbryonicLocationName))
                        {
                            var embryonicPath = Server.MapPath("~/EmbryonicForm/") + form.EmbryonicLocationName;
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(form.EmbryonicPath) + ".pdf";
                            var tempPath = FileManager.Default.Resolve(fileName, ResourceLocation.Default.FileUploadTemp);
                            _formHelper.ParseEmbryonic(jsDocument, embryonicPath, tempPath, isPdf: true);

                            var temp = new Dictionary<string, string>();
                            temp.Add("name", fileName);
                            newAttachments.Add(fileName, temp);
                        }
                    }
                }
            }

            // Lấy node nhận trong quy trình
            var userSendId = GetUserCreatedId();
            var workflow = _workflowService.GetIsActived(model.DocTypeId);
            var startNodes = _workflowHelper.GetStartNodes(workflow, userSendId);
            if (startNodes == null || !startNodes.Any())
            {
                throw new ApplicationException("Chưa cấu hình quy trình. Vui lòng liên hệ với quản trị viên.");
            }
            var nodeSend = startNodes.First();

            // Thời hạn xử lý
            model.DateAppointed = _workTimeHelper.GetDateAppoint(DateTime.Now, workflow.ExpireProcess);

            //Tệp đính kềm để gửi mail
            if (doc.Files != null && doc.Files.Any())
            {
                var index = 0;
                foreach (var file in doc.Files)
                {
                    long leng = 0;
                    var stream = GetFileOnlines(file.FileId, out leng);
                    if (stream != null)
                    {
                        var temp = new Dictionary<string, string>();
                        var tempPath = ResourceLocation.Default.FileUploadTemp;
                        var tempFile = FileManager.Default.Create(stream, tempPath, file.FileName);
                        temp.Add("name" + index, file.FileName);
                        if (newAttachments.ContainsKey(file.FileName))
                        {
                            file.FileName = file.FileName + "(" + index + ")";
                        }
                        newAttachments.Add(file.FileName, temp);
                    }
                    index++;
                }
            }

            var documentCopyIsTransfering = new DocumentCopy();
            Document documentIsTransfering = null;
            CreateDocumentDefault(doc.Comment, newAttachments, null, model, nodeSend,
                userSendId, nodeSend, userSendId, DateTime.Now,
                DocumentStatus.DangXuLy, out documentIsTransfering, out documentCopyIsTransfering, false);

            document = documentIsTransfering;
            CreateActivityLog(ActivityLogType.TiepNhan, string.Format("{0} tiếp nhận văn bản: {1}",
                User.GetUserNameWithDomain(), model.Compendium));
        }

        /// <summary>
        /// Từ chối
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private bool RejectDocument(int docId, string comment)
        {
            ActiveCert();
            var success = false;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/Reject";
                var response = client.PostAsJsonAsync(url, new
                {
                    docId = docId,
                    comment = comment
                }).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    if (string.IsNullOrEmpty(result))
                    {
                        success = true;
                    }
                }
            }

            return success;
        }

        private bool AdditionalRequirements(int docId, string comment, string supplenmentaryRequirement, int expireDate)
        {
            //ActiveCert();
            var success = false;
            var user = _userService.CurrentUser;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/AdditionalRequirements";
                var response = client.PostAsJsonAsync(url, new
                {
                    docId = docId,
                    comment = comment,
                    supplenmentaryRequirement = supplenmentaryRequirement,
                    expireDate = expireDate,
                    userSendId = user.UserId,
                    userSendName = user.FullName
                }).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    if (string.IsNullOrEmpty(result))
                    {
                        success = true;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// cập nhật tiếp nhận thành công
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="localDocCode"></param>
        /// <param name="localId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private bool hasComfirmAcceptedDocument(int docId, string localDocCode, Guid localId, string comment, DateTime? dateAppointed)
        {
            ActiveCert();
            var success = false;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/Accept";
                var response = client.PostAsJsonAsync(url, new
                {
                    docId = docId,
                    localDocCode = localDocCode,
                    localId = localId,
                    comment = comment,
                    dateAppointed = dateAppointed
                }).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    if (string.IsNullOrEmpty(result))
                    {
                        success = true;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Alow qua tầng bảo mật của Cert
        /// note: Khi cert của hệ thống bị lỗi thì HttpWebRequest và HttpWebResponse không thao tác được
        /// </summary>
        private void ActiveCert()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        /// <summary>
        /// Download từ api online
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        private System.IO.Stream GetFileOnlines(int fileId, out long fileLength)
        {
            ActiveCert();
            var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/Download/" + fileId;
            var fileReq = (HttpWebRequest)HttpWebRequest.Create(url);
            var fileResp = (HttpWebResponse)fileReq.GetResponse();
            fileLength = fileResp.ContentLength;
            return fileResp.GetResponseStream();
        }

        /// <summary>
        /// Lấy tổng số hồ sơ đăng ký qua mạng
        /// </summary>
        /// <returns></returns>
        private int GetTotal()
        {
            ActiveCert();
            var success = 0;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/TotalRegister";
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    success = Convert.ToInt32(result);
                }
            }

            return success;
        }

        /// <summary>
        /// Lấy tổng số hồ sơ đăng ký qua mạng đã bị hủy bỏ
        /// </summary>
        /// <returns></returns>
        private int GetTotalCancel()
        {
            ActiveCert();
            var success = 0;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/TotalCancelDocument ";
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    success = Convert.ToInt32(result);
                }
            }

            return success;
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
        /// Lấy danh sách văn bản/hồ sơ đăng ký qua mạng
        /// </summary>
        /// <returns></returns>
        private string GetDocumentRegister()
        {
            ActiveCert();
            var results = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/RegisterList";
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    results = response.Content.ReadAsStringAsync().Result as string;
                }
            }
            return results;
        }

        /// <summary>
        /// Lấy danh sách văn bản/hồ sơ đăng ký qua mạng đã bị hủy bỏ
        /// </summary>
        /// <returns></returns>
        private string GetDocumentCancel()
        {
            ActiveCert();
            var results = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/CancelDocumentList";
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    results = response.Content.ReadAsStringAsync().Result as string;
                }
            }
            return results;
        }

        /// <summary>
        /// Lấy chi tiết hồ sơ đăng ký qua mạng
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        private string GetDocumentDetail(int docId)
        {
            var results = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/Document/Detail?docId=" + docId;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    results = response.Content.ReadAsStringAsync().Result as string;
                }
            }

            return results;
        }

        #region sms, mail

        private void SendSmsAccept(Document document)
        {
            var user = _userService.CurrentUser;
            // _smsHelper.SendSmsAccept(document, user);
        }

        private void SendMailAccept(Document document, IEnumerable<int> attachmentIds = null)
        {
            var user = _userService.CurrentUser;
            _mailHelper.SendAcceptedDocumentOnline(document, attachmentIds.ToList(), 0);
        }

        private void SendSmsAndMailReject(DocumentOnline model, string comment)
        {
            var docFieldName = string.Empty;
            var docTypeName = string.Empty;
            var accountCommand = string.Empty;
            var fullNameCommand = string.Empty;
            var dateCommand = DateTime.Now.ToString(DATE_FORMAT);
            var officeName = string.Empty;

            if (model.DocTypeId != null)
            {
                docTypeName = _docTypeService.GetAs(p => p.DocTypeName, model.DocTypeId);
            }

            var user = _userService.CurrentUser;
            accountCommand = user.UsernameEmailDomain;
            fullNameCommand = user.FullName;

            officeName = _addresService.GetCurrent().Name;
            try
            {
                _smsHelper.SendRejectedDocumentOnline(model, user, comment);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            try
            {
                _mailHelper.SendRejectedDocumentOnline(model, user, comment);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void SendSmsAndMailAdditionalRequirements(DocumentOnline model, string comment)
        {
            var docFieldName = string.Empty;
            var docTypeName = string.Empty;
            var accountCommand = string.Empty;
            var fullNameCommand = string.Empty;
            var dateCommand = DateTime.Now.ToString(DATE_FORMAT);
            var officeName = string.Empty;

            if (model.DocTypeId != null)
            {
                docTypeName = _docTypeService.GetAs(p => p.DocTypeName, model.DocTypeId);
            }

            var user = _userService.CurrentUser;
            accountCommand = user.UsernameEmailDomain;
            fullNameCommand = user.FullName;

            officeName = _addresService.GetCurrent().Name;

            try
            {
                //Gửi tin nhắn
                //_smsHelper.SendRequireSupplementarySms(model.Phone,
                // docFieldName, model.DocCode, docTypeName, model.Compendium,
                // accountCommand, dateCommand, fullNameCommand, officeName, model.PersonInfo, model.DateReceived.ToString(DATE_FORMAT));
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            try
            {
                //Gửi mail
                //_mailHelper.SendRequireSupplementaryEmail(model.Email,
                // docFieldName, model.DocCode, docTypeName, model.Compendium, accountCommand,
                // dateCommand, fullNameCommand, officeName, model.PersonInfo, comment, model.DateReceived.ToString(DATE_FORMAT), model.Token);

            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        #region xem không dùng thì bỏ

        ///// <summary>
        ///// Gủi tin nhắn thông báo tới người dân khi hồ sơ bị từ chối
        ///// </summary>
        ///// <param name="phoneNumber">Số diện thoại đăng ký</param>
        ///// <param name="compendium">Trích yếu</param>
        //private void SendSmsReject(string phoneNumber, string compendium)
        //{
        //    //xem xử lsy chỗ này
        //    Document document = null;
        //    var user = _userService.CurrentUser;
        //    _smsHelper.SendSmsAccept(document, user);
        //}

        ///// <summary>
        ///// Gủi tin nhắn thông báo tới người dân khi hồ sơ bị từ chối
        ///// </summary>
        ///// <param name="phoneNumber">Số diện thoại đăng ký</param>
        ///// <param name="compendium">Trích yếu</param>
        //private void SendSmsAdditionalRequirements(string phoneNumber, string compendium)
        //{
        //    string message = string.Format(
        //        _resourceService.GetResource("DocumentOnline.OnlineRegistration.AdditionalRequirements.TemplateSendSms"), compendium);
        //    var userSend = _userService.CurrentUser;
        //    _smsHelper.CreateSms(phoneNumber, message, userSend);
        //}

        ///// <summary>
        ///// Gủi mail thông báo từ chối tiếp nhặn hồ sơ tới người dân
        ///// </summary>
        ///// <param name="mail">email đăng ký</param>
        ///// <param name="doctypeId">Loại hồ sơ đăng ký</param>
        ///// <param name="docFieldId">Lĩnh vực của hồ sơ</param>
        //private void SendMailReject(string mail, Guid? doctypeId, int? docFieldId)
        //{
        //    var doctypeIds = new List<Guid>();
        //    var docFieldIds = new List<int>();
        //    if (doctypeId.HasValue)
        //    {
        //        doctypeIds.Add(doctypeId.Value);
        //    }

        //    if (docFieldId.HasValue)
        //    {
        //        docFieldIds.Add(docFieldId.Value);
        //    }
        //    var processTypes = new List<Entities.Enum.DocumentProcessType>
        //    {
        //        Entities.Enum.DocumentProcessType.TuChoiDKQM
        //    };

        //    var template = _templateService.GetTemplates(doctypeIds, docFieldIds, processTypes,
        //        (int)TemplateType.Email).FirstOrDefault();
        //    var userSend = _userService.CurrentUser;
        //    _mailHelper.CreateEmail(mail, template, userSend);
        //}

        ///// <summary>
        ///// Gủi mail thông báo từ chối tiếp nhặn hồ sơ tới người dân
        ///// </summary>
        ///// <param name="mail">email đăng ký</param>
        ///// <param name="doctypeId">Loại hồ sơ đăng ký</param>
        ///// <param name="docFieldId">Lĩnh vực của hồ sơ</param>
        //private void SendMailAdditionalRequirements(string mail, Guid? doctypeId, int? docFieldId)
        //{
        //    //var doctypeIds = new List<Guid>();
        //    //var docFieldIds = new List<int>();
        //    //if (doctypeId.HasValue)
        //    //{
        //    //    doctypeIds.Add(doctypeId.Value);
        //    //}

        //    //if (docFieldId.HasValue)
        //    //{
        //    //    docFieldIds.Add(docFieldId.Value);
        //    //}
        //    //var processTypes = new List<Entities.Enum.DocumentProcessType>
        //    //{
        //    //    Entities.Enum.DocumentProcessType.TiepNhanBoSung
        //    //};

        //    //var template = _templateService.GetTemplates(doctypeIds, docFieldIds, processTypes,
        //    //    (int)TemplateType.Email).FirstOrDefault();

        //    var cfg = _notifyConfigService.Get(p =>
        //       p.Key == Entities.Enum.NotifyConfigType.AdditionalRequirementsDocumentOnline.ToString());
        //    if (cfg == null)
        //    {
        //        throw new Exception("Chưa cấu hình mẫu gửi tin nhắn cho tiếp nhận văn bản.");
        //    }

        //    if (cfg.HasAutoSendMail || cfg.MailTemplateId == 0)
        //    {
        //        //không kích hoạt gửi mail hoặc chưa cấu hình mẫu gửi
        //        return;
        //    }

        //    var template = _templateService.Get(cfg.SmsTemplateId);
        //    var userSend = _userService.CurrentUser;
        //    _mailHelper.CreateEmail(mail, template, userSend);
        //}

        #endregion

        #endregion

        private string ParseToOriginJson(string jsonString)
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                var result = new List<string>();
                var jsons = JArray.Parse(jsonString);
                foreach (var json in jsons.Children<JObject>())
                {
                    JsDocument jsDocument;
                    if (DynamicFormHelper.TryParse(json.ToString(), out jsDocument))
                    {
                        var form = "{}";
                        var formjs = _formHelper.ParseFormModel(jsDocument);
                        form = formjs.StringifyJs();
                        result.Add(form);
                    }
                }
                return "[" + string.Join(",", result) + "]";
            }
            return string.Empty;
        }

        public string GetOpenStreamUrl(System.IO.Stream stream, string fileName, long length)
        {
            var fileExtention = System.IO.Path.GetExtension(fileName).ToLower();

            try
            {
                using (var image = Image.FromStream(stream))
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        image.Save(ms, image.RawFormat);
                        return "data:image/png;base64," + ms.ToBase64String();
                    }
                }
            }
            catch (Exception)
            {
                if (".pdf".IndexOf(fileExtention) >= 0)
                {
                    return "data:application/pdf;base64," + stream.ToBase64String(length);
                }
                else
                {
                    return "";
                }
            }
        }

        private System.IO.MemoryStream ZipAttachment(List<Bkav.eGovCloud.Entities.Customer.File> files)
        {
            var filesResult = new Dictionary<string, System.IO.Stream>();
            var tempPath = ResourceLocation.Default.FileUploadTemp;
            foreach (var file in files)
            {
                var fileStream = FileManager.Default.Open(file.FileLocalName, tempPath, false, file.FileExtension);
                filesResult.Add(file.FileName, fileStream);
            }
            return StreamExtension.ZipAttachment(filesResult);
        }

#endif
    }
}