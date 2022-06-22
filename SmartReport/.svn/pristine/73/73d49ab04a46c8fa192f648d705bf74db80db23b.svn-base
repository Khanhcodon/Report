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
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class CategoryController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly CategoryBll _categoryService;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public CategoryController(CategoryBll categoryService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CodeBll codeService,
            DocTypeBll docTypeService)
            : base()
        {
            _categoryService = categoryService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _codeService = codeService;
            _docTypeService = docTypeService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Category.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Category.NotPermission"));
                return RedirectToAction("Index");
            }

            var categoryBusinessId = 0;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var searchName = string.Empty;
            var httpCookie = Request.Cookies[CookieName.SearchCategory];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    searchName = data["searchName"] == null ? string.Empty : data["searchName"].ToString();
                    categoryBusinessId = data["categoryBusinessId"] == null ? 0 : int.Parse(data["categoryBusinessId"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _categoryService.Gets(out totalRecords,
                                        sortBy: sortAndPage.SortBy,
                                        isDescending: sortAndPage.IsSortDescending,
                                        pageSize: sortAndPage.PageSize,
                                        currentPage: sortAndPage.CurrentPage,
                                        categoryBusinessId: categoryBusinessId,
                                        search: searchName).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(categoryBusinessId, searchName, sortAndPage);
            }
            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            BindCategoryBusiness(categoryBusinessId);
            ViewBag.SearchName = searchName;
            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Category.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Category.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            BindCreate();
            GetAllCode(null);
            return View(new CategoryModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "CategoryCreate")]
        public ActionResult Create(CategoryModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Category.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Category.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var category = model.ToEntity();
                category.CategoryBusinessId = 0;
                foreach (var cate in model.CategoryBusiness)
                {
                    category.CategoryBusinessId |= cate;
                }

                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.CategoryName.Split(';').Distinct();
                        var list = new List<Category>();
                        foreach (var name in names)
                        {
                            var item = category.Clone();
                            item.CategoryName = name;
                            list.Add(item);
                        }
                        _categoryService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _categoryService.Create(category);
                    }

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Created"));
                    BindCreate();
                    GetAllCode();
                    ModelState.Clear();
                    return View(new CategoryModel
                    {
                        CategoryName = string.Empty,
                        CategoryBusinessId = category.CategoryBusinessId
                    });
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                }
            }

            GetAllCode();
            BindCategoryBusiness();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Category.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Category.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var category = _categoryService.Get(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var model = category.ToModel();
            var checkedCode = _categoryService.GetListCodeIdsChecked(id);
            int checkedCodeDefault = _categoryService.getCodeIDCheckedDefault(category.CategoryId);
            GetAllCode(checkedCode, checkedCodeDefault);
            BindCategoryBusiness();
            model.DefaultCodeId = checkedCodeDefault;
            BindCodeIds(model, category);
            //BindEdit(model.CategoryId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Category.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Category.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var category = _categoryService.Get(model.CategoryId);
                if (category == null)
                {
                    return RedirectToAction("Index");
                }

                var oldCategoryName = category.CategoryName;
                model.CategoryBusinessId = 0;
                foreach (var cate in model.CategoryBusiness)
                {
                    model.CategoryBusinessId |= cate;
                }

                try
                {
                    var defaultCodeId = model.DefaultCodeId;
                    category = model.ToEntity(category);
                    _categoryService.Update(category, oldCategoryName, defaultCodeId);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                    BindCodeIds(model, category);
                }
            }

            GetAllCode();
            BindCategoryBusiness();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Category.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Category.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var category = _categoryService.Get(id);
            if (category != null)
            {
                try
                {
                    _categoryService.Delete(category);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(string sortBy,
            bool isSortDesc, int page, int pageSize,
            int categoryBusinessId, string searchName)
        {
            IEnumerable<CategoryModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _categoryService.Gets(out totalRecords,
                    sortBy: sortBy,
                    isDescending: isSortDesc,
                    pageSize: pageSize,
                    currentPage: page,
                    categoryBusinessId: categoryBusinessId,
                    search: searchName).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                };

                sortAndPage.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.SearchName = searchName;
                ViewBag.CategoryBusinessId = categoryBusinessId;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                BindCategoryBusiness(categoryBusinessId);
                CreateCookieSearch(categoryBusinessId, searchName, sortAndPage);
            }
            return PartialView("_PartialList", model);
        }

        public ActionResult GetByCategoryBusiness(int categoryBusinessId, string searchName)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchCategory];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
            }

            int totalRecords;
            var model = _categoryService.Gets(out totalRecords,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                pageSize: sortAndPage.PageSize,
                currentPage: sortAndPage.CurrentPage,
                categoryBusinessId: categoryBusinessId,
                search: searchName).ToListModel();

            BindCategoryBusiness(categoryBusinessId);
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.SearchName = searchName;
            ViewBag.CategoryBusinessId = categoryBusinessId;
            CreateCookieSearch(categoryBusinessId, searchName, sortAndPage);

            return PartialView("_PartialList", model);
        }

        private void BindCreate()
        {
            var categoryBusinessId = 0;
            var httpCookie = Request.Cookies[CookieName.SearchCategory];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                categoryBusinessId = data["categoryBusinessId"] == null ? 0 : int.Parse(data["categoryBusinessId"].ToString());
            }
            BindCategoryBusiness(categoryBusinessId);
        }

        private void CreateCookieSearch(int categoryBusinessId, string searchName, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "searchName", searchName }, { "categoryBusinessId", categoryBusinessId }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchCategory];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchCategory, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
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

        private void GetAllCode(List<int> codeCheckeds = null, int codeCheckedDefault = 443)
        {
            var allCodes = _codeService.GetsAs(c => new CodeModel
            {
                CodeId = c.CodeId,
                CodeName = c.CodeName,
                Template = c.Template,
                Checked = false,
                IsDefault = false
            }).OrderByDescending(c => c.Checked).ThenBy(c => c.CodeName);

            if (codeCheckeds != null && codeCheckeds.Any())
            {
                foreach (var code in allCodes)
                {
                    if (!codeCheckeds.Contains(code.CodeId))
                    {
                        continue;
                    }

                    code.Checked = true;
                    if (code.CodeId == codeCheckedDefault && codeCheckedDefault != 0)
                    {
                        code.IsDefault = true;
                    }
                }
            }

            ViewBag.AllCodes = allCodes;
        }

        private void BindCodeIds(CategoryModel model, Category category)
        {
            var results = new List<int>();

            if (category.CategoryCodes == null || !category.CategoryCodes.Any())
            {
                category.CategoryCodes = _categoryService.GetCodes(category.CategoryId, false).ToList();
            }
            results = category.CategoryCodes.Select(p => p.CodeId).ToList();
            model.CodeIds = results;
        }

        private void BindEdit(int categoryId)
        {
            var allDocTypes = _docTypeService.GetsAs(p => new
            {
                p.DocTypeId,
                p.DocTypeName,
                p.CategoryId
            });

            ViewBag.SelectDocTypes = allDocTypes.Where(p => p.CategoryId == categoryId).Stringify();
            ViewBag.AllDocTypes = allDocTypes.Stringify();
        }
    }
}
