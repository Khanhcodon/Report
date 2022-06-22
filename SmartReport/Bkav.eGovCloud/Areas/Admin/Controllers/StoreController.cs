using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class StoreController : CustomController //BaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly StoreBll _storeService;
        private readonly ResourceBll _resourceService;
        private readonly DepartmentBll _departmentService;
        private readonly UserBll _userService;
        private readonly CodeBll _codeService;
        private readonly DocFieldBll _docfieldService;
        private readonly StoreDocBll _storeDocService;
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _documentCopyService;

        private const string DEFAULT_SORT_BY = "StoreName";

        public StoreController(
            StoreBll storeService,
            UserBll userService,
            CodeBll codeService,
            ResourceBll resourceService,
            DepartmentBll departmentService,
            AdminGeneralSettings generalSettings,
            StoreDocBll storeDocService,
            DocumentBll documentService,
            DocumentCopyBll documentCopyService,
            DocFieldBll docfieldService
            )
            : base()
        {
            _storeService = storeService;
            _resourceService = resourceService;
            _departmentService = departmentService;
            _userService = userService;
            _codeService = codeService;
            _generalSettings = generalSettings;
            _docfieldService = docfieldService;
            _storeDocService = storeDocService;
            _documentCopyService = documentCopyService;
            _documentService = documentService;
        }

        public JsonResult SyncErrorStore(int year)
        {
            var from = new DateTime(year, 1, 1, 0, 0, 0);
            var to = new DateTime(year, 12, 31, 23, 59, 59);
            var query = "SELECT s.StoreId, s.StoreName, c.CodeName,  replace(REPLACE(c.Template, '$n$', '$N$') ,'$N$', '') as Template, s.CategoryBusinessId from store s, store_code sc, `code` c WHERE s.StoreId = sc.StoreId and sc.CodeId = c.CodeId and s.CategoryBusinessId = 1";
            var storeCodes = _storeService.ExecuteDatabase<StoreCodeConvert>(query);

            var documents = _documentService.Gets(true, d => d.DateCreated > from
           && d.DateCreated < to
           && d.CategoryBusinessId == 1);
            var result = new List<StoreCodeConvert>();
            foreach (var doc in documents)
            {
                var inOutCode = doc.InOutCode;
                if (string.IsNullOrEmpty(inOutCode) || !doc.StoreId.HasValue)
                {
                    continue;
                }
                var storeId = doc.StoreId.Value;

                var storeTemplate = storeCodes.FirstOrDefault(sc => doc.InOutCode.Contains(sc.Template));
                if (storeTemplate != null)
                {
                    if (storeTemplate.StoreId != doc.StoreId.Value)
                    {
                        result.Add(new StoreCodeConvert {
                            StoreId = doc.StoreId.Value,
                            Template = storeTemplate.Template,
                            StoreName = doc.InOutCode,
                            CodeName = storeTemplate.StoreId.ToString()
                        });
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SyncStoreDoc(int year)
        {
            var storeDocs = _storeDocService.Gets();
            var newStoreDocs = new List<StoreDoc>();
            var from = new DateTime(year, 1, 1, 0, 0, 0);
            var to = new DateTime(year, 12, 31, 23, 59, 59);
            var documents = _documentService.Gets(true, d => d.DateCreated > from 
            && d.DateCreated < to 
            && d.CategoryBusinessId != 4);
            var docGroups = documents.OrderBy(d=>d.DateCreated).GroupBy(x => x.InOutCode).Select(x => x.First());
            if (storeDocs != null)
            {
                foreach (var item in docGroups)
                {
                    if (!storeDocs.Any(st => st.DocumentId == item.DocumentId) && item.StoreId.HasValue)
                    {
                        newStoreDocs.Add(new StoreDoc
                        {
                            DocumentId = item.DocumentId,
                            StoreId = item.StoreId.Value
                        });
                    }
                }
                _storeDocService.Create(newStoreDocs);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Store.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Store.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var categoryBusinessId = 0;
            var sortAndPage = new SortAndPagingModel
            {
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY,
            };
            var httpCookie = Request.Cookies[CookieName.SearchStore];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    categoryBusinessId = data["categoryBusinessId"] == null ? 0 : int.Parse(data["categoryBusinessId"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            BindCategoryBusiness(categoryBusinessId);
            var model = _storeService.GetsAs(t => new StoreModel
            {
                StoreId = t.StoreId,
                StoreName =
                        t.StoreName,
                CategoryBusinessId =
                        t.
                            CategoryBusinessId,
                FullName = t.User.FullName
            },
                sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                categoryBusinessId: categoryBusinessId);

            if (isInvalidCookie)
            {
                CreateCookieSearch(categoryBusinessId, sortAndPage);
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.AllUser = _userService.GetAllCached(true);
            return View(model);
        }

        private void CreateCookieSearch(int categoryBusinessId, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "categoryBusinessId", categoryBusinessId }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchStore];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchStore, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        private void BindCategoryBusiness(int? categoryBusinessId = null)
        {
            var listCategoryBusiness = _resourceService.EnumToSelectList<CategoryBusinessTypes>(categoryBusinessId);
            //#if !HoSoMotCuaEdition
            //            listCategoryBusiness = listCategoryBusiness.Where(l => l.Value != ((int)CategoryBusinessTypes.Hsmc).ToString()).ToList();
            //#endif
            ViewBag.AllCategoryBusiness = listCategoryBusiness;
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Store.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Store.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var model = new StoreModel();
            var categoryBusinessId = 0;
            var httpCookie = Request.Cookies[CookieName.SearchStore];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                categoryBusinessId = data["categoryBusinessId"] == null ? 0 : int.Parse(data["categoryBusinessId"].ToString());
            }
            model.CategoryBusinessId = categoryBusinessId;
            ReBindDataWhenError(model);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "StoreCreate")]
        public ActionResult Create(StoreModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Store.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Store.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entityCreate = model.ToEntity();
                    _storeService.Create(entityCreate);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    ReBindDataWhenError(model);
                    return View(model);
                }
            }
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Store.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Store.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var store = _storeService.Get(id);
            if (store == null)
                return RedirectToAction("Index");

            int codeIdDefault = _storeService.GetCodeIdDefault(store.StoreId);
            var model = store.ToModel();
            model.DefaultCodeId = codeIdDefault;
            ReBindDataWhenError(model, codeIdDefault);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "StoreEdit")]
        public ActionResult Edit(StoreModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Store.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Store.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var store = _storeService.Get(model.StoreId);
                if (store == null)
                {
                    return RedirectToAction("Index");
                }

                var oldStoreName = store.StoreName;
                try
                {
                    int defaultCodeId = model.DefaultCodeId;
                    _storeService.Update(model.ToEntity(store), oldStoreName);
                    _storeService.UpdateCodeDefault(model.ToEntity(store), defaultCodeId);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Updated"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Updated"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    ReBindDataWhenError(model);
                    return View(model);
                }
                //SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Updated"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "StoreDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Store.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Store.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var store = _storeService.Get(id);
            if (store != null)
            {
                try
                {
                    _storeService.Delete(store);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult StoreByCategoryBusinessCode(int categoryBusinessId, string searchName)
        {
            var sortAndPage = new SortAndPagingModel
            {
                CurrentPage = 1,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchStore];

            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
            }

            var model =
                _storeService.GetsAs(t => new StoreModel
                {
                    StoreId = t.StoreId,
                    StoreName =
                        t.StoreName,
                    CategoryBusinessId =
                        t.
                            CategoryBusinessId,
                    FullName = t.User.FullName
                },
                sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                categoryBusinessId: categoryBusinessId,
                searchName: searchName);

            CreateCookieSearch(categoryBusinessId, sortAndPage);
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.SearchName = searchName;
            ViewBag.CategoryBusinessId = categoryBusinessId;

            BindCategoryBusiness(categoryBusinessId);
            ViewBag.AllUser = _userService.GetAllCached();
            return PartialView("_PartialList", model);
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
                                    firstpositionid = 0
                                }).StringifyJs();
        }

        private IEnumerable<CodeModel> GetAllCodes(List<int> codeChecked, int codeIdDefault)
        {
            var allCodes = _codeService.GetsAs(c => new CodeModel
            {
                CodeId = c.CodeId,
                CodeName = c.CodeName,
                Template = c.Template,
                Checked = false
            }).OrderByDescending(c => c.Checked).ThenBy(c => c.CodeName);
            foreach (var code in allCodes)
            {
                if (codeChecked.Count > 0 && codeChecked.Contains(code.CodeId))
                {
                    code.Checked = true;
                    if (code.CodeId == codeIdDefault)
                    {
                        code.IsDefault = true;
                    }
                }
            }
            return allCodes;
        }

        private string GetAllDepartment()
        {
            var alldepartments = _departmentService.GetCacheAllDepartments(true);
            return alldepartments.Select(u => new
            {
                value = u.DepartmentId,
                label = u.DepartmentName,
                parentid = u.ParentId.HasValue ? u.ParentId : 0,
                data = u.DepartmentName,
                metadata = new { id = u.DepartmentId },
                state = "leaf",
                extvalue = u.DepartmentIdExt,
                path = u.DepartmentPath,
                order = u.Order,
                level = u.Level,
                attr = new { id = u.DepartmentId, rel = "dept" }
            }).StringifyJs();
        }

        /// <summary>
        /// Trả về danh sách tất cả các quan hệ người dùng - phòng ban - chức vụ.
        /// </summary>
        /// <returns>Json object danh sách quan hệ người dùng - phòng ban - chức vụ.</returns>
        private string GetAllUserDepartmentJobTitlesPosition()
        {
            var result = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
                t =>
                    new
                    {
                        departmentid = t.DepartmentId,
                        userid = t.UserId,
                        positionid = t.PositionId,
                        idext = t.DepartmentIdExt,
                        jobtitleid = t.JobTitlesId,
                        hasReceiveDocument = t.HasReceiveDocument
                    }).StringifyJs();
            return result;
        }

        private void ReBindDataWhenError(StoreModel store, int codeIdDefault = 0)
        {
            ViewBag.SelectedUsers = store.ListUserViewIds != null ? store.ListUserViewIds.StringifyJs() : "[]";
            ViewBag.AllUserDepartmentJobTitlesPosition = GetAllUserDepartmentJobTitlesPosition();
            ViewBag.AllDepartments = GetAllDepartment();
            ViewBag.AllUsers = GetAllUsers();
            var selectedCode = store.StoreId > 0
                ? _storeService.GetCodeIds(store.StoreId)
                : string.IsNullOrWhiteSpace(store.CodeIds) ? new List<int>() : Json2.ParseAs<List<int>>(store.CodeIds);
            ViewBag.SelectedCode = selectedCode;
            ViewBag.UserNameResponsible = "";
            if (store.UserId.HasValue)
            {
                var user = _userService.Get(store.UserId.Value, true);
                if (user != null)
                {
                    ViewBag.UserNameResponsible = user.FullName;
                }
            }
            ViewBag.DepartmentResponsible = "";
            if (store.DepartmentId.HasValue)
            {
                var dept = _departmentService.Get(store.DepartmentId.Value);
                if (dept != null)
                {
                    ViewBag.DepartmentResponsible = dept.DepartmentName;
                }
            }
            var listCodeChecked = selectedCode.ToList();
            //int codeIdDefault = _storeService.GetCodeIdDefault(store.StoreId);
            ViewBag.AllCodes = GetAllCodes(listCodeChecked, codeIdDefault);
            BindCategoryBusiness(store.CategoryBusinessId);
            var docFields = _docfieldService.Gets(d => d.IsActivated == true);
            ViewBag.Docfields = docFields.Select(d => new
            {
                value = d.DocFieldId,
                label = d.DocFieldName,
                CategoryBusinessId = d.CategoryBusinessId
            }).StringifyJs();
        }

        public ActionResult SortAndPaging(
            string sortBy, bool isSortDesc, int page,
            int pageSize, int categoryBusinessId, string searchName)
        {
            IEnumerable<StoreModel> model = null;
            if (Request.IsAjaxRequest())
            {
                model =
                    _storeService.GetsAs(t => new StoreModel
                    {
                        StoreId = t.StoreId,
                        StoreName =
                            t.StoreName,
                        CategoryBusinessId =
                            t.
                                CategoryBusinessId,
                        FullName = t.User.FullName
                    }, sortBy, isDescending: isSortDesc,
                        categoryBusinessId: categoryBusinessId,
                        searchName: searchName);
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                };

                CreateCookieSearch(categoryBusinessId, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                ViewBag.SearchName = searchName;
                ViewBag.CategoryBusinessId = categoryBusinessId;
                BindCategoryBusiness(categoryBusinessId);
                ViewBag.AllUser = _userService.GetAllCached();
            }
            return PartialView("_PartialList", model);
        }

        /// <summary>
        /// Sử dụng để fix lỗi hồ sơ cá nhân convert sang
        /// </summary>
        /// <returns></returns>
        public string FixConvertName()
        {
            _storeService.FixConvert();
            return "Thành công";
        }
    }

    public class StoreCodeConvert
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string CodeName { get; set; }
        public string Template { get; set; }
    }
}
