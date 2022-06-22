using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class BussinessDocFieldDocTypeGroupController : CustomController
    {
        private readonly BussinessDocFieldDocTypeGroupBll _bussinessDocFieldDocTypeGroupService;
        private readonly DocFieldBll _docFieldService;
        private readonly ResourceBll _resourceService;
        private readonly DocTypeBll _docTypeService;
        private readonly CodeBll _codeService;
        private readonly IncreaseBll _increaseService;
        private readonly AdminGeneralSettings _generalSettings;

        private const string DEFAULT_SORT_BY = "IsActived";

        public BussinessDocFieldDocTypeGroupController(
            BussinessDocFieldDocTypeGroupBll bussinessDocFieldDocTypeGroupService,
            ResourceBll resourceService,
            DocFieldBll docFieldService,
            DocTypeBll docTypeService,
            AdminGeneralSettings generalSettings,
            CodeBll codeService,
            IncreaseBll increaseService)
        {
            _bussinessDocFieldDocTypeGroupService = bussinessDocFieldDocTypeGroupService;
            _docFieldService = docFieldService;
            _resourceService = resourceService;
            _docTypeService = docTypeService;
            _generalSettings = generalSettings;
            _codeService = codeService;
            _increaseService = increaseService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            int totalRecords;
            var model = _bussinessDocFieldDocTypeGroupService.Gets(out totalRecords,
                t => t,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            BindData();
            return View(model);
        }

        public ActionResult Search(BussinessDocFieldDocTypeGroupSearchModel search, int pageSize)
        {
            IEnumerable<BussinessDocFieldDocTypeGroupModel> model = null;
            if (Request.IsAjaxRequest())
            {
                Expression<Func<BussinessDocFieldDocTypeGroup, bool>> spec;
                spec = p => (!search.CategoryBusinessId.HasValue
                          || (search.CategoryBusinessId.HasValue && p.CategoryBusinessId == search.CategoryBusinessId.Value))
                          && (!search.DocFieldId.HasValue
                          || (search.DocFieldId.HasValue && p.DocFieldId == search.DocFieldId.Value))
                          && (string.IsNullOrEmpty(search.DocTypeId)
                          || (!string.IsNullOrEmpty(search.DocTypeId) && p.DocTypeId == search.DocTypeId))
                           && (string.IsNullOrEmpty(search.Name)
                          || (!string.IsNullOrEmpty(search.Name) && p.Name.Contains(search.Name)));

                int totalRecords;
                model = _bussinessDocFieldDocTypeGroupService.Gets(out totalRecords,
                    t => t,
                    spec: spec,
                    pageSize: pageSize,
                    sortBy: DEFAULT_SORT_BY,
                    isDescending: true).ToListModel();

                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = 1,
                    IsSortDescending = true,
                    SortBy = DEFAULT_SORT_BY,
                    TotalRecordCount = totalRecords
                };

                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }
            BindData();
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
           BussinessDocFieldDocTypeGroupSearchModel search,
            string sortBy, bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<BussinessDocFieldDocTypeGroupModel> model = null;
            if (Request.IsAjaxRequest())
            {
                Expression<Func<BussinessDocFieldDocTypeGroup, bool>> spec;
                spec = p => (!search.CategoryBusinessId.HasValue
                    || (search.CategoryBusinessId.HasValue && p.CategoryBusinessId == search.CategoryBusinessId.Value))
                    && (!search.DocFieldId.HasValue
                    || (search.DocFieldId.HasValue && p.DocFieldId == search.DocFieldId.Value))
                    && (string.IsNullOrEmpty(search.DocTypeId)
                    || (!string.IsNullOrEmpty(search.DocTypeId) && p.DocTypeId == search.DocTypeId))
                     && (string.IsNullOrEmpty(search.Name)
                    || (!string.IsNullOrEmpty(search.Name) && p.Name.Contains(search.Name)));

                int totalRecords;
                model = _bussinessDocFieldDocTypeGroupService.Gets(out totalRecords,
                    t => t,
                    spec: spec,
                    pageSize: pageSize,
                    currentPage: page,
                    sortBy: DEFAULT_SORT_BY,
                    isDescending: true).ToListModel();

                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = true,
                    SortBy = DEFAULT_SORT_BY,
                    TotalRecordCount = totalRecords
                };

                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            BindData();
            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            BindData();
            return View(new BussinessDocFieldDocTypeGroupModel());
        }

        [HttpPost]
        public ActionResult Create(BussinessDocFieldDocTypeGroupModel model)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    _bussinessDocFieldDocTypeGroupService.Create(entity);
                    SuccessNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Created.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Created.Error"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Created.Error"));
                }
            }
            BindData();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //    if (!HasPermission())
            //    {
            //        ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //        return RedirectToAction("Index", "Welcome");
            //    }

            var entity = _bussinessDocFieldDocTypeGroupService.Get(id);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.NotExist"));
                return RedirectToAction("Index");
            }

            BindData();
            var model = entity.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BussinessDocFieldDocTypeGroupModel model)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var entity = _bussinessDocFieldDocTypeGroupService.Get(model.BussinessDocFieldDocTypeGroupId);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.NotExist"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    entity = model.ToEntity(entity);
                    _bussinessDocFieldDocTypeGroupService.Update(entity);
                    SuccessNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Updated.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Updated.Error"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Updated.Error"));
                }
            }
            BindData();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var entity = _bussinessDocFieldDocTypeGroupService.Get(id);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                var codes = _codeService.GetsAs(p => p.BussinessDocFieldDocTypeGroupId, p => p.BussinessDocFieldDocTypeGroupId == id);
                if (codes != null && codes.Any())
                {
                    ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.IsUse"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.IsUse"));
                    return RedirectToAction("Index");
                }

                var increases = _increaseService.GetsAs(p => p.BussinessDocFieldDocTypeGroupId, p => p.BussinessDocFieldDocTypeGroupId == id);
                if (increases != null && increases.Any())
                {
                    ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.IsUse"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.IsUse"));
                    return RedirectToAction("Index");
                }

                //if (timer.IsRunning)
                //{
                //    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BussinessDocFieldDocTypeGroup.BussinessDocFieldDocTypeGroupIsRunning"));
                //    return RedirectToAction("Index");
                //}

                _bussinessDocFieldDocTypeGroupService.Delete(entity);
                SuccessNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Deleted.Success"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(_resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Deleted.Error"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BussinessDocFieldDocTypeGroup.Deleted.Error"));
            }

            return RedirectToAction("Index");
        }

        private void BindData()
        {
            ViewBag.AllBussiness = GetAllBussiness();

            ViewBag.AllDocField = _docFieldService.GetsAs(p => new
            {
                value = p.DocFieldId,
                label = p.DocFieldName,
                categoryBusinessId = p.CategoryBusinessId
            }, p => p.IsActivated).Stringify();

            ViewBag.AllDocType = _docTypeService.GetsAs(p => new
            {
                value = p.DocTypeId,
                label = p.DocTypeName,
                categoryBusinessId = p.CategoryBusinessId,
                docFieldId = p.DocFieldId
            }, p => p.IsActivated).Stringify();
        }

        private IEnumerable<SelectListItem> GetAllBussiness()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(CategoryBusinessTypes));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((CategoryBusinessTypes)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<CategoryBusinessTypes>((CategoryBusinessTypes)val),
                    Value = itemValue.ToString()
                });
            }
            return result;
        }
    }
}
