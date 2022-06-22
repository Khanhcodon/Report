using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using Bkav.eGovCloud.Core.ReadFile;
using ClosedXML.Excel;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class AddressController : CustomController
    {
        private readonly AddressBll _addressService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DocTypeBll _docTypeService;
        private readonly SyncDocTypeBll _syncDocTypeService;
        private readonly UserBll _userService;
        private readonly DepartmentBll _departmentService;

        private const string DEFAULT_SORT_BY = "GroupName";

        public AddressController(AddressBll addressService,
                                ResourceBll resourceService,
                                AdminGeneralSettings generalSettings,
                                DocTypeBll docTypeService,
                                SyncDocTypeBll syncDocTypeService,
                                UserBll userService,
                                DepartmentBll departmentService)
            : base()
        {
            _addressService = addressService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _docTypeService = docTypeService;
            _syncDocTypeService = syncDocTypeService;
            _userService = userService;
            _departmentService = departmentService;
        }

        public ActionResult Index(string GroupName = "", int LevelEdocId = 0)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Address.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchAddress];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    isInvalidCookie = true;
                    LogException(ex);
                }
            }

            int totalRecords = 0;
            ViewBag.GroupName = GetGroupNameSelectList();

            Expression<Func<Address, bool>> spec = GetQueryAddress(GroupName, search, LevelEdocId);
            var model = _addressService.Gets(out totalRecords, spec,
                        currentPage: sortAndPage.CurrentPage,
                        pageSize: sortAndPage.PageSize,
                        sortBy: sortAndPage.SortBy,
                        isDescending: sortAndPage.IsSortDescending).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;

            ViewBag.GroupNames = GetGroupNames();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateGroupName(string listAddressId, string groupName, bool isOverride = false)
        {
            var listAddressIds = JsonConvert.DeserializeObject<List<int>>(listAddressId);

            var addresses = _addressService.Gets(listAddressIds, isReadonly: false);
            _addressService.UpdateGroupName(addresses, groupName, isOverride);

            return Redirect("Index");
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchAddress];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchAddress, data.StringifyJs())
                {
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/admin"
                };
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult SortAndPaging(
            string filter, string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            IEnumerable<AddressModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords = 0;
                Expression<Func<Address, bool>> spec = GetQueryAddress("", filter, 0);


                model = _addressService.Gets(out totalRecords, spec,
                    currentPage: page, pageSize: pageSize,
                    sortBy: DEFAULT_SORT_BY, isDescending: isSortDesc).ToListModel();

                sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
            }

            ViewBag.Search = filter;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.GroupNames = GetGroupNames();
            CreateCookieSearch(filter, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult Search(string filter, int pageSize, string GroupName = "", int levelEdocId = 0)
        {
            IEnumerable<AddressModel> model = null;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY,
                TotalRecordCount = 0
            };

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords = 0;
                    Expression<Func<Address, bool>> spec = GetQueryAddress(GroupName, filter, levelEdocId);
                    model = _addressService.Gets(out totalRecords, spec,
                        currentPage: 1,
                        pageSize: pageSize,
                        sortBy: DEFAULT_SORT_BY,
                        isDescending: true).ToListModel();

                    sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                }
            }

            ViewBag.Search = filter;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.GroupNames = GetGroupNames();
            CreateCookieSearch(filter, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Address.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}
            ViewBag.AllGroupName = GetAllGroupName();
            ViewBag.AllDepartments = GetAllDepartments();
            ViewBag.AllUsers = GetAllUsers();
            return View(new AddressModel());
        }

        [HttpPost]
        public JsonResult CreateChild(AddressModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _addressService.GetByeDocId(model.EdocId);
                    if (exist != null)
                    {
                        if (model.AddressId != 0)
                        {
                            var address = _addressService.Get(model.AddressId);
                            address.EdocId = model.EdocId;
                            address.Name = model.Name;
                            address.GroupName = model.GroupName;
                            _addressService.Update(address);
                            return Json(new { status = true, data = address }, JsonRequestBehavior.AllowGet);
                        }

                        return Json(new { status = false, message = "Trùng mã định danh với cơ quan khác" }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.AddressId == 0)
                    {
                        var address = model.ToEntity();
                        _addressService.Create(address);
                        return Json(new { status = true, data = address }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var address = _addressService.Get(model.AddressId);
                        address.EdocId = model.EdocId;
                        address.Name = model.Name;
                        address.GroupName = model.GroupName;
                        _addressService.Update(address);
                        return Json(new { status = true, data = address }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    return Json(new { status = false, message = "Có lỗi xảy ra xem chi tiết tại log hệ thống" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = false, message = "Không thành công" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteChild(int id)
        {
            try
            {
                var address = _addressService.Get(id);
                _addressService.Delete(address);

                return Json(new { status = true, data = address }, JsonRequestBehavior.AllowGet);
            }
            catch (EgovException ex)
            {
                LogException(ex);
                return Json(new { status = false, message = "Có lỗi xảy ra xem chi tiết tại log hệ thống" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "AddressCreate")]
        public ActionResult Create(AddressModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Address.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _addressService.GetByeDocId(model.EdocId);
                    if (exist != null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.EdocExist"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.EdocExist"));
                        ModelState.AddModelError("EdocId", _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.EdocExist"));
                        return View(model);
                    }

                    var address = model.ToEntity();
                    _addressService.Create(address);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Created.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Created.Error"));
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Address.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var address = _addressService.Get(id);

            ViewBag.AllGroupName = GetAllGroupName();
            ViewBag.AllDepartments = GetAllDepartments();
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.AllChildren = GetChildrens(id);

            if (address == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                return RedirectToAction("Index");
            }

            return View(address.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "AddressEdit")]
        public ActionResult Edit(AddressModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Address.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var address = _addressService.Get(model.AddressId);
            if (address == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _addressService.GetByeDocId(model.EdocId);
                    if (exist != null)
                    {
                        if (address.AddressId != exist.AddressId)
                        {
                            ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.EdocExist"));
                            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.EdocExist"));
                            ModelState.AddModelError("EdocId", _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.EdocExist"));
                            return View(model);
                        }
                    }
                    if (model.GroupName != address.GroupName)
                    {
                        var list = new List<Address>();
                        address = model.ToEntity(address);
                        list.Add(address);
                        _addressService.UpdateGroupName(list, model.GroupName);
                    }

                    address = model.ToEntity(address);
                    _addressService.Update(address);

                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Updated.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Updated.Error"));
                }
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "AddressDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Address.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var address = _addressService.Get(id);
            if (address == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                return RedirectToAction("Index");
            }
            try
            {
                _addressService.Delete(address);
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Deleted.Success"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult SyncDocType(int id)
        {
            var address = _addressService.Get(id);
            if (address == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Address.NotExist"));
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(address.Website))
            {
                return View();

            }

            var apiAddress = address.Website + "/webapi/doctype/GetSyncDocTypes";
            var request = WebRequest.Create(apiAddress);
            var response = request.GetResponse();
            var streamResponse = response.GetResponseStream();
            var streamRead = new StreamReader(streamResponse);
            var outsideDocTypes = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(streamRead.ReadToEnd());

            ViewBag.OutsideDocTypes = outsideDocTypes.Select(u => new
            {
                value = u.DocTypeId,
                label = u.DocTypeName
            }).StringifyJs();

            ViewBag.InsideDocTypes = _docTypeService.Gets(d => d.IsActivated).Select(u => new
            {
                value = u.DocTypeId,
                label = u.DocTypeName
            }).StringifyJs();

            //QuangP: Tạm thời lấy chính docType của mình do chưa liên thông
            //var x = (int)DocTypePermissions.DuocPhepLienThong;
            //ViewBag.OutsideDocTypes = _docTypeService.Gets(d => d.DocTypePermission.HasValue && (d.DocTypePermission.Value & x) == x).Select(u => new
            //{
            //    value = u.DocTypeId,
            //    label = u.DocTypeName
            //}).StringifyJs();

            ViewBag.CurrentSyncs = _syncDocTypeService.Gets().Select(u => new
            {
                insideDoctypeId = u.InsideDocTypeId,
                outsideDoctypeId = u.OutsideDocTypeId
            }).StringifyJs();

            return View();
        }

        public JsonResult SyncDocTypes(string docTypesMap)
        {
            try
            {
                var mapDoctypeIds = Json2.ParseAs<Dictionary<string, string>>(docTypesMap);
                if (mapDoctypeIds.Any())
                {
                    var syncDocTypes = new List<SyncDocType>();
                    foreach (var map in mapDoctypeIds)
                    {
                        syncDocTypes.Add(new SyncDocType
                        {
                            InsideDocTypeId = Guid.Parse(map.Value),
                            OutsideDocTypeId = Guid.Parse(map.Key)
                        });
                    }
                    _syncDocTypeService.Create(syncDocTypes);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ImportAddressFromFile()
        {
            return View();
        }
		
        private string GetAllGroupName()
        {
            var allAddress = _addressService.Gets();

            return allAddress.GroupBy(x => x.GroupName).Select(x => x.First().GroupName).Stringify();

        }

        private string GetAllUsers()
        {
            var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
            return allUsers.Select(
                                u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                    fullname = u.FullName,
                                    username = u.Username,
                                    phone = u.Phone
                                }).StringifyJs();
        }

        private string GetChildrens(int addressId)
        {
            var result = "[]";
            var listAddress = _addressService.GetAddresses(addressId);
            if (listAddress != null)
            {
                result = listAddress.StringifyJs();
            }

            return result;
        }

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

        private IEnumerable<string> GetGroupNames()
        {
            var resultGroupName = new List<string>();
            var groupNames = _addressService.Gets().Select(x => x.GroupName).Distinct();
            var listGroupName = groupNames.ToList();
            foreach (var grpName in listGroupName)
            {
                var listGroup = _addressService.ParseGroupName(grpName);
                resultGroupName.AddRange(listGroup);
            }

            return resultGroupName.Distinct();
        }

        private IEnumerable<SelectListItem> GetGroupNameSelectList(string selected = "")
        {
            var addresses = _addressService.GetsFromCache().Where(d => d.ParentId == null);
            var gnSelectList = new List<SelectListItem>();

            gnSelectList.Add(new SelectListItem()
            {
                Text = "Tất cả",
                Value = "",
                Selected = selected == ""
            });

            if (addresses != null || addresses.Any())
            {
                var groupName = addresses.Select(x => x.GroupName).Distinct().Select(x =>
                {
                    if (x == null)
                    {
                        return new SelectListItem
                        {
                            Text = "Chưa phân nhóm",
                            Value = "cpn",
                            Selected = x == selected
                        };
                    }

                    return new SelectListItem
                    {
                        Text = x,
                        Value = x,
                        Selected = x == selected
                    };
                });

                gnSelectList.AddRange(groupName);
            }

            return gnSelectList;
        }

        private Expression<Func<Address, bool>> GetQueryAddress(string groups, string search, int level)
        {
            Expression<Func<Address, bool>> spec = p => p.ParentId == null &&
                (groups == "" || p.GroupName == groups) && (level == 0 || p.LevelEdocId == level);
            if (!string.IsNullOrWhiteSpace(search))
            {
                spec = p => p.ParentId == null &&
                (groups == "" || p.GroupName == groups) &&
                (p.EdocId.Contains(search) || p.AddressString.Contains(search) || p.Name.Contains(search));
            }

            return spec;
        }
    }
}
