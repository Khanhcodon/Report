#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Microsoft.AspNet.SignalR;
using Microsoft.JScript;
using Newtonsoft.Json.Linq;
using SolrNet.Utils;
using FeeType = Bkav.eGovCloud.Entities.FeeType;
using FormType = Bkav.eGovCloud.Entities.FormType;
using System.Data;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Business.Objects.CacheObjects;
using System.Dynamic;
using MySql.Data.MySqlClient;
using ActiveQueryBuilder.Web.Server;
using ActiveQueryBuilder.Core;
using System.Text;
using Bkav.eGovCloud.Areas.Admin.Models;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Bkav.eGovCloud.DataAccess;
using System.Web;

#endregion

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    [EgovAuthorize]
    public class DocumentReportController : CustomerBaseController
    {
        #region Readonly & Static Fields

        private readonly DocumentOnlineBll _docOnlineService;
        private readonly ApproverBll _approverService;
        private readonly CategoryBll _categoryService;
        private readonly CodeBll _codeService;
        private readonly CommentBll _commentService;
        private readonly CommonCommentBll _commonCommentService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserDepartmentJobTitlesPositionBll _userDepartmentService;
        private readonly DocTimelineBll _docTimelineService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocFieldBll _docfieldService;
        private readonly DocumentContentBll _documentContentService;
        private readonly DocumentCopyBll _documentCopyService;
        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly DocumentBll _documentService;
        private readonly ExtensionTimeBll _extensionTimeService;
        private readonly DocumentPublishBll _publishService;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly FormHelper _formHelper;
        private readonly SupplementaryBll _supplementaryServcie;
        private readonly UserBll _userService;
        private readonly WorktimeHelper _workTimeHelper;
        private readonly StorePrivateBll _storePrivateService;
        private readonly WorkflowBll _workflowService;
        private readonly AddressBll _addressService;
        private readonly StoreBll _storeService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly BusinessLicenseBll _businessLicenseService;
        private readonly AnticipateBll _anticipateService;
        private readonly FormBll _formService;
        private readonly DocFinishBll _docfinishService;
        private readonly AttachmentBll _attachmentService;
        private readonly EgovSearch _searchService;
        private readonly KeyWordBll _keyWordService;
        private readonly PaperBll _paperService;
        private readonly FeeBll _feeService;
        private readonly TransferTypeBll _transferTypeService;
        private readonly UserActivityLogBll _userActivityLogService;
        private readonly UserConnectionBll _userConnectionService;
        private readonly SearchSettings _searchSettings;
        private readonly ISearchInDatabase _searchInDatabaseService;
        private readonly ImageSettings _imageSettings;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly ResourceBll _resourceService;
        private readonly CitizenBll _citizenService;
        private readonly TemplateBll _templateService;
        private readonly InterfaceConfigBll _interfaceConfigService;
        private readonly SendSmsHelper _smsHelper;
        private readonly SendEmailHelper _mailHelper;
        private readonly WorkflowHelper _workflowHelper;
        private readonly TemplateHelper _templateHelper;
        private readonly LogBll _logService;
        private readonly TransferSettings _transferSetting;

        private readonly EmailSettings _emailSettings;
        private readonly SmsSettings _smsSettings;
        private readonly AdminGeneralSettings _adminSetting;
        private readonly PermissionBll _permissionSerice;
        private readonly AuthorizeBll _authorizeService;
        private readonly CatalogBll _catalogService;
        private string DateFormat = "dd/MM/yyyy";
        private readonly TemplateKeyBll _templateKeyService;

        private readonly AdminGeneralSettings _generalSettings;

        private const int FORM_CATEGORY_TARGET = 2;
        private const int FORM_CATEGORY_SUMMARY = 3;

        #endregion

        #region C'tors

        public DocumentReportController(
            DocumentOnlineBll docOnlineService,
            DocFieldBll docfieldService,
            DocTypeBll docTypeService,
            CategoryBll categoryService,
            FormBll formService,
            DocumentBll documentService,
            FormHelper formHelper,
            DocumentCopyBll documentCopyService,
            UserBll userService,
            DepartmentBll departmentService,
            PositionBll postionService,
            UserDepartmentJobTitlesPositionBll userDepartmentService,
            CommentBll commentService,
            ApproverBll approverService,
            DocumentContentBll documentContentService,
            CodeBll codeService,
            DocumentPermissionHelper documentPermissionHelper,
            DocTimelineBll docTimelineService,
            CommonCommentBll commonCommentBll,
            SupplementaryBll supplementaryServcie,
            WorktimeHelper worktimeHelper,
            ExtensionTimeBll extensionTimeService,
            FileUploadSettings fileUploadSettings,
            StorePrivateBll storePrivateService,
            WorkflowBll workflowService,
            AddressBll addressService,
            StoreBll storeService,
            DocTypeFormBll docTypeFormService,
            BusinessLicenseBll businessLicenseService,
            AnticipateBll anticipateService,
            DocFinishBll docfinishService,
            AttachmentBll attachmentService,
            EgovSearch searchService,
            KeyWordBll keyWordService,
            TransferTypeBll transferTypeService,
            UserConnectionBll userConnectionService,
            SearchSettings searchSettings,
            PaperBll paperService,
            FeeBll feeService,
            ISearchInDatabase searchInDatabaseService,
            UserActivityLogBll userActivityLogService,
            ImageSettings imageSettings,
            Helper.UserSetting helperUserSetting,
            ResourceBll resourceService,
            CitizenBll citizenService,
            TemplateBll templateService,
            SendSmsHelper smsHelper,
            SendEmailHelper mailHelper,
            WorkflowHelper workflowHelper,
            TemplateHelper templateHelper,
            EmailSettings emailSettings,
            SmsSettings smsSettings,
            AdminGeneralSettings adminSetting,
            InterfaceConfigBll interfaceConfigService,
            DocumentPublishBll publishService,
            LogBll logService,
            TransferSettings transferSetting, AuthorizeBll authorizeService,
            PermissionBll permissionSerice,
            CatalogBll catalogService,
            TemplateKeyBll templateKeyService)
        {
            _docOnlineService = docOnlineService;
            _docfieldService = docfieldService;
            _docTypeService = docTypeService;
            _categoryService = categoryService;
            _documentService = documentService;
            _formHelper = formHelper;
            _documentCopyService = documentCopyService;
            _userService = userService;
            _departmentService = departmentService;
            _positionService = postionService;
            _userDepartmentService = userDepartmentService;
            _commentService = commentService;
            _approverService = approverService;
            _documentContentService = documentContentService;
            _codeService = codeService;
            _documentPermissionHelper = documentPermissionHelper;
            _docTimelineService = docTimelineService;
            _commonCommentService = commonCommentBll;
            _supplementaryServcie = supplementaryServcie;
            _workTimeHelper = worktimeHelper;
            _extensionTimeService = extensionTimeService;
            _fileUploadSettings = fileUploadSettings;
            _storePrivateService = storePrivateService;
            _workflowService = workflowService;
            _addressService = addressService;
            _storeService = storeService;
            _doctypeFormService = docTypeFormService;
            _businessLicenseService = businessLicenseService;
            _anticipateService = anticipateService;
            _formService = formService;
            _docfinishService = docfinishService;
            _attachmentService = attachmentService;
            _searchService = searchService;
            _keyWordService = keyWordService;
            _paperService = paperService;
            _feeService = feeService;
            _transferTypeService = transferTypeService;
            _userConnectionService = userConnectionService;
            //_hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHubs>();
            _searchSettings = searchSettings;
            _searchInDatabaseService = searchInDatabaseService;
            _userActivityLogService = userActivityLogService;
            _imageSettings = imageSettings;
            _helperUserSetting = helperUserSetting;
            _resourceService = resourceService;
            _citizenService = citizenService;
            _templateService = templateService;
            _smsHelper = smsHelper;
            _mailHelper = mailHelper;
            _workflowHelper = workflowHelper;
            _templateHelper = templateHelper;
            _emailSettings = emailSettings;
            _smsSettings = smsSettings;
            _adminSetting = adminSetting;
            _interfaceConfigService = interfaceConfigService;
            _publishService = publishService;
            _permissionSerice = permissionSerice;
            _logService = logService;
            _transferSetting = transferSetting;
            _authorizeService = authorizeService;
            _catalogService = catalogService;
            _templateKeyService = templateKeyService;
        }
        #endregion
        public List<Children> LoopChildren( List<Children> listInfo, Guid parentId) {
            List<Children> list = new List<Children>();
            //name nodeName type level code label link children id parentId
            foreach (var item in listInfo)
            {
                Children child = new Children();
                if (item.parentId == parentId) {
                    child.id = item.id;
                    child.parentId = item.parentId;
                    child.name = item.name;
                    child.positionName = item.positionName;
                    child.departmentName = item.departmentName;
                    child.nodeName = item.nodeName;
                    child.version = item.version;
                    child.type = item.type;
                    child.level = item.level;
                    child.code = item.code;
                    child.label = item.label;
                    child.link = item.link;
                    child.children = LoopChildren( listInfo, item.id );
                    list.Add(child);
                }
            }
            return list;

        }
        public ActionResult UploadImage() {
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Temp"))
                              .Select(fn => "~/Temp/" + System.IO.Path.GetFileName(fn));
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = System.IO.Path.GetFileName(file.FileName);
                        var ServerSavePath = System.IO.Path.Combine(Server.MapPath("~/Temp/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " file tải lên thành công!.";
                     
                    }

                }
            }
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Temp"))
                          .Select(fn => "~/Temp/" + System.IO.Path.GetFileName(fn));
            return View();
        }
        public ActionResult Timeline(Guid docId) {

            var list = _commentService.GetByDocId(docId);
            List<Children> listInfoChild = new List<Children>();
            List<ListId> listId = new List<ListId>();
            _documentCopyService.GetAsDocId(p => p.Status , docId);
            int count = 0;
            foreach ( var item in list ) {

                var userSendID = item.UserSendId;
                var userReceiveId = item.UserReceiveId;
                var user = _userService.Get(userSendID);
                var fullName = user.FullName;
                var date = item.DateCreated.ToString();

                if (count == 0)
                {
                    
                    Children childParent = new Children();
                    var userDepartment = _userDepartmentService.GetbyUserId(userSendID);
                    foreach (var userD in userDepartment)
                    {
                        int DepartmentID = userD.DepartmentId;
                        int PositionID = userD.PositionId;
                        var listDepartment = _departmentService.Get(DepartmentID);
                        var listPosition = _positionService.Get(PositionID);

                        childParent.departmentName = listDepartment.DepartmentName ;
                        childParent.positionName = listPosition.PositionName ;            
                    }
                    Guid g = Guid.NewGuid();
                    childParent.id = g;
                    childParent.name = fullName;
                    childParent.nodeName = fullName;
                    childParent.code = "codeParent";
                    childParent.label = fullName;
                    childParent.version = date;
                    childParent.link = new Link()
                    {
                        name = fullName,
                        nodeName = fullName,
                        mainProcess = "1",
                        direction = "ASYN"
                    };
                    childParent.level = "0";
                    childParent.type = "type4";       
                    childParent.parentId = new Guid("8fb81e61-1157-4e01-af25-491ecefad232");
                    listId.Add(new ListId
                    {
                        id = g,
                        parentId = new Guid("8fb81e61-1157-4e01-af25-491ecefad232"),
                        userReceiveId = userSendID
                    });
                    listInfoChild.Add(childParent);
                }
                var jsonContent = item.Content;
                Contents contents = JsonConvert.DeserializeObject<Contents>(jsonContent);
                var transfers = contents.Transfers;
                if (transfers.Count >= 1)
                {
                    foreach (var transfer in transfers)
                    {
                        //xlc
                        if (transfer.type == "1")
                        {
                            Children child = new Children();
                            Link link = new Link();
                            var userDepartment = _userDepartmentService.GetbyUserId(userReceiveId);
                            foreach (var userD in userDepartment)
                            {
                                int DepartmentID = userD.DepartmentId;
                                int PositionID = userD.PositionId;
                                var listDepartment = _departmentService.Get(DepartmentID);
                                var listPosition = _positionService.Get(PositionID);

                                child.departmentName = listDepartment.DepartmentName;
                                child.positionName = listPosition.PositionName;
                            }
                            Guid g = Guid.NewGuid();
                            child.id = g;
                            child.name = transfer.label;
                            child.nodeName = transfer.label;
                            child.code = "code";
                            child.label = transfer.label;
                            child.version = date;
                            link.content = contents.Content;
                            link.name = transfer.label;
                            link.nodeName = transfer.label;
                            link.mainProcess = "1";
                            link.direction = "ASYN";

                            child.type = "type1";
                            child.level = transfer.type;
                            child.link = link;
                            var getParentId = (from idU in listId
                                               where idU.userReceiveId == userSendID
                                               select new ListId { id = idU.id, parentId = idU.parentId, userReceiveId = idU.userReceiveId }).FirstOrDefault();
                            Guid pId;
                            if (getParentId != null) {
                                pId = getParentId.id;
                            } else {
                                var getAll = (from idU in listId
                                                   select new ListId { id = idU.id, parentId = idU.parentId, userReceiveId = idU.userReceiveId }).FirstOrDefault();
                                pId = getAll.id;

                            }
                            child.parentId = pId;
                            listId.Add(new ListId
                            {
                                id = g,
                                parentId = pId,
                                userReceiveId = userReceiveId
                            });
                            listInfoChild.Add(child);
                        }
                        //dxl va nhan thong bao
                        else {
                            Children child = new Children();
                            Link link = new Link();
                            Guid g = Guid.NewGuid();
                            child.id = g;
                            child.name = transfer.label;
                            child.nodeName = transfer.label;
                            child.code = "code";
                            child.label = transfer.label;
                            child.version = date;
                            link.content = contents.Content;
                            link.name = transfer.label;
                            link.nodeName = transfer.label;
                            link.mainProcess = "2";
                            link.direction = "ASYN";
                            // dong xl
                            if (transfer.type == "2")
                            {
                                child.type = "type2";
                            }
                            //nhan thong bao
                            else
                            {
                                child.type = "type3";
                            }
                            child.level = transfer.type;
                            child.link = link;
                            var getParentId = (from idU in listId
                                               where idU.userReceiveId == userSendID
                                               select new ListId { id = idU.id, parentId = idU.parentId, userReceiveId = idU.userReceiveId }).FirstOrDefault();

                            //get id to value 
                            string[] splitValue = transfer.value.Split('_');
                            int ReceiveId;
                            if (splitValue.Length > 1)
                            {
                                 ReceiveId = int.Parse(splitValue[1]);
                            }
                            else {
                                string[] splitValue2 = transfer.value.Split('-');
                                if (splitValue2.Length > 1)
                                {
                                    ReceiveId = int.Parse(splitValue2[1]);
                                }
                                else {
                                    ReceiveId = userReceiveId;
                                }
                               
                            }
                            var userDepartment = _userDepartmentService.GetbyUserId(ReceiveId);
                            foreach (var userD in userDepartment)
                            {
                                int DepartmentID = userD.DepartmentId;
                                int PositionID = userD.PositionId;
                                var listDepartment = _departmentService.Get(DepartmentID);
                                var listPosition = _positionService.Get(PositionID);

                                child.departmentName = listDepartment.DepartmentName;
                                child.positionName = listPosition.PositionName;
                            }
                            //end
                            child.parentId = getParentId.id;
                            listId.Add(new ListId
                            {
                                id = g,
                                parentId = getParentId.id,
                                userReceiveId = ReceiveId
                            });
                            listInfoChild.Add(child);
                        }
                    }
                }
                count++;
            }
            var lstTree = new Tree();
            var jsonModel =  LoopChildren(listInfoChild, Guid.Parse("8fb81e61-1157-4e01-af25-491ecefad232") );
            lstTree.tree = jsonModel;
            string json = JsonConvert.SerializeObject(lstTree);
            ViewData["json"] = json;
            return View();
        }

        //public JsonResult GetWarningCompilation(string doctypeId, string timekey, int userCreateId)
        //{
        //    var dtId = Guid.Parse(doctypeId);
        //    var currentUser = _userService.CurrentUser;
        //    var doctype = _docTypeService.Get(dtId);
        //    if (doctype == null)
        //    {
        //        return Json( new {error= true, message = "Loại báo cáo không tồn tại" }, JsonRequestBehavior.AllowGet);
        //    }
        //    var doctypeForm = _doctypeFormService.Get(doctype.DocTypeId, true);

        //    if (doctypeForm == null)
        //    {
        //        return Json( new {error = true, message = "Loại báo cáo không tồn tại" }, JsonRequestBehavior.AllowGet);
        //    }
        //    var form = _formService.Get(doctypeForm.FormId);
        //    if (form == null)
        //    {
        //        return Json(new { error = true, message = "Loại báo cáo không tồn tại" }, JsonRequestBehavior.AllowGet);
        //    }

        //    var doctypeChildId = form.ChildCompilationId;
        //    if (doctypeChildId == null)
        //        return Json(new { error = true, message = "Loại báo cáo không tồn tại" }, JsonRequestBehavior.AllowGet);

        //    var dtChildId = Guid.Parse(doctypeChildId);
        //    var doctypeChild = _docTypeService.Get(dtChildId);
        //    if (doctypeChild == null)
        //    {
        //        return Json(new { error = true, message = "Loại báo cáo không tồn tại" }, JsonRequestBehavior.AllowGet);
        //    }
        //    var doctypeFormChild = _doctypeFormService.Get(doctypeChild.DocTypeId, true);

        //    if (doctypeFormChild == null)
        //    {
        //        return Json(new { error = true, message = "Loại báo cáo không tồn tại" }, JsonRequestBehavior.AllowGet);
        //    }
        //    var formChild = _formService.Get(doctypeFormChild.FormId);
        //    if (formChild == null)
        //    {
        //        return Json(new { error = true, message = "Loại báo cáo không tồn tại" }, JsonRequestBehavior.AllowGet);
        //    }
        //    var userId = userCreateId == 0 ? User.GetUserId() : userCreateId;
        //    var typeTime = GetTypeTime(doctypeChild.ActionLevel.Value);
        //    var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == userId).OrderBy(p => p.IsPrimary).FirstOrDefault();
        //    var organizeKey = currentDepartment?.Emails;
        //    if (doctype.CategoryBusinessId != 16 )
        //    {
        //        var datas = GetDataSendThuyetMinh(timekey, organizeKey, currentDepartment.DepartmentId, doctypeChild.DocTypeId.ToString());
        //        return Json(datas, JsonRequestBehavior.AllowGet);
        //    }
            
        //    return Json(new { succcess = false}, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetDataByQuery(Guid formId, int timekey, string organizationkey)
        {
            var param = new List<object>
            {
                new SqlParameter("@timekey", timekey), new SqlParameter("@organize", organizationkey)
            };
            var form = _formService.Get(formId);
            if (form != null)
            {
                var sql = form.ConfigFunction;
                sql = sql.Replace("@timekey", timekey.ToString()).Replace("@organizationkey", organizationkey);
                var arrPara = param.ToArray();
                var data = _templateKeyService.GetDataByQuery(sql, arrPara);
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = 1, message = "Không tồn tại form" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSurveyWarningCompilation(string documentId)
        {
            var docId = Guid.Parse(documentId);
            var document = _documentService.Get(docId);
            if (document == null)
            {
                return Json(new { error = true, message = "Phiếu khảo sát không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            
            if (document.CategoryBusinessId == 32)
            {
                var datas = GetDataSurvey(document.DocumentId);
                return Json(datas, JsonRequestBehavior.AllowGet);
            }

            return Json(new { succcess = false }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<dynamic> GetDataSendThuyetMinh(string timekey, string organizeKey, int? parentId, string doctypeId)
        {
            var sql = @"SELECT de.DepartmentName as organizename, org.StatusReport, org.OrganizationCode as reported, org.DocumentId  
                                from (select department.DepartmentId, department.ParentId,department.Emails, department.DepartmentName
                                From user_department_jobtitles_position , department
                                where department.ParentId = {2} 
                                and user_department_jobtitles_position.HasReceiveDocument = 1
                                group by department.DepartmentId ) as de
                                LEFT JOIN
                                (SELECT OrganizationCode , document.StatusReport, document.DocumentId from document WHERE
                                OrganizationCode in (
                                SELECT dc.Emails FROM department dc WHERE dc.ParentId = {2} )
                                and document.TimeKey = {0} and DocTypeId = '{1}' GROUP BY OrganizationCode) org on
                                de.Emails = org.OrganizationCode WHERE de.ParentId = {2}";
            sql = string.Format(sql, timekey, doctypeId, parentId);
            var param = new List<object>
            {
                new SqlParameter("@timekey", timekey), new SqlParameter("@organize", organizeKey)
            };

            var arrPara = param.ToArray();
            IEnumerable<dynamic> list = _templateKeyService.GetDataByQuery(sql, arrPara).ToList();
            return list;
        }

        private IEnumerable<dynamic> GetDataSurvey(Guid documentId)
        {
            var sql = @"SELECT UserCurrentName, `Status`
                        FROM documentcopy
                        WHERE ParentId IS NOT NULL
	                        AND DocumentId = @documentId;";
            var param = new List<object>
            {
                new SqlParameter("@documentId", documentId)
            };

            var arrPara = param.ToArray();
            IEnumerable<dynamic> list = _templateKeyService.GetDataByQuery(sql, arrPara).ToList();
            return list;
        }

        private IEnumerable<dynamic> GetDataSend(string timekey, string organizeKey, string tablename, string typeTime)
        {
            var sql = @"dashboard: SELECT depart.organizekey, org.organizekey reported, depart.organizename from dim_organize_standard depart 
                        LEFT JOIN (SELECT bc.organizekey  from {0} 
                        bc WHERE bc.{1}  = @timekey GROUP BY bc.organizekey) org  on depart.organizekey = org.organizekey WHERE parent = @organize and 
                        (depart.organizename LIKE 'Xã%' or depart.organizename LIKE 'Phường%' or depart.organizename LIKE 'Thị trấn%' 
                        and depart.organizename LIKE 'Huyện%' or depart.organizename LIKE 'Thị xã%'  or depart.organizename LIKE 'Thành phố%' )";
            sql = string.Format(sql, tablename, typeTime, timekey, organizeKey);
            var param = new List<object>
            {
                new SqlParameter("@timekey", timekey), new SqlParameter("@organize", organizeKey)
            };

            var arrPara = param.ToArray();
            IEnumerable<dynamic> list = _templateKeyService.GetDataByQuery(sql, arrPara).ToList();
            return list;
        }


        private string GetTypeTime(int actionLevel)
        {
            switch (actionLevel)
            {
                case 1:
                    return "yearkey";
                case 2:
                    return "halfkey";
                case 3:
                    return "quarterkey";
                case 4:
                    return "monthkey";
                case 5:
                    return "weekkey";
                case 6:
                    return "datekey";
                default:
                    return "minutekey";
            }
        }

        // Class Link in ViewTree Iframe
        public class Link {
            public string name { get; set; }
            public string mainProcess {get; set;}
            public string nodeName { get; set; }
            public string direction { get; set; }
            public string content { get; set; }
        }
        public class Tree {
            public List<Children> tree { get; set; }
        }
        public class ListId {
            public Guid id { get; set; }
            public Guid parentId { get; set; }
            public int userReceiveId { get; set; }
        }
        public class Children  {
            public Guid id { get; set; }
            public string name { get; set; }
            public string nodeName { get; set; }
            public string departmentName { get; set; }
            public string positionName { get; set; }
            public string type { get; set; }
            public string level { get; set; }
            public string code { get; set; }
            public string label { get; set; }
            public string version { get; set; }
            public Link link { get; set; }
            public Guid parentId { get; set; }
            public List<Children> children { get; set; }
        }
        // Class Transfer of Content
        public class Transfer {
            public string label { get; set; }
            public string department { get; set; }
            public string value { get; set; }
            public string type { get; set; }
            public string isMobile { get; set; }
        }
        // Class Content of Table Comment
        public class Contents {
            public string Content { get; set; }
            public string SubContent { get; set; }
            public List<Transfer> Transfers { get; set; }
        }
    }
}