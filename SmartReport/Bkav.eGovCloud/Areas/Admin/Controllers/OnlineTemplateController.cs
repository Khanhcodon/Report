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
    public class OnlineTemplateController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileUploadSettings _fileSettings;
        private readonly FileBll _fileService;
        private readonly OnlineTemplateBll _onlineTemplateService;
        private readonly DoctypeTemplateBll _doctypeTemplateService;
        private const string DEFAULT_SORT_BY = "Name";

        public OnlineTemplateController(
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            FileUploadSettings fileSettings,
            FileBll fileService,
            OnlineTemplateBll onlineTemplateService,
            DoctypeTemplateBll doctypeTemplateService)
            : base()
        {
            _onlineTemplateService = onlineTemplateService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _fileService = fileService;
            _fileSettings = fileSettings;
            _doctypeTemplateService = doctypeTemplateService;
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
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchOnlineTemplate];
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

            int totalRecords;
            var model = _onlineTemplateService.GetOnlineTemplates(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                onlineTemplateName: search,
                currentPage: sortAndPage.CurrentPage).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchOnlineTemplate];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();

            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchOnlineTemplate, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(string onlineTemplateName, int pageSize)
        {
            IEnumerable<OnlineTemplateModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords = 0;
                model = _onlineTemplateService.GetOnlineTemplates(out totalRecords, pageSize: pageSize,
                                    sortBy: DEFAULT_SORT_BY, isDescending: true,
                                    onlineTemplateName: onlineTemplateName, currentPage: 1).ToListModel();

                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = 1,
                    IsSortDescending = true,
                    SortBy = DEFAULT_SORT_BY,
                    TotalRecordCount = totalRecords
                };
                ViewBag.SortAndPage = sortAndPage;
                CreateCookieSearch(onlineTemplateName, sortAndPage);
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

            ViewBag.Search = onlineTemplateName;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_TemplateList", model);
        }

        public ActionResult SortAndPaging(string onlineTemplateName, string sortBy, bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<OnlineTemplateModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(onlineTemplateName))
                {
                    onlineTemplateName = onlineTemplateName.Trim();
                }
                int totalRecords;
                model = _onlineTemplateService.GetOnlineTemplates(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            onlineTemplateName: onlineTemplateName, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = onlineTemplateName;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                CreateCookieSearch(onlineTemplateName, sortAndPage);
            }
            return PartialView("_TemplateList", model);
        }

        public ActionResult Create()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            return View(new OnlineTemplateModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OnlineTemplateModel model, HttpPostedFileBase fileUpload)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var maximumSizeBytes = _fileSettings.FileUploadMaximumSizeBytes;
                        var allOwedExtensions = _fileSettings.FileUploadAllowedExtensions;
                        var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);
                        var fileName = Path.GetFileName(fileUpload.FileName);

                        if (!allOwedExtensions.Any(a => a.Contains(fileExt.ToLower())))
                        {
                            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.AllOwedExtensions.NotSupport"));
                            ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.AllOwedExtensions.NotSupport"));
                            return View(model);
                        }

                        if (fileUpload.ContentLength > maximumSizeBytes)
                        {
                            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.MaximumSizeBytes.NotSupport"));
                            ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.MaximumSizeBytes.NotSupport"));
                            return View(model);
                        }

                        var newFile = _fileService.Create(fileName, fileExt, fileUpload.InputStream);
                        model.FileId = newFile.FileId;
                    }

                    var onlineTemplate = model.ToEntity();
                    _onlineTemplateService.Create(onlineTemplate);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.Created.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.OnlineTemplate.Created.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.Created.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.Created.Error"));
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
            //    return RedirectToAction("Index");
            //}

            var onlineTemplate = _onlineTemplateService.GetById(id);
            if (onlineTemplate == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.NotExist"));
                ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.NotExist"));
            }
            else
            {
                try
                {
                    var doctypeTemplates = _doctypeTemplateService.Gets(false, x => x.OnlineTemplateId == onlineTemplate.OnlineTemplateId);
                    if (doctypeTemplates != null && doctypeTemplates.Any())
                    {
                        _doctypeTemplateService.Delete(doctypeTemplates);
                    }

                    _onlineTemplateService.Delete(onlineTemplate);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.Message.Delete.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.OnlineTemplate.Message.Delete.Success"));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.Message.Delete.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.Message.Delete.Error"));
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewOnlineTemplate()
        {
            var model = _onlineTemplateService.GetOnlineTemplates().ToListModel().ToList();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            var entity = _onlineTemplateService.GetById(id);
            if (entity != null)
            {
                var file = _fileService.GetFile(entity.FileId);
                if (file != null)
                {
                    ViewBag.FileName = file.FileName;
                }

                var model = entity.ToModel();
                return View(model);
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.NotExist"));
            ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.NotExist"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OnlineTemplateModel model, HttpPostedFileBase fileUpload)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Level.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var onlineTemplate = _onlineTemplateService.GetById(model.OnlineTemplateId);
                    if (onlineTemplate == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.NotExist"));
                        return RedirectToAction("Index");
                    }

                    model.FileId = onlineTemplate.FileId;

                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var maximumSizeBytes = _fileSettings.FileUploadMaximumSizeBytes;
                        var allOwedExtensions = _fileSettings.FileUploadAllowedExtensions;
                        var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);
                        var fileName = Path.GetFileName(fileUpload.FileName);

                        if (!allOwedExtensions.Any(a => a.Contains(fileExt.ToLower())))
                        {
                            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.AllOwedExtensions.NotSupport"));
                            ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.AllOwedExtensions.NotSupport"));
                            return View(model);
                        }

                        if (fileUpload.ContentLength > maximumSizeBytes)
                        {
                            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.MaximumSizeBytes.NotSupport"));
                            ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.MaximumSizeBytes.NotSupport"));
                            return View(model);
                        }

                        var fileOld = _fileService.GetFile(onlineTemplate.FileId);
                        if (fileOld != null)
                        {
                            _fileService.DeleteFileLocation(fileOld);
                            var newFile = _fileService.Upload(fileUpload.InputStream, fileName, fileExt, 0);
                            fileOld.CreatedOnDate = newFile.CreatedOnDate;
                            fileOld.FileExtension = newFile.FileExtension;
                            fileOld.FileLocationId = newFile.FileLocationId;
                            fileOld.FileLocationKey = newFile.FileLocationKey;
                            fileOld.IdentityFolder = newFile.IdentityFolder;
                            fileOld.Size = newFile.Size;
                            fileOld.FileLocalName = newFile.FileLocalName;
                            fileOld.FileName = newFile.FileName;

                            _fileService.Update(fileOld);
                        }
                        else
                        {
                            var newFile = _fileService.Create(fileName, fileExt, fileUpload.InputStream);
                            model.FileId = newFile.FileId;
                        }
                    }

                    onlineTemplate = model.ToEntity(onlineTemplate);
                    _onlineTemplateService.Update(onlineTemplate);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.OnlineTemplate.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.OnlineTemplate.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.OnlineTemplate.Updated.Error"));
                }
            }
            return View(model);
        }

        public FileResult Download(int id)
        {
            string fileName = string.Empty;
            var stream = _fileService.DownloadFile(out fileName, id);
            if (stream == null || stream.Length <= 0)
            {
                throw new Exception("File is not exist.");
            }
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
    }
}