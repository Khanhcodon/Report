using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using ClosedXML.Excel;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Utils;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
	[EgovAuthorize]
	//[RequireHttps]
	public class DocTypePlusController : CustomController
	{
		private const string DEFAULT_SORT_BY = "DocTypeName";
		private readonly CategoryBll _categoryService;
		private readonly CodeBll _codeService;
		private readonly DepartmentBll _departmentService;
		private readonly DocFieldBll _docFieldService;
		private readonly DocTypeBll _docTypeService;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly PositionBll _positionService;
		private readonly ResourceBll _resourceService;
		private readonly StoreBll _storeService;
		private readonly UserBll _userService;
		private readonly WorkflowBll _workflowService;
		private readonly FormBll _formService;
		private readonly FormGroupBll _formgroupService;
		private readonly PaperBll _paperService;
		private readonly OfficeBll _officeService;//egovOnline
		private readonly DocTypeFormBll _doctypeformService;
		private readonly Bkav.eGovOnline.Business.Customer.LawBll _lawService;//eGovOnline
        private readonly DocTypeFormBll _doctypeFormService;

        private readonly DoctypePaperBll _doctypePaperService;//eGovOnline

		private readonly FeeBll _feeService;
		private readonly DoctypeFeeBll _doctypeFeeService;
		private readonly DoctypeTemplateBll _doctypeTemplateService;
		private readonly OnlineTemplateBll _onlineTemplateService;
		private const string TEMPLALTE_PATH = "~/Content/TemplateUICategoryBusiness";

		private readonly IncreaseBll _increaseService;
		private readonly ActionLevelBll _actionLevelService;

		public DocTypePlusController(FeeBll feeService,
								 DoctypeFeeBll doctypeFeeService,
								 DoctypePaperBll doctypePaperService,
								 DocTypeBll docTypeService,
								 CodeBll codeService, UserBll userService,
								 DepartmentBll departmentService,
								 CategoryBll categoryService,
								 StoreBll storeService,
								 PositionBll positionService,
								 DocFieldBll docFieldService,
								 ResourceBll resourceService,
								 WorkflowBll workflowService,
								 AdminGeneralSettings generalSettings,
								 FormBll formService,
								 FormGroupBll formgroupService,
								 DocTypeFormBll doctypeformService,
								 LawBll lawService,
								 OfficeBll officeService,
								 PaperBll paperService,
								 DoctypeTemplateBll doctypeTemplateService,
								OnlineTemplateBll onlineTemplateService,
								IncreaseBll increaseService,
                                DocTypeFormBll doctypeFormService,
								ActionLevelBll actionLevelService
			)
			: base()
		{
			_doctypeFeeService = doctypeFeeService;
			_feeService = feeService;
			_doctypePaperService = doctypePaperService;
			_paperService = paperService;
			_officeService = officeService;
			_lawService = lawService;
			_docTypeService = docTypeService;
			_categoryService = categoryService;
			_docFieldService = docFieldService;
			_generalSettings = generalSettings;
			_storeService = storeService;
			_resourceService = resourceService;
			_workflowService = workflowService;
			_codeService = codeService;
			_positionService = positionService;
			_userService = userService;
			_departmentService = departmentService;
			_formService = formService;
			_formgroupService = formgroupService;
			_doctypeformService = doctypeformService;
			_doctypeTemplateService = doctypeTemplateService;
			_onlineTemplateService = onlineTemplateService;
			_increaseService = increaseService;
            _doctypeFormService = doctypeFormService;
			_actionLevelService = actionLevelService;

		}

        #region "Module Admin"

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = new DocTypeSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            HttpCookie httpCookie = Request.Cookies[CookieName.SearchDocType];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<DocTypeSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { }
            }

            var model = GetDocTypeModels(search, sortAndPage.SortBy, sortAndPage.IsSortDescending,
                                         sortAndPage.CurrentPage, sortAndPage.PageSize);

            ViewBag.AllDocFields = _docFieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId })
                                        .OrderBy(d => d.DocFieldName)
                                        .StringifyJs();

            ViewBag.Search = search;
            ViewBag.CategoryBusinessId = BindCategoryBusiness(4);

            return View(model);
        }

        public ActionResult IndexAic(int? docfieldId, int? actionLevel = 1, int levelId = 1)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			//if (!HasPermission())
			//{
			//	CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermission"));
			//	ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermission"));
			//	return RedirectToAction("Index", "Welcome");
			//}

			var search = new DocTypeSearchModel();
			var sortAndPage = new SortAndPagingModel
			{
				PageSize = _generalSettings.DefaultPageSize,
				CurrentPage = 1,
				IsSortDescending = false,
				SortBy = DEFAULT_SORT_BY
			};

			HttpCookie httpCookie = Request.Cookies[CookieName.SearchDocType];
			if (httpCookie != null)
			{
				try
				{
					var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
					search = Json2.ParseAs<DocTypeSearchModel>(data["Search"].ToString());
					sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
				}
				catch { }
			}

			var docfields = _docFieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId, d.Order }).OrderBy(df => df.Order);
			docfieldId = docfieldId ?? docfields.First().DocFieldId;

			search.DocFieldId = docfieldId;
			search.ActionLevel = null;

			var doctypes = GetDocTypeModels(search, sortAndPage.SortBy, sortAndPage.IsSortDescending,
										 sortAndPage.CurrentPage, sortAndPage.PageSize);

			ViewBag.AllDocFields = docfields.Select(df => new SelectListItem()
										{
											Selected = docfieldId.HasValue ? df.DocFieldId == docfieldId.Value : false,
											Text = df.DocFieldName,
											Value = df.DocFieldId.ToString()
										});

			ViewBag.Total = _docTypeService.CountHSMC();

			var counts = doctypes.GroupBy(dt => dt.ActionLevel).Select(g => new { actionLevel = g.Key, count = g.Count() }).ToList();
			ViewBag.LevelCount = counts.Stringify() ;

			ViewBag.Search = search;
			ViewBag.ActionLevel = actionLevel;
			ViewBag.ListActionLevel = GetActionLevel();

			var model = doctypes.Where(dt => dt.ActionLevel == actionLevel && dt.LevelId == levelId);
			return View("IndexAic", model);
		}

		

        private object List<T>()
        {
            throw new NotImplementedException();
        }

        public ActionResult GetForm(Guid doctypeId, FormSearchModel search)
		{
			var docformIds = _doctypeformService.GetsAs(f => f.FormId, f => f.DocTypeId == doctypeId);
			var allform = _formService.Gets(f =>
				(!search.FormGroupId.HasValue || (search.FormGroupId.HasValue && f.FormGroupId == search.FormGroupId))
				&& (!search.FormTypeId.HasValue || (search.FormTypeId.HasValue && f.FormTypeId == search.FormTypeId))
				&& (string.IsNullOrEmpty(search.SearchKey) || (!string.IsNullOrEmpty(search.SearchKey) && f.FormName.Contains(search.SearchKey)))
				&& !docformIds.Contains(f.FormId)).ToListModel();
			var hasPrimary = _doctypeformService.Exist(f => f.DocTypeId == doctypeId && f.IsPrimary);
			ViewBag.AllForm = allform;
			ViewBag.Search = search;
			ViewBag.DocTypeId = doctypeId;
			ViewBag.HasPrimary = hasPrimary;
			return PartialView("_PartialForm");
		}

		

		public ActionResult Form(Guid id)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionForm"));
				ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionForm"));
				return RedirectToAction("Index");
			}

			var forms = _doctypeformService.GetsAs(f => new
			{
				f.Form.FormId,
				f.Form.FormName,
				f.Form.Description,
				f.IsPrimary,
				f.IsActive
			}, f => f.DocTypeId == id).Select(f => new Form
			{
				FormId = f.FormId,
				FormName = f.FormName,
				Description = f.Description,
				IsPrimary = f.IsPrimary,
				IsActivated = f.IsActive ? 1 : 0
			});
			DocType docType = _docTypeService.Get(id);
			ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
			ViewBag.AllForm = forms.ToListModel();
			ViewBag.DocTypeId = id;
			return View();
		}

		public ActionResult AddForm(Guid id)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionAddForm"));
				ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionAddForm"));
				return RedirectToAction("Index");
			}

			var allformgroup = _formgroupService.Gets();
			var allformtype = _formService.GetTypes();
			ViewBag.DoctypeId = id;
			ViewBag.AllFormGroup = allformgroup.ToListModel();
			ViewBag.FormTypes = allformtype;
			//var formgroupfirtid = allformgroup.First().FormGroupId;
			//var formtypefirtid = allformtype.First().FormTypeId;
			var docforms = _doctypeformService.GetsAs(f => f.FormId, f => f.DocTypeId == id);
			var hasPrimary = _doctypeformService.Exist(f => f.DocTypeId == id && f.IsPrimary);
			var allform =
				_formService.GetsAs(
					f =>
						new FormModel
						{
							FormId = f.FormId,
							FormName = f.FormName,
							EmbryonicPath = f.EmbryonicPath,
							Description = f.Description,
							FormTypeId = f.FormTypeId
						},
					f =>
						// f.FormGroupId == formgroupfirtid && f.FormTypeId == formtypefirtid &&
						!docforms.Contains(f.FormId));
			//var search = new FormSearchModel
			//{
			//    FormGroupId = formgroupfirtid,
			//    FormTypeId = formtypefirtid,
			//};
			ViewBag.AllForm = allform;
			//  ViewBag.Search = search;
			ViewBag.HasPrimary = hasPrimary;
			return PartialView("AddForm");
		}

		#region Thêm quy  trình

		public ActionResult FindWorkflow(Guid doctypeId, DocTypeWorkflowSearchModel search)
		{
			var docTypeWorkflowIds = _docTypeService.GetWorkFlows(p => p.WorkflowId, p => p.DocTypeId == doctypeId);
			var allWorkflows = _workflowService.GetsAs(p => new WorkflowModel
			{
				WorkflowId = p.WorkflowId,
				WorkflowName = p.WorkflowName,
				IsActivated = p.IsActivated
			},
			 p => !docTypeWorkflowIds.Contains(p.WorkflowId)
				 && (!search.IsActivatated.HasValue || (search.IsActivatated.HasValue && p.IsActivated == search.IsActivatated.Value))
				 && (string.IsNullOrEmpty(search.SearchKey) || (!string.IsNullOrEmpty(search.SearchKey) && p.WorkflowName.Contains(search.SearchKey))));
			ViewBag.Search = search;
			ViewBag.AllWorkflows = allWorkflows;
			ViewBag.DoctypeId = doctypeId;
			return PartialView("_PartialWorkflow");
		}

		public ActionResult AddDocTypeWorkflow(Guid id)
		{
			var docTypeWorkflowIds = _docTypeService.GetWorkFlows(p => p.WorkflowId, p => p.DocTypeId == id);
			Expression<Func<Workflow, bool>> spec = null;
			if (docTypeWorkflowIds != null && docTypeWorkflowIds.Any())
			{
				spec = p => !docTypeWorkflowIds.Contains(p.WorkflowId);
			}
			var notExist = _workflowService.GetsAs(p => new WorkflowModel
			{
				WorkflowId = p.WorkflowId,
				WorkflowName = p.WorkflowName,
				IsActivated = p.IsActivated
			}, spec);

			ViewBag.DoctypeId = id;
			ViewBag.AllWorkflows = notExist;
			return PartialView("AddDocTypeWorkflow");
		}

		[HttpPost]
		public JsonResult UpdateDocTypeWorkflow(Guid id, List<int> workflowIds)
		{
			if (workflowIds == null || !workflowIds.Any())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success"));
				return Json(new { success = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success") });
			}
			try
			{
				_docTypeService.UpdateWorkflows(id, workflowIds);
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success"));
				return Json(new { success = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success") });
			}
			catch (EgovException ex)
			{
				LogException(ex);

				return Json(new { error = ex.Message });
			}
			catch (Exception ex)
			{
				LogException(ex);
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error"));
				return Json(new { error = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error") });
			}
		}

		[HttpPost]
		public JsonResult ChangeDocTypeWorkflowStatus(Guid docTypeId, int workflowId, bool status)
		{
			try
			{
				_docTypeService.ChangeActivatedWorkflows(docTypeId, workflowId, status);
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Success"));
				return Json(new { success = _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Success") });
			}
			catch (EgovException ex)
			{
				LogException(ex);
				return Json(new { error = ex.Message });
			}
			catch (Exception ex)
			{
				LogException(ex);
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Error"));
				return Json(new { error = _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Error") });
			}
		}

		[HttpPost]
		public ActionResult DeleteDocTypeWorkFlow(Guid id, int workflowId)
		{
			try
			{
				_docTypeService.DeleteWorkflows(id, new List<int> { workflowId });
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.DocType.Deleted"));
				SuccessNotification(_resourceService.GetResource("Common.DocType.Deleted"));
			}
			catch (EgovException ex)
			{
				LogException(ex);
				ErrorNotification(ex.Message);
			}
			catch (Exception ex)
			{
				LogException(ex);
			}

			return RedirectToAction("DocTypeWorkflow", new { id = id });
		}

		public ActionResult DocTypeWorkflow(Guid id)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			//if (!HasPermission())
			//{
			//    ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionAddForm"));
			//    return RedirectToAction("Index");
			//}

			var docType = _docTypeService.Get(id);
			if (docType == null)
			{
				return RedirectToAction("Index");
			}

			var docTypeWorkflows = _workflowService.Raw
					  .Join(_docFieldService.RawDocfieldDoctypeWorkflow,
						  p => p.WorkflowId, x => x.WorkflowId,
						  (p, x) => new { Workflow = p, DocfieldDoctypeWorkflow = x })
					  .Where(p => p.DocfieldDoctypeWorkflow.DocTypeId == id)
					  .Select(p => new WorkflowModel
					  {
						  WorkflowId = p.Workflow.WorkflowId,
						  WorkflowName = p.Workflow.WorkflowName,
						  IsActivated = p.DocfieldDoctypeWorkflow.IsActivated
					  }).ToList();

			ViewBag.DoctypeId = id;
			ViewBag.DocTypeWorkflows = docTypeWorkflows;
			ViewBag.CategoryBusinessId = docType.CategoryBusinessId;

			return PartialView("DocTypeWorkflow");
		}

		#endregion

		[HttpPost]
		public ActionResult Delete(Guid id)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionDelete"));
				ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionDelete"));
				return RedirectToAction("Index");
			}

			DocType docType = _docTypeService.Get(id);
			if (docType != null)
			{
				try
				{
					_docTypeService.Delete(docType);
					CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.DocType.Deleted"));
					SuccessNotification(_resourceService.GetResource("Common.DocType.Deleted"));
				}
				catch (EgovException ex)
				{
					LogException(ex);
					ErrorNotification(ex.Message);
				}
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult DeleteForm(Guid id, Guid dtype)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionDeleteForm"));
				ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionDeleteForm"));
				return RedirectToAction("Index");
			}

			var doctypeform = _doctypeformService.Get(id, dtype);
			if (doctypeform != null)
			{
				_doctypeformService.Delete(doctypeform);
			}
			return RedirectToAction("Form", new { id = dtype });
		}

		public ActionResult Search(DocTypeSearchModel search, int pageSize)
		{
			IEnumerable<DocTypeModel> model = null;
			if (Request.IsAjaxRequest())
			{
				if (ModelState.IsValid)
				{
					model = GetDocTypeModels(search, DEFAULT_SORT_BY, false, 1, pageSize);
				}
				else
				{
					ViewBag.SortAndPage = new SortAndPagingModel
					{
						PageSize = pageSize,
						CurrentPage = 1,
						IsSortDescending = false,
						SortBy = DEFAULT_SORT_BY,
						TotalRecordCount = 0
					};
				}
			}
			ViewBag.ListPageSize = _generalSettings.ListPageSize;
			ViewBag.Search = search;
			return PartialView("_PartialList", model);
		}

		public ActionResult SortAndPaging(
			DocTypeSearchModel search, string sortBy,
			bool isSortDesc, int page, int pageSize)
		{
			IEnumerable<DocTypeModel> model = null;
			if (Request.IsAjaxRequest())
			{
				model = GetDocTypeModels(search, sortBy, isSortDesc, page, pageSize);
			}
			else
			{
				ViewBag.SortAndPage = new SortAndPagingModel
				{
					PageSize = pageSize,
					CurrentPage = 1,
					IsSortDescending = false,
					SortBy = DEFAULT_SORT_BY,
					TotalRecordCount = 0
				};
			}
			return PartialView("_PartialList", model);
		}

		#region quản lý quy trình cũ

		//public ActionResult ConfigWorkflow()
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionConfigWorkflow"));
		//        return RedirectToAction("Index");
		//    }

		//    var allCategoryBusiness = BindCategoryBusiness().StringifyJs();
		//    var allDocFields = _docFieldService.GetsAs(df => new { df.DocFieldId, df.DocFieldName, df.CategoryBusinessId }).StringifyJs();
		//    var allDocType = _docTypeService.GetsAs(d => new { d.DocTypeId, d.DocTypeName, d.CategoryBusinessId, d.DocFieldId, d.IsActivated })
		//        .OrderBy(d => d.DocTypeName).StringifyJs();
		//    var allWorkflow =
		//        _workflowService.GetsAs(w => new { w.WorkflowId, w.WorkflowName, w.DocTypeId, w.Template, w.IsActivated, w.ExpireProcess, w.WorkflowTypeJson })
		//            .OrderBy(w => w.WorkflowName).StringifyJs();

		//    ViewBag.AllCategoryBusiness = allCategoryBusiness;
		//    ViewBag.AllDocFields = allDocFields;
		//    ViewBag.AllDocType = allDocType;
		//    ViewBag.AllWorkflow = allWorkflow;
		//    return View();
		//}

		//[HttpPost]
		////[ValidateAntiForgeryToken(Salt = "DocTypeCreateWorkflow")]
		//public JsonResult CreateWorkflow(Guid doctypeId, string workflowName, string expireprocess, string workflowTypes)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionCreateWorkflow") });
		//    }

		//    var doctype = _docTypeService.Get(doctypeId);
		//    if (doctype == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Doctype.NotFindId") + doctypeId });
		//    }
		//    var workflowTypesTemp = Json2.ParseAs<List<WorkflowType>>(workflowTypes);
		//    foreach (var workflowType in workflowTypesTemp)
		//    {
		//        if (workflowType.Id == Guid.Empty)
		//        {
		//            workflowType.Id = Guid.NewGuid();
		//        }
		//    }
		//    workflowTypes = Json2.Stringify(workflowTypesTemp);
		//    var workflow = new Workflow
		//                       {
		//                           IsActivated = false,
		//                           DocTypeId = doctypeId,
		//                           WorkflowName = workflowName,
		//                           Json = string.Empty,
		//                           CreatedOnDate = DateTime.Now,
		//                           CreatedByUserId = User.GetUserId(),
		//                           VersionDateTime = DateTime.Now,
		//                           ExpireProcess = int.Parse(expireprocess),
		//                           WorkflowTypeJson = workflowTypes
		//                       };
		//    _workflowService.Create(workflow);
		//    return Json(new { id = workflow.WorkflowId });
		//}

		//[HttpPost]
		////[ValidateAntiForgeryToken(Salt = "DocTypeCopyWorkflow")]
		//public JsonResult CopyWorkflow(int id, Guid doctypeId)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionCopyWorkflow") });
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
		//    }
		//    var doctype = _docTypeService.Get(doctypeId);
		//    if (doctype == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Doctype.NotFindId") + doctypeId });
		//    }

		//    //HopCV
		//    //Nếu loại văn bản chưa có  quy trình nào thì active quy trình lên
		//    var exist = _workflowService.GetsAs(p => p.WorkflowId, p => p.DocTypeId == doctypeId);
		//    var copy = new Workflow
		//                   {
		//                       IsActivated = (exist != null && exist.Any()) ? false : true,
		//                       DocTypeId = doctypeId,
		//                       WorkflowName = workflow.WorkflowName,
		//                       Json = workflow.Json,
		//                       Template = workflow.Template,
		//                       CreatedOnDate = DateTime.Now,
		//                       CreatedByUserId = User.GetUserId(),
		//                       VersionDateTime = DateTime.Now,
		//                       ExpireProcess = workflow.ExpireProcess
		//                   };
		//    _workflowService.Create(copy);
		//    return Json(new { id = copy.WorkflowId });
		//}

		//public ActionResult Workflow(int id, int nodeId)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionWorkflow"));
		//        return RedirectToAction("Index");
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id);
		//        return RedirectToAction("Index", "Error");
		//    }
		//    if (string.IsNullOrEmpty(workflow.Json))
		//    {
		//        return View();
		//    }
		//    var pathOutput = workflow.JsonInObject;

		//    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
		//    if (nodeOutput == null)
		//    {
		//        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId);
		//        return RedirectToAction("Index", "Error");
		//    }

		//    ViewBag.GetTimeType = TimeType();
		//    ViewBag.AllPositions = GetAllJobTitless();
		//    ViewBag.ViewOption = GetViewOption(nodeOutput.ViewOption);
		//    ViewBag.SelectedNodes = nodeOutput.Address.StringifyJs();
		//    ViewBag.AllNodesInPath = pathOutput.Nodes.Select(u => new
		//    {
		//        u.Id,
		//        u.NodeName,
		//    }).StringifyJs();
		//    ViewBag.AllUsers = GetAllUsers();
		//    ViewBag.AllDepartments = GetAllDepartment();
		//    ViewBag.MaxDepartmentLevel = _departmentService.GetMaxLevel();
		//    ViewBag.PathId = id;
		//    ViewBag.AllDeptUserPosition = GetAllDeptUserPosition();
		//    if (nodeOutput.TimeType == 0)
		//    {
		//        nodeOutput.TimeInNode = nodeOutput.TimeInNode / 24;
		//    }
		//    return View(nodeOutput.ToModel());
		//}

		//[HttpPost]
		////[ValidateAntiForgeryToken(Salt = "DocTypeWorkflow")]
		//public JsonResult Workflow(NodeModel model)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = true, message = _resourceService.GetResource("Customer.DocType.NotPermissionUpdateWorkflow") });
		//    }

		//    // TODO: (CuongNT-?-CuongNT-030713) Xem lại việc sử dụng thuộc tính PathId
		//    if (model.PathId <= 0)
		//    {
		//        LogException(new ArgumentException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Node.PathIdError")));
		//        return Json(new { error = true });
		//    }

		//    var workflow = _workflowService.Get(model.PathId);
		//    if (workflow == null)
		//    {
		//        return Json(new { success = true });
		//    }
		//    var pathOutput = workflow.JsonInObject;
		//    var nodeOutput = pathOutput.Nodes.SingleOrDefault(t => t.Id == model.Id);
		//    if (nodeOutput == null)
		//    {
		//        return Json(new { success = true });
		//    }
		//    var oldNodeName = nodeOutput.NodeName;
		//    model.ViewOption = (string.IsNullOrEmpty(model.DocTypeView)
		//        ? 0
		//        : model.DocTypeView == "DocTypeIgnore"
		//            ? 4096
		//            : 8192)
		//                       + (string.IsNullOrEmpty(model.PageNumberView)
		//                           ? 0
		//                           : model.PageNumberView == "PageNumberIgnore" ? 32 : 2048) +
		//                       (string.IsNullOrEmpty(model.DocFieldView)
		//                           ? 0
		//                           : model.DocFieldView == "DocFieldIgnore" ? 1 : 64) +
		//                       (string.IsNullOrEmpty(model.KeywordView)
		//                           ? 0
		//                           : model.KeywordView == "KeywordIgnore" ? 2 : 128) +
		//                       (string.IsNullOrEmpty(model.GroupView)
		//                           ? 0
		//                           : model.GroupView == "GroupIgnore" ? 4 : 256) +
		//                       (string.IsNullOrEmpty(model.SendTypeView)
		//                           ? 0
		//                           : model.SendTypeView == "SenTypeIgnore" ? 8 : 512) +
		//                       (string.IsNullOrEmpty(model.DestinationView)
		//                           ? 0
		//                           : model.DestinationView == "DestinationIgnore" ? 16 : 1024);

		//    model.TimeInNode = model.TimeType == 0 ? model.TimeInNode * 24 : model.TimeInNode;
		//    var newNode = model.ToEntity(nodeOutput);
		//    pathOutput.Nodes.Remove(nodeOutput);
		//    pathOutput.Id = model.PathId;
		//    pathOutput.IsActivated = workflow.IsActivated;
		//    pathOutput.Name = workflow.WorkflowName;
		//    var listAddress = Json2.ParseAs<List<Core.Workflow.Address>>(model.NodeAddress);
		//    newNode.Address = listAddress;
		//    pathOutput.Nodes.Add(newNode);
		//    pathOutput.Nodes = pathOutput.Nodes.OrderBy(t => t.Id).ToList();
		//    if (oldNodeName != model.NodeName)
		//    {
		//        //Cập nhật lại tất cả Address của các Node
		//        foreach (var node in pathOutput.Nodes)
		//        {
		//            var address = node.Address.SingleOrDefault(t => t.NodeFrom == model.Id);
		//            if (address != null)
		//            {
		//                address.NodeNameFrom = model.NodeName;
		//            }
		//        }
		//    }
		//    workflow.Json = pathOutput.Stringify();
		//    try
		//    {
		//        _workflowService.Update(workflow);
		//    }
		//    catch (EgovException ex)
		//    {
		//        return Json(new { error = true, message = ex.Message });
		//    }

		//    return Json(new { success = true });
		//}
		//[HttpPost]
		////[ValidateAntiForgeryToken(Salt = "DocTypeUpdateWorkflow")]
		//public JsonResult UpdateWorkflow(int id, string workflowName, string expireprocess, string workflowTypes)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionUpdateWorkflow") });
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
		//    }
		//    var workflowTypesTemp = Json2.ParseAs<List<WorkflowType>>(workflowTypes);
		//    foreach (var workflowType in workflowTypesTemp)
		//    {
		//        if (workflowType.Id == Guid.Empty)
		//        {
		//            workflowType.Id = Guid.NewGuid();
		//        }
		//    }
		//    workflowTypes = Json2.Stringify(workflowTypesTemp);
		//    workflow.WorkflowName = workflowName;
		//    workflow.ExpireProcess = int.Parse(expireprocess);
		//    workflow.WorkflowTypeJson = workflowTypes;
		//    _workflowService.Update(workflow);
		//    return Json(new { id = workflow.WorkflowId });
		//}

		//[HttpPost]
		////[ValidateAntiForgeryToken(Salt = "DocTypeDeleteWorkflow")]
		//public JsonResult DeleteWorkflow(int id)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionDeleteWorkflow") });
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
		//    }
		//    _workflowService.Delete(workflow);
		//    return Json(new { success = true });
		//}

		//public JsonResult GetWorkflow(int id)
		//{
		//    var workflow = _workflowService.Get(id);
		//    return workflow == null
		//               ? Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id }, JsonRequestBehavior.AllowGet)
		//               : Json(new { workflow = workflow.Json }, JsonRequestBehavior.AllowGet);
		//}

		//[HttpPost]
		//[ValidateInput(false)]
		////[ValidateAntiForgeryToken(Salt = "DocTypeSaveWorkflow")]
		//public JsonResult SaveWorkflow(int id, string json)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionSaveWorkflow") });
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
		//    }
		//    if (!string.IsNullOrWhiteSpace(json))
		//    {
		//        try
		//        {
		//            var data = Json2.ParseAs<Path>(json);
		//        }
		//        catch (Exception)
		//        {
		//            return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotValid") });
		//        }
		//    }
		//    workflow.Json = json;
		//    workflow.LastModifiedByUserId = User.GetUserId();
		//    workflow.LastModifiedOnDate = DateTime.Now;
		//    _workflowService.Update(workflow);
		//    return Json(new { success = true });
		//}

		//[HttpPost]
		////[ValidateAntiForgeryToken(Salt = "DocTypeSetIsActiveWorkflow")]
		//public JsonResult SetIsActiveWorkflow(int id, bool isActivated)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionSetIsActiveWorkflow") });
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
		//    }
		//    if (isActivated)
		//    {
		//        var doctype = _docTypeService.Get(workflow.DocTypeId);
		//        var allWorkflowOfDocType = doctype.Workflows;
		//        var currentWorkflowIsActive =
		//            allWorkflowOfDocType.SingleOrDefault(w => w.DocTypeId == workflow.DocTypeId && w.IsActivated);
		//        if (currentWorkflowIsActive != null)
		//        {
		//            currentWorkflowIsActive.IsActivated = false;
		//        }
		//        workflow.IsActivated = true;
		//    }
		//    else
		//    {
		//        workflow.IsActivated = false;
		//    }
		//    _workflowService.Update(workflow);
		//    return Json(new { id = workflow.WorkflowId });
		//}     

		//public string GetWorkflowTemplate(int id)
		//{
		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        return null;
		//    }
		//    return workflow.Template;
		//}

		//public string GetNodeTemplate(int workflowId, int nodeId)
		//{
		//    var workflow = _workflowService.Get(workflowId);
		//    if (workflow == null)
		//    {
		//        return null;
		//    }
		//    if (string.IsNullOrEmpty(workflow.Json))
		//    {
		//        return null;
		//    }
		//    var pathOutput = workflow.JsonInObject;
		//    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
		//    if (nodeOutput == null)
		//    {
		//        return null;
		//    }
		//    return nodeOutput.Template;
		//}

		//public ActionResult ConfigTemplateWorkflow(int id)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateWorkflow"));
		//        return RedirectToAction("Index", "Error");
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id);
		//        return RedirectToAction("Index", "Error");
		//    }
		//    ViewBag.CategoryBusiness = workflow.DocType.CategoryBusinessIdInEnum.ToString();
		//    ViewBag.WorkflowId = id;
		//    ViewBag.Template = workflow.Template;
		//    ViewBag.TemplateCategoryBusiness = FileManager.Default.Exist(ViewBag.CategoryBusiness, Server.MapPath(TEMPLALTE_PATH))
		//        ? FileManager.Default.ReadString(ViewBag.CategoryBusiness, Server.MapPath(TEMPLALTE_PATH))
		//        : string.Empty;
		//    ViewBag.OtherWorkflows = _workflowService.GetsAs(
		//        w => new { w.WorkflowId, w.WorkflowName, w.DocType.DocTypeName },
		//        w => w.WorkflowId != id && w.DocType.CategoryBusinessId == workflow.DocType.CategoryBusinessId).StringifyJs();
		//    return View();
		//}

		//[HttpPost]
		//[ValidateInput(false)]
		////[ValidateAntiForgeryToken(Salt = "DocTypeConfigTemplateWorkflow")]
		//public JsonResult ConfigTemplateWorkflow(int id, string template)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateWorkflow") });
		//    }

		//    var workflow = _workflowService.Get(id);
		//    if (workflow == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + id });
		//    }
		//    workflow.Template = template;
		//    _workflowService.Update(workflow);
		//    return Json(new { success = true }); ;
		//}

		//public ActionResult ConfigTemplateNode(int workflowId, int nodeId)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateNode"));
		//        return RedirectToAction("Index", "Error");
		//    }

		//    var workflow = _workflowService.Get(workflowId);
		//    if (workflow == null)
		//    {
		//        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + workflowId);
		//        return RedirectToAction("Index", "Error");
		//    }
		//    if (string.IsNullOrEmpty(workflow.Json))
		//    {
		//        ErrorNotification("Chưa cấu hình quy trình");
		//        return RedirectToAction("Index", "Error");
		//    }
		//    var pathOutput = workflow.JsonInObject;
		//    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
		//    if (nodeOutput == null)
		//    {
		//        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId);
		//        return RedirectToAction("Index", "Error");
		//    }

		//    ViewBag.CategoryBusiness = workflow.DocType.CategoryBusinessIdInEnum.ToString();
		//    ViewBag.WorkflowId = workflowId;
		//    ViewBag.TemplateWorkflow = workflow.Template;
		//    ViewBag.NodeId = nodeId;
		//    ViewBag.TemplateNode = nodeOutput.Template;
		//    ViewBag.OtherNodes =
		//        pathOutput.Nodes.Where(n => n.Id != nodeId)
		//            .Select(n => new { n.Id, n.NodeName })
		//            .StringifyJs();

		//    return View();
		//}

		//[HttpPost]
		//[ValidateInput(false)]
		////[ValidateAntiForgeryToken(Salt = "DocTypeConfigTemplateNode")]
		//public JsonResult ConfigTemplateNode(int workflowId, int nodeId, string template)
		//{
		//    //Hopcv: 190614
		//    //Kiểm tra quyền
		//    if (!HasPermission())
		//    {
		//        return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionConfigTemplateNode") });
		//    }

		//    var workflow = _workflowService.Get(workflowId);
		//    if (workflow == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotFindId") + workflowId });
		//    }
		//    if (string.IsNullOrEmpty(workflow.Json))
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Workflow.NotSetting") });
		//    }
		//    var pathOutput = workflow.JsonInObject;
		//    var nodeOutput = pathOutput.Nodes.Single(t => t.Id == nodeId);
		//    if (nodeOutput == null)
		//    {
		//        return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.NodeOutput.NotFindId") + nodeId });
		//    }
		//    nodeOutput.Template = template;
		//    workflow.Json = pathOutput.Stringify();
		//    _workflowService.Update(workflow);
		//    return Json(new { success = true }); ;
		//}

		#endregion

		public JsonResult CategoryBusinessChange(int categoryBusinessId)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCategoryBusinessChange"));
				return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionCategoryBusinessChange") });
			}

			var result = Json(
						   new
						   {
							   AllCategorys =
							   GetAllCategorys(categoryBusinessId).Select(
								   u => new { u.CategoryId, u.CategoryName }).StringifyJs(),
							   AllDocFields =
							   GetAllDocFields(categoryBusinessId).Select(
								   u => new { u.DocFieldId, u.DocFieldName }).StringifyJs()
						   }, JsonRequestBehavior.AllowGet);
			return result;
		}

		[HttpPost]
		public JsonResult ChangeIsPrimary(Guid formid, Guid doctypeid, bool isprimary)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsPrimary"));
				return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsPrimary") });
			}

			_doctypeformService.Create(new DocTypeForm
			{
				DocTypeId = doctypeid,
				FormId = formid,
				IsPrimary = isprimary,
				IsActive = true
			});

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ChangeIsActive(Guid formid, Guid doctypeid, bool status)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive"));
				return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionChangeIsActive") });
			}

			var doctypeform = _doctypeformService.Get(formid, doctypeid);
			if (doctypeform == null)
			{
				return Json(new { error = true }, JsonRequestBehavior.AllowGet);
			}
			doctypeform.IsActive = status;
			_doctypeformService.Update(doctypeform);
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult UpdateForm(Guid doctypeId, Guid formMainId, string formsId)
		{
			//Hopcv: 190614
			//Kiểm tra quyền
			if (!HasPermission())
			{
				return Json(new { error = _resourceService.GetResource("Customer.DocType.NotPermissionUpdateForm") });
			}

			if (formMainId.ToString() != "00000000-0000-0000-0000-000000000000")
			{
				_doctypeformService.Create(new DocTypeForm
				{
					DocTypeId = doctypeId,
					FormId = formMainId,
					IsPrimary = true
				});
			}

			if (formsId != "")
			{
				var forms = formsId.Split(',');
				foreach (var formId in forms)
				{
					_doctypeformService.Create(new DocTypeForm
					{
						DocTypeId = doctypeId,
						FormId = Guid.Parse(formId),
						IsPrimary = false
					});
				}
			}
			//return RedirectToAction("Form", new { id = doctypeId });
			CreateActivityLog(ActivityLogType.Admin, "Update Form thành công");
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Paper(Guid id)
		{
			ViewBag.DoctypeId = id;
			var model = _doctypePaperService.GetPapersOfDoctype(id);
			var docType = _docTypeService.Get(id);
			ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
			return View(model);
		}

		public ActionResult DisplayPapers(Guid id)
		{
			return View(_doctypePaperService.GetPapersNotOfDoctype(id).ToListModel());
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult AddPaper(Guid doctypeId, int paperId)
		{
			var doctypePaper = new DoctypePaper(doctypeId, paperId, true);
			_doctypePaperService.Create(doctypePaper);
			CreateActivityLog(ActivityLogType.Admin, "Add Paper thành công");
			return RedirectToAction("Paper", "Admin/Doctype", new { id = doctypeId });
		}

		[HttpPost]
		public ActionResult DeleteDoctypePaper(Guid doctypeId, int doctypePaperId)
		{
			try
			{
				var doctypePaper = _doctypePaperService.Get(doctypePaperId);
				_doctypePaperService.Delete(doctypePaper);
				CreateActivityLog(ActivityLogType.Admin, "Xóa doctype paper thành công");
			}
			catch (Exception ex)
			{
				LogException(ex);
				ErrorNotification(ex.Message);
			}
			return RedirectToAction("Paper", "Doctype", new { id = doctypeId });
		}

		//[HttpPost]
		//public ActionResult ChangePaperRequire(Guid doctypeId, int doctypePaperId, bool isRequired)
		//{
		//    try
		//    {
		//        var doctypePaper = _doctypePaperService.Get(doctypePaperId);
		//        if (doctypePaper != null)
		//        {
		//            doctypePaper.IsRequired = isRequired;
		//            _doctypePaperService.Update(doctypePaper);
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorNotification(ex.Message);
		//    }
		//    return RedirectToAction("Paper", "Doctype", new { id = doctypeId });
		//}

		[HttpPost]
		public JsonResult ChangePaperRequire(int doctypePaperId)
		{
			var result = false;
			var doctypePaper = _doctypePaperService.Get(doctypePaperId);
			if (doctypePaper != null)
			{
				if (doctypePaper.IsRequired.HasValue)
				{
					doctypePaper.IsRequired ^= true;
				}
				else
				{
					//Trường hợp chuyển từ hệ thống cũ sang IsRequired = null
					doctypePaper.IsRequired = true;
				}
				_doctypePaperService.Update(doctypePaper);
				result = true;
			}

			return Json(result);
		}

		public ActionResult Fee(Guid id)
		{
			ViewBag.DoctypeId = id;
			var model = _doctypeFeeService.GetFeesOfDoctype(id);
			var docType = _docTypeService.Get(id);
			ViewBag.CategoryBusinessId = docType.CategoryBusinessId;
			return View(model);
		}

		public ActionResult DisplayFees(Guid id)
		{
			return View(_doctypeFeeService.GetFeesNotOfDoctype(id).ToListModel());
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult AddFee(Guid doctypeId, int feeId)
		{
			var doctypeFee = new DoctypeFee(doctypeId, feeId, true);
			_doctypeFeeService.Create(doctypeFee);
			return RedirectToAction("Fee", "Admin/Doctype", new { id = doctypeId });
		}

		[HttpPost]
		public ActionResult DeleteDoctypeFee(Guid doctypeId, int doctypeFeeId)
		{
			try
			{
				var doctypeFee = _doctypeFeeService.Get(doctypeFeeId);
				_doctypeFeeService.Delete(doctypeFee);
			}
			catch (Exception ex)
			{
				LogException(ex);
				ErrorNotification(ex.Message);
			}
			return RedirectToAction("Fee", "Doctype", new { id = doctypeId });
		}

		#region "Doctype-EgovOnline"

		/// <summary>
		/// Danh sách văn bản quy phạm của thủ tục hành chính
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult DocTypeLaw(Guid id)
		{
			ViewBag.DoctypeId = id;
			var doctype = _docTypeService.Get(id);
			ViewBag.CategoryBusinessId = doctype.CategoryBusinessId;
			if (doctype == null)
			{
				return RedirectToAction("Index");
			}
			var model = doctype.ToModel();
			return View(model);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult CreateDocTypeLaw(Guid docTypeId, int[] lawIds)
		{
			if (lawIds != null && lawIds.Any())
			{
				var docType = _docTypeService.Get(docTypeId);
				if (docType == null)
				{
					return Json(new { error = _resourceService.GetResource("Customer.DocType.NotExist") });
				}
				try
				{
					var laws = _lawService.Gets(p => lawIds.Contains(p.LawId));
					var lawIdToAdds = new List<int>();
					var result = new List<Law>();
					foreach (var law in laws)
					{
						if (!docType.DocTypeLaws.Any(d => d.LawId == law.LawId))
						{
							result.Add(law);
							lawIdToAdds.Add(law.LawId);
						};
					}

					_docTypeService.AddDoctypeLaws(docType.DocTypeId, lawIdToAdds);
					return Json(new
					{
						success = _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Success"),
						data = result.Select(p => new { p.LawId, p.NumberSign, p.SubContent })
					});
				}
				catch (Exception ex)
				{
					LogException(ex);
					CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Error"));
					return Json(new { error = _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Error") });
				}
			}

			CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Success"));
			return Json(new { success = _resourceService.GetResource("Customer.DocType.CreateDocTypeLaw.Success") });
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteDocTypeLaw(Guid docTypeId, int lawId)
		{
			try
			{
				_docTypeService.DeleteDocTypeLaw(docTypeId, lawId);
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Success"));
				SuccessNotification(_resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Success"));
			}
			catch (Exception ex)
			{
				LogException(ex);
				CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Error"));
				ErrorNotification(_resourceService.GetResource("Customer.DocType.DeleteDocTypeLaw.Error"));
			}
			return RedirectToAction("DocTypeLaw", "DocType", new { id = docTypeId });
		}

		/// <summary>
		/// Danh sách văn bản quy phạm của thủ tục hành chính
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult DocTypeTemplate(Guid id)
		{
			ViewBag.DoctypeId = id;
			ViewBag.CategoryBusinessId = 4;
			return View(_doctypeTemplateService.GetsByDoctypeId(id).ToListModel());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <param name="doctypeTemplateId"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult AddDoctypeTemplate(Guid doctypeId, int onlineTemplateId, string onlineTemplateName)
		{
			if (!_doctypeTemplateService.Exist(x => x.DoctypeId == doctypeId && x.OnlineTemplateId == onlineTemplateId))
			{
				var doctypeTemplate = new DoctypeTemplate
				{
					DoctypeId = doctypeId,
					OnlineTemplateId = onlineTemplateId,
					Name = onlineTemplateName
				};
				_doctypeTemplateService.Create(doctypeTemplate);
			}
			return Json(new { success = true });
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult DisplayOnlineTemplates()
		{
			return View(_onlineTemplateService.Gets().ToListModel());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <param name="doctypeTemplateId"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteDoctypeTemplate(Guid doctypeId, int doctypeTemplateId)
		{
			try
			{
				var doctypeTemplate = _doctypeTemplateService.Get(doctypeTemplateId);
				_doctypeTemplateService.Delete(doctypeTemplate);
			}
			catch (Exception ex)
			{
				ErrorNotification(ex.Message);
			}
			return RedirectToAction("DocTypeTemplate", "DocType", new { id = doctypeId });
		}

		[HttpPost]
		public ActionResult GetManufacturer(string Item)
		{
			var levelId = Convert.ToInt32(Item);
			var offfices = _officeService.GetOfficesByLevelId(levelId);
			return Json(offfices);
		}

		#endregion "Doctype-EgovOnline"

		#region Private Method

		private IEnumerable<SelectListItem> TimeType()
		{
			return new List<SelectListItem>{
					 new SelectListItem{Value="0",Text="Ngày"},
					 new SelectListItem{Value="1",Text="Giờ"}
				};
		}

		private string ConverToUnsign(string text)
		{
			text = text.ToUpper();
			string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
			string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
			for (int i = 0; i < To.Length; i++)
			{
				text = text.Replace(convert[i], To[i]);
			}
			return text;
		}

		private void LoadDropDownList(int? levelId = 0, int? officeId = 0, int? type = 0)
		{
			ViewBag.ListActionLevel = GetActionLevel();
			ViewBag.ListOffice = new SelectList(_officeService.GetOfficesByLevelId(levelId.HasValue ? (int)levelId : 0), "OfficeId", "OfficeName", officeId);
			ViewBag.ListLevel = _resourceService.EnumToSelectList<LevelType>(levelId);
		}

		private List<SelectListItem> GetActionLevel()
		{
			return _actionLevelService.GetsAs(a => new SelectListItem { Text = a.ActionLevelName, Value = a.ActionLevelCode }).ToList();
			/* return new List<SelectListItem>() {
				new SelectListItem() {Text = "Năm", Value = "1" },
				new SelectListItem() {Text = "6 Tháng", Value = "2" },
				new SelectListItem() {Text = "Quý", Value = "3" },
				new SelectListItem() {Text = "Tháng", Value = "4" },
				new SelectListItem() {Text = "Tuần", Value = "5" },
				new SelectListItem() {Text = "Ngày", Value = "6" },
				new SelectListItem() {Text = "Khẩn cấp ", Value = "7" },
            }; */
		}

		private IEnumerable<CategoryModel> GetAllCategorys(int? categoryBusinessId = null)
		{
			if (categoryBusinessId == null)
			{
				return _categoryService.GetsAs(c => new CategoryModel { CategoryId = c.CategoryId, CategoryName = c.CategoryName }).OrderBy(t => t.CategoryName);
			}

			return
				_categoryService.GetsAs(
					c => new CategoryModel { CategoryId = c.CategoryId, CategoryName = c.CategoryName },
					t =>
						((CategoryBusinessTypes)t.CategoryBusinessId & (CategoryBusinessTypes)categoryBusinessId) ==
						(CategoryBusinessTypes)categoryBusinessId).
					OrderBy(t => t.CategoryName);
		}

		private IEnumerable<DocFieldModel> GetAllDocFields(int? categoryBusinessId = null)
		{
			if (categoryBusinessId == null)
			{
				return
					_docFieldService.GetsAs(
						d => new DocFieldModel { DocFieldId = d.DocFieldId, DocFieldName = d.DocFieldName })
						.OrderBy(t => t.DocFieldName);
			}

			return
				_docFieldService.GetsAs(d => new DocFieldModel { DocFieldId = d.DocFieldId, DocFieldName = d.DocFieldName },
					t =>
					((CategoryBusinessTypes)t.CategoryBusinessId & (CategoryBusinessTypes)categoryBusinessId) == (CategoryBusinessTypes)categoryBusinessId).
					OrderBy(t => t.DocFieldName);
		}

		private IEnumerable<CodeModel> GetAllCodes()
		{
			return _codeService.GetsAs(c => new CodeModel { CodeId = c.CodeId, Template = c.Template });
		}

		private string GetAllJobTitless()
		{
			return
				_positionService.GetCacheAllPosition().Select(
					u => new { value = u.PositionId, label = u.PositionName }).StringifyJs();
		}

		private void ReBindDataWhenError(int? categoryBusinessId = null, List<int> storeIdCheckeds = null, int storeIdDefault = 0)
		{
			ViewBag.AllCategoryBusiness = BindCategoryBusiness(categoryBusinessId);
			ViewBag.AllCategorys = GetAllCategorys(categoryBusinessId == null ? (int)CategoryBusinessTypes.VbDen : categoryBusinessId.Value);

			var stores = _storeService.GetsAs(s => new StoreModel { StoreId = s.StoreId, StoreName = s.StoreName, Checked = false }).OrderByDescending(s => s.Checked).ThenBy(s => s.StoreName);
			if (storeIdCheckeds != null && storeIdCheckeds.Any())
			{
				foreach (var store in stores)
				{
					if (storeIdCheckeds.Contains(store.StoreId))
					{
						store.Checked = true;
						if (storeIdDefault != 0 && store.StoreId == storeIdDefault)
						{
							store.IsDefault = true;
						}
					}
				}
			}
			//stores.OrderBy(s => s.Checked).OrderByDescending(s => s.StoreName).ToList();

			ViewBag.AllStores = stores;

			ViewBag.AllDocFields = GetAllDocFields(categoryBusinessId == null ? (int)CategoryBusinessTypes.VbDen : categoryBusinessId.Value);
			ViewBag.AllCodes = GetAllCodes();
		}

		private List<SelectListItem> BindCategoryBusiness(int? categoryBusinessId = null)
		{
			var result = _resourceService.EnumToSelectList<CategoryBusinessTypes>(categoryBusinessId);
			return result;
		}

		private static List<int> GetViewOption(int giatri)
		{
			var result = new List<int>();
			for (int i = 13; i >= 0; i--)
			{
				if (giatri >= Math.Pow(2, i))
				{
					giatri = giatri - (int)Math.Pow(2, i);
					result.Add(i);
				}
			}
			return result;
		}

		private string GetAllUsers()
		{
			return _userService.GetAllCached().Select(u =>
				new
				{
					value =
						u.UserId,
					label =
						u.Username + " - " +
						u.FullName,
					fullname =
						u.FullName,
					username =
						u.Username,
					objecttype =
						"user",
					storevalue =
						"$userid:" +
						u.UserId.ToString(CultureInfo.InvariantCulture)
				}).OrderBy(
					u => u.username)
				.StringifyJs();
		}

		private string GetAllDepartment()
		{
			var alldepartments =
				_departmentService.GetCacheAllDepartments(true);
			return alldepartments.Select(u => new
			{
				value = u.DepartmentId,
				parentid = u.ParentId.HasValue ? u.ParentId : 0,
				data = u.DepartmentName,
				metadata = new { id = u.DepartmentId },
				idext = u.DepartmentIdExt,
				state = "leaf",
				attr = new { id = u.DepartmentId, rel = "dept" },
				pathname = u.DepartmentPath
			}).StringifyJs();
		}

		private string GetAllDeptUserPosition()
		{
			return _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
				t =>
				new
				{
					departmentid = t.DepartmentId,
					userid = t.UserId,
					positionid = t.PositionId,
					idext = t.DepartmentIdExt
				}).StringifyJs();
		}

		private IEnumerable<DocTypeModel> GetDocTypeModels(DocTypeSearchModel search, string sortBy,
			 bool isSortDesc, int page, int pageSize)
		{
			int totalRecords;
			var model = _docTypeService.GetsAs(out totalRecords,
				d => new { d.DocTypeId, d.DocTypeName, d.IsActivated, d.DocField.DocFieldName, d.ActionLevel },
				pageSize: pageSize,
				sortBy: sortBy,
				isDescending: isSortDesc,
				currentPage: page,
				docFieldId: search.DocFieldId,
				actionLevel: search.ActionLevel,
				isActivated: search.IsActivated,
				docTypeName: search.DocTypeName)
				.Select(t => new DocTypeModel
				{
					DocTypeId = t.DocTypeId,
					DocTypeName = t.DocTypeName,
					IsActivated = t.IsActivated,
					DocFieldName = t.DocFieldName,
					ActionLevel = t.ActionLevel
				});

			var sortAndPage = new SortAndPagingModel
			{
				PageSize = pageSize,
				CurrentPage = page,
				IsSortDescending = isSortDesc,
				SortBy = sortBy,
				TotalRecordCount = totalRecords
			};

			CreateCookieSearch(search, sortAndPage);

			ViewBag.SortAndPage = sortAndPage;
			ViewBag.ListPageSize = _generalSettings.ListPageSize;
			ViewBag.Search = search;
			return model;
		}

		private void CreateCookieSearch(DocTypeSearchModel search, SortAndPagingModel sortpage)
		{
			var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
			HttpCookie cookie = Request.Cookies[CookieName.SearchDocType];
			if (cookie != null)
			{
				cookie.Expires = DateTime.Now.AddDays(1);
				cookie.Value = data.StringifyJs();
			}
			else
			{
				cookie = new HttpCookie(CookieName.SearchDocType, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
			}

			cookie.Path = "/admin";
			Response.Cookies.Add(cookie);
		}

		private IEnumerable<WorkflowModel> GetAllWorkflows()
		{
			return _workflowService.GetsAs(c => new WorkflowModel { WorkflowId = c.WorkflowId, WorkflowName = c.WorkflowName });
		}

        #endregion Private Method


        #endregion "Module Admin"

        #region Import

        public ActionResult ImportDocTypes()
		{
			return View();
		}

        [HttpPost]
        public ActionResult ImportDocTypes(HttpPostedFileBase importFile, string sheetName)
        {
            try
            {
                var now = DateTime.Now;
                if (importFile != null)
                {
                    var xlsxParser = new XlsxToJson(importFile.InputStream);
                    var json = xlsxParser.ConvertXlsxToJson(1, 2, 1, 1);
                    var jsonConvert = Newtonsoft.Json.JsonConvert.SerializeObject(json);
                    var listJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocTypePlusImport>>(jsonConvert);
                    string currentDocfield = "";
                    Workflow workFlow = null;
                    Store store = null;
                    DocField docField = null;
                   
                    foreach (var item in listJson)
                    {
                        try
                        {
                            var doctypeName = item.doctypename;
                            if (string.IsNullOrEmpty(doctypeName) || doctypeName.Equals("Tên loại hồ sơ", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            var doctype = _docTypeService.Gets(d => d.DocTypeName.Equals(doctypeName, StringComparison.OrdinalIgnoreCase));
                            if (doctype.Any())
                            {
                                continue;
                            }

                            var docfieldName = item.docfieldname;

                            if (currentDocfield != docfieldName)
                            {
                                // Tạo lĩnh vực
                                docField = EnsureDocField(docfieldName);
                            }

                            // Tạo quy trình
                            // workFlow = CreateWorkflow(doctypeName);

                            // Lĩnh vực - quy trình
                            // _docFieldService.UpdateWorkflows(docField.DocFieldId, new List<int>() { workFlow.WorkflowId });

                            var storeCode = "$N$";

                            // Tạo sổ hồ sơ cho lĩnh vực
                            store = EnsureStore(doctypeName, storeCode);

                            var form = EnsureForm(doctypeName, item.docfieldname, item.formcode, item.fileName, item.filePath, item.configForm);

                            var level = item.level;
                            var levelValue = GetLevelValue(level);
                            CreateNewDoctype(doctypeName, docField, workFlow, form, store, levelValue, int.Parse(item.caphanhchinh));
                        }
                        catch (Exception ex)
                        {
                            LogException("Lỗi: RowNumber = " + item.docfieldname + ", Message = " + ex.Message);
                        }

                        // break; //test dòng đầu
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return View();
        }
        private int GetLevelValue(string level)
		{
			var levelValue = 1;
			if (!int.TryParse(level, out levelValue))
			{
				return 2;
			}
			if (levelValue < 0 || levelValue > 4)
			{
				return 2;
			}
			return levelValue;
		}

		private void CreateNewDoctype(string doctypeName, DocField docField, Workflow workflow, Form form, Store store, int level, int caphanhchinh)
		{
			if (docField == null || form == null)
			{
				return;
			}

			// Add sổ vào loại hồ sơ
			var storeIds = new List<int>() { store.StoreId };

			var doctype = new DocType()
			{
				DocTypeName = doctypeName,
				DocFieldId = docField.DocFieldId,
				CategoryBusinessId = 4,
				CategoryId = 1,
				ActionLevel = level,
                LevelId = caphanhchinh,
				CreatedByUserId = User.GetUserId(),
				CreatedOnDate = DateTime.Now,
				IsActivated = true,
				LastModifiedByUserId = User.GetUserId(),
				LastModifiedOnDate = DateTime.Now,
				StoreIds = string.Join(";", storeIds),
			};

			_docTypeService.Create(doctype);

			// thêm form
			_doctypeformService.Create(new DocTypeForm()
			{
				FormId = form.FormId,
				DocTypeId = doctype.DocTypeId,
				IsActive = true,
				IsPrimary = true
			});

			//var doctypeWorkflow = new DocfieldDoctypeWorkflow()
			//{
			//	DocFieldId = docField.DocFieldId,
			//	DocTypeId = doctype.DocTypeId,
			//	WorkflowId = workflow.WorkflowId
			//};

			//_workflowService.CreateDocFieldDocTypeWorkflow(new List<DocfieldDoctypeWorkflow>() { doctypeWorkflow });
		}

		private Form EnsureForm(string doctypeName, string docfield, string formCode, string fileName, string filePath, string configForm)
		{
			var formName = "Biểu mẫu " + " " + doctypeName;

			Form result;
			FormGroup formGroup;
            var formGroups = _formgroupService.Gets(d => d.FormGroupName.Equals(docfield, StringComparison.OrdinalIgnoreCase));
            if (!formGroups.Any())
            {
                formGroup = new FormGroup()
                {
                   FormGroupName = docfield
                };

                _formgroupService.Create(formGroup);
            }
            else
            {
                formGroup = formGroups.First();
            }

            // Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
            var forms = _formService.Gets(d => d.FormName.Equals(formName, StringComparison.OrdinalIgnoreCase) && d.FormTypeId == (int)Bkav.eGovCloud.Entities.FormType.DynamicForm);
			if (!forms.Any())
			{
				result = new Form()
				{
					FormName = formName,
					FormTypeId = (int)Bkav.eGovCloud.Entities.FormType.DynamicForm,
					IsActivated = 1,
					IsPrimary = true,
					LastModifiedByUserId = User.GetUserId(),
					LastModifiedOnDate = DateTime.Now,
					CreatedByUserId = User.GetUserId(),
					CreatedOnDate = DateTime.Now,
					Description = "",
					FormGroupId = formGroup.FormGroupId,
                    EmbryonicPath = fileName,
                    EmbryonicLocationName = filePath,
                    ConfigForm = configForm
                };

				_formService.Create(result);
			}
			else
			{
				result = forms.First();
			}
			return result;
		}

		private Store EnsureStore(string doctypeName, string codePattern)
		{
			if (string.IsNullOrWhiteSpace(doctypeName))
			{
				return null;
			}

			Store result;
			var storeName = "Sổ " + doctypeName;

			// Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
			var stores = _storeService.Gets(d => d.StoreName.Equals(storeName, StringComparison.OrdinalIgnoreCase));
			if (!stores.Any())
			{
				result = CreateNewStore(storeName, codePattern);
			}
			else
			{
				result = stores.First();
			}
			return result;
		}

		private Store CreateNewStore(string storeName, string codePattern)
		{
			var increasement = EnsureIncrease(storeName);
			var code = EnsureCode(storeName, codePattern, increasement.IncreaseId);
			var storeCode = new List<StoreCode>();
			storeCode.Add(new StoreCode() { CodeId = code.CodeId });
			var store = new Store()
			{
				StoreName = storeName,
				CategoryBusinessId = 4,
				StoreCodes = storeCode
			};
			_storeService.Create(store);

			return store;
		}

		private Code EnsureCode(string storeName, string codePattern, int increaseId)
		{
			var codeName = "Mẫu số " + storeName;
			Code result;
			// Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
			var codes = _codeService.GetsFromCache().Where(d => d.CodeName.Equals(codeName, StringComparison.OrdinalIgnoreCase));
			if (!codes.Any())
			{
				result = new Code()
				{
					CodeName = codeName,
					CreatedByUserId = User.GetUserId(),
					CreatedOnDate = DateTime.Now,
					LastModifiedByUserId = User.GetUserId(),
					LastModifiedOnDate = DateTime.Now,
					NumberLastest = 0,
					Template = codePattern,
					IncreaseId = increaseId,
					BussinessDocFieldDocTypeGroupId = 3
				};

				_codeService.Create(result);

				var hsmcCategory = _categoryService.Get(16);
				if (hsmcCategory != null && hsmcCategory.CategoryName.Equals("Hồ sơ một cửa", StringComparison.OrdinalIgnoreCase))
				{
					if (hsmcCategory.CodeIds == null)
					{
						hsmcCategory.CodeIds = new List<int>();
					}
					hsmcCategory.CodeIds.Add(result.CodeId);
					_categoryService.Update(hsmcCategory, hsmcCategory.CategoryName);
				}
			}
			else
			{
				result = codes.First();
			}

			return result;
		}

		private Increase EnsureIncrease(string storeName)
		{
			var increaseName = "Nhảy số " + storeName;

			Increase result;
			// Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
			var incs = _increaseService.Gets(d => d.Name.Equals(increaseName, StringComparison.OrdinalIgnoreCase));
			if (!incs.Any())
			{
				result = new Increase()
				{
					Name = increaseName,
					BussinessDocFieldDocTypeGroupId = 3,
					Value = 1
				};

				_increaseService.Create(result);
			}
			else
			{
				result = incs.First();
			}

			return result;
		}

		private Workflow CreateWorkflow(string doctypeName)
		{
			var workflowName = "Quy trình " + doctypeName;
			Workflow workflow;
			var workflows = _workflowService.Gets(w => w.WorkflowName.Equals(workflowName, StringComparison.OrdinalIgnoreCase));
			if (workflows.Any())
			{
				workflow = workflows.First();
				if (!workflow.IsActivated)
				{
					workflow.IsActivated = true;
					_workflowService.Update(workflow);
				}
			}
			else
			{
				workflow = new Workflow()
				{
					IsActivated = true,
					WorkflowName = workflowName,
					CreatedByUserId = User.GetUserId(),
					CreatedOnDate = DateTime.Now,
					ExpireProcess = 240,
					LastModifiedByUserId = User.GetUserId(),
					LastModifiedOnDate = DateTime.Now,
					Json = @"{""ID"":""210"",""NAME"":""Quy trình chuẩn hồ sơ một cửa"",""TYPE"":0,""ISACTIVATE"":false,""NODE"":[{""ID"":1,""NAME"":""Tiếp nhận"",""START"":true,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":120.0,""RIGHT"":280.0,""TOP"":55.0,""BOTTOM"":125.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""b06def57-5cf7-47c5-a40d-8e9f93b50bfb"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":1,""STARTLEFT"":200.0,""STARTTOP"":90.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""50c466b3-d390-4dc4-9d2a-5b0bf2a4f4d7"",""NAME"":""Chuyển cán bộ Tiếp nhận khác"",""NEXT"":1,""CURRENT"":1,""STARTLEFT"":200.0,""STARTTOP"":90.0,""ENDLEFT"":263.0,""ENDTOP"":116.0,""TYPE"":""Circle"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":18,""Type"":""TheoViTri""}]}]},{""ID"":2,""NAME"":""Thụ lý"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":true,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":577.0,""RIGHT"":737.0,""TOP"":56.0,""BOTTOM"":126.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""0cde30e1-abc3-435b-a3da-79dce42cdde3"",""NAME"":""Chuyển Lãnh đạo Phòng duyệt"",""NEXT"":3,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""a2818c74-e1be-4633-a01a-217bf8b70661"",""NAME"":""Chuyển Bộ phận Bổ sung"",""NEXT"":5,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":197.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""44a5fb43-f54d-4001-b893-700bfc363426"",""NAME"":""Chuyển cán bộ Tiếp nhận"",""NEXT"":1,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":200.0,""ENDTOP"":90.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""2965ce01-6502-408c-83e1-3001714acdef"",""NAME"":""Chuyển Liên thông"",""NEXT"":7,""CURRENT"":2,""STARTLEFT"":657.0,""STARTTOP"":91.0,""ENDLEFT"":1004.0,""ENDTOP"":284.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":19,""Type"":""TheoViTri""}]}]},{""ID"":3,""NAME"":""Lãnh đạo phòng"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":true,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":572.0,""RIGHT"":732.0,""TOP"":248.0,""BOTTOM"":318.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""18677ce1-8b4c-4381-bf87-3c87ba2a276e"",""NAME"":""Chuyển Lãnh đạo Sở duyệt"",""NEXT"":4,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":653.0,""ENDTOP"":455.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""44192b73-7a1f-4130-bdd3-0cc15f20af15"",""NAME"":""Chuyển Bộ phận Bổ sung"",""NEXT"":5,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":197.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""e6c3db58-5299-4854-b65b-2a9feff955fc"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""7f698c85-7848-4956-97d9-db0ce955c391"",""NAME"":""Chuyển Liên thông"",""NEXT"":7,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":1004.0,""ENDTOP"":284.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""de3aca03-5416-4f12-b44b-283c120f98fb"",""NAME"":""Chuyển Lãnh đạo Phòng khác"",""NEXT"":3,""CURRENT"":3,""STARTLEFT"":652.0,""STARTTOP"":283.0,""ENDLEFT"":711.0,""ENDTOP"":285.0,""TYPE"":""Circle"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":20,""Type"":""TheoViTri""}]}]},{""ID"":4,""NAME"":""Lãnh đạo Sở"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":true,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":573.0,""RIGHT"":733.0,""TOP"":420.0,""BOTTOM"":490.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""43f484ba-7471-40a5-8ae5-ccc3947733ae"",""NAME"":""Chuyển Trả kết Quả"",""NEXT"":6,""CURRENT"":4,""STARTLEFT"":653.0,""STARTTOP"":455.0,""ENDLEFT"":198.0,""ENDTOP"":453.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""0d1b194b-f383-4459-bb77-17d3cee83ac5"",""NAME"":""Chuyển Lãnh đạo Phòng"",""NEXT"":3,""CURRENT"":4,""STARTLEFT"":653.0,""STARTTOP"":455.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""18cc7e0d-fe0f-49e5-8163-a293b7ea0956"",""NAME"":""Chuyển Lãnh đạo Sở khác"",""NEXT"":4,""CURRENT"":4,""STARTLEFT"":653.0,""STARTTOP"":455.0,""ENDLEFT"":728.0,""ENDTOP"":451.0,""TYPE"":""Circle"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViHienTai"",""DepId"":409,""PositionId"":21,""Type"":""TheoViTri""}]}]},{""ID"":5,""NAME"":""Bổ sung"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":true,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":117.0,""RIGHT"":277.0,""TOP"":248.0,""BOTTOM"":318.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""04f82af0-b369-4a4c-82be-b84eacb9a7ce"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":5,""STARTLEFT"":197.0,""STARTTOP"":283.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""bfc305d9-b676-4045-8a38-672c5eb6d219"",""NAME"":""Chuyển Lãnh đạo Phòng"",""NEXT"":3,""CURRENT"":5,""STARTLEFT"":197.0,""STARTTOP"":283.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""9fb37f66-4536-49d8-9d1e-7d2e1d775c87"",""NAME"":""Chuyển Trả kết quả"",""NEXT"":6,""CURRENT"":5,""STARTLEFT"":197.0,""STARTTOP"":283.0,""ENDLEFT"":198.0,""ENDTOP"":453.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":22,""Type"":""TheoViTri""}]}]},{""ID"":6,""NAME"":""Trả kết quả"",""START"":false,""CLOSE"":true,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":true,""DUNGXULY"":false,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":118.0,""RIGHT"":278.0,""TOP"":418.0,""BOTTOM"":488.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""84681680-fc9a-4c04-9b54-09ca165088b7"",""NAME"":""Chuyển Lãnh đạo Sở"",""NEXT"":4,""CURRENT"":6,""STARTLEFT"":198.0,""STARTTOP"":453.0,""ENDLEFT"":653.0,""ENDTOP"":455.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""d5eb4ea1-9f00-4681-b4aa-a1c458ddc0b3"",""NAME"":""Chuyển Bộ phận Bổ sung"",""NEXT"":5,""CURRENT"":6,""STARTLEFT"":198.0,""STARTTOP"":453.0,""ENDLEFT"":197.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":23,""Type"":""TheoViTri""}]}]},{""ID"":7,""NAME"":""Liên thông"",""START"":false,""CLOSE"":false,""KYNHAY"":false,""KYCHINHTHUC"":false,""DONGDAU"":false,""CHANGECONTENT"":false,""CHANGETYPE"":false,""LUU"":false,""KYDUYET"":false,""CAPNHATKETQUA"":false,""TRAKETQUA"":false,""DUNGXULY"":true,""YEUCAUBOSUNG"":false,""TIEPNHANBOSUNG"":false,""TIMEINNODE"":24,""YKIEN"":0,""LEFT"":924.0,""RIGHT"":1084.0,""TOP"":249.0,""BOTTOM"":319.0,""WIDTH"":160.0,""HEIGHT"":70.0,""ACTION"":[{""ID"":""15152f97-e3bd-4dbd-ab1a-3b5f81a88ced"",""NAME"":""Chuyển Lãnh đạo Phòng"",""NEXT"":3,""CURRENT"":7,""STARTLEFT"":1004.0,""STARTTOP"":284.0,""ENDLEFT"":652.0,""ENDTOP"":283.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false},{""ID"":""a2a21e76-caea-46d3-af63-b41cccde6f3e"",""NAME"":""Chuyển Thụ lý"",""NEXT"":2,""CURRENT"":7,""STARTLEFT"":1004.0,""STARTTOP"":284.0,""ENDLEFT"":657.0,""ENDTOP"":91.0,""TYPE"":""Turnaround"",""ISALLOWSIGN"":false}],""ADDRESS"":[{""NODE_FROM"":0,""NODE_NAME_FROM"":""Bất kỳ"",""POSITION_QUERIES"":[{""Position"":""DonViTrucThuoc"",""DepId"":409,""PositionId"":19,""Type"":""TheoViTri""},{""Position"":""DonViHienTai"",""DepId"":409,""PositionId"":24,""Type"":""TheoViTri""}]}]}]}"
				};
				_workflowService.Create(workflow);
			}

			return workflow;
		}

		private DocField EnsureDocField(string docfieldName)
		{
			if (string.IsNullOrWhiteSpace(docfieldName))
			{
				return null;
			}

			DocField result;

			// Khi thêm mới đã set trùng tên, nhưng cứ kiểm tra cho chắc
			var docfields = _docFieldService.Gets(d => d.DocFieldName.Equals(docfieldName, StringComparison.OrdinalIgnoreCase));
			if (!docfields.Any())
			{
				result = new DocField()
				{
					DocFieldName = docfieldName,
					CreatedByUserId = User.GetUserId(),
					CreatedOnDate = DateTime.Now,
					IsActivated = true,
					LastModifiedOnDate = DateTime.Now,
					LastModifiedByUserId = User.GetUserId(),
					CategoryBusinessId = 4
				};

				_docFieldService.Create(result);
			}
			else
			{
				result = docfields.First();
				if (!result.IsActivated)
				{
					result.IsActivated = true;
					_docFieldService.Update(result, result.DocFieldName);
				}
			}

			return result;
		}

		#endregion
	}

    public class DocTypePlusImport
    {
        public string caphanhchinh { get; set; }
        public string doctypename { get; set; }
        public string docfieldname { get; set; }
        public string level { get; set; }
        public string formcode { get; set; }
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string configForm { get; set; }
    }

}