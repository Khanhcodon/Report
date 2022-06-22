using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class OfficeController : CustomController
    {
        private readonly ApiBll _apiService;
        private readonly OfficeBll _officeService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly LevelBll _levelService;
        private const string DefaultSortBy = "OfficeName";

        public OfficeController(OfficeBll officeService,
                            ResourceBll resourceService,
                                 AdminGeneralSettings generalSettings,
                                LevelBll levelService,
                                 ApiBll apiService)
        {
            _apiService = apiService;
            _officeService = officeService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _levelService = levelService;
        }

        //
        // GET: /Admin/Office/
        public ActionResult Index()
        {
            LoadDropDownList();
            var levelId = string.Empty;
            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DefaultSortBy
            };

            int totalRecords;
            var model = _officeService.GetOffices(out totalRecords, pageSize: sortAndPage.PageSize,
                                                            sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                                                            officeName: search, currentPage: sortAndPage.CurrentPage, levelId: levelId).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.OfficeName = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.LevelId = levelId;
            //duyệt vòng lặp để lấy giá trị ParentName 
            foreach (var item in model)
            {
                if (item.ParentId != null)
                {
                    int parentId = Convert.ToInt16(item.ParentId);
                    item.ParentName = _officeService.Get(parentId).OfficeName.ToString();
                }
                else
                {
                    item.ParentName = _resourceService.GetResource("Common.Office.Message.NotParent");
                }
                item.LevelName = _resourceService.GetEnumDescription<LevelType>((LevelType)item.LevelId);
            }
            return View(model);
        }

        public ActionResult Search(string officeName, int pageSize, string levelId)
        {
            if (string.IsNullOrWhiteSpace(levelId))
            {
                LoadDropDownList();
            }
            else
            {
                LoadDropDownList(0, 0, Convert.ToInt32(levelId));
            }
            IEnumerable<OfficeModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(officeName))
                    {
                        officeName = officeName.Trim();
                    }
                    int totalRecords;
                    model = _officeService.GetOffices(out totalRecords, pageSize: pageSize,
                                                                sortBy: DefaultSortBy, isDescending: false,
                                                                officeName: officeName, levelId: levelId).ToListModel();
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DefaultSortBy,
                        TotalRecordCount = totalRecords
                    };
                    ViewBag.LevelId = levelId;
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.OfficeName = officeName;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                    foreach (var item in model)
                    {
                        item.LevelName = _resourceService.GetEnumDescription<LevelType>((LevelType)item.LevelId);
                        if (item.ParentId != null)
                        {
                            var parentId = Convert.ToInt16(item.ParentId);
                            item.ParentName = _officeService.Get(parentId).OfficeName.ToString();
                        }
                        else
                        {
                            item.ParentName = _resourceService.GetResource("Common.Office.Message.NotParent");
                        }
                    }
                }
            }
            return PartialView("_OfficeList", model);
        }

        public ActionResult Create()
        {
            LoadDropDownList();
            return View(new OfficeModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OfficeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    _officeService.Create(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.CreateOrEdit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.CreateOrEdit.Success"));
                    LoadDropDownList();
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            else
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.CreateOrEdit.Fail"));
                ErrorNotification(_resourceService.GetResource("Common.CreateOrEdit.Fail"));
            }

            LoadDropDownList();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var office = _officeService.Get(id);
            if (office == null)
            {
                return RedirectToAction("Index");
            }
            var model = office.ToModel();
            LoadDropDownList(model.ParentId, model.OfficeId, model.LevelId);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OfficeModel model)
        {
            //  var level = _levelService.Get(model.LevelId);
            var office = _officeService.Get(model.OfficeId);
            if (ModelState.IsValid)
            {
                if (office == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Office.Message.DeleteBeforeEdit.Exist"));
                    ErrorNotification(_resourceService.GetResource("Common.Office.Message.DeleteBeforeEdit.Exist"));
                }
                var oldOfficeName = office.OfficeName;
                try
                {
                    office = model.ToEntity(office);
                    _officeService.Update(office, oldOfficeName);
                    //Kiêm tra boolean Isme 
                    if (office.IsMe == true)
                    {
                        var officeByIsMe = _officeService.GetByIsMe(office.IsMe, office.OfficeId);
                        if (officeByIsMe != null)
                        {
                            // gán giá trị false cho isMe trong object được tìm thấy 
                            officeByIsMe.IsMe = false;
                            _officeService.Update(officeByIsMe, officeByIsMe.OfficeName);
                        }
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Office.Message.Edit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.Office.Message.Edit.Success"));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    LoadDropDownList(office.ParentId, office.LevelId, office.OfficeId);
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            else
            {
                LoadDropDownList(office.ParentId, office.LevelId, office.OfficeId);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var office = _officeService.Get(id);
            if (office == null)
            {

            }
            else
            {
                _officeService.Delete(office);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Office.Message.Delete.Success"));
                SuccessNotification(_resourceService.GetResource("Common.Office.Message.Delete.Success"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(string officeName,
                                                string sortBy,
                                                bool isSortDesc,
                                                int page,
                                                int pageSize,
                                                string levelId)
        {
            if (string.IsNullOrWhiteSpace(levelId))
            {
                LoadDropDownList();
            }
            else
            {
                LoadDropDownList(0, 0, Convert.ToInt32(levelId));
            }
            IEnumerable<OfficeModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(officeName))
                {
                    officeName = officeName.Trim();
                }
                int totalRecords;
                model = _officeService.GetOffices(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            officeName: officeName, currentPage: page, levelId: levelId).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                ViewBag.LevelId = levelId;
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.OfficeName = officeName;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                foreach (var item in model)
                {
                    item.LevelName = _resourceService.GetEnumDescription<LevelType>((LevelType)item.LevelId);
                    if (item.ParentId != null)
                    {
                        int parentId = Convert.ToInt16(item.ParentId);
                        item.ParentName = _officeService.Get(parentId).OfficeName.ToString();
                    }
                    else
                    {
                        item.ParentName = _resourceService.GetResource("Common.Office.Message.NotParent");
                    }
                }
            }
            return PartialView("_OfficeList", model);
        }

        private void LoadDropDownList(int? parentId = 0, int? officeId = 0, int? levelId = 0)
        {
            ViewBag.ListParent = new SelectList(_officeService.GetOffices().Where(o => o.OfficeId != officeId), "OfficeId", "OfficeName", parentId);
            ViewBag.ListLevel = _resourceService.EnumToSelectList<LevelType>(levelId);
        }
    }
}

