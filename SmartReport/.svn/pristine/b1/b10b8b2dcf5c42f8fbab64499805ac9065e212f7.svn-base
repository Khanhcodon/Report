using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AjaxExtensions; - public - Framework
    /// Access Modifiers: 
    /// Create Date : 080812
    /// Author      : TrungVH
    /// Description : 1 helper mở rộng cho AjaxHelper
    /// </summary>
    public static class AjaxExtensions
    {
        #region "Pager"
        private const int NumericLinkSize = 5;

        /// <summary>
        /// Phân trang cho 1 table
        /// </summary>
        /// <param name="helper">Ajax helper</param>
        /// <param name="pageSize">Số bản ghi / 1 trang</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="actionName">Tên action (ActionName trong controller)</param>
        /// <param name="cssPagerButton">Tên class css cho nút trang</param>
        /// <param name="cssPagerButtonDisabled">Tên class khi disable nút trang</param>
        /// <param name="cssPagerButtonCurrentPage">Tên class cho trang hiện tại</param>
        /// <param name="valuesDictionary">Các tham số (query string)</param>
        /// <param name="ajaxOptions">Ajax option</param>
        /// <param name="listPageSize">Danh sách pagesize</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this AjaxHelper helper, int pageSize, int currentPage,
                                            int totalRecords, string actionName, string cssPagerButton,
                                            string cssPagerButtonDisabled, string cssPagerButtonCurrentPage,
                                            object valuesDictionary, AjaxOptions ajaxOptions, List<int> listPageSize = null)
        {
            var action = valuesDictionary != null
                             ? new RouteValueDictionary(valuesDictionary)
                             : new RouteValueDictionary();

            return Pager(helper, pageSize, currentPage, totalRecords, actionName, cssPagerButton, cssPagerButtonDisabled,
                         cssPagerButtonCurrentPage, action, ajaxOptions, listPageSize);
        }

        /// <summary>
        /// Phân trang cho 1 table
        /// </summary>
        /// <param name="helper">Ajax helper</param>
        /// <param name="pageSize">Số bản ghi / 1 trang</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="actionName">Tên action (ActionName trong controller)</param>
        /// <param name="cssPagerButton">Tên class css cho nút trang</param>
        /// <param name="cssPagerButtonDisabled">Tên class khi disable nút trang</param>
        /// <param name="cssPagerButtonCurrentPage">Tên class cho trang hiện tại</param>
        /// <param name="valuesDictionary">Các tham số (query string)</param>
        /// <param name="ajaxOptions">Ajax option</param>
        /// <param name="listPageSize">Danh sách pagesize</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this AjaxHelper helper, int pageSize, int currentPage,
                                            int totalRecords, string actionName, string cssPagerButton,
                                            string cssPagerButtonDisabled, string cssPagerButtonCurrentPage,
                                            RouteValueDictionary valuesDictionary, AjaxOptions ajaxOptions, List<int> listPageSize = null)
        {
            var viewContext = helper.ViewContext;
            var action = valuesDictionary ?? new RouteValueDictionary { { "action", actionName } };
            if (!action.ContainsKey("action"))
            {
                action.Add("action", actionName);
            }
            var pageCount = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var sb = new StringBuilder(string.Empty);
            if (pageCount > 1)
            {
                //sb.Append("<div style='float:left; height: 25px;line-height:25px'>");
                //AppendFirstButtonLink(sb, currentPage, pageSize, cssPagerButton,
                //                        cssPagerButtonDisabled, viewContext, action, ajaxOptions);
                //AppendPageContent(sb, pageCount, currentPage, pageSize, cssPagerButtonCurrentPage,
                //                    cssPagerButton, viewContext, action, ajaxOptions);
                //AppendLastButtonLink(sb, currentPage, pageSize, pageCount, cssPagerButton,
                //                        cssPagerButtonDisabled, viewContext, action, ajaxOptions);
                //sb.Append("</div>");

                sb.Append("<nav style=\"float:left\"><ul class='pagination'>");
                AppendFirstButtonLink(sb, currentPage, pageSize, cssPagerButton,
                                        cssPagerButtonDisabled, viewContext, action, ajaxOptions);
                AppendPageContent(sb, pageCount, currentPage, pageSize, cssPagerButtonCurrentPage,
                                    cssPagerButton, viewContext, action, ajaxOptions);
                AppendLastButtonLink(sb, currentPage, pageSize, pageCount, cssPagerButton,
                                        cssPagerButtonDisabled, viewContext, action, ajaxOptions);
                sb.Append("</ul></nav>");
            }
            //page size
            sb.Append(AppendPageSizeDropdown(viewContext, pageSize, action, ajaxOptions, listPageSize));

            return new MvcHtmlString(sb.ToString());
        }

        private static string AppendPageSizeDropdown(ViewContext viewContext, int pageSize,
                                                        RouteValueDictionary action, AjaxOptions ajaxOptions,
                                                        List<int> listPageSize = null)
        {
            var pageLink = new RouteValueDictionary(action) { { "page", 1 } };
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, pageLink);

            if (virtualPathForArea == null)
                return null;
            var stringBuilder = new StringBuilder("<div style='float:left; padding: 21px 0'><form method=\"get\"");

            if (ajaxOptions != null)
                foreach (var ajaxOption in ajaxOptions.ToUnobtrusiveHtmlAttributes())
                    stringBuilder.AppendFormat(" {0}=\"{1}\"", ajaxOption.Key, ajaxOption.Value);

            stringBuilder.AppendFormat(" action=\"{0}\" >",
                                        virtualPathForArea.VirtualPath);

            stringBuilder.Append("<span class=\"lblresultpage\">Số kết quả/trang:</span><select name=\"pageSize\" class=\"form-control input-sm\" style=\"width:80px;\" onchange=\"$(this).parents('form:first').submit()\">");
            if (listPageSize != null && listPageSize.Count > 0)
            {
                foreach (var option in listPageSize)
                {
                    stringBuilder.Append("<option value=\"" + option + "\"");
                    if (option == pageSize)
                    {
                        stringBuilder.Append(" selected=\"selected\"");
                    }
                    stringBuilder.Append(" >" + option + "</option>");
                }
            }
            else
            {
                var option = 0;
                for (var i = 0; i <= 3; i++)
                {
                    var add = i <= 1 ? 25 : 50;
                    option = option + add;
                    stringBuilder.Append("<option value=\"" + option + "\"");
                    if (i == pageSize)
                    {
                        stringBuilder.Append(" selected=\"selected\"");
                    }
                    stringBuilder.Append(" >" + option + "</option>");
                }
            }
            stringBuilder.Append("</select></form></div>");

            return stringBuilder.ToString();
        }

        private static void AppendFirstButtonLink(StringBuilder sb, int currentPage,
                                                    int pageSize, string cssPagerButton,
                                                    string cssPagerButtonDisabled,
                                                    ViewContext viewContext,
                                                    RouteValueDictionary action,
                                                    AjaxOptions ajaxOptions)
        {
            if (currentPage > 1)
            {
                sb.Append(GenerateLink(viewContext, "|&lt;", 1, pageSize,
                                            action, ajaxOptions, cssPagerButton));
                sb.Append(GenerateLink(viewContext, "&lt;&lt;", currentPage - 1,
                                            pageSize, action, ajaxOptions, cssPagerButton));
            }
            else
            {
                //sb.Append("<span class=\"" + cssPagerButtonDisabled + "\">|&lt;</span>");
                //sb.Append("<span class=\"" + cssPagerButtonDisabled + "\">&lt;&lt;</span>");
                sb.Append("<li class=\"disabled " + cssPagerButtonDisabled + "\"><a href='#'>|&lt;</a></li>");
                sb.Append("<li class=\"disabled " + cssPagerButtonDisabled + "\"><a href='#'>&lt;&lt;</a></li>");
            }
        }

        private static void AppendLastButtonLink(StringBuilder sb, int currentPage,
                                                    int pageSize, int pageCount,
                                                    string cssPagerButton, string cssPagerButtonDisabled,
                                                    ViewContext viewContext, RouteValueDictionary action,
                                                    AjaxOptions ajaxOptions)
        {
            if (currentPage < pageCount)
            {
                sb.Append(GenerateLink(viewContext, "&gt;&gt;", currentPage + 1,
                                            pageSize, action, ajaxOptions, cssPagerButton));
                sb.Append(GenerateLink(viewContext, "&gt;|", pageCount,
                                            pageSize, action, ajaxOptions, cssPagerButton));
            }
            else
            {
                //sb.Append("<span class=\"" + cssPagerButtonDisabled + "\">&gt;&gt;</span>");
                //sb.Append("<span class=\"" + cssPagerButtonDisabled + "\">&gt;|</span>");
                sb.Append("<li class=\"disabled " + cssPagerButtonDisabled + "\"><a href='#'>&gt;&gt;</a></li>");
                sb.Append("<li class=\"disabled " + cssPagerButtonDisabled + "\"><a href='#'>&gt;|</a></li>");
            }
        }

        private static void AppendPageContent(StringBuilder sb, int pageCount, int currentPage,
                                                int pageSize,
                                                string cssPagerButtonCurrentPage,
                                                string cssPagerButton, ViewContext viewContext,
                                                RouteValueDictionary action,
                                                AjaxOptions ajaxOptions)
        {
            var numericStart = CalculateStartIndex(currentPage);
            var numericEnd = CalculateEndIndex(numericStart, pageCount);

            AppendPrevButtonsLink(sb, numericStart, pageSize, cssPagerButton,
                                    viewContext, action, ajaxOptions);

            AppendNumericButtons(sb, numericStart, numericEnd, currentPage, pageSize,
                                    cssPagerButtonCurrentPage, cssPagerButton, viewContext, action, ajaxOptions);

            AppendNextButtonsLink(sb, pageCount, numericEnd, pageSize,
                                    cssPagerButton, viewContext, action, ajaxOptions);
        }

        private static void AppendNumericButtons(StringBuilder sb, int numericStart, int numericEnd,
                                                    int currentPage, int pageSize,
                                                    string cssPagerButtonCurrentPage,
                                                    string cssPagerButton, ViewContext viewContext,
                                                    RouteValueDictionary action, AjaxOptions ajaxOptions)
        {
            for (var pageIndex = numericStart; pageIndex <= numericEnd; ++pageIndex)
            {
                if (pageIndex == currentPage)
                {
                    // sb.Append("<span class=\"" + cssPagerButtonCurrentPage + "\">" + pageIndex + "</span>");
                    sb.Append("<li class=\"disabled " + cssPagerButtonCurrentPage + "\"><a href='#'>" + pageIndex + "</a></li>");
                }
                else
                {
                    sb.Append(GenerateLink(viewContext, pageIndex.ToString(CultureInfo.InvariantCulture),
                                                pageIndex, pageSize, action, ajaxOptions, cssPagerButton));
                }
            }
        }

        private static void AppendNextButtonsLink(StringBuilder sb, int pageCount,
                                                    int numericEnd, int pageSize,
                                                    string cssPagerButton, ViewContext viewContext,
                                                    RouteValueDictionary action, AjaxOptions ajaxOptions)
        {
            if (numericEnd >= pageCount) return;

            sb.Append(GenerateLink(viewContext, "...", numericEnd + 1,
                                    pageSize, action, ajaxOptions, cssPagerButton));
        }

        private static void AppendPrevButtonsLink(StringBuilder sb, int numericStart, int pageSize,
                                                    string cssPagerButton, ViewContext viewContext,
                                                    RouteValueDictionary action, AjaxOptions ajaxOptions)
        {
            if (numericStart <= 1) return;
            sb.Append(GenerateLink(viewContext, "...", numericStart - 1, pageSize,
                                    action, ajaxOptions, cssPagerButton));
        }

        private static string GenerateLink(ViewContext viewContext, string linkText,
                                            int pageNumber, int pageSize,
                                            RouteValueDictionary action,
                                            AjaxOptions ajaxOptions,
                                            string cssClass)
        {
            var pageLink = new RouteValueDictionary(action) { { "page", pageNumber }, { "pageSize", pageSize } };
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, pageLink);

            if (virtualPathForArea == null)
                return null;

            // var stringBuilder = new StringBuilder("<a");
            var stringBuilder = new StringBuilder("<li><a");

            if (ajaxOptions != null)
                foreach (var ajaxOption in ajaxOptions.ToUnobtrusiveHtmlAttributes())
                    stringBuilder.AppendFormat(" {0}=\"{1}\"", ajaxOption.Key, ajaxOption.Value);

            // stringBuilder.AppendFormat(" href=\"{0}\" class=\"{1}\">{2}</a>",
            stringBuilder.AppendFormat(" href=\"{0}\" class=\"{1}\">{2}</a></li>",
                                        virtualPathForArea.VirtualPath,
                                        cssClass,
                                        linkText);

            return stringBuilder.ToString();
        }

        private static int CalculateEndIndex(int numericStart, int pageCount)
        {
            var num = numericStart + NumericLinkSize - 1;
            if (num > pageCount)
            {
                num = pageCount;
            }
            return num;
        }

        private static int CalculateStartIndex(int currentPage)
        {
            var num1 = 1;
            if (currentPage > NumericLinkSize)
            {
                var num2 = currentPage % NumericLinkSize;
                num1 = num2 == 0 ? currentPage - NumericLinkSize + 1 : currentPage - num2 + 1;
            }
            return num1;
        }
        #endregion

        #region "SortLink"
        /// <summary>
        /// Tạo link sắp xếp cho 1 cột trong bảng
        /// </summary>
        /// <param name="helper">Ajax helper</param>
        /// <param name="columnName">Tên column trong CSDL</param>
        /// <param name="text">Tên link</param>
        /// <param name="currentSortBy">Tên column hiện tại đang được sắp xếp</param>
        /// <param name="sortDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại: false</param>
        /// <param name="actionName">Tên action (ActionName trong controller)</param>
        /// <param name="cssSortAsc">Tên class css khi sắp xếp từ nhỏ đến lớn</param>
        /// <param name="cssSortDesc">Tên class css khi sắp xếp từ lớn đến nhỏ</param>
        /// <param name="valuesDictionary">Các tham số (query string)</param>
        /// <param name="ajaxOptions">Ajax option</param>
        /// <returns></returns>
        public static MvcHtmlString SortLink(this AjaxHelper helper, string columnName, string text,
                                                string currentSortBy, bool sortDescending, string actionName,
                                                string cssSortAsc, string cssSortDesc,
                                                object valuesDictionary, AjaxOptions ajaxOptions)
        {
            var viewContext = helper.ViewContext;
            var isDescending = string.CompareOrdinal(currentSortBy, columnName) == 0 && !sortDescending;
            var action = valuesDictionary != null
                             ? new RouteValueDictionary(valuesDictionary) 
                               { 
                                    { "action", actionName },
                                    { "sortBy", columnName },
                                    { "isSortDesc", isDescending }
                               }
                             : new RouteValueDictionary
                               { 
                                    { "action", actionName },
                                    { "sortBy", columnName },
                                    { "isSortDesc", isDescending }
                               };
            var classSort = "";
            if (string.CompareOrdinal(currentSortBy, columnName) == 0)
            {
                classSort = sortDescending ? cssSortDesc : cssSortAsc;
            }

            return new MvcHtmlString(GenerateSortLink(viewContext, text, action, ajaxOptions, classSort));
        }

        private static string GenerateSortLink(ViewContext viewContext, string linkText,
                                                RouteValueDictionary action, AjaxOptions ajaxOptions, string cssClass)
        {
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, action);
            if (virtualPathForArea == null)
                return null;

            var stringBuilder = new StringBuilder("<a");

            if (ajaxOptions != null)
                foreach (var ajaxOption in ajaxOptions.ToUnobtrusiveHtmlAttributes())
                {
                    stringBuilder.AppendFormat(" {0}=\"{1}\"", ajaxOption.Key, ajaxOption.Value);
                }

            stringBuilder.AppendFormat(" href=\"{0}\" class=\"{1}\">{2}</a>",
                                        virtualPathForArea.VirtualPath,
                                        cssClass,
                                        linkText);

            return stringBuilder.ToString();
        }
        #endregion
    }
}
