using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Domain.License;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Data;
using System.Xml;
using Bkav.eGovCloud.Business.Utils;
using System.Web;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Configuration;
using Bkav.eGovCloud.DataAccess;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Business.Objects;
using static Bkav.eGovCloud.Controllers.SearchDocumentController;
using System.Data.SqlClient;

namespace Bkav.eGovCloud.Controllers
{
	//[RequireHttps]
	[EgovAuthorize]
	public class DashboardController : CustomerBaseController
	{
		#region properties

		private readonly AdminGeneralSettings _generalSettings;
		private readonly ImageSettings _insertImageSettings;
		private readonly DocTypeBll _docTypeService;
		private readonly ProcessFunctionBll _processFunctionService;
		private readonly ProcessFunctionGroupBll _processFunctionGroupService;
		private readonly DocumentCopyBll _docCopyService;
		private readonly DocumentBll _docService;
		private readonly EgovSearch _searchService;
		private readonly DocFieldBll _docFieldService;
		private readonly UserBll _userService;
		private readonly JobTitlesBll _jobTitlesService;
		private readonly PositionBll _positionService;
		private readonly DocFinishBll _docFinishService;
		private readonly WorktimeHelper _workTimeHelper;
		private readonly SmsSettings _smsSettings;
		private readonly LuceneBll _luceneService;
		private readonly UserActivityLogBll _userActivityLogService;
		private readonly ResourceBll _resourceService;
		private readonly FileUploadSettings _fileUploadSettings;
		private readonly UserConnectionBll _userConnectionService;
		private readonly DocumentOnlineBll _docOnlineService;
		private readonly Helper.UserSetting _helperUserSetting;
		private readonly ConnectionSettings _connectionSettings;
		private readonly SsoSettings _ssoSettings;
		private readonly LanguageSettings _languageSettings;
		private readonly DepartmentBll _departmentService;
		private readonly PermissionBll _permissionSerice;
		private readonly TreeGroupBll _treeGroupSerice;
		private readonly DocColumnSettingBll _docColumnSettingSerice;
		private readonly InfomationBll _infoService;
		private readonly CategoryBll _categoryService;
		private readonly SignatureBll _signatureService;

		private readonly CodeBll _codeService;
		private readonly StorePrivateBll _storePrivateService;
		private readonly CommonCommentBll _commonCommentService;

		private readonly OnlineRegistrationSettings _onlineRegistrationSettings;
		private readonly FAQSetting _faqSettings;
		private readonly AttachmentBll _attachmentService;
		private readonly SettingBll _settingService;
		private readonly VoteSettings _voteSettings;
		private readonly VersionTreeSetting _versionTreeSettings;
		private readonly AuthenticationSettings _authenSetting;
        private readonly WorkflowHelper _workflowHelper;
        private readonly TemplateKeyBll _templateKeyService;

        private readonly ReportModeBll _reportModeService;

        private readonly ActionLevelBll _actionLevelSerivec;

        private LicenseSettings _license;
        private IndicatorValueDepartmentBll _inDepartService;

		private const string COLUMN_IS_VIEWED = "IsViewed";
		private const string COLUMN_KEY = "DocumentCopyId";
		private const string COLUMN_COMPENDIUM = "Compendium";
		private const string COLUMN_DOCTYPE_ID = "DocTypeId";
		private const string EMOTICONS_PATH = "~/Content/bkav.egov/emoticons";
        private readonly DocumentRelatedBll _docRelatedService;
        #endregion properties

        #region constructor

        public DashboardController(
			DocumentOnlineBll docOnlineService,
			AdminGeneralSettings generalSettings,
			ImageSettings insertImageSettings,
			DocTypeBll docTypeBll,
			DocumentCopyBll documentCopyService,
			DocumentBll documentService,
			ProcessFunctionBll processFunctionService,
			ProcessFunctionGroupBll processFunctionGroupService,
			EgovSearch searchService,
			DocFieldBll docFieldService,
			UserBll userService,
			DocFinishBll docFinishService,
			WorktimeHelper workTimeHelper,
			SmsSettings smsSettings,
			UserActivityLogBll userLogServices,
			LuceneBll luceneService,
			UserActivityLogBll userActivityLogService,
			UserConnectionBll userConnectionService,
			ResourceBll resourceService,
			FileUploadSettings fileUploadSettings,
			Helper.UserSetting helperUserSetting,
			DepartmentBll departmentService,
			PermissionBll permissionSerice,
			LanguageSettings languageSettings,
			OnlineRegistrationSettings onlineRegistrationSettings,
			TreeGroupBll treeGroupSerice,
			DocColumnSettingBll docColumnSettingSerice,
			InfomationBll infoService,
			ConnectionSettings connectionSettings,
			FAQSetting faqSettings,
			CategoryBll categoryService,
			SettingBll settingService, AuthenticationSettings authenSettings,
			VoteSettings voteSetting, StorePrivateBll storePrivateService,
			VersionTreeSetting versionTreeSettings, PositionBll positionService,
			AttachmentBll attachmentService, JobTitlesBll jobTitlesService,
			SignatureBll signatureService, CommonCommentBll commonCommentService,
			CodeBll codeService, DocumentRelatedBll docRelatedService, ReportModeBll reportModeService,ActionLevelBll actionLevelService, IndicatorValueDepartmentBll inDepartService, TemplateKeyBll templateKeyService)
		{
			_docOnlineService = docOnlineService;
			_generalSettings = generalSettings;
			_insertImageSettings = insertImageSettings;
			_docTypeService = docTypeBll;
			_processFunctionService = processFunctionService;
			_searchService = searchService;
			_docCopyService = documentCopyService;
			_docFieldService = docFieldService;
			_userService = userService;
			_docFinishService = docFinishService;
			_workTimeHelper = workTimeHelper;
			_smsSettings = smsSettings;
			_luceneService = luceneService;
			_userActivityLogService = userActivityLogService;
			_userConnectionService = userConnectionService;
			_resourceService = resourceService;
			_fileUploadSettings = fileUploadSettings;
			_processFunctionGroupService = processFunctionGroupService;
			_ssoSettings = SsoSettings.Instance;
			_connectionSettings = connectionSettings;
			_helperUserSetting = helperUserSetting;
			_departmentService = departmentService;
			_permissionSerice = permissionSerice;
			_languageSettings = languageSettings;
			_onlineRegistrationSettings = onlineRegistrationSettings;
			_treeGroupSerice = treeGroupSerice;
			_docColumnSettingSerice = docColumnSettingSerice;
			_infoService = infoService;
			_settingService = settingService;
			_faqSettings = faqSettings;
			_docService = documentService;
			_license = LicenseSettings.Current;
			_categoryService = categoryService;
			_voteSettings = voteSetting;
			_versionTreeSettings = versionTreeSettings;
			_attachmentService = attachmentService;
			_signatureService = signatureService;
			_jobTitlesService = jobTitlesService;
			_positionService = positionService;
			_storePrivateService = storePrivateService;
			_commonCommentService = commonCommentService;
			_authenSetting = authenSettings;
			_codeService = codeService;
            _docRelatedService = docRelatedService;
            _reportModeService = reportModeService;
            _workflowHelper = DependencyResolver.Current.GetService<WorkflowHelper>();
            _actionLevelSerivec = actionLevelService;
            _inDepartService = inDepartService;
            _templateKeyService = templateKeyService;
        }

		#endregion constructor

        public ActionResult Index()
        {
			var appSettings = ConfigurationManager.AppSettings;
			ViewBag.ProcessFunctionId = appSettings["dashboardProcessFunctionId"];

			ViewBag.CurrentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
				.Where(p => p.UserId == User.GetUserId())
				.OrderBy(p => p.IsPrimary)
				.FirstOrDefault();
			ViewBag.SoLieu_ChoXuLy = 0;
			ViewBag.SoLieu_ChoDuyet = 0;
			ViewBag.SoLieu_QuaHan = 0;

			ViewBag.TuongMinh_ChoXuLy = 0;
			ViewBag.TuongMinh_ChoDuyet = 0;
			ViewBag.TuongMinh_QuaHan = 0;

			ViewBag.TongHop_ChoXuLy = 0;
			ViewBag.TongHop_ChoDuyet = 0;
			ViewBag.TongHop_QuaHan = 0;

			var connectionString = DataSettings.Current.DataConnectionString;
			var connection = new MySqlConnection(connectionString);

			using (var context = new EfContext(connection))
			{
				ViewBag.SoLieu_ChoXuLy = context.RawQuery(GetSql("SoLieu", "ChoXuLy")).First().Count;
				ViewBag.TuongMinh_ChoXuLy = context.RawQuery(GetSql("TuongMinh", "ChoXuLy")).First().Count;
				ViewBag.TongHop_ChoXuLy = context.RawQuery(GetSql("TongHop", "ChoXuLy")).First().Count;

				ViewBag.SoLieu_ChoDuyet = context.RawQuery(GetSql("SoLieu", "ChoDuyet")).First().Count;
				ViewBag.TuongMinh_ChoDuyet = context.RawQuery(GetSql("TuongMinh", "ChoDuyet")).First().Count;
				ViewBag.TongHop_ChoDuyet = context.RawQuery(GetSql("TongHop", "ChoDuyet")).First().Count;

				//ViewBag.SoLieu_QuaHan = context.RawQuery(GetSqlQuaHan(1)).First().Count;
				//ViewBag.TuongMinh_QuaHan = context.RawQuery(GetSqlQuaHan(2)).First().Count;
				//ViewBag.TongHop_QuaHan = context.RawQuery(GetSqlQuaHan(3)).First().Count;
			}

			return View();
        }

		private string GetSql(string type, string status)
		{
			var sqlJoinForm = string.Empty;
			var sqlCategoryBusinessId = string.Empty;
			var sqlFormCategoryId = string.Empty;
			if (type == "SoLieu" || type == "TongHop")
			{
				sqlJoinForm = "INNER JOIN `form` as `f` ON `d`.`FormId` = `f`.`FormId`";
				sqlCategoryBusinessId = "AND `d`.CategoryBusinessId NOT IN (8, 16)";
				sqlFormCategoryId = $"AND `f`.`FormCategoryId` = {(type == "SoLieu" ? 1 : 3)}";
			}
			else if (type == "TuongMinh")
			{
				sqlCategoryBusinessId = "AND `d`.CategoryBusinessId = 8";
			}

             
			var sqlStatus = string.Empty;
			var sqlUser = string.Empty;
			if (status == "ChoXuLy")
			{
				sqlStatus = "WHERE `dc`.`Status` = 2";
				sqlUser = $"AND `dc`.`UserCurrentId` = {_userService.CurrentUser.UserId}";
			}
			else if (status == "ChoDuyet")
			{
				sqlStatus = "WHERE `dc`.`Status` IN (2, 16)";
				sqlUser = $@"AND dc.UserNguoiThamGia LIKE CONCAT('%;',{_userService.CurrentUser.UserId},';%')
AND `dc`.`UserCurrentId` != {_userService.CurrentUser.UserId}
AND `dc`.HasJustCreated != 1";
			}

			return $@"SELECT Count(1) AS Count
FROM `document` as `d`
INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId`
{sqlJoinForm}
{sqlStatus}
AND `d`.`Status`  in (2, 16)
{sqlCategoryBusinessId}
{sqlUser}
AND `dc`.`DocumentCopyType` in (1, 2, 4)
{sqlFormCategoryId};";
		}

        public ActionResult StatisticDocument()
        {
            var reportMode = _reportModeService.Gets();           
            return View(reportMode);
        }

        public ActionResult StatisticIndicator()
        {
            var reportMode = _reportModeService.Gets();
            return View(reportMode);
        }

        public ActionResult StatisticDocumentDepartment()
        {
            var reportMode = _reportModeService.Gets();
            var listSelect = new List<SelectListItem>();
            listSelect.Add(new SelectListItem()
            {
                Selected = true,
                Text = "Chọn chế độ báo cáo",
                Value = ""
            });
            foreach(var item in reportMode)
            {
                listSelect.Add(new SelectListItem()
                {
                    Selected = false,
                    Text = item.Name,
                    Value = item.ReportModeId.ToString()
                });
            }
            ViewBag.ListReportMode = listSelect;
            return View(reportMode);
        }

        public JsonResult ViewTimeReports(Guid id, string  departmentName, string categoryBusinessId)
        {
            var documentListDocType = _docService.GetToDocId(id).DocTypeId;
            int categoryId = 0;
            if(categoryBusinessId == "Báo cáo số liệu")
            {
                categoryId = 4;
            }
            else if(categoryBusinessId == "Báo cáo thuyết minh")
            {
                categoryId = 8;
            }
            var documentList = _docService.Gets(true, d => d.DocTypeId == documentListDocType
                                   && d.InOutPlace == departmentName && d.CategoryBusinessId == categoryId)
                                   .OrderByDescending(d => d.DateCreated);
            return Json(documentList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataIndicator(string listDepartId, int year)
        {
            var param = new List<object>
            {
                new SqlParameter("@timekey", year)
            };
            var sql = RenderSQLforIndicator(listDepartId, year);
            var arrPara = param.ToArray();
            var template = _templateKeyService.GetDataByQuery(sql, arrPara);
            return Json(new {data= template}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDepartmentIndicator()
        {
            var param = new List<object>
            {
                new SqlParameter("@timekey", 2020)
            };
            var sql = @"select DISTINCT d.DepartmentName, d.DepartmentId from department d 
                        join indicatorvaluedepartment ivd on d.DepartmentId = ivd.DepartmentId";
            var arrPara = param.ToArray();
            var template = _templateKeyService.GetDataByQuery(sql, arrPara);
            return Json(new { data = template }, JsonRequestBehavior.AllowGet);
        }

        public string RenderSQLforIndicator(string listDepartId, int year)
        {
            var listInt = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(listDepartId);
            var departs = _departmentService.GetCacheAllDepartments();
            var dept = departs.Where(d => listInt.Contains(d.DepartmentId));
            var sql = "";
            var i = 1;
            foreach (var item in dept)
            {
                sql += RenderRowDataSQL(item.Emails, item.Emails, year, item.DepartmentName);
                if (i < dept.Count())
                {
                    sql += " union ";
                }
                i++;
            }
            return sql;
        }

        private string GetSqlReportMode(string type, string status)
        {
            var sqlJoinForm = string.Empty;
            var sqlCategoryBusinessId = string.Empty;
            var sqlFormCategoryId = string.Empty;
            if (type == "SoLieu" || type == "TongHop")
            {
                sqlJoinForm = "INNER JOIN `form` as `f` ON `d`.`FormId` = `f`.`FormId`";
                sqlCategoryBusinessId = "AND `d`.CategoryBusinessId NOT IN (8, 16)";
                sqlFormCategoryId = $"AND `f`.`FormCategoryId` = {(type == "SoLieu" ? 1 : 3)}";
            }
            else if (type == "TuongMinh")
            {
                sqlCategoryBusinessId = "AND `d`.CategoryBusinessId = 8";
            }


            var sqlStatus = string.Empty;
            var sqlUser = string.Empty;
            if (status == "ChoXuLy")
            {
                sqlStatus = "WHERE `dc`.`Status` = 2";
                sqlUser = $"AND `dc`.`UserCurrentId` = {_userService.CurrentUser.UserId}";
            }
            else if (status == "ChoDuyet")
            {
                sqlStatus = "WHERE `dc`.`Status` IN (2, 16)";
                sqlUser = $@"AND dc.UserNguoiThamGia LIKE CONCAT('%;',{_userService.CurrentUser.UserId},';%')
AND `dc`.`UserCurrentId` != {_userService.CurrentUser.UserId}
AND `dc`.HasJustCreated != 1";
            }

            return $@"SELECT Count(1) AS Count
FROM `document` as `d`
INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId` INNER JOIN `doctype` dt on dt.DocTypeId = dc.DocTypeId 
 INNER JOIN `reportmodes` rm on rm.ReportModeId = dt.ReportModeId 
{sqlJoinForm}
{sqlStatus}
AND `d`.`Status`  in (2, 16)
{sqlCategoryBusinessId}
{sqlUser}
AND `dc`.`DocumentCopyType` in (1, 2, 4)
{sqlFormCategoryId};";
        }

        private string RenderRowDataSQL(string localityKey,string organization, int year, string nameDepart)
        {
            
            var list = new List<ReportIndicatorModel>();
            list.Add(new ReportIndicatorModel { Name = "N", LocalityKey = localityKey, Organization = organization, TimeType = "yearkey", Timekey = year*10000 + 101 });
            list.Add(new ReportIndicatorModel { Name = "NN", LocalityKey = localityKey, Organization = organization, TimeType = "halfkey", Timekey = year*10000 + 101 });
            list.Add(new ReportIndicatorModel { Name = "Q1", LocalityKey = localityKey, Organization = organization, TimeType = "quarterkey", Timekey = year*10000 + 101 });
            list.Add(new ReportIndicatorModel { Name = "Q2", LocalityKey = localityKey, Organization = organization, TimeType = "quarterkey", Timekey = year*10000 + 401 });
            list.Add(new ReportIndicatorModel { Name = "Q3", LocalityKey = localityKey, Organization = organization, TimeType = "quarterkey", Timekey = year*10000 + 701 });
            list.Add(new ReportIndicatorModel { Name = "Q4", LocalityKey = localityKey, Organization = organization, TimeType = "quarterkey", Timekey = year*10000 + 1001 });

            list.Add(new ReportIndicatorModel { Name = "T01", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0101 });
            list.Add(new ReportIndicatorModel { Name = "T02", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0201 });
            list.Add(new ReportIndicatorModel { Name = "T03", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0301 });
            list.Add(new ReportIndicatorModel { Name = "T04", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0401 });
            list.Add(new ReportIndicatorModel { Name = "T05", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0501 });
            list.Add(new ReportIndicatorModel { Name = "T06", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0601 });
            list.Add(new ReportIndicatorModel { Name = "T07", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0701 });
            list.Add(new ReportIndicatorModel { Name = "T08", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0801 });
            list.Add(new ReportIndicatorModel { Name = "T09", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 0901 });
            list.Add(new ReportIndicatorModel { Name = "T10", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 1001 });
            list.Add(new ReportIndicatorModel { Name = "T11", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 1101 });
            list.Add(new ReportIndicatorModel { Name = "T12", LocalityKey = localityKey, Organization = organization, TimeType = "monthkey", Timekey = year*10000 + 1201 });
            var sql = "select '"+ nameDepart + "' as 'donvi', ";
            var i = 1;
            foreach (var item in list)
            {
                sql += string.Format("Get_StatisticIndicator('{0}', '{1}', {2}, '{3}') as {4}", item.LocalityKey, item.Organization, item.Timekey, item.TimeType, item.Name);
                if (i < list.Count())
                {
                    sql += ",";
                }
                i++;
            }
            return sql;
        } 

        public JsonResult GetReport(int reportModeid)
        {
            var doctypes = _docTypeService.Gets(d => d.ReportModeId.HasValue && d.ReportModeId == reportModeid);

            return Json(doctypes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReportStatus(int reportModeid, int actionCode,string timekey_,  bool getss)
        {
            var doctypes = new List<DocType>();
            var doctypesReportModeNew = new List<DocType>();
            var actionLevels = _actionLevelSerivec.Get(actionCode);

            if(getss == true)
            {
                doctypes = _docTypeService.Gets(d => d.ReportModeId.HasValue).ToList();
            }
            else
            {
                doctypes = _docTypeService.Gets(d => d.ReportModeId.HasValue && d.ReportModeId == reportModeid && d.ActionLevel == actionLevels.ActionLevelId).ToList();
            }  
            var allUserIds = _userService.GetAllUserIds(true);
            var departs = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
            var reportStatuses = new List<ReportStatus>();
          
            if (doctypes != null && doctypes.Any())
            {
                
                foreach (var doctype in doctypes)
                {
                    var reportStatus = new ReportStatus();
                    var reportStatusDetail = new List<ReportStatusDetail>();
                    reportStatus.doctype = doctype;
                    var actionLevel = _docTypeService.Get(doctype.DocTypeId);
                    var actionLevelName = _actionLevelSerivec.GetIdAc(actionLevel.ActionLevel);
                    var reportModels = _reportModeService.GetId(doctype.ReportModeId);

                    var workflow = _docTypeService.GetWorkflowActive(doctype.DocTypeId);
                    if(workflow == null)
                    {
                        continue;
                    }
                    var userSendIds = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any());
                    var stt = 0;
                    foreach (var userSend in userSendIds)
                    {
                        var userDepart = departs.Where(p => p.UserId == userSend).OrderBy(p => p.IsPrimary).FirstOrDefault();
                        if (userDepart != null)
                        {
                            stt++;
                            reportStatusDetail.Add(new ReportStatusDetail()
                            {
                                DoctypeId = doctype.DocTypeId,
                                DepartmentName = userDepart.DepartmentName,
                                UserName = userDepart.UserFullName,
                                OrganizeKey = userDepart.Emails,
                                nameDocType = reportModels.Name,
                                ActionLevel = actionLevelName.ActionLevelName,
                                Stt = stt,
                                docmodeId = reportModels.ReportModeId,
                                CompendiumDefault = doctype.DocTypeName,
                                CategoryBussiness = doctype.CategoryBusinessId
                            });
                        }
                    }

                    reportStatus.reportDoctype = reportStatusDetail;
                    reportStatuses.Add(reportStatus);
                }
            }

            foreach (var reportStatus in reportStatuses)
            {
                if(getss == true)
                {
                    var documents = _docService.GetsAs(x => new ReportStatusDocument()
                    {
                        OrganizationCode = x.OrganizationCode,
                        StatusReport = x.StatusReport.HasValue ? x.StatusReport.Value : 0,
                        Status = x.Status,
                        DateCreated = x.DateCreated,
                        CategoryBusinness = x.CategoryBusinessId,
                        DocumentId = x.DocumentId

                    }, x => x.DocTypeId == reportStatus.doctype.DocTypeId);

                    foreach (var it in reportStatus.reportDoctype)
                    {
                        var docs = documents.Where(dc => dc.OrganizationCode == it.OrganizeKey);
                        if (docs != null && docs.Any())
                        {
                            var doc = docs.OrderByDescending(d => d.DateCreated).FirstOrDefault();
                            it.Status = doc.Status;
                            it.StatusReport = doc.StatusReport;
                            it.CategoryBussiness = doc.CategoryBusinness;
                        }
                    }
                }
                else
                {
                    var documents = _docService.GetsAs(x => new ReportStatusDocument()
                    {
                        OrganizationCode = x.OrganizationCode,
                        StatusReport = x.StatusReport.HasValue ? x.StatusReport.Value : 0,
                        Status = x.Status,
                        DateCreated = x.DateCreated,
                        CategoryBusinness = x.CategoryBusinessId
                    }, x => x.DocTypeId == reportStatus.doctype.DocTypeId && x.TimeKey == timekey_);
                    foreach (var it in reportStatus.reportDoctype)
                    {
                        var docs = documents.Where(dc => dc.OrganizationCode == it.OrganizeKey);
                        if (docs != null && docs.Any())
                        {
                            var doc = docs.OrderByDescending(d => d.DateCreated).FirstOrDefault();
                            it.Status = doc.Status;
                            it.CategoryBussiness = doc.CategoryBusinness;
                            it.StatusReport = doc.StatusReport;
                        }
                    }
                }                 
            }

            return Json(reportStatuses, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDataDocRelated()
        {
            var result = _docRelatedService.Gets().OrderByDescending(c => c.CreatedAt);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult GetDepartment()
        {

            var departs = _departmentService.GetTreeParentDepartment();
            var result = departs.Select(d => new
            {
                id = d.DepartmentId.ToString(),
                parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
                text = d.DepartmentName,
                order = d.Order,
                level = d.Level
                //userDepartmentJobTitlesPositionId = d.UserDepartmentJobTitlesPositionId,
                //userId = d.UserId,
                //departmentIdExt = d.DepartmentIdExt,
                //jobTitlesId = d.JobTitlesId,
                //positionId = d.PositionId,
                //isPrimary = d.IsPrimary,
                //isAdmin = d.IsAdmin,
                //hasReceiveDocument = d.HasReceiveDocument,
                //username = d.Username,
                //userFullName = d.UserFullName
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult GetCatalog(Guid inCatalogId)
        //{
        //    var inCatalogValue = _inCatalogValueService.Gets(c => c.InCatalogId == inCatalogId);

        //    var indicatorValues = inCatalogValue.Select(d => new {
        //        id = d.InCatalogValueId.ToString(),
        //        name = d.InCatalogValueName,
        //        parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
        //        order = d.Order,
        //        level = d.Level,
        //        catalog = d.InCatalogId.ToString(),
        //        text = d.InCatalogValueName,
        //        code = d.InCatalogValueName,
        //        unit = d.Unit.ToString()
        //    });
        //    return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        //}

        //statiticDocumentDepartment

        public JsonResult GetAllStatiticDocument(List<int?> depart, int reportModeid, int actionCode, 
            string timekey_, int? Status, int? StatusReport_1, int? StatusReport_2)
        {
            var listObj = new List<ReportStatusDetail_New>();
            //var listDepart = _departmentService.GetByID(depart);
            //var departEmails = "";
            var strQueryUpdate = "";
            if (depart != null)
            {
                foreach(var item1 in depart)
                {
                    var strQuery = "SELECT dpm.DepartmentName, d.DocumentId, d.Compendium, d.DateCreated, d.UserCreatedId,"
                            + " d.`Status`, d.CategoryBusinessId as CategoryBusinessDocument,"
                            + " d.InOutPlace, d.OrganizationCode, d.UserCreatedName,"
                            + " d.DocTypeName as DocTypeNameDocument, d.TimeKey, d.StatusReport, d.DocTypeCode,"
                            + " dt.ReportModeId, dt.CategoryBusinessId as CategoryBusinessDoctype,dt.DocTypeId,"
                            + " dt.DocTypeName as DocTypeNameDocType, dt.CreatedByUserId, dt.ActionLevel, rmode.`Code`,rmode.`Name`"
                            + " FROM doctype dt LEFT JOIN reportmodes rmode"
                            + " ON dt.ReportModeId = rmode.ReportModeId"
                            + " LEFT JOIN document d ON d.DocTypeId = dt.DocTypeId"
                            + " LEFT JOIN department dpm ON dpm.Emails = d.OrganizationCode"
                            + " WHERE rmode.ReportModeId = {0} AND dt.ActionLevel = {1} AND d.TimeKey = {2} AND dpm.DepartmentId = {3}";
                    strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_, item1);
                    if (Status != null && StatusReport_1 != null && StatusReport_2 != null)
                    {
                        if (Status == 0)
                        {
                            strQuery += " AND d.`Status` = {4}";
                            strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_, item1, Status);
                        }
                        else if (Status == 2 && (StatusReport_1 == 2 || StatusReport_2 == 1))
                        {
                            strQuery += " AND d.`Status` = {4} AND (d.StatusReport = {5} OR d.StatusReport = {6})";
                            strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_, item1, Status, StatusReport_1, StatusReport_2);
                        }
                    }
                    if (StatusReport_1 != null && Status != null && StatusReport_2 != null)
                    {
                        if (StatusReport_1 == 4 && (Status == 2 || Status == 4))
                        {
                            strQuery += " AND d.`StatusReport` = {4} AND (d.Status = {5} OR d.Status = {6})";
                            strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_, item1, Status, StatusReport_1, StatusReport_2);
                        }
                    }
                    //departEmails = listDepart.Emails;

                    using (var context = new EfContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
                    {
                        var resultUserCreateName = context.RawQuery(strQueryUpdate);
                        foreach (var item in resultUserCreateName)
                        {
                            listObj.Add(new ReportStatusDetail_New()
                            {
                                DocumentId = item.DocumentId,
                                Compendium = item.Compendium,
                                CreatedByUserId = item.CreatedByUserId,
                                DateCreated = item.DateCreated,
                                UserCreatedId = item.UserCreatedId,
                                Status = item.Status,
                                CategoryBusinessDocument = item.CategoryBusinessDocument,
                                InOutPlace = item.InOutPlace,
                                OrganizationCode = item.OrganizationCode,
                                UserCreatedName = item.UserCreatedName,
                                DocTypeNameDocument = item.DocTypeNameDocument,
                                TimeKey = item.TimeKey,
                                StatusReport = item.StatusReport,
                                DocTypeCode = item.DocTypeCode,
                                ReportModeId = item.ReportModeId,
                                CategoryBusinessDoctype = item.CategoryBusinessDoctype,
                                DocTypeNameDocType = item.DocTypeNameDocType,
                                DocTypeId = item.DocTypeId,
                                ActionLevel = item.ActionLevel,
                                Code = item.Code,
                                Name = item.Name
                            });
                        }
                    }
                }

            }
            else
            {
                var strQuery = "SELECT dpm.DepartmentName, d.DocumentId, d.Compendium, d.DateCreated, d.UserCreatedId,"
                            + " d.`Status`, d.CategoryBusinessId as CategoryBusinessDocument,"
                            + " d.InOutPlace, d.OrganizationCode, d.UserCreatedName,"
                            + " d.DocTypeName as DocTypeNameDocument, d.TimeKey, d.StatusReport, d.DocTypeCode,"
                            + " dt.ReportModeId, dt.CategoryBusinessId as CategoryBusinessDoctype,dt.DocTypeId,"
                            + " dt.DocTypeName as DocTypeNameDocType, dt.CreatedByUserId, dt.ActionLevel, rmode.`Code`,rmode.`Name`"
                            + " FROM doctype dt LEFT JOIN reportmodes rmode"
                            + " ON dt.ReportModeId = rmode.ReportModeId"
                            + " LEFT JOIN document d ON d.DocTypeId = dt.DocTypeId"
                            + " LEFT JOIN department dpm ON dpm.Emails = d.OrganizationCode"
                            + " WHERE rmode.ReportModeId = {0} AND dt.ActionLevel = {1} AND d.TimeKey = {2}";
                strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_);
                if (Status != null && StatusReport_1 != null && StatusReport_2 != null)
                {
                    if (Status == 0)
                    {
                        strQuery += " AND d.`Status` = {4}";
                        strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_, depart, Status);
                    }else if(Status == 2 && (StatusReport_1 == 2 || StatusReport_2 == 1))
                    {
                        strQuery += " AND d.`Status` = {4} AND (d.StatusReport = {5} OR d.StatusReport = {6})";
                        strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_, depart, Status, StatusReport_1, StatusReport_2);
                    }
                }
                if (StatusReport_1 != null && Status != null && StatusReport_2 != null)
                {
                    if (StatusReport_1 == 4 && (Status == 2 || Status == 4))
                    {
                        strQuery += " AND d.`StatusReport` = {4} AND (d.Status = {5} OR d.Status = {6})";
                        strQueryUpdate = string.Format(strQuery, reportModeid, actionCode, timekey_, depart, Status, StatusReport_1, StatusReport_2);
                    }
                }
                using (var context = new EfContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
                {
                    var resultUserCreateName = context.RawQuery(strQueryUpdate);
                    foreach (var item in resultUserCreateName)
                    {
                        listObj.Add(new ReportStatusDetail_New()
                        {
                            DocumentId = item.DocumentId,
                            Compendium = item.Compendium,
                            CreatedByUserId = item.CreatedByUserId,
                            DateCreated = item.DateCreated,
                            UserCreatedId = item.UserCreatedId,
                            Status = item.Status,
                            CategoryBusinessDocument = item.CategoryBusinessDocument,
                            InOutPlace = item.InOutPlace,
                            OrganizationCode = item.OrganizationCode,
                            UserCreatedName = item.UserCreatedName,
                            DocTypeNameDocument = item.DocTypeNameDocument,
                            TimeKey = item.TimeKey,
                            StatusReport = item.StatusReport,
                            DocTypeCode = item.DocTypeCode,
                            ReportModeId = item.ReportModeId,
                            CategoryBusinessDoctype = item.CategoryBusinessDoctype,
                            DocTypeNameDocType = item.DocTypeNameDocType,
                            DocTypeId = item.DocTypeId,
                            ActionLevel = item.ActionLevel,
                            Code = item.Code,
                            Name = item.Name
                        });
                    }
                }
            }

            var listReportStatusDetail_New = new List<ReportStatusDetail_New>();
            
            var resultUpdate = listObj.Select(d => new {
                DocTypeId = d.DocTypeId,
            }).Distinct();

            var listObjnew = new List<ReportStatusDetail_New>();
            foreach (var item_ in resultUpdate)
            {
                var newnew = listObj.Where(d => d.DocTypeId == item_.DocTypeId)
                    .OrderByDescending(d => d.DateCreated).
                    FirstOrDefault();
                listObjnew.Add(newnew);
            }

            return Json(listObjnew, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReportStatiticDocument(int depart, int reportModeid, int actionCode, string timekey_)
        {
            var doctypes = new List<DocType>();
            var actionLevels = _actionLevelSerivec.Get(actionCode);
            var allUserIds = _userService.GetAllUserIds(true);
            var departs = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
            List<int> departTotal = new List<int>();
            departTotal.Add(depart);
            var departChild = _departmentService.GetChildrens(depart, true);
            if (departChild != null && departChild.Any())
            {
                departTotal.AddRange(departChild.Select(d => d.DepartmentId));
            }
            var departUser = departs.Where(d => departTotal.Contains(d.DepartmentId));
            var reportStatuses = new List<ReportStatus>();
            doctypes = _docTypeService.Gets(d => d.ReportModeId.HasValue && d.ReportModeId == reportModeid 
            && d.ActionLevel == actionLevels.ActionLevelId).ToList();
            if (doctypes != null && doctypes.Any())
            {

                foreach (var doctype in doctypes)
                {
                    var reportStatus = new ReportStatus();
                    var reportStatusDetail = new List<ReportStatusDetail>();
                    
                    var workflow = _docTypeService.GetWorkflowActive(doctype.DocTypeId);
                    if (workflow == null)
                    {
                        continue;
                    }
                    var userSendIds = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any());
                    var stt = 0;
                    foreach (var userSend in userSendIds)
                    {
                        var userDepart = departUser.Where(p => p.UserId == userSend).OrderBy(p => p.IsPrimary).FirstOrDefault();
                        if (userDepart != null)
                        {
                            reportStatus.doctype = doctype;
                            var actionLevel = _docTypeService.Get(doctype.DocTypeId);
                            var actionLevelName = _actionLevelSerivec.GetIdAc(actionLevel.ActionLevel);
                            var reportModels = _reportModeService.GetId(doctype.ReportModeId);

                            stt++;
                            reportStatusDetail.Add(new ReportStatusDetail()
                            {
                                DoctypeId = doctype.DocTypeId,
                                DepartmentName = userDepart.DepartmentName,
                                UserName = userDepart.UserFullName,
                                OrganizeKey = userDepart.Emails,
                                nameDocType = reportModels.Name,
                                ActionLevel = actionLevelName.ActionLevelName,
                                Stt = stt,
                                docmodeId = reportModels.ReportModeId,
                                CompendiumDefault = doctype.DocTypeName,
                                CategoryBussiness = doctype.CategoryBusinessId
                            });
                            reportStatus.reportDoctype = reportStatusDetail;    
                        }
                        
                    }

                    if(reportStatus.reportDoctype != null && reportStatus.reportDoctype.Any())
                    {
                        reportStatuses.Add(reportStatus);
                    }
                    
                }
            }


            foreach (var reportStatus in reportStatuses)
            {
                
                var documents = _docService.GetsAs(x => new ReportStatusDocument()
                {
                    OrganizationCode = x.OrganizationCode,
                    StatusReport = x.StatusReport.HasValue ? x.StatusReport.Value : 0,
                    Status = x.Status,
                    DateCreated = x.DateCreated,
                    CategoryBusinness = x.CategoryBusinessId
                }, x => x.DocTypeId == reportStatus.doctype.DocTypeId && x.TimeKey == timekey_);
                foreach (var it in reportStatus.reportDoctype)
                {
                    var docs = documents.Where(dc => dc.OrganizationCode == it.OrganizeKey);
                    if (docs != null && docs.Any())
                    {
                        var doc = docs.OrderByDescending(d => d.DateCreated).FirstOrDefault();
                        it.Status = doc.Status;
                        it.CategoryBussiness = doc.CategoryBusinness;
                        it.StatusReport = doc.StatusReport;
                    }
                }
                
            }


            return Json(reportStatuses, JsonRequestBehavior.AllowGet);
        }


    }

    public class ReportStatus
    {
        public DocType doctype { get; set; }
        public List<ReportStatusDetail> reportDoctype { get; set; }
        public List<ReportStatusDetail> reportChildrens { get; set; }
    }

    public class ReportStatusDetail_New
    {
        public Guid? DocumentId { get; set; }
        public string Compendium { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserCreatedId { get; set; }
        public int? Status { get; set; }
        public int? CategoryBusinessDocument { get; set; }
        public string InOutPlace { get; set; }
        public string OrganizationCode { get; set; }
        public string UserCreatedName { get; set; }
        public string DocTypeNameDocType { get; set; }
        public string DocTypeNameDocument { get; set; }
        public string TimeKey { get; set; }
        public int? StatusReport { get; set; }
        public string DocTypeCode { get; set; }
        public int? ReportModeId { get; set; }
        public int? CategoryBusinessDoctype { get; set; }
        public Guid DocTypeId { get; set; }
        public int? ActionLevel { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ReportStatusDetail
    {
        public Guid DoctypeId { get; set; }
        public string DepartmentName { get; set; }
        public string UserName { get; set; }
        public string OrganizeKey { get; set; }
        public int Status { get; set; }
        public string nameDocType { get; set; }

        public string ActionLevel { get; set; }
        public int Stt { get; set; }

        public string CompendiumDefault { get; set; }

        public int CategoryBussiness { get; set; }

        public Guid DocumentId { get; set; }

        public int StatusReport { get; set; }

        public int docmodeId { get; set; }
    }

    public class ReportStatusDocument
    {
        public Guid DoctypeId { get; set; }
        public string OrganizationCode { get; set; }
        public int StatusReport { get; set; }
        public int Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public int CategoryBusinness { get; set; }

        public Guid DocumentId { get; set; }
    }

    public class ReportIndicatorModel
    {
        public string Name { get; set; }
        public string Organization { get; set; }
        public string LocalityKey { get; set; }
        public int Timekey { get; set; }
        public string TimeType { get; set; }
    }
}