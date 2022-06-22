using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class LogController : CustomController//BaseController
    {
        private readonly LogBll _logService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "CreatedOnDate";

        public LogController(
            LogBll logService,
            AdminGeneralSettings generalSettings,
            ResourceBll resourceService)
            : base()
        {
            _logService = logService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Log.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };
            var search = new LogSearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchLog];
            var isValid = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<LogSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { isValid = true; }
            }

            int totalRecords;
            var model = _logService.GetsAs(out totalRecords,
                                            l => new
                                            {
                                                l.LogId,
                                                l.IpAddress,
                                                l.LogType,
                                                LogTypeValue = ((Entities.LogType)l.LogType),
                                                l.ShortMessage,
                                                l.CreatedOnDate
                                            },
                                            pageSize: sortAndPage.PageSize,
                                            sortBy: sortAndPage.SortBy,
                                            isDescending: sortAndPage.IsSortDescending,
                                            currentPage: sortAndPage.CurrentPage,
                                            from: string.IsNullOrEmpty(search.FromDate)
                                                        ? (DateTime?)null
                                                        : DateTime.Parse(search.FromDate,
                                                                        System.Globalization.CultureInfo.
                                                                            GetCultureInfo("vi-VN").
                                                                            DateTimeFormat),
                                            to: string.IsNullOrEmpty(search.ToDate)
                                                    ? (DateTime?)null
                                                    : DateTime.Parse(search.ToDate,
                                                                    System.Globalization.CultureInfo.
                                                                        GetCultureInfo("vi-VN").
                                                                        DateTimeFormat).AddDays(1).AddSeconds(-1))
                                            .Select(l => new LogModel
                                                    {
                                                        CreatedOnDate = l.CreatedOnDate,
                                                        IpAddress = l.IpAddress,
                                                        ShortMessage = l.ShortMessage,
                                                        LogId = l.LogId,
                                                        LogType = l.LogType,
                                                        LogTypeValue = l.LogTypeValue.ToString()
                                                    });

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Logs = model;
            ViewBag.Search = search;

            if (isValid)
            {
                CreateCookieSearch(search, sortAndPage);
            }
            return View(search);
        }

        private void CreateCookieSearch(LogSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchLog];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchLog, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(LogSearchModel search, int pageSize)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    var model = _logService.GetsAs(out totalRecords,
                                                    l => new
                                                    {
                                                        l.LogId,
                                                        l.IpAddress,
                                                        l.LogType,
                                                        LogTypeValue = ((Entities.LogType)l.LogType),
                                                        l.ShortMessage,
                                                        l.CreatedOnDate
                                                    },
                                                    pageSize: pageSize,
                                                    sortBy: DEFAULT_SORT_BY,
                                                    isDescending: true,
                                                    from: string.IsNullOrEmpty(search.FromDate)
                                                                ? (DateTime?)null
                                                                : DateTime.Parse(search.FromDate,
                                                                                System.Globalization.CultureInfo.
                                                                                    GetCultureInfo("vi-VN").
                                                                                    DateTimeFormat),
                                                    to: string.IsNullOrEmpty(search.ToDate)
                                                            ? (DateTime?)null
                                                            : DateTime.Parse(search.ToDate,
                                                                            System.Globalization.CultureInfo.
                                                                                GetCultureInfo("vi-VN").
                                                                                DateTimeFormat).AddDays(1).AddSeconds(-1))
                                                    .Select(l => new LogModel
                                                    {
                                                        CreatedOnDate = l.CreatedOnDate,
                                                        IpAddress = l.IpAddress,
                                                        ShortMessage = l.ShortMessage,
                                                        LogId = l.LogId,
                                                        LogType = l.LogType,
                                                        LogTypeValue = l.LogTypeValue.ToString()
                                                    });
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                    CreateCookieSearch(search, sortAndPage);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.Logs = model;
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
                    ViewBag.Logs = null;
                }
            }

            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_PartialList");
        }

        public ActionResult SortAndPaging(
            LogSearchModel search, string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                var model = _logService.GetsAs(out totalRecords,
                                                l => new
                                                {
                                                    l.LogId,
                                                    l.IpAddress,
                                                    l.LogType,
                                                    LogTypeValue = ((Entities.LogType)l.LogType),
                                                    l.ShortMessage,
                                                    l.CreatedOnDate
                                                },
                                                pageSize: pageSize,
                                                sortBy: sortBy,
                                                isDescending: isSortDesc,
                                                from: string.IsNullOrEmpty(search.FromDate)
                                                        ? (DateTime?)null
                                                        : DateTime.Parse(search.FromDate,
                                                            System.Globalization.CultureInfo.
                                                                    GetCultureInfo("vi-VN").
                                                                    DateTimeFormat),
                                                to: string.IsNullOrEmpty(search.ToDate)
                                                        ? (DateTime?)null
                                                        : DateTime.Parse(search.ToDate,
                                                            System.Globalization.CultureInfo.
                                                                    GetCultureInfo("vi-VN").
                                                                    DateTimeFormat).AddDays(1).AddSeconds(-1),
                                                currentPage: page)
                                                .Select(l => new LogModel
                                                {
                                                    CreatedOnDate = l.CreatedOnDate,
                                                    IpAddress = l.IpAddress,
                                                    ShortMessage = l.ShortMessage,
                                                    LogId = l.LogId,
                                                    LogType = l.LogType,
                                                    LogTypeValue = l.LogTypeValue.ToString()
                                                });
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(search, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                ViewBag.Logs = model;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            return PartialView("_PartialList");
        }

        public ActionResult View(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Log.NotPermissionView"));
                return RedirectToAction("Index");
            }

            var log = _logService.Get(id);
            if (log == null)
            {
                return RedirectToAction("Index");
            }

            var model = log.ToModel();
            if (!string.IsNullOrWhiteSpace(model.FullMessage))
            {
                var stringWriter = new StringWriter();
                using (var writer = new HtmlTextWriter(stringWriter))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "ErrorDetail");
                    writer.RenderBeginTag(HtmlTextWriterTag.Pre);
                    writer.Flush();
                    MarkupStackTrace(model.FullMessage, writer.InnerWriter);
                    writer.RenderEndTag();
                    writer.WriteLine();
                }
                ViewBag.DetailException = stringWriter.ToString();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ClearLog()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Log.NotPermissionClearLog"));
                return RedirectToAction("Index");
            }

            _logService.ClearLog();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Log.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Log.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var log = _logService.Get(id);
            if (log == null)
            {
                return RedirectToAction("Index");
            }

            _logService.DeleteLog(log);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Logs.Deleted"));
            SuccessNotification(_resourceService.GetResource("Customer.Logs.Deleted"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "LogDeleteCheckBox")]
        public ActionResult DeleteCheckBox(List<int> logids)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Log.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Log.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            _logService.ClearLog(logids);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Logs.Deleted"));
            SuccessNotification(_resourceService.GetResource("Customer.Logs.Deleted"));
            return RedirectToAction("Index");
        }

        private void MarkupStackTrace(string text, TextWriter writer)
        {
            var reStackTrace = new Regex("\r\n                ^\r\n                \\s*\r\n                \\w+ \\s+ \r\n                (?<type> .+ ) \\.\r\n                (?<method> .+? ) \r\n                (?<params> \\( (?<params> .*? ) \\) )\r\n                ( \\s+ \r\n                \\w+ \\s+ \r\n                  (?<file> [a-z] \\: .+? ) \r\n                  \\: \\w+ \\s+ \r\n                  (?<line> [0-9]+ ) \\p{P}? )?\r\n                \\s*\r\n                $", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
            var startIndex = 0;
            foreach (Match match in reStackTrace.Matches(text))
            {
                HtmlEncode(text.Substring(startIndex, match.Index - startIndex), writer);
                MarkupStackFrame(text, match, writer);
                startIndex = match.Index + match.Length;
            }
            HtmlEncode(text.Substring(startIndex), writer);
        }

        private void MarkupStackFrame(string text, Match match, TextWriter writer)
        {
            var index1 = match.Index;
            var groups = match.Groups;
            var group1 = groups["type"];
            HtmlEncode(text.Substring(index1, group1.Index - index1), writer);
            var index2 = group1.Index;
            writer.Write("<span class='st-frame'>");
            var anchor1 = StackFrameSpan(text, index2, "st-type", group1, writer);
            var startIndex1 = StackFrameSpan(text, anchor1, "st-method", groups["method"], writer);
            var group2 = groups["params"];
            HtmlEncode(text.Substring(startIndex1, group2.Index - startIndex1), writer);
            writer.Write("<span class='st-params'>(");
            var num1 = 0;
            var str1 = group2.Captures[0].Value;
            var chArray = new[] { ',' };

            foreach (var str2 in str1.Split(chArray))
            {
                var length = str2.LastIndexOf(' ');
                if (length <= 0)
                {
                    Span(writer, "st-param", str2.Trim());
                }
                else
                {
                    if (num1++ > 0)
                        writer.Write(", ");
                    var str3 = str2.Substring(0, length).Trim();
                    Span(writer, "st-param-type", str3);
                    writer.Write(' ');
                    var str4 = str2.Substring(length + 1).Trim();
                    Span(writer, "st-param-name", str4);
                }
            }
            writer.Write(")</span>");
            var anchor2 = group2.Index + group2.Length;
            var anchor3 = StackFrameSpan(text, anchor2, "st-file", groups["file"], writer);
            var startIndex2 = StackFrameSpan(text, anchor3, "st-line", groups["line"], writer);
            writer.Write("</span>");
            var num2 = match.Index + match.Length;
            HtmlEncode(text.Substring(startIndex2, num2 - startIndex2), writer);
        }

        private int StackFrameSpan(string text, int anchor, string klass, Group group, TextWriter writer)
        {
            return !@group.Success ? anchor : StackFrameSpan(text, anchor, klass, @group.Value, @group.Index, @group.Length, writer);
        }

        private int StackFrameSpan(string text, int anchor, string klass, string value, int index, int length, TextWriter writer)
        {
            HtmlEncode(text.Substring(anchor, index - anchor), writer);
            Span(writer, klass, value);
            return index + length;
        }

        private void Span(TextWriter writer, string klass, string value)
        {
            writer.Write("<span class='");
            writer.Write(klass);
            writer.Write("'>");
            HtmlEncode(value, writer);
            writer.Write("</span>");
        }

        private void HtmlEncode(string text, TextWriter writer)
        {
            Server.HtmlEncode(text, writer);
        }
    }
}
