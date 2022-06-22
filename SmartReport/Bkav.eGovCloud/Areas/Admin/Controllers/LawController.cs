
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class LawController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileUploadSettings _fileSettings;
        private readonly FileBll _fileService;
        private readonly LawBll _lawService;
        private const string DEFAULT_SORT_BY = "NumberSign";

        public LawController(
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            FileUploadSettings fileSettings,
            FileBll fileService,
            LawBll lawService)
            : base()
        {
            _lawService = lawService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _fileService = fileService;
            _fileSettings = fileSettings;
        }

        public ActionResult Index()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            var isInvalidCookie = false;
            var httpCookie = Request.Cookies[CookieName.SearchLaw];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { isInvalidCookie = true; }
            }

            int totalRecords;
            var model = _lawService.GetLaws(out totalRecords, pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                lawName: search, currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }


            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchLaw];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchLaw, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }

            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult SortAndPaging(string search, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<LawModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.Trim();
                }
                int totalRecords;
                model = _lawService.GetLaws(out totalRecords, pageSize: pageSize,
                                            sortBy: sortBy, isDescending: isSortDesc,
                                            lawName: search, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };

                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                CreateCookieSearch(search, sortAndPage);
            }
            return PartialView("_LawList", model);
        }

        public ActionResult Search(string search, int pageSize)
        {
            IEnumerable<LawModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords = 0;
                model = _lawService.GetLaws(out totalRecords, pageSize: pageSize,
                                           sortBy: DEFAULT_SORT_BY, isDescending: true,
                                           lawName: search).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = 1,
                    IsSortDescending = true,
                    SortBy = DEFAULT_SORT_BY,
                    TotalRecordCount = totalRecords
                };
                ViewBag.SortAndPage = sortAndPage;
                CreateCookieSearch(search, sortAndPage);
            }
            else
            {
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = 1,
                    IsSortDescending = false,
                    SortBy = DEFAULT_SORT_BY,
                    TotalRecordCount = 0
                };

                ViewBag.SortAndPage = sortAndPage;
            }

            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_LawList", model);
        }

        public ActionResult Create()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            return View(new LawModel());
        }

        [HttpPost]
        public ActionResult Create(LawModel model, List<HttpPostedFileBase> fileUpload)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    if (fileUpload != null && fileUpload.Any())
                    {
                        var maximumSizeBytes = _fileSettings.FileUploadMaximumSizeBytes;
                        var allOwedExtensions = _fileSettings.FileUploadAllowedExtensions;
                        foreach (var file in fileUpload)
                        {
                            if (file == null)
                            {
                                continue;
                            }

                            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                            var fileName = Path.GetFileName(file.FileName);
                            if (!allOwedExtensions.Any(a => a.Contains(fileExt.ToLower())))
                            {
                                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.AllOwedExtensions.NotSupport"));
                                ErrorNotification(_resourceService.GetResource("Common.Law.AllOwedExtensions.NotSupport"));
                                return View(model);
                            }

                            if (file.ContentLength > maximumSizeBytes)
                            {
                                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.MaximumSizeBytes.Valid"));
                                ErrorNotification(_resourceService.GetResource("Common.Law.MaximumSizeBytes.Valid"));
                                return View(model);
                            }

                            var fileTmp = _fileService.Upload(file.InputStream, fileName, fileExt, model.LawId);
                            model.Files.Add(fileTmp);
                        }
                    }

                    var law = model.ToEntity();
                    _lawService.Create(law);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.Create.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.Law.Create.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.Create.Error") + ": " + ex.Message);
                    ErrorNotification(_resourceService.GetResource("Common.Law.Create.Error") + ": " + ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var law = _lawService.GetById(id);
            if (law == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.NotExist"));
                ErrorNotification(_resourceService.GetResource("Common.Law.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _lawService.Delete(law);
                CreateActivityLog(ActivityLogType.Admin,_resourceService.GetResource("Common.Law.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Common.Law.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.Deleted.Error"));
                ErrorNotification(_resourceService.GetResource("Common.Law.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewLaw()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _lawService.GetLaws().ToListModel().ToList();
            return View(model);
        }

        public JsonResult RemoveFile(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var file = _fileService.GetFile(id);
            if (file == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.File.NotExist"));
                return Json(new { error = _resourceService.GetResource("Common.Law.File.NotExist") }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                _fileService.Delete(file);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.File.Deleted.Success"));
                return Json(new { success = _resourceService.GetResource("Common.Law.File.Deleted.Success") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.File.Deleted.Error"));
                return Json(new { error = _resourceService.GetResource("Common.Law.File.Deleted.Error") }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var entity = _lawService.GetById(id);
            if (entity != null)
            {
                return View(entity.ToModel());
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.NotExist"));
            ErrorNotification(_resourceService.GetResource("Common.Law.NotExist"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(LawModel model, List<HttpPostedFileBase> fileUpload)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var law = _lawService.GetById(model.LawId);
                    if (law == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Common.Law.NotExist"));
                        return RedirectToAction("Index");
                    }

                    model.Files = law.Files;
                    law = model.ToEntity(law);
                    if (fileUpload != null && fileUpload.Any())
                    {
                        var maximumSizeBytes = _fileSettings.FileUploadMaximumSizeBytes;
                        var allOwedExtensions = _fileSettings.FileUploadAllowedExtensions;
                        var files = new List<Bkav.eGovCloud.Entities.Customer.File>();
                        foreach (var file in fileUpload)
                        {
                            if (file == null)
                            {
                                continue;
                            }

                            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                            var fileName = Path.GetFileName(file.FileName);

                            if (!allOwedExtensions.Any(a => a.Contains(fileExt.ToLower())))
                            {
                                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.AllOwedExtensions.NotSupport"));
                                ErrorNotification(_resourceService.GetResource("Common.Law.AllOwedExtensions.NotSupport"));
                                return View(model);
                            }

                            if (file.ContentLength > maximumSizeBytes)
                            {
                                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.MaximumSizeBytes.Valid"));
                                ErrorNotification(_resourceService.GetResource("Common.Law.MaximumSizeBytes.Valid"));
                                return View(model);
                            }

                            var fileInfo = _fileService.Upload(file.InputStream, fileName, fileExt, model.LawId);
                            law.Files.Add(fileInfo);
                        }
                    }

                    _lawService.Update(law);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.Law.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Law.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.Law.Updated.Error"));
                }
            }
            return View(model);
        }

        public FileResult Download(int fileId)
        {
            string fileName = string.Empty;
            var stream = _fileService.DownloadFile(out fileName, fileId);
            if (stream == null || stream.Length <= 0)
            {
                throw new Exception("File is not exist.");
            }
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}