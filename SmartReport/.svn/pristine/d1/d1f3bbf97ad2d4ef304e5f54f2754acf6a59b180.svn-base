using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Core.Caching;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ProcessFunctionController : CustomController
    {
        private readonly ProcessFunctionBll _processFunctionService;
        private readonly DocFieldBll _docfieldService;
        private readonly DocTypeBll _doctypeService;
        private readonly ProcessFunctionTypeBll _processFunctionTypeService;
        private readonly ProcessFunctionGroupBll _processFunctionGroupService;
        private readonly ProcessFunctionFilterBll _processFunctionFilterService;
        private readonly ResourceBll _resourceService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;
        private readonly TreeGroupBll _treeGroupService;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly PermissionSettingBll _permissionSettingService;
        private readonly DocColumnSettingBll _docColumnSettingService;
        private readonly MemoryCacheManager _cacheManager;
        private readonly SettingBll _settingService;

        private VersionTreeSetting _versionTreeSettings;

        private const string DEFAULT_SORT_BY = "DocFieldId";

        public ProcessFunctionController(
            ProcessFunctionBll processFunctionService,
            ProcessFunctionTypeBll processFunctionTypeService,
            ResourceBll resourceService,
            DocFieldBll docfieldService,
            DocTypeBll doctypeService,
            DepartmentBll departmentService,
            PositionBll positionService,
            ProcessFunctionGroupBll processFunctionGroupService,
            ProcessFunctionFilterBll processFunctionFilterService,
            UserBll userService,
            TreeGroupBll treeGroupService,
            FileUploadSettings fileUploadSettings,
            PermissionSettingBll permissionSettingService,
            DocColumnSettingBll docColumnSettingService,
            MemoryCacheManager cacheManager,
            VersionTreeSetting versionTreeSettings,
            SettingBll settingService
            )
            : base()
        {
            _processFunctionService = processFunctionService;
            _processFunctionTypeService = processFunctionTypeService;
            _resourceService = resourceService;
            _docfieldService = docfieldService;
            _doctypeService = doctypeService;
            _departmentService = departmentService;
            _positionService = positionService;
            _userService = userService;
            _processFunctionGroupService = processFunctionGroupService;
            _processFunctionFilterService = processFunctionFilterService;
            _treeGroupService = treeGroupService;
            _fileUploadSettings = fileUploadSettings;
            _permissionSettingService = permissionSettingService;
            _docColumnSettingService = docColumnSettingService;
            _cacheManager = cacheManager;
            _versionTreeSettings = versionTreeSettings;
            _settingService = settingService;
        }

        #region Process Function Tree

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var user = _userService.CurrentUser;
            var allTreeGroups = _treeGroupService.GetCacheAllTreeGroups(isActived: true);
            if (allTreeGroups == null || !allTreeGroups.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotConfigTreeGroup"));
                ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotConfigTreeGroup"));
                return RedirectToAction("Create", "TreeGroup");
            }

            ViewBag.AllTreeGroups = allTreeGroups.OrderBy(p => p.Order)
                          .Select(p => new
                          {
                              id = p.TreeGroupId,
                              name = p.IsShowUserFullName ? user.FullName : p.TreeGroupName,
                              isActivated = p.IsActived,
                              isleaf = true,
                              order = p.Order,
                              hasChildrenContextMenu = p.HasChildrenContextMenuAdmin
                          }).Stringify();

            ViewBag.AllFunctions = _processFunctionService.GetsForTree();

            return View();
        }

        public ActionResult Create(int id, int? treeGroupId = null)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            int type = 0;
            ViewBag.AllTypes = GetAllFunctionType();
            ViewBag.AllGroups = GetAllFunctionGroup();
            ViewBag.TreeGroups = GetTreeGroups(treeGroupId);
            ViewBag.AllPermissionSettings = GetAllPermissionSettings();
            ViewBag.AllDocColumnSettings = GetAllDocColumnSettings();
            ViewBag.ShowTotalInTreeTypeList = GetShowTotalInTreeType();
            //ViewBag.SortTypes = GetSortTypes();

            var function = _processFunctionService.GetFromCache(id);
            if (function != null)
            {
                type = function.Type;
            }

            ViewBag.Type = type;
            return PartialView("_CreateOrEditFunction", new ProcessFunctionModel
            {
                ParentId = id,
                IsActivated = true,
                TreeGroupId = treeGroupId.HasValue ? treeGroupId.Value : 0
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionCreate")]
        public JsonResult Create(ProcessFunctionModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermissionCreate"));
                return Json(new
                {
                    error = true,
                    message = _resourceService.GetResource("Customer.ProcessFunction.NotPermissionCreate")
                });
            }

            ViewBag.ShowTotalInTreeTypeList = GetShowTotalInTreeType();
            //ViewBag.SortTypes = GetSortTypes();
            ViewBag.TreeGroups = GetTreeGroups();
            ViewBag.AllPermissionSettings = GetAllPermissionSettings();
            ViewBag.AllDocColumnSettings = GetAllDocColumnSettings();

            if (ModelState.IsValid)
            {
                var funcGroup = _treeGroupService.Get(model.TreeGroupId);
                if (funcGroup == null)
                {
                    ModelState.AddModelError("TreeGroupId", _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Create.TreeGroup.NotFound"));
                    return Json(new
                    {
                        error = true,
                        message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Create.TreeGroup.NotFound")
                    });
                }

                //kiểm tra nếu  không là cây văn banr thì không cho tạo mới
                if (!funcGroup.IsDocumentTree)
                {
                    ModelState.AddModelError("TreeGroupId", _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Create.TreeGroup.NotSupportCreate"));
                    return Json(new
                    {
                        error = true,
                        message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Create.TreeGroup.NotSupportCreate")
                    });
                }

                model.Type = (int)ProcessFunctionTypes.VanBan;

                ////kiểm tra nếu  type la !=0 thì không cho tao j mới
                //if (model.Type != (int)ProcessFunctionTypes.VanBan)
                //{
                //    return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Create.Error") });
                //}

                if (model.ParentId == 0)
                {
                    model.ParentId = null;
                }

                #region
                //if (!string.IsNullOrWhiteSpace(model.Color))
                //{
                //    if (!model.Color.StartsWith("#"))
                //    {
                //        if (("#" + model.Color).IsMatchColorCodes())
                //        {
                //            model.Color = "#" + model.Color;
                //        }
                //    }
                //}

                #endregion

                var entity = model.ToEntity();
                var allOrder = _processFunctionService.GetsAs(t => t.Order, t => t.ParentId == model.ParentId);
                if (allOrder != null && allOrder.Any())
                {
                    entity.Order = allOrder.Max() + 1;
                }
                else
                {
                    entity.Order = 0;
                }

                _processFunctionService.Create(entity);

                return
                    Json(
                        new
                        {
                            functionType = "Create",
                            treeGroupId = entity.TreeGroupId,
                            item =
                            new
                            {
                                id = entity.ProcessFunctionId,
                                name = entity.Name,
                                parentid = entity.ParentId,
                                icon = entity.Icon,
                                isfilter = entity.ProcessFunctionType != null &&
                                            entity.ProcessFunctionType.ListParam.Any(
                                              p => p.ValueField == DEFAULT_SORT_BY),
                                isActivated = entity.IsActivated,
                                color = entity.Color
                            }
                        });
            }

            return null;
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            ViewBag.AllTypes = GetAllFunctionType();
            ViewBag.AllGroups = GetAllFunctionGroup();
            ViewBag.ShowTotalInTreeTypeList = GetShowTotalInTreeType();
            //ViewBag.SortTypes = GetSortTypes();
            ViewBag.TreeGroups = GetTreeGroups();
            ViewBag.AllPermissionSettings = GetAllPermissionSettings();
            ViewBag.AllDocColumnSettings = GetAllDocColumnSettings();

            var filterIds = _processFunctionService.GetProcessFunctionAndFilterAs(p =>
                p.ProcessFunctionFilterId,
                p => p.ProcessFunctionId == id);
            var allFilters = _processFunctionFilterService.GetsAs(p => new
            {
                name = p.Name,
                id = p.ProcessFunctionFilterId,
                functionId = id
            },
            p => filterIds.Contains(p.ProcessFunctionFilterId));

            ViewBag.AllFilters = allFilters.Stringify();

            if (id == 0)
            {
                return PartialView("_CreateOrEditFunction", new ProcessFunctionModel());
            }

            var function = _processFunctionService.GetFromCache(id);
            if (function == null)
            {
                return PartialView("_CreateOrEditFunction", new ProcessFunctionModel());
            }

            ViewBag.Type = function.Type;
            var model = function.ToModel();
            //model.IsDateFilter = string.IsNullOrEmpty(model.DateFilter) ? false : true;
            model.IsFilterByDocFieldDocType = function.ProcessFunctionType != null &&
                                              function.ProcessFunctionType.ListParam.Any(
                                                  p => p.ValueField == DEFAULT_SORT_BY);
            return PartialView("_CreateOrEditFunction", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionEdit")]
        public JsonResult Edit(ProcessFunctionModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermissionUpdate"));
                return Json(new { error = true, message = _resourceService.GetResource("Customer.ProcessFunction.NotPermissionUpdate") });
            }

            ViewBag.ShowTotalInTreeTypeList = GetShowTotalInTreeType();
            //ViewBag.SortTypes = GetSortTypes();
            ViewBag.TreeGroups = GetTreeGroups();
            ViewBag.AllPermissionSettings = GetAllPermissionSettings();
            ViewBag.AllDocColumnSettings = GetAllDocColumnSettings();

            if (ModelState.IsValid)
            {
                var function = _processFunctionService.Get(model.ProcessFunctionId);
                if (function == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId"));
                    return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + model.ProcessFunctionId });
                }

                if (model.ParentId == 0)
                {
                    model.ParentId = null;
                }

                #region
                //if (!string.IsNullOrWhiteSpace(model.Color))
                //{
                //    if (!model.Color.StartsWith("#"))
                //    {
                //        // TODO: Đưa vào validateExtension.cs để quản lý tập trung các regex trên dự án.
                //        var regColor = new Regex(@"^\#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$");
                //        if (regColor.IsMatch("#" + model.Color))
                //        {
                //            model.Color = "#" + model.Color;
                //        }
                //    }
                //}
                #endregion

                var oldName = function.Name;
                var oldParent = function.ParentId;
                var oldType = function.Type;
                var oldTreeGroupId = function.TreeGroupId;
                var exportFileConfig = function.ExportFileConfig;

                function = model.ToEntity(function);
                function.ParentId = oldParent;
                function.Type = oldType;
                function.TreeGroupId = oldTreeGroupId;
                function.ExportFileConfig = exportFileConfig;

                if (oldType != (int)ProcessFunctionTypes.VanBan)
                {
                    function.IsActivated = true;
                }

                _processFunctionService.Update(function);
                return
                    Json(
                        new
                        {
                            functionType = "Update",
                            treeGroupId = function.TreeGroupId,
                            item = new
                            {
                                id = function.ProcessFunctionId,
                                name = function.Name,
                                oldname = oldName,
                                parentid = function.ParentId,
                                icon = function.Icon,
                                isfilter = function.ProcessFunctionType != null && function.ProcessFunctionType.ListParam.Any(p => p.ValueField == DEFAULT_SORT_BY),
                                isActivated = function.IsActivated,
                                color = function.Color
                            }
                        });
            }

            return null;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionPaste")]
        public JsonResult Paste(int id, int parentId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermissionpPaste"));
                return Json(new { error = true, message = _resourceService.GetResource("Customer.ProcessFunction.NotPermissionpPaste") });
            }

            var function = _processFunctionService.Get(id);
            if (function == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId"));
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + id });
            }

            //Kiêm tra  nếu type của function !=0 thì không cho copy
            if (function.Type != (int)ProcessFunctionTypes.VanBan)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotCopy"));
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotCopy") + id });
            }

            if (parentId > 0)
            {
                var parentFunction = _processFunctionService.Get(parentId);
                if (parentFunction == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindParentId"));
                    return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindParentId") + parentId });
                }
            }

            var now = DateTime.Now;
            var newFunction = new ProcessFunction
            {
                Color = function.Color,
                Icon = function.Icon,
                IsActivated = function.IsActivated,
                IsEnablePaging = function.IsEnablePaging,
                Name = function.Name,
                ParentId = parentId > 0 ? parentId : (int?)null,
                ProcessFunctionTypeId = function.ProcessFunctionTypeId,
                QueryLatest = function.QueryLatest,
                QueryOlder = function.QueryOlder,
                QueryItemRemove = function.QueryItemRemove,
                QueryCountItemUnread = function.QueryCountItemUnread,
                QueryPaging = function.QueryPaging,
                QueryCountAllItems = function.QueryCountAllItems,
                DateFilter = function.DateFilter,
                DateFilterView = function.DateFilterView,
                IsDateFilter = function.IsDateFilter,
                IsOverdueFilter = function.IsOverdueFilter,
                DateModified = now,
                ShowTotalInTreeType = function.ShowTotalInTreeType,
                HasUyQuyen = function.HasUyQuyen,
                HasTransferTheoLo = function.HasTransferTheoLo,
                TreeGroupId = function.TreeGroupId,
                DocColumnSettingId = function.DocColumnSettingId,
                PermissionSettingId = function.PermissionSettingId
            };
            var allOrder = _processFunctionService.GetsAs(t => t.Order, t => t.ParentId == parentId);

            if (allOrder != null && allOrder.Any())
            {
                newFunction.Order = allOrder.Max() + 1;
            }
            else
            {
                newFunction.Order = 0;
            }
            _processFunctionService.Create(newFunction);

            return
                Json(
                    new
                    {
                        success = true,
                        id = newFunction.ProcessFunctionId,
                        name = newFunction.Name,
                        parentId = newFunction.ParentId.HasValue ? newFunction.ParentId.Value : 0,
                        isActivated = newFunction.IsActivated,
                        color = newFunction.Color,
                        treeGroupId = newFunction.TreeGroupId
                    });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionDelete")]
        public JsonResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermissionUpdate"));
                return Json(new { error = true, message = _resourceService.GetResource("Customer.ProcessFunction.NotPermissionUpdate") });
            }

            var function = _processFunctionService.Get(id);
            if (function == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId"));
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + id });
            }

            //Kiểm tra nếu type khác 0 thì không cho xóa
            if (function.Type != (int)ProcessFunctionTypes.VanBan)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotDelete"));
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotDelete") + id });
            }

            //Lấy các bộ lọc của node
            var filters = _processFunctionService.GetProcessFunctionAndFilters(p => p.ProcessFunctionId == id);
            if (filters != null)
            {
                _processFunctionService.DeleteFilter(filters);
            }

            _processFunctionService.Delete(function, true);
            return Json(new { success = true });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionMove")]
        public JsonResult Move(int id, int target, string position, int? parentId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotPermissionMove"));
                return Json(new { error = true, message = _resourceService.GetResource("Customer.ProcessFunction.NotPermissionMove") });
            }

            if (parentId <= 0)
            {
                parentId = null;
            }
            var siblingFunction = parentId.HasValue
                                      ? _processFunctionService.Gets(f => f.ParentId == parentId)
                                      : _processFunctionService.Gets(f => !f.ParentId.HasValue);
            if (siblingFunction == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindParentId"));
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindParentId") + parentId });
            }
            var functionMove = siblingFunction.SingleOrDefault(t => t.ProcessFunctionId == id);
            if (functionMove == null)
            {
                return Json(new { error = true, message = "Không tìm thấy node được di chuyển với id là" + id });
            }
            var functionTarget = siblingFunction.SingleOrDefault(t => t.ProcessFunctionId == target);
            if (functionTarget == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId"));
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + id });
            }
            var orderMoveValue = functionMove.Order;
            var orderTargetValue = functionTarget.Order;

            if (orderMoveValue < orderTargetValue)
            {
                var functionMustReOrder =
                    siblingFunction.Where(
                        t =>
                        t.Order > orderMoveValue && t.Order < orderTargetValue && t.ProcessFunctionId != id &&
                        t.ProcessFunctionId != target);
                foreach (var function in functionMustReOrder)
                {
                    function.Order = function.Order - 1;
                }
                if (position == "before")
                {
                    functionMove.Order = orderTargetValue - 1;
                }
                if (position == "after")
                {
                    functionMove.Order = orderTargetValue;
                    functionTarget.Order = orderTargetValue - 1;
                }
            }
            else
            {
                var functionMustReOrder =
                    siblingFunction.Where(
                        t =>
                        t.Order > orderTargetValue && t.Order < orderMoveValue && t.ProcessFunctionId != id &&
                        t.ProcessFunctionId != target);
                foreach (var function in functionMustReOrder)
                {
                    function.Order = function.Order + 1;
                }
                if (position == "before")
                {
                    functionMove.Order = orderTargetValue;
                    functionTarget.Order = orderTargetValue + 1;
                }
                if (position == "after")
                {
                    functionMove.Order = orderTargetValue + 1;
                }
            }
            _processFunctionService.Update(functionMove);
            _cacheManager.Clear();
            _versionTreeSettings.CacheVersion = DateTime.Now.ToString();
            _settingService.SaveSetting(_versionTreeSettings);

            return Json(new { success = true });
        }

        #region Khong su dung => xem xet xoa di

        public ActionResult SettingColumn(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionSettingColumn"));
            //    return RedirectToAction("Index");
            //}

            var function = _processFunctionService.Get(id);
            if (function == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + id);
                return RedirectToAction("Index");
            }
            if (!(function.ProcessFunctionType != null && function.ProcessFunctionType.ListParam.Any(p => p.ValueField == DEFAULT_SORT_BY)))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotSearchField"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotSearchField"));
                return RedirectToAction("Index");
            }
            //var listSetting = function.ListSettings
            //                    .SingleOrDefault(l => l.ProcessFunctionId == id
            //                                        && !l.DocFieldId.HasValue
            //                                        && !l.DocTypeId.HasValue
            //                                        && !l.UserId.HasValue);
            //ViewBag.ColumnSetting = listSetting != null ? listSetting.SettingContent : string.Empty;
            ViewBag.AllDocField = _docfieldService.GetsAs(f => new { f.DocFieldId, f.DocFieldName })
                                .Select(f => new SelectListItem { Text = f.DocFieldName, Value = f.DocFieldId.ToString() });
            ViewBag.AllDocType = _doctypeService
                                .GetsAs(t => new { id = t.DocTypeId, name = t.DocTypeName, docfieldId = t.DocFieldId })
                                .StringifyJs();
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionSaveSettingColumn")]
        public JsonResult SaveSettingColumn(int id, string content, int? docfieldId, Guid? doctypeId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new { error = true, message = _resourceService.GetResource("Customer.ProcessFunction.NotPermissionSettingColumn") });
            //}

            //try
            //{
            //    Json2.ParseAs<List<ColumnSetting>>(content);
            //}
            //catch (Exception)
            //{
            //    return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Setting.Error") });
            //}
            //var function = _processFunctionService.Get(id);
            //if (function == null)
            //{
            //    return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + id });
            //}
            //if (!(function.ProcessFunctionType != null && function.ProcessFunctionType.ListParam.Any(p => p.ValueField == DEFAULT_SORT_BY)))
            //{
            //    return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotSearchFieldAndDocument") });
            //}
            //ListSetting listSetting;
            //if (!doctypeId.HasValue && !docfieldId.HasValue)
            //{
            //    listSetting = function.ListSettings
            //                    .SingleOrDefault(l => l.ProcessFunctionId == id
            //                                        && !l.DocFieldId.HasValue
            //                                        && !l.DocTypeId.HasValue
            //                                        && !l.UserId.HasValue);
            //}
            //else if (docfieldId.HasValue && !doctypeId.HasValue)
            //{
            //    listSetting = function.ListSettings
            //                    .SingleOrDefault(l => l.ProcessFunctionId == id
            //                                        && l.DocFieldId.HasValue && l.DocFieldId == docfieldId
            //                                        && !l.DocTypeId.HasValue);
            //}
            //else
            //{
            //    listSetting = function.ListSettings
            //                    .SingleOrDefault(l => l.ProcessFunctionId == id
            //                                        && l.DocTypeId.HasValue && l.DocTypeId == doctypeId);
            //}
            //if (listSetting == null)
            //{
            //    listSetting = new ListSetting
            //    {
            //        SettingContent = content,
            //        CreatedOnDate = DateTime.Now,
            //        DocFieldId = docfieldId,
            //        DocTypeId = doctypeId
            //    };
            //    function.ListSettings.Add(listSetting);
            //}
            //else
            //{
            //    listSetting.SettingContent = content;
            //    listSetting.LastModifiedOnDate = DateTime.Now;
            //}
            //_processFunctionService.Update(function);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Updated"));
            return Json(new { success = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Updated") });
        }

        public JsonResult GetProcessFunctionColumnSetting(int id, int? docfieldId, Guid? doctypeId)
        {
            var function = _processFunctionService.Get(id);
            if (function == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + id);
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFindId") + id });
            }
            if (!(function.ProcessFunctionType != null && function.ProcessFunctionType.ListParam.Any(p => p.ValueField == DEFAULT_SORT_BY)))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotSearchFieldAndDocument"));
                return Json(new { error = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotSearchFieldAndDocument") });
            }
            ListSetting listSetting = new ListSetting();
            //if (!doctypeId.HasValue && !docfieldId.HasValue)
            //{
            //    listSetting = function.ListSettings
            //                    .SingleOrDefault(l => l.ProcessFunctionId == id
            //                                        && !l.DocFieldId.HasValue
            //                                        && !l.DocTypeId.HasValue
            //                                        && !l.UserId.HasValue);
            //}
            //else if (docfieldId.HasValue && !doctypeId.HasValue)
            //{
            //    listSetting = function.ListSettings
            //                    .SingleOrDefault(l => l.ProcessFunctionId == id
            //                                        && l.DocFieldId.HasValue && l.DocFieldId == docfieldId
            //                                        && !l.DocTypeId.HasValue);
            //}
            //else
            //{
            //    listSetting = function.ListSettings
            //                    .SingleOrDefault(l => l.ProcessFunctionId == id
            //                                        && l.DocTypeId.HasValue && l.DocTypeId == doctypeId);
            //}
            //if (listSetting == null)
            //{
            //    return Json(new { setting = "[]" }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { setting = listSetting.SettingContent }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult AddFilter(int id)
        {
            if (id > 0)
            {
                ViewBag.AllFilters = _processFunctionFilterService.GetFilterNotInFunction(id).ToListModel();
                ViewBag.FunctionId = id;
                return PartialView("AddFilter");
            }
            else
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotExist"));
                return null;
            }
        }

        [HttpPost]
        public JsonResult AddProcessFilter(int functionId, List<int> filterIds)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new { success = false, message = "Bạn không có quyền thêm bộ lọc." });
            //}

            if (filterIds == null)
            {
                return Json(new { success = false });
            }

            var processFunctionAndFilters = new List<ProcessFunctionAndFilter>();
            foreach (var filterId in filterIds)
            {
                processFunctionAndFilters.Add(new ProcessFunctionAndFilter
                {
                    ProcessFunctionFilterId = filterId,
                    ProcessFunctionId = functionId
                });
            }

            _processFunctionService.AddFilter(processFunctionAndFilters);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult RemoveProcessFilter(int functionId, int filterId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    return Json(new { result = false, message = "Bạn không có quyền xóa."});
            //}

            var results = _processFunctionService.GetProcessFunctionAndFilters(functionId, filterId);
            if (results == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NullProcessFunctionAndFilters"));
                return Json(new { result = false, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NullProcessFunctionAndFilters") });
            }

            _processFunctionService.DeleteFilter(results);
            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult UploadFile(int id, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFile"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFile") });
            }

            var length = file.InputStream.Length;
            if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Upload.FileTooLarge"));
                return Json(new
                {
                    error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Upload.FileTooLarge")
                });
            }

            var extension = Path.GetExtension(file.FileName);
            if (!extension.Equals(".rpt"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Extension"));
                return Json(new
                {
                    error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Extension")
                });
            }

            var function = _processFunctionService.Get(id);
            if (function == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFound"));
                return Json(new
                {
                    error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFound")
                });
            }

            try
            {
                _processFunctionService.Update(file.InputStream, file.FileName, function);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Success"));
                return Json(new
                {
                    success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Success")
                });
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Error"));
                return Json(new
                {
                    error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.Error")
                });
            }
        }

        /// <summary>
        /// Download tệp
        /// </summary>
        /// <param name="id">Id của mẫ phôi</param>
        /// <returns></returns>
        public FileResult Download(int id)
        {
            var function = _processFunctionService.Get(id);
            if (function == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFound"));
                throw new Exception(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotFound"));
            }

            if (string.IsNullOrEmpty(function.ExportFileConfig))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotExportFileConfig"));
                throw new Exception(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.NotExportFileConfig"));
            }

            try
            {
                var exportConFig = Json2.ParseAs<FunctionFile>(function.ExportFileConfig);
                var stream = _processFunctionService.Download(exportConFig);
                return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, exportConFig.RealFileName);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        #endregion

        #region Process Function Group

        public ActionResult ProcessFunctionGroup()
        {
            var model = _processFunctionGroupService.Gets().ToListModel();
            return View(model);
        }

        public ActionResult CreateGroup()
        {
            return View(new ProcessFunctionGroupModel());
        }

        [HttpPost]
        public ActionResult CreateGroup(ProcessFunctionGroupModel model)
        {
            if (ModelState.IsValid)
            {
                _processFunctionGroupService.Create(model.ToEntity());
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionGroup.Created"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionGroup.Created"));
                return RedirectToAction("ProcessFunctionGroup");
            }

            return RedirectToAction("CreateGroup", model);
        }

        [HttpPost]
        public ActionResult DeleteGroup(int id)
        {
            var group = _processFunctionGroupService.Get(id);
            if (group != null)
            {
                _processFunctionGroupService.Delete(group);
            }
            return RedirectToAction("ProcessFunctionGroup");
        }

        public ActionResult EditGroup(int id)
        {
            var group = _processFunctionGroupService.Get(id);
            if (group != null)
            {
                return View(group.ToModel());
            }
            return RedirectToAction("ProcessFunctionGroup");
        }

        [HttpPost]
        public ActionResult EditGroup(ProcessFunctionGroupModel model)
        {
            if (ModelState.IsValid)
            {
                var group = _processFunctionGroupService.Get(model.ProcessFunctionGroupId);
                if (group != null)
                {
                    var entity = model.ToEntity(group);
                    _processFunctionGroupService.Update(entity);
                    return RedirectToAction("ProcessFunctionGroup");
                }
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionGroup.Update.Error"));
            ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionGroup.Update.Error"));
            return RedirectToAction("EditGroup", model);
        }

        #endregion

        #region Process Function Filter

        public ActionResult ProcessFunctionFilter()
        {
            var model = _processFunctionFilterService.Gets().ToListModel();
            return View(model);
        }

        public ActionResult CreateFilter()
        {
            ViewBag.FilterExpression = _resourceService.EnumToSelectList<ProcessFilterExpression>(1);
            return View(new ProcessFunctionFilterModel());
        }

        [HttpPost]
        public ActionResult CreateFilter(ProcessFunctionFilterModel model)
        {
            if (ModelState.IsValid)
            {
                _processFunctionFilterService.Create(model.ToEntity());
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionFilter.Created"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionFilter.Created"));
                return RedirectToAction("CreateFilter");
            }
            else
            {
                ViewBag.FilterExpression = _resourceService.EnumToSelectList<ProcessFilterExpression>(1);
                return RedirectToAction("CreateFilter", model);
            }
        }

        [HttpPost]
        public ActionResult DeleteFilter(int id)
        {
            var group = _processFunctionFilterService.Get(id);
            if (group != null)
            {
                _processFunctionFilterService.Delete(group);
            }
            return RedirectToAction("ProcessFunctionFilter");
        }

        public ActionResult EditFilter(int id)
        {
            var group = _processFunctionFilterService.Get(id);
            if (group != null)
            {
                ViewBag.FilterExpression = _resourceService.EnumToSelectList<ProcessFilterExpression>(group.FilterExpression);
                return View(group.ToModel());
            }
            return RedirectToAction("ProcessFunctionFilter");
        }

        [HttpPost]
        public ActionResult EditFilter(ProcessFunctionFilterModel model)
        {
            if (ModelState.IsValid)
            {
                var group = _processFunctionFilterService.Get(model.ProcessFunctionFilterId);
                if (group != null)
                {
                    var entity = model.ToEntity(group);
                    _processFunctionFilterService.Update(entity);
                    return RedirectToAction("ProcessFunctionFilter");
                }
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionFilter.Update.Error"));
            ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunctionFilter.Update.Error"));
            ViewBag.FilterExpression = _resourceService.EnumToSelectList<ProcessFilterExpression>(model.FilterExpression);
            return RedirectToAction("EditFilter", model);
        }

        #endregion

        #region Process Function Type

        public ActionResult ListFunctionType()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionListFunctionType"));
            //    return RedirectToAction("Index");
            //}

            var listType = _processFunctionTypeService.Gets().ToListModel();
            return View(listType);
        }

        public ActionResult CreateFunctionType()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionCreateFunctionType"));
            //    return RedirectToAction("Index");
            //}

            return View(new ProcessFunctionTypeModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionCreateFunctionType")]
        public ActionResult CreateFunctionType(ProcessFunctionTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionCreateFunctionType"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                _processFunctionTypeService.Create(model.ToEntity());
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("ProcessFunction.ProcessFunctionType.Created"));
                SuccessNotification(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.Created"));
                return RedirectToAction("CreateFunctionType");
            }
            return View(model);
        }

        public ActionResult EditFunctionType(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionEditFunctionType"));
            //    return RedirectToAction("Index");
            //}

            var type = _processFunctionTypeService.Get(id);
            if (type == null)
            {
                return RedirectToAction("ListFunctionType");
            }
            return View(type.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionEditFunctionType")]
        public ActionResult EditFunctionType(ProcessFunctionTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionEditFunctionType"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var type = _processFunctionTypeService.Get(model.ProcessFunctionTypeId);
                if (type == null)
                {
                    return RedirectToAction("ListFunctionType");
                }
                type = model.ToEntity(type);
                _processFunctionTypeService.Update(type);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("ProcessFunction.ProcessFunctionType.Updated"));
                SuccessNotification(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.Updated"));
                return RedirectToAction("ListFunctionType");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ProcessFunctionDeleteFunctionType")]
        public ActionResult DeleteFunctionType(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ProcessFunction.NotPermissionDeleteFunctionType"));
            //    return RedirectToAction("Index");
            //}

            var type = _processFunctionTypeService.Get(id);
            if (type == null)
            {
                return RedirectToAction("ListFunctionType");
            }
            if (type.ProcessFunctions.Count > 0)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("ProcessFunction.ProcessFunctionType.CanNotDelete"));
                ErrorNotification(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.CanNotDelete"));
                return RedirectToAction("ListFunctionType");
            }
            _processFunctionTypeService.Delete(type);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("ProcessFunction.ProcessFunctionType.Deleted"));
            SuccessNotification(_resourceService.GetResource("ProcessFunction.ProcessFunctionType.Deleted"));
            return RedirectToAction("ListFunctionType");
        }

        #endregion

        #region Private Functions

        private string GetAllDepartments()
        {
            var result = "[]";
            var allDepartments = _departmentService.GetCacheAllDepartments(true);
            if (allDepartments != null)
            {
                result = allDepartments
                            .Select(d =>
                                new
                                {
                                    label = d.DepartmentPath,
                                    value = d.DepartmentId,
                                    departmentName = d.DepartmentName,
                                    parentId = d.ParentId
                                }
                            )
                            .OrderBy(d => d.label).StringifyJs();
            }
            return result;
        }

        //private string GetAllUsers()
        //{
        //    var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
        //    return allUsers.Select(
        //                        u =>
        //                        new
        //                        {
        //                            value = u.UserId,
        //                            label = u.Username + " - " + u.FullName,
        //                            fullname = u.FullName,
        //                            username = u.Username,
        //                            phone = u.Phone
        //                        }).StringifyJs();
        //}

        /// <summary>
        /// Lấy tất cả loại function
        /// </summary>
        /// <returns></returns>
        private string GetAllFunctionType()
        {
            return _processFunctionTypeService.Gets()
                            .Select(t => new { id = t.ProcessFunctionTypeId, name = t.Name, param = t.Param })
                            .StringifyJs();
        }

        private List<SelectListItem> GetAllFunctionGroup()
        {
            var list = new List<SelectListItem>();
            var results = _processFunctionGroupService.Gets();

            if (results != null)
            {
                list.AddRange(results.Select(p =>
                    new SelectListItem
                    {
                        Value = p.ProcessFunctionGroupId.ToString(),
                        Text = p.Name
                    }).ToList());
            }

            return list;
        }

        #endregion

        private List<SelectListItem> GetShowTotalInTreeType()
        {
            return new List<SelectListItem>() { 
                 new SelectListItem{Value = "0",Text = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.ShowTotalInTreeType.Hide")},
                 new SelectListItem{Value = "1",Text = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.ShowTotalInTreeType.ShowUnread")},
                 new SelectListItem{Value = "2",Text =_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.ShowTotalInTreeType.ShowUnreadOnAll")},
                 new SelectListItem{Value = "3",Text = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ProcessFunction.ShowTotalInTreeType.ShowAll")},
            };
        }

        private List<SelectListItem> GetTreeGroups(int? treeGroupId = null)
        {
            var groups = _treeGroupService.GetCacheAllTreeGroups();
            if (groups != null && groups.Any())
            {
                return groups.Select(p => new SelectListItem()
                {
                    Value = p.TreeGroupId.ToString(),
                    Text = p.TreeGroupName,
                    Selected = (treeGroupId.HasValue && treeGroupId.Value == p.TreeGroupId)
                }).ToList();
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotConfigTreeGroup"));
            throw new ApplicationException(_resourceService.GetResource("Customer.ProcessFunction.NotConfigTreeGroup"));
        }

        private List<SelectListItem> GetAllPermissionSettings(int? permisId = null)
        {
            var groups = _permissionSettingService.GetsAs(p => new { p.PermissionSettingId, p.PermissionSettingName });
            if (groups != null && groups.Any())
            {
                return groups.Select(p => new SelectListItem()
                {
                    Value = p.PermissionSettingId.ToString(),
                    Text = p.PermissionSettingName,
                    Selected = (permisId.HasValue && permisId.Value == p.PermissionSettingId)
                }).ToList();
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotConfigPermissionSetting"));
            throw new ApplicationException(_resourceService.GetResource("Customer.ProcessFunction.NotConfigPermissionSetting"));
        }

        private List<SelectListItem> GetAllDocColumnSettings(int? docColumnId = null)
        {
            var groups = _docColumnSettingService.GetsAs(p => new { p.DocColumnSettingId, p.DocColumnSettingName });
            if (groups != null && groups.Any())
            {
                return groups.Select(p => new SelectListItem()
                {
                    Value = p.DocColumnSettingId.ToString(),
                    Text = p.DocColumnSettingName,
                    Selected = (docColumnId.HasValue && docColumnId.Value == p.DocColumnSettingId)
                }).ToList();
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ProcessFunction.NotConfigDocColumn"));
            throw new ApplicationException(_resourceService.GetResource("Customer.ProcessFunction.NotConfigDocColumn"));
        }
    }
}
