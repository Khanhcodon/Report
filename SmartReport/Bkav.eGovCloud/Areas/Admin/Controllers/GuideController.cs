using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov Online.
    /// Project: eGov Online.
    /// Class: GuideController - public - Controller.
    /// Create Date: 170714.
    /// Author: TrinhNVd.
    /// Description: Chứa các Action người dùng tương tác.
    /// </summary>
    [EgovAuthorize]
    //[RequireHttps]
    public class GuideController : CustomController
    {

        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly GuideBll _guideService;
        private const string DEFAULT_SORT = "Name";

        /// <summary>TrinhNVd - 170714
        /// Khởi tạo giá trị cho _resourceService, _guideService, _pageSettings
        /// </summary>
        /// <param name="guideService"></param>
        /// <param name="pageSettings"></param>
        /// <param name="resourceService"></param>
        public GuideController(AdminGeneralSettings generalSettings, ResourceBll resourceService, GuideBll guideService)
        {
            _resourceService = resourceService;
            _guideService = guideService;
            _generalSettings = generalSettings;
        }

        /// <summary>TrinhNVd - 170714
        /// Hiển thị danh sách hướng dẫn
        /// </summary>
        /// <returns>Giao diện danh sách hướng dẫn có phân trang</returns>
        public ActionResult Index()
        {
            var defaultPageSize = _generalSettings.DefaultPageSize;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = defaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT
            };

            var search = string.Empty;
            var httpCookie = Request.Cookies[CookieName.SearchGuide];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { }
            }
            var model = GetPageList(search, sortAndPage);
            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchGuide];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchGuide, data.StringifyJs())
                {
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/admin"
                };
            }
            Response.Cookies.Add(cookie);
        }

        /// <summary>TrinhNVd - 170714
        /// Hiển thị danh sách hướng dẫn theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="guideName">Tên hướng dẫn cần tìm</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên 1 trang</param>
        /// <returns>Giao diện danh sách hướng dẫn với từ khóa tìm kiếm, có phân trang</returns>
        public ActionResult Search(string guideName, int pageSize)
        {
            IEnumerable<GuideModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var search = string.Empty;
                search = guideName.Trim();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = 1,
                    IsSortDescending = false,
                    SortBy = DEFAULT_SORT
                };
                model = GetPageList(search, sortAndPage);
            }
            return PartialView("_List", model);
        }

        /// <summary>TrinhNVd - 170714
        /// Hiển thị danh sách hướng dẫn theo từ khóa tìm kiếm, trường sắp xếp, có phân trang
        /// </summary>
        /// <param name="guideName">Tên hướng dẫn tìm kiếm</param>
        /// <param name="sortBy">Trường được sắp xếp</param>
        /// <param name="isSortDesc">Sắp xếp từ thấp đến cao: false, ngược lại: true</param>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Kích thước 1 trang</param>
        /// <returns>Giao diện danh sách hướng dẫn theo từ khóa tìm kiếm, trường sắp xếp, có phân trang</returns>
        public ActionResult SortAndPaging(string guideName, string sortBy,
                                            bool isSortDesc, int page, int pageSize)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                IsSortDescending = isSortDesc,
                SortBy = sortBy
            };
            var model = GetPageList(guideName, sortAndPage);
            return PartialView("_List", model);
        }

        /// <summary>TrinhNVd - 170714
        /// Trả về danh sách theo điều kiện
        /// </summary>
        /// <param name="search">Tên hướng dẫn tìm kiếm</param>
        /// <param name="sortAndPage">Số trang</param>
        private IEnumerable<GuideModel> GetPageList(string search, SortAndPagingModel sortAndPage)
        {
            var totalRecords = 0;
            var model = _guideService.GetGuides(out totalRecords, currentPage: sortAndPage.CurrentPage,
                                            pageSize: sortAndPage.PageSize, sortBy: sortAndPage.SortBy,
                                            isDescending: sortAndPage.IsSortDescending,
                                            guideName: search).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            CreateCookieSearch(search, sortAndPage);
            ViewBag.GuideName = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return model;
        }

        /// <summary>
        /// Trả về trang chi tiết hướng dẫn theo url
        /// </summary>
        /// <param name="url">Đường dẫn thân thiện được đăng ký</param>
        /// <returns>Giao diện trang hướng dẫn với đường dẫn được click</returns>
        public ActionResult Detail(string url)
        {
            try
            {
                var guide = _guideService.GetByUrl(url);
                return View(guide.ToModel());
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
                return RedirectToAction("Index");
            }
        }

        /// <summary>TrinhNVd - 170714
        /// Tạo mới hướng dẫn
        /// </summary>
        /// <returns>Giao diện tạo mới hướng dẫn</returns>
        public ActionResult Create()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            return View(new GuideModel());
        }

        /// <summary>
        /// Thêm hướng dẫn vào CSDL nếu hợp lệ và chưa có trên CSDL
        /// </summary>
        /// <param name="guide">Hướng dẫn cần thêm mới</param>
        /// <returns>
        /// Success: Thông báo thành công và hiển thị giao diện thêm mới.
        /// UnSuccess: Thông báo thất bại và hiển thị lại gieo diện thêm mới với các thông tin được nhập
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GuideModel guide)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            if (ModelState.IsValid)
            {
                var entity = guide.ToEntity();
                try
                {
                    _guideService.Create(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Create.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.Create.Success"));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(guide);
                }
                return RedirectToAction("Create");
            }
            else
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Create.Fail"));
                ErrorNotification(_resourceService.GetResource("Common.Create.Fail"));
            }
            return View(guide);
        }

        /// <summary>TrinhNVd - 170714
        /// Chỉnh sửa thông tin hướng dẫn
        /// </summary>
        /// <param name="id">Mã hướng dẫn</param>
        /// <returns>
        /// Giao diện chỉnh sửa với hướng dẫn có mã tương ứng.
        /// </returns>
        public ActionResult Edit(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            var guide = _guideService.GetById(id);
            if (guide == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Guide.Message.DeleteBeforeEdit.Exist"));
                ErrorNotification(_resourceService.GetResource("Common.Guide.Message.DeleteBeforeEdit.Exist"));
                return RedirectToAction("Index");
            }
            return View(guide.ToModel());
        }

        /// <summary>TrinhNVd - 170714
        /// Cập nhật lại thông tin hướng dẫn vào CSDL
        /// </summary>
        /// <param name="guideModel">Hướng dẫn được chỉnh sửa</param>
        /// <returns>
        /// Success: Thông báo thành công và hiển thị giao diện danh sách hướng dẫn.
        /// UnSuccess: Thông báo thất bại và hiển thị lại gieo diện chỉnh sửa với các thông tin được nhập.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(GuideModel guideModel)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            if (ModelState.IsValid)
            {
                var guide = _guideService.GetById(guideModel.GuideId);
                if (guide == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Guide.Message.DeleteBeforeEdit.Exist"));
                    ErrorNotification(_resourceService.GetResource("Common.Guide.Message.DeleteBeforeEdit.Exist"));
                }
                else
                {
                    var oldGuideName = guide.Name;
                    var oldGuideUrl = guide.Url;
                    guide = guideModel.ToEntity(guide);
                    try
                    {
                        _guideService.Update(guide, oldGuideName, oldGuideUrl);
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Edit.Success"));
                        SuccessNotification(_resourceService.GetResource("Common.Edit.Success"));
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                        ErrorNotification(ex.Message);
                        return View(guideModel);
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Edit.Fail"));
                ErrorNotification(_resourceService.GetResource("Common.Edit.Fail"));
            }
            return View(guideModel);
        }

        /// <summary>TrinhNVd - 170714
        /// Xóa hướng dẫn theo mã
        /// </summary>
        /// <param name="id">Mã hướng dẫn cần xóa</param>
        /// <returns>
        /// Success: Thông báo thành công và chuyển đến giao diện danh sách hướng dẫn.
        /// UnSuccess: Thông báo thất bại và chuyển đến giao diện danh sách hướng dẫn.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionScopeArea"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            var guide = _guideService.GetById(id);
            try
            {
                _guideService.Delete(guide);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Guide.Message.Delete.Success"));
                SuccessNotification(_resourceService.GetResource("Common.Guide.Message.Delete.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}