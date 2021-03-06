using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public partial class DocTypeController : CustomController
    {
        public ActionResult Survey()
        {
            return View("../DocType/IndexSurvey",
                new DocTypeModel { IsActivated = true, IsAllowOnline = true, CategoryId = 5, ActionLevel = 1 });
        }
        public ActionResult CreateSurvey()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = 32 });
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetListSurvey(Guid docTypeId, int page = 1, int pageSize = 10, string search = "",bool checkLoad = false)
        {
            try
            {
                var docTypes = string.IsNullOrWhiteSpace(search) ? _docTypeService.Gets(c => c.CategoryBusinessId == 32).OrderBy(c => c.CreatedOnDate).Select(s => new SurveyList { DocTypeId = s.DocTypeId, DocTypeName = s.DocTypeName, Active = s.DocTypeId == docTypeId }) : _docTypeService.Gets(c => c.CategoryBusinessId == 32 && c.DocTypeName.Contains(search)).OrderBy(c => c.CreatedOnDate).Select(s => new SurveyList { DocTypeId = s.DocTypeId, DocTypeName = s.DocTypeName, Active = s.DocTypeId == docTypeId });
                var surveyLists = docTypes as SurveyList[] ?? docTypes.ToArray();
                var index = Array.FindIndex(surveyLists, c => c.DocTypeId == docTypeId);
                page = index < 0 || checkLoad ? page : (int) Math.Ceiling((int) index / (decimal) pageSize);
                var pager = new Pager(surveyLists.Length, page, pageSize);
                var model = new Surveys
                {
                    Items = surveyLists.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                    Pager = pager
                };
                return Json(new { success = true, html = this.RenderPartialView("_PartialSurveyList", model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetSurvey(Guid docTypeId)
        {
            try
            {
                var doc = _docTypeService.Get(docTypeId);
                return doc == null ? Json(new { success = true, data = doc, html = this.RenderPartialView("_PartialSurvey", new DocTypeModel { IsActivated = true, IsAllowOnline = true, CategoryId = 5, ActionLevel = 1, CategoryBusinessId = 32 }) }, JsonRequestBehavior.AllowGet) : Json(new { success = true, data = doc, html = this.RenderPartialView("_PartialSurvey", doc.ToModel()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { success = false, mess = "Đã có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateOrEditSurvey(DocTypeModel model)
        {
            var mess = "";
            Guid docTypeId;
            try
            {
                if (string.IsNullOrWhiteSpace(model.DocTypeCode))
                    ModelState.AddModelError("DocTypeCode", "Mã phiếu khảo sát chưa được nhập");
                if (string.IsNullOrWhiteSpace(model.DocTypeName))
                    ModelState.AddModelError("DocTypeName", "Mã phiếu khảo sát chưa được nhập");
                if (model.DocTypeId == default(Guid))
                {
                    model.CategoryBusinessId = 32;
                    if (_docTypeService.Exist(DocTypeQuery.WithDocTypeName(model.DocTypeName)))
                        ModelState.AddModelError("DocTypeName", "Tên phiếu khảo sát đã tồn tại");
                    if (_docTypeService.Exist(DocTypeQuery.WithDocTypeCode(model.DocTypeCode)))
                        ModelState.AddModelError("DocTypeCode", "Mã phiếu khảo sát đã tồn tại");
                    if (!ModelState.IsValid)
                        return Json(new { success = false, html = this.RenderPartialView("_PartialSurvey", model) },
                            JsonRequestBehavior.AllowGet);
                    model.CompendiumDefault = model.DocTypeName;
                    if (model.SurveyReport != null) {
                        model.SurveyReport = model.SurveyReport.Replace("&quot;", "'");
                    }
                    var docTypeCreate = model.ToEntity();
                    docTypeCreate.CreatedByUserId = User.GetUserId();
                    docTypeCreate.CreatedOnDate = DateTime.Now;
                    docTypeCreate.LastModifiedOnDate = DateTime.Now;
                    docTypeCreate.DocTypePermission = 0;
                    docTypeCreate.TotalRegisted = 1;//eGovOnline
                    docTypeCreate.TotalViewed = 1;//eGovOnline
                    docTypeCreate.Unsigned = ConverToUnsign(model.DocTypeName);//eGovOnline
                    _docTypeService.Create(docTypeCreate);
                    docTypeId = docTypeCreate.DocTypeId;
                    model = docTypeCreate.ToModel();
                    mess = "Thêm mới thành công!";
                }
                else
                {
                    if (!ModelState.IsValid)
                        return Json(new { success = false, html = this.RenderPartialView("_PartialSurvey", model) },
                            JsonRequestBehavior.AllowGet);
                    var docType = _docTypeService.Get(model.DocTypeId);
                    if (docType == null)
                        return Json(new { success = false, mess = "Phiếu khảo sát không tồn tại!" }, JsonRequestBehavior.AllowGet);
                    var oldDocTypeName = docType.DocTypeName;
                    var oldDocTypeCode = docType.DocTypeCode;
                    docType.CompendiumDefault = model.DocTypeName;
                    docType.LastModifiedByUserId = User.GetUserId();
                    docType.LastModifiedOnDate = DateTime.Now;
                    model.Unsigned = ConverToUnsign(model.DocTypeName);
                    model.DocTypePermission = 0;
                    model.TotalViewed = 1;
                    model.TotalRegisted = 1;
                    model.DocTypeLaws = docType.DocTypeLaws;
                    model.SurveyReport = model.SurveyReport.Replace("&quot;", "'");
                    docType = model.ToEntity(docType);
                    _docTypeService.Update(docType, oldDocTypeName, oldDocTypeCode);
                    docTypeId = docType.DocTypeId;
                    mess = "Chỉnh sửa thành công!";
                    model = docType.ToModel();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { success = false, mess = "Đã có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);
            }
            ModelState.Clear();
            return Json(new { success = true, mess, docTypeId, html = this.RenderPartialView("_PartialSurvey", model) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSurvey(Guid id)
        {
            try
            {
                var docType = _docTypeService.Get(id);
                if (docType == null)
                    return Json(new { success = false, mess = "Phiếu khảo sát không tồn tại!" }, JsonRequestBehavior.AllowGet);
                _docTypeService.Delete(docType);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { success = false, mess = "Đã có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, mess = "Xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditSurvey()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermissionCreate"));
                return RedirectToAction("Index", new { categoryBusinessId = 32 });
            }
            return View();
        }

        public ActionResult FindSurveyWorkflow(Guid doctypeId, DocTypeWorkflowSearchModel search)
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
            return PartialView("_PartialContentWorkflow", allWorkflows);
        }

        public ActionResult AddSurveyWorkflow(Guid id)
        {
            var docTypeWorkflowIds = _docTypeService.GetWorkFlows(p => p.WorkflowId, p => p.DocTypeId == id);
            Expression<Func<Workflow, bool>> spec = null;
            var typeWorkflowIds = docTypeWorkflowIds as int[] ?? docTypeWorkflowIds.ToArray();
            if (typeWorkflowIds.Any())
            {
                spec = p => !typeWorkflowIds.Contains(p.WorkflowId);
            }
            var notExist = _workflowService.GetsAs(p => new WorkflowModel
            {
                WorkflowId = p.WorkflowId,
                WorkflowName = p.WorkflowName,
                IsActivated = p.IsActivated
            }, spec);

            ViewBag.DoctypeId = id;
            ViewBag.AllWorkflows = notExist;
            return PartialView("AddSurveyWorkflow", notExist);
        }

        [HttpPost]
        public JsonResult UpdateSurveyWorkflow(Guid id, List<int> workflowIds)
        {
            if (workflowIds == null || !workflowIds.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.UpdateSurveyWorkflow.Success"));
                return Json(new { success = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Success") });
            }
            try
            {
                var survey = _docTypeService.Get(id);
                if (survey == null)
                    return Json(new { success = false, mess = "Phiếu khảo sát không tồn tại!" }, JsonRequestBehavior.AllowGet);
                _docTypeService.UpdateWorkflows(id, workflowIds);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.UpdateSurveyWorkflow.Success"));
                var model = GetFlow(id);
                ViewData["DoctypeId"] = id;
                return Json(new { success = true, mess = _resourceService.GetResource("Admin.UpdateSurveyWorkflow.Success"), html = this.RenderPartialView("_PartialSurveyWorkflow", model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error"));
                return Json(new { error = _resourceService.GetResource("Customer.DocType.UpdateDocTypeWorkflow.Error") });
            }
        }

        [HttpPost]
        public JsonResult SurveyWorkflow(Guid id)
        {
            try
            {
                var survey = _docTypeService.Get(id);
                if (survey == null)
                    return Json(new { success = false, mess = "Phiếu khảo sát không tồn tại!" }, JsonRequestBehavior.AllowGet);
                var model = GetFlow(id);
                ViewData["DoctypeId"] = id;
                return Json(new { success = true, html = this.RenderPartialView("_PartialSurveyWorkflow", model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { success = false, mess = "Đã có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult ChangeSurveyWorkflowStatus(Guid docTypeId, int workflowId, bool status)
        {
            try
            {
                var survey = _docTypeService.Get(docTypeId);
                if (survey == null)
                    return Json(new { success = false, mess = "Phiếu khảo sát không tồn tại!" }, JsonRequestBehavior.AllowGet);
                _docTypeService.ChangeActivatedWorkflows(docTypeId, workflowId, status);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Success"));
                var model = GetFlow(docTypeId);
                ViewData["DoctypeId"] = docTypeId;
                return Json(new { success = true, mess = _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Success"), html = this.RenderPartialView("_PartialSurveyWorkflow", model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Error"));
                return Json(new { success = false, mess = _resourceService.GetResource("Customer.DocType.ChangeDocTypeWorkflowStatus.Error") });
            }
        }

        [HttpPost]
        public JsonResult DeleteSurveyWorkflow(Guid id, int workflowId)
        {
            try
            {
                var survey = _docTypeService.Get(id);
                if (survey == null)
                    return Json(new { success = false, mess = "Phiếu khảo sát không tồn tại!" }, JsonRequestBehavior.AllowGet);
                _docTypeService.DeleteWorkflows(id, new List<int> { workflowId });
                CreateActivityLog(ActivityLogType.Admin, "Xóa workflow survey thành công!");
                var model = GetFlow(id);
                ViewData["DoctypeId"] = id;
                return Json(new { success = true, mess = "Xóa thành công!", html = this.RenderPartialView("_PartialSurveyWorkflow", model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { success = false, mess = "Đã có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);
            }
        }

        private IEnumerable<WorkflowModel> GetFlow(Guid id)
        {
            return _workflowService.Raw.Join(_docFieldService.RawDocfieldDoctypeWorkflow, p => p.WorkflowId, x => x.WorkflowId,
                    (p, x) =>
                        new { Workflow = p, DocfieldDoctypeWorkflow = x })
                .Where(p => p.DocfieldDoctypeWorkflow.DocTypeId == id)
                .Select(p => new WorkflowModel
                {
                    WorkflowId = p.Workflow.WorkflowId,
                    WorkflowName = p.Workflow.WorkflowName,
                    IsActivated = p.DocfieldDoctypeWorkflow.IsActivated
                });
        }
    }
}
