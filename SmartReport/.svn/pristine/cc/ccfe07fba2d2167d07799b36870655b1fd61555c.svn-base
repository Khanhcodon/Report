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
using Renci.SshNet.Common;

namespace Bkav.eGovCloud.Controllers
{
	//[RequireHttps]
	[EgovAuthorize]
	public class HomeController : CustomerBaseController
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
        private readonly UserDepartmentJobTitlesPositionBll _userdepartService;

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
        private readonly FormBll _formService;
        private readonly DocTypeFormBll _doctypeformService;

        private LicenseSettings _license;

		private const string COLUMN_IS_VIEWED = "IsViewed";
		private const string COLUMN_KEY = "DocumentCopyId";
		private const string COLUMN_COMPENDIUM = "Compendium";
		private const string COLUMN_DOCTYPE_ID = "DocTypeId";
		private const string EMOTICONS_PATH = "~/Content/bkav.egov/emoticons";

		#endregion properties

		#region constructor

		public HomeController(
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
            CodeBll codeService,
            UserDepartmentJobTitlesPositionBll userdepartService,
            FormBll formService,
            DocTypeFormBll doctypeformService)
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
            _userdepartService = userdepartService;
            _formService = formService;
            _doctypeformService = doctypeformService;
        }

		#endregion constructor

		public ActionResult OverView()
		{
			var user = _userService.CurrentUser;
			ViewBag.Avatar = user.Avatar;
			ViewBag.FullName = user.FullName;
			ViewBag.ParentDomain = _ssoSettings.BkavSSOParentDomain;

			// ViewBag.Position = user.UserDepartmentJobTitless.First().DepartmentName;
			return View();
		}

		public ActionResult Statictis()
		{
			return View();
		}

		public ActionResult Main(bool isShowNotify = false)
		{
			var user = _userService.CurrentUser;
			if (user == null)
			{
				return RedirectToAction("LogOut", "Account");
			}

            if (_permissionSerice.HasPemission("WelcomeIndex"))//Nếu có quyền vào WelcomeIndex trong admin
            {
                ViewBag.IsAdmin = true;
                return RedirectToAction("General", "Setting", new { area = "Admin" });
            }
            var userId = user.UserId;
            var lstUserDepart = _userdepartService.GetbyUserId(userId).FirstOrDefault();
            var systemName = "";
            if (lstUserDepart != null)
            {
                var departId = lstUserDepart.DepartmentId;
                var departmentPath = _departmentService.Get(departId).DepartmentPath;
                if (departmentPath == null)
                {
                    systemName = "";
                }
                string[] lstSplitDepartment = departmentPath.Split('\\');
                if (lstSplitDepartment.Length == 2)
                {
                    systemName = lstSplitDepartment[1];
                }
                else
                {
                    systemName = lstSplitDepartment[2];
                }
            }
            else {
                systemName = "";
            }
           
            var isDisplayName = _infoService.IsDisplayName();
            if (isDisplayName == true)
            {
                ViewBag.SystemName2 = "";
                var systemname = _infoService.GetCurrentSystemName();
                ViewBag.SystemName = systemname;
            }
            else {
                ViewBag.SystemName2 = systemName;
                ViewBag.SystemName = "";
            }
            ViewBag.SystemApps = GetDefaulApps(_connectionSettings);

			ViewBag.Username = user.Username;
			ViewBag.FullName = user.FullName;
			ViewBag.UserId = user.UserId;
			ViewBag.AvatarPath = _helperUserSetting.GetAvaterPath();

			ViewBag.Avatar = user.Avatar;

			var infomation = _infoService.GetCurrentOfficeName();
			ViewBag.OfficeName = infomation;

            var infor = _infoService.First();
            ViewBag.NameFile = infor.ImagePath;

            var isDevVersion = false;
#if DEBUG
			isDevVersion = true;
#endif

			ViewBag.IsDevVersion = isDevVersion;

			ViewBag.IsCreateVote = _voteSettings.ListUserCreateVote().Contains(user.UserId) ? true : false;
			ViewBag.VersionValue = _versionTreeSettings.CacheVersion == null ? "DefaulValue" : _versionTreeSettings.CacheVersion;

			ViewBag.ParentDomain = _ssoSettings.BkavSSOParentDomain;

			if (Request.IsMobileOrTablet())
			{
				return RedirectToRoute(isShowNotify ? "MobileWithNotify" : "Mobile"); ;
			}

			return View("MainNew");
		}
		
		public ActionResult Index()
		{
			var allTreeGroups = _treeGroupSerice.GetCacheAllTreeGroups(true);
			if (allTreeGroups == null || !allTreeGroups.Any())
			{
				throw new ApplicationException(
					_resourceService.GetResource("Home.Index.NotConfigTreeGroup"));
			}

			ViewBag.AllTreeGroups = allTreeGroups.ToListModel();

			var user = _userService.CurrentUser;
			ViewBag.Avatar = user.Avatar;
			ViewBag.FullName = user.FullName;
			ViewBag.AvatarPath = _helperUserSetting.GetAvaterPath();

			ViewBag.HasOnlineRegistration = _onlineRegistrationSettings.ToModel().HasPermisson(user.UserId);
			ViewBag.OnlineRegistrationGroupId = _onlineRegistrationSettings.TreeGroupId;

			ViewBag.HasFAQ = _faqSettings.ToModel().HasPermisson(user.UserId);
			ViewBag.FAQGroupId = _faqSettings.TreeGroupId;
			ViewBag.UserId = user.UserId;
			ViewBag.ParentDomain = _ssoSettings.BkavSSOParentDomain;
			ViewBag.UserId = user.UserId;
            var appSettings = ConfigurationManager.AppSettings;
            var urlOnlyOffice = appSettings["urlOnlyOffice"] ?? "";
            ViewBag.UrlOnlyOffice = urlOnlyOffice;

            return View("Index");
		}

		public ActionResult Error()
		{
			return View();
		}

		public ActionResult LicenseHasExpired()
		{
			return View();
		}

		public JsonResult GetConnectionSettings()
		{
			var apps = JsonConvert.DeserializeObject<List<Apps>>(_connectionSettings.Apps);
			var bmail = apps.FirstOrDefault(a => a.Name.Equals("bmail", StringComparison.OrdinalIgnoreCase));
			var chat = apps.FirstOrDefault(a => a.Name.Equals("chat", StringComparison.OrdinalIgnoreCase));
			return Json(new
			{
				ParentDomain = _ssoSettings.BkavSSOParentDomain,
				MailType = _connectionSettings.MailType,
				BmailLink = bmail == null ? "" : bmail.AppUrl,
				ChatLink = chat == null ? "" : chat.AppUrl
			}, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetCommonConfigs()
		{
			var user = _userService.CurrentUser;
			var userId = user.UserId;

			var isMobile = Request.Browser.IsMobileDevice;

			// DVC setting
			var onlineRegistrationSettings = _onlineRegistrationSettings.ToModel();
			var faqSettings = _faqSettings.ToModel();

			// User settings
			var avatarPath = _generalSettings.Avatar;
			var userSettingModel = _helperUserSetting.GetUserSetting(user.UserSetting);

			// Notification
			var userNotify = _helperUserSetting.GetNotifyInfo(user.NotifyInfo);

            var allForm = _formService.Gets(null);
            var allFormDoctype = _doctypeformService.Gets(null);
            // Loại văn bản
            var doctypeAll = _docTypeService.GetsByUserId(userId).Select(dt => new
            {
                dt.DocTypeId,
                dt.DocTypeName,
                dt.DocFieldName,
                dt.ActionLevel,
                dt.DocFieldId,
                dt.CategoryBusinessId,
                Stores = dt.Stores.Where(s => s.ListUserViewIds.Contains(userId)).Select(s => new Store
                {
                    StoreId = s.StoreId,
                    StoreName = s.StoreName
                })
            })
            .Join(allFormDoctype, p => p.DocTypeId, pc => pc.DocTypeId, (p, pc) => new { p, pc })
            .Join(allForm, ppc => ppc.pc.FormId, c => c.FormId, (ppc, c) => new { ppc, c })
            .Select(t => new 
            {
                t.ppc.p.DocTypeId,
                t.ppc.p.DocTypeName,
                t.ppc.p.DocFieldName,
                t.ppc.p.ActionLevel,
                t.ppc.p.DocFieldId,
                t.ppc.p.CategoryBusinessId,
                t.ppc.p.Stores,
                t.c.FormCategoryId
            }); 
            var enumerable = doctypeAll.ToList();
            var doctypes = enumerable.Where(c => c.CategoryBusinessId != 32);
            // survey
            var survey = enumerable.Where(c => c.CategoryBusinessId == 32).Select(c=> new {           
                c.DocTypeId,
                c.DocTypeName,
                c.DocFieldName,
                c.DocFieldId,
                c.CategoryBusinessId,
                c.ActionLevel,
                _docTypeService.Get(c.DocTypeId).SurveyImgPath });
            
           
            var documentTemplates = GetDefaultDocumentTempate();

			// Cây văn bản
			var processFunction = _processFunctionService.GetsFromCache();

            var allTreeGroups = _treeGroupSerice.GetCacheAllTreeGroups(true);

            // Cấu hình chữ ký số
            var signerConfig = _signatureService.Gets(s => s.UserId == userId)
									.Select(s => new
									{
										Id = s.SignatureId,
										Title = s.SignatureName,
										FindText = s.SearchWord,
										FindType = s.IsFindType ? 1 : 0,
										SignType = s.IsTypeImage ? 0 : 1,
										PosType = s.SignaturePosition,
										OffsetX = 0,
										OffsetY = 0,
										TextInfor = s.IsDispplayCertificate ? 1 : 0,
										ImagePath = s.Image,
										Ext = s.ImageExtension,
										hasSameToken = true
									});

			//TienNVg them truong cho cau hinh type Chuc vu, chuc danh
			var typePositionTitleJob = _generalSettings.TypePositionTitleJob;

			// Phòng ban hiện tại.
			var currentDepts = _departmentService.GetsPath(userId);
			var primaryDepartment = _departmentService.GetPrimaryDepartment(userId);

			// Danh sách người dùng
			var allUsers = _userService.GetAllCached(true)
				.Select(u => new
				{
					value = u.UserId,
					label = u.Username + " - " + u.FullName,
					fullname = u.FullName,
					username = u.Username,
					avatar = u.Avatar,
					status = u.Status
				})
				.OrderBy(u => u.username);

			// Danh sách phòng ban
			var allDeps = _departmentService
				.GetCacheAllDepartments(true)
				.Select(u => new
				{
					value = u.DepartmentId,
					parentid = u.ParentId.HasValue ? u.ParentId : 0,
					data = u.DepartmentName,
					metadata = new { id = u.DepartmentId },
					idext = u.DepartmentIdExt,
					state = "leaf",
					order = u.Order,
					level = u.Level,
					attr = new { id = u.DepartmentId, rel = "dept", idext = u.DepartmentIdExt, label = u.DepartmentPath },
					label = u.DepartmentLabel
				})
				.OrderBy(u => u.idext);

			// Danh sách chức danh
			var allJobtitles = _jobTitlesService.GetCacheAllJobtitles()
				.Select(u => new
				{
					value = u.JobTitlesId,
					label = u.JobTitlesName,
					isApprover = u.IsApproved
				})
				.OrderBy(u => u.label);

			// Danh sách chức vụ
			var allPositions = _positionService.GetCacheAllPosition()
				.Select(u => new
				{
					value = u.PositionId,
					label = u.PositionName,
					level = u.PriorityLevel,
					isApprover = u.IsApproved
				})
				.OrderBy(u => u.label);

			// Danh sách liên quan người dùng - phòng ban
			var allUserDeps = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
								t => new
								{
									departmentid = t.DepartmentId,
									userid = t.UserId,
									username = t.Username,
									fullname = t.UserFullName,
									positionid = t.PositionId,
									idext = t.DepartmentIdExt,
									jobtitleid = t.JobTitlesId,
									hasReceiveDocument = t.HasReceiveDocument
								});

			// Hồ sơ cá nhân
			var storeShare = _storePrivateService.GetsStoreShared(userId, parentId: 0).Select(s => new
			{
				id = s.StorePrivateId,
				storePrivateId = s.StorePrivateId,
				name = s.StorePrivateName,
				descStorePrivate = s.Description,
				parentId = s.ParentId,
				level = s.Level,
				status = s.Status,
				userCreated = s.CreatedByUserId,
				isStoreShared = s.HasShared,
				isPrivate = !s.HasShared,
				userIdJoined = s.UserIdJoined,
				deptIdJoined = s.DeptIdJoined
			});

			var storePrivate = _storePrivateService.GetsStorePrivate(userId, parentId: 0).Select(s => new
			{
				id = s.StorePrivateId,
				storePrivateId = s.StorePrivateId,
				name = s.StorePrivateName,
				descStorePrivate = s.Description,
				parentId = s.ParentId,
				level = s.Level,
				status = s.Status,
				isPrivate = true,
				userCreated = s.CreatedByUserId,
				userIdJoined = s.UserIdJoined,
				deptIdJoined = s.DeptIdJoined,
				isStoreShared = s.HasShared
			});

			var commentComments = _commonCommentService.Gets(c => c.UserId == userId);

			var codeNotations = _codeService.GetCodeNotations(userId);

			var requirePublishPlan = _generalSettings.RequirePublishPlanWhenCreate;
			if (requirePublishPlan && _generalSettings.IgnoreRequirePublishPlanList.Any())
			{
				requirePublishPlan = !_generalSettings.IgnoreRequirePublishPlanList.Any(u => u.Trim().Equals(user.Username, StringComparison.OrdinalIgnoreCase));
			}

            var allDoctypes = _docTypeService.Gets(d => d.IsActivated == true).Select(dt => new
            {
                dt.DocTypeId,
                dt.DocTypeName,
                dt.DocFieldName,
                dt.DocFieldId,
                dt.CategoryBusinessId,
                dt.ActionLevel,
            }); ;

            /*
             * Với request này cần trả về tất cả dữ liệu cần thiết để client chạy được luôn.
             * Cũng xem bỏ dần cache dưới client.
             */

            var appSettings = ConfigurationManager.AppSettings;
            var urlOnlyOffice = appSettings["urlOnlyOffice"] ?? "";
            var domain = appSettings["domain"] ?? "";
            var forderReport = appSettings["ForderReport"] ?? "";

            return Json(new
            {
                eform = new
                {
                    urlOnlyOffice = urlOnlyOffice,
                    domain = domain,
                    forderReport = forderReport
                },
				isMobile = isMobile,
				user = new
				{
					userId = user.UserId,
					fullName = user.FullName,
					userName = user.Username,
					userDomainName = user.UsernameEmailDomain,
					userSetting = userSettingModel,
					avatarPath = avatarPath,
					avatar = user.Avatar,
					signerConfig = signerConfig,
					notifyConfig = userNotify,
					primaryDepartment = primaryDepartment == null ? "" : primaryDepartment.DepartmentIdExt,
					defaultDomain = _authenSetting.DefaultDomain
				},
				commentComments = commentComments,
				moneyFormat = _generalSettings.MoneyFormat,
				isNotAllowFinishDocument = _generalSettings.IsNotAllowFinishDocument,
				isNotAllowRenewal = _generalSettings.IsNotAllowRenewal,
				fileUploadSettings = new
				{
					maxFileSize = _fileUploadSettings.FileUploadMaximumSizeBytes,
					acceptFileTypes = string.Join("|", _fileUploadSettings.FileUploadAllowedExtensions)
				},
				parentDomain = _ssoSettings.BkavSSOParentDomain,
				requirePublishPlan = requirePublishPlan,

				typePositionTitleJob = typePositionTitleJob,

				doctypes = doctypes,
				survey = survey,
                allDoctypes = allDoctypes,

                documentTemplates = documentTemplates,

				processFunction = processFunction,
                allTreeGroups = allTreeGroups,

                currentDepts = currentDepts,
				allUsers = allUsers,
				allDeps = allDeps,
				allJobtitles = allJobtitles,
				allPositions = allPositions,
				allUserDeps = allUserDeps,

				storeShare = storeShare,
				storePrivate = storePrivate,

				codeNotations = codeNotations,

				transfer = new
				{
                    isfasttransfer = _generalSettings.IsFastTransfer,
                    isfiletag = _generalSettings.IsFileTag,
                    iscreatedform =  _generalSettings.IsCreatedForm,
					requireXlc = _generalSettings.RequireChooseXlc,
					requireCommentWhenFinish = _generalSettings.RequireCommentWhenFinish,
					showApproverByDepartment = _generalSettings.ShowApproverByDepartment,
					requirePlan = requirePublishPlan
				},

				publish = new
				{
					showPlaceInOffice = _generalSettings.ShowPlaceInOffice,
					detectPdfChangeContent = _generalSettings.DetectPdfChangeContent,
					allowThuHoiLt = _generalSettings.AllowThuHoiVbLienThong
				},

//#if HoSoMotCuaEdition
//				onlineRegistration = new
//				{
//					Active = onlineRegistrationSettings.HasPermisson(user.UserId),
//					Name = onlineRegistrationSettings.Name,
//					TreeGroupId = onlineRegistrationSettings.TreeGroupId,
//					onlineApiUrl = onlineRegistrationSettings.ApiUrl,
//				},
//				faq = new
//				{
//					Active = faqSettings.HasPermisson(user.UserId),
//					Name = faqSettings.Name,
//					TreeGroupId = faqSettings.TreeGroupId,
//					onlineApiUrl = faqSettings.ApiUrl
//				}
//#endif
			}, JsonRequestBehavior.AllowGet);
		}

		public void CheckConnection()
		{
			return;
		}

		#region Document Tree

		/// <summary>
		///
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public JsonResult GetFunctionByParentId(int id = 0)
		{
			var result = _processFunctionService.GetsFromCache(id);
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public JsonResult GetFunctionHasTransferTheoLoByParentId(int id = 0)
		{
			if (id == 0)
			{
				return Json(_processFunctionService.GetsTransferTheoLoByParentId(null, User.GetUserId()), JsonRequestBehavior.AllowGet);
			}
			return Json(_processFunctionService.GetsTransferTheoLoByParentId(id, User.GetUserId()), JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult GetTotalDocumentUnreadMultiFunction(string ids)
		{
			if (string.IsNullOrEmpty(ids))
			{
				return null;
			}
			var functionIds = Json2.ParseAs<IEnumerable<int>>(ids);
			return Json(_processFunctionService.GetTotalDocumentUnreadMultiFunction(functionIds, User.GetUserId()));
		}

		#endregion

		#region Document List

		[HttpPost]
		public JsonResult GetDocuments(int id, GetDocumentParameters parameters,
			IEnumerable<int> reloadFunctionIds = null, IEnumerable<int> currentDocCopyIds = null, string paramsQuery = null)
		{
			var function = _processFunctionService.GetFromCache(id);
			if (function == null)
			{
				return null;
			}

			IEnumerable<IDictionary<string, object>> documents;

			//lấy kiểu phân trang
			int defaultPageSizeHome = _generalSettings.DefaultPageSizeHome;
			var userSettingModel = _helperUserSetting.GetUserCurrentSetting();
			if (userSettingModel != null)
			{
				defaultPageSizeHome = userSettingModel.DefaultPageSizeHome.HasValue
										? userSettingModel.DefaultPageSizeHome.Value
										: defaultPageSizeHome;
			}

			if (!string.IsNullOrWhiteSpace(paramsQuery) && paramsQuery != "[]")
			{
				var objectParams = Json2.ParseAs<List<ObjectParams>>(paramsQuery);
				if (objectParams != null && objectParams.Any())
				{
					foreach (var item in objectParams)
					{
						parameters.Params.Add(item.Key, item.Value);
					}
				}
			}

			// Mặc định để phân trang dạng scroll.
			IEnumerable<int> removeDocumentCopyIds = null;
			var totalDocuments = 0;
			var pageSize = defaultPageSizeHome;
			documents = parameters.LastDate.HasValue
				? _processFunctionService.GetDocumentLatestByFunction(out removeDocumentCopyIds, function, parameters.LastDate.Value, currentDocCopyIds, parameters.Params)
				: _processFunctionService.GetDocumentOlderByFunction(function, DateTime.Now, pageSize + 1, User.GetUserId(), parameters.Params);

			var listSetting = _docColumnSettingSerice.GetAllCaches().FirstOrDefault(p => p.DocColumnSettingId == function.DocColumnSettingId);

			var isFilterByDocFieldDocType = false;
			var functionType = function.ProcessFunctionType;
			if (functionType != null)
			{
				var paramDocField = functionType.ListParam.FirstOrDefault(p => p.ValueField == "DocFieldId");
				if (paramDocField != null)
				{
					isFilterByDocFieldDocType = true;

					if (!parameters.Params.ContainsKey(paramDocField.ParamName))
					{
						throw new Exception("Chuỗi query string không có tham số " + paramDocField.ParamName);
					}
				}
			}

			var columnSetting = listSetting != null
							 ? Json2.ParseAs<List<Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting>>(listSetting.DisplayColumn)
							 : new List<Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting>();

			var docTypeColumnSettings = new Dictionary<string, List<Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting>>();

			if (documents.Any())
			{
				var listColumnNames = documents.First().Keys;
				if (!listColumnNames.Any(n => n.Equals(COLUMN_KEY)))
				{
					throw new Exception("Câu truy vấn không lấy ra cột " + COLUMN_KEY);
				}
				if (!listColumnNames.Any(n => n.Equals(COLUMN_COMPENDIUM)))
				{
					throw new Exception("Câu truy vấn không lấy ra cột trích yếu");
				}
				if (!listColumnNames.Any(n => n.Equals(COLUMN_IS_VIEWED)))
				{
					throw new Exception("Câu truy vấn không lấy ra cột đã đọc");
				}
				if (isFilterByDocFieldDocType)
				{
					if (!listColumnNames.Any(n => n.Equals(COLUMN_DOCTYPE_ID)))
					{
						throw new Exception("Câu truy vấn không lấy ra cột id của loại vb, hs");
					}
				}

				if (columnSetting.Count == 0)
				{
					columnSetting.AddRange(listColumnNames.Select(columnName => new Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting
					{
						ColumnName = columnName,
						DisplayName = columnName
					}));
				}
			}

			var totalUnread = 0;// _processFunctionService.GetTotalDocumentUnread(function, User.GetUserId(), parameters.Params);

			if (reloadFunctionIds != null && reloadFunctionIds.Any())
			{
				reloadFunctionIds = reloadFunctionIds.Where(i => i != function.ProcessFunctionId).Distinct();
				var reloadFunctions = _processFunctionService.GetTotalDocumentUnreadMultiFunction(reloadFunctionIds, User.GetUserId());
				return Json(new
				{
					listDocuments = new
					{
						totalUnread = totalUnread,
						page = 1,
						pageSize = pageSize,
						totalDocuments = totalDocuments,
						enablePaging = function.IsEnablePaging,
						isFilterByDocFieldDocType = isFilterByDocFieldDocType,
						dateFilter = string.IsNullOrEmpty(function.DateFilter) ? "" : function.DateFilter,
						dateFilterView = string.IsNullOrEmpty(function.DateFilterView) ? "" : function.DateFilterView,
						isOverdueFilter = function.IsOverdueFilter,
						isDateFilter = function.IsDateFilter,
						columnSetting = columnSetting,
						documents = documents.StringifyJs(),
						docTypeColumnSettings = docTypeColumnSettings,
						removeDocumentCopyIds = removeDocumentCopyIds
					},
					reloadFunctions
				});
			}

			return Json(new
			{
				totalUnread = totalUnread,
				page = 1,
				pageSize = defaultPageSizeHome,
				totalDocuments = totalDocuments,
				enablePaging = function.IsEnablePaging,
				isFilterByDocFieldDocType,
				dateFilter = string.IsNullOrEmpty(function.DateFilter) ? "" : function.DateFilter,
				dateFilterView = string.IsNullOrEmpty(function.DateFilterView) ? "" : function.DateFilterView,
				isOverdueFilter = function.IsOverdueFilter,
				isDateFilter = function.IsDateFilter,
				columnSetting = columnSetting,
				documents = documents,
				docTypeColumnSettings,
				removeDocumentCopyIds = removeDocumentCopyIds
			});
		}

		/// <summary>
		/// Trả về json danh sách các văn bản mới được thêm vào node và văn bản mới được xóa khỏi node
		/// </summary>
		/// <remarks>
		/// Sử dụng khi phân trang (cuộn) ở trang 1.
		/// </remarks>
		/// <param name="id">Id của node trên cây văn bản</param>
		/// <param name="parameters">Các tiêu chí phân trang</param>
		/// <param name="currentDocCopyIds">Danh sách các văn bản đã tải</param>
		/// <param name="paramsQuery"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult GetLastestDocuments(int id,
			GetDocumentParameters parameters,
			IEnumerable<int> currentDocCopyIds = null,
			string paramsQuery = null)
		{
			var function = _processFunctionService.GetFromCache(id);
			if (function == null)
			{
				return null;
			}

			IEnumerable<int> removeDocumentCopyIds = null;
			IEnumerable<IDictionary<string, object>> documents = null;
			var pageSize = parameters.PageSize.HasValue
					? parameters.PageSize.Value
					: _generalSettings.DefaultPageSizeHome;

			if (!string.IsNullOrWhiteSpace(paramsQuery) && paramsQuery != "[]")
			{
				var objectParams = Json2.ParseAs<List<ObjectParams>>(paramsQuery);
				if (objectParams != null && objectParams.Any())
				{
					foreach (var item in objectParams)
					{
						parameters.Params.Add(item.Key, item.Value);
					}
				}
			}

			if (parameters.LastDate.HasValue)
			{
				documents = _processFunctionService.GetDocumentLatestByFunction(out removeDocumentCopyIds,
																				function,
																				parameters.LastDate.Value, currentDocCopyIds,
																				parameters.Params);
			}
			else
			{
				documents = _processFunctionService.GetAllDocumentByFunction(function, parameters.Params, parameters.QuickSearch);
			}
			return Json(new
			{
				documents = documents,
				removeds = removeDocumentCopyIds
			});
		}

		public JsonResult QuickViewDocument(string id)
		{
            if (id == "0")
            {
                return Json(new
                {
                    id = "",
                    type = 1,
                    docCode = "Văn bản mới",
                    docType =  "",
                    dateReceived = DateTime.Now,
                    dateAppoint = DateTime.Now,
                    personInfo = DateTime.Now,
                    email = DateTime.Now,
                    phone = "",
                    address = "",
                }, JsonRequestBehavior.AllowGet);
            }
			int documentCopyId;
			if (Int32.TryParse(id, out documentCopyId))
			{
				var document = _docCopyService.GetFromCache(documentCopyId, CurrentUserId());
				var urgent = _resourceService.GetEnumDescription<Urgent>((Urgent)document.UrgentId);

				return Json(new
				{
					id = documentCopyId,
					type = 1,
					compendium = document.Compendium,
					lastComment =
						new
						{
							date = !document.LastDateComment.HasValue
									? string.Empty
									: document.LastDateComment.Value.ToString("dd/MM/yyyy hh:mm tt"),
							content = document.LastComment,
							user = document.LastUserComment
						},
					docType = document.DocTypeName,
					department = document.InOutPlace,
					dateCreate = document.DateCreated.ToString("dd/MM/yyyy"),
					lastUser = document.LastUserComment,
					docField = "",
					urgent = urgent,
					docCode = document.DocCode,
					totalPage = document.TotalPage,
					processinfo = document.ProcessInfo,
					approver = document.SuccessNote,
                    document.CategoryBusinessId
                }, JsonRequestBehavior.AllowGet);
			}

			var documentOnline = _docOnlineService.Get(new Guid(id));
			if (documentOnline == null)
			{
				return null;
			}

			return Json(new
			{
				id = new Guid(id),
				type = 2,
				docCode = documentOnline.DocCode,
				docType = documentOnline.Doctype == null ? "" : documentOnline.Doctype.DocTypeName,
				dateReceived = documentOnline.DateReceived,
				dateAppoint = documentOnline.DateAppoint,
				personInfo = documentOnline.PersonInfo,
				email = documentOnline.Email,
				phone = documentOnline.Phone,
				address = documentOnline.Address,
			}, JsonRequestBehavior.AllowGet);
		}

		#endregion

		public ActionResult Setting()
		{
			return View();
		}

		public ActionResult Linkeds()
		{
			return View();
		}

		public void ExportDocumentListToXML(int id, string docCopyIds)
		{
			List<int> docIds = null;
			try
			{
				docIds = Json2.ParseAs<List<int>>(docCopyIds);
			}
			catch
			{
				throw new Exception(_resourceService.GetResource("Function.ExportFile.Error"));
			}

			var documents = _docCopyService.Gets(docIds);
			if (documents != null && documents.Count() > 0)
			{
				ExportToXML(documents);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public JsonResult GetVersionValue()
		{
			var version = _versionTreeSettings.CacheVersion == null ? "DefaulValue" : _versionTreeSettings.CacheVersion;
			return Json(version, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public JsonResult SetVersionValue()
		{
			var version = DateTime.Now.ToString();
			_versionTreeSettings.CacheVersion = version;
			_settingService.SaveSetting(_versionTreeSettings);
			return Json(version, JsonRequestBehavior.AllowGet);
		}

		#region Private

		private Dictionary<string, string> GetDefaultDocumentTempate()
		{
			var vbdenTemplate = Business.Utils.DocumentHelper.GetDocumentTemplate(CategoryBusinessTypes.VbDen);
			var vbdiTemplate = Business.Utils.DocumentHelper.GetDocumentTemplate(CategoryBusinessTypes.VbDi);
			var hsmcTemplate = Business.Utils.DocumentHelper.GetDocumentTemplate(CategoryBusinessTypes.Hsmc);
			var result = new Dictionary<string, string>();
			result.Add("vbden", vbdenTemplate);
			result.Add("vbdi", vbdiTemplate);
			result.Add("hsmc", hsmcTemplate);

			return result;
		}

		private void ExportToXML(IEnumerable<DocumentCopy> documents)
		{
			var result = new XmlDocument();
			result.LoadXml("<egov></egov>");

			DocumentsToXml(documents, result);

			var s = result.OuterXml;

			var filename = "eGovExport_" + DateTime.Now.ToString("yyMddHHmmss");
			var attachment = "attachment; filename=" + filename + ".xml";
			Response.ClearContent();
			Response.ContentType = "application/xml";
			Response.AddHeader("content-disposition", attachment);
			Response.Write(s);
			Response.End();
		}

		private void DocumentsToXml(IEnumerable<DocumentCopy> documents, XmlDocument doc)
		{
			var root = doc.GetElementsByTagName("egov")[0];
			foreach (var dc in documents)
			{
				var docElement = doc.CreateElement("document");
				var document = dc.Document;
				if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen)
				{
					docElement.AppendChild(CreateNodeFromDocument("Organization", doc, document.Organization == null ? "" : document.Organization));
				}
				else if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDi)
				{
					docElement.AppendChild(CreateNodeFromDocument("Organization", doc, document.Organization == null ? "" : document.InOutPlace));
				}
				docElement.AppendChild(CreateNodeFromDocument("DocCode", doc, document.DocCode));
				docElement.AppendChild(CreateNodeFromDocument("InOutCode", doc, document.InOutCode));
				docElement.AppendChild(CreateNodeFromDocument("Compendium", doc, document.Compendium));
				docElement.AppendChild(CreateNodeFromDocument("DocTotal", doc, 1));
				docElement.AppendChild(CreateNodeFromDocument("DocPage", doc, document.TotalPage ?? 1));
				docElement.AppendChild(CreateNodeFromDocument("Category", doc, document.CategoryName));
				docElement.AppendChild(CreateNodeFromDocument("DatePublished", doc, document.DatePublished.HasValue ? document.DatePublished.Value.ToString("dd/MM/yyyy") : ""));
				docElement.AppendChild(CreateNodeFromDocument("DateCreated", doc, document.DateCreated.ToString("dd/MM/yyyy")));
				docElement.AppendChild(CreateNodeFromDocument("Security", doc, document.SecurityId == null || document.SecurityId == 1 ? "Thường" : document.SecurityId == 2 ? "Mật" : "Tối mật"));
				docElement.AppendChild(CreateNodeFromDocument("Urgent", doc, _resourceService.GetEnumDescription<Urgent>((Urgent)document.UrgentId)));
				docElement.AppendChild(CreateNodeFromDocument("InOutPlace", doc, document.InOutPlace));
				docElement.AppendChild(CreateNodeFromDocument("Note", doc, ""));
				docElement.AppendChild(CreateNodeFromDocument("Approver", doc, document.SuccessNote));

				if (document.Attachments != null)
				{
					var attachmentsNode = doc.CreateElement("Attachments");
					var attachmentIds = document.Attachments.Select(a => a.AttachmentId).ToList();
					var attachmentFiles = _attachmentService.DownloadAttachmentName(attachmentIds, User.GetUserId());
					if (attachmentFiles != null)
					{
						foreach (var file in attachmentFiles)
						{
							var attachmentNode = doc.CreateElement("Attachment");
							attachmentNode.AppendChild(CreateNodeFromDocument("Name", doc, file.Key));
							attachmentNode.AppendChild(CreateNodeFromDocument("Value", doc, file.Value));

							attachmentsNode.AppendChild(attachmentNode);
						}
						docElement.AppendChild(attachmentsNode);
					}

				}

				root.AppendChild(docElement);
			}
		}

		private XmlElement CreateNodeFromDocument(string name, XmlDocument doc, object value)
		{
			var result = doc.CreateElement(name);
			result.InnerText = value == null ? "" : value.ToString();
			return result;
		}

		private string GetDefaulApps(ConnectionSettings connectionSettings)
		{
			if (string.IsNullOrWhiteSpace(connectionSettings.Apps))
			{
				var settingService = DependencyResolver.Current.GetService<SettingBll>();
				var newApps = new List<Apps>();

				newApps.Add(new Apps
				{
					Id = 1,
					Name = "bmail",
					Title = "Điều hành",
					Order = 1,
					IsDefaultApp = false,
					IsBackgroundApp = false,
					AppUrl = connectionSettings.BmailLink,
					IsActived = !string.IsNullOrWhiteSpace(connectionSettings.BmailLink)
				});

				newApps.Add(new Apps
				{
					Id = 2,
					Name = "documents",
					Title = "Văn bản",
					AppUrl = "/Home/Index",
					Order = 2,
					IsDefaultApp = true,
					IsBackgroundApp = false,
					IsActived = true
				});

				newApps.Add(new Apps
				{
					Id = 3,
					Name = "chat",
					Title = "Trao đổi",
					AppUrl = connectionSettings.ChatLink,
					Order = 3,
					IsDefaultApp = false,
					IsBackgroundApp = false,
					IsActived = !string.IsNullOrWhiteSpace(connectionSettings.ChatLink)
				});
				newApps.Add(new Apps
				{
					Id = 4,
					Name = "calendar",
					Title = "Lịch làm việc",
					AppUrl = connectionSettings.BmailLink,
					Order = 4,
					IsDefaultApp = false,
					IsBackgroundApp = false,
					IsActived = !string.IsNullOrWhiteSpace(connectionSettings.BmailLink)
				});
				newApps.Add(new Apps
				{
					Id = 5,
					Name = "report",
					Title = "Báo cáo",
					AppUrl = "/ReportViewer/Index",
					Order = 5,
					IsDefaultApp = false,
					IsBackgroundApp = false,
					IsActived = true
				});
				newApps.Add(new Apps
				{
					Id = 6,
					Name = "statistics",
					Title = "Giám sát",
					AppUrl = "/statistics/Index",
					Order = 6,
					IsDefaultApp = false,
					IsBackgroundApp = false,
					IsActived = true
				});
				connectionSettings.Apps = newApps.Stringify();
				settingService.SaveSetting(connectionSettings);
			}
			return connectionSettings.Apps;
		}

		private void ExportToFile(ProcessFunction function, System.Data.DataTable documents, string exportKey = "EXCELL")
		{
			string fileName = string.Empty;
			var functionFile = Json2.ParseAs<Entities.Customer.FunctionFile>(function.ExportFileConfig);
			var strPath = LoadCrystalFile(functionFile);
			var rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			rd.Load(strPath);
			rd.SetDataSource(documents);

			if (exportKey == "EXCELL")
			{
				rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel,
					System.Web.HttpContext.Current.Response, true, function.Name.StripVietnameseChars() + ".xls");
			}
			else if (exportKey == "WORD")
			{
				rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows,
				   System.Web.HttpContext.Current.Response, true, function.Name.StripVietnameseChars() + ".doc");
			}
			rd.Close();
		}

		private string LoadCrystalFile(Entities.Customer.FunctionFile functionFile)
		{
			var stream = _processFunctionService.Download(functionFile);
			var tempPath = ResourceLocation.Default.FileUploadTemp;
			var temp = FileManager.Default.Create(stream, tempPath, null, ".rpt");
			return temp.FullName;
		}

		private int CurrentUserId()
		{
			return _userService.CurrentUser.UserId;
		}

		#endregion

		#region bỏ

		//[HttpPost]
		//public JsonResult GetDocumentPaging(int id, GetDocumentParameters parameters, string paramsQuery = null)
		//{
		//    var function = _processFunctionService.GetFromCache(id);
		//    if (function != null)
		//    {
		//        var totalDocuments = 0;
		//        var pageSize = 0;
		//        var page = 0;
		//        IEnumerable<IDictionary<string, object>> documents;

		//        //lấy kiểu phân trang
		//        bool isLoadPageScroll = _generalSettings.IsLoadPageScroll;
		//        int defaultPageSizeHome = _generalSettings.DefaultPageSizeHome;

		//        var userSettingModel = _helperUserSetting.GetUserCurrentSetting();
		//        if (userSettingModel != null)
		//        {
		//            isLoadPageScroll = userSettingModel.IsLoadPageScroll;
		//            defaultPageSizeHome = userSettingModel.DefaultPageSizeHome.HasValue
		//                                    ? userSettingModel.DefaultPageSizeHome.Value
		//                                    : defaultPageSizeHome;
		//        }

		//        if (!string.IsNullOrWhiteSpace(paramsQuery) && paramsQuery != "[]")
		//        {
		//            var objectParams = Json2.ParseAs<List<ObjectParams>>(paramsQuery);
		//            if (objectParams != null && objectParams.Any())
		//            {
		//                foreach (var item in objectParams)
		//                {
		//                    parameters.Params.Add(item.Key, item.Value);
		//                }
		//            }
		//        }

		//        if (isLoadPageScroll)
		//        {
		//            documents = _processFunctionService.GetDocumentOlderByFunction(function,
		//                                                                           parameters.LastDate.HasValue
		//                                                                           ? parameters.LastDate.Value
		//                                                                           : DateTime.Now,
		//                                                                           defaultPageSizeHome + 1,
		//                                                                           parameters.Params, parameters.QuickSearch);
		//        }
		//        else
		//        {
		//            pageSize = parameters.PageSize.HasValue
		//                       ? parameters.PageSize.Value
		//                       : defaultPageSizeHome;
		//            page = parameters.Page.HasValue
		//                   ? parameters.Page.Value
		//                   : 1;
		//            documents = _processFunctionService.GetDocumentPagingByFunction(out totalDocuments
		//                                                                            , function
		//                                                                            , page
		//                                                                            , pageSize + 1
		//                                                                            , parameters.Params
		//                                                                            , parameters.QuickSearch);
		//        }
		//        return Json(new { documents = documents, totalDocuments, pageSize, page });
		//    }
		//    return null;
		//}

		///// <summary>
		///// HopCV:100914
		///// Hàm lấy văn bản theo node của cầy văn bản trên tablet hoặc mobile
		///// Khi người dung scroll thì sẽ lấy ra văn bản theo node hiện tại và các diều kiện truyền vào để lấy ra các văn bản
		///// </summary>
		///// <param name="id">id cuar node cây văn bản để lấy văn bản</param>
		///// <param name="parameters">Đối tượng tham số truyền vào để lọc lấy văn bản</param>
		///// <param name="paramsQuery"></param>
		///// <returns></returns>
		//[HttpPost]
		//public JsonResult GetDocumentPagingForTabletAndMobile(int id, GetDocumentParameters parameters, string paramsQuery = null)
		//{
		//    var function = _processFunctionService.GetFromCache(id);
		//    if (function == null)
		//    {
		//        return null;
		//    }

		//    IEnumerable<IDictionary<string, object>> documents;
		//    int defaultPageSizeHome = _generalSettings.DefaultPageSizeHome;

		//    var userSettingModel = _helperUserSetting.GetUserCurrentSetting();
		//    if (userSettingModel != null)
		//    {
		//        defaultPageSizeHome = userSettingModel.DefaultPageSizeHome.HasValue
		//                                ? userSettingModel.DefaultPageSizeHome.Value
		//                                : defaultPageSizeHome;
		//    }

		//    if (!string.IsNullOrWhiteSpace(paramsQuery) && paramsQuery != "[]")
		//    {
		//        var objectParams = Json2.ParseAs<List<ObjectParams>>(paramsQuery);
		//        if (objectParams != null && objectParams.Any())
		//        {
		//            foreach (var item in objectParams)
		//            {
		//                parameters.Params.Add(item.Key, item.Value);
		//            }
		//        }
		//    }

		//    documents = _processFunctionService.GetDocumentOlderByFunction(function,
		//                                                                   parameters.LastDate.HasValue
		//                                                                   ? parameters.LastDate.Value
		//                                                                   : DateTime.Now,
		//                                                                   defaultPageSizeHome + 1,
		//                                                                   parameters.Params, parameters.QuickSearch);

		//    return Json(new { documents = documents });
		//}

		//public void ExportToFile(int id, string docCopyIds, string paramsQuery = null, string exportKey = "EXCELL")
		//{
		//    var function = _processFunctionService.GetFromCache(id);
		//    if (function == null)
		//    {
		//        throw new Exception(_resourceService.GetResource("Function.NotFound"));
		//    }

		//    if (!function.HasExportFile)
		//    {
		//        throw new Exception(_resourceService.GetResource("Function.NotSuportExportFile"));
		//    }

		//    var parameters = new Dictionary<string, string>();
		//    if (!string.IsNullOrWhiteSpace(paramsQuery) && paramsQuery != "[]")
		//    {
		//        var objectParams = Json2.ParseAs<List<ObjectParams>>(paramsQuery);
		//        if (objectParams != null && objectParams.Any())
		//        {
		//            foreach (var item in objectParams)
		//            {
		//                parameters.Add(item.Key, item.Value);
		//            }
		//        }
		//    }
		//    List<int> docIds = null;
		//    try
		//    {
		//        docIds = Json2.ParseAs<List<int>>(docCopyIds);
		//    }
		//    catch
		//    {
		//        throw new Exception(_resourceService.GetResource("Function.ExportFile.Error"));
		//    }
		//    var documents = _processFunctionService.GetDataExportToFile(function, docIds, parameters, null);
		//    if (documents == null || documents.Rows.Count <= 0)
		//    {
		//        throw new Exception(_resourceService.GetResource("Function.ExportFile.Documents.IsNull"));
		//    }

		//    try
		//    {
		//        ExportToFile(function, documents, exportKey);
		//    }
		//    catch (Exception ex)
		//    {
		//        LogException(ex);
		//        throw new Exception(_resourceService.GetResource("Function.ExportFile.Error"));
		//    }
		//}

		//public void ExportDocumentListToExcel(int id, string docCopyIds)
		//{
		//    var function = _processFunctionService.GetFromCache(id);
		//    if (function == null)
		//    {
		//        return;
		//    }

		//    var documentCopyIds = Json2.ParseAs<List<int>>(docCopyIds);
		//    var userId = User.GetUserId();
		//    var dataReport = _processFunctionService.GetDataExport(function, documentCopyIds, userId);
		//    ExportToFile(function, dataReport);
		//}

		//public void ExportDocumentListToWord(int id, string docCopyIds)
		//{
		//    var function = _processFunctionService.GetFromCache(id);
		//    if (function == null)
		//    {
		//        return;
		//    }

		//    var documentCopyIds = Json2.ParseAs<List<int>>(docCopyIds);
		//    var userId = User.GetUserId();
		//    var dataReport = _processFunctionService.GetDataExport(function, documentCopyIds, userId);
		//    ExportToFile(function, dataReport, "WORD");
		//}

		//public ActionResult CreateNewApp()
		//{
		//    return View(new Apps());
		//}

		//[HttpPost]
		//public JsonResult CreateNewApp(Apps model)
		//{
		//    bool success = false;
		//    try
		//    {
		//        var currentSetting = _helperUserSetting.GetUserCurrentSetting();

		//        var id = currentSetting.AppCreates.Count + 1;
		//        model.Id = id;
		//        model.Name = "customApp_" + id;
		//        currentSetting.AppCreates.Add(model);
		//        var json = currentSetting.StringifyJs();
		//        _userService.UpdateUserSetting(json);
		//        success = true;
		//        return Json(new { success, name = model.Name, id = model.Id, message = "Success" });
		//    }
		//    catch (Exception)
		//    {
		//    }

		//    return Json(new { success, message = "Error" });
		//}

		//[HttpPost]
		//public JsonResult DeleteApp(int id)
		//{
		//    bool success = false;
		//    try
		//    {
		//        var currentSetting = _helperUserSetting.GetUserCurrentSetting();
		//        var apps = currentSetting.AppCreates;
		//        var deleteApp = apps.FirstOrDefault(x => x.Id == id);
		//        if (deleteApp != null)
		//        {
		//            apps.Remove(deleteApp);
		//        }
		//        for (int i = 0; i < apps.Count; i++)
		//        {
		//            apps[i].Id = i + 1;
		//        }

		//        currentSetting.AppCreates = apps;
		//        var json = currentSetting.StringifyJs();
		//        _userService.UpdateUserSetting(json);
		//        success = true;
		//        return Json(new { success, message = "Success" });
		//    }
		//    catch (Exception)
		//    {
		//    }

		//    return Json(new { success, message = "Error" });
		//}

		//public JsonResult AppIconUpload()
		//{
		//    var file = Request.Files["file"];
		//    bool success = false;
		//    if (file != null && file.ContentLength > 0)
		//    {
		//        if (file.InputStream.Length > _fileUploadSettings.ProfilePictureMaximumSizeBytes)
		//        {
		//            return Json(new { success, message = _resourceService.GetResource("Account.Avartar.Fields.FileUploadMaximumSizeBytes") + " (" + _fileUploadSettings.ProfilePictureMaximumSizeBytes + " KB)" });
		//        }
		//        var ext = Path.GetExtension(file.FileName);
		//        if (!_fileUploadSettings.ProfilePictureAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
		//        {
		//            return Json(new { success, message = _resourceService.GetResource("Account.Avartar.Fields.FileUploadAllowedExtensions") });
		//        }

		//        Helper.ResizeAndCropImage.CropAndCropResizeImage(file.InputStream, 25, 25,
		//                                                        Server.MapPath("~/ImagesUpload/") + file.FileName + ".jpg");
		//        success = true;
		//        return Json(new { success, Avatar = "/ImagesUpload/" + file.FileName + ".jpg" });
		//    }

		//    return Json(new { success, message = "Error" });
		//}

		public JsonResult HasHideSaveStore(bool hasHideStore = false)
		{
			var user = _userService.CurrentUser;
			var userSettingModel = _helperUserSetting.GetUserSetting(user.UserSetting);
			if (userSettingModel != null)
			{
				userSettingModel.HasHideLuuSo = hasHideStore;

				var json = userSettingModel.StringifyJs();
				_userService.UpdateUserSetting(json);

				return Json(new { status = true, data = hasHideStore }, JsonRequestBehavior.AllowGet);
			}
			return Json(new { status = false, data = false }, JsonRequestBehavior.AllowGet);
		}

		///// <summary>
		/////
		///// </summary>
		///// <param name="id"></param>
		///// <param name="parameters"></param>
		///// <param name="paramsQuery"></param>
		///// <returns></returns>
		//[HttpPost]
		//public int GetTotalDocumentUnread(int id, GetDocumentParameters parameters, string paramsQuery = null)
		//{
		//    var function = _processFunctionService.GetFromCache(id);

		//    if (!string.IsNullOrWhiteSpace(paramsQuery))
		//    {
		//        var objectParams = Json2.ParseAs<List<ObjectParams>>(paramsQuery);
		//        if (objectParams != null && objectParams.Any())
		//        {
		//            foreach (var item in objectParams)
		//            {
		//                parameters.Params.Add(item.Key, item.Value);
		//            }
		//        }
		//    }
		//    return function == null
		//               ? 0
		//               : _processFunctionService.GetTotalDocumentUnread(function, User.GetUserId(), parameters.Params);
		//}

		//public JsonResult GetDocumentTree()
		//{
		//    return Json(_processFunctionService.GetDocumentTree(), JsonRequestBehavior.AllowGet);
		//}

		//public JsonResult SyncDocumentTree(DateTime lastUpdate)
		//{
		//    return Json(_processFunctionService.SyncDocumentTree(lastUpdate), JsonRequestBehavior.AllowGet);
		//}

		///// <summary>
		///// Trả về danh sách document theo kho.
		///// </summary>
		///// <param name="id">Id của kho</param>
		///// <returns></returns>
		//public JsonResult GetDocumentStore(int id)
		//{
		//    return Json(_processFunctionService.GetDocumentStore(id), JsonRequestBehavior.AllowGet);
		//}

		//public JsonResult SyncDocumentStore(int id, DateTime lastUpdate)
		//{
		//    return Json(_processFunctionService.SyncDocumentStore(id, lastUpdate), JsonRequestBehavior.AllowGet);
		//}

		//public JsonResult GetFunctionGroups()
		//{
		//    return Json(_processFunctionGroupService.Gets().Select(g => new
		//    {
		//        functionGroupId = g.ProcessFunctionGroupId,
		//        clientExpression = g.ClientExpression
		//    }), JsonRequestBehavior.AllowGet);
		//}

		#endregion
	}
}